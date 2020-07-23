using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>数据字典详情</summary>
    [Serializable]
    [DataObject]
    [Description("数据字典详情")]
    [BindTable("DataDictionaryDetail", Description = "数据字典详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class DataDictionaryDetail : IDataDictionaryDetail
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _DataDictionaryId;
        /// <summary>字典ID</summary>
        [DisplayName("字典ID")]
        [Description("字典ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DataDictionaryId", "字典ID", "")]
        public Int32 DataDictionaryId { get => _DataDictionaryId; set { if (OnPropertyChanging(__.DataDictionaryId, value)) { _DataDictionaryId = value; OnPropertyChanged(__.DataDictionaryId); } } }

        private String _Val;
        /// <summary>字典值</summary>
        [DisplayName("字典值")]
        [Description("字典值")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Val", "字典值", "")]
        public String Val { get => _Val; set { if (OnPropertyChanging(__.Val, value)) { _Val = value; OnPropertyChanged(__.Val); } } }

        private String _Title;
        /// <summary>字典标签</summary>
        [DisplayName("字典标签")]
        [Description("字典标签")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Title", "字典标签", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Description;
        /// <summary>字典介绍</summary>
        [DisplayName("字典介绍")]
        [Description("字典介绍")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Description", "字典介绍", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private Int32 _IsDefault;
        /// <summary>是否默认值</summary>
        [DisplayName("是否默认值")]
        [Description("是否默认值")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否默认值", "")]
        public Int32 IsDefault { get => _IsDefault; set { if (OnPropertyChanging(__.IsDefault, value)) { _IsDefault = value; OnPropertyChanged(__.IsDefault); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }
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
                    case __.Id: return _Id;
                    case __.DataDictionaryId: return _DataDictionaryId;
                    case __.Val: return _Val;
                    case __.Title: return _Title;
                    case __.Description: return _Description;
                    case __.Rank: return _Rank;
                    case __.IsDefault: return _IsDefault;
                    case __.AddTime: return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id: _Id = value.ToInt(); break;
                    case __.DataDictionaryId: _DataDictionaryId = value.ToInt(); break;
                    case __.Val: _Val = Convert.ToString(value); break;
                    case __.Title: _Title = Convert.ToString(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.Rank: _Rank = value.ToInt(); break;
                    case __.IsDefault: _IsDefault = value.ToInt(); break;
                    case __.AddTime: _AddTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得数据字典详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>字典ID</summary>
            public static readonly Field DataDictionaryId = FindByName(__.DataDictionaryId);

            /// <summary>字典值</summary>
            public static readonly Field Val = FindByName(__.Val);

            /// <summary>字典标签</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>字典介绍</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>是否默认值</summary>
            public static readonly Field IsDefault = FindByName(__.IsDefault);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得数据字典详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>字典ID</summary>
            public const String DataDictionaryId = "DataDictionaryId";

            /// <summary>字典值</summary>
            public const String Val = "Val";

            /// <summary>字典标签</summary>
            public const String Title = "Title";

            /// <summary>字典介绍</summary>
            public const String Description = "Description";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>是否默认值</summary>
            public const String IsDefault = "IsDefault";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }

    /// <summary>数据字典详情接口</summary>
    public partial interface IDataDictionaryDetail
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>字典ID</summary>
        Int32 DataDictionaryId { get; set; }

        /// <summary>字典值</summary>
        String Val { get; set; }

        /// <summary>字典标签</summary>
        String Title { get; set; }

        /// <summary>字典介绍</summary>
        String Description { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>是否默认值</summary>
        Int32 IsDefault { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}