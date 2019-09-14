using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>订单</summary>
    [Serializable]
    [DataObject]
    [Description("订单")]
    [BindTable("Order", Description = "订单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Order : IOrder
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get { return _OrderNum; } set { if (OnPropertyChanging(__.OrderNum, value)) { _OrderNum = value; OnPropertyChanged(__.OrderNum); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _UserName;
        /// <summary>下单用户名</summary>
        [DisplayName("下单用户名")]
        [Description("下单用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserName", "下单用户名", "")]
        public String UserName { get { return _UserName; } set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } } }

        private Int32 _ShopId;
        /// <summary>商户ID</summary>
        [DisplayName("商户ID")]
        [Description("商户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "商户ID", "")]
        public Int32 ShopId { get { return _ShopId; } set { if (OnPropertyChanging(__.ShopId, value)) { _ShopId = value; OnPropertyChanged(__.ShopId); } } }

        private Int32 _Credit;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credit", "积分", "")]
        public Int32 Credit { get { return _Credit; } set { if (OnPropertyChanging(__.Credit, value)) { _Credit = value; OnPropertyChanged(__.Credit); } } }

        private Int32 _TotalQty;
        /// <summary>总数量</summary>
        [DisplayName("总数量")]
        [Description("总数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalQty", "总数量", "")]
        public Int32 TotalQty { get { return _TotalQty; } set { if (OnPropertyChanging(__.TotalQty, value)) { _TotalQty = value; OnPropertyChanged(__.TotalQty); } } }

        private Decimal _TotalPrice;
        /// <summary>总价格</summary>
        [DisplayName("总价格")]
        [Description("总价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalPrice", "总价格", "")]
        public Decimal TotalPrice { get { return _TotalPrice; } set { if (OnPropertyChanging(__.TotalPrice, value)) { _TotalPrice = value; OnPropertyChanged(__.TotalPrice); } } }

        private Decimal _Discount;
        /// <summary>折扣</summary>
        [DisplayName("折扣")]
        [Description("折扣")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "折扣", "")]
        public Decimal Discount { get { return _Discount; } set { if (OnPropertyChanging(__.Discount, value)) { _Discount = value; OnPropertyChanged(__.Discount); } } }

        private Decimal _Fare;
        /// <summary>运费</summary>
        [DisplayName("运费")]
        [Description("运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Fare", "运费", "")]
        public Decimal Fare { get { return _Fare; } set { if (OnPropertyChanging(__.Fare, value)) { _Fare = value; OnPropertyChanged(__.Fare); } } }

        private Decimal _TotalTax;
        /// <summary>税</summary>
        [DisplayName("税")]
        [Description("税")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalTax", "税", "")]
        public Decimal TotalTax { get { return _TotalTax; } set { if (OnPropertyChanging(__.TotalTax, value)) { _TotalTax = value; OnPropertyChanged(__.TotalTax); } } }

        private Decimal _TotalPay;
        /// <summary>总支付价格</summary>
        [DisplayName("总支付价格")]
        [Description("总支付价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalPay", "总支付价格", "")]
        public Decimal TotalPay { get { return _TotalPay; } set { if (OnPropertyChanging(__.TotalPay, value)) { _TotalPay = value; OnPropertyChanged(__.TotalPay); } } }

        private Int32 _BackCredits;
        /// <summary>返现积分</summary>
        [DisplayName("返现积分")]
        [Description("返现积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BackCredits", "返现积分", "")]
        public Int32 BackCredits { get { return _BackCredits; } set { if (OnPropertyChanging(__.BackCredits, value)) { _BackCredits = value; OnPropertyChanged(__.BackCredits); } } }

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RealName", "姓名", "")]
        public String RealName { get { return _RealName; } set { if (OnPropertyChanging(__.RealName, value)) { _RealName = value; OnPropertyChanged(__.RealName); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "")]
        public String Country { get { return _Country; } set { if (OnPropertyChanging(__.Country, value)) { _Country = value; OnPropertyChanged(__.Country); } } }

        private String _Province;
        /// <summary>省份</summary>
        [DisplayName("省份")]
        [Description("省份")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省份", "")]
        public String Province { get { return _Province; } set { if (OnPropertyChanging(__.Province, value)) { _Province = value; OnPropertyChanged(__.Province); } } }

        private String _City;
        /// <summary>城市</summary>
        [DisplayName("城市")]
        [Description("城市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "城市", "")]
        public String City { get { return _City; } set { if (OnPropertyChanging(__.City, value)) { _City = value; OnPropertyChanged(__.City); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "")]
        public String District { get { return _District; } set { if (OnPropertyChanging(__.District, value)) { _District = value; OnPropertyChanged(__.District); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "")]
        public String Address { get { return _Address; } set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } } }

        private String _PostCode;
        /// <summary>邮编</summary>
        [DisplayName("邮编")]
        [Description("邮编")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("PostCode", "邮编", "")]
        public String PostCode { get { return _PostCode; } set { if (OnPropertyChanging(__.PostCode, value)) { _PostCode = value; OnPropertyChanged(__.PostCode); } } }

        private String _Tel;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _Mobile;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Mobile", "手机", "")]
        public String Mobile { get { return _Mobile; } set { if (OnPropertyChanging(__.Mobile, value)) { _Mobile = value; OnPropertyChanged(__.Mobile); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "")]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private String _Notes;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Notes", "备注", "")]
        public String Notes { get { return _Notes; } set { if (OnPropertyChanging(__.Notes, value)) { _Notes = value; OnPropertyChanged(__.Notes); } } }

        private String _AdminNotes;
        /// <summary>管理员备注</summary>
        [DisplayName("管理员备注")]
        [Description("管理员备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("AdminNotes", "管理员备注", "")]
        public String AdminNotes { get { return _AdminNotes; } set { if (OnPropertyChanging(__.AdminNotes, value)) { _AdminNotes = value; OnPropertyChanged(__.AdminNotes); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get { return _Pic; } set { if (OnPropertyChanging(__.Pic, value)) { _Pic = value; OnPropertyChanged(__.Pic); } } }

        private Int32 _DeliverId;
        /// <summary>配送方式ID</summary>
        [DisplayName("配送方式ID")]
        [Description("配送方式ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeliverId", "配送方式ID", "")]
        public Int32 DeliverId { get { return _DeliverId; } set { if (OnPropertyChanging(__.DeliverId, value)) { _DeliverId = value; OnPropertyChanged(__.DeliverId); } } }

        private String _DeliverType;
        /// <summary>配送方式名称</summary>
        [DisplayName("配送方式名称")]
        [Description("配送方式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverType", "配送方式名称", "")]
        public String DeliverType { get { return _DeliverType; } set { if (OnPropertyChanging(__.DeliverType, value)) { _DeliverType = value; OnPropertyChanged(__.DeliverType); } } }

        private String _DeliverNO;
        /// <summary>运单号</summary>
        [DisplayName("运单号")]
        [Description("运单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverNO", "运单号", "")]
        public String DeliverNO { get { return _DeliverNO; } set { if (OnPropertyChanging(__.DeliverNO, value)) { _DeliverNO = value; OnPropertyChanged(__.DeliverNO); } } }

        private String _DeliverNotes;
        /// <summary>配送备注</summary>
        [DisplayName("配送备注")]
        [Description("配送备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("DeliverNotes", "配送备注", "")]
        public String DeliverNotes { get { return _DeliverNotes; } set { if (OnPropertyChanging(__.DeliverNotes, value)) { _DeliverNotes = value; OnPropertyChanged(__.DeliverNotes); } } }

        private Int32 _PayId;
        /// <summary>支付方式ID</summary>
        [DisplayName("支付方式ID")]
        [Description("支付方式ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PayId", "支付方式ID", "")]
        public Int32 PayId { get { return _PayId; } set { if (OnPropertyChanging(__.PayId, value)) { _PayId = value; OnPropertyChanged(__.PayId); } } }

        private String _PayType;
        /// <summary>付款方式</summary>
        [DisplayName("付款方式")]
        [Description("付款方式")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PayType", "付款方式", "")]
        public String PayType { get { return _PayType; } set { if (OnPropertyChanging(__.PayType, value)) { _PayType = value; OnPropertyChanged(__.PayType); } } }

        private String _PayTypeNotes;
        /// <summary>支付备注</summary>
        [DisplayName("支付备注")]
        [Description("支付备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("PayTypeNotes", "支付备注", "")]
        public String PayTypeNotes { get { return _PayTypeNotes; } set { if (OnPropertyChanging(__.PayTypeNotes, value)) { _PayTypeNotes = value; OnPropertyChanged(__.PayTypeNotes); } } }

        private String _OrderStatus;
        /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
        [DisplayName("订单状态：未确认、已确认、已完成、已取消")]
        [Description("订单状态：未确认、已确认、已完成、已取消")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderStatus", "订单状态：未确认、已确认、已完成、已取消", "")]
        public String OrderStatus { get { return _OrderStatus; } set { if (OnPropertyChanging(__.OrderStatus, value)) { _OrderStatus = value; OnPropertyChanged(__.OrderStatus); } } }

        private String _PaymentStatus;
        /// <summary>支付状态：未支付、已支付、已退款</summary>
        [DisplayName("支付状态：未支付、已支付、已退款")]
        [Description("支付状态：未支付、已支付、已退款")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PaymentStatus", "支付状态：未支付、已支付、已退款", "")]
        public String PaymentStatus { get { return _PaymentStatus; } set { if (OnPropertyChanging(__.PaymentStatus, value)) { _PaymentStatus = value; OnPropertyChanged(__.PaymentStatus); } } }

        private String _DeliverStatus;
        /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
        [DisplayName("配送状态：未配送、配货中、已配送、已收到、退货中、已退货")]
        [Description("配送状态：未配送、配货中、已配送、已收到、退货中、已退货")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverStatus", "配送状态：未配送、配货中、已配送、已收到、退货中、已退货", "")]
        public String DeliverStatus { get { return _DeliverStatus; } set { if (OnPropertyChanging(__.DeliverStatus, value)) { _DeliverStatus = value; OnPropertyChanged(__.DeliverStatus); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private String _Ip;
        /// <summary>下单IP</summary>
        [DisplayName("下单IP")]
        [Description("下单IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "下单IP", "")]
        public String Ip { get { return _Ip; } set { if (OnPropertyChanging(__.Ip, value)) { _Ip = value; OnPropertyChanged(__.Ip); } } }

        private Int32 _IsInvoice;
        /// <summary>是否需要发票</summary>
        [DisplayName("是否需要发票")]
        [Description("是否需要发票")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsInvoice", "是否需要发票", "")]
        public Int32 IsInvoice { get { return _IsInvoice; } set { if (OnPropertyChanging(__.IsInvoice, value)) { _IsInvoice = value; OnPropertyChanged(__.IsInvoice); } } }

        private String _InvoiceCompanyName;
        /// <summary>抬头</summary>
        [DisplayName("抬头")]
        [Description("抬头")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InvoiceCompanyName", "抬头", "")]
        public String InvoiceCompanyName { get { return _InvoiceCompanyName; } set { if (OnPropertyChanging(__.InvoiceCompanyName, value)) { _InvoiceCompanyName = value; OnPropertyChanged(__.InvoiceCompanyName); } } }

        private String _InvoiceCompanyID;
        /// <summary>纳税人识别号</summary>
        [DisplayName("纳税人识别号")]
        [Description("纳税人识别号")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("InvoiceCompanyID", "纳税人识别号", "")]
        public String InvoiceCompanyID { get { return _InvoiceCompanyID; } set { if (OnPropertyChanging(__.InvoiceCompanyID, value)) { _InvoiceCompanyID = value; OnPropertyChanged(__.InvoiceCompanyID); } } }

        private String _InvoiceType;
        /// <summary>发票内容</summary>
        [DisplayName("发票内容")]
        [Description("发票内容")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InvoiceType", "发票内容", "")]
        public String InvoiceType { get { return _InvoiceType; } set { if (OnPropertyChanging(__.InvoiceType, value)) { _InvoiceType = value; OnPropertyChanged(__.InvoiceType); } } }

        private String _InvoiceNote;
        /// <summary>发票备注</summary>
        [DisplayName("发票备注")]
        [Description("发票备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("InvoiceNote", "发票备注", "")]
        public String InvoiceNote { get { return _InvoiceNote; } set { if (OnPropertyChanging(__.InvoiceNote, value)) { _InvoiceNote = value; OnPropertyChanged(__.InvoiceNote); } } }

        private Int32 _IsRead;
        /// <summary>是否未读</summary>
        [DisplayName("是否未读")]
        [Description("是否未读")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRead", "是否未读", "")]
        public Int32 IsRead { get { return _IsRead; } set { if (OnPropertyChanging(__.IsRead, value)) { _IsRead = value; OnPropertyChanged(__.IsRead); } } }

        private Int32 _IsEnd;
        /// <summary>是否结束</summary>
        [DisplayName("是否结束")]
        [Description("是否结束")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsEnd", "是否结束", "")]
        public Int32 IsEnd { get { return _IsEnd; } set { if (OnPropertyChanging(__.IsEnd, value)) { _IsEnd = value; OnPropertyChanged(__.IsEnd); } } }

        private DateTime _EndTime;
        /// <summary>结束时间</summary>
        [DisplayName("结束时间")]
        [Description("结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "结束时间", "")]
        public DateTime EndTime { get { return _EndTime; } set { if (OnPropertyChanging(__.EndTime, value)) { _EndTime = value; OnPropertyChanged(__.EndTime); } } }

        private Int32 _IsOk;
        /// <summary>订单是否顺利完成</summary>
        [DisplayName("订单是否顺利完成")]
        [Description("订单是否顺利完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOk", "订单是否顺利完成", "")]
        public Int32 IsOk { get { return _IsOk; } set { if (OnPropertyChanging(__.IsOk, value)) { _IsOk = value; OnPropertyChanged(__.IsOk); } } }

        private Int32 _IsComment;
        /// <summary>订单已经评论</summary>
        [DisplayName("订单已经评论")]
        [Description("订单已经评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "订单已经评论", "")]
        public Int32 IsComment { get { return _IsComment; } set { if (OnPropertyChanging(__.IsComment, value)) { _IsComment = value; OnPropertyChanged(__.IsComment); } } }

        private String _Flag1;
        /// <summary>预留字段1</summary>
        [DisplayName("预留字段1")]
        [Description("预留字段1")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag1", "预留字段1", "")]
        public String Flag1 { get { return _Flag1; } set { if (OnPropertyChanging(__.Flag1, value)) { _Flag1 = value; OnPropertyChanged(__.Flag1); } } }

        private String _Flag2;
        /// <summary>预留字段2</summary>
        [DisplayName("预留字段2")]
        [Description("预留字段2")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag2", "预留字段2", "")]
        public String Flag2 { get { return _Flag2; } set { if (OnPropertyChanging(__.Flag2, value)) { _Flag2 = value; OnPropertyChanged(__.Flag2); } } }

        private String _Flag3;
        /// <summary>预留字段3</summary>
        [DisplayName("预留字段3")]
        [Description("预留字段3")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag3", "预留字段3", "")]
        public String Flag3 { get { return _Flag3; } set { if (OnPropertyChanging(__.Flag3, value)) { _Flag3 = value; OnPropertyChanged(__.Flag3); } } }

        private String _Title;
        /// <summary>订单名称</summary>
        [DisplayName("订单名称")]
        [Description("订单名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Title", "订单名称", "")]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private DateTime _LastModTime;
        /// <summary>最后操作时间</summary>
        [DisplayName("最后操作时间")]
        [Description("最后操作时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastModTime", "最后操作时间", "")]
        public DateTime LastModTime { get { return _LastModTime; } set { if (OnPropertyChanging(__.LastModTime, value)) { _LastModTime = value; OnPropertyChanged(__.LastModTime); } } }

        private Int32 _OrderType;
        /// <summary>订单类型，0为商品订单</summary>
        [DisplayName("订单类型")]
        [Description("订单类型，0为商品订单")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderType", "订单类型，0为商品订单", "")]
        public Int32 OrderType { get { return _OrderType; } set { if (OnPropertyChanging(__.OrderType, value)) { _OrderType = value; OnPropertyChanged(__.OrderType); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "")]
        public Int32 MyType { get { return _MyType; } set { if (OnPropertyChanging(__.MyType, value)) { _MyType = value; OnPropertyChanged(__.MyType); } } }

        private String _OutTradeNo;
        /// <summary>支付成功流水号</summary>
        [DisplayName("支付成功流水号")]
        [Description("支付成功流水号")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("OutTradeNo", "支付成功流水号", "")]
        public String OutTradeNo { get { return _OutTradeNo; } set { if (OnPropertyChanging(__.OutTradeNo, value)) { _OutTradeNo = value; OnPropertyChanged(__.OutTradeNo); } } }
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
                    case __.OrderNum : return _OrderNum;
                    case __.UId : return _UId;
                    case __.UserName : return _UserName;
                    case __.ShopId : return _ShopId;
                    case __.Credit : return _Credit;
                    case __.TotalQty : return _TotalQty;
                    case __.TotalPrice : return _TotalPrice;
                    case __.Discount : return _Discount;
                    case __.Fare : return _Fare;
                    case __.TotalTax : return _TotalTax;
                    case __.TotalPay : return _TotalPay;
                    case __.BackCredits : return _BackCredits;
                    case __.RealName : return _RealName;
                    case __.Country : return _Country;
                    case __.Province : return _Province;
                    case __.City : return _City;
                    case __.District : return _District;
                    case __.Address : return _Address;
                    case __.PostCode : return _PostCode;
                    case __.Tel : return _Tel;
                    case __.Mobile : return _Mobile;
                    case __.Email : return _Email;
                    case __.Notes : return _Notes;
                    case __.AdminNotes : return _AdminNotes;
                    case __.Pic : return _Pic;
                    case __.DeliverId : return _DeliverId;
                    case __.DeliverType : return _DeliverType;
                    case __.DeliverNO : return _DeliverNO;
                    case __.DeliverNotes : return _DeliverNotes;
                    case __.PayId : return _PayId;
                    case __.PayType : return _PayType;
                    case __.PayTypeNotes : return _PayTypeNotes;
                    case __.OrderStatus : return _OrderStatus;
                    case __.PaymentStatus : return _PaymentStatus;
                    case __.DeliverStatus : return _DeliverStatus;
                    case __.AddTime : return _AddTime;
                    case __.Ip : return _Ip;
                    case __.IsInvoice : return _IsInvoice;
                    case __.InvoiceCompanyName : return _InvoiceCompanyName;
                    case __.InvoiceCompanyID : return _InvoiceCompanyID;
                    case __.InvoiceType : return _InvoiceType;
                    case __.InvoiceNote : return _InvoiceNote;
                    case __.IsRead : return _IsRead;
                    case __.IsEnd : return _IsEnd;
                    case __.EndTime : return _EndTime;
                    case __.IsOk : return _IsOk;
                    case __.IsComment : return _IsComment;
                    case __.Flag1 : return _Flag1;
                    case __.Flag2 : return _Flag2;
                    case __.Flag3 : return _Flag3;
                    case __.Title : return _Title;
                    case __.LastModTime : return _LastModTime;
                    case __.OrderType : return _OrderType;
                    case __.MyType : return _MyType;
                    case __.OutTradeNo : return _OutTradeNo;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = value.ToInt(); break;
                    case __.OrderNum : _OrderNum = Convert.ToString(value); break;
                    case __.UId : _UId = value.ToInt(); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.ShopId : _ShopId = value.ToInt(); break;
                    case __.Credit : _Credit = value.ToInt(); break;
                    case __.TotalQty : _TotalQty = value.ToInt(); break;
                    case __.TotalPrice : _TotalPrice = Convert.ToDecimal(value); break;
                    case __.Discount : _Discount = Convert.ToDecimal(value); break;
                    case __.Fare : _Fare = Convert.ToDecimal(value); break;
                    case __.TotalTax : _TotalTax = Convert.ToDecimal(value); break;
                    case __.TotalPay : _TotalPay = Convert.ToDecimal(value); break;
                    case __.BackCredits : _BackCredits = value.ToInt(); break;
                    case __.RealName : _RealName = Convert.ToString(value); break;
                    case __.Country : _Country = Convert.ToString(value); break;
                    case __.Province : _Province = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.District : _District = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.PostCode : _PostCode = Convert.ToString(value); break;
                    case __.Tel : _Tel = Convert.ToString(value); break;
                    case __.Mobile : _Mobile = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.Notes : _Notes = Convert.ToString(value); break;
                    case __.AdminNotes : _AdminNotes = Convert.ToString(value); break;
                    case __.Pic : _Pic = Convert.ToString(value); break;
                    case __.DeliverId : _DeliverId = value.ToInt(); break;
                    case __.DeliverType : _DeliverType = Convert.ToString(value); break;
                    case __.DeliverNO : _DeliverNO = Convert.ToString(value); break;
                    case __.DeliverNotes : _DeliverNotes = Convert.ToString(value); break;
                    case __.PayId : _PayId = value.ToInt(); break;
                    case __.PayType : _PayType = Convert.ToString(value); break;
                    case __.PayTypeNotes : _PayTypeNotes = Convert.ToString(value); break;
                    case __.OrderStatus : _OrderStatus = Convert.ToString(value); break;
                    case __.PaymentStatus : _PaymentStatus = Convert.ToString(value); break;
                    case __.DeliverStatus : _DeliverStatus = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = value.ToDateTime(); break;
                    case __.Ip : _Ip = Convert.ToString(value); break;
                    case __.IsInvoice : _IsInvoice = value.ToInt(); break;
                    case __.InvoiceCompanyName : _InvoiceCompanyName = Convert.ToString(value); break;
                    case __.InvoiceCompanyID : _InvoiceCompanyID = Convert.ToString(value); break;
                    case __.InvoiceType : _InvoiceType = Convert.ToString(value); break;
                    case __.InvoiceNote : _InvoiceNote = Convert.ToString(value); break;
                    case __.IsRead : _IsRead = value.ToInt(); break;
                    case __.IsEnd : _IsEnd = value.ToInt(); break;
                    case __.EndTime : _EndTime = value.ToDateTime(); break;
                    case __.IsOk : _IsOk = value.ToInt(); break;
                    case __.IsComment : _IsComment = value.ToInt(); break;
                    case __.Flag1 : _Flag1 = Convert.ToString(value); break;
                    case __.Flag2 : _Flag2 = Convert.ToString(value); break;
                    case __.Flag3 : _Flag3 = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.LastModTime : _LastModTime = value.ToDateTime(); break;
                    case __.OrderType : _OrderType = value.ToInt(); break;
                    case __.MyType : _MyType = value.ToInt(); break;
                    case __.OutTradeNo : _OutTradeNo = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得订单字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>下单用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>商户ID</summary>
            public static readonly Field ShopId = FindByName(__.ShopId);

            /// <summary>积分</summary>
            public static readonly Field Credit = FindByName(__.Credit);

            /// <summary>总数量</summary>
            public static readonly Field TotalQty = FindByName(__.TotalQty);

            /// <summary>总价格</summary>
            public static readonly Field TotalPrice = FindByName(__.TotalPrice);

            /// <summary>折扣</summary>
            public static readonly Field Discount = FindByName(__.Discount);

            /// <summary>运费</summary>
            public static readonly Field Fare = FindByName(__.Fare);

            /// <summary>税</summary>
            public static readonly Field TotalTax = FindByName(__.TotalTax);

            /// <summary>总支付价格</summary>
            public static readonly Field TotalPay = FindByName(__.TotalPay);

            /// <summary>返现积分</summary>
            public static readonly Field BackCredits = FindByName(__.BackCredits);

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName(__.RealName);

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName(__.Country);

            /// <summary>省份</summary>
            public static readonly Field Province = FindByName(__.Province);

            /// <summary>城市</summary>
            public static readonly Field City = FindByName(__.City);

            /// <summary>区</summary>
            public static readonly Field District = FindByName(__.District);

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            /// <summary>邮编</summary>
            public static readonly Field PostCode = FindByName(__.PostCode);

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>手机</summary>
            public static readonly Field Mobile = FindByName(__.Mobile);

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>备注</summary>
            public static readonly Field Notes = FindByName(__.Notes);

            /// <summary>管理员备注</summary>
            public static readonly Field AdminNotes = FindByName(__.AdminNotes);

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName(__.Pic);

            /// <summary>配送方式ID</summary>
            public static readonly Field DeliverId = FindByName(__.DeliverId);

            /// <summary>配送方式名称</summary>
            public static readonly Field DeliverType = FindByName(__.DeliverType);

            /// <summary>运单号</summary>
            public static readonly Field DeliverNO = FindByName(__.DeliverNO);

            /// <summary>配送备注</summary>
            public static readonly Field DeliverNotes = FindByName(__.DeliverNotes);

            /// <summary>支付方式ID</summary>
            public static readonly Field PayId = FindByName(__.PayId);

            /// <summary>付款方式</summary>
            public static readonly Field PayType = FindByName(__.PayType);

            /// <summary>支付备注</summary>
            public static readonly Field PayTypeNotes = FindByName(__.PayTypeNotes);

            /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
            public static readonly Field OrderStatus = FindByName(__.OrderStatus);

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public static readonly Field PaymentStatus = FindByName(__.PaymentStatus);

            /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
            public static readonly Field DeliverStatus = FindByName(__.DeliverStatus);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>下单IP</summary>
            public static readonly Field Ip = FindByName(__.Ip);

            /// <summary>是否需要发票</summary>
            public static readonly Field IsInvoice = FindByName(__.IsInvoice);

            /// <summary>抬头</summary>
            public static readonly Field InvoiceCompanyName = FindByName(__.InvoiceCompanyName);

            /// <summary>纳税人识别号</summary>
            public static readonly Field InvoiceCompanyID = FindByName(__.InvoiceCompanyID);

            /// <summary>发票内容</summary>
            public static readonly Field InvoiceType = FindByName(__.InvoiceType);

            /// <summary>发票备注</summary>
            public static readonly Field InvoiceNote = FindByName(__.InvoiceNote);

            /// <summary>是否未读</summary>
            public static readonly Field IsRead = FindByName(__.IsRead);

            /// <summary>是否结束</summary>
            public static readonly Field IsEnd = FindByName(__.IsEnd);

            /// <summary>结束时间</summary>
            public static readonly Field EndTime = FindByName(__.EndTime);

            /// <summary>订单是否顺利完成</summary>
            public static readonly Field IsOk = FindByName(__.IsOk);

            /// <summary>订单已经评论</summary>
            public static readonly Field IsComment = FindByName(__.IsComment);

            /// <summary>预留字段1</summary>
            public static readonly Field Flag1 = FindByName(__.Flag1);

            /// <summary>预留字段2</summary>
            public static readonly Field Flag2 = FindByName(__.Flag2);

            /// <summary>预留字段3</summary>
            public static readonly Field Flag3 = FindByName(__.Flag3);

            /// <summary>订单名称</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>最后操作时间</summary>
            public static readonly Field LastModTime = FindByName(__.LastModTime);

            /// <summary>订单类型，0为商品订单</summary>
            public static readonly Field OrderType = FindByName(__.OrderType);

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            /// <summary>支付成功流水号</summary>
            public static readonly Field OutTradeNo = FindByName(__.OutTradeNo);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得订单字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>下单用户名</summary>
            public const String UserName = "UserName";

            /// <summary>商户ID</summary>
            public const String ShopId = "ShopId";

            /// <summary>积分</summary>
            public const String Credit = "Credit";

            /// <summary>总数量</summary>
            public const String TotalQty = "TotalQty";

            /// <summary>总价格</summary>
            public const String TotalPrice = "TotalPrice";

            /// <summary>折扣</summary>
            public const String Discount = "Discount";

            /// <summary>运费</summary>
            public const String Fare = "Fare";

            /// <summary>税</summary>
            public const String TotalTax = "TotalTax";

            /// <summary>总支付价格</summary>
            public const String TotalPay = "TotalPay";

            /// <summary>返现积分</summary>
            public const String BackCredits = "BackCredits";

            /// <summary>姓名</summary>
            public const String RealName = "RealName";

            /// <summary>国家</summary>
            public const String Country = "Country";

            /// <summary>省份</summary>
            public const String Province = "Province";

            /// <summary>城市</summary>
            public const String City = "City";

            /// <summary>区</summary>
            public const String District = "District";

            /// <summary>详细地址</summary>
            public const String Address = "Address";

            /// <summary>邮编</summary>
            public const String PostCode = "PostCode";

            /// <summary>手机</summary>
            public const String Tel = "Tel";

            /// <summary>手机</summary>
            public const String Mobile = "Mobile";

            /// <summary>邮箱</summary>
            public const String Email = "Email";

            /// <summary>备注</summary>
            public const String Notes = "Notes";

            /// <summary>管理员备注</summary>
            public const String AdminNotes = "AdminNotes";

            /// <summary>图片</summary>
            public const String Pic = "Pic";

            /// <summary>配送方式ID</summary>
            public const String DeliverId = "DeliverId";

            /// <summary>配送方式名称</summary>
            public const String DeliverType = "DeliverType";

            /// <summary>运单号</summary>
            public const String DeliverNO = "DeliverNO";

            /// <summary>配送备注</summary>
            public const String DeliverNotes = "DeliverNotes";

            /// <summary>支付方式ID</summary>
            public const String PayId = "PayId";

            /// <summary>付款方式</summary>
            public const String PayType = "PayType";

            /// <summary>支付备注</summary>
            public const String PayTypeNotes = "PayTypeNotes";

            /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
            public const String OrderStatus = "OrderStatus";

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public const String PaymentStatus = "PaymentStatus";

            /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
            public const String DeliverStatus = "DeliverStatus";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>下单IP</summary>
            public const String Ip = "Ip";

            /// <summary>是否需要发票</summary>
            public const String IsInvoice = "IsInvoice";

            /// <summary>抬头</summary>
            public const String InvoiceCompanyName = "InvoiceCompanyName";

            /// <summary>纳税人识别号</summary>
            public const String InvoiceCompanyID = "InvoiceCompanyID";

            /// <summary>发票内容</summary>
            public const String InvoiceType = "InvoiceType";

            /// <summary>发票备注</summary>
            public const String InvoiceNote = "InvoiceNote";

            /// <summary>是否未读</summary>
            public const String IsRead = "IsRead";

            /// <summary>是否结束</summary>
            public const String IsEnd = "IsEnd";

            /// <summary>结束时间</summary>
            public const String EndTime = "EndTime";

            /// <summary>订单是否顺利完成</summary>
            public const String IsOk = "IsOk";

            /// <summary>订单已经评论</summary>
            public const String IsComment = "IsComment";

            /// <summary>预留字段1</summary>
            public const String Flag1 = "Flag1";

            /// <summary>预留字段2</summary>
            public const String Flag2 = "Flag2";

            /// <summary>预留字段3</summary>
            public const String Flag3 = "Flag3";

            /// <summary>订单名称</summary>
            public const String Title = "Title";

            /// <summary>最后操作时间</summary>
            public const String LastModTime = "LastModTime";

            /// <summary>订单类型，0为商品订单</summary>
            public const String OrderType = "OrderType";

            /// <summary>系统类型</summary>
            public const String MyType = "MyType";

            /// <summary>支付成功流水号</summary>
            public const String OutTradeNo = "OutTradeNo";
        }
        #endregion
    }

    /// <summary>订单接口</summary>
    public partial interface IOrder
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>下单用户名</summary>
        String UserName { get; set; }

        /// <summary>商户ID</summary>
        Int32 ShopId { get; set; }

        /// <summary>积分</summary>
        Int32 Credit { get; set; }

        /// <summary>总数量</summary>
        Int32 TotalQty { get; set; }

        /// <summary>总价格</summary>
        Decimal TotalPrice { get; set; }

        /// <summary>折扣</summary>
        Decimal Discount { get; set; }

        /// <summary>运费</summary>
        Decimal Fare { get; set; }

        /// <summary>税</summary>
        Decimal TotalTax { get; set; }

        /// <summary>总支付价格</summary>
        Decimal TotalPay { get; set; }

        /// <summary>返现积分</summary>
        Int32 BackCredits { get; set; }

        /// <summary>姓名</summary>
        String RealName { get; set; }

        /// <summary>国家</summary>
        String Country { get; set; }

        /// <summary>省份</summary>
        String Province { get; set; }

        /// <summary>城市</summary>
        String City { get; set; }

        /// <summary>区</summary>
        String District { get; set; }

        /// <summary>详细地址</summary>
        String Address { get; set; }

        /// <summary>邮编</summary>
        String PostCode { get; set; }

        /// <summary>手机</summary>
        String Tel { get; set; }

        /// <summary>手机</summary>
        String Mobile { get; set; }

        /// <summary>邮箱</summary>
        String Email { get; set; }

        /// <summary>备注</summary>
        String Notes { get; set; }

        /// <summary>管理员备注</summary>
        String AdminNotes { get; set; }

        /// <summary>图片</summary>
        String Pic { get; set; }

        /// <summary>配送方式ID</summary>
        Int32 DeliverId { get; set; }

        /// <summary>配送方式名称</summary>
        String DeliverType { get; set; }

        /// <summary>运单号</summary>
        String DeliverNO { get; set; }

        /// <summary>配送备注</summary>
        String DeliverNotes { get; set; }

        /// <summary>支付方式ID</summary>
        Int32 PayId { get; set; }

        /// <summary>付款方式</summary>
        String PayType { get; set; }

        /// <summary>支付备注</summary>
        String PayTypeNotes { get; set; }

        /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
        String OrderStatus { get; set; }

        /// <summary>支付状态：未支付、已支付、已退款</summary>
        String PaymentStatus { get; set; }

        /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
        String DeliverStatus { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>下单IP</summary>
        String Ip { get; set; }

        /// <summary>是否需要发票</summary>
        Int32 IsInvoice { get; set; }

        /// <summary>抬头</summary>
        String InvoiceCompanyName { get; set; }

        /// <summary>纳税人识别号</summary>
        String InvoiceCompanyID { get; set; }

        /// <summary>发票内容</summary>
        String InvoiceType { get; set; }

        /// <summary>发票备注</summary>
        String InvoiceNote { get; set; }

        /// <summary>是否未读</summary>
        Int32 IsRead { get; set; }

        /// <summary>是否结束</summary>
        Int32 IsEnd { get; set; }

        /// <summary>结束时间</summary>
        DateTime EndTime { get; set; }

        /// <summary>订单是否顺利完成</summary>
        Int32 IsOk { get; set; }

        /// <summary>订单已经评论</summary>
        Int32 IsComment { get; set; }

        /// <summary>预留字段1</summary>
        String Flag1 { get; set; }

        /// <summary>预留字段2</summary>
        String Flag2 { get; set; }

        /// <summary>预留字段3</summary>
        String Flag3 { get; set; }

        /// <summary>订单名称</summary>
        String Title { get; set; }

        /// <summary>最后操作时间</summary>
        DateTime LastModTime { get; set; }

        /// <summary>订单类型，0为商品订单</summary>
        Int32 OrderType { get; set; }

        /// <summary>系统类型</summary>
        Int32 MyType { get; set; }

        /// <summary>支付成功流水号</summary>
        String OutTradeNo { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}