using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class OrderDetail : IOrderDetail
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int", Master = true)]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _OrderId;
        /// <summary>订单ID</summary>
        [DisplayName("订单ID")]
        [Description("订单ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单ID", "int")]
        public Int32 OrderId { get { return _OrderId; } set { if (OnPropertyChanging(__.OrderId, value)) { _OrderId = value; OnPropertyChanged(__.OrderId); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "nvarchar(50)")]
        public String OrderNum { get { return _OrderNum; } set { if (OnPropertyChanging(__.OrderNum, value)) { _OrderNum = value; OnPropertyChanged(__.OrderNum); } } }

        private Int32 _ShopId;
        /// <summary>商户ID</summary>
        [DisplayName("商户ID")]
        [Description("商户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "商户ID", "int")]
        public Int32 ShopId { get { return _ShopId; } set { if (OnPropertyChanging(__.ShopId, value)) { _ShopId = value; OnPropertyChanged(__.ShopId); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private Int32 _PId;
        /// <summary>商品ID</summary>
        [DisplayName("商品ID")]
        [Description("商品ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "商品ID", "int")]
        public Int32 PId { get { return _PId; } set { if (OnPropertyChanging(__.PId, value)) { _PId = value; OnPropertyChanged(__.PId); } } }

        private Int32 _TypeId;
        /// <summary>类型ID</summary>
        [DisplayName("类型ID")]
        [Description("类型ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "类型ID", "int")]
        public Int32 TypeId { get { return _TypeId; } set { if (OnPropertyChanging(__.TypeId, value)) { _TypeId = value; OnPropertyChanged(__.TypeId); } } }

        private Int32 _PriceId;
        /// <summary>价格ID</summary>
        [DisplayName("价格ID")]
        [Description("价格ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PriceId", "价格ID", "int")]
        public Int32 PriceId { get { return _PriceId; } set { if (OnPropertyChanging(__.PriceId, value)) { _PriceId = value; OnPropertyChanged(__.PriceId); } } }

        private String _Title;
        /// <summary>商品名称</summary>
        [DisplayName("商品名称")]
        [Description("商品名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "商品名称", "nvarchar(250)")]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Pic;
        /// <summary>商品名称</summary>
        [DisplayName("商品名称")]
        [Description("商品名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "商品名称", "nvarchar(250)")]
        public String Pic { get { return _Pic; } set { if (OnPropertyChanging(__.Pic, value)) { _Pic = value; OnPropertyChanged(__.Pic); } } }

        private String _Attr;
        /// <summary>商品属性</summary>
        [DisplayName("商品属性")]
        [Description("商品属性")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Attr", "商品属性", "nvarchar(250)")]
        public String Attr { get { return _Attr; } set { if (OnPropertyChanging(__.Attr, value)) { _Attr = value; OnPropertyChanged(__.Attr); } } }

        private String _Color;
        /// <summary>商品颜色</summary>
        [DisplayName("商品颜色")]
        [Description("商品颜色")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Color", "商品颜色", "nvarchar(50)")]
        public String Color { get { return _Color; } set { if (OnPropertyChanging(__.Color, value)) { _Color = value; OnPropertyChanged(__.Color); } } }

        private String _Spec;
        /// <summary>规格</summary>
        [DisplayName("规格")]
        [Description("规格")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Spec", "规格", "nvarchar(50)")]
        public String Spec { get { return _Spec; } set { if (OnPropertyChanging(__.Spec, value)) { _Spec = value; OnPropertyChanged(__.Spec); } } }

        private String _ItemNO;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ItemNO", "编号", "nvarchar(50)")]
        public String ItemNO { get { return _ItemNO; } set { if (OnPropertyChanging(__.ItemNO, value)) { _ItemNO = value; OnPropertyChanged(__.ItemNO); } } }

        private Int32 _Qty;
        /// <summary>数量</summary>
        [DisplayName("数量")]
        [Description("数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Qty", "数量", "int")]
        public Int32 Qty { get { return _Qty; } set { if (OnPropertyChanging(__.Qty, value)) { _Qty = value; OnPropertyChanged(__.Qty); } } }

        private Decimal _Price;
        /// <summary>商品价格</summary>
        [DisplayName("商品价格")]
        [Description("商品价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "商品价格", "money")]
        public Decimal Price { get { return _Price; } set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } } }

        private Decimal _MarketPrice;
        /// <summary>商品市场价格</summary>
        [DisplayName("商品市场价格")]
        [Description("商品市场价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MarketPrice", "商品市场价格", "money")]
        public Decimal MarketPrice { get { return _MarketPrice; } set { if (OnPropertyChanging(__.MarketPrice, value)) { _MarketPrice = value; OnPropertyChanged(__.MarketPrice); } } }

        private Decimal _SpecialPrice;
        /// <summary>商品特价</summary>
        [DisplayName("商品特价")]
        [Description("商品特价")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpecialPrice", "商品特价", "money")]
        public Decimal SpecialPrice { get { return _SpecialPrice; } set { if (OnPropertyChanging(__.SpecialPrice, value)) { _SpecialPrice = value; OnPropertyChanged(__.SpecialPrice); } } }

        private Decimal _Discount;
        /// <summary>商品优惠</summary>
        [DisplayName("商品优惠")]
        [Description("商品优惠")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "商品优惠", "money")]
        public Decimal Discount { get { return _Discount; } set { if (OnPropertyChanging(__.Discount, value)) { _Discount = value; OnPropertyChanged(__.Discount); } } }

        private Decimal _Tax;
        /// <summary>商品税</summary>
        [DisplayName("商品税")]
        [Description("商品税")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Tax", "商品税", "money")]
        public Decimal Tax { get { return _Tax; } set { if (OnPropertyChanging(__.Tax, value)) { _Tax = value; OnPropertyChanged(__.Tax); } } }

        private Int32 _Credit;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credit", "积分", "int")]
        public Int32 Credit { get { return _Credit; } set { if (OnPropertyChanging(__.Credit, value)) { _Credit = value; OnPropertyChanged(__.Credit); } } }

        private Int32 _BackCredits;
        /// <summary>返现积分</summary>
        [DisplayName("返现积分")]
        [Description("返现积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BackCredits", "返现积分", "int")]
        public Int32 BackCredits { get { return _BackCredits; } set { if (OnPropertyChanging(__.BackCredits, value)) { _BackCredits = value; OnPropertyChanged(__.BackCredits); } } }

        private Int32 _IsOK;
        /// <summary>是否完成</summary>
        [DisplayName("是否完成")]
        [Description("是否完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOK", "是否完成", "int")]
        public Int32 IsOK { get { return _IsOK; } set { if (OnPropertyChanging(__.IsOK, value)) { _IsOK = value; OnPropertyChanged(__.IsOK); } } }

        private Int32 _IsComment;
        /// <summary>是否已经评论</summary>
        [DisplayName("是否已经评论")]
        [Description("是否已经评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "是否已经评论", "int")]
        public Int32 IsComment { get { return _IsComment; } set { if (OnPropertyChanging(__.IsComment, value)) { _IsComment = value; OnPropertyChanged(__.IsComment); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _CheckInDate;
        /// <summary>入住日期</summary>
        [DisplayName("入住日期")]
        [Description("入住日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CheckInDate", "入住日期", "datetime")]
        public DateTime CheckInDate { get { return _CheckInDate; } set { if (OnPropertyChanging(__.CheckInDate, value)) { _CheckInDate = value; OnPropertyChanged(__.CheckInDate); } } }

        private DateTime _LeaveDate;
        /// <summary>离开日期</summary>
        [DisplayName("离开日期")]
        [Description("离开日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LeaveDate", "离开日期", "datetime")]
        public DateTime LeaveDate { get { return _LeaveDate; } set { if (OnPropertyChanging(__.LeaveDate, value)) { _LeaveDate = value; OnPropertyChanged(__.LeaveDate); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "int")]
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
                    case __.OrderId : return _OrderId;
                    case __.OrderNum : return _OrderNum;
                    case __.ShopId : return _ShopId;
                    case __.UId : return _UId;
                    case __.PId : return _PId;
                    case __.TypeId : return _TypeId;
                    case __.PriceId : return _PriceId;
                    case __.Title : return _Title;
                    case __.Pic : return _Pic;
                    case __.Attr : return _Attr;
                    case __.Color : return _Color;
                    case __.Spec : return _Spec;
                    case __.ItemNO : return _ItemNO;
                    case __.Qty : return _Qty;
                    case __.Price : return _Price;
                    case __.MarketPrice : return _MarketPrice;
                    case __.SpecialPrice : return _SpecialPrice;
                    case __.Discount : return _Discount;
                    case __.Tax : return _Tax;
                    case __.Credit : return _Credit;
                    case __.BackCredits : return _BackCredits;
                    case __.IsOK : return _IsOK;
                    case __.IsComment : return _IsComment;
                    case __.AddTime : return _AddTime;
                    case __.CheckInDate : return _CheckInDate;
                    case __.LeaveDate : return _LeaveDate;
                    case __.MyType : return _MyType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.OrderId : _OrderId = Convert.ToInt32(value); break;
                    case __.OrderNum : _OrderNum = Convert.ToString(value); break;
                    case __.ShopId : _ShopId = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.PId : _PId = Convert.ToInt32(value); break;
                    case __.TypeId : _TypeId = Convert.ToInt32(value); break;
                    case __.PriceId : _PriceId = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Pic : _Pic = Convert.ToString(value); break;
                    case __.Attr : _Attr = Convert.ToString(value); break;
                    case __.Color : _Color = Convert.ToString(value); break;
                    case __.Spec : _Spec = Convert.ToString(value); break;
                    case __.ItemNO : _ItemNO = Convert.ToString(value); break;
                    case __.Qty : _Qty = Convert.ToInt32(value); break;
                    case __.Price : _Price = Convert.ToDecimal(value); break;
                    case __.MarketPrice : _MarketPrice = Convert.ToDecimal(value); break;
                    case __.SpecialPrice : _SpecialPrice = Convert.ToDecimal(value); break;
                    case __.Discount : _Discount = Convert.ToDecimal(value); break;
                    case __.Tax : _Tax = Convert.ToDecimal(value); break;
                    case __.Credit : _Credit = Convert.ToInt32(value); break;
                    case __.BackCredits : _BackCredits = Convert.ToInt32(value); break;
                    case __.IsOK : _IsOK = Convert.ToInt32(value); break;
                    case __.IsComment : _IsComment = Convert.ToInt32(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.CheckInDate : _CheckInDate = Convert.ToDateTime(value); break;
                    case __.LeaveDate : _LeaveDate = Convert.ToDateTime(value); break;
                    case __.MyType : _MyType = Convert.ToInt32(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName(__.OrderId);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>商户ID</summary>
            public static readonly Field ShopId = FindByName(__.ShopId);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>商品ID</summary>
            public static readonly Field PId = FindByName(__.PId);

            /// <summary>类型ID</summary>
            public static readonly Field TypeId = FindByName(__.TypeId);

            /// <summary>价格ID</summary>
            public static readonly Field PriceId = FindByName(__.PriceId);

            /// <summary>商品名称</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>商品名称</summary>
            public static readonly Field Pic = FindByName(__.Pic);

            /// <summary>商品属性</summary>
            public static readonly Field Attr = FindByName(__.Attr);

            /// <summary>商品颜色</summary>
            public static readonly Field Color = FindByName(__.Color);

            /// <summary>规格</summary>
            public static readonly Field Spec = FindByName(__.Spec);

            /// <summary>编号</summary>
            public static readonly Field ItemNO = FindByName(__.ItemNO);

            /// <summary>数量</summary>
            public static readonly Field Qty = FindByName(__.Qty);

            /// <summary>商品价格</summary>
            public static readonly Field Price = FindByName(__.Price);

            /// <summary>商品市场价格</summary>
            public static readonly Field MarketPrice = FindByName(__.MarketPrice);

            /// <summary>商品特价</summary>
            public static readonly Field SpecialPrice = FindByName(__.SpecialPrice);

            /// <summary>商品优惠</summary>
            public static readonly Field Discount = FindByName(__.Discount);

            /// <summary>商品税</summary>
            public static readonly Field Tax = FindByName(__.Tax);

            /// <summary>积分</summary>
            public static readonly Field Credit = FindByName(__.Credit);

            /// <summary>返现积分</summary>
            public static readonly Field BackCredits = FindByName(__.BackCredits);

            /// <summary>是否完成</summary>
            public static readonly Field IsOK = FindByName(__.IsOK);

            /// <summary>是否已经评论</summary>
            public static readonly Field IsComment = FindByName(__.IsComment);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>入住日期</summary>
            public static readonly Field CheckInDate = FindByName(__.CheckInDate);

            /// <summary>离开日期</summary>
            public static readonly Field LeaveDate = FindByName(__.LeaveDate);

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>订单详情接口</summary>
    public partial interface IOrderDetail
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>订单ID</summary>
        Int32 OrderId { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>商户ID</summary>
        Int32 ShopId { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>商品ID</summary>
        Int32 PId { get; set; }

        /// <summary>类型ID</summary>
        Int32 TypeId { get; set; }

        /// <summary>价格ID</summary>
        Int32 PriceId { get; set; }

        /// <summary>商品名称</summary>
        String Title { get; set; }

        /// <summary>商品名称</summary>
        String Pic { get; set; }

        /// <summary>商品属性</summary>
        String Attr { get; set; }

        /// <summary>商品颜色</summary>
        String Color { get; set; }

        /// <summary>规格</summary>
        String Spec { get; set; }

        /// <summary>编号</summary>
        String ItemNO { get; set; }

        /// <summary>数量</summary>
        Int32 Qty { get; set; }

        /// <summary>商品价格</summary>
        Decimal Price { get; set; }

        /// <summary>商品市场价格</summary>
        Decimal MarketPrice { get; set; }

        /// <summary>商品特价</summary>
        Decimal SpecialPrice { get; set; }

        /// <summary>商品优惠</summary>
        Decimal Discount { get; set; }

        /// <summary>商品税</summary>
        Decimal Tax { get; set; }

        /// <summary>积分</summary>
        Int32 Credit { get; set; }

        /// <summary>返现积分</summary>
        Int32 BackCredits { get; set; }

        /// <summary>是否完成</summary>
        Int32 IsOK { get; set; }

        /// <summary>是否已经评论</summary>
        Int32 IsComment { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>入住日期</summary>
        DateTime CheckInDate { get; set; }

        /// <summary>离开日期</summary>
        DateTime LeaveDate { get; set; }

        /// <summary>系统类型</summary>
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