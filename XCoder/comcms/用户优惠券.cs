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
    /// <summary>用户优惠券</summary>
    [Serializable]
    [DataObject]
    [Description("用户优惠券")]
    [BindTable("MemberCoupon", Description = "用户优惠券", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class MemberCoupon
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "", Master = true)]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _ItemNO;
        /// <summary>券号</summary>
        [DisplayName("券号")]
        [Description("券号")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ItemNO", "券号", "")]
        public String ItemNO { get => _ItemNO; set { if (OnPropertyChanging("ItemNO", value)) { _ItemNO = value; OnPropertyChanged("ItemNO"); } } }

        private Int32 _CouponId;
        /// <summary>优惠券ID</summary>
        [DisplayName("优惠券ID")]
        [Description("优惠券ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponId", "优惠券ID", "")]
        public Int32 CouponId { get => _CouponId; set { if (OnPropertyChanging("CouponId", value)) { _CouponId = value; OnPropertyChanged("CouponId"); } } }

        private Int32 _CouponType;
        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        [DisplayName("优惠券类型")]
        [Description("优惠券类型，0 现金用券，1打折券")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponType", "优惠券类型，0 现金用券，1打折券", "")]
        public Int32 CouponType { get => _CouponType; set { if (OnPropertyChanging("CouponType", value)) { _CouponType = value; OnPropertyChanged("CouponType"); } } }

        private Decimal _DiscuountRates;
        /// <summary>打折率，只有是打折券才有用</summary>
        [DisplayName("打折率")]
        [Description("打折率，只有是打折券才有用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DiscuountRates", "打折率，只有是打折券才有用", "")]
        public Decimal DiscuountRates { get => _DiscuountRates; set { if (OnPropertyChanging("DiscuountRates", value)) { _DiscuountRates = value; OnPropertyChanged("DiscuountRates"); } } }

        private Int32 _IsLimit;
        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        [DisplayName("是否有类别限制")]
        [Description("是否有类别限制，0 无限制；1 是类别限制，2是商品限制")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLimit", "是否有类别限制，0 无限制；1 是类别限制，2是商品限制", "")]
        public Int32 IsLimit { get => _IsLimit; set { if (OnPropertyChanging("IsLimit", value)) { _IsLimit = value; OnPropertyChanged("IsLimit"); } } }

        private Decimal _Price;
        /// <summary>面额</summary>
        [DisplayName("面额")]
        [Description("面额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "面额", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging("Price", value)) { _Price = value; OnPropertyChanged("Price"); } } }

        private Decimal _NeedPrice;
        /// <summary>需要消费面额</summary>
        [DisplayName("需要消费面额")]
        [Description("需要消费面额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NeedPrice", "需要消费面额", "")]
        public Decimal NeedPrice { get => _NeedPrice; set { if (OnPropertyChanging("NeedPrice", value)) { _NeedPrice = value; OnPropertyChanged("NeedPrice"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _StartTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StartTime", "添加时间", "")]
        public DateTime StartTime { get => _StartTime; set { if (OnPropertyChanging("StartTime", value)) { _StartTime = value; OnPropertyChanged("StartTime"); } } }

        private DateTime _EndTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "添加时间", "")]
        public DateTime EndTime { get => _EndTime; set { if (OnPropertyChanging("EndTime", value)) { _EndTime = value; OnPropertyChanged("EndTime"); } } }

        private Int32 _IsUse;
        /// <summary>是否使用</summary>
        [DisplayName("是否使用")]
        [Description("是否使用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsUse", "是否使用", "")]
        public Int32 IsUse { get => _IsUse; set { if (OnPropertyChanging("IsUse", value)) { _IsUse = value; OnPropertyChanged("IsUse"); } } }

        private DateTime _UseTime;
        /// <summary>使用时间</summary>
        [DisplayName("使用时间")]
        [Description("使用时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UseTime", "使用时间", "")]
        public DateTime UseTime { get => _UseTime; set { if (OnPropertyChanging("UseTime", value)) { _UseTime = value; OnPropertyChanged("UseTime"); } } }

        private String _OrderNum;
        /// <summary>订单号（使用后）</summary>
        [DisplayName("订单号")]
        [Description("订单号（使用后）")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号（使用后）", "")]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

        private Int32 _OrderId;
        /// <summary>订单编号（使用后）</summary>
        [DisplayName("订单编号")]
        [Description("订单编号（使用后）")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单编号（使用后）", "")]
        public Int32 OrderId { get => _OrderId; set { if (OnPropertyChanging("OrderId", value)) { _OrderId = value; OnPropertyChanged("OrderId"); } } }
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
                    case "UId": return _UId;
                    case "ItemNO": return _ItemNO;
                    case "CouponId": return _CouponId;
                    case "CouponType": return _CouponType;
                    case "DiscuountRates": return _DiscuountRates;
                    case "IsLimit": return _IsLimit;
                    case "Price": return _Price;
                    case "NeedPrice": return _NeedPrice;
                    case "AddTime": return _AddTime;
                    case "StartTime": return _StartTime;
                    case "EndTime": return _EndTime;
                    case "IsUse": return _IsUse;
                    case "UseTime": return _UseTime;
                    case "OrderNum": return _OrderNum;
                    case "OrderId": return _OrderId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "ItemNO": _ItemNO = Convert.ToString(value); break;
                    case "CouponId": _CouponId = value.ToInt(); break;
                    case "CouponType": _CouponType = value.ToInt(); break;
                    case "DiscuountRates": _DiscuountRates = Convert.ToDecimal(value); break;
                    case "IsLimit": _IsLimit = value.ToInt(); break;
                    case "Price": _Price = Convert.ToDecimal(value); break;
                    case "NeedPrice": _NeedPrice = Convert.ToDecimal(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "StartTime": _StartTime = value.ToDateTime(); break;
                    case "EndTime": _EndTime = value.ToDateTime(); break;
                    case "IsUse": _IsUse = value.ToInt(); break;
                    case "UseTime": _UseTime = value.ToDateTime(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "OrderId": _OrderId = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户优惠券字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>券号</summary>
            public static readonly Field ItemNO = FindByName("ItemNO");

            /// <summary>优惠券ID</summary>
            public static readonly Field CouponId = FindByName("CouponId");

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public static readonly Field CouponType = FindByName("CouponType");

            /// <summary>打折率，只有是打折券才有用</summary>
            public static readonly Field DiscuountRates = FindByName("DiscuountRates");

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public static readonly Field IsLimit = FindByName("IsLimit");

            /// <summary>面额</summary>
            public static readonly Field Price = FindByName("Price");

            /// <summary>需要消费面额</summary>
            public static readonly Field NeedPrice = FindByName("NeedPrice");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>添加时间</summary>
            public static readonly Field StartTime = FindByName("StartTime");

            /// <summary>添加时间</summary>
            public static readonly Field EndTime = FindByName("EndTime");

            /// <summary>是否使用</summary>
            public static readonly Field IsUse = FindByName("IsUse");

            /// <summary>使用时间</summary>
            public static readonly Field UseTime = FindByName("UseTime");

            /// <summary>订单号（使用后）</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>订单编号（使用后）</summary>
            public static readonly Field OrderId = FindByName("OrderId");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户优惠券字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>券号</summary>
            public const String ItemNO = "ItemNO";

            /// <summary>优惠券ID</summary>
            public const String CouponId = "CouponId";

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

            /// <summary>是否使用</summary>
            public const String IsUse = "IsUse";

            /// <summary>使用时间</summary>
            public const String UseTime = "UseTime";

            /// <summary>订单号（使用后）</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>订单编号（使用后）</summary>
            public const String OrderId = "OrderId";
        }
        #endregion
    }
}