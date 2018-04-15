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
    /// <summary>用户角色</summary>
    public partial class MemberRoles : Entity<MemberRoles>
    {
        #region 对象操作
        static MemberRoles()
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
            // 货币保留6位小数
            CashBack = Math.Round(CashBack, 2);
            ParentCashBack = Math.Round(ParentCashBack, 2);
            GrandfatherCashBack = Math.Round(GrandfatherCashBack, 2);
            HalvedParentCashBack = Math.Round(HalvedParentCashBack, 2);
            HalvedGrandfatherCashBack = Math.Round(HalvedGrandfatherCashBack, 2);
            YearsPerformance = Math.Round(YearsPerformance, 2);
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化MemberRoles[用户角色]数据……");

            var entity = new MemberRoles();
            entity.RoleName = "钻石代理";
            entity.RoleDescription = "钻石代理(一级代理)";
            entity.Stars = 5;
            entity.NotAllowDel = 1;
            entity.Rank = 0;
            entity.Color = "";
            entity.CashBack = 50;
            entity.ParentCashBack = 10;
            entity.GrandfatherCashBack = 5;
            entity.IsDefault = 0;
            entity.IsHalved = 0;
            entity.HalvedParentCashBack = 10;
            entity.HalvedGrandfatherCashBack = 5;
            entity.YearsPerformance = 100000;
            entity.IsSellers = 1;
            entity.JoinPrice = 5000;
            entity.Insert();

            var entity2 = new MemberRoles();
            entity2.RoleName = "白金代理";
            entity2.RoleDescription = "白金代理(二级代理)";
            entity2.Stars = 4;
            entity2.NotAllowDel = 1;
            entity2.Rank = 0;
            entity2.Color = "";
            entity2.CashBack = 30;
            entity2.ParentCashBack = 10;
            entity2.GrandfatherCashBack = 0;
            entity2.IsDefault = 0;
            entity2.IsHalved = 1;
            entity2.HalvedParentCashBack = 5;
            entity2.HalvedGrandfatherCashBack = 0;
            entity2.YearsPerformance = 50000;
            entity.IsSellers = 1;
            entity.JoinPrice = 0;
            entity2.Insert();

            //var entity3 = new MemberRoles();
            //entity3.RoleName = "白金代理";
            //entity3.RoleDescription = "白金代理(三级代理)";
            //entity3.Stars = 3;
            //entity3.NotAllowDel = 1;
            //entity3.Rank = 0;
            //entity3.Color = "";
            //entity3.CashBack = 25;
            //entity3.ParentCashBack = 0;
            //entity3.GrandfatherCashBack = 0;
            //entity3.IsDefault = 0;
            //entity3.IsHalved = 1;
            //entity3.HalvedParentCashBack = 0;
            //entity3.HalvedGrandfatherCashBack = 0;
            //entity3.YearsPerformance = 30000;
            //entity3.Insert();

            var entity4 = new MemberRoles();
            entity4.RoleName = "普通会员";
            entity4.RoleDescription = "普通会员";
            entity4.Stars = 1;
            entity4.NotAllowDel = 1;
            entity4.Rank = 0;
            entity4.Color = "";
            entity4.CashBack = 0;
            entity4.ParentCashBack = 0;
            entity4.GrandfatherCashBack = 0;
            entity4.IsDefault = 1;
            entity4.IsHalved = 1;
            entity4.HalvedParentCashBack = 0;
            entity4.HalvedGrandfatherCashBack = 0;
            entity4.YearsPerformance = 0;
            entity4.Insert();


            if (XTrace.Debug) XTrace.WriteLine("完成初始化MemberRoles[用户角色]数据！");
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
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static MemberRoles FindById(Int32 id)
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

        #region 业务操作
        #endregion
    }
}