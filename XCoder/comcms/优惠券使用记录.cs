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
    /// <summary>优惠券使用记录</summary>
    [Serializable]
    [DataObject]
    [Description("优惠券使用记录")]
    [BindTable("CouponUseLog", Description = "优惠券使用记录", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class CouponUseLog
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

        private Int32 _CouponId;
        /// <summary>优惠券ID</summary>
        [DisplayName("优惠券ID")]
        [Description("优惠券ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponId", "优惠券ID", "")]
        public Int32 CouponId { get => _CouponId; set { if (OnPropertyChanging("CouponId", value)) { _CouponId = value; OnPropertyChanged("CouponId"); } } }

        private String _ItemNO;
        /// <summary>优惠券编号</summary>
        [DisplayName("优惠券编号")]
        [Description("优惠券编号")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ItemNO", "优惠券编号", "")]
        public String ItemNO { get => _ItemNO; set { if (OnPropertyChanging("ItemNO", value)) { _ItemNO = value; OnPropertyChanged("ItemNO"); } } }

        private String _OrderNum;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OrderNum", "订单号", "")]
        public String OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

        private Int32 _OrderId;
        /// <summary>订单编号</summary>
        [DisplayName("订单编号")]
        [Description("订单编号")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OrderId", "订单编号", "")]
        public Int32 OrderId { get => _OrderId; set { if (OnPropertyChanging("OrderId", value)) { _OrderId = value; OnPropertyChanged("OrderId"); } } }

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
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Title", "订单名称", "")]
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
        [BindColumn("Actions", "记录详情", "")]
        public String Actions { get => _Actions; set { if (OnPropertyChanging("Actions", value)) { _Actions = value; OnPropertyChanged("Actions"); } } }
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
                    case "CouponId": return _CouponId;
                    case "ItemNO": return _ItemNO;
                    case "OrderNum": return _OrderNum;
                    case "OrderId": return _OrderId;
                    case "UserName": return _UserName;
                    case "Title": return _Title;
                    case "AddTime": return _AddTime;
                    case "Ip": return _Ip;
                    case "Actions": return _Actions;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "CouponId": _CouponId = value.ToInt(); break;
                    case "ItemNO": _ItemNO = Convert.ToString(value); break;
                    case "OrderNum": _OrderNum = Convert.ToString(value); break;
                    case "OrderId": _OrderId = value.ToInt(); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "Actions": _Actions = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>优惠券ID</summary>
            public static readonly Field CouponId = FindByName("CouponId");

            /// <summary>优惠券编号</summary>
            public static readonly Field ItemNO = FindByName("ItemNO");

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName("OrderNum");

            /// <summary>订单编号</summary>
            public static readonly Field OrderId = FindByName("OrderId");

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

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}