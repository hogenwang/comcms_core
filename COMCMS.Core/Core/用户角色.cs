using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MemberRoles : IMemberRoles
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _RoleName;
        /// <summary>角色名称</summary>
        [DisplayName("角色名称")]
        [Description("角色名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RoleName", "角色名称", "nvarchar(50)", Master = true)]
        public String RoleName { get { return _RoleName; } set { if (OnPropertyChanging(__.RoleName, value)) { _RoleName = value; OnPropertyChanged(__.RoleName); } } }

        private String _RoleDescription;
        /// <summary>角色简单介绍</summary>
        [DisplayName("角色简单介绍")]
        [Description("角色简单介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("RoleDescription", "角色简单介绍", "nvarchar(250)", Master = true)]
        public String RoleDescription { get { return _RoleDescription; } set { if (OnPropertyChanging(__.RoleDescription, value)) { _RoleDescription = value; OnPropertyChanged(__.RoleDescription); } } }

        private Int32 _Stars;
        /// <summary>星级</summary>
        [DisplayName("星级")]
        [Description("星级")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stars", "星级", "int")]
        public Int32 Stars { get { return _Stars; } set { if (OnPropertyChanging(__.Stars, value)) { _Stars = value; OnPropertyChanged(__.Stars); } } }

        private Int32 _NotAllowDel;
        /// <summary>是否不允许删除</summary>
        [DisplayName("是否不允许删除")]
        [Description("是否不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NotAllowDel", "是否不允许删除", "int")]
        public Int32 NotAllowDel { get { return _NotAllowDel; } set { if (OnPropertyChanging(__.NotAllowDel, value)) { _NotAllowDel = value; OnPropertyChanged(__.NotAllowDel); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "int")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Color", "颜色", "nvarchar(20)", Master = true)]
        public String Color { get { return _Color; } set { if (OnPropertyChanging(__.Color, value)) { _Color = value; OnPropertyChanged(__.Color); } } }

        private Decimal _CashBack;
        /// <summary>返现百分比</summary>
        [DisplayName("返现百分比")]
        [Description("返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CashBack", "返现百分比", "money")]
        public Decimal CashBack { get { return _CashBack; } set { if (OnPropertyChanging(__.CashBack, value)) { _CashBack = value; OnPropertyChanged(__.CashBack); } } }

        private Decimal _ParentCashBack;
        /// <summary>父级返现百分比</summary>
        [DisplayName("父级返现百分比")]
        [Description("父级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ParentCashBack", "父级返现百分比", "money")]
        public Decimal ParentCashBack { get { return _ParentCashBack; } set { if (OnPropertyChanging(__.ParentCashBack, value)) { _ParentCashBack = value; OnPropertyChanged(__.ParentCashBack); } } }

        private Decimal _GrandfatherCashBack;
        /// <summary>爷级返现百分比</summary>
        [DisplayName("爷级返现百分比")]
        [Description("爷级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GrandfatherCashBack", "爷级返现百分比", "money")]
        public Decimal GrandfatherCashBack { get { return _GrandfatherCashBack; } set { if (OnPropertyChanging(__.GrandfatherCashBack, value)) { _GrandfatherCashBack = value; OnPropertyChanged(__.GrandfatherCashBack); } } }

        private Int32 _IsDefault;
        /// <summary>是否是默认角色</summary>
        [DisplayName("是否是默认角色")]
        [Description("是否是默认角色")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否是默认角色", "int")]
        public Int32 IsDefault { get { return _IsDefault; } set { if (OnPropertyChanging(__.IsDefault, value)) { _IsDefault = value; OnPropertyChanged(__.IsDefault); } } }

        private Int32 _IsHalved;
        /// <summary>超过级别是否减半</summary>
        [DisplayName("超过级别是否减半")]
        [Description("超过级别是否减半")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHalved", "超过级别是否减半", "int")]
        public Int32 IsHalved { get { return _IsHalved; } set { if (OnPropertyChanging(__.IsHalved, value)) { _IsHalved = value; OnPropertyChanged(__.IsHalved); } } }

        private Decimal _HalvedParentCashBack;
        /// <summary>超过级别父级返现百分比</summary>
        [DisplayName("超过级别父级返现百分比")]
        [Description("超过级别父级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HalvedParentCashBack", "超过级别父级返现百分比", "money")]
        public Decimal HalvedParentCashBack { get { return _HalvedParentCashBack; } set { if (OnPropertyChanging(__.HalvedParentCashBack, value)) { _HalvedParentCashBack = value; OnPropertyChanged(__.HalvedParentCashBack); } } }

        private Decimal _HalvedGrandfatherCashBack;
        /// <summary>超过级别爷级返现百分比</summary>
        [DisplayName("超过级别爷级返现百分比")]
        [Description("超过级别爷级返现百分比")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HalvedGrandfatherCashBack", "超过级别爷级返现百分比", "money")]
        public Decimal HalvedGrandfatherCashBack { get { return _HalvedGrandfatherCashBack; } set { if (OnPropertyChanging(__.HalvedGrandfatherCashBack, value)) { _HalvedGrandfatherCashBack = value; OnPropertyChanged(__.HalvedGrandfatherCashBack); } } }

        private Decimal _YearsPerformance;
        /// <summary>年业务量</summary>
        [DisplayName("年业务量")]
        [Description("年业务量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("YearsPerformance", "年业务量", "money")]
        public Decimal YearsPerformance { get { return _YearsPerformance; } set { if (OnPropertyChanging(__.YearsPerformance, value)) { _YearsPerformance = value; OnPropertyChanged(__.YearsPerformance); } } }

        private Int32 _IsSellers;
        /// <summary>是否是分销商</summary>
        [DisplayName("是否是分销商")]
        [Description("是否是分销商")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSellers", "是否是分销商", "int")]
        public Int32 IsSellers { get { return _IsSellers; } set { if (OnPropertyChanging(__.IsSellers, value)) { _IsSellers = value; OnPropertyChanged(__.IsSellers); } } }

        private Decimal _JoinPrice;
        /// <summary>加入分销商价格</summary>
        [DisplayName("加入分销商价格")]
        [Description("加入分销商价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("JoinPrice", "加入分销商价格", "money")]
        public Decimal JoinPrice { get { return _JoinPrice; } set { if (OnPropertyChanging(__.JoinPrice, value)) { _JoinPrice = value; OnPropertyChanged(__.JoinPrice); } } }
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
                    case __.RoleName : return _RoleName;
                    case __.RoleDescription : return _RoleDescription;
                    case __.Stars : return _Stars;
                    case __.NotAllowDel : return _NotAllowDel;
                    case __.Rank : return _Rank;
                    case __.Color : return _Color;
                    case __.CashBack : return _CashBack;
                    case __.ParentCashBack : return _ParentCashBack;
                    case __.GrandfatherCashBack : return _GrandfatherCashBack;
                    case __.IsDefault : return _IsDefault;
                    case __.IsHalved : return _IsHalved;
                    case __.HalvedParentCashBack : return _HalvedParentCashBack;
                    case __.HalvedGrandfatherCashBack : return _HalvedGrandfatherCashBack;
                    case __.YearsPerformance : return _YearsPerformance;
                    case __.IsSellers : return _IsSellers;
                    case __.JoinPrice : return _JoinPrice;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.RoleName : _RoleName = Convert.ToString(value); break;
                    case __.RoleDescription : _RoleDescription = Convert.ToString(value); break;
                    case __.Stars : _Stars = Convert.ToInt32(value); break;
                    case __.NotAllowDel : _NotAllowDel = Convert.ToInt32(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
                    case __.Color : _Color = Convert.ToString(value); break;
                    case __.CashBack : _CashBack = Convert.ToDecimal(value); break;
                    case __.ParentCashBack : _ParentCashBack = Convert.ToDecimal(value); break;
                    case __.GrandfatherCashBack : _GrandfatherCashBack = Convert.ToDecimal(value); break;
                    case __.IsDefault : _IsDefault = Convert.ToInt32(value); break;
                    case __.IsHalved : _IsHalved = Convert.ToInt32(value); break;
                    case __.HalvedParentCashBack : _HalvedParentCashBack = Convert.ToDecimal(value); break;
                    case __.HalvedGrandfatherCashBack : _HalvedGrandfatherCashBack = Convert.ToDecimal(value); break;
                    case __.YearsPerformance : _YearsPerformance = Convert.ToDecimal(value); break;
                    case __.IsSellers : _IsSellers = Convert.ToInt32(value); break;
                    case __.JoinPrice : _JoinPrice = Convert.ToDecimal(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>角色名称</summary>
            public static readonly Field RoleName = FindByName(__.RoleName);

            /// <summary>角色简单介绍</summary>
            public static readonly Field RoleDescription = FindByName(__.RoleDescription);

            /// <summary>星级</summary>
            public static readonly Field Stars = FindByName(__.Stars);

            /// <summary>是否不允许删除</summary>
            public static readonly Field NotAllowDel = FindByName(__.NotAllowDel);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName(__.Color);

            /// <summary>返现百分比</summary>
            public static readonly Field CashBack = FindByName(__.CashBack);

            /// <summary>父级返现百分比</summary>
            public static readonly Field ParentCashBack = FindByName(__.ParentCashBack);

            /// <summary>爷级返现百分比</summary>
            public static readonly Field GrandfatherCashBack = FindByName(__.GrandfatherCashBack);

            /// <summary>是否是默认角色</summary>
            public static readonly Field IsDefault = FindByName(__.IsDefault);

            /// <summary>超过级别是否减半</summary>
            public static readonly Field IsHalved = FindByName(__.IsHalved);

            /// <summary>超过级别父级返现百分比</summary>
            public static readonly Field HalvedParentCashBack = FindByName(__.HalvedParentCashBack);

            /// <summary>超过级别爷级返现百分比</summary>
            public static readonly Field HalvedGrandfatherCashBack = FindByName(__.HalvedGrandfatherCashBack);

            /// <summary>年业务量</summary>
            public static readonly Field YearsPerformance = FindByName(__.YearsPerformance);

            /// <summary>是否是分销商</summary>
            public static readonly Field IsSellers = FindByName(__.IsSellers);

            /// <summary>加入分销商价格</summary>
            public static readonly Field JoinPrice = FindByName(__.JoinPrice);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>用户角色接口</summary>
    public partial interface IMemberRoles
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>角色名称</summary>
        String RoleName { get; set; }

        /// <summary>角色简单介绍</summary>
        String RoleDescription { get; set; }

        /// <summary>星级</summary>
        Int32 Stars { get; set; }

        /// <summary>是否不允许删除</summary>
        Int32 NotAllowDel { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>颜色</summary>
        String Color { get; set; }

        /// <summary>返现百分比</summary>
        Decimal CashBack { get; set; }

        /// <summary>父级返现百分比</summary>
        Decimal ParentCashBack { get; set; }

        /// <summary>爷级返现百分比</summary>
        Decimal GrandfatherCashBack { get; set; }

        /// <summary>是否是默认角色</summary>
        Int32 IsDefault { get; set; }

        /// <summary>超过级别是否减半</summary>
        Int32 IsHalved { get; set; }

        /// <summary>超过级别父级返现百分比</summary>
        Decimal HalvedParentCashBack { get; set; }

        /// <summary>超过级别爷级返现百分比</summary>
        Decimal HalvedGrandfatherCashBack { get; set; }

        /// <summary>年业务量</summary>
        Decimal YearsPerformance { get; set; }

        /// <summary>是否是分销商</summary>
        Int32 IsSellers { get; set; }

        /// <summary>加入分销商价格</summary>
        Decimal JoinPrice { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}