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
    /// <summary>商家</summary>
    [Serializable]
    [DataObject]
    [Description("商家")]
    [BindTable("Shop", Description = "商家", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Shop
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _ShopName;
        /// <summary>店铺名称</summary>
        [DisplayName("店铺名称")]
        [Description("店铺名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("ShopName", "店铺名称", "")]
        public String ShopName { get => _ShopName; set { if (OnPropertyChanging("ShopName", value)) { _ShopName = value; OnPropertyChanged("ShopName"); } } }

        private Int32 _KId;
        /// <summary>商家分类ID</summary>
        [DisplayName("商家分类ID")]
        [Description("商家分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "商家分类ID", "")]
        public Int32 KId { get => _KId; set { if (OnPropertyChanging("KId", value)) { _KId = value; OnPropertyChanged("KId"); } } }

        private Int32 _AId;
        /// <summary>地区ID</summary>
        [DisplayName("地区ID")]
        [Description("地区ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AId", "地区ID", "")]
        public Int32 AId { get => _AId; set { if (OnPropertyChanging("AId", value)) { _AId = value; OnPropertyChanged("AId"); } } }

        private Int32 _Sequence;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序", "")]
        public Int32 Sequence { get => _Sequence; set { if (OnPropertyChanging("Sequence", value)) { _Sequence = value; OnPropertyChanged("Sequence"); } } }

        private Decimal _Latitude;
        /// <summary>纬度</summary>
        [DisplayName("纬度")]
        [Description("纬度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Latitude", "纬度", "")]
        public Decimal Latitude { get => _Latitude; set { if (OnPropertyChanging("Latitude", value)) { _Latitude = value; OnPropertyChanged("Latitude"); } } }

        private Decimal _Longitude;
        /// <summary>经度</summary>
        [DisplayName("经度")]
        [Description("经度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Longitude", "经度", "")]
        public Decimal Longitude { get => _Longitude; set { if (OnPropertyChanging("Longitude", value)) { _Longitude = value; OnPropertyChanged("Longitude"); } } }

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

        private Int32 _IsDel;
        /// <summary>是否删除</summary>
        [DisplayName("是否删除")]
        [Description("是否删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除", "")]
        public Int32 IsDel { get => _IsDel; set { if (OnPropertyChanging("IsDel", value)) { _IsDel = value; OnPropertyChanged("IsDel"); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "")]
        public Int32 IsHide { get => _IsHide; set { if (OnPropertyChanging("IsHide", value)) { _IsHide = value; OnPropertyChanged("IsHide"); } } }

        private Int32 _IsDisabled;
        /// <summary>是否禁用</summary>
        [DisplayName("是否禁用")]
        [Description("是否禁用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisabled", "是否禁用", "")]
        public Int32 IsDisabled { get => _IsDisabled; set { if (OnPropertyChanging("IsDisabled", value)) { _IsDisabled = value; OnPropertyChanged("IsDisabled"); } } }

        private String _Content;
        /// <summary>店铺介绍</summary>
        [DisplayName("店铺介绍")]
        [Description("店铺介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "店铺介绍", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

        private String _Keyword;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "关键字", "")]
        public String Keyword { get => _Keyword; set { if (OnPropertyChanging("Keyword", value)) { _Keyword = value; OnPropertyChanged("Keyword"); } } }

        private String _Description;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "描述", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

        private String _BannerImg;
        /// <summary>banner图片</summary>
        [DisplayName("banner图片")]
        [Description("banner图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("BannerImg", "banner图片", "")]
        public String BannerImg { get => _BannerImg; set { if (OnPropertyChanging("BannerImg", value)) { _BannerImg = value; OnPropertyChanged("BannerImg"); } } }

        private Decimal _Balance;
        /// <summary>余额</summary>
        [DisplayName("余额")]
        [Description("余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Balance", "余额", "")]
        public Decimal Balance { get => _Balance; set { if (OnPropertyChanging("Balance", value)) { _Balance = value; OnPropertyChanged("Balance"); } } }

        private Int32 _IsTop;
        /// <summary>是否置顶</summary>
        [DisplayName("是否置顶")]
        [Description("是否置顶")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsTop", "是否置顶", "")]
        public Int32 IsTop { get => _IsTop; set { if (OnPropertyChanging("IsTop", value)) { _IsTop = value; OnPropertyChanged("IsTop"); } } }

        private Int32 _IsVip;
        /// <summary>是否vip</summary>
        [DisplayName("是否vip")]
        [Description("是否vip")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVip", "是否vip", "")]
        public Int32 IsVip { get => _IsVip; set { if (OnPropertyChanging("IsVip", value)) { _IsVip = value; OnPropertyChanged("IsVip"); } } }

        private Int32 _IsRecommend;
        /// <summary>是否推荐</summary>
        [DisplayName("是否推荐")]
        [Description("是否推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRecommend", "是否推荐", "")]
        public Int32 IsRecommend { get => _IsRecommend; set { if (OnPropertyChanging("IsRecommend", value)) { _IsRecommend = value; OnPropertyChanged("IsRecommend"); } } }

        private Int32 _Likes;
        /// <summary>点赞数</summary>
        [DisplayName("点赞数")]
        [Description("点赞数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Likes", "点赞数", "")]
        public Int32 Likes { get => _Likes; set { if (OnPropertyChanging("Likes", value)) { _Likes = value; OnPropertyChanged("Likes"); } } }

        private Decimal _AvgScore;
        /// <summary>平均分数</summary>
        [DisplayName("平均分数")]
        [Description("平均分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AvgScore", "平均分数", "")]
        public Decimal AvgScore { get => _AvgScore; set { if (OnPropertyChanging("AvgScore", value)) { _AvgScore = value; OnPropertyChanged("AvgScore"); } } }

        private Decimal _ServiceScore;
        /// <summary>服务分数</summary>
        [DisplayName("服务分数")]
        [Description("服务分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ServiceScore", "服务分数", "")]
        public Decimal ServiceScore { get => _ServiceScore; set { if (OnPropertyChanging("ServiceScore", value)) { _ServiceScore = value; OnPropertyChanged("ServiceScore"); } } }

        private Decimal _SpeedScore;
        /// <summary>速度分数</summary>
        [DisplayName("速度分数")]
        [Description("速度分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpeedScore", "速度分数", "")]
        public Decimal SpeedScore { get => _SpeedScore; set { if (OnPropertyChanging("SpeedScore", value)) { _SpeedScore = value; OnPropertyChanged("SpeedScore"); } } }

        private Decimal _EnvironmentScore;
        /// <summary>环境分数</summary>
        [DisplayName("环境分数")]
        [Description("环境分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EnvironmentScore", "环境分数", "")]
        public Decimal EnvironmentScore { get => _EnvironmentScore; set { if (OnPropertyChanging("EnvironmentScore", value)) { _EnvironmentScore = value; OnPropertyChanged("EnvironmentScore"); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

        private String _MorePics;
        /// <summary>店铺所有图片</summary>
        [DisplayName("店铺所有图片")]
        [Description("店铺所有图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("MorePics", "店铺所有图片", "")]
        public String MorePics { get => _MorePics; set { if (OnPropertyChanging("MorePics", value)) { _MorePics = value; OnPropertyChanged("MorePics"); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "")]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Tel", "电话", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Phone;
        /// <summary>固定电话</summary>
        [DisplayName("固定电话")]
        [Description("固定电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Phone", "固定电话", "")]
        public String Phone { get => _Phone; set { if (OnPropertyChanging("Phone", value)) { _Phone = value; OnPropertyChanged("Phone"); } } }

        private String _Qq;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Qq", "QQ", "")]
        public String Qq { get => _Qq; set { if (OnPropertyChanging("Qq", value)) { _Qq = value; OnPropertyChanged("Qq"); } } }

        private String _Skype;
        /// <summary>Skype</summary>
        [DisplayName("Skype")]
        [Description("Skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "Skype", "")]
        public String Skype { get => _Skype; set { if (OnPropertyChanging("Skype", value)) { _Skype = value; OnPropertyChanged("Skype"); } } }

        private String _HomePage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("HomePage", "主页", "")]
        public String HomePage { get => _HomePage; set { if (OnPropertyChanging("HomePage", value)) { _HomePage = value; OnPropertyChanged("HomePage"); } } }

        private String _Weixin;
        /// <summary>微信</summary>
        [DisplayName("微信")]
        [Description("微信")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weixin", "微信", "")]
        public String Weixin { get => _Weixin; set { if (OnPropertyChanging("Weixin", value)) { _Weixin = value; OnPropertyChanged("Weixin"); } } }

        private Int32 _IsShip;
        /// <summary>是否配送</summary>
        [DisplayName("是否配送")]
        [Description("是否配送")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShip", "是否配送", "")]
        public Int32 IsShip { get => _IsShip; set { if (OnPropertyChanging("IsShip", value)) { _IsShip = value; OnPropertyChanged("IsShip"); } } }

        private DateTime _OpenTime;
        /// <summary>开店时间</summary>
        [DisplayName("开店时间")]
        [Description("开店时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("OpenTime", "开店时间", "")]
        public DateTime OpenTime { get => _OpenTime; set { if (OnPropertyChanging("OpenTime", value)) { _OpenTime = value; OnPropertyChanged("OpenTime"); } } }

        private DateTime _CloseTime;
        /// <summary>关店时间</summary>
        [DisplayName("关店时间")]
        [Description("关店时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CloseTime", "关店时间", "")]
        public DateTime CloseTime { get => _CloseTime; set { if (OnPropertyChanging("CloseTime", value)) { _CloseTime = value; OnPropertyChanged("CloseTime"); } } }

        private DateTime _ShippingStartTime;
        /// <summary>配送开始时间</summary>
        [DisplayName("配送开始时间")]
        [Description("配送开始时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ShippingStartTime", "配送开始时间", "")]
        public DateTime ShippingStartTime { get => _ShippingStartTime; set { if (OnPropertyChanging("ShippingStartTime", value)) { _ShippingStartTime = value; OnPropertyChanged("ShippingStartTime"); } } }

        private DateTime _ShippingEndTime;
        /// <summary>配送结束时间</summary>
        [DisplayName("配送结束时间")]
        [Description("配送结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ShippingEndTime", "配送结束时间", "")]
        public DateTime ShippingEndTime { get => _ShippingEndTime; set { if (OnPropertyChanging("ShippingEndTime", value)) { _ShippingEndTime = value; OnPropertyChanged("ShippingEndTime"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private Int32 _Hits;
        /// <summary>点击量</summary>
        [DisplayName("点击量")]
        [Description("点击量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging("Hits", value)) { _Hits = value; OnPropertyChanged("Hits"); } } }

        private Int32 _MyType;
        /// <summary>店铺类型</summary>
        [DisplayName("店铺类型")]
        [Description("店铺类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "店铺类型", "")]
        public Int32 MyType { get => _MyType; set { if (OnPropertyChanging("MyType", value)) { _MyType = value; OnPropertyChanged("MyType"); } } }

        private Decimal _DefaultFare;
        /// <summary>默认运费</summary>
        [DisplayName("默认运费")]
        [Description("默认运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DefaultFare", "默认运费", "")]
        public Decimal DefaultFare { get => _DefaultFare; set { if (OnPropertyChanging("DefaultFare", value)) { _DefaultFare = value; OnPropertyChanged("DefaultFare"); } } }

        private Decimal _MaxFreeFare;
        /// <summary>最大免运费金额</summary>
        [DisplayName("最大免运费金额")]
        [Description("最大免运费金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxFreeFare", "最大免运费金额", "")]
        public Decimal MaxFreeFare { get => _MaxFreeFare; set { if (OnPropertyChanging("MaxFreeFare", value)) { _MaxFreeFare = value; OnPropertyChanged("MaxFreeFare"); } } }
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
                    case "ShopName": return _ShopName;
                    case "KId": return _KId;
                    case "AId": return _AId;
                    case "Sequence": return _Sequence;
                    case "Latitude": return _Latitude;
                    case "Longitude": return _Longitude;
                    case "Country": return _Country;
                    case "Province": return _Province;
                    case "City": return _City;
                    case "District": return _District;
                    case "Address": return _Address;
                    case "Postcode": return _Postcode;
                    case "IsDel": return _IsDel;
                    case "IsHide": return _IsHide;
                    case "IsDisabled": return _IsDisabled;
                    case "Content": return _Content;
                    case "Keyword": return _Keyword;
                    case "Description": return _Description;
                    case "BannerImg": return _BannerImg;
                    case "Balance": return _Balance;
                    case "IsTop": return _IsTop;
                    case "IsVip": return _IsVip;
                    case "IsRecommend": return _IsRecommend;
                    case "Likes": return _Likes;
                    case "AvgScore": return _AvgScore;
                    case "ServiceScore": return _ServiceScore;
                    case "SpeedScore": return _SpeedScore;
                    case "EnvironmentScore": return _EnvironmentScore;
                    case "Pic": return _Pic;
                    case "MorePics": return _MorePics;
                    case "Email": return _Email;
                    case "Tel": return _Tel;
                    case "Phone": return _Phone;
                    case "Qq": return _Qq;
                    case "Skype": return _Skype;
                    case "HomePage": return _HomePage;
                    case "Weixin": return _Weixin;
                    case "IsShip": return _IsShip;
                    case "OpenTime": return _OpenTime;
                    case "CloseTime": return _CloseTime;
                    case "ShippingStartTime": return _ShippingStartTime;
                    case "ShippingEndTime": return _ShippingEndTime;
                    case "AddTime": return _AddTime;
                    case "Hits": return _Hits;
                    case "MyType": return _MyType;
                    case "DefaultFare": return _DefaultFare;
                    case "MaxFreeFare": return _MaxFreeFare;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "ShopName": _ShopName = Convert.ToString(value); break;
                    case "KId": _KId = value.ToInt(); break;
                    case "AId": _AId = value.ToInt(); break;
                    case "Sequence": _Sequence = value.ToInt(); break;
                    case "Latitude": _Latitude = Convert.ToDecimal(value); break;
                    case "Longitude": _Longitude = Convert.ToDecimal(value); break;
                    case "Country": _Country = Convert.ToString(value); break;
                    case "Province": _Province = Convert.ToString(value); break;
                    case "City": _City = Convert.ToString(value); break;
                    case "District": _District = Convert.ToString(value); break;
                    case "Address": _Address = Convert.ToString(value); break;
                    case "Postcode": _Postcode = Convert.ToString(value); break;
                    case "IsDel": _IsDel = value.ToInt(); break;
                    case "IsHide": _IsHide = value.ToInt(); break;
                    case "IsDisabled": _IsDisabled = value.ToInt(); break;
                    case "Content": _Content = Convert.ToString(value); break;
                    case "Keyword": _Keyword = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "BannerImg": _BannerImg = Convert.ToString(value); break;
                    case "Balance": _Balance = Convert.ToDecimal(value); break;
                    case "IsTop": _IsTop = value.ToInt(); break;
                    case "IsVip": _IsVip = value.ToInt(); break;
                    case "IsRecommend": _IsRecommend = value.ToInt(); break;
                    case "Likes": _Likes = value.ToInt(); break;
                    case "AvgScore": _AvgScore = Convert.ToDecimal(value); break;
                    case "ServiceScore": _ServiceScore = Convert.ToDecimal(value); break;
                    case "SpeedScore": _SpeedScore = Convert.ToDecimal(value); break;
                    case "EnvironmentScore": _EnvironmentScore = Convert.ToDecimal(value); break;
                    case "Pic": _Pic = Convert.ToString(value); break;
                    case "MorePics": _MorePics = Convert.ToString(value); break;
                    case "Email": _Email = Convert.ToString(value); break;
                    case "Tel": _Tel = Convert.ToString(value); break;
                    case "Phone": _Phone = Convert.ToString(value); break;
                    case "Qq": _Qq = Convert.ToString(value); break;
                    case "Skype": _Skype = Convert.ToString(value); break;
                    case "HomePage": _HomePage = Convert.ToString(value); break;
                    case "Weixin": _Weixin = Convert.ToString(value); break;
                    case "IsShip": _IsShip = value.ToInt(); break;
                    case "OpenTime": _OpenTime = value.ToDateTime(); break;
                    case "CloseTime": _CloseTime = value.ToDateTime(); break;
                    case "ShippingStartTime": _ShippingStartTime = value.ToDateTime(); break;
                    case "ShippingEndTime": _ShippingEndTime = value.ToDateTime(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "Hits": _Hits = value.ToInt(); break;
                    case "MyType": _MyType = value.ToInt(); break;
                    case "DefaultFare": _DefaultFare = Convert.ToDecimal(value); break;
                    case "MaxFreeFare": _MaxFreeFare = Convert.ToDecimal(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得商家字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>店铺名称</summary>
            public static readonly Field ShopName = FindByName("ShopName");

            /// <summary>商家分类ID</summary>
            public static readonly Field KId = FindByName("KId");

            /// <summary>地区ID</summary>
            public static readonly Field AId = FindByName("AId");

            /// <summary>排序</summary>
            public static readonly Field Sequence = FindByName("Sequence");

            /// <summary>纬度</summary>
            public static readonly Field Latitude = FindByName("Latitude");

            /// <summary>经度</summary>
            public static readonly Field Longitude = FindByName("Longitude");

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

            /// <summary>是否删除</summary>
            public static readonly Field IsDel = FindByName("IsDel");

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName("IsHide");

            /// <summary>是否禁用</summary>
            public static readonly Field IsDisabled = FindByName("IsDisabled");

            /// <summary>店铺介绍</summary>
            public static readonly Field Content = FindByName("Content");

            /// <summary>关键字</summary>
            public static readonly Field Keyword = FindByName("Keyword");

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName("Description");

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName("BannerImg");

            /// <summary>余额</summary>
            public static readonly Field Balance = FindByName("Balance");

            /// <summary>是否置顶</summary>
            public static readonly Field IsTop = FindByName("IsTop");

            /// <summary>是否vip</summary>
            public static readonly Field IsVip = FindByName("IsVip");

            /// <summary>是否推荐</summary>
            public static readonly Field IsRecommend = FindByName("IsRecommend");

            /// <summary>点赞数</summary>
            public static readonly Field Likes = FindByName("Likes");

            /// <summary>平均分数</summary>
            public static readonly Field AvgScore = FindByName("AvgScore");

            /// <summary>服务分数</summary>
            public static readonly Field ServiceScore = FindByName("ServiceScore");

            /// <summary>速度分数</summary>
            public static readonly Field SpeedScore = FindByName("SpeedScore");

            /// <summary>环境分数</summary>
            public static readonly Field EnvironmentScore = FindByName("EnvironmentScore");

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName("Pic");

            /// <summary>店铺所有图片</summary>
            public static readonly Field MorePics = FindByName("MorePics");

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>固定电话</summary>
            public static readonly Field Phone = FindByName("Phone");

            /// <summary>QQ</summary>
            public static readonly Field Qq = FindByName("Qq");

            /// <summary>Skype</summary>
            public static readonly Field Skype = FindByName("Skype");

            /// <summary>主页</summary>
            public static readonly Field HomePage = FindByName("HomePage");

            /// <summary>微信</summary>
            public static readonly Field Weixin = FindByName("Weixin");

            /// <summary>是否配送</summary>
            public static readonly Field IsShip = FindByName("IsShip");

            /// <summary>开店时间</summary>
            public static readonly Field OpenTime = FindByName("OpenTime");

            /// <summary>关店时间</summary>
            public static readonly Field CloseTime = FindByName("CloseTime");

            /// <summary>配送开始时间</summary>
            public static readonly Field ShippingStartTime = FindByName("ShippingStartTime");

            /// <summary>配送结束时间</summary>
            public static readonly Field ShippingEndTime = FindByName("ShippingEndTime");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>点击量</summary>
            public static readonly Field Hits = FindByName("Hits");

            /// <summary>店铺类型</summary>
            public static readonly Field MyType = FindByName("MyType");

            /// <summary>默认运费</summary>
            public static readonly Field DefaultFare = FindByName("DefaultFare");

            /// <summary>最大免运费金额</summary>
            public static readonly Field MaxFreeFare = FindByName("MaxFreeFare");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得商家字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>店铺名称</summary>
            public const String ShopName = "ShopName";

            /// <summary>商家分类ID</summary>
            public const String KId = "KId";

            /// <summary>地区ID</summary>
            public const String AId = "AId";

            /// <summary>排序</summary>
            public const String Sequence = "Sequence";

            /// <summary>纬度</summary>
            public const String Latitude = "Latitude";

            /// <summary>经度</summary>
            public const String Longitude = "Longitude";

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

            /// <summary>是否删除</summary>
            public const String IsDel = "IsDel";

            /// <summary>是否隐藏</summary>
            public const String IsHide = "IsHide";

            /// <summary>是否禁用</summary>
            public const String IsDisabled = "IsDisabled";

            /// <summary>店铺介绍</summary>
            public const String Content = "Content";

            /// <summary>关键字</summary>
            public const String Keyword = "Keyword";

            /// <summary>描述</summary>
            public const String Description = "Description";

            /// <summary>banner图片</summary>
            public const String BannerImg = "BannerImg";

            /// <summary>余额</summary>
            public const String Balance = "Balance";

            /// <summary>是否置顶</summary>
            public const String IsTop = "IsTop";

            /// <summary>是否vip</summary>
            public const String IsVip = "IsVip";

            /// <summary>是否推荐</summary>
            public const String IsRecommend = "IsRecommend";

            /// <summary>点赞数</summary>
            public const String Likes = "Likes";

            /// <summary>平均分数</summary>
            public const String AvgScore = "AvgScore";

            /// <summary>服务分数</summary>
            public const String ServiceScore = "ServiceScore";

            /// <summary>速度分数</summary>
            public const String SpeedScore = "SpeedScore";

            /// <summary>环境分数</summary>
            public const String EnvironmentScore = "EnvironmentScore";

            /// <summary>图片</summary>
            public const String Pic = "Pic";

            /// <summary>店铺所有图片</summary>
            public const String MorePics = "MorePics";

            /// <summary>邮箱</summary>
            public const String Email = "Email";

            /// <summary>电话</summary>
            public const String Tel = "Tel";

            /// <summary>固定电话</summary>
            public const String Phone = "Phone";

            /// <summary>QQ</summary>
            public const String Qq = "Qq";

            /// <summary>Skype</summary>
            public const String Skype = "Skype";

            /// <summary>主页</summary>
            public const String HomePage = "HomePage";

            /// <summary>微信</summary>
            public const String Weixin = "Weixin";

            /// <summary>是否配送</summary>
            public const String IsShip = "IsShip";

            /// <summary>开店时间</summary>
            public const String OpenTime = "OpenTime";

            /// <summary>关店时间</summary>
            public const String CloseTime = "CloseTime";

            /// <summary>配送开始时间</summary>
            public const String ShippingStartTime = "ShippingStartTime";

            /// <summary>配送结束时间</summary>
            public const String ShippingEndTime = "ShippingEndTime";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>点击量</summary>
            public const String Hits = "Hits";

            /// <summary>店铺类型</summary>
            public const String MyType = "MyType";

            /// <summary>默认运费</summary>
            public const String DefaultFare = "DefaultFare";

            /// <summary>最大免运费金额</summary>
            public const String MaxFreeFare = "MaxFreeFare";
        }
        #endregion
    }
}