using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
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
    public partial class AdminRoles
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _RoleType;
        /// <summary>角色类型</summary>
        [DisplayName("角色类型")]
        [Description("角色类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleType", "角色类型", "")]
        public Int32 RoleType { get => _RoleType; set { if (OnPropertyChanging("RoleType", value)) { _RoleType = value; OnPropertyChanged("RoleType"); } } }

        private String _RoleName;
        /// <summary>角色名称</summary>
        [DisplayName("角色名称")]
        [Description("角色名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RoleName", "角色名称", "", Master = true)]
        public String RoleName { get => _RoleName; set { if (OnPropertyChanging("RoleName", value)) { _RoleName = value; OnPropertyChanged("RoleName"); } } }

        private String _RoleDescription;
        /// <summary>角色简单介绍</summary>
        [DisplayName("角色简单介绍")]
        [Description("角色简单介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("RoleDescription", "角色简单介绍", "", Master = true)]
        public String RoleDescription { get => _RoleDescription; set { if (OnPropertyChanging("RoleDescription", value)) { _RoleDescription = value; OnPropertyChanged("RoleDescription"); } } }

        private Int32 _IsSuperAdmin;
        /// <summary>是否是超级管理员</summary>
        [DisplayName("是否是超级管理员")]
        [Description("是否是超级管理员")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSuperAdmin", "是否是超级管理员", "")]
        public Int32 IsSuperAdmin { get => _IsSuperAdmin; set { if (OnPropertyChanging("IsSuperAdmin", value)) { _IsSuperAdmin = value; OnPropertyChanged("IsSuperAdmin"); } } }

        private Int32 _Stars;
        /// <summary>星级</summary>
        [DisplayName("星级")]
        [Description("星级")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stars", "星级", "")]
        public Int32 Stars { get => _Stars; set { if (OnPropertyChanging("Stars", value)) { _Stars = value; OnPropertyChanged("Stars"); } } }

        private Int32 _NotAllowDel;
        /// <summary>是否不允许删除</summary>
        [DisplayName("是否不允许删除")]
        [Description("是否不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NotAllowDel", "是否不允许删除", "")]
        public Int32 NotAllowDel { get => _NotAllowDel; set { if (OnPropertyChanging("NotAllowDel", value)) { _NotAllowDel = value; OnPropertyChanged("NotAllowDel"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Color", "颜色", "", Master = true)]
        public String Color { get => _Color; set { if (OnPropertyChanging("Color", value)) { _Color = value; OnPropertyChanged("Color"); } } }

        private String _Menus;
        /// <summary>管理菜单</summary>
        [DisplayName("管理菜单")]
        [Description("管理菜单")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Menus", "管理菜单", "", Master = true)]
        public String Menus { get => _Menus; set { if (OnPropertyChanging("Menus", value)) { _Menus = value; OnPropertyChanged("Menus"); } } }

        private String _Powers;
        /// <summary>权限</summary>
        [DisplayName("权限")]
        [Description("权限")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Powers", "权限", "", Master = true)]
        public String Powers { get => _Powers; set { if (OnPropertyChanging("Powers", value)) { _Powers = value; OnPropertyChanged("Powers"); } } }

        private String _AuthorizedArticleCagegory;
        /// <summary>已授权文章栏目</summary>
        [DisplayName("已授权文章栏目")]
        [Description("已授权文章栏目")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("AuthorizedArticleCagegory", "已授权文章栏目", "", Master = true)]
        public String AuthorizedArticleCagegory { get => _AuthorizedArticleCagegory; set { if (OnPropertyChanging("AuthorizedArticleCagegory", value)) { _AuthorizedArticleCagegory = value; OnPropertyChanged("AuthorizedArticleCagegory"); } } }

        private String _AuthorizedCagegory;
        /// <summary>已授权商品栏目</summary>
        [DisplayName("已授权商品栏目")]
        [Description("已授权商品栏目")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("AuthorizedCagegory", "已授权商品栏目", "", Master = true)]
        public String AuthorizedCagegory { get => _AuthorizedCagegory; set { if (OnPropertyChanging("AuthorizedCagegory", value)) { _AuthorizedCagegory = value; OnPropertyChanged("AuthorizedCagegory"); } } }

        private Int32 _OnlyEditMyselfArticle;
        /// <summary>是否只允许编辑自己的文章</summary>
        [DisplayName("是否只允许编辑自己的文章")]
        [Description("是否只允许编辑自己的文章")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OnlyEditMyselfArticle", "是否只允许编辑自己的文章", "")]
        public Int32 OnlyEditMyselfArticle { get => _OnlyEditMyselfArticle; set { if (OnPropertyChanging("OnlyEditMyselfArticle", value)) { _OnlyEditMyselfArticle = value; OnPropertyChanged("OnlyEditMyselfArticle"); } } }

        private Int32 _OnlyEditMyselfProduct;
        /// <summary>是否只允许编辑自己的产品</summary>
        [DisplayName("是否只允许编辑自己的产品")]
        [Description("是否只允许编辑自己的产品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OnlyEditMyselfProduct", "是否只允许编辑自己的产品", "")]
        public Int32 OnlyEditMyselfProduct { get => _OnlyEditMyselfProduct; set { if (OnPropertyChanging("OnlyEditMyselfProduct", value)) { _OnlyEditMyselfProduct = value; OnPropertyChanged("OnlyEditMyselfProduct"); } } }
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
                    case "Id": return _Id;
                    case "RoleType": return _RoleType;
                    case "RoleName": return _RoleName;
                    case "RoleDescription": return _RoleDescription;
                    case "IsSuperAdmin": return _IsSuperAdmin;
                    case "Stars": return _Stars;
                    case "NotAllowDel": return _NotAllowDel;
                    case "Rank": return _Rank;
                    case "Color": return _Color;
                    case "Menus": return _Menus;
                    case "Powers": return _Powers;
                    case "AuthorizedArticleCagegory": return _AuthorizedArticleCagegory;
                    case "AuthorizedCagegory": return _AuthorizedCagegory;
                    case "OnlyEditMyselfArticle": return _OnlyEditMyselfArticle;
                    case "OnlyEditMyselfProduct": return _OnlyEditMyselfProduct;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "RoleType": _RoleType = value.ToInt(); break;
                    case "RoleName": _RoleName = Convert.ToString(value); break;
                    case "RoleDescription": _RoleDescription = Convert.ToString(value); break;
                    case "IsSuperAdmin": _IsSuperAdmin = value.ToInt(); break;
                    case "Stars": _Stars = value.ToInt(); break;
                    case "NotAllowDel": _NotAllowDel = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "Color": _Color = Convert.ToString(value); break;
                    case "Menus": _Menus = Convert.ToString(value); break;
                    case "Powers": _Powers = Convert.ToString(value); break;
                    case "AuthorizedArticleCagegory": _AuthorizedArticleCagegory = Convert.ToString(value); break;
                    case "AuthorizedCagegory": _AuthorizedCagegory = Convert.ToString(value); break;
                    case "OnlyEditMyselfArticle": _OnlyEditMyselfArticle = value.ToInt(); break;
                    case "OnlyEditMyselfProduct": _OnlyEditMyselfProduct = value.ToInt(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>角色类型</summary>
            public static readonly Field RoleType = FindByName("RoleType");

            /// <summary>角色名称</summary>
            public static readonly Field RoleName = FindByName("RoleName");

            /// <summary>角色简单介绍</summary>
            public static readonly Field RoleDescription = FindByName("RoleDescription");

            /// <summary>是否是超级管理员</summary>
            public static readonly Field IsSuperAdmin = FindByName("IsSuperAdmin");

            /// <summary>星级</summary>
            public static readonly Field Stars = FindByName("Stars");

            /// <summary>是否不允许删除</summary>
            public static readonly Field NotAllowDel = FindByName("NotAllowDel");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName("Color");

            /// <summary>管理菜单</summary>
            public static readonly Field Menus = FindByName("Menus");

            /// <summary>权限</summary>
            public static readonly Field Powers = FindByName("Powers");

            /// <summary>已授权文章栏目</summary>
            public static readonly Field AuthorizedArticleCagegory = FindByName("AuthorizedArticleCagegory");

            /// <summary>已授权商品栏目</summary>
            public static readonly Field AuthorizedCagegory = FindByName("AuthorizedCagegory");

            /// <summary>是否只允许编辑自己的文章</summary>
            public static readonly Field OnlyEditMyselfArticle = FindByName("OnlyEditMyselfArticle");

            /// <summary>是否只允许编辑自己的产品</summary>
            public static readonly Field OnlyEditMyselfProduct = FindByName("OnlyEditMyselfProduct");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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

            /// <summary>已授权文章栏目</summary>
            public const String AuthorizedArticleCagegory = "AuthorizedArticleCagegory";

            /// <summary>已授权商品栏目</summary>
            public const String AuthorizedCagegory = "AuthorizedCagegory";

            /// <summary>是否只允许编辑自己的文章</summary>
            public const String OnlyEditMyselfArticle = "OnlyEditMyselfArticle";

            /// <summary>是否只允许编辑自己的产品</summary>
            public const String OnlyEditMyselfProduct = "OnlyEditMyselfProduct";
        }
        #endregion
    }
}