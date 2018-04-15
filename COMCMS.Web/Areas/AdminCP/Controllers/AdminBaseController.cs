using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class AdminBaseController : Controller
    {
        public JsonTip tip = new JsonTip();

        /// <summary>
        /// 如果没登录
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool islogin = Admin.IsAdminLogin();
            if (!islogin)
            {
                context.Result = new RedirectResult("/AdminCP/Login");
                return;
            }
            base.OnActionExecuting(context);
        }

        #region 获取验证第一条错误
        /// <summary>
        /// 获取服务端验证的第一条错误信息
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }
        #endregion

        #region 信息提示页面
        /// <summary>
        /// 显示提示页面
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">类型，0失败，1成功</param>
        /// <param name="isCloseDialog">是否关闭掉dialog</param>
        /// <param name="backURL">后退地址，为空则默认上一页</param>
        /// <returns></returns>
        public IActionResult EchoTipPage(string message, int type = 0, bool isCloseDialog = false, string backURL = "")
        {
            ViewBag.Message = message;
            ViewBag.Type = type;
            ViewBag.IsCloseDialog = isCloseDialog;
            ViewBag.BackURL = backURL;
            return View("TipPage");
        }
        #endregion
    }
}