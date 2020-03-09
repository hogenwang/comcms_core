using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Core;
using COMCMS.Common;
using XCode;
using NewLife.Log;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.TenPay.V3;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using COMCMS.Web.Common;

namespace COMCMS.Web.Controllers
{
    public class wxpaymentController : HomeBaseController
    {
        #region 微信支付异步通知
        /// <summary>
        /// 微信支付异步通知
        /// </summary>
        /// <returns></returns>
        public IActionResult notify()
        {
            XTrace.WriteLine("微信支付异步通知开始：");
            try
            {
                
                ResponseHandler resHandler = new ResponseHandler(null);
                string return_code = resHandler.GetParameter("return_code");
                string return_msg = resHandler.GetParameter("return_msg");

                //配置
                Core.Config cfg = Core.Config.GetSystemConfig();
                string appId = cfg.WXAppId;// ConfigurationManager.AppSettings["WeixinAppId"];
                string appSecrect = cfg.WXAppSecret;// ConfigurationManager.AppSettings["WeixinAppSecrect"];
                string wxmchId = cfg.MCHId;// ConfigurationManager.AppSettings["WeixinMCHId"];
                string wxmchKey = cfg.MCHKey;// ConfigurationManager.AppSettings["WeixinMCHKey"];

                TenPayV3Info TenPayV3Info = new TenPayV3Info(appId, appSecrect, wxmchId, wxmchKey,"","", Utils.GetServerUrl() + "/wxpayment/notify", Utils.GetServerUrl() + "/wxpayment/notify");
                string res = null;

                resHandler.SetKey(TenPayV3Info.Key);
                //验证请求是否从微信发过来（安全）
                if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
                {
                    res = "success";//正确的订单处理
                                    //直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
                    string out_trade_no = resHandler.GetParameter("out_trade_no");//商户订单号
                    XTrace.WriteLine("微信异步通知订单号：" + out_trade_no + "；" + JsonConvert.SerializeObject(resHandler));

                    OnlinePayOrder payOrder = OnlinePayOrder.Find(OnlinePayOrder._.OrderNum == out_trade_no);
                    if (payOrder == null)
                    {
                        XTrace.WriteLine($"支付成功，但是支付订单不存在：{out_trade_no}");
                        res = "wrong";//错误的订单处理
                    }
                    else
                    {
                        if (payOrder.PaymentStatus == Utils.PaymentState[0])
                        {
                            //更新支付订单
                            payOrder.PaymentStatus = Utils.PaymentState[1];
                            payOrder.ReceiveTime = DateTime.Now;
                            payOrder.OutTradeNo = out_trade_no;
                            payOrder.IsOK = 1;
                            payOrder.Update();

                            //获取订单
                            Order order = Order.Find(Order._.OrderNum == payOrder.OrderNum);
                            if (order != null)
                            {
                                order.PaymentStatus = Utils.PaymentState[1];
                                order.PayType = "微信支付";
                                order.OutTradeNo = out_trade_no;

                                if (order.MyType == (int)Utils.MyType.分销商认证)
                                {
                                    order.OrderStatus = Utils.OrdersState[2];
                                }
                                order.Update();
                                //如果是属于升级会员的，那要修改会员状态
                                if (order.MyType == (int)Utils.MyType.分销商认证 && order.OrderType > 0)
                                {
                                    Member he = Member.FindById(order.UId);
                                    if (he.RoleId != order.OrderType)
                                    {
                                        he.RoleId = order.OrderType;
                                        he.IsVerifySellers = 1;
                                        he.Update();
                                    }
                                }
                                //写入订单记录
                                OrderLog log = new OrderLog();
                                log.AddTime = DateTime.Now;
                                log.OrderId = order.Id;
                                log.OrderNum = order.OrderNum;
                                log.UId = order.UId;
                                log.Actions = "微信支付成功;订单号：" + order.OrderNum + ";金额：" + order.TotalPay.ToString("N2");
                                log.Insert();
                            }
                        }
                    }
                }
                else
                {
                    res = "wrong";//错误的订单处理
                }

                #region 记录日志
                XTrace.WriteLine($"微信支付回调处理结果：{res}");
                #endregion

                string xml = string.Format(@"<xml>
<return_code><![CDATA[{0}]]></return_code>
<return_msg><![CDATA[{1}]]></return_msg>
</xml>", return_code, return_msg);
                return Content(xml, "text/xml");
            }
            catch (Exception ex)
            {
                new WeixinException(ex.Message, ex);
                throw;
            }
        }
        #endregion

    }
}