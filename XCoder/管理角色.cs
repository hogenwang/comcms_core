using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>管理角色</summary>
    [Serializable]
    [DataObject]
    [Description("管理角色")]
    [BindTable("AdminRoles", Description = "管理角色", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdminRoles : IAdminRoles
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _RoleType;
        /// <summary>角色类型</summary>
        [DisplayName("角色类型")]
        [Description("角色类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleType", "角色类型", "int")]
        public Int32 RoleType { get { return _RoleType; } set { if (OnPropertyChanging(__.RoleType, value)) { _RoleType = value; OnPropertyChanged(__.RoleType); } } }

        private String _RoleName;
        /// <summary>角色名称</summary>
        [DisplayName("角色名称")]
        [Description("角色名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RoleName", "角色名称", "nvarchar(50)", Master = true)]
        public String RoleName { get { return _RoleName; } set { if (OnPropertyChanging(__.RoleName, value)) { _RoleName = value; OnPropertyChanged(__.RoleName); } } }

        private String _RoleDescription;
        /// <summary>角色简单介绍</summary>
        [DisplayName("角色简单介绍")]
        [Description("角色简单介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("RoleDescription", "角色简单介绍", "nvarchar(250)", Master = true)]
        public String RoleDescription { get { return _RoleDescription; } set { if (OnPropertyChanging(__.RoleDescription, value)) { _RoleDescription = value; OnPropertyChanged(__.RoleDescription); } } }

        private Int32 _IsSuperAdmin;
        /// <summary>是否是超级管理员</summary>
        [DisplayName("是否是超级管理员")]
        [Description("是否是超级管理员")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSuperAdmin", "是否是超级管理员", "int")]
        public Int32 IsSuperAdmin { get { return _IsSuperAdmin; } set { if (OnPropertyChanging(__.IsSuperAdmin, value)) { _IsSuperAdmin = value; OnPropertyChanged(__.IsSuperAdmin); } } }

        private Int32 _Stars;
        /// <summary>星级</summary>
        [DisplayName("星级")]
        [Description("星级")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stars", "星级", "int")]
        public Int32 Stars { get { return _Stars; } set { if (OnPropertyChanging(__.Stars, value)) { _Stars = value; OnPropertyChanged(__.Stars); } } }

        private Int32 _NotAllowDel;
        /// <summary>是否不允许删除</summary>
        [DisplayName("是否不允许删除")]
        [Description("是否不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NotAllowDel", "是否不允许删除", "int")]
        public Int32 NotAllowDel { get { return _NotAllowDel; } set { if (OnPropertyChanging(__.NotAllowDel, value)) { _NotAllowDel = value; OnPropertyChanged(__.NotAllowDel); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "int")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Color", "颜色", "nvarchar(20)", Master = true)]
        public String Color { get { return _Color; } set { if (OnPropertyChanging(__.Color, value)) { _Color = value; OnPropertyChanged(__.Color); } } }

        private String _Menus;
        /// <summary>管理菜单</summary>
        [DisplayName("管理菜单")]
        [Description("管理菜单")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Menus", "管理菜单", "ntext", Master = true)]
        public String Menus { get { return _Menus; } set { if (OnPropertyChanging(__.Menus, value)) { _Menus = value; OnPropertyChanged(__.Menus); } } }

        private String _Powers;
        /// <summary>权限</summary>
        [DisplayName("权限")]
        [Description("权限")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Powers", "权限", "ntext", Master = true)]
        public String Powers { get { return _Powers; } set { if (OnPropertyChanging(__.Powers, value)) { _Powers = value; OnPropertyChanged(__.Powers); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.Id : return _Id;
                    case __.RoleType : return _RoleType;
                    case __.RoleName : return _RoleName;
                    case __.RoleDescription : return _RoleDescription;
                    case __.IsSuperAdmin : return _IsSuperAdmin;
                    case __.Stars : return _Stars;
                    case __.NotAllowDel : return _NotAllowDel;
                    case __.Rank : return _Rank;
                    case __.Color : return _Color;
                    case __.Menus : return _Menus;
                    case __.Powers : return _Powers;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.RoleType : _RoleType = Convert.ToInt32(value); break;
                    case __.RoleName : _RoleName = Convert.ToString(value); break;
                    case __.RoleDescription : _RoleDescription = Convert.ToString(value); break;
                    case __.IsSuperAdmin : _IsSuperAdmin = Convert.ToInt32(value); break;
                    case __.Stars : _Stars = Convert.ToInt32(value); break;
                    case __.NotAllowDel : _NotAllowDel = Convert.ToInt32(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
                    case __.Color : _Color = Convert.ToString(value); break;
                    case __.Menus : _Menus = Convert.ToString(value); break;
                    case __.Powers : _Powers = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得管理角色字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>角色类型</summary>
            public static readonly Field RoleType = FindByName(__.RoleType);

            /// <summary>角色名称</summary>
            public static readonly Field RoleName = FindByName(__.RoleName);

            /// <summary>角色简单介绍</summary>
            public static readonly Field RoleDescription = FindByName(__.RoleDescription);

            /// <summary>是否是超级管理员</summary>
            public static readonly Field IsSuperAdmin = FindByName(__.IsSuperAdmin);

            /// <summary>星级</summary>
            public static readonly Field Stars = FindByName(__.Stars);

            /// <summary>是否不允许删除</summary>
            public static readonly Field NotAllowDel = FindByName(__.NotAllowDel);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName(__.Color);

            /// <summary>管理菜单</summary>
            public static readonly Field Menus = FindByName(__.Menus);

            /// <summary>权限</summary>
            public static readonly Field Powers = FindByName(__.Powers);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得管理角色字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>角色类型</summary>
            public const String RoleType = "RoleType";

            /// <summary>角色名称</summary>
            public const String RoleName = "RoleName";

            /// <summary>角色简单介绍</summary>
            public const String RoleDescription = "RoleDescription";

            /// <summary>是否是超级管理员</summary>
            public const String IsSuperAdmin = "IsSuperAdmin";

            /// <summary>星级</summary>
            public const String Stars = "Stars";

            /// <summary>是否不允许删除</summary>
            public const String NotAllowDel = "NotAllowDel";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>颜色</summary>
            public const String Color = "Color";

            /// <summary>管理菜单</summary>
            public const String Menus = "Menus";

            /// <summary>权限</summary>
            public const String Powers = "Powers";
        }
        #endregion
    }

    /// <summary>管理角色接口</summary>
    public partial interface IAdminRoles
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>角色类型</summary>
        Int32 RoleType { get; set; }

        /// <summary>角色名称</summary>
        String RoleName { get; set; }

        /// <summary>角色简单介绍</summary>
        String RoleDescription { get; set; }

        /// <summary>是否是超级管理员</summary>
        Int32 IsSuperAdmin { get; set; }

        /// <summary>星级</summary>
        Int32 Stars { get; set; }

        /// <summary>是否不允许删除</summary>
        Int32 NotAllowDel { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>颜色</summary>
        String Color { get; set; }

        /// <summary>管理菜单</summary>
        String Menus { get; set; }

        /// <summary>权限</summary>
        String Powers { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}