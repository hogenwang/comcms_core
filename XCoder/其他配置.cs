using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>其他配置</summary>
    [Serializable]
    [DataObject]
    [Description("其他配置")]
    [BindTable("OtherConfig", Description = "其他配置", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OtherConfig : IOtherConfig
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _ConfigName;
        /// <summary>配置名称</summary>
        [DisplayName("配置名称")]
        [Description("配置名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ConfigName", "配置名称", "nvarchar(50)")]
        public String ConfigName { get { return _ConfigName; } set { if (OnPropertyChanging(__.ConfigName, value)) { _ConfigName = value; OnPropertyChanged(__.ConfigName); } } }

        private String _ConfigValue;
        /// <summary>配置值 JSON</summary>
        [DisplayName("配置值JSON")]
        [Description("配置值 JSON")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ConfigValue", "配置值 JSON", "ntext")]
        public String ConfigValue { get { return _ConfigValue; } set { if (OnPropertyChanging(__.ConfigValue, value)) { _ConfigValue = value; OnPropertyChanged(__.ConfigValue); } } }

        private DateTime _LastUpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "最后更新时间", "datetime")]
        public DateTime LastUpdateTime { get { return _LastUpdateTime; } set { if (OnPropertyChanging(__.LastUpdateTime, value)) { _LastUpdateTime = value; OnPropertyChanged(__.LastUpdateTime); } } }
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
                    case __.ConfigName : return _ConfigName;
                    case __.ConfigValue : return _ConfigValue;
                    case __.LastUpdateTime : return _LastUpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.ConfigName : _ConfigName = Convert.ToString(value); break;
                    case __.ConfigValue : _ConfigValue = Convert.ToString(value); break;
                    case __.LastUpdateTime : _LastUpdateTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得其他配置字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>配置名称</summary>
            public static readonly Field ConfigName = FindByName(__.ConfigName);

            /// <summary>配置值 JSON</summary>
            public static readonly Field ConfigValue = FindByName(__.ConfigValue);

            /// <summary>最后更新时间</summary>
            public static readonly Field LastUpdateTime = FindByName(__.LastUpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得其他配置字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>配置名称</summary>
            public const String ConfigName = "ConfigName";

            /// <summary>配置值 JSON</summary>
            public const String ConfigValue = "ConfigValue";

            /// <summary>最后更新时间</summary>
            public const String LastUpdateTime = "LastUpdateTime";
        }
        #endregion
    }

    /// <summary>其他配置接口</summary>
    public partial interface IOtherConfig
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>配置名称</summary>
        String ConfigName { get; set; }

        /// <summary>配置值 JSON</summary>
        String ConfigValue { get; set; }

        /// <summary>最后更新时间</summary>
        DateTime LastUpdateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}