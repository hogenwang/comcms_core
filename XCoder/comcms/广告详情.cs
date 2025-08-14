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
    /// <summary>广告详情</summary>
    [Serializable]
    [DataObject]
    [Description("广告详情")]
    [BindTable("Ads", Description = "广告详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Ads
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _Title;
        /// <summary>广告标题</summary>
        [DisplayName("广告标题")]
        [Description("广告标题")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Title", "广告标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _Content;
        /// <summary>广告详情JSON</summary>
        [DisplayName("广告详情JSON")]
        [Description("广告详情JSON")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "广告详情JSON", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

        private Int32 _KId;
        /// <summary>分类ID</summary>
        [DisplayName("分类ID")]
        [Description("分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "分类ID", "")]
        public Int32 KId { get => _KId; set { if (OnPropertyChanging("KId", value)) { _KId = value; OnPropertyChanged("KId"); } } }

        private Int32 _TId;
        /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
        [DisplayName("广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告")]
        [Description("广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TId", "广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告", "")]
        public Int32 TId { get => _TId; set { if (OnPropertyChanging("TId", value)) { _TId = value; OnPropertyChanged("TId"); } } }

        private DateTime _StartTime;
        /// <summary>起始时间</summary>
        [DisplayName("起始时间")]
        [Description("起始时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StartTime", "起始时间", "")]
        public DateTime StartTime { get => _StartTime; set { if (OnPropertyChanging("StartTime", value)) { _StartTime = value; OnPropertyChanged("StartTime"); } } }

        private DateTime _EndTime;
        /// <summary>结束时间</summary>
        [DisplayName("结束时间")]
        [Description("结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "结束时间", "")]
        public DateTime EndTime { get => _EndTime; set { if (OnPropertyChanging("EndTime", value)) { _EndTime = value; OnPropertyChanged("EndTime"); } } }

        private Boolean _IsDisabled;
        /// <summary>是否禁用广告</summary>
        [DisplayName("是否禁用广告")]
        [Description("是否禁用广告")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisabled", "是否禁用广告", "")]
        public Boolean IsDisabled { get => _IsDisabled; set { if (OnPropertyChanging("IsDisabled", value)) { _IsDisabled = value; OnPropertyChanged("IsDisabled"); } } }

        private Int32 _Sequence;
        /// <summary>排序，默认999</summary>
        [DisplayName("排序")]
        [Description("排序，默认999")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序，默认999", "")]
        public Int32 Sequence { get => _Sequence; set { if (OnPropertyChanging("Sequence", value)) { _Sequence = value; OnPropertyChanged("Sequence"); } } }
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
                    case "Title": return _Title;
                    case "Content": return _Content;
                    case "KId": return _KId;
                    case "TId": return _TId;
                    case "StartTime": return _StartTime;
                    case "EndTime": return _EndTime;
                    case "IsDisabled": return _IsDisabled;
                    case "Sequence": return _Sequence;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Content": _Content = Convert.ToString(value); break;
                    case "KId": _KId = value.ToInt(); break;
                    case "TId": _TId = value.ToInt(); break;
                    case "StartTime": _StartTime = value.ToDateTime(); break;
                    case "EndTime": _EndTime = value.ToDateTime(); break;
                    case "IsDisabled": _IsDisabled = value.ToBoolean(); break;
                    case "Sequence": _Sequence = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得广告详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>广告标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>广告详情JSON</summary>
            public static readonly Field Content = FindByName("Content");

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName("KId");

            /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
            public static readonly Field TId = FindByName("TId");

            /// <summary>起始时间</summary>
            public static readonly Field StartTime = FindByName("StartTime");

            /// <summary>结束时间</summary>
            public static readonly Field EndTime = FindByName("EndTime");

            /// <summary>是否禁用广告</summary>
            public static readonly Field IsDisabled = FindByName("IsDisabled");

            /// <summary>排序，默认999</summary>
            public static readonly Field Sequence = FindByName("Sequence");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得广告详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>广告标题</summary>
            public const String Title = "Title";

            /// <summary>广告详情JSON</summary>
            public const String Content = "Content";

            /// <summary>分类ID</summary>
            public const String KId = "KId";

            /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
            public const String TId = "TId";

            /// <summary>起始时间</summary>
            public const String StartTime = "StartTime";

            /// <summary>结束时间</summary>
            public const String EndTime = "EndTime";

            /// <summary>是否禁用广告</summary>
            public const String IsDisabled = "IsDisabled";

            /// <summary>排序，默认999</summary>
            public const String Sequence = "Sequence";
        }
        #endregion
    }
}