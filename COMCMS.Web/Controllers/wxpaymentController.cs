using System;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Core;
using COMCMS.Common;
using XCode;
using NewLife.Log;
using Senparc.Weixin.TenPay.V3;
using COMCMS.Web.Common;

namespace COMCMS.Web.Controllers
{
    public class wxpaymentController : HomeBaseController
    {
        public const string XmlSuccessResponse = "<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>";
        public const string XmlFailResponse = "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[INVALID]]></return_msg></xml>";

        #region 微信支付异步通知
        /// <summary>
        /// 微信支付异步通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ExternalWebhook]
        public IActionResult notify()
        {
            XTrace.WriteLine("微信支付异步通知开始。");
            try
            {
                Core.Config sysCfg = Core.Config.GetSystemConfig();
                if (sysCfg == null ||
                    string.IsNullOrWhiteSpace(sysCfg.WXAppId) ||
                    string.IsNullOrWhiteSpace(sysCfg.MCHId) ||
                    string.IsNullOrWhiteSpace(sysCfg.MCHKey))
                {
                    XTrace.WriteLine("微信支付配置不完整，拒绝处理回调。");
                    return Content(XmlFailResponse, "text/xml");
                }

                ResponseHandler resHandler = new ResponseHandler(HttpContext);
                resHandler.SetKey(sysCfg.MCHKey);

                string return_code = resHandler.GetParameter("return_code");
                string result_code = resHandler.GetParameter("result_code");
                string notifyAppId = resHandler.GetParameter("appid");
                string notifyMchId = resHandler.GetParameter("mch_id");
                string out_trade_no = resHandler.GetParameter("out_trade_no");
                string transaction_id = resHandler.GetParameter("transaction_id");
                string total_fee_raw = resHandler.GetParameter("total_fee");

                if (!resHandler.IsTenpaySign() ||
                    !IsValidNotifyPayload(return_code, result_code, notifyAppId, sysCfg.WXAppId, notifyMchId, sysCfg.MCHId, out_trade_no, total_fee_raw, out var notifiedFeeFen))
                {
                    XTrace.WriteLine($"微信支付回调签名或业务参数校验失败：order={out_trade_no}");
                    return Content(XmlFailResponse, "text/xml");
                }

                OnlinePayOrder payOrder = OnlinePayOrder.Find(OnlinePayOrder._.OrderNum == out_trade_no);
                if (payOrder == null)
                {
                    XTrace.WriteLine($"微信支付回调订单不存在：{out_trade_no}");
                    return Content(XmlFailResponse, "text/xml");
                }

                if (!IsAmountMatched(payOrder.TotalPrice, notifiedFeeFen))
                {
                    XTrace.WriteLine($"微信支付回调金额不一致：order={out_trade_no}, actualFen={notifiedFeeFen}");
                    return Content(XmlFailResponse, "text/xml");
                }

                if (payOrder.PaymentStatus == Utils.PaymentState[1])
                {
                    return Content(XmlSuccessResponse, "text/xml");
                }

                using var transaction = new EntityTransaction<OnlinePayOrder>();
                var now = DateTime.Now;
                var tradeRef = string.IsNullOrWhiteSpace(transaction_id) ? out_trade_no : transaction_id;
                var claimed = OnlinePayOrder.Update(
                    new[] { OnlinePayOrder.__.PaymentStatus, OnlinePayOrder.__.ReceiveTime, OnlinePayOrder.__.OutTradeNo, OnlinePayOrder.__.IsOK },
                    new object[] { Utils.PaymentState[1], now, tradeRef, 1 },
                    new[] { OnlinePayOrder.__.Id, OnlinePayOrder.__.PaymentStatus },
                    new object[] { payOrder.Id, Utils.PaymentState[0] });

                if (claimed == 1)
                {
                    Order order = Order.Find(Order._.OrderNum == payOrder.OrderNum);
                    if (order != null)
                    {
                        order.PaymentStatus = Utils.PaymentState[1];
                        order.PayType = "微信支付";
                        order.OutTradeNo = tradeRef;

                        if (order.MyType == (int)Utils.MyType.分销商认证)
                        {
                            order.OrderStatus = Utils.OrdersState[2];
                        }
                        order.Update();

                        if (order.MyType == (int)Utils.MyType.分销商认证 && order.OrderType > 0)
                        {
                            Member he = Member.FindById(order.UId);
                            if (he != null && he.RoleId != order.OrderType)
                            {
                                he.RoleId = order.OrderType;
                                he.IsVerifySellers = 1;
                                he.Update();
                            }
                        }

                        OrderLog log = new OrderLog
                        {
                            AddTime = now,
                            OrderId = order.Id,
                            OrderNum = order.OrderNum,
                            UId = order.UId,
                            Actions = "微信支付成功;订单号：" + order.OrderNum + ";金额：" + order.TotalPay.ToString("N2")
                        };
                        log.Insert();
                    }
                }
                transaction.Commit();

                XTrace.WriteLine($"微信支付回调处理成功：order={out_trade_no}, tx={transaction_id}, claimed={claimed}");
                return Content(XmlSuccessResponse, "text/xml");
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return Content(XmlFailResponse, "text/xml");
            }
        }
        #endregion

        internal static bool IsValidNotifyPayload(
            string returnCode,
            string resultCode,
            string notifyAppId,
            string expectedAppId,
            string notifyMchId,
            string expectedMchId,
            string outTradeNo,
            string totalFeeRaw,
            out int notifiedFeeFen)
        {
            notifiedFeeFen = 0;
            return string.Equals(returnCode, "SUCCESS", StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(resultCode, "SUCCESS", StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(notifyAppId, expectedAppId, StringComparison.Ordinal) &&
                   string.Equals(notifyMchId, expectedMchId, StringComparison.Ordinal) &&
                   !string.IsNullOrWhiteSpace(outTradeNo) &&
                   int.TryParse(totalFeeRaw, out notifiedFeeFen) &&
                   notifiedFeeFen > 0;
        }

        internal static bool IsAmountMatched(decimal expectedYuan, int actualFen)
        {
            var expectedFen = (int)Math.Round(expectedYuan * 100m, MidpointRounding.AwayFromZero);
            return expectedFen > 0 && expectedFen == actualFen;
        }
    }
}
