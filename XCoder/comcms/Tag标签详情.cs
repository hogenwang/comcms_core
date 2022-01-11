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
    /// <summary>Tag标签详情</summary>
    [Serializable]
    [DataObject]
    [Description("Tag标签详情")]
    [BindIndex("IX_TagsDetail_TypeId", false, "TypeId")]
    [BindTable("TagsDetail", Description = "Tag标签详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class TagsDetail
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _TagsId;
        /// <summary>所属标签ID</summary>
        [DisplayName("所属标签ID")]
        [Description("所属标签ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TagsId", "所属标签ID", "")]
        public Int32 TagsId { get => _TagsId; set { if (OnPropertyChanging("TagsId", value)) { _TagsId = value; OnPropertyChanged("TagsId"); } } }

        private String _TagName;
        /// <summary>Tag标签名称</summary>
        [DisplayName("Tag标签名称")]
        [Description("Tag标签名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("TagName", "Tag标签名称", "")]
        public String TagName { get => _TagName; set { if (OnPropertyChanging("TagName", value)) { _TagName = value; OnPropertyChanged("TagName"); } } }

        private Int32 _TypeId;
        /// <summary>类型</summary>
        [DisplayName("类型")]
        [Description("类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "类型", "")]
        public Int32 TypeId { get => _TypeId; set { if (OnPropertyChanging("TypeId", value)) { _TypeId = value; OnPropertyChanged("TypeId"); } } }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("Title", "标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private Int32 _Hits;
        /// <summary>点击量</summary>
        [DisplayName("点击量")]
        [Description("点击量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging("Hits", value)) { _Hits = value; OnPropertyChanged("Hits"); } } }

        private Int32 _RId;
        /// <summary>目标Id</summary>
        [DisplayName("目标Id")]
        [Description("目标Id")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RId", "目标Id", "")]
        public Int32 RId { get => _RId; set { if (OnPropertyChanging("RId", value)) { _RId = value; OnPropertyChanged("RId"); } } }

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
                    case "TagsId": return _TagsId;
                    case "TagName": return _TagName;
                    case "TypeId": return _TypeId;
                    case "Title": return _Title;
                    case "Hits": return _Hits;
                    case "RId": return _RId;
                    case "AddTime": return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "TagsId": _TagsId = value.ToInt(); break;
                    case "TagName": _TagName = Convert.ToString(value); break;
                    case "TypeId": _TypeId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Hits": _Hits = value.ToInt(); break;
                    case "RId": _RId = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Tag标签详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>所属标签ID</summary>
            public static readonly Field TagsId = FindByName("TagsId");

            /// <summary>Tag标签名称</summary>
            public static readonly Field TagName = FindByName("TagName");

            /// <summary>类型</summary>
            public static readonly Field TypeId = FindByName("TypeId");

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>点击量</summary>
            public static readonly Field Hits = FindByName("Hits");

            /// <summary>目标Id</summary>
            public static readonly Field RId = FindByName("RId");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得Tag标签详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>所属标签ID</summary>
            public const String TagsId = "TagsId";

            /// <summary>Tag标签名称</summary>
            public const String TagName = "TagName";

            /// <summary>类型</summary>
            public const String TypeId = "TypeId";

            /// <summary>标题</summary>
            public const String Title = "Title";

            /// <summary>点击量</summary>
            public const String Hits = "Hits";

            /// <summary>目标Id</summary>
            public const String RId = "RId";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }
}