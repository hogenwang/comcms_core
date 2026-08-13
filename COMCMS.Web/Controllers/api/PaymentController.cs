using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Senparc.Weixin.TenPay.V3;
using Microsoft.AspNetCore.Authorization;
using COMCMS.Common.Security;
using COMCMS.Web.Services;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace COMCMS.Web.Controllers.api
{
    /// <summary>
    /// 支付API
    /// </summary>
    public class PaymentController : APIBaseController
    {
        private static readonly TimeSpan IdempotencyLifetime = TimeSpan.FromMinutes(15);
        private readonly PaymentIdempotencyService _idempotency;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(PaymentIdempotencyService idempotency, ILogger<PaymentController> logger)
        {
            _idempotency = idempotency;
            _logger = logger;
        }

        #region 微信小程序订单支付
        [HttpPost]
        [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
        public async Task<ActionResult<ReJson>> DoWXAppPayOrder([FromBody] PayOrderRequest request)
        {
            var ordernum = request?.OrderNum?.Trim();
            if (string.IsNullOrEmpty(ordernum)) return BadRequest(new ReJson(40000, "订单号不能为空！"));
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var memberId))
                return Unauthorized(new ReJson(401, "会员认证无效！"));

            var idempotencyKey = Request.Headers["Idempotency-Key"].ToString();
            if (idempotencyKey.Length < 16 || idempotencyKey.Length > 128)
                return BadRequest(new ReJson(40000, "请提供有效的 Idempotency-Key！"));

            //获取订单
            Order entity = Order.Find(Order._.OrderNum == ordernum);
            if (entity == null)
            {
                //reJson.code = 40000;
                //reJson.message = "系统找不到本订单！";
                //return reJson;

                return NotFound(new ReJson(404, "系统找不到本订单！"));
            }
            if (entity.UId != memberId)
            {
                return StatusCode(403, new ReJson(403, "无权操作该订单！"));
            }
            //判断订单状态
            if (entity.OrderStatus == Utils.OrdersState[3])
            {
                //reJson.code = 40000;
                //reJson.message = "已完成订单不允许支付！";
                //return reJson;
                return BadRequest(new ReJson(40000, "已完成订单不允许支付！"));
            }
            if (entity.PaymentStatus != Utils.PaymentState[0])
            {
                //reJson.code = 40000;
                //reJson.message = "当前订单支付状态不允许支付！";
                //return reJson;
                return BadRequest(new ReJson(40000, "当前订单支付状态不允许支付！"));
            }
            //获取用户并判断是否是已经注册用户
            Member my = Member.FindById(entity.UId);
            if (my == null || string.IsNullOrEmpty(my.WeixinAppOpenId))
            {
                //reJson.code = 40000;
                //reJson.message = "用户状态错误，无法使用本功能！";
                //return reJson;
                return BadRequest(new ReJson(40000, "用户状态错误，无法使用本功能！"));
            }

            if (!await _idempotency.TryAcquireAsync(memberId, ordernum, idempotencyKey, IdempotencyLifetime))
                return Conflict(new ReJson(409, "该支付请求已处理，请勿重复提交！"));

            var keepIdempotencyKey = false;
            try
            {
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
            var nonceStr = TenPayV3Util.GetNoncestr();
            string rtimeStamp = Utils.GetTimeStamp();

            //创建请求统一订单接口参数
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, entity.Title, model.OrderNum, (int)(entity.TotalPay * 100), Utils.GetIP(), TenPayV3Info.TenPayV3Notify,  Senparc.Weixin.TenPay.TenPayV3Type.JSAPI, my.WeixinAppOpenId, TenPayV3Info.Key, nonceStr);

                //调用统一订单接口
                var result = TenPayV3.Unifiedorder(xmlDataInfo);
                _logger.LogInformation("WeChat unified order completed for order {OrderNumber} with result {ResultCode}",
                    entity.OrderNum, result.return_code);

                if (result.return_code == "FAIL")
                {
                    //reJson.code = 40005;
                    //reJson.message = result.return_msg;
                    //return reJson;
                    return StatusCode(502, new ReJson(40005, "支付平台未能受理请求。"));
                }
                //https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=7_7&index=3
                //paySign = MD5(appId=wxd678efh567hg6787&nonceStr=5K8264ILTKCH16CQ2502SI8ZNMTM67VS&package=prepay_id=wx2017033010242291fcfe0db70013231072&signType=MD5&timeStamp=1490840662&key=qazwsxedcrfvtgbyhnujmikolp111111)
                string paySign = Utils.MD5($"appId={TenPayV3Info.AppId}&nonceStr={nonceStr}&package=prepay_id={result.prepay_id}&signType=MD5&timeStamp={rtimeStamp}&key={TenPayV3Info.Key}").ToUpper();

                string package = $"prepay_id={result.prepay_id}";

                dynamic detail = new { timeStamp = rtimeStamp, nonceStr = nonceStr, package = package, signType = "MD5", paySign = paySign };

                keepIdempotencyKey = true;

                //reJson.code = 0;
                //reJson.message = "下单成功！";
                //reJson.detail = detail;
                //return reJson;
                return new ReJson(0, "下单成功！", detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeChat unified order failed for order {OrderNumber}", entity.OrderNum);

                //reJson.code = 40005;
                //reJson.message = "统一下单失败，请联系管理员！";
                return StatusCode(502, new ReJson(40005, "统一下单失败，请稍后重试！"));
            }
            finally
            {
                if (!keepIdempotencyKey)
                    await _idempotency.ReleaseAsync(memberId, ordernum, idempotencyKey);
            }
        }
        #endregion

        public sealed class PayOrderRequest
        {
            public string OrderNum { get; set; }
        }
    }
}
