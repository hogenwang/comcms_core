using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>购物车</summary>
    [Serializable]
    [DataObject]
    [Description("购物车")]
    [BindTable("ShoppingCart", Description = "购物车", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class ShoppingCart : IShoppingCart
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _GUID;
        /// <summary>唯一GUID，没登录用户使用</summary>
        [DisplayName("唯一GUID")]
        [Description("唯一GUID，没登录用户使用")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("GUID", "唯一GUID，没登录用户使用", "nvarchar(50)")]
        public String GUID { get { return _GUID; } set { if (OnPropertyChanging(__.GUID, value)) { _GUID = value; OnPropertyChanged(__.GUID); } } }

        private String _Details;
        /// <summary>购物车内容，JOSN</summary>
        [DisplayName("购物车内容")]
        [Description("购物车内容，JOSN")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Details", "购物车内容，JOSN", "ntext")]
        public String Details { get { return _Details; } set { if (OnPropertyChanging(__.Details, value)) { _Details = value; OnPropertyChanged(__.Details); } } }

        private DateTime _AddTime;
        /// <summary>加入购物车时间</summary>
        [DisplayName("加入购物车时间")]
        [Description("加入购物车时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "加入购物车时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _UpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "最后更新时间", "datetime")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }
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
                    case __.UId : return _UId;
                    case __.GUID : return _GUID;
                    case __.Details : return _Details;
                    case __.AddTime : return _AddTime;
                    case __.UpdateTime : return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.GUID : _GUID = Convert.ToString(value); break;
                    case __.Details : _Details = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得购物车字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>唯一GUID，没登录用户使用</summary>
            public static readonly Field GUID = FindByName(__.GUID);

            /// <summary>购物车内容，JOSN</summary>
            public static readonly Field Details = FindByName(__.Details);

            /// <summary>加入购物车时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>最后更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得购物车字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>唯一GUID，没登录用户使用</summary>
            public const String GUID = "GUID";

            /// <summary>购物车内容，JOSN</summary>
            public const String Details = "Details";

            /// <summary>加入购物车时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>最后更新时间</summary>
            public const String UpdateTime = "UpdateTime";
        }
        #endregion
    }

    /// <summary>购物车接口</summary>
    public partial interface IShoppingCart
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>唯一GUID，没登录用户使用</summary>
        String GUID { get; set; }

        /// <summary>购物车内容，JOSN</summary>
        String Details { get; set; }

        /// <summary>加入购物车时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>最后更新时间</summary>
        DateTime UpdateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}