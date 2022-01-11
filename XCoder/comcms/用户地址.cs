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
    /// <summary>用户地址</summary>
    [Serializable]
    [DataObject]
    [Description("用户地址")]
    [BindTable("MemberAddress", Description = "用户地址", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class MemberAddress
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

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RealName", "姓名", "")]
        public String RealName { get => _RealName; set { if (OnPropertyChanging("RealName", value)) { _RealName = value; OnPropertyChanged("RealName"); } } }

        private String _Tel;
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "")]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private Boolean _IsDefault;
        /// <summary>是否是默认</summary>
        [DisplayName("是否是默认")]
        [Description("是否是默认")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否是默认", "")]
        public Boolean IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

        private String _Phone;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Phone", "电话", "")]
        public String Phone { get => _Phone; set { if (OnPropertyChanging("Phone", value)) { _Phone = value; OnPropertyChanged("Phone"); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Company", "公司", "")]
        public String Company { get => _Company; set { if (OnPropertyChanging("Company", value)) { _Company = value; OnPropertyChanged("Company"); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "")]
        public String Country { get => _Country; set { if (OnPropertyChanging("Country", value)) { _Country = value; OnPropertyChanged("Country"); } } }

        private String _Province;
        /// <summary>省</summary>
        [DisplayName("省")]
        [Description("省")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省", "")]
        public String Province { get => _Province; set { if (OnPropertyChanging("Province", value)) { _Province = value; OnPropertyChanged("Province"); } } }

        private String _City;
        /// <summary>市</summary>
        [DisplayName("市")]
        [Description("市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "市", "")]
        public String City { get => _City; set { if (OnPropertyChanging("City", value)) { _City = value; OnPropertyChanged("City"); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "")]
        public String District { get => _District; set { if (OnPropertyChanging("District", value)) { _District = value; OnPropertyChanged("District"); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "")]
        public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

        private String _Postcode;
        /// <summary>邮政编码</summary>
        [DisplayName("邮政编码")]
        [Description("邮政编码")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Postcode", "邮政编码", "")]
        public String Postcode { get => _Postcode; set { if (OnPropertyChanging("Postcode", value)) { _Postcode = value; OnPropertyChanged("Postcode"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }
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
                    case "RealName": return _RealName;
                    case "Tel": return _Tel;
                    case "Email": return _Email;
                    case "IsDefault": return _IsDefault;
                    case "Phone": return _Phone;
                    case "Company": return _Company;
                    case "Country": return _Country;
                    case "Province": return _Province;
                    case "City": return _City;
                    case "District": return _District;
                    case "Address": return _Address;
                    case "Postcode": return _Postcode;
                    case "AddTime": return _AddTime;
                    case "UpdateTime": return _UpdateTime;
                    case "Rank": return _Rank;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "RealName": _RealName = Convert.ToString(value); break;
                    case "Tel": _Tel = Convert.ToString(value); break;
                    case "Email": _Email = Convert.ToString(value); break;
                    case "IsDefault": _IsDefault = value.ToBoolean(); break;
                    case "Phone": _Phone = Convert.ToString(value); break;
                    case "Company": _Company = Convert.ToString(value); break;
                    case "Country": _Country = Convert.ToString(value); break;
                    case "Province": _Province = Convert.ToString(value); break;
                    case "City": _City = Convert.ToString(value); break;
                    case "District": _District = Convert.ToString(value); break;
                    case "Address": _Address = Convert.ToString(value); break;
                    case "Postcode": _Postcode = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    case "Rank": _Rank = value.ToInt(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName("RealName");

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>是否是默认</summary>
            public static readonly Field IsDefault = FindByName("IsDefault");

            /// <summary>电话</summary>
            public static readonly Field Phone = FindByName("Phone");

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName("Company");

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName("Country");

            /// <summary>省</summary>
            public static readonly Field Province = FindByName("Province");

            /// <summary>市</summary>
            public static readonly Field City = FindByName("City");

            /// <summary>区</summary>
            public static readonly Field District = FindByName("District");

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName("Address");

            /// <summary>邮政编码</summary>
            public static readonly Field Postcode = FindByName("Postcode");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}