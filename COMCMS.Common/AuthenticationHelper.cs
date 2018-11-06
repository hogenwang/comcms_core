using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Common
{
    /// <summary>
    /// 认证助手
    /// </summary>
    public class AuthenticationHelper
    {
        /*
         登陆过程

         //信息类型集合，比如姓名、性别、出生日期
         var claims = new[]
         {
             new Claim("UserName",username)
         };

         //信息类型集合组合成一张证件，比如身份证、驾照、护照
         var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

         //持有者，拥有以上证件的持有者，同时有身份证、驾照、护照、教师证的一个人
         var user = new ClaimsPrincipal(claimsIdentity);

         //注册改证件持有者
         HttpContext.SignInAsync(
             CookieAuthenticationDefaults.AuthenticationScheme, 
             user,
             new AuthenticationProperties
             {
                 IsPersistent = true,//session跨多个请求,否则只在当前请求有效
                 ExpiresUtc = (remember ?? false) ? (DateTimeOffset?)null : DateTimeOffset.Now.AddMilliseconds(60 * 2),//过期时间，null表示不过期
             }).Wait();
         */

        private static ClaimsPrincipal _user => _httpContext.User;
        private static HttpContext _httpContext => MyHttpContext.Current;

        #region 获取Claim

        /// <summary>
        /// 获取一项claim信息
        /// </summary>
        /// <param name="claimType">claim名称</param>
        /// <returns></returns>
        public static string GetClaim(string claimType)
        {
            if (_user != null && _user.HasClaim(c => c.Type == claimType))
            {
                return _user.FindFirst(claimType).Value;
            }

            return "";
        }

        #endregion

        #region 登录

      /// <summary>
      /// 登录
      /// </summary>
      /// <param name="claims">身份信息项集合</param>
      /// <param name="expiresUtc">过期时间，null为不过期</param>
      /// <param name="authenticationScheme">认证方式，默认为cookie方式</param>
      /// <returns></returns>
        public static async  Task SignInAsync(Claim[] claims, DateTimeOffset? expiresUtc, 
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var claimsIdentity = new ClaimsIdentity(claims, authenticationScheme);
            var user = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true, //session跨多个请求,否则只在当前请求有效
                ExpiresUtc = expiresUtc, //过期时间，null表示不过期
            };

            await _httpContext.SignInAsync(authenticationScheme, user, authenticationProperties);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="claimsPrincipal">身份证持有者</param>
        /// <param name="expiresUtc">过期时间，null为不过期</param>
        /// <param name="authenticationScheme">认证方式，默认为cookie方式</param>
        /// <returns></returns>
        public static async Task SignInAsync(ClaimsPrincipal claimsPrincipal, DateTimeOffset? expiresUtc,
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true, //session跨多个请求,否则只在当前请求有效
                ExpiresUtc = expiresUtc, //过期时间，null表示不过期
            };

            await _httpContext.SignInAsync(authenticationScheme, claimsPrincipal, authenticationProperties);
        }

        #endregion

        #region 注销

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="authenticationScheme">认证方式</param>
        /// <returns></returns>
        public static async Task SignOutAsync(string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            await _httpContext.SignOutAsync(authenticationScheme);
        }

        #endregion
    }
}
