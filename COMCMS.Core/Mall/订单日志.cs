using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>订单日志</summary>
    [Serializable]
    [DataObject]
    [Description("订单日志")]
    [BindTable("OrderLog", Description = "订单日志", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class OrderLog : IOrderLog
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

        private String _Actions;
        /// <summary>日志记录</summary>
        [DisplayName("日志记录")]
        [Description("日志记录")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Actions", "日志记录", "nvarchar(200)")]
        public String Actions { get { return _Actions; } set { if (OnPropertyChanging(__.Actions, value)) { _Actions = value; OnPropertyChanged(__.Actions); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }
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
                    case __.Actions : return _Actions;
                    case __.UId : return _UId;
                    case __.AddTime : return _AddTime;
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
                    case __.Actions : _Actions = Convert.ToString(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得订单日志字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>订单ID</summary>
            public static readonly Field OrderId = FindByName(__.OrderId);

            /// <summary>订单号</summary>
            public static readonly Field OrderNum = FindByName(__.OrderNum);

            /// <summary>日志记录</summary>
            public static readonly Field Actions = FindByName(__.Actions);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得订单日志字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>订单ID</summary>
            public const String OrderId = "OrderId";

            /// <summary>订单号</summary>
            public const String OrderNum = "OrderNum";

            /// <summary>日志记录</summary>
            public const String Actions = "Actions";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }

    /// <summary>订单日志接口</summary>
    public partial interface IOrderLog
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>订单ID</summary>
        Int32 OrderId { get; set; }

        /// <summary>订单号</summary>
        String OrderNum { get; set; }

        /// <summary>日志记录</summary>
        String Actions { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}