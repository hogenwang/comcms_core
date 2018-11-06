using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using COMCMS.Common;
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

namespace COMCMS.Core
{
    /// <summary>用户</summary>
    public partial class Member : Entity<Member>
    {
        #region 对象操作
        static Member()
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
            if (isNew && !Dirtys[__.RegIP]) RegIP = Utils.GetIP();
            if (isNew && !Dirtys[__.LastLoginIP]) LastLoginIP = Utils.GetIP();
            if (isNew && !Dirtys[__.ThisLoginIP]) ThisLoginIP = Utils.GetIP();
            if (isNew && !Dirtys[__.Sex]) Sex = 0;
            if (isNew && !Dirtys[__.Birthday]) Birthday = DateTime.Parse("1998-01-01");
            if (isNew && !Dirtys[__.RegTime]) RegTime = DateTime.Now;

            // 货币保留6位小数
            Balance = Math.Round(Balance, 2);
            GiftBalance = Math.Round(GiftBalance, 2);
            YearsPerformance = Math.Round(YearsPerformance, 2);
            ExtCredits1 = Math.Round(ExtCredits1, 2);
            ExtCredits2 = Math.Round(ExtCredits2, 2);
            ExtCredits3 = Math.Round(ExtCredits3, 2);
            ExtCredits4 = Math.Round(ExtCredits4, 2);
            ExtCredits5 = Math.Round(ExtCredits5, 2);
            TotalCredits = Math.Round(TotalCredits, 2);
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Member[用户]数据……");

        //    var entity = new Member();
        //    entity.Id = 0;
        //    entity.UserName = "abc";
        //    entity.PassWord = "abc";
        //    entity.Salt = "abc";
        //    entity.RealName = "abc";
        //    entity.Tel = "abc";
        //    entity.Email = "abc";
        //    entity.RoleId = 0;
        //    entity.LastLoginTime = DateTime.Now;
        //    entity.LastLoginIP = "abc";
        //    entity.ThisLoginTime = DateTime.Now;
        //    entity.ThisLoginIP = "abc";
        //    entity.IsLock = 0;
        //    entity.Nickname = "abc";
        //    entity.UserImg = "abc";
        //    entity.Sex = 0;
        //    entity.Birthday = DateTime.Now;
        //    entity.Phone = "abc";
        //    entity.Fax = "abc";
        //    entity.QQ = "abc";
        //    entity.Weixin = "abc";
        //    entity.Alipay = "abc";
        //    entity.Skype = "abc";
        //    entity.Homepage = "abc";
        //    entity.Company = "abc";
        //    entity.IDNO = "abc";
        //    entity.Country = "abc";
        //    entity.Province = "abc";
        //    entity.City = "abc";
        //    entity.District = "abc";
        //    entity.Address = "abc";
        //    entity.Postcode = "abc";
        //    entity.RegIP = "abc";
        //    entity.RegTime = DateTime.Now;
        //    entity.LoginCount = 0;
        //    entity.RndNum = "abc";
        //    entity.RePassWordTime = DateTime.Now;
        //    entity.Question = "abc";
        //    entity.Answer = "abc";
        //    entity.Balance = 0.0;
        //    entity.GiftBalance = 0.0;
        //    entity.Bank = "abc";
        //    entity.BankCardNO = "abc";
        //    entity.BankBranch = "abc";
        //    entity.BankRealname = "abc";
        //    entity.YearsPerformance = 0.0;
        //    entity.ExtCredits1 = 0.0;
        //    entity.ExtCredits2 = 0.0;
        //    entity.ExtCredits3 = 0.0;
        //    entity.ExtCredits4 = 0.0;
        //    entity.ExtCredits5 = 0.0;
        //    entity.TotalCredits = 0.0;
        //    entity.Parent = 0;
        //    entity.Grandfather = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Member[用户]数据！");
        //}

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
        private MemberRoles _Roles;
        /// <summary>用户对应的用户组</summary>
        public MemberRoles Roles
        {
            get
            {
                if (_Roles == null && RoleId > 0 && !Dirtys["RoleId_" + RoleId])
                {
                    _Roles = MemberRoles.Find(MemberRoles._.Id == RoleId);
                    Dirtys["RoleId_" + RoleId] = true;
                }
                return _Roles;
            }
            set { _Roles = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Member FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.Id == id);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 静态KEY
        /// <summary>
        /// Session 中 用户名 key
        /// </summary>
        public static readonly string KEY_S_UserName = Utils.PrefixKey + "username";
        /// <summary>
        /// Session 中 用户ID key
        /// </summary>
        public static readonly string KEY_S_Uid = Utils.PrefixKey + "uid";

        /// <summary>
        /// Cookies 中 用户名 key
        /// </summary>
        public static readonly string KEY_C_UserName = Utils.PrefixKey + "username";
        /// <summary>
        /// Cookies 中 用户ID key
        /// </summary>
        public static readonly string KEY_C_Uid = Utils.PrefixKey + "uid";
        /// <summary>
        /// Cookies 中 用户信息 key
        /// </summary>
        public static readonly string KEY_C_UserInfo = Utils.PrefixKey + "userinfo";
        /// <summary>
        /// Session 中 用户的OpenId key
        /// </summary>
        public static readonly string KEY_OAUTH_OPENID = Utils.PrefixKey + "openid";
        /// <summary>
        /// Session 中 用户的Access Token
        /// </summary>
        public static readonly string KEY_OAUTH_ACCESS_TOKEN = Utils.PrefixKey + "accesstoken";
        /// <summary>
        /// Session 中 用户的Oauth Type
        /// </summary>
        public static readonly string KEY_OAUTH_TYPE = Utils.PrefixKey + "oauthtype";

        public static readonly string KEY_C_LOGID = Utils.PrefixKey + "log";
        #endregion

        #region 业务
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="userName">用户名，用户ID或者用户Email</param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static bool MemberLogin(String userName, String passWord)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                return false;
            userName = userName.Trim();
            Member entity = new Member();
            Guid GUID = Guid.NewGuid();
            MemberLog log = new MemberLog();
            log.GUID = GUID.ToString();
            log.IsLoginOK = 0;
            log.PassWord = passWord.Trim();
            log.LoginIP = Utils.GetIP();
            log.UId = 0;
            log.LoginTime = DateTime.Now;
            log.UserName = userName.Trim();
            log.LastUpdateTime = DateTime.Now;


            //if (Utils.IsInt(userName) && !Utils.IsTel(userName))//使用UID登录
            //{
            //    //如果数字过大，则用用户名登录
            //    if (userName.Length > 8)
            //        entity = Member.Find(Member._.UserName == userName);
            //    else
            //    {
            //        entity = Member.Find(_.Id, int.Parse(userName));
            //        if (entity == null)
            //            entity = Find(Member._.UserName == userName);
            //    }

            //}
            //else if (Utils.IsTel(userName))
            //{
            //    entity = Member.Find(_.Tel == userName);
            //}
            //else if (Utils.IsValidEmail(userName))
            //{
            //    entity = Member.Find(_.Email, userName);
            //}
            //else
            //{
            //    entity = Member.Find(_.UserName, userName);
            //}
            entity = Member.Find(Member._.UserName == userName);

            if (entity != null && entity.PassWord == Utils.MD5(entity.Salt + passWord.Trim()))
            {
                entity.LoginCount += 1;
                entity.LastLoginIP = entity.ThisLoginIP;
                entity.LastLoginTime = entity.ThisLoginTime;
                entity.ThisLoginIP = Utils.GetIP();
                entity.ThisLoginTime = DateTime.Now;
                entity.Update();

                //记录
                log.UId = entity.Id;
                log.IsLoginOK = 1;
                log.PassWord = "******";
                log.Actions = "用户登录成功。";
                log.Insert();

                SetUserInfo(entity.UserName, entity.PassWord, entity.Id, 60 * 2, entity.Salt, GUID.ToString());
                flag = true;
            }
            else
            {
                log.Actions = "登录失败：用户名或者密码错误。";
                log.Insert();
            }
            return flag;
        }

        /// <summary>
        /// 获取我的资料
        /// </summary>
        /// <returns></returns>
        public static Member GetMyInfo()
        {
            if (IsMemberLogin())
            {
                string uid = SessionHelper.GetSession(KEY_S_Uid).ToString();//ID
                return Member.FindById(int.Parse(uid));
            }
            else
                return null;
        }
        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <returns>是否登录</returns>
        public static bool IsMemberLogin()
        {
            string username = SessionHelper.GetSession(KEY_S_UserName).ToString();//用户名
            string uid = SessionHelper.GetSession(KEY_S_Uid).ToString();//ID
            //使用Oauth 第三方登录
            string oauth_access_token = SessionHelper.GetSession(KEY_OAUTH_ACCESS_TOKEN).ToString();
            string oauth_openid = SessionHelper.GetSession(KEY_OAUTH_OPENID).ToString();

            //如果Session失效，则用Cookies判断
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(uid))
            {
                string cooUserName = CookiesHelper.GetCookie(KEY_C_UserName);//用户名
                string cooUID = CookiesHelper.GetCookie(KEY_C_Uid);//ID
                string cooLoginInfo = CookiesHelper.GetCookie(KEY_C_UserInfo);//信息
                string cooLogID = CookiesHelper.GetCookie(KEY_C_LOGID);//日志GUID

                if (string.IsNullOrEmpty(cooUID) || string.IsNullOrEmpty(cooUserName) || string.IsNullOrEmpty(cooLoginInfo))
                {
                    return false;//信息不完整
                }
                else
                {
                    //全不为空则判断信息是否正确
                    Member model = Find(_.UserName, Utils.SqlStr(cooUserName));
                    if (model == null)
                    {
                        return false;//
                    }
                    else
                    {
                        if (Utils.MD5(model.UserName + model.PassWord + model.Salt) == cooLoginInfo)
                        {
                            SetUserInfo(model.UserName, model.PassWord, model.Id, 60 * 2, model.Salt, cooLogID);
                            return true;
                        }
                        else
                        {
                            ClearUserInfo();//清除用户信息
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
        /// 将用户信息写入Session 和Cookies 保存用户登录状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码（经过md5加密）</param>
        /// <param name="uid">用户ID</param>
        /// <param name="expiresMin">保存时间</param>
        /// <param name="logguid">日志guid</param>
        /// <param name="salt">盐</param>
        public static void SetUserInfo(string username, string password, int uid, int expiresMin, string salt, string logguid)
        {
            //写入 session
            SessionHelper.WriteSession(KEY_S_UserName, username);
            SessionHelper.WriteSession(KEY_S_Uid, uid);
            SessionHelper.WriteSession(KEY_C_LOGID, logguid);

            //写入cookie 
            CookiesHelper.WriteCookie(KEY_C_UserName, username, expiresMin);
            CookiesHelper.WriteCookie(KEY_C_Uid, uid.ToString(), expiresMin);
            CookiesHelper.WriteCookie(KEY_C_UserInfo, Utils.MD5(username + password), expiresMin);

            //2016-6-01 增加IP加密信息，防止cookies被盗用
            CookiesHelper.WriteCookie(KEY_C_UserInfo, Utils.MD5(username + password + salt), expiresMin);
            CookiesHelper.WriteCookie(KEY_C_LOGID, logguid, 60 * 2);
        }
        /// <summary>
        /// 清空我的登录信息
        /// </summary>
        public static void ClearUserInfo()
        {
            //清空 session
            SessionHelper.WriteSession(KEY_S_UserName, null);
            SessionHelper.WriteSession(KEY_S_Uid, null);

            //清空cookie 
            CookiesHelper.ClearCookies(KEY_C_UserName);
            CookiesHelper.ClearCookies(KEY_C_Uid);
            CookiesHelper.ClearCookies(KEY_C_UserInfo);
            CookiesHelper.ClearCookies(KEY_C_LOGID);
        }


        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="action">动作</param>
        public static void WriteLogActions(string action)
        {
            if (IsMemberLogin())
            {
                string LogId = SessionHelper.GetSession(KEY_C_LOGID).ToString();
                if (string.IsNullOrEmpty(LogId))
                {
                    LogId = CookiesHelper.GetCookie(KEY_C_LOGID);//日志GUID
                }
                if (!string.IsNullOrEmpty(LogId))
                {
                    MemberLog log = MemberLog.FindByGUID(LogId);
                    if (log != null)
                    {
                        log.Actions = log.Actions + action;
                        log.LastUpdateTime = DateTime.Now;
                        log.Update();

                    }
                }
            }
        }
        #endregion

        #region 分销提成
        /// <summary>
        /// 分销提成
        /// </summary>
        /// <param name="fromMember">来源用户</param>
        /// <param name="totalprice">总价格</param>
        /// <param name="uid">用户ID</param>
        /// <param name="ordernum">订单号</param>
        /// <param name="fater">父1，爷2</param>
        public static void SaleCommission(int fromUId, decimal totalprice, int uid, string ordernum, int fater = 1, int orderid = 0, decimal realSaleprice = 0M)
        {
            Member he = FindById(uid);
            if (he == null)
                return;
            if (totalprice <= 0)
                return;

            decimal reward = 0M;
            string msg = "";

            Member heParent = Member.Find(Member._.Id == fromUId);
            if (fater == 1)//父的情况
            {
                if (he.Roles.ParentCashBack > 0 && he.IsVerifySellers ==1)
                {
                    reward = totalprice * he.Roles.ParentCashBack / 100;
                }
                msg = $"我的一级代理({heParent.Tel}[{heParent.RealName}])销售￥{realSaleprice.ToString("N2")},我的分销提成：￥{reward.ToString("N2")}";
            }
            else if (fater == 2)//爷级的情况
            {
                if (he.Roles.GrandfatherCashBack > 0 && he.IsVerifySellers == 1)
                {
                    reward = totalprice * he.Roles.GrandfatherCashBack / 100;
                }
                msg = $"我的二级代理({heParent.Tel}[{heParent.RealName}])销售￥{realSaleprice.ToString("N2")},我的分销提成：￥{reward.ToString("N2")}";
            }
            if (reward > 0)//触发分销提成
            {
                ToCashBack(he.Id, reward, msg, orderid, ordernum);
            }
        }
        /// <summary>
        /// 分销提成
        /// </summary>
        /// <param name="uid">目标用户ID</param>
        /// <param name="price">返现金额</param>
        /// <param name="msg">信息日志</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="ordernum">订单号</param>
        public static void ToCashBack(int uid, decimal price, string msg, int orderid = 0, string ordernum = "")
        {
            Member my = Find(Member._.Id == uid);
            if (my != null)
            {
                decimal befor = my.Rebate;
                //减少用户余额
                my.Rebate = my.Rebate + price;
                my.Update();
                //写入记录
                RebateChangeLog log = new RebateChangeLog();
                log.UId = my.Id;
                log.BeforChange = befor;
                log.Reward = price;
                log.AfterChange = my.Rebate;
                log.OrderId = orderid;
                log.OrderNum = ordernum;
                log.Actions = msg;
                log.AddTime = DateTime.Now;
                log.AdminId = 0;
                log.IP = Utils.GetIP();
                log.LogDetails = msg;
                log.TypeId = (int)BalanceChangeLog.XType.分销提成;
                log.UserName = my.UserName;
                log.Insert();
            }
        }
        #endregion

        #region 获取类型
        public static string GetBalanceTypeName(int tid)
        {
            string name = "";
            switch (tid)
            {
                case 0:
                    name = "充值";
                    break;
                case 1:
                    name = "购买广告";
                    break;
                case 4:
                    name = "分销提成";
                    break;
                case 5:
                    name = "扣款";
                    break;
                case 6:
                    name = "提现";
                    break;
            }
            return name;
        }
        #endregion


    }
}