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
    /// <summary>用户角色</summary>
    [Serializable]
    [DataObject]
    [Description("用户角色")]
    [BindTable("MemberRoles", Description = "用户角色", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class MemberRoles
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _RoleName;
        /// <summary>角色名称</summary>
        [DisplayName("角色名称")]
        [Description("角色名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RoleName", "角色名称", "", Master = true)]
        public String RoleName { get => _RoleName; set { if (OnPropertyChanging("RoleName", value)) { _RoleName = value; OnPropertyChanged("RoleName"); } } }

        private String _RoleDescription;
        /// <summary>角色简单介绍</summary>
        [DisplayName("角色简单介绍")]
        [Description("角色简单介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("RoleDescription", "角色简单介绍", "", Master = true)]
        public String RoleDescription { get => _RoleDescription; set { if (OnPropertyChanging("RoleDescription", value)) { _RoleDescription = value; OnPropertyChanged("RoleDescription"); } } }

        private Int32 _Stars;
        /// <summary>星级</summary>
        [DisplayName("星级")]
        [Description("星级")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stars", "星级", "")]
        public Int32 Stars { get => _Stars; set { if (OnPropertyChanging("Stars", value)) { _Stars = value; OnPropertyChanged("Stars"); } } }

        private Int32 _NotAllowDel;
        /// <summary>是否不允许删除</summary>
        [DisplayName("是否不允许删除")]
        [Description("是否不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NotAllowDel", "是否不允许删除", "")]
        public Int32 NotAllowDel { get => _NotAllowDel; set { if (OnPropertyChanging("NotAllowDel", value)) { _NotAllowDel = value; OnPropertyChanged("NotAllowDel"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Color", "颜色", "", Master = true)]
        public String Color { get => _Color; set { if (OnPropertyChanging("Color", value)) { _Color = value; OnPropertyChanged("Color"); } } }

        private Decimal _CashBack;
        /// <summary>返现百分比</summary>
        [DisplayName("返现百分比")]
        [Description("返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CashBack", "返现百分比", "")]
        public Decimal CashBack { get => _CashBack; set { if (OnPropertyChanging("CashBack", value)) { _CashBack = value; OnPropertyChanged("CashBack"); } } }

        private Decimal _ParentCashBack;
        /// <summary>父级返现百分比</summary>
        [DisplayName("父级返现百分比")]
        [Description("父级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ParentCashBack", "父级返现百分比", "")]
        public Decimal ParentCashBack { get => _ParentCashBack; set { if (OnPropertyChanging("ParentCashBack", value)) { _ParentCashBack = value; OnPropertyChanged("ParentCashBack"); } } }

        private Decimal _GrandfatherCashBack;
        /// <summary>爷级返现百分比</summary>
        [DisplayName("爷级返现百分比")]
        [Description("爷级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GrandfatherCashBack", "爷级返现百分比", "")]
        public Decimal GrandfatherCashBack { get => _GrandfatherCashBack; set { if (OnPropertyChanging("GrandfatherCashBack", value)) { _GrandfatherCashBack = value; OnPropertyChanged("GrandfatherCashBack"); } } }

        private Int32 _IsDefault;
        /// <summary>是否是默认角色</summary>
        [DisplayName("是否是默认角色")]
        [Description("是否是默认角色")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否是默认角色", "")]
        public Int32 IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

        private Int32 _IsHalved;
        /// <summary>超过级别是否减半</summary>
        [DisplayName("超过级别是否减半")]
        [Description("超过级别是否减半")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHalved", "超过级别是否减半", "")]
        public Int32 IsHalved { get => _IsHalved; set { if (OnPropertyChanging("IsHalved", value)) { _IsHalved = value; OnPropertyChanged("IsHalved"); } } }

        private Decimal _HalvedParentCashBack;
        /// <summary>超过级别父级返现百分比</summary>
        [DisplayName("超过级别父级返现百分比")]
        [Description("超过级别父级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HalvedParentCashBack", "超过级别父级返现百分比", "")]
        public Decimal HalvedParentCashBack { get => _HalvedParentCashBack; set { if (OnPropertyChanging("HalvedParentCashBack", value)) { _HalvedParentCashBack = value; OnPropertyChanged("HalvedParentCashBack"); } } }

        private Decimal _HalvedGrandfatherCashBack;
        /// <summary>超过级别爷级返现百分比</summary>
        [DisplayName("超过级别爷级返现百分比")]
        [Description("超过级别爷级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HalvedGrandfatherCashBack", "超过级别爷级返现百分比", "")]
        public Decimal HalvedGrandfatherCashBack { get => _HalvedGrandfatherCashBack; set { if (OnPropertyChanging("HalvedGrandfatherCashBack", value)) { _HalvedGrandfatherCashBack = value; OnPropertyChanged("HalvedGrandfatherCashBack"); } } }

        private Decimal _YearsPerformance;
        /// <summary>年业务量</summary>
        [DisplayName("年业务量")]
        [Description("年业务量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("YearsPerformance", "年业务量", "")]
        public Decimal YearsPerformance { get => _YearsPerformance; set { if (OnPropertyChanging("YearsPerformance", value)) { _YearsPerformance = value; OnPropertyChanged("YearsPerformance"); } } }

        private Int32 _IsSellers;
        /// <summary>是否是分销商</summary>
        [DisplayName("是否是分销商")]
        [Description("是否是分销商")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSellers", "是否是分销商", "")]
        public Int32 IsSellers { get => _IsSellers; set { if (OnPropertyChanging("IsSellers", value)) { _IsSellers = value; OnPropertyChanged("IsSellers"); } } }

        private Decimal _JoinPrice;
        /// <summary>加入分销商价格</summary>
        [DisplayName("加入分销商价格")]
        [Description("加入分销商价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("JoinPrice", "加入分销商价格", "")]
        public Decimal JoinPrice { get => _JoinPrice; set { if (OnPropertyChanging("JoinPrice", value)) { _JoinPrice = value; OnPropertyChanged("JoinPrice"); } } }
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
                    case "RoleName": return _RoleName;
                    case "RoleDescription": return _RoleDescription;
                    case "Stars": return _Stars;
                    case "NotAllowDel": return _NotAllowDel;
                    case "Rank": return _Rank;
                    case "Color": return _Color;
                    case "CashBack": return _CashBack;
                    case "ParentCashBack": return _ParentCashBack;
                    case "GrandfatherCashBack": return _GrandfatherCashBack;
                    case "IsDefault": return _IsDefault;
                    case "IsHalved": return _IsHalved;
                    case "HalvedParentCashBack": return _HalvedParentCashBack;
                    case "HalvedGrandfatherCashBack": return _HalvedGrandfatherCashBack;
                    case "YearsPerformance": return _YearsPerformance;
                    case "IsSellers": return _IsSellers;
                    case "JoinPrice": return _JoinPrice;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "RoleName": _RoleName = Convert.ToString(value); break;
                    case "RoleDescription": _RoleDescription = Convert.ToString(value); break;
                    case "Stars": _Stars = value.ToInt(); break;
                    case "NotAllowDel": _NotAllowDel = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "Color": _Color = Convert.ToString(value); break;
                    case "CashBack": _CashBack = Convert.ToDecimal(value); break;
                    case "ParentCashBack": _ParentCashBack = Convert.ToDecimal(value); break;
                    case "GrandfatherCashBack": _GrandfatherCashBack = Convert.ToDecimal(value); break;
                    case "IsDefault": _IsDefault = value.ToInt(); break;
                    case "IsHalved": _IsHalved = value.ToInt(); break;
                    case "HalvedParentCashBack": _HalvedParentCashBack = Convert.ToDecimal(value); break;
                    case "HalvedGrandfatherCashBack": _HalvedGrandfatherCashBack = Convert.ToDecimal(value); break;
                    case "YearsPerformance": _YearsPerformance = Convert.ToDecimal(value); break;
                    case "IsSellers": _IsSellers = value.ToInt(); break;
                    case "JoinPrice": _JoinPrice = Convert.ToDecimal(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户角色字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>角色名称</summary>
            public static readonly Field RoleName = FindByName("RoleName");

            /// <summary>角色简单介绍</summary>
            public static readonly Field RoleDescription = FindByName("RoleDescription");

            /// <summary>星级</summary>
            public static readonly Field Stars = FindByName("Stars");

            /// <summary>是否不允许删除</summary>
            public static readonly Field NotAllowDel = FindByName("NotAllowDel");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName("Color");

            /// <summary>返现百分比</summary>
            public static readonly Field CashBack = FindByName("CashBack");

            /// <summary>父级返现百分比</summary>
            public static readonly Field ParentCashBack = FindByName("ParentCashBack");

            /// <summary>爷级返现百分比</summary>
            public static readonly Field GrandfatherCashBack = FindByName("GrandfatherCashBack");

            /// <summary>是否是默认角色</summary>
            public static readonly Field IsDefault = FindByName("IsDefault");

            /// <summary>超过级别是否减半</summary>
            public static readonly Field IsHalved = FindByName("IsHalved");

            /// <summary>超过级别父级返现百分比</summary>
            public static readonly Field HalvedParentCashBack = FindByName("HalvedParentCashBack");

            /// <summary>超过级别爷级返现百分比</summary>
            public static readonly Field HalvedGrandfatherCashBack = FindByName("HalvedGrandfatherCashBack");

            /// <summary>年业务量</summary>
            public static readonly Field YearsPerformance = FindByName("YearsPerformance");

            /// <summary>是否是分销商</summary>
            public static readonly Field IsSellers = FindByName("IsSellers");

            /// <summary>加入分销商价格</summary>
            public static readonly Field JoinPrice = FindByName("JoinPrice");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户角色字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>角色名称</summary>
            public const String RoleName = "RoleName";

            /// <summary>角色简单介绍</summary>
            public const String RoleDescription = "RoleDescription";

            /// <summary>星级</summary>
            public const String Stars = "Stars";

            /// <summary>是否不允许删除</summary>
            public const String NotAllowDel = "NotAllowDel";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>颜色</summary>
            public const String Color = "Color";

            /// <summary>返现百分比</summary>
            public const String CashBack = "CashBack";

            /// <summary>父级返现百分比</summary>
            public const String ParentCashBack = "ParentCashBack";

            /// <summary>爷级返现百分比</summary>
            public const String GrandfatherCashBack = "GrandfatherCashBack";

            /// <summary>是否是默认角色</summary>
            public const String IsDefault = "IsDefault";

            /// <summary>超过级别是否减半</summary>
            public const String IsHalved = "IsHalved";

            /// <summary>超过级别父级返现百分比</summary>
            public const String HalvedParentCashBack = "HalvedParentCashBack";

            /// <summary>超过级别爷级返现百分比</summary>
            public const String HalvedGrandfatherCashBack = "HalvedGrandfatherCashBack";

            /// <summary>年业务量</summary>
            public const String YearsPerformance = "YearsPerformance";

            /// <summary>是否是分销商</summary>
            public const String IsSellers = "IsSellers";

            /// <summary>加入分销商价格</summary>
            public const String JoinPrice = "JoinPrice";
        }
        #endregion
    }
}