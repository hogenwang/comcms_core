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
    /// <summary>商品</summary>
    [Serializable]
    [DataObject]
    [Description("商品")]
    [BindTable("Product", Description = "商品", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Product
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _KId;
        /// <summary>栏目ID</summary>
        [DisplayName("栏目ID")]
        [Description("栏目ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "栏目ID", "")]
        public Int32 KId { get => _KId; set { if (OnPropertyChanging("KId", value)) { _KId = value; OnPropertyChanged("KId"); } } }

        private Int32 _BId;
        /// <summary>品牌ID</summary>
        [DisplayName("品牌ID")]
        [Description("品牌ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BId", "品牌ID", "")]
        public Int32 BId { get => _BId; set { if (OnPropertyChanging("BId", value)) { _BId = value; OnPropertyChanged("BId"); } } }

        private Int32 _ShopId;
        /// <summary>店铺ID</summary>
        [DisplayName("店铺ID")]
        [Description("店铺ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "店铺ID", "")]
        public Int32 ShopId { get => _ShopId; set { if (OnPropertyChanging("ShopId", value)) { _ShopId = value; OnPropertyChanged("ShopId"); } } }

        private Int32 _CId;
        /// <summary>颜色ID</summary>
        [DisplayName("颜色ID")]
        [Description("颜色ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CId", "颜色ID", "")]
        public Int32 CId { get => _CId; set { if (OnPropertyChanging("CId", value)) { _CId = value; OnPropertyChanged("CId"); } } }

        private Int32 _SupportId;
        /// <summary>供货商ID</summary>
        [DisplayName("供货商ID")]
        [Description("供货商ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SupportId", "供货商ID", "")]
        public Int32 SupportId { get => _SupportId; set { if (OnPropertyChanging("SupportId", value)) { _SupportId = value; OnPropertyChanged("SupportId"); } } }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _ItemNO;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ItemNO", "编号", "")]
        public String ItemNO { get => _ItemNO; set { if (OnPropertyChanging("ItemNO", value)) { _ItemNO = value; OnPropertyChanged("ItemNO"); } } }

        private String _SubTitle;
        /// <summary>副标题</summary>
        [DisplayName("副标题")]
        [Description("副标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SubTitle", "副标题", "")]
        public String SubTitle { get => _SubTitle; set { if (OnPropertyChanging("SubTitle", value)) { _SubTitle = value; OnPropertyChanged("SubTitle"); } } }

        private String _Unit;
        /// <summary>商品单位</summary>
        [DisplayName("商品单位")]
        [Description("商品单位")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Unit", "商品单位", "")]
        public String Unit { get => _Unit; set { if (OnPropertyChanging("Unit", value)) { _Unit = value; OnPropertyChanged("Unit"); } } }

        private String _Spec;
        /// <summary>商品规格尺寸</summary>
        [DisplayName("商品规格尺寸")]
        [Description("商品规格尺寸")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Spec", "商品规格尺寸", "")]
        public String Spec { get => _Spec; set { if (OnPropertyChanging("Spec", value)) { _Spec = value; OnPropertyChanged("Spec"); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Color", "颜色", "")]
        public String Color { get => _Color; set { if (OnPropertyChanging("Color", value)) { _Color = value; OnPropertyChanged("Color"); } } }

        private String _Weight;
        /// <summary>重量</summary>
        [DisplayName("重量")]
        [Description("重量")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weight", "重量", "")]
        public String Weight { get => _Weight; set { if (OnPropertyChanging("Weight", value)) { _Weight = value; OnPropertyChanged("Weight"); } } }

        private Decimal _Price;
        /// <summary>价格</summary>
        [DisplayName("价格")]
        [Description("价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "价格", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging("Price", value)) { _Price = value; OnPropertyChanged("Price"); } } }

        private Decimal _MarketPrice;
        /// <summary>市场价格</summary>
        [DisplayName("市场价格")]
        [Description("市场价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MarketPrice", "市场价格", "")]
        public Decimal MarketPrice { get => _MarketPrice; set { if (OnPropertyChanging("MarketPrice", value)) { _MarketPrice = value; OnPropertyChanged("MarketPrice"); } } }

        private Decimal _SpecialPrice;
        /// <summary>特价，如有特价，以特价为准</summary>
        [DisplayName("特价")]
        [Description("特价，如有特价，以特价为准")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpecialPrice", "特价，如有特价，以特价为准", "")]
        public Decimal SpecialPrice { get => _SpecialPrice; set { if (OnPropertyChanging("SpecialPrice", value)) { _SpecialPrice = value; OnPropertyChanged("SpecialPrice"); } } }

        private Decimal _Fare;
        /// <summary>运费</summary>
        [DisplayName("运费")]
        [Description("运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Fare", "运费", "")]
        public Decimal Fare { get => _Fare; set { if (OnPropertyChanging("Fare", value)) { _Fare = value; OnPropertyChanged("Fare"); } } }

        private Decimal _Discount;
        /// <summary>折扣</summary>
        [DisplayName("折扣")]
        [Description("折扣")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "折扣", "")]
        public Decimal Discount { get => _Discount; set { if (OnPropertyChanging("Discount", value)) { _Discount = value; OnPropertyChanged("Discount"); } } }

        private String _Material;
        /// <summary>材料</summary>
        [DisplayName("材料")]
        [Description("材料")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Material", "材料", "")]
        public String Material { get => _Material; set { if (OnPropertyChanging("Material", value)) { _Material = value; OnPropertyChanged("Material"); } } }

        private String _Front;
        /// <summary>封面</summary>
        [DisplayName("封面")]
        [Description("封面")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Front", "封面", "")]
        public String Front { get => _Front; set { if (OnPropertyChanging("Front", value)) { _Front = value; OnPropertyChanged("Front"); } } }

        private Int32 _Credits;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credits", "积分", "")]
        public Int32 Credits { get => _Credits; set { if (OnPropertyChanging("Credits", value)) { _Credits = value; OnPropertyChanged("Credits"); } } }

        private Int32 _Stock;
        /// <summary>库存</summary>
        [DisplayName("库存")]
        [Description("库存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stock", "库存", "")]
        public Int32 Stock { get => _Stock; set { if (OnPropertyChanging("Stock", value)) { _Stock = value; OnPropertyChanged("Stock"); } } }

        private Int32 _WarnStock;
        /// <summary>警告库存</summary>
        [DisplayName("警告库存")]
        [Description("警告库存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("WarnStock", "警告库存", "")]
        public Int32 WarnStock { get => _WarnStock; set { if (OnPropertyChanging("WarnStock", value)) { _WarnStock = value; OnPropertyChanged("WarnStock"); } } }

        private Int32 _IsSubProduct;
        /// <summary>是否为子商品</summary>
        [DisplayName("是否为子商品")]
        [Description("是否为子商品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSubProduct", "是否为子商品", "")]
        public Int32 IsSubProduct { get => _IsSubProduct; set { if (OnPropertyChanging("IsSubProduct", value)) { _IsSubProduct = value; OnPropertyChanged("IsSubProduct"); } } }

        private Int32 _PPId;
        /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
        [DisplayName("父商品ID")]
        [Description("父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PPId", "父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能", "")]
        public Int32 PPId { get => _PPId; set { if (OnPropertyChanging("PPId", value)) { _PPId = value; OnPropertyChanged("PPId"); } } }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "内容", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

        private String _Parameters;
        /// <summary>商品参数</summary>
        [DisplayName("商品参数")]
        [Description("商品参数")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Parameters", "商品参数", "")]
        public String Parameters { get => _Parameters; set { if (OnPropertyChanging("Parameters", value)) { _Parameters = value; OnPropertyChanged("Parameters"); } } }

        private String _Keyword;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "描述", "")]
        public String Keyword { get => _Keyword; set { if (OnPropertyChanging("Keyword", value)) { _Keyword = value; OnPropertyChanged("Keyword"); } } }

        private String _Description;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "介绍", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

        private String _LinkURL;
        /// <summary>跳转链接</summary>
        [DisplayName("跳转链接")]
        [Description("跳转链接")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkURL", "跳转链接", "")]
        public String LinkURL { get => _LinkURL; set { if (OnPropertyChanging("LinkURL", value)) { _LinkURL = value; OnPropertyChanged("LinkURL"); } } }

        private String _TitleColor;
        /// <summary>类别名称颜色</summary>
        [DisplayName("类别名称颜色")]
        [Description("类别名称颜色")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TitleColor", "类别名称颜色", "")]
        public String TitleColor { get => _TitleColor; set { if (OnPropertyChanging("TitleColor", value)) { _TitleColor = value; OnPropertyChanged("TitleColor"); } } }

        private String _TemplateFile;
        /// <summary>模板</summary>
        [DisplayName("模板")]
        [Description("模板")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("TemplateFile", "模板", "")]
        public String TemplateFile { get => _TemplateFile; set { if (OnPropertyChanging("TemplateFile", value)) { _TemplateFile = value; OnPropertyChanged("TemplateFile"); } } }

        private Int32 _IsRecommend;
        /// <summary>是否推荐</summary>
        [DisplayName("是否推荐")]
        [Description("是否推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRecommend", "是否推荐", "")]
        public Int32 IsRecommend { get => _IsRecommend; set { if (OnPropertyChanging("IsRecommend", value)) { _IsRecommend = value; OnPropertyChanged("IsRecommend"); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "")]
        public Int32 IsHide { get => _IsHide; set { if (OnPropertyChanging("IsHide", value)) { _IsHide = value; OnPropertyChanged("IsHide"); } } }

        private Int32 _IsLock;
        /// <summary>是否锁定，不允许删除</summary>
        [DisplayName("是否锁定")]
        [Description("是否锁定，不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLock", "是否锁定，不允许删除", "")]
        public Int32 IsLock { get => _IsLock; set { if (OnPropertyChanging("IsLock", value)) { _IsLock = value; OnPropertyChanged("IsLock"); } } }

        private Int32 _IsDel;
        /// <summary>是否删除,已经删除到回收站</summary>
        [DisplayName("是否删除")]
        [Description("是否删除,已经删除到回收站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除,已经删除到回收站", "")]
        public Int32 IsDel { get => _IsDel; set { if (OnPropertyChanging("IsDel", value)) { _IsDel = value; OnPropertyChanged("IsDel"); } } }

        private Int32 _IsTop;
        /// <summary>是否置顶</summary>
        [DisplayName("是否置顶")]
        [Description("是否置顶")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsTop", "是否置顶", "")]
        public Int32 IsTop { get => _IsTop; set { if (OnPropertyChanging("IsTop", value)) { _IsTop = value; OnPropertyChanged("IsTop"); } } }

        private Int32 _IsBest;
        /// <summary>是否精华</summary>
        [DisplayName("是否精华")]
        [Description("是否精华")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBest", "是否精华", "")]
        public Int32 IsBest { get => _IsBest; set { if (OnPropertyChanging("IsBest", value)) { _IsBest = value; OnPropertyChanged("IsBest"); } } }

        private Int32 _IsComment;
        /// <summary>是否允许评论</summary>
        [DisplayName("是否允许评论")]
        [Description("是否允许评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "是否允许评论", "")]
        public Int32 IsComment { get => _IsComment; set { if (OnPropertyChanging("IsComment", value)) { _IsComment = value; OnPropertyChanged("IsComment"); } } }

        private Int32 _IsMember;
        /// <summary>是否会员栏目</summary>
        [DisplayName("是否会员栏目")]
        [Description("是否会员栏目")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsMember", "是否会员栏目", "")]
        public Int32 IsMember { get => _IsMember; set { if (OnPropertyChanging("IsMember", value)) { _IsMember = value; OnPropertyChanged("IsMember"); } } }

        private Int32 _IsNew;
        /// <summary>是否新品</summary>
        [DisplayName("是否新品")]
        [Description("是否新品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsNew", "是否新品", "")]
        public Int32 IsNew { get => _IsNew; set { if (OnPropertyChanging("IsNew", value)) { _IsNew = value; OnPropertyChanged("IsNew"); } } }

        private Int32 _IsSpecial;
        /// <summary>是否特价</summary>
        [DisplayName("是否特价")]
        [Description("是否特价")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSpecial", "是否特价", "")]
        public Int32 IsSpecial { get => _IsSpecial; set { if (OnPropertyChanging("IsSpecial", value)) { _IsSpecial = value; OnPropertyChanged("IsSpecial"); } } }

        private Int32 _IsPromote;
        /// <summary>是否促销</summary>
        [DisplayName("是否促销")]
        [Description("是否促销")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsPromote", "是否促销", "")]
        public Int32 IsPromote { get => _IsPromote; set { if (OnPropertyChanging("IsPromote", value)) { _IsPromote = value; OnPropertyChanged("IsPromote"); } } }

        private Int32 _IsHotSales;
        /// <summary>是否热销</summary>
        [DisplayName("是否热销")]
        [Description("是否热销")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHotSales", "是否热销", "")]
        public Int32 IsHotSales { get => _IsHotSales; set { if (OnPropertyChanging("IsHotSales", value)) { _IsHotSales = value; OnPropertyChanged("IsHotSales"); } } }

        private Int32 _IsBreakup;
        /// <summary>是否缺货</summary>
        [DisplayName("是否缺货")]
        [Description("是否缺货")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBreakup", "是否缺货", "")]
        public Int32 IsBreakup { get => _IsBreakup; set { if (OnPropertyChanging("IsBreakup", value)) { _IsBreakup = value; OnPropertyChanged("IsBreakup"); } } }

        private Int32 _IsShelves;
        /// <summary>是否下架</summary>
        [DisplayName("是否下架")]
        [Description("是否下架")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShelves", "是否下架", "")]
        public Int32 IsShelves { get => _IsShelves; set { if (OnPropertyChanging("IsShelves", value)) { _IsShelves = value; OnPropertyChanged("IsShelves"); } } }

        private Int32 _IsVerify;
        /// <summary>是否审核，1为已经审核前台显示</summary>
        [DisplayName("是否审核")]
        [Description("是否审核，1为已经审核前台显示")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否审核，1为已经审核前台显示", "")]
        public Int32 IsVerify { get => _IsVerify; set { if (OnPropertyChanging("IsVerify", value)) { _IsVerify = value; OnPropertyChanged("IsVerify"); } } }

        private Int32 _Hits;
        /// <summary>点击数量</summary>
        [DisplayName("点击数量")]
        [Description("点击数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击数量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging("Hits", value)) { _Hits = value; OnPropertyChanged("Hits"); } } }

        private Int32 _IsGift;
        /// <summary>是否为礼品商品</summary>
        [DisplayName("是否为礼品商品")]
        [Description("是否为礼品商品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsGift", "是否为礼品商品", "")]
        public Int32 IsGift { get => _IsGift; set { if (OnPropertyChanging("IsGift", value)) { _IsGift = value; OnPropertyChanged("IsGift"); } } }

        private Int32 _IsPart;
        /// <summary>是否为配件</summary>
        [DisplayName("是否为配件")]
        [Description("是否为配件")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsPart", "是否为配件", "")]
        public Int32 IsPart { get => _IsPart; set { if (OnPropertyChanging("IsPart", value)) { _IsPart = value; OnPropertyChanged("IsPart"); } } }

        private Int32 _Sequence;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序", "")]
        public Int32 Sequence { get => _Sequence; set { if (OnPropertyChanging("Sequence", value)) { _Sequence = value; OnPropertyChanged("Sequence"); } } }

        private Int32 _CommentCount;
        /// <summary>评论数量</summary>
        [DisplayName("评论数量")]
        [Description("评论数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CommentCount", "评论数量", "")]
        public Int32 CommentCount { get => _CommentCount; set { if (OnPropertyChanging("CommentCount", value)) { _CommentCount = value; OnPropertyChanged("CommentCount"); } } }

        private String _Icon;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Icon", "图标", "")]
        public String Icon { get => _Icon; set { if (OnPropertyChanging("Icon", value)) { _Icon = value; OnPropertyChanged("Icon"); } } }

        private String _ClassName;
        /// <summary>样式名称</summary>
        [DisplayName("样式名称")]
        [Description("样式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ClassName", "样式名称", "")]
        public String ClassName { get => _ClassName; set { if (OnPropertyChanging("ClassName", value)) { _ClassName = value; OnPropertyChanged("ClassName"); } } }

        private String _BannerImg;
        /// <summary>banner图片</summary>
        [DisplayName("banner图片")]
        [Description("banner图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("BannerImg", "banner图片", "")]
        public String BannerImg { get => _BannerImg; set { if (OnPropertyChanging("BannerImg", value)) { _BannerImg = value; OnPropertyChanged("BannerImg"); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get => _Pic; set { if (OnPropertyChanging("Pic", value)) { _Pic = value; OnPropertyChanged("Pic"); } } }

        private Int32 _AdsId;
        /// <summary>广告ID</summary>
        [DisplayName("广告ID")]
        [Description("广告ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AdsId", "广告ID", "")]
        public Int32 AdsId { get => _AdsId; set { if (OnPropertyChanging("AdsId", value)) { _AdsId = value; OnPropertyChanged("AdsId"); } } }

        private String _Tags;
        /// <summary>TAG</summary>
        [DisplayName("TAG")]
        [Description("TAG")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Tags", "TAG", "")]
        public String Tags { get => _Tags; set { if (OnPropertyChanging("Tags", value)) { _Tags = value; OnPropertyChanged("Tags"); } } }

        private String _ItemImg;
        /// <summary>更多图片</summary>
        [DisplayName("更多图片")]
        [Description("更多图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ItemImg", "更多图片", "")]
        public String ItemImg { get => _ItemImg; set { if (OnPropertyChanging("ItemImg", value)) { _ItemImg = value; OnPropertyChanged("ItemImg"); } } }

        private String _Service;
        /// <summary>售后服务</summary>
        [DisplayName("售后服务")]
        [Description("售后服务")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Service", "售后服务", "")]
        public String Service { get => _Service; set { if (OnPropertyChanging("Service", value)) { _Service = value; OnPropertyChanged("Service"); } } }

        private Int32 _AuthorId;
        /// <summary>添加管理员ID</summary>
        [DisplayName("添加管理员ID")]
        [Description("添加管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AuthorId", "添加管理员ID", "")]
        public Int32 AuthorId { get => _AuthorId; set { if (OnPropertyChanging("AuthorId", value)) { _AuthorId = value; OnPropertyChanged("AuthorId"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _UpdateTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "")]
        public String Location { get => _Location; set { if (OnPropertyChanging("Location", value)) { _Location = value; OnPropertyChanged("Location"); } } }

        private String _FilePath;
        /// <summary>存放目录</summary>
        [DisplayName("存放目录")]
        [Description("存放目录")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("FilePath", "存放目录", "")]
        public String FilePath { get => _FilePath; set { if (OnPropertyChanging("FilePath", value)) { _FilePath = value; OnPropertyChanged("FilePath"); } } }

        private String _FileName;
        /// <summary>文件名称</summary>
        [DisplayName("文件名称")]
        [Description("文件名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FileName", "文件名称", "")]
        public String FileName { get => _FileName; set { if (OnPropertyChanging("FileName", value)) { _FileName = value; OnPropertyChanged("FileName"); } } }
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
                    case "KId": return _KId;
                    case "BId": return _BId;
                    case "ShopId": return _ShopId;
                    case "CId": return _CId;
                    case "SupportId": return _SupportId;
                    case "Title": return _Title;
                    case "ItemNO": return _ItemNO;
                    case "SubTitle": return _SubTitle;
                    case "Unit": return _Unit;
                    case "Spec": return _Spec;
                    case "Color": return _Color;
                    case "Weight": return _Weight;
                    case "Price": return _Price;
                    case "MarketPrice": return _MarketPrice;
                    case "SpecialPrice": return _SpecialPrice;
                    case "Fare": return _Fare;
                    case "Discount": return _Discount;
                    case "Material": return _Material;
                    case "Front": return _Front;
                    case "Credits": return _Credits;
                    case "Stock": return _Stock;
                    case "WarnStock": return _WarnStock;
                    case "IsSubProduct": return _IsSubProduct;
                    case "PPId": return _PPId;
                    case "Content": return _Content;
                    case "Parameters": return _Parameters;
                    case "Keyword": return _Keyword;
                    case "Description": return _Description;
                    case "LinkURL": return _LinkURL;
                    case "TitleColor": return _TitleColor;
                    case "TemplateFile": return _TemplateFile;
                    case "IsRecommend": return _IsRecommend;
                    case "IsHide": return _IsHide;
                    case "IsLock": return _IsLock;
                    case "IsDel": return _IsDel;
                    case "IsTop": return _IsTop;
                    case "IsBest": return _IsBest;
                    case "IsComment": return _IsComment;
                    case "IsMember": return _IsMember;
                    case "IsNew": return _IsNew;
                    case "IsSpecial": return _IsSpecial;
                    case "IsPromote": return _IsPromote;
                    case "IsHotSales": return _IsHotSales;
                    case "IsBreakup": return _IsBreakup;
                    case "IsShelves": return _IsShelves;
                    case "IsVerify": return _IsVerify;
                    case "Hits": return _Hits;
                    case "IsGift": return _IsGift;
                    case "IsPart": return _IsPart;
                    case "Sequence": return _Sequence;
                    case "CommentCount": return _CommentCount;
                    case "Icon": return _Icon;
                    case "ClassName": return _ClassName;
                    case "BannerImg": return _BannerImg;
                    case "Pic": return _Pic;
                    case "AdsId": return _AdsId;
                    case "Tags": return _Tags;
                    case "ItemImg": return _ItemImg;
                    case "Service": return _Service;
                    case "AuthorId": return _AuthorId;
                    case "AddTime": return _AddTime;
                    case "UpdateTime": return _UpdateTime;
                    case "Location": return _Location;
                    case "FilePath": return _FilePath;
                    case "FileName": return _FileName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "KId": _KId = value.ToInt(); break;
                    case "BId": _BId = value.ToInt(); break;
                    case "ShopId": _ShopId = value.ToInt(); break;
                    case "CId": _CId = value.ToInt(); break;
                    case "SupportId": _SupportId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "ItemNO": _ItemNO = Convert.ToString(value); break;
                    case "SubTitle": _SubTitle = Convert.ToString(value); break;
                    case "Unit": _Unit = Convert.ToString(value); break;
                    case "Spec": _Spec = Convert.ToString(value); break;
                    case "Color": _Color = Convert.ToString(value); break;
                    case "Weight": _Weight = Convert.ToString(value); break;
                    case "Price": _Price = Convert.ToDecimal(value); break;
                    case "MarketPrice": _MarketPrice = Convert.ToDecimal(value); break;
                    case "SpecialPrice": _SpecialPrice = Convert.ToDecimal(value); break;
                    case "Fare": _Fare = Convert.ToDecimal(value); break;
                    case "Discount": _Discount = Convert.ToDecimal(value); break;
                    case "Material": _Material = Convert.ToString(value); break;
                    case "Front": _Front = Convert.ToString(value); break;
                    case "Credits": _Credits = value.ToInt(); break;
                    case "Stock": _Stock = value.ToInt(); break;
                    case "WarnStock": _WarnStock = value.ToInt(); break;
                    case "IsSubProduct": _IsSubProduct = value.ToInt(); break;
                    case "PPId": _PPId = value.ToInt(); break;
                    case "Content": _Content = Convert.ToString(value); break;
                    case "Parameters": _Parameters = Convert.ToString(value); break;
                    case "Keyword": _Keyword = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "LinkURL": _LinkURL = Convert.ToString(value); break;
                    case "TitleColor": _TitleColor = Convert.ToString(value); break;
                    case "TemplateFile": _TemplateFile = Convert.ToString(value); break;
                    case "IsRecommend": _IsRecommend = value.ToInt(); break;
                    case "IsHide": _IsHide = value.ToInt(); break;
                    case "IsLock": _IsLock = value.ToInt(); break;
                    case "IsDel": _IsDel = value.ToInt(); break;
                    case "IsTop": _IsTop = value.ToInt(); break;
                    case "IsBest": _IsBest = value.ToInt(); break;
                    case "IsComment": _IsComment = value.ToInt(); break;
                    case "IsMember": _IsMember = value.ToInt(); break;
                    case "IsNew": _IsNew = value.ToInt(); break;
                    case "IsSpecial": _IsSpecial = value.ToInt(); break;
                    case "IsPromote": _IsPromote = value.ToInt(); break;
                    case "IsHotSales": _IsHotSales = value.ToInt(); break;
                    case "IsBreakup": _IsBreakup = value.ToInt(); break;
                    case "IsShelves": _IsShelves = value.ToInt(); break;
                    case "IsVerify": _IsVerify = value.ToInt(); break;
                    case "Hits": _Hits = value.ToInt(); break;
                    case "IsGift": _IsGift = value.ToInt(); break;
                    case "IsPart": _IsPart = value.ToInt(); break;
                    case "Sequence": _Sequence = value.ToInt(); break;
                    case "CommentCount": _CommentCount = value.ToInt(); break;
                    case "Icon": _Icon = Convert.ToString(value); break;
                    case "ClassName": _ClassName = Convert.ToString(value); break;
                    case "BannerImg": _BannerImg = Convert.ToString(value); break;
                    case "Pic": _Pic = Convert.ToString(value); break;
                    case "AdsId": _AdsId = value.ToInt(); break;
                    case "Tags": _Tags = Convert.ToString(value); break;
                    case "ItemImg": _ItemImg = Convert.ToString(value); break;
                    case "Service": _Service = Convert.ToString(value); break;
                    case "AuthorId": _AuthorId = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    case "Location": _Location = Convert.ToString(value); break;
                    case "FilePath": _FilePath = Convert.ToString(value); break;
                    case "FileName": _FileName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得商品字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>栏目ID</summary>
            public static readonly Field KId = FindByName("KId");

            /// <summary>品牌ID</summary>
            public static readonly Field BId = FindByName("BId");

            /// <summary>店铺ID</summary>
            public static readonly Field ShopId = FindByName("ShopId");

            /// <summary>颜色ID</summary>
            public static readonly Field CId = FindByName("CId");

            /// <summary>供货商ID</summary>
            public static readonly Field SupportId = FindByName("SupportId");

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>编号</summary>
            public static readonly Field ItemNO = FindByName("ItemNO");

            /// <summary>副标题</summary>
            public static readonly Field SubTitle = FindByName("SubTitle");

            /// <summary>商品单位</summary>
            public static readonly Field Unit = FindByName("Unit");

            /// <summary>商品规格尺寸</summary>
            public static readonly Field Spec = FindByName("Spec");

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName("Color");

            /// <summary>重量</summary>
            public static readonly Field Weight = FindByName("Weight");

            /// <summary>价格</summary>
            public static readonly Field Price = FindByName("Price");

            /// <summary>市场价格</summary>
            public static readonly Field MarketPrice = FindByName("MarketPrice");

            /// <summary>特价，如有特价，以特价为准</summary>
            public static readonly Field SpecialPrice = FindByName("SpecialPrice");

            /// <summary>运费</summary>
            public static readonly Field Fare = FindByName("Fare");

            /// <summary>折扣</summary>
            public static readonly Field Discount = FindByName("Discount");

            /// <summary>材料</summary>
            public static readonly Field Material = FindByName("Material");

            /// <summary>封面</summary>
            public static readonly Field Front = FindByName("Front");

            /// <summary>积分</summary>
            public static readonly Field Credits = FindByName("Credits");

            /// <summary>库存</summary>
            public static readonly Field Stock = FindByName("Stock");

            /// <summary>警告库存</summary>
            public static readonly Field WarnStock = FindByName("WarnStock");

            /// <summary>是否为子商品</summary>
            public static readonly Field IsSubProduct = FindByName("IsSubProduct");

            /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
            public static readonly Field PPId = FindByName("PPId");

            /// <summary>内容</summary>
            public static readonly Field Content = FindByName("Content");

            /// <summary>商品参数</summary>
            public static readonly Field Parameters = FindByName("Parameters");

            /// <summary>描述</summary>
            public static readonly Field Keyword = FindByName("Keyword");

            /// <summary>介绍</summary>
            public static readonly Field Description = FindByName("Description");

            /// <summary>跳转链接</summary>
            public static readonly Field LinkURL = FindByName("LinkURL");

            /// <summary>类别名称颜色</summary>
            public static readonly Field TitleColor = FindByName("TitleColor");

            /// <summary>模板</summary>
            public static readonly Field TemplateFile = FindByName("TemplateFile");

            /// <summary>是否推荐</summary>
            public static readonly Field IsRecommend = FindByName("IsRecommend");

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName("IsHide");

            /// <summary>是否锁定，不允许删除</summary>
            public static readonly Field IsLock = FindByName("IsLock");

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName("IsDel");

            /// <summary>是否置顶</summary>
            public static readonly Field IsTop = FindByName("IsTop");

            /// <summary>是否精华</summary>
            public static readonly Field IsBest = FindByName("IsBest");

            /// <summary>是否允许评论</summary>
            public static readonly Field IsComment = FindByName("IsComment");

            /// <summary>是否会员栏目</summary>
            public static readonly Field IsMember = FindByName("IsMember");

            /// <summary>是否新品</summary>
            public static readonly Field IsNew = FindByName("IsNew");

            /// <summary>是否特价</summary>
            public static readonly Field IsSpecial = FindByName("IsSpecial");

            /// <summary>是否促销</summary>
            public static readonly Field IsPromote = FindByName("IsPromote");

            /// <summary>是否热销</summary>
            public static readonly Field IsHotSales = FindByName("IsHotSales");

            /// <summary>是否缺货</summary>
            public static readonly Field IsBreakup = FindByName("IsBreakup");

            /// <summary>是否下架</summary>
            public static readonly Field IsShelves = FindByName("IsShelves");

            /// <summary>是否审核，1为已经审核前台显示</summary>
            public static readonly Field IsVerify = FindByName("IsVerify");

            /// <summary>点击数量</summary>
            public static readonly Field Hits = FindByName("Hits");

            /// <summary>是否为礼品商品</summary>
            public static readonly Field IsGift = FindByName("IsGift");

            /// <summary>是否为配件</summary>
            public static readonly Field IsPart = FindByName("IsPart");

            /// <summary>排序</summary>
            public static readonly Field Sequence = FindByName("Sequence");

            /// <summary>评论数量</summary>
            public static readonly Field CommentCount = FindByName("CommentCount");

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName("Icon");

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName("ClassName");

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName("BannerImg");

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName("Pic");

            /// <summary>广告ID</summary>
            public static readonly Field AdsId = FindByName("AdsId");

            /// <summary>TAG</summary>
            public static readonly Field Tags = FindByName("Tags");

            /// <summary>更多图片</summary>
            public static readonly Field ItemImg = FindByName("ItemImg");

            /// <summary>售后服务</summary>
            public static readonly Field Service = FindByName("Service");

            /// <summary>添加管理员ID</summary>
            public static readonly Field AuthorId = FindByName("AuthorId");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName("Location");

            /// <summary>存放目录</summary>
            public static readonly Field FilePath = FindByName("FilePath");

            /// <summary>文件名称</summary>
            public static readonly Field FileName = FindByName("FileName");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得商品字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>栏目ID</summary>
            public const String KId = "KId";

            /// <summary>品牌ID</summary>
            public const String BId = "BId";

            /// <summary>店铺ID</summary>
            public const String ShopId = "ShopId";

            /// <summary>颜色ID</summary>
            public const String CId = "CId";

            /// <summary>供货商ID</summary>
            public const String SupportId = "SupportId";

            /// <summary>标题</summary>
            public const String Title = "Title";

            /// <summary>编号</summary>
            public const String ItemNO = "ItemNO";

            /// <summary>副标题</summary>
            public const String SubTitle = "SubTitle";

            /// <summary>商品单位</summary>
            public const String Unit = "Unit";

            /// <summary>商品规格尺寸</summary>
            public const String Spec = "Spec";

            /// <summary>颜色</summary>
            public const String Color = "Color";

            /// <summary>重量</summary>
            public const String Weight = "Weight";

            /// <summary>价格</summary>
            public const String Price = "Price";

            /// <summary>市场价格</summary>
            public const String MarketPrice = "MarketPrice";

            /// <summary>特价，如有特价，以特价为准</summary>
            public const String SpecialPrice = "SpecialPrice";

            /// <summary>运费</summary>
            public const String Fare = "Fare";

            /// <summary>折扣</summary>
            public const String Discount = "Discount";

            /// <summary>材料</summary>
            public const String Material = "Material";

            /// <summary>封面</summary>
            public const String Front = "Front";

            /// <summary>积分</summary>
            public const String Credits = "Credits";

            /// <summary>库存</summary>
            public const String Stock = "Stock";

            /// <summary>警告库存</summary>
            public const String WarnStock = "WarnStock";

            /// <summary>是否为子商品</summary>
            public const String IsSubProduct = "IsSubProduct";

            /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
            public const String PPId = "PPId";

            /// <summary>内容</summary>
            public const String Content = "Content";

            /// <summary>商品参数</summary>
            public const String Parameters = "Parameters";

            /// <summary>描述</summary>
            public const String Keyword = "Keyword";

            /// <summary>介绍</summary>
            public const String Description = "Description";

            /// <summary>跳转链接</summary>
            public const String LinkURL = "LinkURL";

            /// <summary>类别名称颜色</summary>
            public const String TitleColor = "TitleColor";

            /// <summary>模板</summary>
            public const String TemplateFile = "TemplateFile";

            /// <summary>是否推荐</summary>
            public const String IsRecommend = "IsRecommend";

            /// <summary>是否隐藏</summary>
            public const String IsHide = "IsHide";

            /// <summary>是否锁定，不允许删除</summary>
            public const String IsLock = "IsLock";

            /// <summary>是否删除,已经删除到回收站</summary>
            public const String IsDel = "IsDel";

            /// <summary>是否置顶</summary>
            public const String IsTop = "IsTop";

            /// <summary>是否精华</summary>
            public const String IsBest = "IsBest";

            /// <summary>是否允许评论</summary>
            public const String IsComment = "IsComment";

            /// <summary>是否会员栏目</summary>
            public const String IsMember = "IsMember";

            /// <summary>是否新品</summary>
            public const String IsNew = "IsNew";

            /// <summary>是否特价</summary>
            public const String IsSpecial = "IsSpecial";

            /// <summary>是否促销</summary>
            public const String IsPromote = "IsPromote";

            /// <summary>是否热销</summary>
            public const String IsHotSales = "IsHotSales";

            /// <summary>是否缺货</summary>
            public const String IsBreakup = "IsBreakup";

            /// <summary>是否下架</summary>
            public const String IsShelves = "IsShelves";

            /// <summary>是否审核，1为已经审核前台显示</summary>
            public const String IsVerify = "IsVerify";

            /// <summary>点击数量</summary>
            public const String Hits = "Hits";

            /// <summary>是否为礼品商品</summary>
            public const String IsGift = "IsGift";

            /// <summary>是否为配件</summary>
            public const String IsPart = "IsPart";

            /// <summary>排序</summary>
            public const String Sequence = "Sequence";

            /// <summary>评论数量</summary>
            public const String CommentCount = "CommentCount";

            /// <summary>图标</summary>
            public const String Icon = "Icon";

            /// <summary>样式名称</summary>
            public const String ClassName = "ClassName";

            /// <summary>banner图片</summary>
            public const String BannerImg = "BannerImg";

            /// <summary>图片</summary>
            public const String Pic = "Pic";

            /// <summary>广告ID</summary>
            public const String AdsId = "AdsId";

            /// <summary>TAG</summary>
            public const String Tags = "Tags";

            /// <summary>更多图片</summary>
            public const String ItemImg = "ItemImg";

            /// <summary>售后服务</summary>
            public const String Service = "Service";

            /// <summary>添加管理员ID</summary>
            public const String AuthorId = "AuthorId";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>路径</summary>
            public const String Location = "Location";

            /// <summary>存放目录</summary>
            public const String FilePath = "FilePath";

            /// <summary>文件名称</summary>
            public const String FileName = "FileName";
        }
        #endregion
    }
}