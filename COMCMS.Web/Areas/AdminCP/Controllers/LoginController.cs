using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class LoginController : Controller
    {
        public JsonTip tip = new JsonTip();

        #region 登录页面
        public IActionResult Index()
        {
            if (Admin.IsAdminLogin())
            {
                return Redirect("/AdminCP");
            }

            bool hasData = AdminMenu.FindCount(null, null, null, 0, 0) > 0;
            if (!hasData)
            {
                return Redirect("/Home/Install");
            }

            string key = Utils.GetRandomChar(12);
            SessionHelper.WriteSession("des_key", key);
            ViewBag.key = key;
            return View();
        }
        #endregion

        #region 低版本IE界面
        //判断低版本IE
        public IActionResult Ie()
        {
            return View();
        }
        #endregion

        #region 执行登录
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login()
        {
            string strUserName = Request.Form["username"];
            string strPassWord = Request.Form["password"];

            if (strUserName.Length % 8 != 0)
            {
                tip.Message = "请输入用户名不合法！";
                return Json(tip);
            }
            if (strPassWord.Length % 8 != 0)
            {
                tip.Message = "请输入密码不合法！";
                return Json(tip);
            }
            //判断并解密
            string key = SessionHelper.GetSession("des_key").ToString();
            if (string.IsNullOrEmpty(key))
            {
                tip.Message = "页面访问超时，请刷新页面重新登录！";
                tip.Other = "reload";
                return Json(tip);
            }
            //解密
            string username = "";
            string password = "";
            try
            {
                username = MyDES.uncMe(strUserName, key);
                password = MyDES.uncMe(strPassWord, key);
            }
            catch (Exception exp)
            {
                NewLife.Log.XTrace.WriteException(exp);
                tip.Message = "页面访问超时，请刷新页面重新登录！";
                tip.Other = "reload";
                return Json(tip);
            }

            //验证用户
            if (string.IsNullOrEmpty(username))
            {
                tip.Message = "请输入用户名！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(password) || Utils.GetStringLength(password) < 5)
            {
                tip.Message = "登录密码不能为空或者长度小于5！";
                return Json(tip);
            }
            //如果15分钟内有10次失败登录，则提示错误
            string ip = Utils.GetIP();
            Expression ex = AdminLog._.IsLoginOK == 0 & AdminLog._.LoginIP == ip & AdminLog._.LoginTime >= DateTime.Now.AddMinutes(-15);

            if (AdminLog.FindCount(ex, null, null, 0, 0) >= 10)
            {
                tip.Message = "错误登录次数限制！";
                return Json(tip);
            }
            //执行登录操作
            if (Admin.AdminLogin(username, password))
            {
                tip.Status = JsonTip.SUCCESS;
                tip.Message = "登录成功";
                tip.ReturnUrl = "/AdminCP";
                return Json(tip);
            }
            else
            {
                tip.Message = "用户名或者密码错误！请重新登录！";
                return Json(tip);
            }
        }
        #endregion


        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            Admin.ClearInfo();
            //Admin.ClearInfoAsync().Wait();
            return Redirect("~/AdminCP/Login");
        }
        #endregion

    }
}