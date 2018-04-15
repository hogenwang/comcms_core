using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>用户地址</summary>
    [Serializable]
    [DataObject]
    [Description("用户地址")]
    [BindTable("MemberAddress", Description = "用户地址", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class MemberAddress : IMemberAddress
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

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RealName", "姓名", "nvarchar(20)")]
        public String RealName { get { return _RealName; } set { if (OnPropertyChanging(__.RealName, value)) { _RealName = value; OnPropertyChanged(__.RealName); } } }

        private String _Tel;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "nvarchar(20)")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "nvarchar(100)")]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private Boolean _IsDefault;
        /// <summary>是否是默认</summary>
        [DisplayName("是否是默认")]
        [Description("是否是默认")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否是默认", "bit")]
        public Boolean IsDefault { get { return _IsDefault; } set { if (OnPropertyChanging(__.IsDefault, value)) { _IsDefault = value; OnPropertyChanged(__.IsDefault); } } }

        private String _Phone;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Phone", "电话", "nvarchar(20)")]
        public String Phone { get { return _Phone; } set { if (OnPropertyChanging(__.Phone, value)) { _Phone = value; OnPropertyChanged(__.Phone); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Company", "公司", "nvarchar(100)")]
        public String Company { get { return _Company; } set { if (OnPropertyChanging(__.Company, value)) { _Company = value; OnPropertyChanged(__.Company); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "nvarchar(50)")]
        public String Country { get { return _Country; } set { if (OnPropertyChanging(__.Country, value)) { _Country = value; OnPropertyChanged(__.Country); } } }

        private String _Province;
        /// <summary>省</summary>
        [DisplayName("省")]
        [Description("省")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省", "nvarchar(50)")]
        public String Province { get { return _Province; } set { if (OnPropertyChanging(__.Province, value)) { _Province = value; OnPropertyChanged(__.Province); } } }

        private String _City;
        /// <summary>市</summary>
        [DisplayName("市")]
        [Description("市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "市", "nvarchar(50)")]
        public String City { get { return _City; } set { if (OnPropertyChanging(__.City, value)) { _City = value; OnPropertyChanged(__.City); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "nvarchar(50)")]
        public String District { get { return _District; } set { if (OnPropertyChanging(__.District, value)) { _District = value; OnPropertyChanged(__.District); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "nvarchar(250)")]
        public String Address { get { return _Address; } set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } } }

        private String _Postcode;
        /// <summary>邮政编码</summary>
        [DisplayName("邮政编码")]
        [Description("邮政编码")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Postcode", "邮政编码", "nvarchar(20)")]
        public String Postcode { get { return _Postcode; } set { if (OnPropertyChanging(__.Postcode, value)) { _Postcode = value; OnPropertyChanged(__.Postcode); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "datetime")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "int")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }
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
                    case __.RealName : return _RealName;
                    case __.Tel : return _Tel;
                    case __.Email : return _Email;
                    case __.IsDefault : return _IsDefault;
                    case __.Phone : return _Phone;
                    case __.Company : return _Company;
                    case __.Country : return _Country;
                    case __.Province : return _Province;
                    case __.City : return _City;
                    case __.District : return _District;
                    case __.Address : return _Address;
                    case __.Postcode : return _Postcode;
                    case __.AddTime : return _AddTime;
                    case __.UpdateTime : return _UpdateTime;
                    case __.Rank : return _Rank;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.RealName : _RealName = Convert.ToString(value); break;
                    case __.Tel : _Tel = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.IsDefault : _IsDefault = Convert.ToBoolean(value); break;
                    case __.Phone : _Phone = Convert.ToString(value); break;
                    case __.Company : _Company = Convert.ToString(value); break;
                    case __.Country : _Country = Convert.ToString(value); break;
                    case __.Province : _Province = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.District : _District = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Postcode : _Postcode = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.UpdateTime : _UpdateTime = Convert.ToDateTime(value); break;
                    case __.Rank : _Rank = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户地址字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName(__.RealName);

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>是否是默认</summary>
            public static readonly Field IsDefault = FindByName(__.IsDefault);

            /// <summary>电话</summary>
            public static readonly Field Phone = FindByName(__.Phone);

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName(__.Company);

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName(__.Country);

            /// <summary>省</summary>
            public static readonly Field Province = FindByName(__.Province);

            /// <summary>市</summary>
            public static readonly Field City = FindByName(__.City);

            /// <summary>区</summary>
            public static readonly Field District = FindByName(__.District);

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            /// <summary>邮政编码</summary>
            public static readonly Field Postcode = FindByName(__.Postcode);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得用户地址字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>姓名</summary>
            public const String RealName = "RealName";

            /// <summary>手机</summary>
            public const String Tel = "Tel";

            /// <summary>邮件</summary>
            public const String Email = "Email";

            /// <summary>是否是默认</summary>
            public const String IsDefault = "IsDefault";

            /// <summary>电话</summary>
            public const String Phone = "Phone";

            /// <summary>公司</summary>
            public const String Company = "Company";

            /// <summary>国家</summary>
            public const String Country = "Country";

            /// <summary>省</summary>
            public const String Province = "Province";

            /// <summary>市</summary>
            public const String City = "City";

            /// <summary>区</summary>
            public const String District = "District";

            /// <summary>详细地址</summary>
            public const String Address = "Address";

            /// <summary>邮政编码</summary>
            public const String Postcode = "Postcode";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>排序</summary>
            public const String Rank = "Rank";
        }
        #endregion
    }

    /// <summary>用户地址接口</summary>
    public partial interface IMemberAddress
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>姓名</summary>
        String RealName { get; set; }

        /// <summary>手机</summary>
        String Tel { get; set; }

        /// <summary>邮件</summary>
        String Email { get; set; }

        /// <summary>是否是默认</summary>
        Boolean IsDefault { get; set; }

        /// <summary>电话</summary>
        String Phone { get; set; }

        /// <summary>公司</summary>
        String Company { get; set; }

        /// <summary>国家</summary>
        String Country { get; set; }

        /// <summary>省</summary>
        String Province { get; set; }

        /// <summary>市</summary>
        String City { get; set; }

        /// <summary>区</summary>
        String District { get; set; }

        /// <summary>详细地址</summary>
        String Address { get; set; }

        /// <summary>邮政编码</summary>
        String Postcode { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}