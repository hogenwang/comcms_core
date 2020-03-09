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
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.RoleId);

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            // 货币保留6位小数
            Balance = Math.Round(Balance, 6);
            GiftBalance = Math.Round(GiftBalance, 6);
            Rebate = Math.Round(Rebate, 6);
            YearsPerformance = Math.Round(YearsPerformance, 6);
            ExtCredits1 = Math.Round(ExtCredits1, 6);
            ExtCredits2 = Math.Round(ExtCredits2, 6);
            ExtCredits3 = Math.Round(ExtCredits3, 6);
            ExtCredits4 = Math.Round(ExtCredits4, 6);
            ExtCredits5 = Math.Round(ExtCredits5, 6);
            TotalCredits = Math.Round(TotalCredits, 6);
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

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
        //    entity.Qq = "abc";
        //    entity.Weixin = "abc";
        //    entity.Alipay = "abc";
        //    entity.Skype = "abc";
        //    entity.Homepage = "abc";
        //    entity.Company = "abc";
        //    entity.Idno = "abc";
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
        //    entity.Rebate = 0.0;
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
        //    entity.IsSellers = 0;
        //    entity.IsVerifySellers = 0;
        //    entity.WeixinOpenId = "abc";
        //    entity.WeixinAppOpenId = "abc";
        //    entity.QQOpenId = "abc";
        //    entity.WeiboOpenId = "abc";
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
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Member FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion
    }
}