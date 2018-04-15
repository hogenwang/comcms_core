using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class AdminMenu : IAdminMenu
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _MenuKey;
        /// <summary>标识key</summary>
        [DisplayName("标识key")]
        [Description("标识key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MenuKey", "标识key", "nvarchar(50)", Master = true)]
        public String MenuKey { get { return _MenuKey; } set { if (OnPropertyChanging(__.MenuKey, value)) { _MenuKey = value; OnPropertyChanged(__.MenuKey); } } }

        private String _MenuName;
        /// <summary>页面名称</summary>
        [DisplayName("页面名称")]
        [Description("页面名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("MenuName", "页面名称", "nvarchar(50)")]
        public String MenuName { get { return _MenuName; } set { if (OnPropertyChanging(__.MenuName, value)) { _MenuName = value; OnPropertyChanged(__.MenuName); } } }

        private String _PermissionKey;
        /// <summary>页面名称</summary>
        [DisplayName("页面名称")]
        [Description("页面名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PermissionKey", "页面名称", "nvarchar(50)")]
        public String PermissionKey { get { return _PermissionKey; } set { if (OnPropertyChanging(__.PermissionKey, value)) { _PermissionKey = value; OnPropertyChanged(__.PermissionKey); } } }

        private String _Description;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "介绍", "nvarchar(250)")]
        public String Description { get { return _Description; } set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private String _Link;
        /// <summary>页面连接地址</summary>
        [DisplayName("页面连接地址")]
        [Description("页面连接地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Link", "页面连接地址", "nvarchar(250)")]
        public String Link { get { return _Link; } set { if (OnPropertyChanging(__.Link, value)) { _Link = value; OnPropertyChanged(__.Link); } } }

        private Int32 _PId;
        /// <summary>上级ID</summary>
        [DisplayName("上级ID")]
        [Description("上级ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "上级ID", "int")]
        public Int32 PId { get { return _PId; } set { if (OnPropertyChanging(__.PId, value)) { _PId = value; OnPropertyChanged(__.PId); } } }

        private Int32 _Level;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Level", "级别", "int")]
        public Int32 Level { get { return _Level; } set { if (OnPropertyChanging(__.Level, value)) { _Level = value; OnPropertyChanged(__.Level); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "nvarchar(100)")]
        public String Location { get { return _Location; } set { if (OnPropertyChanging(__.Location, value)) { _Location = value; OnPropertyChanged(__.Location); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "int")]
        public Int32 IsHide { get { return _IsHide; } set { if (OnPropertyChanging(__.IsHide, value)) { _IsHide = value; OnPropertyChanged(__.IsHide); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "int")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private String _Icon;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Icon", "图标", "nvarchar(100)")]
        public String Icon { get { return _Icon; } set { if (OnPropertyChanging(__.Icon, value)) { _Icon = value; OnPropertyChanged(__.Icon); } } }

        private String _ClassName;
        /// <summary>样式名称</summary>
        [DisplayName("样式名称")]
        [Description("样式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ClassName", "样式名称", "nvarchar(50)")]
        public String ClassName { get { return _ClassName; } set { if (OnPropertyChanging(__.ClassName, value)) { _ClassName = value; OnPropertyChanged(__.ClassName); } } }
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
                    case __.MenuKey : return _MenuKey;
                    case __.MenuName : return _MenuName;
                    case __.PermissionKey : return _PermissionKey;
                    case __.Description : return _Description;
                    case __.Link : return _Link;
                    case __.PId : return _PId;
                    case __.Level : return _Level;
                    case __.Location : return _Location;
                    case __.IsHide : return _IsHide;
                    case __.Rank : return _Rank;
                    case __.Icon : return _Icon;
                    case __.ClassName : return _ClassName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.MenuKey : _MenuKey = Convert.ToString(value); break;
                    case __.MenuName : _MenuName = Convert.ToString(value); break;
                    case __.PermissionKey : _PermissionKey = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.Link : _Link = Convert.ToString(value); break;
                    case __.PId : _PId = Convert.ToInt32(value); break;
                    case __.Level : _Level = Convert.ToInt32(value); break;
                    case __.Location : _Location = Convert.ToString(value); break;
                    case __.IsHide : _IsHide = Convert.ToInt32(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
                    case __.Icon : _Icon = Convert.ToString(value); break;
                    case __.ClassName : _ClassName = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>标识key</summary>
            public static readonly Field MenuKey = FindByName(__.MenuKey);

            /// <summary>页面名称</summary>
            public static readonly Field MenuName = FindByName(__.MenuName);

            /// <summary>页面名称</summary>
            public static readonly Field PermissionKey = FindByName(__.PermissionKey);

            /// <summary>介绍</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>页面连接地址</summary>
            public static readonly Field Link = FindByName(__.Link);

            /// <summary>上级ID</summary>
            public static readonly Field PId = FindByName(__.PId);

            /// <summary>级别</summary>
            public static readonly Field Level = FindByName(__.Level);

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName(__.Location);

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName(__.Icon);

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName(__.ClassName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>后台菜单接口</summary>
    public partial interface IAdminMenu
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>标识key</summary>
        String MenuKey { get; set; }

        /// <summary>页面名称</summary>
        String MenuName { get; set; }

        /// <summary>页面名称</summary>
        String PermissionKey { get; set; }

        /// <summary>介绍</summary>
        String Description { get; set; }

        /// <summary>页面连接地址</summary>
        String Link { get; set; }

        /// <summary>上级ID</summary>
        Int32 PId { get; set; }

        /// <summary>级别</summary>
        Int32 Level { get; set; }

        /// <summary>路径</summary>
        String Location { get; set; }

        /// <summary>是否隐藏</summary>
        Int32 IsHide { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>图标</summary>
        String Icon { get; set; }

        /// <summary>样式名称</summary>
        String ClassName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}