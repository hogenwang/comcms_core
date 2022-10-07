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
    /// <summary>购物车</summary>
    [Serializable]
    [DataObject]
    [Description("购物车")]
    [BindTable("ShoppingCart", Description = "购物车", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class ShoppingCart
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

        private String _Guid;
        /// <summary>唯一GUID，没登录用户使用</summary>
        [DisplayName("唯一GUID")]
        [Description("唯一GUID，没登录用户使用")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Guid", "唯一GUID，没登录用户使用", "")]
        public String Guid { get => _Guid; set { if (OnPropertyChanging("Guid", value)) { _Guid = value; OnPropertyChanged("Guid"); } } }

        private String _Details;
        /// <summary>购物车内容，JOSN</summary>
        [DisplayName("购物车内容")]
        [Description("购物车内容，JOSN")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Details", "购物车内容，JOSN", "")]
        public String Details { get => _Details; set { if (OnPropertyChanging("Details", value)) { _Details = value; OnPropertyChanged("Details"); } } }

        private DateTime _AddTime;
        /// <summary>加入购物车时间</summary>
        [DisplayName("加入购物车时间")]
        [Description("加入购物车时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "加入购物车时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _UpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "最后更新时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }
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
                    case "Guid": return _Guid;
                    case "Details": return _Details;
                    case "AddTime": return _AddTime;
                    case "UpdateTime": return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "Guid": _Guid = Convert.ToString(value); break;
                    case "Details": _Details = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得购物车字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>唯一GUID，没登录用户使用</summary>
            public static readonly Field Guid = FindByName("Guid");

            /// <summary>购物车内容，JOSN</summary>
            public static readonly Field Details = FindByName("Details");

            /// <summary>加入购物车时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>最后更新时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得购物车字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>唯一GUID，没登录用户使用</summary>
            public const String Guid = "Guid";

            /// <summary>购物车内容，JOSN</summary>
            public const String Details = "Details";

            /// <summary>加入购物车时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>最后更新时间</summary>
            public const String UpdateTime = "UpdateTime";
        }
        #endregion
    }
}