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
    /// <summary>订单详情</summary>
    [Serializable]
    [DataObject]
    [Description("订单详情")]
    [BindTable("OrderDetail", Description = "订单详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OrderDetail
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _OrderId;
        /// <summary>订单ID</summary>
        [DisplayName("订单ID")]
        [Description("订单ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单ID", "")]
        public Int32 OrderId { get => _OrderId; set { if (OnPropertyChanging("OrderId", value)) { _OrderId = value; OnPropertyChanged("OrderId"); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

        private Int32 _ShopId;
        /// <summary>商户ID</summary>
        [DisplayName("商户ID")]
        [Description("商户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "商户ID", "")]
        public Int32 ShopId { get => _ShopId; set { if (OnPropertyChanging("ShopId", value)) { _ShopId = value; OnPropertyChanged("ShopId"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private Int32 _PId;
        /// <summary>商品ID</summary>
        [DisplayName("商品ID")]
        [Description("商品ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "商品ID", "")]
        public Int32 PId { get => _PId; set { if (OnPropertyChanging("PId", value)) { _PId = value; OnPropertyChanged("PId"); } } }

        private Int32 _TypeId;
        /// <summary>类型ID</summary>
        [DisplayName("类型ID")]
        [Description("类型ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "类型ID", "")]
        public Int32 TypeId { get => _TypeId; set { if (OnPropertyChanging("TypeId", value)) { _TypeId = value; OnPropertyChanged("TypeId"); } } }

        private Int32 _PriceId;
        /// <summary>价格ID</summary>
        [DisplayName("价格ID")]
        [Description("价格ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PriceId", "价格ID", "")]
        public Int32 PriceId { get => _PriceId; set { if (OnPropertyChanging("PriceId", value)) { _PriceId = value; OnPropertyChanged("PriceId"); } } }

        private String _Title;
        /// <summary>商品名称</summary>
        [DisplayName("商品名称")]
        [Description("商品名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "商品名称", "")]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _Pic;
        /// <summary>商品名称</summary>
        [DisplayName("商品名称")]
        [Description("商品名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "商品名称", "")]
        public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

        private String _Attr;
        /// <summary>商品属性</summary>
        [DisplayName("商品属性")]
        [Description("商品属性")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Attr", "商品属性", "")]
        public String Attr { get => _Attr; set { if (OnPropertyChanging("Attr", value)) { _Attr = value; OnPropertyChanged("Attr"); } } }

        private String _Color;
        /// <summary>商品颜色</summary>
        [DisplayName("商品颜色")]
        [Description("商品颜色")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Color", "商品颜色", "")]
        public String Color { get => _Color; set { if (OnPropertyChanging("Color", value)) { _Color = value; OnPropertyChanged("Color"); } } }

        private String _Spec;
        /// <summary>规格</summary>
        [DisplayName("规格")]
        [Description("规格")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Spec", "规格", "")]
        public String Spec { get => _Spec; set { if (OnPropertyChanging("Spec", value)) { _Spec = value; OnPropertyChanged("Spec"); } } }

        private String _ItemNO;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ItemNO", "编号", "")]
        public String ItemNO { get => _ItemNO; set { if (OnPropertyChanging("ItemNO", value)) { _ItemNO = value; OnPropertyChanged("ItemNO"); } } }

        private Int32 _Qty;
        /// <summary>数量</summary>
        [DisplayName("数量")]
        [Description("数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Qty", "数量", "")]
        public Int32 Qty { get => _Qty; set { if (OnPropertyChanging("Qty", value)) { _Qty = value; OnPropertyChanged("Qty"); } } }

        private Decimal _Price;
        /// <summary>商品价格</summary>
        [DisplayName("商品价格")]
        [Description("商品价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "商品价格", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging("Price", value)) { _Price = value; OnPropertyChanged("Price"); } } }

        private Decimal _MarketPrice;
        /// <summary>商品市场价格</summary>
        [DisplayName("商品市场价格")]
        [Description("商品市场价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MarketPrice", "商品市场价格", "")]
        public Decimal MarketPrice { get => _MarketPrice; set { if (OnPropertyChanging("MarketPrice", value)) { _MarketPrice = value; OnPropertyChanged("MarketPrice"); } } }

        private Decimal _SpecialPrice;
        /// <summary>商品特价</summary>
        [DisplayName("商品特价")]
        [Description("商品特价")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpecialPrice", "商品特价", "")]
        public Decimal SpecialPrice { get => _SpecialPrice; set { if (OnPropertyChanging("SpecialPrice", value)) { _SpecialPrice = value; OnPropertyChanged("SpecialPrice"); } } }

        private Decimal _Discount;
        /// <summary>商品优惠</summary>
        [DisplayName("商品优惠")]
        [Description("商品优惠")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "商品优惠", "")]
        public Decimal Discount { get => _Discount; set { if (OnPropertyChanging("Discount", value)) { _Discount = value; OnPropertyChanged("Discount"); } } }

        private Decimal _Tax;
        /// <summary>商品税</summary>
        [DisplayName("商品税")]
        [Description("商品税")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Tax", "商品税", "")]
        public Decimal Tax { get => _Tax; set { if (OnPropertyChanging("Tax", value)) { _Tax = value; OnPropertyChanged("Tax"); } } }

        private Int32 _Credit;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credit", "积分", "")]
        public Int32 Credit { get => _Credit; set { if (OnPropertyChanging("Credit", value)) { _Credit = value; OnPropertyChanged("Credit"); } } }

        private Int32 _BackCredits;
        /// <summary>返现积分</summary>
        [DisplayName("返现积分")]
        [Description("返现积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BackCredits", "返现积分", "")]
        public Int32 BackCredits { get => _BackCredits; set { if (OnPropertyChanging("BackCredits", value)) { _BackCredits = value; OnPropertyChanged("BackCredits"); } } }

        private Int32 _IsOK;
        /// <summary>是否完成</summary>
        [DisplayName("是否完成")]
        [Description("是否完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOK", "是否完成", "")]
        public Int32 IsOK { get => _IsOK; set { if (OnPropertyChanging("IsOK", value)) { _IsOK = value; OnPropertyChanged("IsOK"); } } }

        private Int32 _IsComment;
        /// <summary>是否已经评论</summary>
        [DisplayName("是否已经评论")]
        [Description("是否已经评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "是否已经评论", "")]
        public Int32 IsComment { get => _IsComment; set { if (OnPropertyChanging("IsComment", value)) { _IsComment = value; OnPropertyChanged("IsComment"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _CheckInDate;
        /// <summary>入住日期</summary>
        [DisplayName("入住日期")]
        [Description("入住日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CheckInDate", "入住日期", "")]
        public DateTime CheckInDate { get => _CheckInDate; set { if (OnPropertyChanging("CheckInDate", value)) { _CheckInDate = value; OnPropertyChanged("CheckInDate"); } } }

        private DateTime _LeaveDate;
        /// <summary>离开日期</summary>
        [DisplayName("离开日期")]
        [Description("离开日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LeaveDate", "离开日期", "")]
        public DateTime LeaveDate { get => _LeaveDate; set { if (OnPropertyChanging("LeaveDate", value)) { _LeaveDate = value; OnPropertyChanged("LeaveDate"); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }
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
                    case "OrderId": return _OrderId;
                    case "OrderNum": return _OrderNum;
                    case "ShopId": return _ShopId;
                    case "UId": return _UId;
                    case "PId": return _PId;
                    case "TypeId": return _TypeId;
                    case "PriceId": return _PriceId;
                    case "Title": return _Title;
                    case "Pic": return _Pic;
                    case "Attr": return _Attr;
                    case "Color": return _Color;
                    case "Spec": return _Spec;
                    case "ItemNO": return _ItemNO;
                    case "Qty": return _Qty;
                    case "Price": return _Price;
                    case "MarketPrice": return _MarketPrice;
                    case "SpecialPrice": return _SpecialPrice;
                    case "Discount": return _Discount;
                    case "Tax": return _Tax;
                    case "Credit": return _Credit;
                    case "BackCredits": return _BackCredits;
                    case "IsOK": return _IsOK;
                    case "IsComment": return _IsComment;
                    case "AddTime": return _AddTime;
                    case "CheckInDate": return _CheckInDate;
                    case "LeaveDate": return _LeaveDate;
                    case "MyType": return _MyType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "OrderId": _OrderId = value.ToInt(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "ShopId": _ShopId = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "PId": _PId = value.ToInt(); break;
                    case "TypeId": _TypeId = value.ToInt(); break;
                    case "PriceId": _PriceId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Pic": _Pic = Convert.ToString(value); break;
                    case "Attr": _Attr = Convert.ToString(value); break;
                    case "Color": _Color = Convert.ToString(value); break;
                    case "Spec": _Spec = Convert.ToString(value); break;
                    case "ItemNO": _ItemNO = Convert.ToString(value); break;
                    case "Qty": _Qty = value.ToInt(); break;
                    case "Price": _Price = Convert.ToDecimal(value); break;
                    case "MarketPrice": _MarketPrice = Convert.ToDecimal(value); break;
                    case "SpecialPrice": _SpecialPrice = Convert.ToDecimal(value); break;
                    case "Discount": _Discount = Convert.ToDecimal(value); break;
                    case "Tax": _Tax = Convert.ToDecimal(value); break;
                    case "Credit": _Credit = value.ToInt(); break;
                    case "BackCredits": _BackCredits = value.ToInt(); break;
                    case "IsOK": _IsOK = value.ToInt(); break;
                    case "IsComment": _IsComment = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "CheckInDate": _CheckInDate = value.ToDateTime(); break;
                    case "LeaveDate": _LeaveDate = value.ToDateTime(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得订单详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName("OrderId");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>商户ID</summary>
            public static readonly Field ShopId = FindByName("ShopId");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>商品ID</summary>
            public static readonly Field PId = FindByName("PId");

            /// <summary>类型ID</summary>
            public static readonly Field TypeId = FindByName("TypeId");

            /// <summary>价格ID</summary>
            public static readonly Field PriceId = FindByName("PriceId");

            /// <summary>商品名称</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>商品名称</summary>
            public static readonly Field Pic = FindByName("Pic");

            /// <summary>商品属性</summary>
            public static readonly Field Attr = FindByName("Attr");

            /// <summary>商品颜色</summary>
            public static readonly Field Color = FindByName("Color");

            /// <summary>规格</summary>
            public static readonly Field Spec = FindByName("Spec");

            /// <summary>编号</summary>
            public static readonly Field ItemNO = FindByName("ItemNO");

            /// <summary>数量</summary>
            public static readonly Field Qty = FindByName("Qty");

            /// <summary>商品价格</summary>
            public static readonly Field Price = FindByName("Price");

            /// <summary>商品市场价格</summary>
            public static readonly Field MarketPrice = FindByName("MarketPrice");

            /// <summary>商品特价</summary>
            public static readonly Field SpecialPrice = FindByName("SpecialPrice");

            /// <summary>商品优惠</summary>
            public static readonly Field Discount = FindByName("Discount");

            /// <summary>商品税</summary>
            public static readonly Field Tax = FindByName("Tax");

            /// <summary>积分</summary>
            public static readonly Field Credit = FindByName("Credit");

            /// <summary>返现积分</summary>
            public static readonly Field BackCredits = FindByName("BackCredits");

            /// <summary>是否完成</summary>
            public static readonly Field IsOK = FindByName("IsOK");

            /// <summary>是否已经评论</summary>
            public static readonly Field IsComment = FindByName("IsComment");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>入住日期</summary>
            public static readonly Field CheckInDate = FindByName("CheckInDate");

            /// <summary>离开日期</summary>
            public static readonly Field LeaveDate = FindByName("LeaveDate");

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName("MyType");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得订单详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>订单ID</summary>
            public const String OrderId = "OrderId";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>商户ID</summary>
            public const String ShopId = "ShopId";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>商品ID</summary>
            public const String PId = "PId";

            /// <summary>类型ID</summary>
            public const String TypeId = "TypeId";

            /// <summary>价格ID</summary>
            public const String PriceId = "PriceId";

            /// <summary>商品名称</summary>
            public const String Title = "Title";

            /// <summary>商品名称</summary>
            public const String Pic = "Pic";

            /// <summary>商品属性</summary>
            public const String Attr = "Attr";

            /// <summary>商品颜色</summary>
            public const String Color = "Color";

            /// <summary>规格</summary>
            public const String Spec = "Spec";

            /// <summary>编号</summary>
            public const String ItemNO = "ItemNO";

            /// <summary>数量</summary>
            public const String Qty = "Qty";

            /// <summary>商品价格</summary>
            public const String Price = "Price";

            /// <summary>商品市场价格</summary>
            public const String MarketPrice = "MarketPrice";

            /// <summary>商品特价</summary>
            public const String SpecialPrice = "SpecialPrice";

            /// <summary>商品优惠</summary>
            public const String Discount = "Discount";

            /// <summary>商品税</summary>
            public const String Tax = "Tax";

            /// <summary>积分</summary>
            public const String Credit = "Credit";

            /// <summary>返现积分</summary>
            public const String BackCredits = "BackCredits";

            /// <summary>是否完成</summary>
            public const String IsOK = "IsOK";

            /// <summary>是否已经评论</summary>
            public const String IsComment = "IsComment";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>入住日期</summary>
            public const String CheckInDate = "CheckInDate";

            /// <summary>离开日期</summary>
            public const String LeaveDate = "LeaveDate";

            /// <summary>系统类型</summary>
            public const String MyType = "MyType";
        }
        #endregion
    }
}