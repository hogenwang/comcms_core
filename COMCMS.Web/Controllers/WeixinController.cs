using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Entities.Request;
using COMCMS.Web.Common;
using Senparc.Weixin.MP;
using NewLife.Log;
using COMCMS.Common;
using Senparc.CO2NET.HttpUtility;
using System.Threading;
using COMCMS.API.Weixin.CustomMessageHandler;
using Senparc.Weixin;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.CO2NET.AspNet.HttpUtility;

namespace COMCMS.Web.Controllers
{
    public class WeixinController : HomeBaseController
    {
        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
        {
            string token = cfg.Token;

            return Task.Factory.StartNew(() =>
                 {
                     if (CheckSignature.Check(signature, timestamp, nonce, token))
                     {
                         return echostr; //返回随机字符串则表示验证通过
                                 }
                     else
                     {
                         return "failed:" + signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, token) + "。" +
                             "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                     }
                 }).ContinueWith<ActionResult>(task => Content(task.Result));
        }


        #region 处理微信接口 消息，事件等
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> Post(PostModel postModel)
        {
            string token = cfg.Token;
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return Content("参数错误！");
            }
            var cancellationToken = new CancellationToken();//给异步方法使用
            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);
            #region 设置消息去重

            /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
             * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
            messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

            messageHandler.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod;

            #endregion
            await messageHandler.ExecuteAsync(cancellationToken); //执行微信处理过程（关键）

            string content = messageHandler.TextResponseMessage.Replace("\r\n", "\n");

            return Content(content, "text/xml");
            //return new FixWeixinBugWeixinResult(messageHandler);

        }
        #endregion

    }
}