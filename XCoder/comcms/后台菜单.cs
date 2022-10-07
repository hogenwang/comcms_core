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
    /// <summary>后台菜单</summary>
    [Serializable]
    [DataObject]
    [Description("后台菜单")]
    [BindTable("AdminMenu", Description = "后台菜单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdminMenu
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _MenuKey;
        /// <summary>标识key</summary>
        [DisplayName("标识key")]
        [Description("标识key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MenuKey", "标识key", "", Master = true)]
        public String MenuKey { get => _MenuKey; set { if (OnPropertyChanging("MenuKey", value)) { _MenuKey = value; OnPropertyChanged("MenuKey"); } } }

        private String _MenuName;
        /// <summary>页面名称</summary>
        [DisplayName("页面名称")]
        [Description("页面名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MenuName", "页面名称", "")]
        public String MenuName { get => _MenuName; set { if (OnPropertyChanging("MenuName", value)) { _MenuName = value; OnPropertyChanged("MenuName"); } } }

        private String _PermissionKey;
        /// <summary>页面名称</summary>
        [DisplayName("页面名称")]
        [Description("页面名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PermissionKey", "页面名称", "")]
        public String PermissionKey { get => _PermissionKey; set { if (OnPropertyChanging("PermissionKey", value)) { _PermissionKey = value; OnPropertyChanged("PermissionKey"); } } }

        private String _Description;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "介绍", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

        private String _Link;
        /// <summary>页面连接地址</summary>
        [DisplayName("页面连接地址")]
        [Description("页面连接地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Link", "页面连接地址", "")]
        public String Link { get => _Link; set { if (OnPropertyChanging("Link", value)) { _Link = value; OnPropertyChanged("Link"); } } }

        private Int32 _PId;
        /// <summary>上级ID</summary>
        [DisplayName("上级ID")]
        [Description("上级ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "上级ID", "")]
        public Int32 PId { get => _PId; set { if (OnPropertyChanging("PId", value)) { _PId = value; OnPropertyChanged("PId"); } } }

        private Int32 _Level;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Level", "级别", "")]
        public Int32 Level { get => _Level; set { if (OnPropertyChanging("Level", value)) { _Level = value; OnPropertyChanged("Level"); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "")]
        public String Location { get => _Location; set { if (OnPropertyChanging("Location", value)) { _Location = value; OnPropertyChanged("Location"); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "")]
        public Int32 IsHide { get => _IsHide; set { if (OnPropertyChanging("IsHide", value)) { _IsHide = value; OnPropertyChanged("IsHide"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }

        private String _Icon;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Icon", "图标", "")]
        public String Icon { get => _Icon; set { if (OnPropertyChanging("Icon", value)) { _Icon = value; OnPropertyChanged("Icon"); } } }

        private String _ClassName;
        /// <summary>样式名称</summary>
        [DisplayName("样式名称")]
        [Description("样式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ClassName", "样式名称", "")]
        public String ClassName { get => _ClassName; set { if (OnPropertyChanging("ClassName", value)) { _ClassName = value; OnPropertyChanged("ClassName"); } } }
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
                    case "MenuKey": return _MenuKey;
                    case "MenuName": return _MenuName;
                    case "PermissionKey": return _PermissionKey;
                    case "Description": return _Description;
                    case "Link": return _Link;
                    case "PId": return _PId;
                    case "Level": return _Level;
                    case "Location": return _Location;
                    case "IsHide": return _IsHide;
                    case "Rank": return _Rank;
                    case "Icon": return _Icon;
                    case "ClassName": return _ClassName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "MenuKey": _MenuKey = Convert.ToString(value); break;
                    case "MenuName": _MenuName = Convert.ToString(value); break;
                    case "PermissionKey": _PermissionKey = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "Link": _Link = Convert.ToString(value); break;
                    case "PId": _PId = value.ToInt(); break;
                    case "Level": _Level = value.ToInt(); break;
                    case "Location": _Location = Convert.ToString(value); break;
                    case "IsHide": _IsHide = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "Icon": _Icon = Convert.ToString(value); break;
                    case "ClassName": _ClassName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得后台菜单字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>标识key</summary>
            public static readonly Field MenuKey = FindByName("MenuKey");

            /// <summary>页面名称</summary>
            public static readonly Field MenuName = FindByName("MenuName");

            /// <summary>页面名称</summary>
            public static readonly Field PermissionKey = FindByName("PermissionKey");

            /// <summary>介绍</summary>
            public static readonly Field Description = FindByName("Description");

            /// <summary>页面连接地址</summary>
            public static readonly Field Link = FindByName("Link");

            /// <summary>上级ID</summary>
            public static readonly Field PId = FindByName("PId");

            /// <summary>级别</summary>
            public static readonly Field Level = FindByName("Level");

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName("Location");

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName("IsHide");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName("Icon");

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName("ClassName");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得后台菜单字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>标识key</summary>
            public const String MenuKey = "MenuKey";

            /// <summary>页面名称</summary>
            public const String MenuName = "MenuName";

            /// <summary>页面名称</summary>
            public const String PermissionKey = "PermissionKey";

            /// <summary>介绍</summary>
            public const String Description = "Description";

            /// <summary>页面连接地址</summary>
            public const String Link = "Link";

            /// <summary>上级ID</summary>
            public const String PId = "PId";

            /// <summary>级别</summary>
            public const String Level = "Level";

            /// <summary>路径</summary>
            public const String Location = "Location";

            /// <summary>是否隐藏</summary>
            public const String IsHide = "IsHide";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>图标</summary>
            public const String Icon = "Icon";

            /// <summary>样式名称</summary>
            public const String ClassName = "ClassName";
        }
        #endregion
    }
}