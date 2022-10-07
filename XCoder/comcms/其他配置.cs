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
    /// <summary>其他配置</summary>
    [Serializable]
    [DataObject]
    [Description("其他配置")]
    [BindTable("OtherConfig", Description = "其他配置", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OtherConfig
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _ConfigName;
        /// <summary>配置名称</summary>
        [DisplayName("配置名称")]
        [Description("配置名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ConfigName", "配置名称", "")]
        public String ConfigName { get => _ConfigName; set { if (OnPropertyChanging("ConfigName", value)) { _ConfigName = value; OnPropertyChanged("ConfigName"); } } }

        private String _ConfigValue;
        /// <summary>配置值 JSON</summary>
        [DisplayName("配置值JSON")]
        [Description("配置值 JSON")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ConfigValue", "配置值 JSON", "")]
        public String ConfigValue { get => _ConfigValue; set { if (OnPropertyChanging("ConfigValue", value)) { _ConfigValue = value; OnPropertyChanged("ConfigValue"); } } }

        private DateTime _LastUpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "最后更新时间", "")]
        public DateTime LastUpdateTime { get => _LastUpdateTime; set { if (OnPropertyChanging("LastUpdateTime", value)) { _LastUpdateTime = value; OnPropertyChanged("LastUpdateTime"); } } }
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
                    case "ConfigName": return _ConfigName;
                    case "ConfigValue": return _ConfigValue;
                    case "LastUpdateTime": return _LastUpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "ConfigName": _ConfigName = Convert.ToString(value); break;
                    case "ConfigValue": _ConfigValue = Convert.ToString(value); break;
                    case "LastUpdateTime": _LastUpdateTime = value.ToDateTime(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>配置名称</summary>
            public static readonly Field ConfigName = FindByName("ConfigName");

            /// <summary>配置值 JSON</summary>
            public static readonly Field ConfigValue = FindByName("ConfigValue");

            /// <summary>最后更新时间</summary>
            public static readonly Field LastUpdateTime = FindByName("LastUpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}