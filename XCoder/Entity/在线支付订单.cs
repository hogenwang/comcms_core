using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>在线支付订单</summary>
    [Serializable]
    [DataObject]
    [Description("在线支付订单")]
    [BindTable("OnlinePayOrder", Description = "在线支付订单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OnlinePayOrder : IOnlinePayOrder
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _PayOrderNum;
        /// <summary>在线支付订单号，唯一</summary>
        [DisplayName("在线支付订单号")]
        [Description("在线支付订单号，唯一")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PayOrderNum", "在线支付订单号，唯一", "")]
        public String PayOrderNum { get { return _PayOrderNum; } set { if (OnPropertyChanging(__.PayOrderNum, value)) { _PayOrderNum = value; OnPropertyChanged(__.PayOrderNum); } } }

        private Int32 _OrderId;
        /// <summary>订单ID</summary>
        [DisplayName("订单ID")]
        [Description("订单ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单ID", "")]
        public Int32 OrderId { get { return _OrderId; } set { if (OnPropertyChanging(__.OrderId, value)) { _OrderId = value; OnPropertyChanged(__.OrderId); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get { return _OrderNum; } set { if (OnPropertyChanging(__.OrderNum, value)) { _OrderNum = value; OnPropertyChanged(__.OrderNum); } } }

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

        private String _Actions;
        /// <summary>日志记录</summary>
        [DisplayName("日志记录")]
        [Description("日志记录")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Actions", "日志记录", "")]
        public String Actions { get { return _Actions; } set { if (OnPropertyChanging(__.Actions, value)) { _Actions = value; OnPropertyChanged(__.Actions); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserName", "用户名", "")]
        public String UserName { get { return _UserName; } set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } } }

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

        private String _PaymentStatus;
        /// <summary>支付状态：未支付、已支付、已退款</summary>
        [DisplayName("支付状态：未支付、已支付、已退款")]
        [Description("支付状态：未支付、已支付、已退款")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PaymentStatus", "支付状态：未支付、已支付、已退款", "")]
        public String PaymentStatus { get { return _PaymentStatus; } set { if (OnPropertyChanging(__.PaymentStatus, value)) { _PaymentStatus = value; OnPropertyChanged(__.PaymentStatus); } } }

        private String _PaymentNotes;
        /// <summary>支付备注</summary>
        [DisplayName("支付备注")]
        [Description("支付备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("PaymentNotes", "支付备注", "")]
        public String PaymentNotes { get { return _PaymentNotes; } set { if (OnPropertyChanging(__.PaymentNotes, value)) { _PaymentNotes = value; OnPropertyChanged(__.PaymentNotes); } } }

        private Int32 _IsOK;
        /// <summary>是否完成</summary>
        [DisplayName("是否完成")]
        [Description("是否完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOK", "是否完成", "")]
        public Int32 IsOK { get { return _IsOK; } set { if (OnPropertyChanging(__.IsOK, value)) { _IsOK = value; OnPropertyChanged(__.IsOK); } } }

        private String _Ip;
        /// <summary>下单IP</summary>
        [DisplayName("下单IP")]
        [Description("下单IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "下单IP", "")]
        public String Ip { get { return _Ip; } set { if (OnPropertyChanging(__.Ip, value)) { _Ip = value; OnPropertyChanged(__.Ip); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _ReceiveTime;
        /// <summary>回传时间</summary>
        [DisplayName("回传时间")]
        [Description("回传时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ReceiveTime", "回传时间", "")]
        public DateTime ReceiveTime { get { return _ReceiveTime; } set { if (OnPropertyChanging(__.ReceiveTime, value)) { _ReceiveTime = value; OnPropertyChanged(__.ReceiveTime); } } }

        private Int32 _TypeId;
        /// <summary>支付的类型</summary>
        [DisplayName("支付的类型")]
        [Description("支付的类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "支付的类型", "")]
        public Int32 TypeId { get { return _TypeId; } set { if (OnPropertyChanging(__.TypeId, value)) { _TypeId = value; OnPropertyChanged(__.TypeId); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "")]
        public Int32 MyType { get { return _MyType; } set { if (OnPropertyChanging(__.MyType, value)) { _MyType = value; OnPropertyChanged(__.MyType); } } }

        private String _Title;
        /// <summary>订单标题</summary>
        [DisplayName("订单标题")]
        [Description("订单标题")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Title", "订单标题", "")]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

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
                    case __.PayOrderNum : return _PayOrderNum;
                    case __.OrderId : return _OrderId;
                    case __.OrderNum : return _OrderNum;
                    case __.PayId : return _PayId;
                    case __.PayType : return _PayType;
                    case __.PayTypeNotes : return _PayTypeNotes;
                    case __.Actions : return _Actions;
                    case __.UId : return _UId;
                    case __.UserName : return _UserName;
                    case __.TotalQty : return _TotalQty;
                    case __.TotalPrice : return _TotalPrice;
                    case __.PaymentStatus : return _PaymentStatus;
                    case __.PaymentNotes : return _PaymentNotes;
                    case __.IsOK : return _IsOK;
                    case __.Ip : return _Ip;
                    case __.AddTime : return _AddTime;
                    case __.ReceiveTime : return _ReceiveTime;
                    case __.TypeId : return _TypeId;
                    case __.MyType : return _MyType;
                    case __.Title : return _Title;
                    case __.OutTradeNo : return _OutTradeNo;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = value.ToInt(); break;
                    case __.PayOrderNum : _PayOrderNum = Convert.ToString(value); break;
                    case __.OrderId : _OrderId = value.ToInt(); break;
                    case __.OrderNum : _OrderNum = Convert.ToString(value); break;
                    case __.PayId : _PayId = value.ToInt(); break;
                    case __.PayType : _PayType = Convert.ToString(value); break;
                    case __.PayTypeNotes : _PayTypeNotes = Convert.ToString(value); break;
                    case __.Actions : _Actions = Convert.ToString(value); break;
                    case __.UId : _UId = value.ToInt(); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.TotalQty : _TotalQty = value.ToInt(); break;
                    case __.TotalPrice : _TotalPrice = Convert.ToDecimal(value); break;
                    case __.PaymentStatus : _PaymentStatus = Convert.ToString(value); break;
                    case __.PaymentNotes : _PaymentNotes = Convert.ToString(value); break;
                    case __.IsOK : _IsOK = value.ToInt(); break;
                    case __.Ip : _Ip = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = value.ToDateTime(); break;
                    case __.ReceiveTime : _ReceiveTime = value.ToDateTime(); break;
                    case __.TypeId : _TypeId = value.ToInt(); break;
                    case __.MyType : _MyType = value.ToInt(); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.OutTradeNo : _OutTradeNo = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得在线支付订单字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>在线支付订单号，唯一</summary>
            public static readonly Field PayOrderNum = FindByName(__.PayOrderNum);

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName(__.OrderId);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>支付方式ID</summary>
            public static readonly Field PayId = FindByName(__.PayId);

            /// <summary>付款方式</summary>
            public static readonly Field PayType = FindByName(__.PayType);

            /// <summary>支付备注</summary>
            public static readonly Field PayTypeNotes = FindByName(__.PayTypeNotes);

            /// <summary>日志记录</summary>
            public static readonly Field Actions = FindByName(__.Actions);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>总数量</summary>
            public static readonly Field TotalQty = FindByName(__.TotalQty);

            /// <summary>总价格</summary>
            public static readonly Field TotalPrice = FindByName(__.TotalPrice);

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public static readonly Field PaymentStatus = FindByName(__.PaymentStatus);

            /// <summary>支付备注</summary>
            public static readonly Field PaymentNotes = FindByName(__.PaymentNotes);

            /// <summary>是否完成</summary>
            public static readonly Field IsOK = FindByName(__.IsOK);

            /// <summary>下单IP</summary>
            public static readonly Field Ip = FindByName(__.Ip);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>回传时间</summary>
            public static readonly Field ReceiveTime = FindByName(__.ReceiveTime);

            /// <summary>支付的类型</summary>
            public static readonly Field TypeId = FindByName(__.TypeId);

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            /// <summary>订单标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>支付成功流水号</summary>
            public static readonly Field OutTradeNo = FindByName(__.OutTradeNo);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得在线支付订单字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>在线支付订单号，唯一</summary>
            public const String PayOrderNum = "PayOrderNum";

            /// <summary>订单ID</summary>
            public const String OrderId = "OrderId";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>支付方式ID</summary>
            public const String PayId = "PayId";

            /// <summary>付款方式</summary>
            public const String PayType = "PayType";

            /// <summary>支付备注</summary>
            public const String PayTypeNotes = "PayTypeNotes";

            /// <summary>日志记录</summary>
            public const String Actions = "Actions";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>用户名</summary>
            public const String UserName = "UserName";

            /// <summary>总数量</summary>
            public const String TotalQty = "TotalQty";

            /// <summary>总价格</summary>
            public const String TotalPrice = "TotalPrice";

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public const String PaymentStatus = "PaymentStatus";

            /// <summary>支付备注</summary>
            public const String PaymentNotes = "PaymentNotes";

            /// <summary>是否完成</summary>
            public const String IsOK = "IsOK";

            /// <summary>下单IP</summary>
            public const String Ip = "Ip";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>回传时间</summary>
            public const String ReceiveTime = "ReceiveTime";

            /// <summary>支付的类型</summary>
            public const String TypeId = "TypeId";

            /// <summary>系统类型</summary>
            public const String MyType = "MyType";

            /// <summary>订单标题</summary>
            public const String Title = "Title";

            /// <summary>支付成功流水号</summary>
            public const String OutTradeNo = "OutTradeNo";
        }
        #endregion
    }

    /// <summary>在线支付订单接口</summary>
    public partial interface IOnlinePayOrder
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>在线支付订单号，唯一</summary>
        String PayOrderNum { get; set; }

        /// <summary>订单ID</summary>
        Int32 OrderId { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>支付方式ID</summary>
        Int32 PayId { get; set; }

        /// <summary>付款方式</summary>
        String PayType { get; set; }

        /// <summary>支付备注</summary>
        String PayTypeNotes { get; set; }

        /// <summary>日志记录</summary>
        String Actions { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>总数量</summary>
        Int32 TotalQty { get; set; }

        /// <summary>总价格</summary>
        Decimal TotalPrice { get; set; }

        /// <summary>支付状态：未支付、已支付、已退款</summary>
        String PaymentStatus { get; set; }

        /// <summary>支付备注</summary>
        String PaymentNotes { get; set; }

        /// <summary>是否完成</summary>
        Int32 IsOK { get; set; }

        /// <summary>下单IP</summary>
        String Ip { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>回传时间</summary>
        DateTime ReceiveTime { get; set; }

        /// <summary>支付的类型</summary>
        Int32 TypeId { get; set; }

        /// <summary>系统类型</summary>
        Int32 MyType { get; set; }

        /// <summary>订单标题</summary>
        String Title { get; set; }

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