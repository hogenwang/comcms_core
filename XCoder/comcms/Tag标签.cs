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
    /// <summary>Tag标签</summary>
    [Serializable]
    [DataObject]
    [Description("Tag标签")]
    [BindIndex("IX_Tags_TagName", false, "TagName")]
    [BindTable("Tags", Description = "Tag标签", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Tags
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _TagName;
        /// <summary>Tag标签名称</summary>
        [DisplayName("Tag标签名称")]
        [Description("Tag标签名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("TagName", "Tag标签名称", "")]
        public String TagName { get => _TagName; set { if (OnPropertyChanging("TagName", value)) { _TagName = value; OnPropertyChanged("TagName"); } } }

        private String _Keywords;
        /// <summary>标签关键词</summary>
        [DisplayName("标签关键词")]
        [Description("标签关键词")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keywords", "标签关键词", "")]
        public String Keywords { get => _Keywords; set { if (OnPropertyChanging("Keywords", value)) { _Keywords = value; OnPropertyChanged("Keywords"); } } }

        private String _Description;
        /// <summary>标签描述</summary>
        [DisplayName("标签描述")]
        [Description("标签描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "标签描述", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

        private Int32 _Counts;
        /// <summary>数量</summary>
        [DisplayName("数量")]
        [Description("数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Counts", "数量", "")]
        public Int32 Counts { get => _Counts; set { if (OnPropertyChanging("Counts", value)) { _Counts = value; OnPropertyChanged("Counts"); } } }

        private Int32 _Hits;
        /// <summary>点击量</summary>
        [DisplayName("点击量")]
        [Description("点击量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging("Hits", value)) { _Hits = value; OnPropertyChanged("Hits"); } } }

        private Int32 _IsTop;
        /// <summary>置顶否</summary>
        [DisplayName("置顶否")]
        [Description("置顶否")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsTop", "置顶否", "")]
        public Int32 IsTop { get => _IsTop; set { if (OnPropertyChanging("IsTop", value)) { _IsTop = value; OnPropertyChanged("IsTop"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }
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
                    case "TagName": return _TagName;
                    case "Keywords": return _Keywords;
                    case "Description": return _Description;
                    case "Counts": return _Counts;
                    case "Hits": return _Hits;
                    case "IsTop": return _IsTop;
                    case "AddTime": return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "TagName": _TagName = Convert.ToString(value); break;
                    case "Keywords": _Keywords = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "Counts": _Counts = value.ToInt(); break;
                    case "Hits": _Hits = value.ToInt(); break;
                    case "IsTop": _IsTop = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Tag标签字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>Tag标签名称</summary>
            public static readonly Field TagName = FindByName("TagName");

            /// <summary>标签关键词</summary>
            public static readonly Field Keywords = FindByName("Keywords");

            /// <summary>标签描述</summary>
            public static readonly Field Description = FindByName("Description");

            /// <summary>数量</summary>
            public static readonly Field Counts = FindByName("Counts");

            /// <summary>点击量</summary>
            public static readonly Field Hits = FindByName("Hits");

            /// <summary>置顶否</summary>
            public static readonly Field IsTop = FindByName("IsTop");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得Tag标签字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>Tag标签名称</summary>
            public const String TagName = "TagName";

            /// <summary>标签关键词</summary>
            public const String Keywords = "Keywords";

            /// <summary>标签描述</summary>
            public const String Description = "Description";

            /// <summary>数量</summary>
            public const String Counts = "Counts";

            /// <summary>点击量</summary>
            public const String Hits = "Hits";

            /// <summary>置顶否</summary>
            public const String IsTop = "IsTop";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }
}