using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>后台菜单对应的事件权限</summary>
    [Serializable]
    [DataObject]
    [Description("后台菜单对应的事件权限")]
    [BindTable("AdminMenuEvent", Description = "后台菜单对应的事件权限", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdminMenuEvent : IAdminMenuEvent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _MenuId;
        /// <summary>菜单ID</summary>
        [DisplayName("菜单ID")]
        [Description("菜单ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MenuId", "菜单ID", "int")]
        public Int32 MenuId { get { return _MenuId; } set { if (OnPropertyChanging(__.MenuId, value)) { _MenuId = value; OnPropertyChanged(__.MenuId); } } }

        private String _MenuKey;
        /// <summary>菜单key</summary>
        [DisplayName("菜单key")]
        [Description("菜单key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MenuKey", "菜单key", "nvarchar(100)", Master = true)]
        public String MenuKey { get { return _MenuKey; } set { if (OnPropertyChanging(__.MenuKey, value)) { _MenuKey = value; OnPropertyChanged(__.MenuKey); } } }

        private Int32 _EventId;
        /// <summary>事件ID</summary>
        [DisplayName("事件ID")]
        [Description("事件ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EventId", "事件ID", "int")]
        public Int32 EventId { get { return _EventId; } set { if (OnPropertyChanging(__.EventId, value)) { _EventId = value; OnPropertyChanged(__.EventId); } } }

        private String _EventKey;
        /// <summary>事件key</summary>
        [DisplayName("事件key")]
        [Description("事件key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("EventKey", "事件key", "nvarchar(100)", Master = true)]
        public String EventKey { get { return _EventKey; } set { if (OnPropertyChanging(__.EventKey, value)) { _EventKey = value; OnPropertyChanged(__.EventKey); } } }

        private String _EventName;
        /// <summary>事件名称</summary>
        [DisplayName("事件名称")]
        [Description("事件名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("EventName", "事件名称", "nvarchar(100)")]
        public String EventName { get { return _EventName; } set { if (OnPropertyChanging(__.EventName, value)) { _EventName = value; OnPropertyChanged(__.EventName); } } }
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
                    case __.MenuId : return _MenuId;
                    case __.MenuKey : return _MenuKey;
                    case __.EventId : return _EventId;
                    case __.EventKey : return _EventKey;
                    case __.EventName : return _EventName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.MenuId : _MenuId = Convert.ToInt32(value); break;
                    case __.MenuKey : _MenuKey = Convert.ToString(value); break;
                    case __.EventId : _EventId = Convert.ToInt32(value); break;
                    case __.EventKey : _EventKey = Convert.ToString(value); break;
                    case __.EventName : _EventName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得后台菜单对应的事件权限字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>菜单ID</summary>
            public static readonly Field MenuId = FindByName(__.MenuId);

            /// <summary>菜单key</summary>
            public static readonly Field MenuKey = FindByName(__.MenuKey);

            /// <summary>事件ID</summary>
            public static readonly Field EventId = FindByName(__.EventId);

            /// <summary>事件key</summary>
            public static readonly Field EventKey = FindByName(__.EventKey);

            /// <summary>事件名称</summary>
            public static readonly Field EventName = FindByName(__.EventName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得后台菜单对应的事件权限字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>菜单ID</summary>
            public const String MenuId = "MenuId";

            /// <summary>菜单key</summary>
            public const String MenuKey = "MenuKey";

            /// <summary>事件ID</summary>
            public const String EventId = "EventId";

            /// <summary>事件key</summary>
            public const String EventKey = "EventKey";

            /// <summary>事件名称</summary>
            public const String EventName = "EventName";
        }
        #endregion
    }

    /// <summary>后台菜单对应的事件权限接口</summary>
    public partial interface IAdminMenuEvent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>菜单ID</summary>
        Int32 MenuId { get; set; }

        /// <summary>菜单key</summary>
        String MenuKey { get; set; }

        /// <summary>事件ID</summary>
        Int32 EventId { get; set; }

        /// <summary>事件key</summary>
        String EventKey { get; set; }

        /// <summary>事件名称</summary>
        String EventName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}