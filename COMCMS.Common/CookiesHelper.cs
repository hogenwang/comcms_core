using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace COMCMS.Common
{
    /// <summary>
    /// Cookies 帮助类
    /// </summary>
    public class CookiesHelper
    {
        public CookiesHelper()
        {
        }

        #region 获取Cookies
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (MyHttpContext.Current.Request.Cookies != null && MyHttpContext.Current.Request.Cookies[strName] != null)
                return HttpUtility.UrlDecode(MyHttpContext.Current.Request.Cookies[strName].ToString());

            return "";
        }
        #endregion

        #region 写入Cookies
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            CookieOptions op = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expires),
                Path = "/",
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            };
            MyHttpContext.Current.Response.Cookies.Append(strName, strValue, op);//用户名
        }
        #endregion

        #region 清除Cookies
        /// <summary>
        /// 移除Cookies
        /// </summary>
        /// <param name="strKey"></param>
        public static void ClearCookies(string strKey)
        {
            CookieOptions op = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(-1),
                Path = "/",
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            };
            MyHttpContext.Current.Response.Cookies.Delete(strKey, op);
        }
        #endregion
    }
}
