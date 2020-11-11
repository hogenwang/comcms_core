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
    /// <summary>在线支付订单</summary>
    [Serializable]
    [DataObject]
    [Description("在线支付订单")]
    [BindTable("OnlinePayOrder", Description = "在线支付订单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OnlinePayOrder
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _PayOrderNum;
        /// <summary>在线支付订单号，唯一</summary>
        [DisplayName("在线支付订单号")]
        [Description("在线支付订单号，唯一")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PayOrderNum", "在线支付订单号，唯一", "")]
        public String PayOrderNum { get => _PayOrderNum; set { if (OnPropertyChanging("PayOrderNum", value)) { _PayOrderNum = value; OnPropertyChanged("PayOrderNum"); } } }

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

        private String _Actions;
        /// <summary>日志记录</summary>
        [DisplayName("日志记录")]
        [Description("日志记录")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Actions", "日志记录", "")]
        public String Actions { get => _Actions; set { if (OnPropertyChanging("Actions", value)) { _Actions = value; OnPropertyChanged("Actions"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserName", "用户名", "")]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

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

        private String _PaymentStatus;
        /// <summary>支付状态：未支付、已支付、已退款</summary>
        [DisplayName("支付状态：未支付、已支付、已退款")]
        [Description("支付状态：未支付、已支付、已退款")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PaymentStatus", "支付状态：未支付、已支付、已退款", "")]
        public String PaymentStatus { get => _PaymentStatus; set { if (OnPropertyChanging("PaymentStatus", value)) { _PaymentStatus = value; OnPropertyChanged("PaymentStatus"); } } }

        private String _PaymentNotes;
        /// <summary>支付备注</summary>
        [DisplayName("支付备注")]
        [Description("支付备注")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("PaymentNotes", "支付备注", "")]
        public String PaymentNotes { get => _PaymentNotes; set { if (OnPropertyChanging("PaymentNotes", value)) { _PaymentNotes = value; OnPropertyChanged("PaymentNotes"); } } }

        private Int32 _IsOK;
        /// <summary>是否完成</summary>
        [DisplayName("是否完成")]
        [Description("是否完成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsOK", "是否完成", "")]
        public Int32 IsOK { get => _IsOK; set { if (OnPropertyChanging("IsOK", value)) { _IsOK = value; OnPropertyChanged("IsOK"); } } }

        private String _Ip;
        /// <summary>下单IP</summary>
        [DisplayName("下单IP")]
        [Description("下单IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "下单IP", "")]
        public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _ReceiveTime;
        /// <summary>回传时间</summary>
        [DisplayName("回传时间")]
        [Description("回传时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ReceiveTime", "回传时间", "")]
        public DateTime ReceiveTime { get => _ReceiveTime; set { if (OnPropertyChanging("ReceiveTime", value)) { _ReceiveTime = value; OnPropertyChanged("ReceiveTime"); } } }

        private Int32 _TypeId;
        /// <summary>支付的类型</summary>
        [DisplayName("支付的类型")]
        [Description("支付的类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "支付的类型", "")]
        public Int32 TypeId { get => _TypeId; set { if (OnPropertyChanging("TypeId", value)) { _TypeId = value; OnPropertyChanged("TypeId"); } } }

        private Int32 _MyType;
        /// <summary>系统类型</summary>
        [DisplayName("系统类型")]
        [Description("系统类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "系统类型", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }

        private String _Title;
        /// <summary>订单标题</summary>
        [DisplayName("订单标题")]
        [Description("订单标题")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Title", "订单标题", "")]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

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
                    case "PayOrderNum": return _PayOrderNum;
                    case "OrderId": return _OrderId;
                    case "OrderNum": return _OrderNum;
                    case "PayId": return _PayId;
                    case "PayType": return _PayType;
                    case "PayTypeNotes": return _PayTypeNotes;
                    case "Actions": return _Actions;
                    case "UId": return _UId;
                    case "UserName": return _UserName;
                    case "TotalQty": return _TotalQty;
                    case "TotalPrice": return _TotalPrice;
                    case "PaymentStatus": return _PaymentStatus;
                    case "PaymentNotes": return _PaymentNotes;
                    case "IsOK": return _IsOK;
                    case "Ip": return _Ip;
                    case "AddTime": return _AddTime;
                    case "ReceiveTime": return _ReceiveTime;
                    case "TypeId": return _TypeId;
                    case "MyType": return _MyType;
                    case "Title": return _Title;
                    case "OutTradeNo": return _OutTradeNo;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "PayOrderNum": _PayOrderNum = Convert.ToString(value); break;
                    case "OrderId": _OrderId = value.ToInt(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "PayId": _PayId = value.ToInt(); break;
                    case "PayType": _PayType = Convert.ToString(value); break;
                    case "PayTypeNotes": _PayTypeNotes = Convert.ToString(value); break;
                    case "Actions": _Actions = Convert.ToString(value); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "TotalQty": _TotalQty = value.ToInt(); break;
                    case "TotalPrice": _TotalPrice = Convert.ToDecimal(value); break;
                    case "PaymentStatus": _PaymentStatus = Convert.ToString(value); break;
                    case "PaymentNotes": _PaymentNotes = Convert.ToString(value); break;
                    case "IsOK": _IsOK = value.ToInt(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "ReceiveTime": _ReceiveTime = value.ToDateTime(); break;
                    case "TypeId": _TypeId = value.ToInt(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "OutTradeNo": _OutTradeNo = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>在线支付订单号，唯一</summary>
            public static readonly Field PayOrderNum = FindByName("PayOrderNum");

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName("OrderId");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>支付方式ID</summary>
            public static readonly Field PayId = FindByName("PayId");

            /// <summary>付款方式</summary>
            public static readonly Field PayType = FindByName("PayType");

            /// <summary>支付备注</summary>
            public static readonly Field PayTypeNotes = FindByName("PayTypeNotes");

            /// <summary>日志记录</summary>
            public static readonly Field Actions = FindByName("Actions");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>总数量</summary>
            public static readonly Field TotalQty = FindByName("TotalQty");

            /// <summary>总价格</summary>
            public static readonly Field TotalPrice = FindByName("TotalPrice");

            /// <summary>支付状态：未支付、已支付、已退款</summary>
            public static readonly Field PaymentStatus = FindByName("PaymentStatus");

            /// <summary>支付备注</summary>
            public static readonly Field PaymentNotes = FindByName("PaymentNotes");

            /// <summary>是否完成</summary>
            public static readonly Field IsOK = FindByName("IsOK");

            /// <summary>下单IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>回传时间</summary>
            public static readonly Field ReceiveTime = FindByName("ReceiveTime");

            /// <summary>支付的类型</summary>
            public static readonly Field TypeId = FindByName("TypeId");

            /// <summary>系统类型</summary>
            public static readonly Field MyType = FindByName("MyType");

            /// <summary>订单标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>支付成功流水号</summary>
            public static readonly Field OutTradeNo = FindByName("OutTradeNo");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}