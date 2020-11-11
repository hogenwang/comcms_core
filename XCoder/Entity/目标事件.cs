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
    /// <summary>目标事件</summary>
    [Serializable]
    [DataObject]
    [Description("目标事件")]
    [BindTable("TargetEvent", Description = "目标事件", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class TargetEvent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _EventKey;
        /// <summary>事件key</summary>
        [DisplayName("事件key")]
        [Description("事件key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("EventKey", "事件key", "", Master = true)]
        public String EventKey { get => _EventKey; set { if (OnPropertyChanging("EventKey", value)) { _EventKey = value; OnPropertyChanged("EventKey"); } } }

        private String _EventName;
        /// <summary>事件名称</summary>
        [DisplayName("事件名称")]
        [Description("事件名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("EventName", "事件名称", "")]
        public String EventName { get => _EventName; set { if (OnPropertyChanging("EventName", value)) { _EventName = value; OnPropertyChanged("EventName"); } } }

        private Int32 _IsDisable;
        /// <summary>是否禁用</summary>
        [DisplayName("是否禁用")]
        [Description("是否禁用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisable", "是否禁用", "")]
        public Int32 IsDisable { get => _IsDisable; set { if (OnPropertyChanging("IsDisable", value)) { _IsDisable = value; OnPropertyChanged("IsDisable"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }
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
                    case "EventKey": return _EventKey;
                    case "EventName": return _EventName;
                    case "IsDisable": return _IsDisable;
                    case "Rank": return _Rank;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "EventKey": _EventKey = Convert.ToString(value); break;
                    case "EventName": _EventName = Convert.ToString(value); break;
                    case "IsDisable": _IsDisable = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得目标事件字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>事件key</summary>
            public static readonly Field EventKey = FindByName("EventKey");

            /// <summary>事件名称</summary>
            public static readonly Field EventName = FindByName("EventName");

            /// <summary>是否禁用</summary>
            public static readonly Field IsDisable = FindByName("IsDisable");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得目标事件字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>事件key</summary>
            public const String EventKey = "EventKey";

            /// <summary>事件名称</summary>
            public const String EventName = "EventName";

            /// <summary>是否禁用</summary>
            public const String IsDisable = "IsDisable";

            /// <summary>排序</summary>
            public const String Rank = "Rank";
        }
        #endregion
    }
}