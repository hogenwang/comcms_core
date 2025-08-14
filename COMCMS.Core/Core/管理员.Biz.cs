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
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化Admin[管理员]数据……");

            var entity = new Admin();
            entity.UserName = "admin";

            entity.Salt = Utils.GetRandomChar(12);
            entity.PassWord = Utils.MD5(entity.Salt + "admin");
            entity.RealName = "admin";
            entity.Tel = "";
            entity.Email = "";
            entity.UserLevel = 100;
            entity.RoleId = 1;
            entity.GroupId = 0;
            entity.LastLoginTime = DateTime.Now;
            entity.LastLoginIP = "127.0.0.1";
            entity.ThisLoginTime = DateTime.Now;
            entity.ThisLoginIP = "127.0.0.1";
            entity.IsLock = 0;
            entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化Admin[管理员]数据！");
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
                return Find(_.UserName == SessionHelper.GetSession(sessionAdminNameKey).ToString());
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
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                return false;

            userName = Utils.SqlStr(userName.Trim());
            Guid GUID = System.Guid.NewGuid();
            Admin entity = Find(Admin._.UserName == userName);
            AdminLog log = new AdminLog();
            log.GUID = GUID.ToString();
            log.IsLoginOK = 0;
            log.PassWord = passWord.Trim();
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
                if (entity.PassWord != Utils.MD5(entity.Salt + passWord.Trim()))
                {
                    log.Actions = "登录失败：密码错误。";
                    log.Insert();
                    return false;
                }
                else
                {
                    entity.LastLoginTime = DateTime.Now;
                    entity.Update();
                    //添加到记录
                    log.IsLoginOK = 1;
                    log.PassWord = "******";
                    log.Insert();
                    //写入Session 和 Cookies
                    //SessionHelper.WriteSession("rtadminguid", GUID.ToString());
                    //CookiesHelper.WriteCookie("rtadminguid", GUID.ToString(), 120);
                    SetAdminInfo(entity.UserName, entity.PassWord, entity.Id, 0, "", GUID.ToString(), entity.Salt);
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
            string adminName = SessionHelper.GetSession(sessionAdminNameKey).ToString();//用户名
            string adminID = SessionHelper.GetSession(sessionAdminIDKey).ToString();//ID
            //string adminName = AuthenticationHelper.GetClaim(sessionAdminNameKey);//用户名
            //string adminID = AuthenticationHelper.GetClaim(sessionAdminIDKey);//ID

            //如果Session失效，则用Cookies判断
            if (string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(adminID))
            {
                string cooAdminName = CookiesHelper.GetCookie(cookiesAdminNameKey);//用户名
                string cooAdminID = CookiesHelper.GetCookie(cookiesAdminIDKey);//ID
                string cooLoginInfo = CookiesHelper.GetCookie(cookiesAdminInfoKey);//信息
                string cooAdminLogID = CookiesHelper.GetCookie(cookiesAdminLogIDKey);//日志GUID
                //string cooAdminName = AuthenticationHelper.GetClaim(cookiesAdminNameKey);//用户名
                //string cooAdminID = AuthenticationHelper.GetClaim(cookiesAdminIDKey);//ID
                //string cooLoginInfo = AuthenticationHelper.GetClaim(cookiesAdminInfoKey);//信息
                //string cooAdminLogID = AuthenticationHelper.GetClaim(cookiesAdminLogIDKey);//日志GUID

                if (string.IsNullOrEmpty(cooAdminID) || string.IsNullOrEmpty(cooAdminName) || string.IsNullOrEmpty(cooLoginInfo) || string.IsNullOrEmpty(cooAdminLogID))
                {
                    return false;//信息不完整
                }
                else
                {
                    //全不为空则判断信息是否正确
                    Admin model = Find(Admin._.UserName == Utils.SqlStr(cooAdminName));// FindByName(Utils.SqlStr(cooAdminName));
                    if (model == null)
                    {
                        return false;//找不到管理员
                    }
                    else
                    {
                        if (Utils.MD5(model.UserName + model.PassWord + model.Salt + Utils.GetIP()) == cooLoginInfo)
                        {
                            //信息正确，重建session
                            //获取日志ID
                            if (AdminLog.FindByGUID(cooAdminLogID) == null)
                            {
                                //ClearInfo();//清除信息
                                ClearInfoAsync().Wait();
                                return false;//日志出错
                            }

                            //重新写入Session 和 Cookies
                            SetAdminInfo(model.UserName, model.PassWord, model.Id, 0, "", cooAdminLogID, model.Salt);

                            //SetAdminInfoAsync(model.UserName, model.PassWord, model.Id, 0, "", cooAdminLogID, model.Salt).Wait();

                            return true;
                        }
                        else
                        {
                            ClearInfo();//清除信息
                            //ClearInfoAsync().Wait();
                            return false;//信息错误
                        }
                    }
                }
            }
            else
            {
                return true;//Session未失效，正确
            }
        }

        /// <summary>
        /// 设置管理员信息，写入Session 和Cookies
        /// </summary>
        /// <param name="adminName">管理员帐号</param>
        /// <param name="adminPwd">管理员密码，经过加密后的</param>
        /// <param name="adminID">管理员ID</param>
        /// <param name="isSupperAdmin">是否是超级管理员</param>
        /// <param name="adminPower">管理员管理权限</param>
        /// <param name="adminLogID">后台日志ID</param>
        /// <param name="adminSalt">加密盐</param>
        public static void SetAdminInfo(string adminName, string adminPwd, int adminID, int isSupperAdmin, string adminPower, string adminLogID, string adminSalt)
        {
            //写入 session
            SessionHelper.WriteSession(sessionAdminIDKey, adminID.ToString());
            SessionHelper.WriteSession(sessionAdminNameKey, adminName);
            SessionHelper.WriteSession(sessionAdminPowerKey, adminPower);
            SessionHelper.WriteSession(sessionIsSupperAdminKey, isSupperAdmin.ToString());
            SessionHelper.WriteSession(sessionAdminLogIDKey, adminLogID);

            //写入cookie 
            CookiesHelper.WriteCookie(cookiesAdminIDKey, adminID.ToString(), 120);
            CookiesHelper.WriteCookie(cookiesAdminNameKey, adminName, 120);
            //2016-6-01 增加IP加密信息，防止cookies被盗用
            CookiesHelper.WriteCookie(cookiesAdminInfoKey, Utils.MD5(adminName + adminPwd + adminSalt + Utils.GetIP()), 120);
            CookiesHelper.WriteCookie(sessionAdminLogIDKey, adminLogID, 120);
        }

        /// <summary>
        /// 设置管理员信息，写入Session 和Cookies
        /// </summary>
        /// <param name="isSupperAdmin">是否是超级管理员</param>
        /// <param name="adminPower">管理员管理权限</param>
        /// <param name="adminLogID">后台日志ID</param>
        public static async Task SetAdminInfoAsync(string adminName, string adminPwd, int adminID, int isSupperAdmin, string adminPower, string adminLogID, string adminSalt)
        {
            var expiresUtc = DateTimeOffset.Now.AddMinutes(60 * 2);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(sessionAdminIDKey, adminID.ToString()),
                new Claim(sessionAdminNameKey, adminName),
                new Claim(sessionAdminPowerKey, adminPower),
                new Claim(sessionIsSupperAdminKey, isSupperAdmin.ToString()),
                new Claim(sessionAdminLogIDKey, adminLogID),

                new Claim(cookiesAdminIDKey, adminID.ToString()),
                new Claim(cookiesAdminNameKey, adminName),
                //2016-6-01 增加IP加密信息，防止cookies被盗用
                new Claim(cookiesAdminInfoKey, Utils.MD5(adminName + adminPwd + adminSalt + Utils.GetIP())),
            });

            var genericPrincipal = new ClaimsPrincipal(claimsIdentity);

           await AuthenticationHelper.SignInAsync(genericPrincipal, expiresUtc);
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
        /// 管理员退出登录，清除信息
        /// </summary>
        /// <returns></returns>
        public static async Task ClearInfoAsync()
        {
            await AuthenticationHelper.SignOutAsync();
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="action">动作</param>
        public static void WriteLogActions(string action)
        {
            if (IsAdminLogin())
            {
                string adminLogId = SessionHelper.GetSession(sessionAdminLogIDKey).ToString();
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