using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class TargetEvent : ITargetEvent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _EventKey;
        /// <summary>事件key</summary>
        [DisplayName("事件key")]
        [Description("事件key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("EventKey", "事件key", "nvarchar(50)", Master = true)]
        public String EventKey { get { return _EventKey; } set { if (OnPropertyChanging(__.EventKey, value)) { _EventKey = value; OnPropertyChanged(__.EventKey); } } }

        private String _EventName;
        /// <summary>事件名称</summary>
        [DisplayName("事件名称")]
        [Description("事件名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("EventName", "事件名称", "nvarchar(50)")]
        public String EventName { get { return _EventName; } set { if (OnPropertyChanging(__.EventName, value)) { _EventName = value; OnPropertyChanged(__.EventName); } } }

        private Int32 _IsDisable;
        /// <summary>是否禁用</summary>
        [DisplayName("是否禁用")]
        [Description("是否禁用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisable", "是否禁用", "int")]
        public Int32 IsDisable { get { return _IsDisable; } set { if (OnPropertyChanging(__.IsDisable, value)) { _IsDisable = value; OnPropertyChanged(__.IsDisable); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "int")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }
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
                    case __.EventKey : return _EventKey;
                    case __.EventName : return _EventName;
                    case __.IsDisable : return _IsDisable;
                    case __.Rank : return _Rank;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.EventKey : _EventKey = Convert.ToString(value); break;
                    case __.EventName : _EventName = Convert.ToString(value); break;
                    case __.IsDisable : _IsDisable = Convert.ToInt32(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>事件key</summary>
            public static readonly Field EventKey = FindByName(__.EventKey);

            /// <summary>事件名称</summary>
            public static readonly Field EventName = FindByName(__.EventName);

            /// <summary>是否禁用</summary>
            public static readonly Field IsDisable = FindByName(__.IsDisable);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>目标事件接口</summary>
    public partial interface ITargetEvent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>事件key</summary>
        String EventKey { get; set; }

        /// <summary>事件名称</summary>
        String EventName { get; set; }

        /// <summary>是否禁用</summary>
        Int32 IsDisable { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}