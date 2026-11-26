using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Entities.Request;
using COMCMS.Web.Common;
using Senparc.Weixin.MP;
using COMCMS.API.Weixin.CustomMessageHandler;
using Senparc.NeuChar.MessageHandlers;
using Senparc.CO2NET.AspNet.HttpUtility;

namespace COMCMS.Web.Controllers
{
    public class WeixinController : HomeBaseController
    {
        private const long MaxTimestampDriftSeconds = 300;

        [HttpGet]
        [ActionName("Index")]
        public IActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            string token = cfg?.Token;
            if (string.IsNullOrWhiteSpace(token) ||
                string.IsNullOrWhiteSpace(signature) ||
                string.IsNullOrWhiteSpace(timestamp) ||
                string.IsNullOrWhiteSpace(nonce) ||
                !IsTimestampFresh(timestamp))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "invalid signature");
            }

            if (CheckSignature.Check(signature, timestamp, nonce, token))
                return Content(echostr ?? string.Empty, "text/plain");

            return StatusCode(StatusCodes.Status403Forbidden, "invalid signature");
        }

        #region 处理微信接口 消息，事件等
        [HttpPost]
        [ExternalWebhook]
        [ActionName("Index")]
        public async Task<IActionResult> Post(PostModel postModel)
        {
            string token = cfg?.Token;
            if (postModel == null ||
                string.IsNullOrWhiteSpace(token) ||
                !IsTimestampFresh(postModel.Timestamp) ||
                !CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "参数错误！");
            }

            var cancellationToken = HttpContext?.RequestAborted ?? CancellationToken.None;
            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10)
            {
                OmitRepeatedMessage = true,
                DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod
            };

            await messageHandler.ExecuteAsync(cancellationToken);

            string content = (messageHandler.TextResponseMessage ?? string.Empty).Replace("\r\n", "\n");
            return Content(content, "text/xml");
        }
        #endregion

        internal static bool IsTimestampFresh(string timestamp)
        {
            if (!long.TryParse(timestamp, out var unixSeconds)) return false;
            var drift = Math.Abs(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - unixSeconds);
            return drift <= MaxTimestampDriftSeconds;
        }
    }
}
