using System;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Common
{
    /// <summary>
    /// 网站前台基础页
    /// </summary>
    public class HomeBaseController : Controller
    {
        public Config cfg;
        public JsonTip tip = new JsonTip();

        public HomeBaseController()
        {
            cfg = Config.GetSystemConfig();
        }

        [NonAction]
        protected IActionResult RenderContentResult(PublicContentResult result)
        {
            if (result == null)
                return EchoTip("系统找不到本记录！");

            if (result.IsError)
                return EchoTip(result.ErrorMessage);

            if (result.IsRedirect)
                return Redirect(result.RedirectUrl);

            foreach (var pair in result.ViewData)
                ViewData[pair.Key] = pair.Value;

            return View(result.ViewPath, result.Model);
        }

        /// <summary>
        /// 提示并返回（统一走安全视图，禁止直接输出内联脚本）
        /// </summary>
        /// <param name="str">提示内容</param>
        [NonAction]
        protected IActionResult AlertAndGoBack(string str)
        {
            return EchoTip(str ?? string.Empty, false, "/");
        }

        /// <summary>
        /// 提示并跳转到指定本地URL
        /// </summary>
        /// <param name="str">提示内容</param>
        /// <param name="url">跳转地址</param>
        [NonAction]
        protected IActionResult AlertAndRedirect(string str, string url)
        {
            var safeUrl = IsSafeLocalUrl(url) ? url : "/";
            return EchoTip(str ?? string.Empty, false, safeUrl);
        }

        /// <summary>
        /// 显示提示页面
        /// </summary>
        [NonAction]
        protected IActionResult EchoTip(string Message, bool IsSuccess = false, string ReturnURL = "")
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.Message = Message;
            ViewBag.ReturnURL = IsSafeLocalUrl(ReturnURL) ? ReturnURL : "/";
            ViewBag.cfg = cfg;
            return View("EchoTip");
        }

        [NonAction]
        protected bool IsSafeLocalUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;
            if (Url != null) return Url.IsLocalUrl(url);
            return url.StartsWith("/", StringComparison.Ordinal) &&
                   !url.StartsWith("//", StringComparison.Ordinal) &&
                   !url.StartsWith("/\\", StringComparison.Ordinal);
        }
    }
}
