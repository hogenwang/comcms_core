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
    /// <summary>优惠券分类</summary>
    [Serializable]
    [DataObject]
    [Description("优惠券分类")]
    [BindTable("CouponKind", Description = "优惠券分类", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class CouponKind
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _IsLimit;
        /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
        [DisplayName("是否有类别限制")]
        [Description("是否有类别限制，0 无限制；1 是类别限制，2是商品限制")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLimit", "是否有类别限制，0 无限制；1 是类别限制，2是商品限制", "")]
        public Int32 IsLimit { get => _IsLimit; set { if (OnPropertyChanging("IsLimit", value)) { _IsLimit = value; OnPropertyChanged("IsLimit"); } } }

        private String _KindName;
        /// <summary>类别名称</summary>
        [DisplayName("类别名称")]
        [Description("类别名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("KindName", "类别名称", "")]
        public String KindName { get => _KindName; set { if (OnPropertyChanging("KindName", value)) { _KindName = value; OnPropertyChanged("KindName"); } } }

        private Int32 _CouponType;
        /// <summary>优惠券类型，0 现金用券，1打折券</summary>
        [DisplayName("优惠券类型")]
        [Description("优惠券类型，0 现金用券，1打折券")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CouponType", "优惠券类型，0 现金用券，1打折券", "")]
        public Int32 CouponType { get => _CouponType; set { if (OnPropertyChanging("CouponType", value)) { _CouponType = value; OnPropertyChanged("CouponType"); } } }

        private String _KIds;
        /// <summary>类别按逗号分开</summary>
        [DisplayName("类别按逗号分开")]
        [Description("类别按逗号分开")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("KIds", "类别按逗号分开", "")]
        public String KIds { get => _KIds; set { if (OnPropertyChanging("KIds", value)) { _KIds = value; OnPropertyChanged("KIds"); } } }

        private String _PIds;
        /// <summary>商品ID，按逗号分开</summary>
        [DisplayName("商品ID")]
        [Description("商品ID，按逗号分开")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("PIds", "商品ID，按逗号分开", "")]
        public String PIds { get => _PIds; set { if (OnPropertyChanging("PIds", value)) { _PIds = value; OnPropertyChanged("PIds"); } } }

        private String _KindNote;
        /// <summary>类别说明</summary>
        [DisplayName("类别说明")]
        [Description("类别说明")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindNote", "类别说明", "")]
        public String KindNote { get => _KindNote; set { if (OnPropertyChanging("KindNote", value)) { _KindNote = value; OnPropertyChanged("KindNote"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private Int32 _MyType;
        /// <summary>可使用类型</summary>
        [DisplayName("可使用类型")]
        [Description("可使用类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "可使用类型", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }
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
                    case "IsLimit": return _IsLimit;
                    case "KindName": return _KindName;
                    case "CouponType": return _CouponType;
                    case "KIds": return _KIds;
                    case "PIds": return _PIds;
                    case "KindNote": return _KindNote;
                    case "AddTime": return _AddTime;
                    case "MyType": return _MyType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "IsLimit": _IsLimit = value.ToInt(); break;
                    case "KindName": _KindName = Convert.ToString(value); break;
                    case "CouponType": _CouponType = value.ToInt(); break;
                    case "KIds": _KIds = Convert.ToString(value); break;
                    case "PIds": _PIds = Convert.ToString(value); break;
                    case "KindNote": _KindNote = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得优惠券分类字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public static readonly Field IsLimit = FindByName("IsLimit");

            /// <summary>类别名称</summary>
            public static readonly Field KindName = FindByName("KindName");

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public static readonly Field CouponType = FindByName("CouponType");

            /// <summary>类别按逗号分开</summary>
            public static readonly Field KIds = FindByName("KIds");

            /// <summary>商品ID，按逗号分开</summary>
            public static readonly Field PIds = FindByName("PIds");

            /// <summary>类别说明</summary>
            public static readonly Field KindNote = FindByName("KindNote");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>可使用类型</summary>
            public static readonly Field MyType = FindByName("MyType");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得优惠券分类字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>是否有类别限制，0 无限制；1 是类别限制，2是商品限制</summary>
            public const String IsLimit = "IsLimit";

            /// <summary>类别名称</summary>
            public const String KindName = "KindName";

            /// <summary>优惠券类型，0 现金用券，1打折券</summary>
            public const String CouponType = "CouponType";

            /// <summary>类别按逗号分开</summary>
            public const String KIds = "KIds";

            /// <summary>商品ID，按逗号分开</summary>
            public const String PIds = "PIds";

            /// <summary>类别说明</summary>
            public const String KindNote = "KindNote";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>可使用类型</summary>
            public const String MyType = "MyType";
        }
        #endregion
    }
}