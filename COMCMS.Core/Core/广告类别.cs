using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>广告类别</summary>
    [Serializable]
    [DataObject]
    [Description("广告类别")]
    [BindTable("AdsKind", Description = "广告类别", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdsKind : IAdsKind
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _KindName;
        /// <summary>广告类别名称</summary>
        [DisplayName("广告类别名称")]
        [Description("广告类别名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("KindName", "广告类别名称", "nvarchar(50)")]
        public String KindName { get { return _KindName; } set { if (OnPropertyChanging(__.KindName, value)) { _KindName = value; OnPropertyChanged(__.KindName); } } }

        private String _KindInfo;
        /// <summary>简单说明</summary>
        [DisplayName("简单说明")]
        [Description("简单说明")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindInfo", "简单说明", "nvarchar(250)")]
        public String KindInfo { get { return _KindInfo; } set { if (OnPropertyChanging(__.KindInfo, value)) { _KindInfo = value; OnPropertyChanged(__.KindInfo); } } }

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
                    case __.KindName : return _KindName;
                    case __.KindInfo : return _KindInfo;
                    case __.Rank : return _Rank;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.KindName : _KindName = Convert.ToString(value); break;
                    case __.KindInfo : _KindInfo = Convert.ToString(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得广告类别字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>广告类别名称</summary>
            public static readonly Field KindName = FindByName(__.KindName);

            /// <summary>简单说明</summary>
            public static readonly Field KindInfo = FindByName(__.KindInfo);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得广告类别字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>广告类别名称</summary>
            public const String KindName = "KindName";

            /// <summary>简单说明</summary>
            public const String KindInfo = "KindInfo";

            /// <summary>排序</summary>
            public const String Rank = "Rank";
        }
        #endregion
    }

    /// <summary>广告类别接口</summary>
    public partial interface IAdsKind
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>广告类别名称</summary>
        String KindName { get; set; }

        /// <summary>简单说明</summary>
        String KindInfo { get; set; }

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