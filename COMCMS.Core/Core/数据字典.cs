using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;
using NewLife;

namespace COMCMS.Core
{
    /// <summary>数据字典</summary>
    [Serializable]
    [DataObject]
    [Description("数据字典")]
    [BindTable("DataDictionary", Description = "数据字典", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class DataDictionary : IDataDictionary
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _Key;
        /// <summary>字典key</summary>
        [DisplayName("字典key")]
        [Description("字典key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Key", "字典key", "")]
        public String Key { get => _Key; set { if (OnPropertyChanging(__.Key, value)) { _Key = value; OnPropertyChanged(__.Key); } } }

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
                    case __.Key: return _Key;
                    case __.Description: return _Description;
                    case __.Rank: return _Rank;
                    case __.AddTime: return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id: _Id = value.ToInt(); break;
                    case __.Key: _Key = Convert.ToString(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.Rank: _Rank = value.ToInt(); break;
                    case __.AddTime: _AddTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得数据字典字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>字典key</summary>
            public static readonly Field Key = FindByName(__.Key);

            /// <summary>字典介绍</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得数据字典字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>字典key</summary>
            public const String Key = "Key";

            /// <summary>字典介绍</summary>
            public const String Description = "Description";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }

    /// <summary>数据字典接口</summary>
    public partial interface IDataDictionary
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>字典key</summary>
        String Key { get; set; }

        /// <summary>字典介绍</summary>
        String Description { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

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