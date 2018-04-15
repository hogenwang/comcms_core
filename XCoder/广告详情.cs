using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Ads : IAds
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _Title;
        /// <summary>广告标题</summary>
        [DisplayName("广告标题")]
        [Description("广告标题")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Title", "广告标题", "nvarchar(100)", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Content;
        /// <summary>广告详情JSON</summary>
        [DisplayName("广告详情JSON")]
        [Description("广告详情JSON")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "广告详情JSON", "ntext")]
        public String Content { get { return _Content; } set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private Int32 _KId;
        /// <summary>分类ID</summary>
        [DisplayName("分类ID")]
        [Description("分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "分类ID", "int")]
        public Int32 KId { get { return _KId; } set { if (OnPropertyChanging(__.KId, value)) { _KId = value; OnPropertyChanged(__.KId); } } }

        private Int32 _TId;
        /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
        [DisplayName("广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告")]
        [Description("广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TId", "广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告", "int")]
        public Int32 TId { get { return _TId; } set { if (OnPropertyChanging(__.TId, value)) { _TId = value; OnPropertyChanged(__.TId); } } }

        private DateTime _StartTime;
        /// <summary>起始时间</summary>
        [DisplayName("起始时间")]
        [Description("起始时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StartTime", "起始时间", "datetime")]
        public DateTime StartTime { get { return _StartTime; } set { if (OnPropertyChanging(__.StartTime, value)) { _StartTime = value; OnPropertyChanged(__.StartTime); } } }

        private DateTime _EndTime;
        /// <summary>结束时间</summary>
        [DisplayName("结束时间")]
        [Description("结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "结束时间", "datetime")]
        public DateTime EndTime { get { return _EndTime; } set { if (OnPropertyChanging(__.EndTime, value)) { _EndTime = value; OnPropertyChanged(__.EndTime); } } }

        private Boolean _IsDisabled;
        /// <summary>是否禁用广告</summary>
        [DisplayName("是否禁用广告")]
        [Description("是否禁用广告")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisabled", "是否禁用广告", "bit")]
        public Boolean IsDisabled { get { return _IsDisabled; } set { if (OnPropertyChanging(__.IsDisabled, value)) { _IsDisabled = value; OnPropertyChanged(__.IsDisabled); } } }

        private Int32 _Sequence;
        /// <summary>排序，默认999</summary>
        [DisplayName("排序")]
        [Description("排序，默认999")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序，默认999", "int")]
        public Int32 Sequence { get { return _Sequence; } set { if (OnPropertyChanging(__.Sequence, value)) { _Sequence = value; OnPropertyChanged(__.Sequence); } } }
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
                    case __.Title : return _Title;
                    case __.Content : return _Content;
                    case __.KId : return _KId;
                    case __.TId : return _TId;
                    case __.StartTime : return _StartTime;
                    case __.EndTime : return _EndTime;
                    case __.IsDisabled : return _IsDisabled;
                    case __.Sequence : return _Sequence;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.KId : _KId = Convert.ToInt32(value); break;
                    case __.TId : _TId = Convert.ToInt32(value); break;
                    case __.StartTime : _StartTime = Convert.ToDateTime(value); break;
                    case __.EndTime : _EndTime = Convert.ToDateTime(value); break;
                    case __.IsDisabled : _IsDisabled = Convert.ToBoolean(value); break;
                    case __.Sequence : _Sequence = Convert.ToInt32(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>广告标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>广告详情JSON</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
            public static readonly Field TId = FindByName(__.TId);

            /// <summary>起始时间</summary>
            public static readonly Field StartTime = FindByName(__.StartTime);

            /// <summary>结束时间</summary>
            public static readonly Field EndTime = FindByName(__.EndTime);

            /// <summary>是否禁用广告</summary>
            public static readonly Field IsDisabled = FindByName(__.IsDisabled);

            /// <summary>排序，默认999</summary>
            public static readonly Field Sequence = FindByName(__.Sequence);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>广告详情接口</summary>
    public partial interface IAds
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>广告标题</summary>
        String Title { get; set; }

        /// <summary>广告详情JSON</summary>
        String Content { get; set; }

        /// <summary>分类ID</summary>
        Int32 KId { get; set; }

        /// <summary>广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告</summary>
        Int32 TId { get; set; }

        /// <summary>起始时间</summary>
        DateTime StartTime { get; set; }

        /// <summary>结束时间</summary>
        DateTime EndTime { get; set; }

        /// <summary>是否禁用广告</summary>
        Boolean IsDisabled { get; set; }

        /// <summary>排序，默认999</summary>
        Int32 Sequence { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}