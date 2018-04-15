using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Shop : IShop
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _ShopName;
        /// <summary>店铺名称</summary>
        [DisplayName("店铺名称")]
        [Description("店铺名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("ShopName", "店铺名称", "nvarchar(200)")]
        public String ShopName { get { return _ShopName; } set { if (OnPropertyChanging(__.ShopName, value)) { _ShopName = value; OnPropertyChanged(__.ShopName); } } }

        private Int32 _KId;
        /// <summary>商家分类ID</summary>
        [DisplayName("商家分类ID")]
        [Description("商家分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "商家分类ID", "int")]
        public Int32 KId { get { return _KId; } set { if (OnPropertyChanging(__.KId, value)) { _KId = value; OnPropertyChanged(__.KId); } } }

        private Int32 _AId;
        /// <summary>地区ID</summary>
        [DisplayName("地区ID")]
        [Description("地区ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AId", "地区ID", "int")]
        public Int32 AId { get { return _AId; } set { if (OnPropertyChanging(__.AId, value)) { _AId = value; OnPropertyChanged(__.AId); } } }

        private Int32 _Sequence;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序", "int")]
        public Int32 Sequence { get { return _Sequence; } set { if (OnPropertyChanging(__.Sequence, value)) { _Sequence = value; OnPropertyChanged(__.Sequence); } } }

        private Decimal _Latitude;
        /// <summary>纬度</summary>
        [DisplayName("纬度")]
        [Description("纬度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Latitude", "纬度", "money")]
        public Decimal Latitude { get { return _Latitude; } set { if (OnPropertyChanging(__.Latitude, value)) { _Latitude = value; OnPropertyChanged(__.Latitude); } } }

        private Decimal _Longitude;
        /// <summary>经度</summary>
        [DisplayName("经度")]
        [Description("经度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Longitude", "经度", "money")]
        public Decimal Longitude { get { return _Longitude; } set { if (OnPropertyChanging(__.Longitude, value)) { _Longitude = value; OnPropertyChanged(__.Longitude); } } }

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

        private Int32 _IsDel;
        /// <summary>是否删除</summary>
        [DisplayName("是否删除")]
        [Description("是否删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除", "int")]
        public Int32 IsDel { get { return _IsDel; } set { if (OnPropertyChanging(__.IsDel, value)) { _IsDel = value; OnPropertyChanged(__.IsDel); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "int")]
        public Int32 IsHide { get { return _IsHide; } set { if (OnPropertyChanging(__.IsHide, value)) { _IsHide = value; OnPropertyChanged(__.IsHide); } } }

        private Int32 _IsDisabled;
        /// <summary>是否禁用</summary>
        [DisplayName("是否禁用")]
        [Description("是否禁用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDisabled", "是否禁用", "int")]
        public Int32 IsDisabled { get { return _IsDisabled; } set { if (OnPropertyChanging(__.IsDisabled, value)) { _IsDisabled = value; OnPropertyChanged(__.IsDisabled); } } }

        private String _Content;
        /// <summary>店铺介绍</summary>
        [DisplayName("店铺介绍")]
        [Description("店铺介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "店铺介绍", "ntext")]
        public String Content { get { return _Content; } set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private String _Keyword;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "关键字", "nvarchar(250)")]
        public String Keyword { get { return _Keyword; } set { if (OnPropertyChanging(__.Keyword, value)) { _Keyword = value; OnPropertyChanged(__.Keyword); } } }

        private String _Description;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "描述", "nvarchar(250)")]
        public String Description { get { return _Description; } set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private String _BannerImg;
        /// <summary>banner图片</summary>
        [DisplayName("banner图片")]
        [Description("banner图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("BannerImg", "banner图片", "nvarchar(250)")]
        public String BannerImg { get { return _BannerImg; } set { if (OnPropertyChanging(__.BannerImg, value)) { _BannerImg = value; OnPropertyChanged(__.BannerImg); } } }

        private Decimal _Balance;
        /// <summary>余额</summary>
        [DisplayName("余额")]
        [Description("余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Balance", "余额", "money")]
        public Decimal Balance { get { return _Balance; } set { if (OnPropertyChanging(__.Balance, value)) { _Balance = value; OnPropertyChanged(__.Balance); } } }

        private Int32 _IsTop;
        /// <summary>是否置顶</summary>
        [DisplayName("是否置顶")]
        [Description("是否置顶")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsTop", "是否置顶", "int")]
        public Int32 IsTop { get { return _IsTop; } set { if (OnPropertyChanging(__.IsTop, value)) { _IsTop = value; OnPropertyChanged(__.IsTop); } } }

        private Int32 _IsVip;
        /// <summary>是否vip</summary>
        [DisplayName("是否vip")]
        [Description("是否vip")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVip", "是否vip", "int")]
        public Int32 IsVip { get { return _IsVip; } set { if (OnPropertyChanging(__.IsVip, value)) { _IsVip = value; OnPropertyChanged(__.IsVip); } } }

        private Int32 _IsRecommend;
        /// <summary>是否推荐</summary>
        [DisplayName("是否推荐")]
        [Description("是否推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRecommend", "是否推荐", "int")]
        public Int32 IsRecommend { get { return _IsRecommend; } set { if (OnPropertyChanging(__.IsRecommend, value)) { _IsRecommend = value; OnPropertyChanged(__.IsRecommend); } } }

        private Int32 _Likes;
        /// <summary>点赞数</summary>
        [DisplayName("点赞数")]
        [Description("点赞数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Likes", "点赞数", "int")]
        public Int32 Likes { get { return _Likes; } set { if (OnPropertyChanging(__.Likes, value)) { _Likes = value; OnPropertyChanged(__.Likes); } } }

        private Decimal _AvgScore;
        /// <summary>平均分数</summary>
        [DisplayName("平均分数")]
        [Description("平均分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AvgScore", "平均分数", "money")]
        public Decimal AvgScore { get { return _AvgScore; } set { if (OnPropertyChanging(__.AvgScore, value)) { _AvgScore = value; OnPropertyChanged(__.AvgScore); } } }

        private Decimal _ServiceScore;
        /// <summary>服务分数</summary>
        [DisplayName("服务分数")]
        [Description("服务分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ServiceScore", "服务分数", "money")]
        public Decimal ServiceScore { get { return _ServiceScore; } set { if (OnPropertyChanging(__.ServiceScore, value)) { _ServiceScore = value; OnPropertyChanged(__.ServiceScore); } } }

        private Decimal _SpeedScore;
        /// <summary>速度分数</summary>
        [DisplayName("速度分数")]
        [Description("速度分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpeedScore", "速度分数", "money")]
        public Decimal SpeedScore { get { return _SpeedScore; } set { if (OnPropertyChanging(__.SpeedScore, value)) { _SpeedScore = value; OnPropertyChanged(__.SpeedScore); } } }

        private Decimal _EnvironmentScore;
        /// <summary>环境分数</summary>
        [DisplayName("环境分数")]
        [Description("环境分数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("EnvironmentScore", "环境分数", "money")]
        public Decimal EnvironmentScore { get { return _EnvironmentScore; } set { if (OnPropertyChanging(__.EnvironmentScore, value)) { _EnvironmentScore = value; OnPropertyChanged(__.EnvironmentScore); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "图片", "nvarchar(250)")]
        public String Pic { get { return _Pic; } set { if (OnPropertyChanging(__.Pic, value)) { _Pic = value; OnPropertyChanged(__.Pic); } } }

        private String _MorePics;
        /// <summary>店铺所有图片</summary>
        [DisplayName("店铺所有图片")]
        [Description("店铺所有图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("MorePics", "店铺所有图片", "ntext")]
        public String MorePics { get { return _MorePics; } set { if (OnPropertyChanging(__.MorePics, value)) { _MorePics = value; OnPropertyChanged(__.MorePics); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "nvarchar(100)")]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Tel", "电话", "nvarchar(50)")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _Phone;
        /// <summary>固定电话</summary>
        [DisplayName("固定电话")]
        [Description("固定电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Phone", "固定电话", "nvarchar(50)")]
        public String Phone { get { return _Phone; } set { if (OnPropertyChanging(__.Phone, value)) { _Phone = value; OnPropertyChanged(__.Phone); } } }

        private String _QQ;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("QQ", "QQ", "nvarchar(20)")]
        public String QQ { get { return _QQ; } set { if (OnPropertyChanging(__.QQ, value)) { _QQ = value; OnPropertyChanged(__.QQ); } } }

        private String _Skype;
        /// <summary>Skype</summary>
        [DisplayName("Skype")]
        [Description("Skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "Skype", "nvarchar(100)")]
        public String Skype { get { return _Skype; } set { if (OnPropertyChanging(__.Skype, value)) { _Skype = value; OnPropertyChanged(__.Skype); } } }

        private String _HomePage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("HomePage", "主页", "nvarchar(250)")]
        public String HomePage { get { return _HomePage; } set { if (OnPropertyChanging(__.HomePage, value)) { _HomePage = value; OnPropertyChanged(__.HomePage); } } }

        private String _Weixin;
        /// <summary>微信</summary>
        [DisplayName("微信")]
        [Description("微信")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weixin", "微信", "nvarchar(50)")]
        public String Weixin { get { return _Weixin; } set { if (OnPropertyChanging(__.Weixin, value)) { _Weixin = value; OnPropertyChanged(__.Weixin); } } }

        private Int32 _IsShip;
        /// <summary>是否配送</summary>
        [DisplayName("是否配送")]
        [Description("是否配送")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShip", "是否配送", "int")]
        public Int32 IsShip { get { return _IsShip; } set { if (OnPropertyChanging(__.IsShip, value)) { _IsShip = value; OnPropertyChanged(__.IsShip); } } }

        private DateTime _OpenTime;
        /// <summary>开店时间</summary>
        [DisplayName("开店时间")]
        [Description("开店时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("OpenTime", "开店时间", "datetime")]
        public DateTime OpenTime { get { return _OpenTime; } set { if (OnPropertyChanging(__.OpenTime, value)) { _OpenTime = value; OnPropertyChanged(__.OpenTime); } } }

        private DateTime _CloseTime;
        /// <summary>关店时间</summary>
        [DisplayName("关店时间")]
        [Description("关店时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CloseTime", "关店时间", "datetime")]
        public DateTime CloseTime { get { return _CloseTime; } set { if (OnPropertyChanging(__.CloseTime, value)) { _CloseTime = value; OnPropertyChanged(__.CloseTime); } } }

        private DateTime _ShippingStartTime;
        /// <summary>配送开始时间</summary>
        [DisplayName("配送开始时间")]
        [Description("配送开始时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ShippingStartTime", "配送开始时间", "datetime")]
        public DateTime ShippingStartTime { get { return _ShippingStartTime; } set { if (OnPropertyChanging(__.ShippingStartTime, value)) { _ShippingStartTime = value; OnPropertyChanged(__.ShippingStartTime); } } }

        private DateTime _ShippingEndTime;
        /// <summary>配送结束时间</summary>
        [DisplayName("配送结束时间")]
        [Description("配送结束时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ShippingEndTime", "配送结束时间", "datetime")]
        public DateTime ShippingEndTime { get { return _ShippingEndTime; } set { if (OnPropertyChanging(__.ShippingEndTime, value)) { _ShippingEndTime = value; OnPropertyChanged(__.ShippingEndTime); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private Int32 _Hits;
        /// <summary>点击量</summary>
        [DisplayName("点击量")]
        [Description("点击量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击量", "int")]
        public Int32 Hits { get { return _Hits; } set { if (OnPropertyChanging(__.Hits, value)) { _Hits = value; OnPropertyChanged(__.Hits); } } }

        private Int32 _MyType;
        /// <summary>店铺类型</summary>
        [DisplayName("店铺类型")]
        [Description("店铺类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MyType", "店铺类型", "int")]
        public Int32 MyType { get { return _MyType; } set { if (OnPropertyChanging(__.MyType, value)) { _MyType = value; OnPropertyChanged(__.MyType); } } }

        private Decimal _DefaultFare;
        /// <summary>默认运费</summary>
        [DisplayName("默认运费")]
        [Description("默认运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DefaultFare", "默认运费", "money")]
        public Decimal DefaultFare { get { return _DefaultFare; } set { if (OnPropertyChanging(__.DefaultFare, value)) { _DefaultFare = value; OnPropertyChanged(__.DefaultFare); } } }

        private Decimal _MaxFreeFare;
        /// <summary>最大免运费金额</summary>
        [DisplayName("最大免运费金额")]
        [Description("最大免运费金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxFreeFare", "最大免运费金额", "money")]
        public Decimal MaxFreeFare { get { return _MaxFreeFare; } set { if (OnPropertyChanging(__.MaxFreeFare, value)) { _MaxFreeFare = value; OnPropertyChanged(__.MaxFreeFare); } } }
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
                    case __.ShopName : return _ShopName;
                    case __.KId : return _KId;
                    case __.AId : return _AId;
                    case __.Sequence : return _Sequence;
                    case __.Latitude : return _Latitude;
                    case __.Longitude : return _Longitude;
                    case __.Country : return _Country;
                    case __.Province : return _Province;
                    case __.City : return _City;
                    case __.District : return _District;
                    case __.Address : return _Address;
                    case __.Postcode : return _Postcode;
                    case __.IsDel : return _IsDel;
                    case __.IsHide : return _IsHide;
                    case __.IsDisabled : return _IsDisabled;
                    case __.Content : return _Content;
                    case __.Keyword : return _Keyword;
                    case __.Description : return _Description;
                    case __.BannerImg : return _BannerImg;
                    case __.Balance : return _Balance;
                    case __.IsTop : return _IsTop;
                    case __.IsVip : return _IsVip;
                    case __.IsRecommend : return _IsRecommend;
                    case __.Likes : return _Likes;
                    case __.AvgScore : return _AvgScore;
                    case __.ServiceScore : return _ServiceScore;
                    case __.SpeedScore : return _SpeedScore;
                    case __.EnvironmentScore : return _EnvironmentScore;
                    case __.Pic : return _Pic;
                    case __.MorePics : return _MorePics;
                    case __.Email : return _Email;
                    case __.Tel : return _Tel;
                    case __.Phone : return _Phone;
                    case __.QQ : return _QQ;
                    case __.Skype : return _Skype;
                    case __.HomePage : return _HomePage;
                    case __.Weixin : return _Weixin;
                    case __.IsShip : return _IsShip;
                    case __.OpenTime : return _OpenTime;
                    case __.CloseTime : return _CloseTime;
                    case __.ShippingStartTime : return _ShippingStartTime;
                    case __.ShippingEndTime : return _ShippingEndTime;
                    case __.AddTime : return _AddTime;
                    case __.Hits : return _Hits;
                    case __.MyType : return _MyType;
                    case __.DefaultFare : return _DefaultFare;
                    case __.MaxFreeFare : return _MaxFreeFare;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.ShopName : _ShopName = Convert.ToString(value); break;
                    case __.KId : _KId = Convert.ToInt32(value); break;
                    case __.AId : _AId = Convert.ToInt32(value); break;
                    case __.Sequence : _Sequence = Convert.ToInt32(value); break;
                    case __.Latitude : _Latitude = Convert.ToDecimal(value); break;
                    case __.Longitude : _Longitude = Convert.ToDecimal(value); break;
                    case __.Country : _Country = Convert.ToString(value); break;
                    case __.Province : _Province = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.District : _District = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Postcode : _Postcode = Convert.ToString(value); break;
                    case __.IsDel : _IsDel = Convert.ToInt32(value); break;
                    case __.IsHide : _IsHide = Convert.ToInt32(value); break;
                    case __.IsDisabled : _IsDisabled = Convert.ToInt32(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.Keyword : _Keyword = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.BannerImg : _BannerImg = Convert.ToString(value); break;
                    case __.Balance : _Balance = Convert.ToDecimal(value); break;
                    case __.IsTop : _IsTop = Convert.ToInt32(value); break;
                    case __.IsVip : _IsVip = Convert.ToInt32(value); break;
                    case __.IsRecommend : _IsRecommend = Convert.ToInt32(value); break;
                    case __.Likes : _Likes = Convert.ToInt32(value); break;
                    case __.AvgScore : _AvgScore = Convert.ToDecimal(value); break;
                    case __.ServiceScore : _ServiceScore = Convert.ToDecimal(value); break;
                    case __.SpeedScore : _SpeedScore = Convert.ToDecimal(value); break;
                    case __.EnvironmentScore : _EnvironmentScore = Convert.ToDecimal(value); break;
                    case __.Pic : _Pic = Convert.ToString(value); break;
                    case __.MorePics : _MorePics = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.Tel : _Tel = Convert.ToString(value); break;
                    case __.Phone : _Phone = Convert.ToString(value); break;
                    case __.QQ : _QQ = Convert.ToString(value); break;
                    case __.Skype : _Skype = Convert.ToString(value); break;
                    case __.HomePage : _HomePage = Convert.ToString(value); break;
                    case __.Weixin : _Weixin = Convert.ToString(value); break;
                    case __.IsShip : _IsShip = Convert.ToInt32(value); break;
                    case __.OpenTime : _OpenTime = Convert.ToDateTime(value); break;
                    case __.CloseTime : _CloseTime = Convert.ToDateTime(value); break;
                    case __.ShippingStartTime : _ShippingStartTime = Convert.ToDateTime(value); break;
                    case __.ShippingEndTime : _ShippingEndTime = Convert.ToDateTime(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.Hits : _Hits = Convert.ToInt32(value); break;
                    case __.MyType : _MyType = Convert.ToInt32(value); break;
                    case __.DefaultFare : _DefaultFare = Convert.ToDecimal(value); break;
                    case __.MaxFreeFare : _MaxFreeFare = Convert.ToDecimal(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>店铺名称</summary>
            public static readonly Field ShopName = FindByName(__.ShopName);

            /// <summary>商家分类ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>地区ID</summary>
            public static readonly Field AId = FindByName(__.AId);

            /// <summary>排序</summary>
            public static readonly Field Sequence = FindByName(__.Sequence);

            /// <summary>纬度</summary>
            public static readonly Field Latitude = FindByName(__.Latitude);

            /// <summary>经度</summary>
            public static readonly Field Longitude = FindByName(__.Longitude);

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

            /// <summary>是否删除</summary>
            public static readonly Field IsDel = FindByName(__.IsDel);

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>是否禁用</summary>
            public static readonly Field IsDisabled = FindByName(__.IsDisabled);

            /// <summary>店铺介绍</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>关键字</summary>
            public static readonly Field Keyword = FindByName(__.Keyword);

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName(__.BannerImg);

            /// <summary>余额</summary>
            public static readonly Field Balance = FindByName(__.Balance);

            /// <summary>是否置顶</summary>
            public static readonly Field IsTop = FindByName(__.IsTop);

            /// <summary>是否vip</summary>
            public static readonly Field IsVip = FindByName(__.IsVip);

            /// <summary>是否推荐</summary>
            public static readonly Field IsRecommend = FindByName(__.IsRecommend);

            /// <summary>点赞数</summary>
            public static readonly Field Likes = FindByName(__.Likes);

            /// <summary>平均分数</summary>
            public static readonly Field AvgScore = FindByName(__.AvgScore);

            /// <summary>服务分数</summary>
            public static readonly Field ServiceScore = FindByName(__.ServiceScore);

            /// <summary>速度分数</summary>
            public static readonly Field SpeedScore = FindByName(__.SpeedScore);

            /// <summary>环境分数</summary>
            public static readonly Field EnvironmentScore = FindByName(__.EnvironmentScore);

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName(__.Pic);

            /// <summary>店铺所有图片</summary>
            public static readonly Field MorePics = FindByName(__.MorePics);

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>固定电话</summary>
            public static readonly Field Phone = FindByName(__.Phone);

            /// <summary>QQ</summary>
            public static readonly Field QQ = FindByName(__.QQ);

            /// <summary>Skype</summary>
            public static readonly Field Skype = FindByName(__.Skype);

            /// <summary>主页</summary>
            public static readonly Field HomePage = FindByName(__.HomePage);

            /// <summary>微信</summary>
            public static readonly Field Weixin = FindByName(__.Weixin);

            /// <summary>是否配送</summary>
            public static readonly Field IsShip = FindByName(__.IsShip);

            /// <summary>开店时间</summary>
            public static readonly Field OpenTime = FindByName(__.OpenTime);

            /// <summary>关店时间</summary>
            public static readonly Field CloseTime = FindByName(__.CloseTime);

            /// <summary>配送开始时间</summary>
            public static readonly Field ShippingStartTime = FindByName(__.ShippingStartTime);

            /// <summary>配送结束时间</summary>
            public static readonly Field ShippingEndTime = FindByName(__.ShippingEndTime);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>点击量</summary>
            public static readonly Field Hits = FindByName(__.Hits);

            /// <summary>店铺类型</summary>
            public static readonly Field MyType = FindByName(__.MyType);

            /// <summary>默认运费</summary>
            public static readonly Field DefaultFare = FindByName(__.DefaultFare);

            /// <summary>最大免运费金额</summary>
            public static readonly Field MaxFreeFare = FindByName(__.MaxFreeFare);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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
            public const String QQ = "QQ";

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

    /// <summary>商家接口</summary>
    public partial interface IShop
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>店铺名称</summary>
        String ShopName { get; set; }

        /// <summary>商家分类ID</summary>
        Int32 KId { get; set; }

        /// <summary>地区ID</summary>
        Int32 AId { get; set; }

        /// <summary>排序</summary>
        Int32 Sequence { get; set; }

        /// <summary>纬度</summary>
        Decimal Latitude { get; set; }

        /// <summary>经度</summary>
        Decimal Longitude { get; set; }

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

        /// <summary>是否删除</summary>
        Int32 IsDel { get; set; }

        /// <summary>是否隐藏</summary>
        Int32 IsHide { get; set; }

        /// <summary>是否禁用</summary>
        Int32 IsDisabled { get; set; }

        /// <summary>店铺介绍</summary>
        String Content { get; set; }

        /// <summary>关键字</summary>
        String Keyword { get; set; }

        /// <summary>描述</summary>
        String Description { get; set; }

        /// <summary>banner图片</summary>
        String BannerImg { get; set; }

        /// <summary>余额</summary>
        Decimal Balance { get; set; }

        /// <summary>是否置顶</summary>
        Int32 IsTop { get; set; }

        /// <summary>是否vip</summary>
        Int32 IsVip { get; set; }

        /// <summary>是否推荐</summary>
        Int32 IsRecommend { get; set; }

        /// <summary>点赞数</summary>
        Int32 Likes { get; set; }

        /// <summary>平均分数</summary>
        Decimal AvgScore { get; set; }

        /// <summary>服务分数</summary>
        Decimal ServiceScore { get; set; }

        /// <summary>速度分数</summary>
        Decimal SpeedScore { get; set; }

        /// <summary>环境分数</summary>
        Decimal EnvironmentScore { get; set; }

        /// <summary>图片</summary>
        String Pic { get; set; }

        /// <summary>店铺所有图片</summary>
        String MorePics { get; set; }

        /// <summary>邮箱</summary>
        String Email { get; set; }

        /// <summary>电话</summary>
        String Tel { get; set; }

        /// <summary>固定电话</summary>
        String Phone { get; set; }

        /// <summary>QQ</summary>
        String QQ { get; set; }

        /// <summary>Skype</summary>
        String Skype { get; set; }

        /// <summary>主页</summary>
        String HomePage { get; set; }

        /// <summary>微信</summary>
        String Weixin { get; set; }

        /// <summary>是否配送</summary>
        Int32 IsShip { get; set; }

        /// <summary>开店时间</summary>
        DateTime OpenTime { get; set; }

        /// <summary>关店时间</summary>
        DateTime CloseTime { get; set; }

        /// <summary>配送开始时间</summary>
        DateTime ShippingStartTime { get; set; }

        /// <summary>配送结束时间</summary>
        DateTime ShippingEndTime { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>点击量</summary>
        Int32 Hits { get; set; }

        /// <summary>店铺类型</summary>
        Int32 MyType { get; set; }

        /// <summary>默认运费</summary>
        Decimal DefaultFare { get; set; }

        /// <summary>最大免运费金额</summary>
        Decimal MaxFreeFare { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}