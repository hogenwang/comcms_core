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
    /// <summary>后台菜单对应的事件权限</summary>
    [Serializable]
    [DataObject]
    [Description("后台菜单对应的事件权限")]
    [BindTable("AdminMenuEvent", Description = "后台菜单对应的事件权限", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdminMenuEvent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _MenuId;
        /// <summary>菜单ID</summary>
        [DisplayName("菜单ID")]
        [Description("菜单ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MenuId", "菜单ID", "")]
        public Int32 MenuId { get => _MenuId; set { if (OnPropertyChanging("MenuId", value)) { _MenuId = value; OnPropertyChanged("MenuId"); } } }

        private String _MenuKey;
        /// <summary>菜单key</summary>
        [DisplayName("菜单key")]
        [Description("菜单key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MenuKey", "菜单key", "", Master = true)]
        public String MenuKey { get => _MenuKey; set { if (OnPropertyChanging("MenuKey", value)) { _MenuKey = value; OnPropertyChanged("MenuKey"); } } }

        private Int32 _EventId;
        /// <summary>事件ID</summary>
        [DisplayName("事件ID")]
        [Description("事件ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EventId", "事件ID", "")]
        public Int32 EventId { get => _EventId; set { if (OnPropertyChanging("EventId", value)) { _EventId = value; OnPropertyChanged("EventId"); } } }

        private String _EventKey;
        /// <summary>事件key</summary>
        [DisplayName("事件key")]
        [Description("事件key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("EventKey", "事件key", "", Master = true)]
        public String EventKey { get => _EventKey; set { if (OnPropertyChanging("EventKey", value)) { _EventKey = value; OnPropertyChanged("EventKey"); } } }

        private String _EventName;
        /// <summary>事件名称</summary>
        [DisplayName("事件名称")]
        [Description("事件名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("EventName", "事件名称", "")]
        public String EventName { get => _EventName; set { if (OnPropertyChanging("EventName", value)) { _EventName = value; OnPropertyChanged("EventName"); } } }
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
                    case "MenuId": return _MenuId;
                    case "MenuKey": return _MenuKey;
                    case "EventId": return _EventId;
                    case "EventKey": return _EventKey;
                    case "EventName": return _EventName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "MenuId": _MenuId = value.ToInt(); break;
                    case "MenuKey": _MenuKey = Convert.ToString(value); break;
                    case "EventId": _EventId = value.ToInt(); break;
                    case "EventKey": _EventKey = Convert.ToString(value); break;
                    case "EventName": _EventName = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>菜单ID</summary>
            public static readonly Field MenuId = FindByName("MenuId");

            /// <summary>菜单key</summary>
            public static readonly Field MenuKey = FindByName("MenuKey");

            /// <summary>事件ID</summary>
            public static readonly Field EventId = FindByName("EventId");

            /// <summary>事件key</summary>
            public static readonly Field EventKey = FindByName("EventKey");

            /// <summary>事件名称</summary>
            public static readonly Field EventName = FindByName("EventName");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}