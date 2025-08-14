using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Filter;
using XCode;
using NewLife.Log;
using Senparc.Weixin.MP;
using Senparc.Weixin.TenPay.V3;
using Newtonsoft.Json;

namespace COMCMS.Web.Controllers.api
{
    /// <summary>
    /// 支付API
    /// </summary>
    public class PaymentController : APIBaseController
    {
        #region 微信小程序订单支付
        [HttpGet]
        [CheckFilter]
        public ReJson DoWXAppPayOrder(string ordernum)
        {
            //获取订单
            Order entity = Order.Find(Order._.OrderNum == ordernum);
            if (entity == null)
            {
                //reJson.code = 40000;
                //reJson.message = "系统找不到本订单！";
                //return reJson;

                return new ReJson(40000, "系统找不到本订单！");
            }
            //判断订单状态
            if (entity.OrderStatus == Utils.OrdersState[3])
            {
                //reJson.code = 40000;
                //reJson.message = "已完成订单不允许支付！";
                //return reJson;
                return new ReJson(40000, "已完成订单不允许支付！");
            }
            if (entity.PaymentStatus != Utils.PaymentState[0])
            {
                //reJson.code = 40000;
                //reJson.message = "当前订单支付状态不允许支付！";
                //return reJson;
                return new ReJson(40000, "当前订单支付状态不允许支付！");
            }
            //获取用户并判断是否是已经注册用户
            Member my = Member.FindById(entity.UId);
            if (my == null || string.IsNullOrEmpty(my.WeixinAppOpenId))
            {
                //reJson.code = 40000;
                //reJson.message = "用户状态错误，无法使用本功能！";
                //return reJson;
                return new ReJson(40000, "用户状态错误，无法使用本功能！");
            }
            //开始生成支付订单
            OnlinePayOrder model = OnlinePayOrder.Find(OnlinePayOrder._.OrderNum == entity.OrderNum);
            if(model == null)
            {
                model = new OnlinePayOrder();
                model.OrderId = entity.Id;
                model.OrderNum = entity.OrderNum;
                model.PayId = 1;
                model.PaymentNotes = "微信支付";
                model.PaymentStatus = Utils.PaymentState[0];
                model.PayOrderNum = Utils.GetOrderNum();//在线支付订单的订单号
                model.PayType = "微信支付";
                model.TotalPrice = entity.TotalPay;
                model.TotalQty = entity.TotalQty;
                model.UId = entity.UId;
                model.Ip = Utils.GetIP();
                model.IsOK = 0;
                model.AddTime = DateTime.Now;
                model.Insert();
            }


            //写入日志
            OrderLog log = new OrderLog();
            log.AddTime = DateTime.Now;
            log.OrderId = entity.Id;
            log.OrderNum = entity.OrderNum;
            log.UId = entity.UId;
            log.Actions = "用户使用微信支付;支付订单号：" + model.PayOrderNum;
            log.Insert();

            Core.Config cfg = Core.Config.GetSystemConfig();
            string appId = cfg.WXAppId;// ConfigurationManager.AppSettings["WeixinAppId"];
            string appSecrect = cfg.WXAppSecret;// ConfigurationManager.AppSettings["WeixinAppSecrect"];
            string wxmchId = cfg.MCHId;// ConfigurationManager.AppSettings["WeixinMCHId"];
            string wxmchKey = cfg.MCHKey;// ConfigurationManager.AppSettings["WeixinMCHKey"];



            TenPayV3Info TenPayV3Info = new TenPayV3Info(appId, appSecrect, wxmchId, wxmchKey,"","", Utils.GetServerUrl() + "/wxpayment/notify", Utils.GetServerUrl() + "/wxpayment/notify");
            TenPayV3Info.TenPayV3Notify = Utils.GetServerUrl() + "/wxpayment/notify";
            XTrace.WriteLine("微信支付异步通知地址：" + TenPayV3Info.TenPayV3Notify);
            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            var sp_billno = DateTime.Now.ToString("HHmmss") + TenPayV3Util.BuildRandomStr(26);//最多32位
            var nonceStr = TenPayV3Util.GetNoncestr();
            string rtimeStamp = Utils.GetTimeStamp();

            //创建请求统一订单接口参数
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, entity.Title, model.OrderNum, (int)(entity.TotalPay * 100), Utils.GetIP(), TenPayV3Info.TenPayV3Notify,  Senparc.Weixin.TenPay.TenPayV3Type.JSAPI, my.WeixinAppOpenId, TenPayV3Info.Key, nonceStr);

            //返回给微信的请求
            RequestHandler res = new RequestHandler(null);
            try
            {
                //调用统一订单接口
                var result = TenPayV3.Unifiedorder(xmlDataInfo);
                XTrace.WriteLine("微信支付统一下单返回：" + JsonConvert.SerializeObject(result));

                if (result.return_code == "FAIL")
                {
                    //reJson.code = 40005;
                    //reJson.message = result.return_msg;
                    //return reJson;
                    return new ReJson(40005, result.return_msg);
                }
                string nativeReqSign = res.CreateMd5Sign("key", TenPayV3Info.Key);
                //https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=7_7&index=3
                //paySign = MD5(appId=wxd678efh567hg6787&nonceStr=5K8264ILTKCH16CQ2502SI8ZNMTM67VS&package=prepay_id=wx2017033010242291fcfe0db70013231072&signType=MD5&timeStamp=1490840662&key=qazwsxedcrfvtgbyhnujmikolp111111)
                string paySign = Utils.MD5($"appId={TenPayV3Info.AppId}&nonceStr={nonceStr}&package=prepay_id={result.prepay_id}&signType=MD5&timeStamp={rtimeStamp}&key={TenPayV3Info.Key}").ToUpper();

                string package = $"prepay_id={result.prepay_id}";

                dynamic detail = new { timeStamp = rtimeStamp, nonceStr = nonceStr, package = package, signType = "MD5", paySign = paySign };

                //reJson.code = 0;
                //reJson.message = "下单成功！";
                //reJson.detail = detail;
                //return reJson;
                return new ReJson(0, "下单成功！", detail);
            }
            catch (Exception ex)
            {
                res.SetParameter("return_code", "FAIL");
                res.SetParameter("return_msg", "统一下单失败");
                XTrace.WriteLine($"统一下单失败：{ex.Message}");

                //reJson.code = 40005;
                //reJson.message = "统一下单失败，请联系管理员！";
                return new ReJson(40005, "统一下单失败，请联系管理员！");
            }
        }
        #endregion
    }
}