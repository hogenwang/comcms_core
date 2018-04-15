using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class WithdrawOrder : IWithdrawOrder
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int", Master = true)]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "nvarchar(50)")]
        public String OrderNum { get { return _OrderNum; } set { if (OnPropertyChanging(__.OrderNum, value)) { _OrderNum = value; OnPropertyChanged(__.OrderNum); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("UserName", "用户名", "nvarchar(200)")]
        public String UserName { get { return _UserName; } set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } } }

        private String _Title;
        /// <summary>订单名称</summary>
        [DisplayName("订单名称")]
        [Description("订单名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Title", "订单名称", "nvarchar(100)", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private DateTime _AddTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private String _IP;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("IP", "登录IP", "nvarchar(20)")]
        public String IP { get { return _IP; } set { if (OnPropertyChanging(__.IP, value)) { _IP = value; OnPropertyChanged(__.IP); } } }

        private String _Actions;
        /// <summary>记录详情</summary>
        [DisplayName("记录详情")]
        [Description("记录详情")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Actions", "记录详情", "nvarchar(250)", Master = true)]
        public String Actions { get { return _Actions; } set { if (OnPropertyChanging(__.Actions, value)) { _Actions = value; OnPropertyChanged(__.Actions); } } }

        private Decimal _Price;
        /// <summary>提现金额</summary>
        [DisplayName("提现金额")]
        [Description("提现金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "提现金额", "money")]
        public Decimal Price { get { return _Price; } set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } } }

        private Int32 _VerifyAdminId;
        /// <summary>审核管理员ID</summary>
        [DisplayName("审核管理员ID")]
        [Description("审核管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("VerifyAdminId", "审核管理员ID", "int")]
        public Int32 VerifyAdminId { get { return _VerifyAdminId; } set { if (OnPropertyChanging(__.VerifyAdminId, value)) { _VerifyAdminId = value; OnPropertyChanged(__.VerifyAdminId); } } }

        private Int32 _IsVerify;
        /// <summary>是否通过审核</summary>
        [DisplayName("是否通过审核")]
        [Description("是否通过审核")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否通过审核", "int")]
        public Int32 IsVerify { get { return _IsVerify; } set { if (OnPropertyChanging(__.IsVerify, value)) { _IsVerify = value; OnPropertyChanged(__.IsVerify); } } }

        private DateTime _VerifyTime;
        /// <summary>审核时间</summary>
        [DisplayName("审核时间")]
        [Description("审核时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("VerifyTime", "审核时间", "datetime")]
        public DateTime VerifyTime { get { return _VerifyTime; } set { if (OnPropertyChanging(__.VerifyTime, value)) { _VerifyTime = value; OnPropertyChanged(__.VerifyTime); } } }

        private String _FormId;
        /// <summary>小程序FormId</summary>
        [DisplayName("小程序FormId")]
        [Description("小程序FormId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FormId", "小程序FormId", "nvarchar(50)")]
        public String FormId { get { return _FormId; } set { if (OnPropertyChanging(__.FormId, value)) { _FormId = value; OnPropertyChanged(__.FormId); } } }
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
                    case __.UId : return _UId;
                    case __.OrderNum : return _OrderNum;
                    case __.UserName : return _UserName;
                    case __.Title : return _Title;
                    case __.AddTime : return _AddTime;
                    case __.IP : return _IP;
                    case __.Actions : return _Actions;
                    case __.Price : return _Price;
                    case __.VerifyAdminId : return _VerifyAdminId;
                    case __.IsVerify : return _IsVerify;
                    case __.VerifyTime : return _VerifyTime;
                    case __.FormId : return _FormId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.OrderNum : _OrderNum = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.IP : _IP = Convert.ToString(value); break;
                    case __.Actions : _Actions = Convert.ToString(value); break;
                    case __.Price : _Price = Convert.ToDecimal(value); break;
                    case __.VerifyAdminId : _VerifyAdminId = Convert.ToInt32(value); break;
                    case __.IsVerify : _IsVerify = Convert.ToInt32(value); break;
                    case __.VerifyTime : _VerifyTime = Convert.ToDateTime(value); break;
                    case __.FormId : _FormId = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>订单名称</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>登录IP</summary>
            public static readonly Field IP = FindByName(__.IP);

            /// <summary>记录详情</summary>
            public static readonly Field Actions = FindByName(__.Actions);

            /// <summary>提现金额</summary>
            public static readonly Field Price = FindByName(__.Price);

            /// <summary>审核管理员ID</summary>
            public static readonly Field VerifyAdminId = FindByName(__.VerifyAdminId);

            /// <summary>是否通过审核</summary>
            public static readonly Field IsVerify = FindByName(__.IsVerify);

            /// <summary>审核时间</summary>
            public static readonly Field VerifyTime = FindByName(__.VerifyTime);

            /// <summary>小程序FormId</summary>
            public static readonly Field FormId = FindByName(__.FormId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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
            public const String IP = "IP";

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

    /// <summary>用户提现订单接口</summary>
    public partial interface IWithdrawOrder
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>订单名称</summary>
        String Title { get; set; }

        /// <summary>时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>登录IP</summary>
        String IP { get; set; }

        /// <summary>记录详情</summary>
        String Actions { get; set; }

        /// <summary>提现金额</summary>
        Decimal Price { get; set; }

        /// <summary>审核管理员ID</summary>
        Int32 VerifyAdminId { get; set; }

        /// <summary>是否通过审核</summary>
        Int32 IsVerify { get; set; }

        /// <summary>审核时间</summary>
        DateTime VerifyTime { get; set; }

        /// <summary>小程序FormId</summary>
        String FormId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}