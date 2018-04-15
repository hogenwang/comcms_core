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
    /// <summary>管理角色</summary>
    public partial class AdminRoles : Entity<AdminRoles>
    {
        #region 对象操作
        static AdminRoles()
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
            if (isNew && !Dirtys[__.IsSuperAdmin]) IsSuperAdmin = 0;
            if (isNew && !Dirtys[__.Rank]) Rank = 0;
            if (isNew && !Dirtys[__.NotAllowDel]) NotAllowDel = 1;
            if (isNew && !Dirtys[__.RoleType]) RoleType = 0;
            if (isNew && !Dirtys[__.Stars]) Stars = 3;
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化AdminRoles[管理角色]数据……");

            var entity = new AdminRoles();
            entity.RoleType = 0;
            entity.RoleName = "超级管理员";
            entity.RoleDescription = "系统超级管理员";
            entity.IsSuperAdmin = 1;
            entity.Stars = 5;
            entity.NotAllowDel = 1;
            entity.Rank = 0;
            entity.Color = "";
            entity.Menus = "";
            entity.Powers = "";
            entity.Insert();

            if (XTrace.Debug) XTrace.WriteLine("完成初始化AdminRoles[管理角色]数据！");
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
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion
    }
}