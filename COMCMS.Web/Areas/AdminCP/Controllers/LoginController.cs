using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using System.Security.Claims;
using COMCMS.Common.Security;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class LoginController : Controller
    {
        public JsonTip tip = new JsonTip();
        private readonly LoginAttemptService _loginAttempts;
        private readonly ILogger<LoginController> _logger;
        private readonly SecurityEventMetrics _securityMetrics;

        public LoginController(LoginAttemptService loginAttempts, ILogger<LoginController> logger,
            SecurityEventMetrics securityMetrics)
        {
            _loginAttempts = loginAttempts;
            _logger = logger;
            _securityMetrics = securityMetrics;
        }

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
        [EnableRateLimiting("login")]
        public async Task<IActionResult> Login()
        {
            string username = Request.Form["username"].ToString().Trim();
            string password = Request.Form["password"].ToString();
            string code = Request.Form["code"];

            if (Utils.GetSetting("SystemSetting:AdminLoginWithCode") == "1")
            {
                if (string.IsNullOrEmpty(code))
                {
                    tip.Message = "请输入验证码！";
                    return Json(tip);
                }
                if (!VerifyCodeHelper.GetSingleObj().VerifyCodeIsOK(code))
                {
                    tip.Message = "验证码错误！";
                    return Json(tip);
                }
            }

            //验证用户
            if (string.IsNullOrEmpty(username))
            {
                tip.Message = "请输入用户名！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(password) || password.Length > 128)
            {
                tip.Message = "管理员登录密码不能为空且不能超过128个字符！";
                return Json(tip);
            }
            string ip = Utils.GetIP();
            if (await _loginAttempts.IsBlockedAsync(username, ip))
            {
                tip.Message = "错误登录次数限制！";
                return Json(tip);
            }
            //执行登录操作
            if (Admin.AdminLogin(username, password, out var loginLogId))
            {
                var admin = Admin.Find(Admin._.UserName == username);
                await _loginAttempts.RecordSuccessAsync(username);
                var stamp = SecurityStampService.Compute("admin", admin.Id, admin.PassWord, admin.RoleId, admin.IsLock);
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.UserName),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ComCmsClaimTypes.SubjectType, "admin"),
                    new Claim(ComCmsClaimTypes.AdminRoleId, admin.RoleId.ToString()),
                    new Claim(ComCmsClaimTypes.SecurityStamp, stamp),
                    new Claim(ComCmsClaimTypes.LoginLogId, loginLogId),
                    new Claim("auth_time", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
                }, AuthenticationSchemes.AdminCookie);
                await HttpContext.SignInAsync(AuthenticationSchemes.AdminCookie, new ClaimsPrincipal(identity));
                _securityMetrics.AuthenticationAttempt("admin", "cookie", true);

                tip.Status = JsonTip.SUCCESS;
                tip.Message = "登录成功";
                tip.ReturnUrl = "/AdminCP";
                return Json(tip);
            }
            else
            {
                await _loginAttempts.RecordFailureAsync(username, ip);
                _securityMetrics.AuthenticationAttempt("admin", "cookie", false);
                _logger.LogWarning("Admin login failed for account hash {AccountHash} from {RemoteIp}",
                    Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(username.ToUpperInvariant()))), ip);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AuthenticationSchemes.AdminCookie);
            Admin.ClearInfo();
            return Redirect("~/AdminCP/Login");
        }
        #endregion

    }
}
