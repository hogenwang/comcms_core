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
    /// <summary>订单</summary>
    [Serializable]
    [DataObject]
    [Description("订单")]
    [BindTable("Order", Description = "订单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Order
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _UserName;
        /// <summary>下单用户名</summary>
        [DisplayName("下单用户名")]
        [Description("下单用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserName", "下单用户名", "")]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

        private Int32 _ShopId;
        /// <summary>商户ID</summary>
        [DisplayName("商户ID")]
        [Description("商户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "商户ID", "")]
        public Int32 ShopId { get => _ShopId; set { if (OnPropertyChanging("ShopId", value)) { _ShopId = value; OnPropertyChanged("ShopId"); } } }

        private Int32 _Credit;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credit", "积分", "")]
        public Int32 Credit { get => _Credit; set { if (OnPropertyChanging("Credit", value)) { _Credit = value; OnPropertyChanged("Credit"); } } }

        private Int32 _TotalQty;
        /// <summary>总数量</summary>
        [DisplayName("总数量")]
        [Description("总数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalQty", "总数量", "")]
        public Int32 TotalQty { get => _TotalQty; set { if (OnPropertyChanging("TotalQty", value)) { _TotalQty = value; OnPropertyChanged("TotalQty"); } } }

        private Decimal _TotalPrice;
        /// <summary>总价格</summary>
        [DisplayName("总价格")]
        [Description("总价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalPrice", "总价格", "")]
        public Decimal TotalPrice { get => _TotalPrice; set { if (OnPropertyChanging("TotalPrice", value)) { _TotalPrice = value; OnPropertyChanged("TotalPrice"); } } }

        private Decimal _Discount;
        /// <summary>折扣</summary>
        [DisplayName("折扣")]
        [Description("折扣")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "折扣", "")]
        public Decimal Discount { get => _Discount; set { if (OnPropertyChanging("Discount", value)) { _Discount = value; OnPropertyChanged("Discount"); } } }

        private Decimal _Fare;
        /// <summary>运费</summary>
        [DisplayName("运费")]
        [Description("运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Fare", "运费", "")]
        public Decimal Fare { get => _Fare; set { if (OnPropertyChanging("Fare", value)) { _Fare = value; OnPropertyChanged("Fare"); } } }

        private Decimal _TotalTax;
        /// <summary>税</summary>
        [DisplayName("税")]
        [Description("税")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalTax", "税", "")]
        public Decimal TotalTax { get => _TotalTax; set { if (OnPropertyChanging("TotalTax", value)) { _TotalTax = value; OnPropertyChanged("TotalTax"); } } }

        private Decimal _TotalPay;
        /// <summary>总支付价格</summary>
        [DisplayName("总支付价格")]
        [Description("总支付价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalPay", "总支付价格", "")]
        public Decimal TotalPay { get => _TotalPay; set { if (OnPropertyChanging("TotalPay", value)) { _TotalPay = value; OnPropertyChanged("TotalPay"); } } }

        private Int32 _BackCredits;
        /// <summary>返现积分</summary>
        [DisplayName("返现积分")]
        [Description("返现积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BackCredits", "返现积分", "")]
        public Int32 BackCredits { get => _BackCredits; set { if (OnPropertyChanging("BackCredits", value)) { _BackCredits = value; OnPropertyChanged("BackCredits"); } } }

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RealName", "姓名", "")]
        public String RealName { get => _RealName; set { if (OnPropertyChanging("RealName", value)) { _RealName = value; OnPropertyChanged("RealName"); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "")]
        public String Country { get => _Country; set { if (OnPropertyChanging("Country", value)) { _Country = value; OnPropertyChanged("Country"); } } }

        private String _Province;
        /// <summary>省份</summary>
        [DisplayName("省份")]
        [Description("省份")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省份", "")]
        public String Province { get => _Province; set { if (OnPropertyChanging("Province", value)) { _Province = value; OnPropertyChanged("Province"); } } }

        private String _City;
        /// <summary>城市</summary>
        [DisplayName("城市")]
        [Description("城市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "城市", "")]
        public String City { get => _City; set { if (OnPropertyChanging("City", value)) { _City = value; OnPropertyChanged("City"); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "")]
        public String District { get => _District; set { if (OnPropertyChanging("District", value)) { _District = value; OnPropertyChanged("District"); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "")]
        public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

        private String _PostCode;
        /// <summary>邮编</summary>
        [DisplayName("邮编")]
        [Description("邮编")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("PostCode", "邮编", "")]
        public String PostCode { get => _PostCode; set { if (OnPropertyChanging("PostCode", value)) { _PostCode = value; OnPropertyChanged("PostCode"); } } }

        private String _Tel;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Mobile;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Mobile", "手机", "")]
        public String Mobile { get => _Mobile; set { if (OnPropertyChanging("Mobile", value)) { _Mobile = value; OnPropertyChanged("Mobile"); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "")]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private String _Notes;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Notes", "备注", "")]
        public String Notes { get => _Notes; set { if (OnPropertyChanging("Notes", value)) { _Notes = value; OnPropertyChanged("Notes"); } } }

        private String _AdminNotes;
        /// <summary>管理员备注</summary>
        [DisplayName("管理员备注")]
        [Description("管理员备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("AdminNotes", "管理员备注", "")]
        public String AdminNotes { get => _AdminNotes; set { if (OnPropertyChanging("AdminNotes", value)) { _AdminNotes = value; OnPropertyChanged("AdminNotes"); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

        private Int32 _DeliverId;
        /// <summary>配送方式ID</summary>
        [DisplayName("配送方式ID")]
        [Description("配送方式ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeliverId", "配送方式ID", "")]
        public Int32 DeliverId { get => _DeliverId; set { if (OnPropertyChanging("DeliverId", value)) { _DeliverId = value; OnPropertyChanged("DeliverId"); } } }

        private String _DeliverType;
        /// <summary>配送方式名称</summary>
        [DisplayName("配送方式名称")]
        [Description("配送方式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverType", "配送方式名称", "")]
        public String DeliverType { get => _DeliverType; set { if (OnPropertyChanging("DeliverType", value)) { _DeliverType = value; OnPropertyChanged("DeliverType"); } } }

        private String _DeliverNO;
        /// <summary>运单号</summary>
        [DisplayName("运单号")]
        [Description("运单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverNO", "运单号", "")]
        public String DeliverNO { get => _DeliverNO; set { if (OnPropertyChanging("DeliverNO", value)) { _DeliverNO = value; OnPropertyChanged("DeliverNO"); } } }

        private String _DeliverNotes;
        /// <summary>配送备注</summary>
        [DisplayName("配送备注")]
        [Description("配送备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("DeliverNotes", "配送备注", "")]
        public String DeliverNotes { get => _DeliverNotes; set { if (OnPropertyChanging("DeliverNotes", value)) { _DeliverNotes = value; OnPropertyChanged("DeliverNotes"); } } }

        private Int32 _PayId;
        /// <summary>支付方式ID</summary>
        [DisplayName("支付方式ID")]
        [Description("支付方式ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PayId", "支付方式ID", "")]
        public Int32 PayId { get => _PayId; set { if (OnPropertyChanging("PayId", value)) { _PayId = value; OnPropertyChanged("PayId"); } } }

        private String _PayType;
        /// <summary>付款方式</summary>
        [DisplayName("付款方式")]
        [Description("付款方式")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PayType", "付款方式", "")]
        public String PayType { get => _PayType; set { if (OnPropertyChanging("PayType", value)) { _PayType = value; OnPropertyChanged("PayType"); } } }

        private String _PayTypeNotes;
        /// <summary>支付备注</summary>
        [DisplayName("支付备注")]
        [Description("支付备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("PayTypeNotes", "支付备注", "")]
        public String PayTypeNotes { get => _PayTypeNotes; set { if (OnPropertyChanging("PayTypeNotes", value)) { _PayTypeNotes = value; OnPropertyChanged("PayTypeNotes"); } } }

        private String _OrderStatus;
        /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
        [DisplayName("订单状态：未确认、已确认、已完成、已取消")]
        [Description("订单状态：未确认、已确认、已完成、已取消")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderStatus", "订单状态：未确认、已确认、已完成、已取消", "")]
        public String OrderStatus { get => _OrderStatus; set { if (OnPropertyChanging("OrderStatus", value)) { _OrderStatus = value; OnPropertyChanged("OrderStatus"); } } }

        private String _PaymentStatus;
        /// <summary>支付状态：未支付、已支付、已退款</summary>
        [DisplayName("支付状态：未支付、已支付、已退款")]
        [Description("支付状态：未支付、已支付、已退款")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PaymentStatus", "支付状态：未支付、已支付、已退款", "")]
        public String PaymentStatus { get => _PaymentStatus; set { if (OnPropertyChanging("PaymentStatus", value)) { _PaymentStatus = value; OnPropertyChanged("PaymentStatus"); } } }

        private String _DeliverStatus;
        /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
        [DisplayName("配送状态：未配送、配货中、已配送、已收到、退货中、已退货")]
        [Description("配送状态：未配送、配货中、已配送、已收到、退货中、已退货")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeliverStatus", "配送状态：未配送、配货中、已配送、已收到、退货中、已退货", "")]
        public String DeliverStatus { get => _DeliverStatus; set { if (OnPropertyChanging("DeliverStatus", value)) { _DeliverStatus = value; OnPropertyChanged("DeliverStatus"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private String _Ip;
        /// <summary>下单IP</summary>
        [DisplayName("下单IP")]
        [Description("下单IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "下单IP", "")]
        public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

        private Int32 _IsInvoice;
        /// <summary>是否需要发票</summary>
        [DisplayName("是否需要发票")]
        [Description("是否需要发票")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsInvoice", "是否需要发票", "")]
        public Int32 IsInvoice { get => _IsInvoice; set { if (OnPropertyChanging("IsInvoice", value)) { _IsInvoice = value; OnPropertyChanged("IsInvoice"); } } }

        private String _InvoiceCompanyName;
        /// <summary>抬头</summary>
        [DisplayName("抬头")]
        [Description("抬头")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InvoiceCompanyName", "抬头", "")]
        public String InvoiceCompanyName { get => _InvoiceCompanyName; set { if (OnPropertyChanging("InvoiceCompanyName", value)) { _InvoiceCompanyName = value; OnPropertyChanged("InvoiceCompanyName"); } } }

        private String _InvoiceCompanyID;
        /// <summary>纳税人识别号</summary>
        [DisplayName("纳税人识别号")]
        [Description("纳税人识别号")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("InvoiceCompanyID", "纳税人识别号", "")]
        public String InvoiceCompanyID { get => _InvoiceCompanyID; set { if (OnPropertyChanging("InvoiceCompanyID", value)) { _InvoiceCompanyID = value; OnPropertyChanged("InvoiceCompanyID"); } } }

        private String _InvoiceType;
        /// <summary>发票内容</summary>
        [DisplayName("发票内容")]
        [Description("发票内容")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InvoiceType", "发票内容", "")]
        public String InvoiceType { get => _InvoiceType; set { if (OnPropertyChanging("InvoiceType", value)) { _InvoiceType = value; OnPropertyChanged("InvoiceType"); } } }

        private String _InvoiceNote;
        /// <summary>发票备注</summary>
        [DisplayName("发票备注")]
        [Description("发票备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("InvoiceNote", "发票备注", "")]
        public String InvoiceNote { get => _InvoiceNote; set { if (OnPropertyChanging("InvoiceNote", value)) { _InvoiceNote = value; OnPropertyChanged("InvoiceNote"); } } }

        private Int32 _IsRead;
        /// <summary>是否未读</summary>
        [DisplayName("是否未读")]
        [Description("是否未读")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRead", "是否未读", "")]
        public Int32 IsRead { get => _IsRead; set { if (OnPropertyChanging("IsRead", value)) { _IsRead = value; OnPropertyChanged("IsRead"); } } }

        private Int32 _IsEnd;
        /// <summary>是否结束</summary>
        [DisplayName("是否结束")]
        [Description("是否结束")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsEnd", "是否结束", "")]
        public Int32 IsEnd { get => _IsEnd; set { if (OnPropertyChanging("IsEnd", value)) { _IsEnd = value; OnPropertyChanged("IsEnd"); } } }

        private DateTime _EndTime;
        /// <summary>结束时间</summary>
        [DisplayName("结束时间")]
        [Description("结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("EndTime", "结束时间", "")]
        public DateTime EndTime { get => _EndTime; set { if (OnPropertyChanging("EndTime", value)) { _EndTime = value; OnPropertyChanged("EndTime"); } } }

        private Int32 _IsOk;
        /// <summary>订单是否顺利完成</summary>
        [DisplayName("订单是否顺利完成")]
        [Description("订单是否顺利完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOk", "订单是否顺利完成", "")]
        public Int32 IsOk { get => _IsOk; set { if (OnPropertyChanging("IsOk", value)) { _IsOk = value; OnPropertyChanged("IsOk"); } } }

        private Int32 _IsComment;
        /// <summary>订单已经评论</summary>
        [DisplayName("订单已经评论")]
        [Description("订单已经评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "订单已经评论", "")]
        public Int32 IsComment { get => _IsComment; set { if (OnPropertyChanging("IsComment", value)) { _IsComment = value; OnPropertyChanged("IsComment"); } } }

        private String _Flag1;
        /// <summary>预留字段1</summary>
        [DisplayName("预留字段1")]
        [Description("预留字段1")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag1", "预留字段1", "")]
        public String Flag1 { get => _Flag1; set { if (OnPropertyChanging("Flag1", value)) { _Flag1 = value; OnPropertyChanged("Flag1"); } } }

        private String _Flag2;
        /// <summary>预留字段2</summary>
        [DisplayName("预留字段2")]
        [Description("预留字段2")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag2", "预留字段2", "")]
        public String Flag2 { get => _Flag2; set { if (OnPropertyChanging("Flag2", value)) { _Flag2 = value; OnPropertyChanged("Flag2"); } } }

        private String _Flag3;
        /// <summary>预留字段3</summary>
        [DisplayName("预留字段3")]
        [Description("预留字段3")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Flag3", "预留字段3", "")]
        public String Flag3 { get => _Flag3; set { if (OnPropertyChanging("Flag3", value)) { _Flag3 = value; OnPropertyChanged("Flag3"); } } }

        private String _Title;
        /// <summary>订单名称</summary>
        [DisplayName("订单名称")]
        [Description("订单名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Title", "订单名称", "")]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private DateTime _LastModTime;
        /// <summary>最后操作时间</summary>
        [DisplayName("最后操作时间")]
        [Description("最后操作时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastModTime", "最后操作时间", "")]
        public DateTime LastModTime { get => _LastModTime; set { if (OnPropertyChanging("LastModTime", value)) { _LastModTime = value; OnPropertyChanged("LastModTime"); } } }

        private Int32 _OrderType;
        /// <summary>订单类型，0为商品订单</summary>
        [DisplayName("订单类型")]
        [Description("订单类型，0为商品订单")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderType", "订单类型，0为商品订单", "")]
        public Int32 OrderType { get => _OrderType; set { if (OnPropertyChanging("OrderType", value)) { _OrderType = value; OnPropertyChanged("OrderType"); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }

        private String _OutTradeNo;
        /// <summary>支付成功流水号</summary>
        [DisplayName("支付成功流水号")]
        [Description("支付成功流水号")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("OutTradeNo", "支付成功流水号", "")]
        public String OutTradeNo { get => _OutTradeNo; set { if (OnPropertyChanging("OutTradeNo", value)) { _OutTradeNo = value; OnPropertyChanged("OutTradeNo"); } } }
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
                    case "OrderNum": return _OrderNum;
                    case "UId": return _UId;
                    case "UserName": return _UserName;
                    case "ShopId": return _ShopId;
                    case "Credit": return _Credit;
                    case "TotalQty": return _TotalQty;
                    case "TotalPrice": return _TotalPrice;
                    case "Discount": return _Discount;
                    case "Fare": return _Fare;
                    case "TotalTax": return _TotalTax;
                    case "TotalPay": return _TotalPay;
                    case "BackCredits": return _BackCredits;
                    case "RealName": return _RealName;
                    case "Country": return _Country;
                    case "Province": return _Province;
                    case "City": return _City;
                    case "District": return _District;
                    case "Address": return _Address;
                    case "PostCode": return _PostCode;
                    case "Tel": return _Tel;
                    case "Mobile": return _Mobile;
                    case "Email": return _Email;
                    case "Notes": return _Notes;
                    case "AdminNotes": return _AdminNotes;
                    case "Pic": return _Pic;
                    case "DeliverId": return _DeliverId;
                    case "DeliverType": return _DeliverType;
                    case "DeliverNO": return _DeliverNO;
                    case "DeliverNotes": return _DeliverNotes;
                    case "PayId": return _PayId;
                    case "PayType": return _PayType;
                    case "PayTypeNotes": return _PayTypeNotes;
                    case "OrderStatus": return _OrderStatus;
                    case "PaymentStatus": return _PaymentStatus;
                    case "DeliverStatus": return _DeliverStatus;
                    case "AddTime": return _AddTime;
                    case "Ip": return _Ip;
                    case "IsInvoice": return _IsInvoice;
                    case "InvoiceCompanyName": return _InvoiceCompanyName;
                    case "InvoiceCompanyID": return _InvoiceCompanyID;
                    case "InvoiceType": return _InvoiceType;
                    case "InvoiceNote": return _InvoiceNote;
                    case "IsRead": return _IsRead;
                    case "IsEnd": return _IsEnd;
                    case "EndTime": return _EndTime;
                    case "IsOk": return _IsOk;
                    case "IsComment": return _IsComment;
                    case "Flag1": return _Flag1;
                    case "Flag2": return _Flag2;
                    case "Flag3": return _Flag3;
                    case "Title": return _Title;
                    case "LastModTime": return _LastModTime;
                    case "OrderType": return _OrderType;
                    case "MyType": return _MyType;
                    case "OutTradeNo": return _OutTradeNo;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "ShopId": _ShopId = value.ToInt(); break;
                    case "Credit": _Credit = value.ToInt(); break;
                    case "TotalQty": _TotalQty = value.ToInt(); break;
                    case "TotalPrice": _TotalPrice = Convert.ToDecimal(value); break;
                    case "Discount": _Discount = Convert.ToDecimal(value); break;
                    case "Fare": _Fare = Convert.ToDecimal(value); break;
                    case "TotalTax": _TotalTax = Convert.ToDecimal(value); break;
                    case "TotalPay": _TotalPay = Convert.ToDecimal(value); break;
                    case "BackCredits": _BackCredits = value.ToInt(); break;
                    case "RealName": _RealName = Convert.ToString(value); break;
                    case "Country": _Country = Convert.ToString(value); break;
                    case "Province": _Province = Convert.ToString(value); break;
                    case "City": _City = Convert.ToString(value); break;
                    case "District": _District = Convert.ToString(value); break;
                    case "Address": _Address = Convert.ToString(value); break;
                    case "PostCode": _PostCode = Convert.ToString(value); break;
                    case "Tel": _Tel = Convert.ToString(value); break;
                    case "Mobile": _Mobile = Convert.ToString(value); break;
                    case "Email": _Email = Convert.ToString(value); break;
                    case "Notes": _Notes = Convert.ToString(value); break;
                    case "AdminNotes": _AdminNotes = Convert.ToString(value); break;
                    case "Pic": _Pic = Convert.ToString(value); break;
                    case "DeliverId": _DeliverId = value.ToInt(); break;
                    case "DeliverType": _DeliverType = Convert.ToString(value); break;
                    case "DeliverNO": _DeliverNO = Convert.ToString(value); break;
                    case "DeliverNotes": _DeliverNotes = Convert.ToString(value); break;
                    case "PayId": _PayId = value.ToInt(); break;
                    case "PayType": _PayType = Convert.ToString(value); break;
                    case "PayTypeNotes": _PayTypeNotes = Convert.ToString(value); break;
                    case "OrderStatus": _OrderStatus = Convert.ToString(value); break;
                    case "PaymentStatus": _PaymentStatus = Convert.ToString(value); break;
                    case "DeliverStatus": _DeliverStatus = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "IsInvoice": _IsInvoice = value.ToInt(); break;
                    case "InvoiceCompanyName": _InvoiceCompanyName = Convert.ToString(value); break;
                    case "InvoiceCompanyID": _InvoiceCompanyID = Convert.ToString(value); break;
                    case "InvoiceType": _InvoiceType = Convert.ToString(value); break;
                    case "InvoiceNote": _InvoiceNote = Convert.ToString(value); break;
                    case "IsRead": _IsRead = value.ToInt(); break;
                    case "IsEnd": _IsEnd = value.ToInt(); break;
                    case "EndTime": _EndTime = value.ToDateTime(); break;
                    case "IsOk": _IsOk = value.ToInt(); break;
                    case "IsComment": _IsComment = value.ToInt(); break;
                    case "Flag1": _Flag1 = Convert.ToString(value); break;
                    case "Flag2": _Flag2 = Convert.ToString(value); break;
                    case "Flag3": _Flag3 = Convert.ToString(value); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "LastModTime": _LastModTime = value.ToDateTime(); break;
                    case "OrderType": _OrderType = value.ToInt(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    case "OutTradeNo": _OutTradeNo = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>下单用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>商户ID</summary>
            public static readonly Field ShopId = FindByName("ShopId");

            /// <summary>积分</summary>
            public static readonly Field Credit = FindByName("Credit");

            /// <summary>总数量</summary>
            public static readonly Field TotalQty = FindByName("TotalQty");

            /// <summary>总价格</summary>
            public static readonly Field TotalPrice = FindByName("TotalPrice");

            /// <summary>折扣</summary>
            public static readonly Field Discount = FindByName("Discount");

            /// <summary>运费</summary>
            public static readonly Field Fare = FindByName("Fare");

            /// <summary>税</summary>
            public static readonly Field TotalTax = FindByName("TotalTax");

            /// <summary>总支付价格</summary>
            public static readonly Field TotalPay = FindByName("TotalPay");

            /// <summary>返现积分</summary>
            public static readonly Field BackCredits = FindByName("BackCredits");

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName("RealName");

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName("Country");

            /// <summary>省份</summary>
            public static readonly Field Province = FindByName("Province");

            /// <summary>城市</summary>
            public static readonly Field City = FindByName("City");

            /// <summary>区</summary>
            public static readonly Field District = FindByName("District");

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName("Address");

            /// <summary>邮编</summary>
            public static readonly Field PostCode = FindByName("PostCode");

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>手机</summary>
            public static readonly Field Mobile = FindByName("Mobile");

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>备注</summary>
            public static readonly Field Notes = FindByName("Notes");

            /// <summary>管理员备注</summary>
            public static readonly Field AdminNotes = FindByName("AdminNotes");

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName("Pic");

            /// <summary>配送方式ID</summary>
            public static readonly Field DeliverId = FindByName("DeliverId");

            /// <summary>配送方式名称</summary>
            public static readonly Field DeliverType = FindByName("DeliverType");

            /// <summary>运单号</summary>
            public static readonly Field DeliverNO = FindByName("DeliverNO");

            /// <summary>配送备注</summary>
            public static readonly Field DeliverNotes = FindByName("DeliverNotes");

            /// <summary>支付方式ID</summary>
            public static readonly Field PayId = FindByName("PayId");

            /// <summary>付款方式</summary>
            public static readonly Field PayType = FindByName("PayType");

            /// <summary>支付备注</summary>
            public static readonly Field PayTypeNotes = FindByName("PayTypeNotes");

            /// <summary>订单状态：未确认、已确认、已完成、已取消</summary>
            public static readonly Field OrderStatus = FindByName("OrderStatus");

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public static readonly Field PaymentStatus = FindByName("PaymentStatus");

            /// <summary>配送状态：未配送、配货中、已配送、已收到、退货中、已退货</summary>
            public static readonly Field DeliverStatus = FindByName("DeliverStatus");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>下单IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>是否需要发票</summary>
            public static readonly Field IsInvoice = FindByName("IsInvoice");

            /// <summary>抬头</summary>
            public static readonly Field InvoiceCompanyName = FindByName("InvoiceCompanyName");

            /// <summary>纳税人识别号</summary>
            public static readonly Field InvoiceCompanyID = FindByName("InvoiceCompanyID");

            /// <summary>发票内容</summary>
            public static readonly Field InvoiceType = FindByName("InvoiceType");

            /// <summary>发票备注</summary>
            public static readonly Field InvoiceNote = FindByName("InvoiceNote");

            /// <summary>是否未读</summary>
            public static readonly Field IsRead = FindByName("IsRead");

            /// <summary>是否结束</summary>
            public static readonly Field IsEnd = FindByName("IsEnd");

            /// <summary>结束时间</summary>
            public static readonly Field EndTime = FindByName("EndTime");

            /// <summary>订单是否顺利完成</summary>
            public static readonly Field IsOk = FindByName("IsOk");

            /// <summary>订单已经评论</summary>
            public static readonly Field IsComment = FindByName("IsComment");

            /// <summary>预留字段1</summary>
            public static readonly Field Flag1 = FindByName("Flag1");

            /// <summary>预留字段2</summary>
            public static readonly Field Flag2 = FindByName("Flag2");

            /// <summary>预留字段3</summary>
            public static readonly Field Flag3 = FindByName("Flag3");

            /// <summary>订单名称</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>最后操作时间</summary>
            public static readonly Field LastModTime = FindByName("LastModTime");

            /// <summary>订单类型，0为商品订单</summary>
            public static readonly Field OrderType = FindByName("OrderType");

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName("MyType");

            /// <summary>支付成功流水号</summary>
            public static readonly Field OutTradeNo = FindByName("OutTradeNo");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}