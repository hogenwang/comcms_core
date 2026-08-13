using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using COMCMS.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using COMCMS.Common.Security;

namespace COMCMS.Core
{
    /// <summary>管理员</summary>
    public partial class Admin : Entity<Admin>, IIdentity
    {
        #region 对象操作
        static Admin()
        {
            // 累加字段
            //Meta.Factory.AdditionalFields.Add(__.Logins);

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            if (isNew && !Dirtys[__.LastLoginIP]) LastLoginIP = Utils.GetIP();
            if (isNew && !Dirtys[__.LastLoginTime]) LastLoginTime = DateTime.Now;
            if (isNew && !Dirtys[__.GroupId]) GroupId = 0;
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // Administrator provisioning is only allowed through the explicit installer.
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        string IIdentity.Name { get; }
        string IIdentity.AuthenticationType { get; }
        bool IIdentity.IsAuthenticated { get; }

        private AdminRoles _Roles;
        /// <summary>用户对应的管理组</summary>
        public AdminRoles Roles
        {
            get
            {
                if (_Roles == null && RoleId > 0 && !Dirtys["AdminRoleId_" + RoleId])
                {
                    _Roles = AdminRoles.Find(AdminRoles._.Id == RoleId);
                    Dirtys["AdminRoleId_" + RoleId] = true;
                }
                return _Roles;
            }
            set { _Roles = value; }
        }
        #endregion

        #region 扩展查询
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion

        #region 静态key
        /// <summary>
        /// Session中后台管理员帐号
        /// </summary>
        public static string sessionAdminNameKey = Utils.PrefixKey + "AdminName";

        /// <summary>
        /// Session中后台管理员ID
        /// </summary>
        public static string sessionAdminIDKey = Utils.PrefixKey + "AdminID";

        /// <summary>
        /// Session中后台管理员是否是超级管理员Key
        /// </summary>
        public static string sessionIsSupperAdminKey = Utils.PrefixKey + "IsSupperAdmin";

        /// <summary>
        /// Session中管理员权限
        /// </summary>
        public static string sessionAdminPowerKey = Utils.PrefixKey + "AdminPower";

        /// <summary>
        /// Session中管理员后台日志ID，本次日志Key
        /// </summary>
        private static string sessionAdminLogIDKey = Utils.PrefixKey + "AdminLogID";

        /// <summary>
        /// Cookies中后台管理员帐号Key
        /// </summary>
        private static string cookiesAdminNameKey = Utils.PrefixKey + "AdminName";

        /// <summary>
        /// cookie 中后台管理员ID Key
        /// </summary>
        private static string cookiesAdminIDKey = Utils.PrefixKey + "AdminID";

        /// <summary>
        /// cookie 中后台管理员信息，加密信息
        /// </summary>
        private static string cookiesAdminInfoKey = Utils.PrefixKey + "AdminInfo";

        private static string cookiesAdminLogIDKey = Utils.PrefixKey + "AdminLogID";

        #endregion

        #region 业务
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <returns>当前管理员实体</returns>
        public static Admin GetMyInfo()
        {
            if (IsAdminLogin())
            {
                return Find(_.Id == ComCmsClaimTypes.GetSubjectId(MyHttpContext.Current.User));
                //return Find(_.UserName == AuthenticationHelper.GetClaim(sessionAdminNameKey));

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 后台管理员登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>是否登录成功</returns>
        public static bool AdminLogin(String userName, String passWord)
        {
            return AdminLogin(userName, passWord, out var ignoredLoginLogId);
        }

        public static bool AdminLogin(String userName, String passWord, out string loginLogId)
        {
            loginLogId = null;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                return false;

            userName = Utils.SqlStr(userName.Trim());
            Guid GUID = System.Guid.NewGuid();
            Admin entity = Find(Admin._.UserName == userName);
            AdminLog log = new AdminLog();
            log.GUID = GUID.ToString();
            log.IsLoginOK = 0;
            log.PassWord = "******";
            log.LoginIP = Utils.GetIP();
            log.LoginTime = DateTime.Now;
            log.UserName = userName.Trim();
            log.LastUpdateTime = DateTime.Now;

            if (entity == null)
            {
                log.Actions = "登录失败：用户名错误。";
                log.Insert();
                return false;
            }
            else
            {
                if (entity.IsLock == 1 || !PasswordHashService.Verify(entity.PassWord, entity.Salt, passWord, out var upgradedHash))
                {
                    log.Actions = "登录失败：密码错误。";
                    log.Insert();
                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(upgradedHash)) entity.PassWord = upgradedHash;
                    entity.LastLoginTime = DateTime.Now;
                    entity.Update();
                    //添加到记录
                    log.IsLoginOK = 1;
                    log.PassWord = "******";
                    log.Insert();
                    loginLogId = GUID.ToString();
                    //写入Session 和 Cookies
                    //SessionHelper.WriteSession("rtadminguid", GUID.ToString());
                    //CookiesHelper.WriteCookie("rtadminguid", GUID.ToString(), 120);
                    // The web layer creates the encrypted ASP.NET Core authentication cookie.
                    //SetAdminInfoAsync(entity.UserName, entity.PassWord, entity.Id, 0, "", GUID.ToString(), entity.Salt).Wait();

                    return true;
                }
            }
        }

        /// <summary>
        /// 验证管理员是否登录
        /// </summary>
        /// <returns>是否登录</returns>
        public static bool IsAdminLogin()
        {
            var principal = MyHttpContext.Current?.User;
            if (principal?.Identity?.IsAuthenticated == true &&
                string.Equals(principal.FindFirstValue(ComCmsClaimTypes.SubjectType), "admin", StringComparison.Ordinal))
            {
                var entity = Find(_.Id == ComCmsClaimTypes.GetSubjectId(principal));
                if (entity == null || entity.IsLock == 1) return false;
                var expectedStamp = SecurityStampService.Compute("admin", entity.Id, entity.PassWord, entity.RoleId, entity.IsLock);
                return SecurityStampService.Equals(expectedStamp, principal.FindFirstValue(ComCmsClaimTypes.SecurityStamp));
            }
            return false;
        }


        /// <summary>
        /// 管理员退出登录，清除信息
        /// </summary>
        public static void ClearInfo()
        {
            //写入 session
            SessionHelper.WriteSession(sessionAdminIDKey, null);
            SessionHelper.WriteSession(sessionAdminNameKey, null);
            SessionHelper.WriteSession(sessionAdminPowerKey, null);
            SessionHelper.WriteSession(sessionIsSupperAdminKey, null);
            SessionHelper.WriteSession(sessionAdminLogIDKey, null);

            //写入cookie 
            CookiesHelper.ClearCookies(cookiesAdminIDKey);
            CookiesHelper.ClearCookies(cookiesAdminNameKey);
            CookiesHelper.ClearCookies(cookiesAdminInfoKey);
            CookiesHelper.ClearCookies(sessionAdminLogIDKey);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="action">动作</param>
        public static void WriteLogActions(string action)
        {
            if (IsAdminLogin())
            {
                string adminLogId = MyHttpContext.Current.User.FindFirstValue(ComCmsClaimTypes.LoginLogId);
                //string adminLogId = AuthenticationHelper.GetClaim(sessionAdminLogIDKey);
                if (string.IsNullOrEmpty(adminLogId))
                {
                    adminLogId = CookiesHelper.GetCookie(cookiesAdminLogIDKey);//日志GUID
                    //adminLogId = AuthenticationHelper.GetClaim(cookiesAdminLogIDKey);//日志GUID
                }
                if (!string.IsNullOrEmpty(adminLogId))
                {
                    AdminLog log = AdminLog.FindByGUID(adminLogId);
                    if (log != null)
                    {
                        if (string.IsNullOrEmpty(log.Actions))
                            log.Actions = $"{DateTime.Now:yyyy-MM-dd HH:mm}: {action}";
                        else
                            log.Actions = log.Actions + $"|||{DateTime.Now:yyyy-MM-dd HH:mm}: {action}";
                        log.LastUpdateTime = DateTime.Now;
                        log.Update();

                    }
                }
            }
        }
        #endregion
    }
}
