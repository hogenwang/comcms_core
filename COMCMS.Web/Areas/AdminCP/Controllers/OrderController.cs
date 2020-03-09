using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("订单")]
    public class OrderController : AdminBaseController
    {
        #region 订单列表
        [MyAuthorize("viewlist", "order")]
        [DisplayName("订单列表")]
        public IActionResult OrderList()
        {
            Core.Admin.WriteLogActions("查看订单列表;");
            return View();
        }

        [MyAuthorize("viewlist", "order", "JSON")]
        public IActionResult GetOrderList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Order._.Id > 0 & Order._.MyType == (int)Utils.MyType.商品;
            string status = Request.Query["status"];
            if (!string.IsNullOrEmpty(status))
                ex &= Order._.OrderStatus == status;

            string paystatus = Request.Query["paystatus"];
            if (!string.IsNullOrEmpty(paystatus))
                ex &= Order._.PaymentStatus == paystatus;

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= (Order._.Title.Contains(keyword) | Order._.OrderNum.Contains(keyword) | Order._.RealName.Contains(keyword) | Order._.Address.Contains(keyword));

            IList<Order> list = Order.FindAll(ex, null, null, startRowIndex, numPerPage);
            long totalCount = Order.FindCount(ex, null, null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 查看/编辑订单详情
        [MyAuthorize("edit", "order")]
        public IActionResult EditOrder(int id)
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            //获取订单详情
            IList<OrderDetail> list = OrderDetail.FindAll(OrderDetail._.OrderId == entity.Id, null, null, 0, 0);
            ViewBag.list = list;
            //日志
            IList<OrderLog> OrderLogList = OrderLog.FindAll(OrderLog._.OrderId == entity.Id, null, null, 0, 0);
            ViewBag.OrderLogList = OrderLogList;
            Core.Admin.WriteLogActions($"查看订单详情(id:{entity.Id});");
            return View(entity);
        }
        #endregion

        #region 删除订单
        [HttpPost]
        [MyAuthorize("del", "order", "JSON")]
        public IActionResult DelOrder(int id)
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本订单";
                return Json(tip);
            }
            if (entity.PaymentStatus == Utils.PaymentState[1])
            {
                tip.Message = "已经支付订单，不能删除！";
                return Json(tip);
            }
            IList<OrderDetail> list = OrderDetail.FindAll(OrderDetail._.OrderId == entity.Id, null, null, 0, 0);
            IList<OrderLog> OrderLogList = OrderLog.FindAll(OrderLog._.OrderId == entity.Id, null, null, 0, 0);
            OrderLogList.Delete();
            list.Delete();
            Admin.WriteLogActions($"删除订单(id:{id},订单号：{entity.OrderNum},{JsonConvert.SerializeObject(entity)});");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除成功！";
            return Json(tip);
        }
        #endregion

        #region 确认订单
        [HttpPost]
        [MyAuthorize("confirm", "order")]
        public IActionResult ConfirmOrder(int id)
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (entity.OrderStatus != Utils.OrdersState[0])
            {
                tip.Message = "非未确认订单，无法确认订单！";
                return Json(tip);
            }
            entity.OrderStatus = Utils.OrdersState[1];
            entity.LastModTime = DateTime.Now;
            entity.Update();
            Admin admin = Admin.GetMyInfo();

            //写入记录
            OrderLog log = new OrderLog()
            {
                AddTime = DateTime.Now,
                Actions = "管理员确认订单",
                OrderId = entity.Id,
                OrderNum = entity.OrderNum,
                UId = -admin.Id
            };
            log.Insert();
            Admin.WriteLogActions($"确认订单（id:{entity.Id}）;");
            tip.Message = "确认订单成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/AdminCP/Order/EditOrder/" + entity.Id;
            return Json(tip);
        }

        #endregion

        #region 完成订单
        [HttpPost]
        [MyAuthorize("confirm", "order")]
        public IActionResult CompleteOrder(int id)
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (entity.OrderStatus != Utils.OrdersState[1] || entity.PaymentStatus != Utils.PaymentState[1] || string.IsNullOrEmpty(entity.DeliverNO))
            {
                tip.Message = "只有已经确认、支付、发货的订单，才能设置成订单已完成！";
                return Json(tip);
            }
            entity.OrderStatus = Utils.OrdersState[2];
            entity.LastModTime = DateTime.Now;
            entity.Update();

            //执行分销
            decimal totalRechargePrice = entity.TotalPrice;
            Member my = Member.FindById(entity.UId);

            //自己返现
            if (my.Roles.CashBack > 0)
            {
                decimal myreward = entity.TotalPrice * my.Roles.CashBack / 100;
                //totalRechargePrice = totalRechargePrice - myreward;//减去自己的计算
                Member.ToCashBack(my.Id, myreward, $"我的销售返现,消费：￥{entity.TotalPrice.ToString("N2")}；返现比例（{my.Roles.CashBack.ToString("N2")}%）,共：￥{myreward.ToString("N2")}", entity.Id, entity.OrderNum);
            }

            if (my.Parent > 0)
            {
                Member.SaleCommission(my.Id, totalRechargePrice, my.Parent, entity.OrderNum, 1, entity.Id, entity.TotalPrice);
            }
            if (my.Grandfather > 0)
            {
                Member.SaleCommission(my.Id, totalRechargePrice, my.Grandfather, entity.OrderNum, 2, entity.Id, entity.TotalPrice);
            }

            Admin admin = Admin.GetMyInfo();

            //写入记录
            OrderLog log = new OrderLog()
            {
                AddTime = DateTime.Now,
                Actions = "管理员设置订单【已完成】",
                OrderId = entity.Id,
                OrderNum = entity.OrderNum,
                UId = -admin.Id
            };
            log.Insert();
            Admin.WriteLogActions($"完成订单（id:{entity.Id}）;");
            tip.Message = "确认订单已完成成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/AdminCP/Order/EditOrder/" + entity.Id;
            return Json(tip);
        }
        #endregion

        #region 修改运费
        [HttpPost]
        public IActionResult DoChangeOrderFare(int id, decimal fare)
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (entity.PaymentStatus != Utils.PaymentState[0])
            {
                tip.Message = "非未支付订单，无法修改订单运费！";
                return Json(tip);
            }
            if (fare < 0)
            {
                tip.Message = "运费不能小于0！";
                return Json(tip);
            }
            entity.Fare = fare;
            entity.TotalPay = entity.TotalPrice + fare;
            entity.LastModTime = DateTime.Now;
            entity.Update();
            Admin admin = Admin.GetMyInfo();

            //写入记录
            OrderLog log = new OrderLog()
            {
                AddTime = DateTime.Now,
                Actions = $"管理员修改订单运费为：￥{entity.Fare.ToString("N2")}",
                OrderId = entity.Id,
                OrderNum = entity.OrderNum,
                UId = -admin.Id
            };
            log.Insert();
            Admin.WriteLogActions($"修改订单运费（id:{entity.Id},fare:{entity.Fare}）;");
            tip.Message = "修改订单运费成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/AdminCP/Order/EditOrder/" + entity.Id;
            return Json(tip);
        }
        #endregion

        #region 修改物流信息
        [HttpPost]
        public IActionResult DoChangeOrderDeliver(int id, string DeliverType = "", string DeliverNO = "")
        {
            Order entity = Order.Find(Order._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (entity.PaymentStatus == Utils.PaymentState[0])
            {
                tip.Message = "非已支付订单，无法修改订单配送状态！";
                return Json(tip);
            }
            if (entity.OrderStatus == Utils.OrdersState[2] || entity.OrderStatus == Utils.OrdersState[3])
            {
                tip.Message = "订单状态错误，无法修改订单配送状态！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(DeliverNO))
            {
                tip.Message = "请输入运单号！";
                return Json(tip);
            }
            if (entity.DeliverType == DeliverType && entity.DeliverNO == DeliverNO)
            {
                tip.Message = "物流信息并无修改！";
                return Json(tip);
            }
            entity.DeliverType = DeliverType;
            entity.DeliverNO = DeliverNO;
            entity.DeliverStatus = Utils.DeliverState[2];
            entity.LastModTime = DateTime.Now;
            entity.Update();
            Admin admin = Admin.GetMyInfo();
            //写入记录
            OrderLog log = new OrderLog()
            {
                AddTime = DateTime.Now,
                Actions = $"发货：物流：{entity.DeliverType};订单号：{entity.DeliverNO}",
                OrderId = entity.Id,
                OrderNum = entity.OrderNum,
                UId = -admin.Id
            };
            log.Insert();
            Admin.WriteLogActions($"修改订单发货状态：物流：{entity.DeliverType};订单号：{entity.DeliverNO};");
            tip.Message = "修改订单物流配送成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/AdminCP/Order/EditOrder/" + entity.Id;
            return Json(tip);
        }
        #endregion

        #region 在线支付成功记录
        [MyAuthorize("viewlist", "payonline")]
        [DisplayName("支付订单")]
        public IActionResult PayOnlineList()
        {
            Core.Admin.WriteLogActions("查看在线支付订单列表;");
            return View();
        }

        [MyAuthorize("viewlist", "payonline", "JSON")]
        public IActionResult GetPayOnlineList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = OnlinePayOrder._.Id > 0 & OnlinePayOrder._.PaymentStatus == Utils.PaymentState[1];

            string mytype = Request.Query["mytype"];
            if (Utils.IsInt(mytype) && int.Parse(mytype) > 0)
            {
                ex &= OnlinePayOrder._.MyType == int.Parse(mytype);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= OnlinePayOrder._.Title.Contains(keyword);

            IList<OnlinePayOrder> list = OnlinePayOrder.FindAll(ex, null, null, startRowIndex, numPerPage);
            long totalCount = OnlinePayOrder.FindCount(ex, null, null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}