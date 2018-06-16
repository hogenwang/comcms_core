using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>优惠券使用记录</summary>
    [Serializable]
    [DataObject]
    [Description("优惠券使用记录")]
    [BindTable("CouponUseLog", Description = "优惠券使用记录", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class CouponUseLog : ICouponUseLog
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

        private Int32 _CouponId;
        /// <summary>优惠券ID</summary>
        [DisplayName("优惠券ID")]
        [Description("优惠券ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponId", "优惠券ID", "int")]
        public Int32 CouponId { get { return _CouponId; } set { if (OnPropertyChanging(__.CouponId, value)) { _CouponId = value; OnPropertyChanged(__.CouponId); } } }

        private String _ItemNO;
        /// <summary>优惠券编号</summary>
        [DisplayName("优惠券编号")]
        [Description("优惠券编号")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ItemNO", "优惠券编号", "nvarchar(20)")]
        public String ItemNO { get { return _ItemNO; } set { if (OnPropertyChanging(__.ItemNO, value)) { _ItemNO = value; OnPropertyChanged(__.ItemNO); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "nvarchar(50)")]
        public String OrderNum { get { return _OrderNum; } set { if (OnPropertyChanging(__.OrderNum, value)) { _OrderNum = value; OnPropertyChanged(__.OrderNum); } } }

        private Int32 _OrderId;
        /// <summary>订单编号</summary>
        [DisplayName("订单编号")]
        [Description("订单编号")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单编号", "int")]
        public Int32 OrderId { get { return _OrderId; } set { if (OnPropertyChanging(__.OrderId, value)) { _OrderId = value; OnPropertyChanged(__.OrderId); } } }

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
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Title", "订单名称", "nvarchar(200)")]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private DateTime _AddTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private String _Ip;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "登录IP", "nvarchar(20)")]
        public String Ip { get { return _Ip; } set { if (OnPropertyChanging(__.Ip, value)) { _Ip = value; OnPropertyChanged(__.Ip); } } }

        private String _Actions;
        /// <summary>记录详情</summary>
        [DisplayName("记录详情")]
        [Description("记录详情")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Actions", "记录详情", "nvarchar(250)")]
        public String Actions { get { return _Actions; } set { if (OnPropertyChanging(__.Actions, value)) { _Actions = value; OnPropertyChanged(__.Actions); } } }
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
                    case __.CouponId : return _CouponId;
                    case __.ItemNO : return _ItemNO;
                    case __.OrderNum : return _OrderNum;
                    case __.OrderId : return _OrderId;
                    case __.UserName : return _UserName;
                    case __.Title : return _Title;
                    case __.AddTime : return _AddTime;
                    case __.Ip : return _Ip;
                    case __.Actions : return _Actions;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.CouponId : _CouponId = Convert.ToInt32(value); break;
                    case __.ItemNO : _ItemNO = Convert.ToString(value); break;
                    case __.OrderNum : _OrderNum = Convert.ToString(value); break;
                    case __.OrderId : _OrderId = Convert.ToInt32(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.Ip : _Ip = Convert.ToString(value); break;
                    case __.Actions : _Actions = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得优惠券使用记录字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>优惠券ID</summary>
            public static readonly Field CouponId = FindByName(__.CouponId);

            /// <summary>优惠券编号</summary>
            public static readonly Field ItemNO = FindByName(__.ItemNO);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>订单编号</summary>
            public static readonly Field OrderId = FindByName(__.OrderId);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>订单名称</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>登录IP</summary>
            public static readonly Field Ip = FindByName(__.Ip);

            /// <summary>记录详情</summary>
            public static readonly Field Actions = FindByName(__.Actions);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得优惠券使用记录字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>优惠券ID</summary>
            public const String CouponId = "CouponId";

            /// <summary>优惠券编号</summary>
            public const String ItemNO = "ItemNO";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>订单编号</summary>
            public const String OrderId = "OrderId";

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
        }
        #endregion
    }

    /// <summary>优惠券使用记录接口</summary>
    public partial interface ICouponUseLog
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>优惠券ID</summary>
        Int32 CouponId { get; set; }

        /// <summary>优惠券编号</summary>
        String ItemNO { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>订单编号</summary>
        Int32 OrderId { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>订单名称</summary>
        String Title { get; set; }

        /// <summary>时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>登录IP</summary>
        String Ip { get; set; }

        /// <summary>记录详情</summary>
        String Actions { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}