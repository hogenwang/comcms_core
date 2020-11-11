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
    /// <summary>用户返现余额变化记录</summary>
    [Serializable]
    [DataObject]
    [Description("用户返现余额变化记录")]
    [BindTable("RebateChangeLog", Description = "用户返现余额变化记录", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class RebateChangeLog
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
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private Int32 _AdminId;
        /// <summary>管理员ID</summary>
        [DisplayName("管理员ID")]
        [Description("管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AdminId", "管理员ID", "")]
        public Int32 AdminId { get => _AdminId; set { if (OnPropertyChanging("AdminId", value)) { _AdminId = value; OnPropertyChanged("AdminId"); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("UserName", "用户名", "")]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

        private DateTime _AddTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private String _Ip;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "登录IP", "")]
        public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

        private String _Actions;
        /// <summary>记录</summary>
        [DisplayName("记录")]
        [Description("记录")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Actions", "记录", "", Master = true)]
        public String Actions { get => _Actions; set { if (OnPropertyChanging("Actions", value)) { _Actions = value; OnPropertyChanged("Actions"); } } }

        private Decimal _Reward;
        /// <summary>返现、扣除金额</summary>
        [DisplayName("返现、扣除金额")]
        [Description("返现、扣除金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Reward", "返现、扣除金额", "")]
        public Decimal Reward { get => _Reward; set { if (OnPropertyChanging("Reward", value)) { _Reward = value; OnPropertyChanged("Reward"); } } }

        private Decimal _BeforChange;
        /// <summary>变化前</summary>
        [DisplayName("变化前")]
        [Description("变化前")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BeforChange", "变化前", "")]
        public Decimal BeforChange { get => _BeforChange; set { if (OnPropertyChanging("BeforChange", value)) { _BeforChange = value; OnPropertyChanged("BeforChange"); } } }

        private Decimal _AfterChange;
        /// <summary>变化后</summary>
        [DisplayName("变化后")]
        [Description("变化后")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AfterChange", "变化后", "")]
        public Decimal AfterChange { get => _AfterChange; set { if (OnPropertyChanging("AfterChange", value)) { _AfterChange = value; OnPropertyChanged("AfterChange"); } } }

        private String _LogDetails;
        /// <summary>详细记录</summary>
        [DisplayName("详细记录")]
        [Description("详细记录")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LogDetails", "详细记录", "", Master = true)]
        public String LogDetails { get => _LogDetails; set { if (OnPropertyChanging("LogDetails", value)) { _LogDetails = value; OnPropertyChanged("LogDetails"); } } }

        private Int32 _TypeId;
        /// <summary>类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成</summary>
        [DisplayName("类型0充值1购买2赠送3退款4分销提成")]
        [Description("类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TypeId", "类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成", "")]
        public Int32 TypeId { get => _TypeId; set { if (OnPropertyChanging("TypeId", value)) { _TypeId = value; OnPropertyChanged("TypeId"); } } }

        private Int32 _MyType;
        /// <summary>消费类型，见MyType</summary>
        [DisplayName("消费类型")]
        [Description("消费类型，见MyType")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "消费类型，见MyType", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }

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
        [BindColumn("OrderNum", "订单号", "", Master = true)]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }
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
                    case "AdminId": return _AdminId;
                    case "UserName": return _UserName;
                    case "AddTime": return _AddTime;
                    case "Ip": return _Ip;
                    case "Actions": return _Actions;
                    case "Reward": return _Reward;
                    case "BeforChange": return _BeforChange;
                    case "AfterChange": return _AfterChange;
                    case "LogDetails": return _LogDetails;
                    case "TypeId": return _TypeId;
                    case "MyType": return _MyType;
                    case "OrderId": return _OrderId;
                    case "OrderNum": return _OrderNum;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "AdminId": _AdminId = value.ToInt(); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "Actions": _Actions = Convert.ToString(value); break;
                    case "Reward": _Reward = Convert.ToDecimal(value); break;
                    case "BeforChange": _BeforChange = Convert.ToDecimal(value); break;
                    case "AfterChange": _AfterChange = Convert.ToDecimal(value); break;
                    case "LogDetails": _LogDetails = Convert.ToString(value); break;
                    case "TypeId": _TypeId = value.ToInt(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    case "OrderId": _OrderId = value.ToInt(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户返现余额变化记录字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>管理员ID</summary>
            public static readonly Field AdminId = FindByName("AdminId");

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>登录IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>记录</summary>
            public static readonly Field Actions = FindByName("Actions");

            /// <summary>返现、扣除金额</summary>
            public static readonly Field Reward = FindByName("Reward");

            /// <summary>变化前</summary>
            public static readonly Field BeforChange = FindByName("BeforChange");

            /// <summary>变化后</summary>
            public static readonly Field AfterChange = FindByName("AfterChange");

            /// <summary>详细记录</summary>
            public static readonly Field LogDetails = FindByName("LogDetails");

            /// <summary>类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成</summary>
            public static readonly Field TypeId = FindByName("TypeId");

            /// <summary>消费类型，见MyType</summary>
            public static readonly Field MyType = FindByName("MyType");

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName("OrderId");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户返现余额变化记录字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>管理员ID</summary>
            public const String AdminId = "AdminId";

            /// <summary>用户名</summary>
            public const String UserName = "UserName";

            /// <summary>时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>登录IP</summary>
            public const String Ip = "Ip";

            /// <summary>记录</summary>
            public const String Actions = "Actions";

            /// <summary>返现、扣除金额</summary>
            public const String Reward = "Reward";

            /// <summary>变化前</summary>
            public const String BeforChange = "BeforChange";

            /// <summary>变化后</summary>
            public const String AfterChange = "AfterChange";

            /// <summary>详细记录</summary>
            public const String LogDetails = "LogDetails";

            /// <summary>类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成</summary>
            public const String TypeId = "TypeId";

            /// <summary>消费类型，见MyType</summary>
            public const String MyType = "MyType";

            /// <summary>订单ID</summary>
            public const String OrderId = "OrderId";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";
        }
        #endregion
    }
}