using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>优惠券</summary>
    [Serializable]
    [DataObject]
    [Description("优惠券")]
    [BindTable("Coupon", Description = "优惠券", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Coupon : ICoupon
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _ItemNO;
        /// <summary>券号</summary>
        [DisplayName("券号")]
        [Description("券号")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ItemNO", "券号", "nvarchar(20)")]
        public String ItemNO { get { return _ItemNO; } set { if (OnPropertyChanging(__.ItemNO, value)) { _ItemNO = value; OnPropertyChanged(__.ItemNO); } } }

        private Int32 _KId;
        /// <summary>类别，0默认没限制</summary>
        [DisplayName("类别")]
        [Description("类别，0默认没限制")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "类别，0默认没限制", "int")]
        public Int32 KId { get { return _KId; } set { if (OnPropertyChanging(__.KId, value)) { _KId = value; OnPropertyChanged(__.KId); } } }

        private Int32 _CouponType;
        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        [DisplayName("优惠券类型")]
        [Description("优惠券类型，0 现金用券，1打折券")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponType", "优惠券类型，0 现金用券，1打折券", "int")]
        public Int32 CouponType { get { return _CouponType; } set { if (OnPropertyChanging(__.CouponType, value)) { _CouponType = value; OnPropertyChanged(__.CouponType); } } }

        private Decimal _DiscuountRates;
        /// <summary>打折率，只有是打折券才有用</summary>
        [DisplayName("打折率")]
        [Description("打折率，只有是打折券才有用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DiscuountRates", "打折率，只有是打折券才有用", "money")]
        public Decimal DiscuountRates { get { return _DiscuountRates; } set { if (OnPropertyChanging(__.DiscuountRates, value)) { _DiscuountRates = value; OnPropertyChanged(__.DiscuountRates); } } }

        private Int32 _IsLimit;
        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        [DisplayName("是否有类别限制")]
        [Description("是否有类别限制，0 无限制；1 是类别限制，2是商品限制")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLimit", "是否有类别限制，0 无限制；1 是类别限制，2是商品限制", "int")]
        public Int32 IsLimit { get { return _IsLimit; } set { if (OnPropertyChanging(__.IsLimit, value)) { _IsLimit = value; OnPropertyChanged(__.IsLimit); } } }

        private Decimal _Price;
        /// <summary>面额</summary>
        [DisplayName("面额")]
        [Description("面额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "面额", "money")]
        public Decimal Price { get { return _Price; } set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } } }

        private Decimal _NeedPrice;
        /// <summary>需要消费面额</summary>
        [DisplayName("需要消费面额")]
        [Description("需要消费面额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NeedPrice", "需要消费面额", "money")]
        public Decimal NeedPrice { get { return _NeedPrice; } set { if (OnPropertyChanging(__.NeedPrice, value)) { _NeedPrice = value; OnPropertyChanged(__.NeedPrice); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _StartTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StartTime", "添加时间", "datetime")]
        public DateTime StartTime { get { return _StartTime; } set { if (OnPropertyChanging(__.StartTime, value)) { _StartTime = value; OnPropertyChanged(__.StartTime); } } }

        private DateTime _EndTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "添加时间", "datetime")]
        public DateTime EndTime { get { return _EndTime; } set { if (OnPropertyChanging(__.EndTime, value)) { _EndTime = value; OnPropertyChanged(__.EndTime); } } }

        private Int32 _TotalCount;
        /// <summary>最大领取数量</summary>
        [DisplayName("最大领取数量")]
        [Description("最大领取数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalCount", "最大领取数量", "int")]
        public Int32 TotalCount { get { return _TotalCount; } set { if (OnPropertyChanging(__.TotalCount, value)) { _TotalCount = value; OnPropertyChanged(__.TotalCount); } } }

        private Int32 _TotalUseCount;
        /// <summary>已使用次数</summary>
        [DisplayName("已使用次数")]
        [Description("已使用次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalUseCount", "已使用次数", "int")]
        public Int32 TotalUseCount { get { return _TotalUseCount; } set { if (OnPropertyChanging(__.TotalUseCount, value)) { _TotalUseCount = value; OnPropertyChanged(__.TotalUseCount); } } }

        private Int32 _SpreadUId;
        /// <summary>推广员ID，可选</summary>
        [DisplayName("推广员ID")]
        [Description("推广员ID，可选")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpreadUId", "推广员ID，可选", "int")]
        public Int32 SpreadUId { get { return _SpreadUId; } set { if (OnPropertyChanging(__.SpreadUId, value)) { _SpreadUId = value; OnPropertyChanged(__.SpreadUId); } } }

        private Int32 _UId;
        /// <summary>用户Id</summary>
        [DisplayName("用户Id")]
        [Description("用户Id")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户Id", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

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
                    case __.ItemNO : return _ItemNO;
                    case __.KId : return _KId;
                    case __.CouponType : return _CouponType;
                    case __.DiscuountRates : return _DiscuountRates;
                    case __.IsLimit : return _IsLimit;
                    case __.Price : return _Price;
                    case __.NeedPrice : return _NeedPrice;
                    case __.AddTime : return _AddTime;
                    case __.StartTime : return _StartTime;
                    case __.EndTime : return _EndTime;
                    case __.TotalCount : return _TotalCount;
                    case __.TotalUseCount : return _TotalUseCount;
                    case __.SpreadUId : return _SpreadUId;
                    case __.UId : return _UId;
                    case __.MyType : return _MyType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.ItemNO : _ItemNO = Convert.ToString(value); break;
                    case __.KId : _KId = Convert.ToInt32(value); break;
                    case __.CouponType : _CouponType = Convert.ToInt32(value); break;
                    case __.DiscuountRates : _DiscuountRates = Convert.ToDecimal(value); break;
                    case __.IsLimit : _IsLimit = Convert.ToInt32(value); break;
                    case __.Price : _Price = Convert.ToDecimal(value); break;
                    case __.NeedPrice : _NeedPrice = Convert.ToDecimal(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.StartTime : _StartTime = Convert.ToDateTime(value); break;
                    case __.EndTime : _EndTime = Convert.ToDateTime(value); break;
                    case __.TotalCount : _TotalCount = Convert.ToInt32(value); break;
                    case __.TotalUseCount : _TotalUseCount = Convert.ToInt32(value); break;
                    case __.SpreadUId : _SpreadUId = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.MyType : _MyType = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得优惠券字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>券号</summary>
            public static readonly Field ItemNO = FindByName(__.ItemNO);

            /// <summary>类别，0默认没限制</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public static readonly Field CouponType = FindByName(__.CouponType);

            /// <summary>打折率，只有是打折券才有用</summary>
            public static readonly Field DiscuountRates = FindByName(__.DiscuountRates);

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public static readonly Field IsLimit = FindByName(__.IsLimit);

            /// <summary>面额</summary>
            public static readonly Field Price = FindByName(__.Price);

            /// <summary>需要消费面额</summary>
            public static readonly Field NeedPrice = FindByName(__.NeedPrice);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>添加时间</summary>
            public static readonly Field StartTime = FindByName(__.StartTime);

            /// <summary>添加时间</summary>
            public static readonly Field EndTime = FindByName(__.EndTime);

            /// <summary>最大领取数量</summary>
            public static readonly Field TotalCount = FindByName(__.TotalCount);

            /// <summary>已使用次数</summary>
            public static readonly Field TotalUseCount = FindByName(__.TotalUseCount);

            /// <summary>推广员ID，可选</summary>
            public static readonly Field SpreadUId = FindByName(__.SpreadUId);

            /// <summary>用户Id</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>可使用类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得优惠券字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>券号</summary>
            public const String ItemNO = "ItemNO";

            /// <summary>类别，0默认没限制</summary>
            public const String KId = "KId";

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public const String CouponType = "CouponType";

            /// <summary>打折率，只有是打折券才有用</summary>
            public const String DiscuountRates = "DiscuountRates";

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public const String IsLimit = "IsLimit";

            /// <summary>面额</summary>
            public const String Price = "Price";

            /// <summary>需要消费面额</summary>
            public const String NeedPrice = "NeedPrice";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>添加时间</summary>
            public const String StartTime = "StartTime";

            /// <summary>添加时间</summary>
            public const String EndTime = "EndTime";

            /// <summary>最大领取数量</summary>
            public const String TotalCount = "TotalCount";

            /// <summary>已使用次数</summary>
            public const String TotalUseCount = "TotalUseCount";

            /// <summary>推广员ID，可选</summary>
            public const String SpreadUId = "SpreadUId";

            /// <summary>用户Id</summary>
            public const String UId = "UId";

            /// <summary>可使用类型</summary>
            public const String MyType = "MyType";
        }
        #endregion
    }

    /// <summary>优惠券接口</summary>
    public partial interface ICoupon
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>券号</summary>
        String ItemNO { get; set; }

        /// <summary>类别，0默认没限制</summary>
        Int32 KId { get; set; }

        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        Int32 CouponType { get; set; }

        /// <summary>打折率，只有是打折券才有用</summary>
        Decimal DiscuountRates { get; set; }

        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        Int32 IsLimit { get; set; }

        /// <summary>面额</summary>
        Decimal Price { get; set; }

        /// <summary>需要消费面额</summary>
        Decimal NeedPrice { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>添加时间</summary>
        DateTime StartTime { get; set; }

        /// <summary>添加时间</summary>
        DateTime EndTime { get; set; }

        /// <summary>最大领取数量</summary>
        Int32 TotalCount { get; set; }

        /// <summary>已使用次数</summary>
        Int32 TotalUseCount { get; set; }

        /// <summary>推广员ID，可选</summary>
        Int32 SpreadUId { get; set; }

        /// <summary>用户Id</summary>
        Int32 UId { get; set; }

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