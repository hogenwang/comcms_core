using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>优惠券分类</summary>
    [Serializable]
    [DataObject]
    [Description("优惠券分类")]
    [BindTable("CouponKind", Description = "优惠券分类", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class CouponKind : ICouponKind
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _IsLimit;
        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        [DisplayName("是否有类别限制")]
        [Description("是否有类别限制，0 无限制；1 是类别限制，2是商品限制")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLimit", "是否有类别限制，0 无限制；1 是类别限制，2是商品限制", "int")]
        public Int32 IsLimit { get { return _IsLimit; } set { if (OnPropertyChanging(__.IsLimit, value)) { _IsLimit = value; OnPropertyChanged(__.IsLimit); } } }

        private String _KindName;
        /// <summary>类别名称</summary>
        [DisplayName("类别名称")]
        [Description("类别名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("KindName", "类别名称", "nvarchar(200)")]
        public String KindName { get { return _KindName; } set { if (OnPropertyChanging(__.KindName, value)) { _KindName = value; OnPropertyChanged(__.KindName); } } }

        private Int32 _CouponType;
        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        [DisplayName("优惠券类型")]
        [Description("优惠券类型，0 现金用券，1打折券")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponType", "优惠券类型，0 现金用券，1打折券", "int")]
        public Int32 CouponType { get { return _CouponType; } set { if (OnPropertyChanging(__.CouponType, value)) { _CouponType = value; OnPropertyChanged(__.CouponType); } } }

        private String _KIds;
        /// <summary>类别按逗号分开</summary>
        [DisplayName("类别按逗号分开")]
        [Description("类别按逗号分开")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("KIds", "类别按逗号分开", "ntext")]
        public String KIds { get { return _KIds; } set { if (OnPropertyChanging(__.KIds, value)) { _KIds = value; OnPropertyChanged(__.KIds); } } }

        private String _PIds;
        /// <summary>商品ID，按逗号分开</summary>
        [DisplayName("商品ID")]
        [Description("商品ID，按逗号分开")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("PIds", "商品ID，按逗号分开", "ntext")]
        public String PIds { get { return _PIds; } set { if (OnPropertyChanging(__.PIds, value)) { _PIds = value; OnPropertyChanged(__.PIds); } } }

        private String _KindNote;
        /// <summary>类别说明</summary>
        [DisplayName("类别说明")]
        [Description("类别说明")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindNote", "类别说明", "nvarchar(250)")]
        public String KindNote { get { return _KindNote; } set { if (OnPropertyChanging(__.KindNote, value)) { _KindNote = value; OnPropertyChanged(__.KindNote); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private Int32 _MyType;
        /// <summary>可使用类型</summary>
        [DisplayName("可使用类型")]
        [Description("可使用类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "可使用类型", "int")]
        public Int32 MyType { get { return _MyType; } set { if (OnPropertyChanging(__.MyType, value)) { _MyType = value; OnPropertyChanged(__.MyType); } } }
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
                    case __.IsLimit : return _IsLimit;
                    case __.KindName : return _KindName;
                    case __.CouponType : return _CouponType;
                    case __.KIds : return _KIds;
                    case __.PIds : return _PIds;
                    case __.KindNote : return _KindNote;
                    case __.AddTime : return _AddTime;
                    case __.MyType : return _MyType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.IsLimit : _IsLimit = Convert.ToInt32(value); break;
                    case __.KindName : _KindName = Convert.ToString(value); break;
                    case __.CouponType : _CouponType = Convert.ToInt32(value); break;
                    case __.KIds : _KIds = Convert.ToString(value); break;
                    case __.PIds : _PIds = Convert.ToString(value); break;
                    case __.KindNote : _KindNote = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.MyType : _MyType = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得优惠券分类字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public static readonly Field IsLimit = FindByName(__.IsLimit);

            /// <summary>类别名称</summary>
            public static readonly Field KindName = FindByName(__.KindName);

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public static readonly Field CouponType = FindByName(__.CouponType);

            /// <summary>类别按逗号分开</summary>
            public static readonly Field KIds = FindByName(__.KIds);

            /// <summary>商品ID，按逗号分开</summary>
            public static readonly Field PIds = FindByName(__.PIds);

            /// <summary>类别说明</summary>
            public static readonly Field KindNote = FindByName(__.KindNote);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>可使用类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得优惠券分类字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public const String IsLimit = "IsLimit";

            /// <summary>类别名称</summary>
            public const String KindName = "KindName";

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public const String CouponType = "CouponType";

            /// <summary>类别按逗号分开</summary>
            public const String KIds = "KIds";

            /// <summary>商品ID，按逗号分开</summary>
            public const String PIds = "PIds";

            /// <summary>类别说明</summary>
            public const String KindNote = "KindNote";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>可使用类型</summary>
            public const String MyType = "MyType";
        }
        #endregion
    }

    /// <summary>优惠券分类接口</summary>
    public partial interface ICouponKind
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        Int32 IsLimit { get; set; }

        /// <summary>类别名称</summary>
        String KindName { get; set; }

        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        Int32 CouponType { get; set; }

        /// <summary>类别按逗号分开</summary>
        String KIds { get; set; }

        /// <summary>商品ID，按逗号分开</summary>
        String PIds { get; set; }

        /// <summary>类别说明</summary>
        String KindNote { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>可使用类型</summary>
        Int32 MyType { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}