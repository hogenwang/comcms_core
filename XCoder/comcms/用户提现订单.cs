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
    /// <summary>用户提现订单</summary>
    [Serializable]
    [DataObject]
    [Description("用户提现订单")]
    [BindTable("WithdrawOrder", Description = "用户提现订单", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WithdrawOrder
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

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("UserName", "用户名", "")]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

        private String _Title;
        /// <summary>订单名称</summary>
        [DisplayName("订单名称")]
        [Description("订单名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Title", "订单名称", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

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
        /// <summary>记录详情</summary>
        [DisplayName("记录详情")]
        [Description("记录详情")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Actions", "记录详情", "", Master = true)]
        public String Actions { get => _Actions; set { if (OnPropertyChanging("Actions", value)) { _Actions = value; OnPropertyChanged("Actions"); } } }

        private Decimal _Price;
        /// <summary>提现金额</summary>
        [DisplayName("提现金额")]
        [Description("提现金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "提现金额", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging("Price", value)) { _Price = value; OnPropertyChanged("Price"); } } }

        private Int32 _VerifyAdminId;
        /// <summary>审核管理员ID</summary>
        [DisplayName("审核管理员ID")]
        [Description("审核管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("VerifyAdminId", "审核管理员ID", "")]
        public Int32 VerifyAdminId { get => _VerifyAdminId; set { if (OnPropertyChanging("VerifyAdminId", value)) { _VerifyAdminId = value; OnPropertyChanged("VerifyAdminId"); } } }

        private Int32 _IsVerify;
        /// <summary>是否通过审核</summary>
        [DisplayName("是否通过审核")]
        [Description("是否通过审核")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否通过审核", "")]
        public Int32 IsVerify { get => _IsVerify; set { if (OnPropertyChanging("IsVerify", value)) { _IsVerify = value; OnPropertyChanged("IsVerify"); } } }

        private DateTime _VerifyTime;
        /// <summary>审核时间</summary>
        [DisplayName("审核时间")]
        [Description("审核时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("VerifyTime", "审核时间", "")]
        public DateTime VerifyTime { get => _VerifyTime; set { if (OnPropertyChanging("VerifyTime", value)) { _VerifyTime = value; OnPropertyChanged("VerifyTime"); } } }

        private String _FormId;
        /// <summary>小程序FormId</summary>
        [DisplayName("小程序FormId")]
        [Description("小程序FormId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FormId", "小程序FormId", "")]
        public String FormId { get => _FormId; set { if (OnPropertyChanging("FormId", value)) { _FormId = value; OnPropertyChanged("FormId"); } } }
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
                    case "OrderNum": return _OrderNum;
                    case "UserName": return _UserName;
                    case "Title": return _Title;
                    case "AddTime": return _AddTime;
                    case "Ip": return _Ip;
                    case "Actions": return _Actions;
                    case "Price": return _Price;
                    case "VerifyAdminId": return _VerifyAdminId;
                    case "IsVerify": return _IsVerify;
                    case "VerifyTime": return _VerifyTime;
                    case "FormId": return _FormId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "Actions": _Actions = Convert.ToString(value); break;
                    case "Price": _Price = Convert.ToDecimal(value); break;
                    case "VerifyAdminId": _VerifyAdminId = value.ToInt(); break;
                    case "IsVerify": _IsVerify = value.ToInt(); break;
                    case "VerifyTime": _VerifyTime = value.ToDateTime(); break;
                    case "FormId": _FormId = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户提现订单字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>订单名称</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>登录IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>记录详情</summary>
            public static readonly Field Actions = FindByName("Actions");

            /// <summary>提现金额</summary>
            public static readonly Field Price = FindByName("Price");

            /// <summary>审核管理员ID</summary>
            public static readonly Field VerifyAdminId = FindByName("VerifyAdminId");

            /// <summary>是否通过审核</summary>
            public static readonly Field IsVerify = FindByName("IsVerify");

            /// <summary>审核时间</summary>
            public static readonly Field VerifyTime = FindByName("VerifyTime");

            /// <summary>小程序FormId</summary>
            public static readonly Field FormId = FindByName("FormId");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户提现订单字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>用户名</summary>
            public const String UserName = "UserName";

            /// <summary>订单名称</summary>
            public const String Title = "Title";

            /// <summary>时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>登录IP</summary>
            public const String Ip = "Ip";

            /// <summary>记录详情</summary>
            public const String Actions = "Actions";

            /// <summary>提现金额</summary>
            public const String Price = "Price";

            /// <summary>审核管理员ID</summary>
            public const String VerifyAdminId = "VerifyAdminId";

            /// <summary>是否通过审核</summary>
            public const String IsVerify = "IsVerify";

            /// <summary>审核时间</summary>
            public const String VerifyTime = "VerifyTime";

            /// <summary>小程序FormId</summary>
            public const String FormId = "FormId";
        }
        #endregion
    }
}