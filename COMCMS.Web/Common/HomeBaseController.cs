using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;

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

        /// <summary>
        /// Alert提示并返回
        /// </summary>
        /// <param name="str">提示内容</param>
        public IActionResult AlertAndGoBack(string str)
        {
            if (!string.IsNullOrWhiteSpace(str)) str = str.Replace("'", "\\'");
            return Content($"<script type=\"text/javascript\">alert('{str}');window.history.go(-1)</script>", "text/html", System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// Alert提示并跳转到指定URL
        /// </summary>
        /// <param name="str">提示内容</param>
        /// <param name="url">跳转地址</param>
        public IActionResult AlertAndRedirect(string str, string url)
        {
            if (!string.IsNullOrWhiteSpace(str)) str = str.Replace("'", "\\'");
            return Content($"<script type=\"text/javascript\">alert('{str}');window.location='{url}'</script>", "text/html", System.Text.Encoding.UTF8);
        }

        #region MyRegion
        /// <summary>
        /// 显示提示页面
        /// </summary>
        /// <param name="Message">提示语</param>
        /// <param name="IsSuccess">成功/失败</param>
        /// <param name="ReturnURL">返回URL</param>
        /// <returns></returns>
        public IActionResult EchoTip(string Message, bool IsSuccess = false, string ReturnURL = "")
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.Message = Message;
            ViewBag.ReturnURL = ReturnURL;
            ViewBag.cfg = cfg;
            return View("EchoTip");
        }
        #endregion
    }
}