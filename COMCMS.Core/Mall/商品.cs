using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Product : IProduct
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _KId;
        /// <summary>栏目ID</summary>
        [DisplayName("栏目ID")]
        [Description("栏目ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "栏目ID", "")]
        public Int32 KId { get => _KId; set { if (OnPropertyChanging(__.KId, value)) { _KId = value; OnPropertyChanged(__.KId); } } }

        private Int32 _BId;
        /// <summary>品牌ID</summary>
        [DisplayName("品牌ID")]
        [Description("品牌ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("BId", "品牌ID", "")]
        public Int32 BId { get => _BId; set { if (OnPropertyChanging(__.BId, value)) { _BId = value; OnPropertyChanged(__.BId); } } }

        private Int32 _ShopId;
        /// <summary>店铺ID</summary>
        [DisplayName("店铺ID")]
        [Description("店铺ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ShopId", "店铺ID", "")]
        public Int32 ShopId { get => _ShopId; set { if (OnPropertyChanging(__.ShopId, value)) { _ShopId = value; OnPropertyChanged(__.ShopId); } } }

        private Int32 _CId;
        /// <summary>颜色ID</summary>
        [DisplayName("颜色ID")]
        [Description("颜色ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CId", "颜色ID", "")]
        public Int32 CId { get => _CId; set { if (OnPropertyChanging(__.CId, value)) { _CId = value; OnPropertyChanged(__.CId); } } }

        private Int32 _SupportId;
        /// <summary>供货商ID</summary>
        [DisplayName("供货商ID")]
        [Description("供货商ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SupportId", "供货商ID", "")]
        public Int32 SupportId { get => _SupportId; set { if (OnPropertyChanging(__.SupportId, value)) { _SupportId = value; OnPropertyChanged(__.SupportId); } } }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _ItemNO;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ItemNO", "编号", "")]
        public String ItemNO { get => _ItemNO; set { if (OnPropertyChanging(__.ItemNO, value)) { _ItemNO = value; OnPropertyChanged(__.ItemNO); } } }

        private String _SubTitle;
        /// <summary>副标题</summary>
        [DisplayName("副标题")]
        [Description("副标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SubTitle", "副标题", "")]
        public String SubTitle { get => _SubTitle; set { if (OnPropertyChanging(__.SubTitle, value)) { _SubTitle = value; OnPropertyChanged(__.SubTitle); } } }

        private String _Unit;
        /// <summary>商品单位</summary>
        [DisplayName("商品单位")]
        [Description("商品单位")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Unit", "商品单位", "")]
        public String Unit { get => _Unit; set { if (OnPropertyChanging(__.Unit, value)) { _Unit = value; OnPropertyChanged(__.Unit); } } }

        private String _Spec;
        /// <summary>商品规格尺寸</summary>
        [DisplayName("商品规格尺寸")]
        [Description("商品规格尺寸")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Spec", "商品规格尺寸", "")]
        public String Spec { get => _Spec; set { if (OnPropertyChanging(__.Spec, value)) { _Spec = value; OnPropertyChanged(__.Spec); } } }

        private String _Color;
        /// <summary>颜色</summary>
        [DisplayName("颜色")]
        [Description("颜色")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Color", "颜色", "")]
        public String Color { get => _Color; set { if (OnPropertyChanging(__.Color, value)) { _Color = value; OnPropertyChanged(__.Color); } } }

        private String _Weight;
        /// <summary>重量</summary>
        [DisplayName("重量")]
        [Description("重量")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weight", "重量", "")]
        public String Weight { get => _Weight; set { if (OnPropertyChanging(__.Weight, value)) { _Weight = value; OnPropertyChanged(__.Weight); } } }

        private Decimal _Price;
        /// <summary>价格</summary>
        [DisplayName("价格")]
        [Description("价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Price", "价格", "")]
        public Decimal Price { get => _Price; set { if (OnPropertyChanging(__.Price, value)) { _Price = value; OnPropertyChanged(__.Price); } } }

        private Decimal _MarketPrice;
        /// <summary>市场价格</summary>
        [DisplayName("市场价格")]
        [Description("市场价格")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MarketPrice", "市场价格", "")]
        public Decimal MarketPrice { get => _MarketPrice; set { if (OnPropertyChanging(__.MarketPrice, value)) { _MarketPrice = value; OnPropertyChanged(__.MarketPrice); } } }

        private Decimal _SpecialPrice;
        /// <summary>特价，如有特价，以特价为准</summary>
        [DisplayName("特价")]
        [Description("特价，如有特价，以特价为准")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SpecialPrice", "特价，如有特价，以特价为准", "")]
        public Decimal SpecialPrice { get => _SpecialPrice; set { if (OnPropertyChanging(__.SpecialPrice, value)) { _SpecialPrice = value; OnPropertyChanged(__.SpecialPrice); } } }

        private Decimal _Fare;
        /// <summary>运费</summary>
        [DisplayName("运费")]
        [Description("运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Fare", "运费", "")]
        public Decimal Fare { get => _Fare; set { if (OnPropertyChanging(__.Fare, value)) { _Fare = value; OnPropertyChanged(__.Fare); } } }

        private Decimal _Discount;
        /// <summary>折扣</summary>
        [DisplayName("折扣")]
        [Description("折扣")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Discount", "折扣", "")]
        public Decimal Discount { get => _Discount; set { if (OnPropertyChanging(__.Discount, value)) { _Discount = value; OnPropertyChanged(__.Discount); } } }

        private String _Material;
        /// <summary>材料</summary>
        [DisplayName("材料")]
        [Description("材料")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Material", "材料", "")]
        public String Material { get => _Material; set { if (OnPropertyChanging(__.Material, value)) { _Material = value; OnPropertyChanged(__.Material); } } }

        private String _Front;
        /// <summary>封面</summary>
        [DisplayName("封面")]
        [Description("封面")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Front", "封面", "")]
        public String Front { get => _Front; set { if (OnPropertyChanging(__.Front, value)) { _Front = value; OnPropertyChanged(__.Front); } } }

        private Int32 _Credits;
        /// <summary>积分</summary>
        [DisplayName("积分")]
        [Description("积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Credits", "积分", "")]
        public Int32 Credits { get => _Credits; set { if (OnPropertyChanging(__.Credits, value)) { _Credits = value; OnPropertyChanged(__.Credits); } } }

        private Int32 _Stock;
        /// <summary>库存</summary>
        [DisplayName("库存")]
        [Description("库存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Stock", "库存", "")]
        public Int32 Stock { get => _Stock; set { if (OnPropertyChanging(__.Stock, value)) { _Stock = value; OnPropertyChanged(__.Stock); } } }

        private Int32 _WarnStock;
        /// <summary>警告库存</summary>
        [DisplayName("警告库存")]
        [Description("警告库存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("WarnStock", "警告库存", "")]
        public Int32 WarnStock { get => _WarnStock; set { if (OnPropertyChanging(__.WarnStock, value)) { _WarnStock = value; OnPropertyChanged(__.WarnStock); } } }

        private Int32 _IsSubProduct;
        /// <summary>是否为子商品</summary>
        [DisplayName("是否为子商品")]
        [Description("是否为子商品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSubProduct", "是否为子商品", "")]
        public Int32 IsSubProduct { get => _IsSubProduct; set { if (OnPropertyChanging(__.IsSubProduct, value)) { _IsSubProduct = value; OnPropertyChanged(__.IsSubProduct); } } }

        private Int32 _PPId;
        /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
        [DisplayName("父商品ID")]
        [Description("父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PPId", "父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能", "")]
        public Int32 PPId { get => _PPId; set { if (OnPropertyChanging(__.PPId, value)) { _PPId = value; OnPropertyChanged(__.PPId); } } }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "内容", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private String _Parameters;
        /// <summary>商品参数</summary>
        [DisplayName("商品参数")]
        [Description("商品参数")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Parameters", "商品参数", "")]
        public String Parameters { get => _Parameters; set { if (OnPropertyChanging(__.Parameters, value)) { _Parameters = value; OnPropertyChanged(__.Parameters); } } }

        private String _Keyword;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "描述", "")]
        public String Keyword { get => _Keyword; set { if (OnPropertyChanging(__.Keyword, value)) { _Keyword = value; OnPropertyChanged(__.Keyword); } } }

        private String _Description;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "介绍", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private String _LinkURL;
        /// <summary>跳转链接</summary>
        [DisplayName("跳转链接")]
        [Description("跳转链接")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkURL", "跳转链接", "")]
        public String LinkURL { get => _LinkURL; set { if (OnPropertyChanging(__.LinkURL, value)) { _LinkURL = value; OnPropertyChanged(__.LinkURL); } } }

        private String _TitleColor;
        /// <summary>类别名称颜色</summary>
        [DisplayName("类别名称颜色")]
        [Description("类别名称颜色")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TitleColor", "类别名称颜色", "")]
        public String TitleColor { get => _TitleColor; set { if (OnPropertyChanging(__.TitleColor, value)) { _TitleColor = value; OnPropertyChanged(__.TitleColor); } } }

        private String _TemplateFile;
        /// <summary>模板</summary>
        [DisplayName("模板")]
        [Description("模板")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("TemplateFile", "模板", "")]
        public String TemplateFile { get => _TemplateFile; set { if (OnPropertyChanging(__.TemplateFile, value)) { _TemplateFile = value; OnPropertyChanged(__.TemplateFile); } } }

        private Int32 _IsRecommend;
        /// <summary>是否推荐</summary>
        [DisplayName("是否推荐")]
        [Description("是否推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRecommend", "是否推荐", "")]
        public Int32 IsRecommend { get => _IsRecommend; set { if (OnPropertyChanging(__.IsRecommend, value)) { _IsRecommend = value; OnPropertyChanged(__.IsRecommend); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "")]
        public Int32 IsHide { get => _IsHide; set { if (OnPropertyChanging(__.IsHide, value)) { _IsHide = value; OnPropertyChanged(__.IsHide); } } }

        private Int32 _IsLock;
        /// <summary>是否锁定，不允许删除</summary>
        [DisplayName("是否锁定")]
        [Description("是否锁定，不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLock", "是否锁定，不允许删除", "")]
        public Int32 IsLock { get => _IsLock; set { if (OnPropertyChanging(__.IsLock, value)) { _IsLock = value; OnPropertyChanged(__.IsLock); } } }

        private Int32 _IsDel;
        /// <summary>是否删除,已经删除到回收站</summary>
        [DisplayName("是否删除")]
        [Description("是否删除,已经删除到回收站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除,已经删除到回收站", "")]
        public Int32 IsDel { get => _IsDel; set { if (OnPropertyChanging(__.IsDel, value)) { _IsDel = value; OnPropertyChanged(__.IsDel); } } }

        private Int32 _IsTop;
        /// <summary>是否置顶</summary>
        [DisplayName("是否置顶")]
        [Description("是否置顶")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsTop", "是否置顶", "")]
        public Int32 IsTop { get => _IsTop; set { if (OnPropertyChanging(__.IsTop, value)) { _IsTop = value; OnPropertyChanged(__.IsTop); } } }

        private Int32 _IsBest;
        /// <summary>是否精华</summary>
        [DisplayName("是否精华")]
        [Description("是否精华")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBest", "是否精华", "")]
        public Int32 IsBest { get => _IsBest; set { if (OnPropertyChanging(__.IsBest, value)) { _IsBest = value; OnPropertyChanged(__.IsBest); } } }

        private Int32 _IsComment;
        /// <summary>是否允许评论</summary>
        [DisplayName("是否允许评论")]
        [Description("是否允许评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "是否允许评论", "")]
        public Int32 IsComment { get => _IsComment; set { if (OnPropertyChanging(__.IsComment, value)) { _IsComment = value; OnPropertyChanged(__.IsComment); } } }

        private Int32 _IsMember;
        /// <summary>是否会员栏目</summary>
        [DisplayName("是否会员栏目")]
        [Description("是否会员栏目")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsMember", "是否会员栏目", "")]
        public Int32 IsMember { get => _IsMember; set { if (OnPropertyChanging(__.IsMember, value)) { _IsMember = value; OnPropertyChanged(__.IsMember); } } }

        private Int32 _IsNew;
        /// <summary>是否新品</summary>
        [DisplayName("是否新品")]
        [Description("是否新品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsNew", "是否新品", "")]
        public Int32 IsNew { get => _IsNew; set { if (OnPropertyChanging(__.IsNew, value)) { _IsNew = value; OnPropertyChanged(__.IsNew); } } }

        private Int32 _IsSpecial;
        /// <summary>是否特价</summary>
        [DisplayName("是否特价")]
        [Description("是否特价")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSpecial", "是否特价", "")]
        public Int32 IsSpecial { get => _IsSpecial; set { if (OnPropertyChanging(__.IsSpecial, value)) { _IsSpecial = value; OnPropertyChanged(__.IsSpecial); } } }

        private Int32 _IsPromote;
        /// <summary>是否促销</summary>
        [DisplayName("是否促销")]
        [Description("是否促销")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsPromote", "是否促销", "")]
        public Int32 IsPromote { get => _IsPromote; set { if (OnPropertyChanging(__.IsPromote, value)) { _IsPromote = value; OnPropertyChanged(__.IsPromote); } } }

        private Int32 _IsHotSales;
        /// <summary>是否热销</summary>
        [DisplayName("是否热销")]
        [Description("是否热销")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHotSales", "是否热销", "")]
        public Int32 IsHotSales { get => _IsHotSales; set { if (OnPropertyChanging(__.IsHotSales, value)) { _IsHotSales = value; OnPropertyChanged(__.IsHotSales); } } }

        private Int32 _IsBreakup;
        /// <summary>是否缺货</summary>
        [DisplayName("是否缺货")]
        [Description("是否缺货")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBreakup", "是否缺货", "")]
        public Int32 IsBreakup { get => _IsBreakup; set { if (OnPropertyChanging(__.IsBreakup, value)) { _IsBreakup = value; OnPropertyChanged(__.IsBreakup); } } }

        private Int32 _IsShelves;
        /// <summary>是否下架</summary>
        [DisplayName("是否下架")]
        [Description("是否下架")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShelves", "是否下架", "")]
        public Int32 IsShelves { get => _IsShelves; set { if (OnPropertyChanging(__.IsShelves, value)) { _IsShelves = value; OnPropertyChanged(__.IsShelves); } } }

        private Int32 _IsVerify;
        /// <summary>是否审核，1为已经审核前台显示</summary>
        [DisplayName("是否审核")]
        [Description("是否审核，1为已经审核前台显示")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否审核，1为已经审核前台显示", "")]
        public Int32 IsVerify { get => _IsVerify; set { if (OnPropertyChanging(__.IsVerify, value)) { _IsVerify = value; OnPropertyChanged(__.IsVerify); } } }

        private Int32 _Hits;
        /// <summary>点击数量</summary>
        [DisplayName("点击数量")]
        [Description("点击数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击数量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging(__.Hits, value)) { _Hits = value; OnPropertyChanged(__.Hits); } } }

        private Int32 _IsGift;
        /// <summary>是否为礼品商品</summary>
        [DisplayName("是否为礼品商品")]
        [Description("是否为礼品商品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsGift", "是否为礼品商品", "")]
        public Int32 IsGift { get => _IsGift; set { if (OnPropertyChanging(__.IsGift, value)) { _IsGift = value; OnPropertyChanged(__.IsGift); } } }

        private Int32 _IsPart;
        /// <summary>是否为配件</summary>
        [DisplayName("是否为配件")]
        [Description("是否为配件")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsPart", "是否为配件", "")]
        public Int32 IsPart { get => _IsPart; set { if (OnPropertyChanging(__.IsPart, value)) { _IsPart = value; OnPropertyChanged(__.IsPart); } } }

        private Int32 _Sequence;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序", "")]
        public Int32 Sequence { get => _Sequence; set { if (OnPropertyChanging(__.Sequence, value)) { _Sequence = value; OnPropertyChanged(__.Sequence); } } }

        private Int32 _CommentCount;
        /// <summary>评论数量</summary>
        [DisplayName("评论数量")]
        [Description("评论数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CommentCount", "评论数量", "")]
        public Int32 CommentCount { get => _CommentCount; set { if (OnPropertyChanging(__.CommentCount, value)) { _CommentCount = value; OnPropertyChanged(__.CommentCount); } } }

        private String _Icon;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Icon", "图标", "")]
        public String Icon { get => _Icon; set { if (OnPropertyChanging(__.Icon, value)) { _Icon = value; OnPropertyChanged(__.Icon); } } }

        private String _ClassName;
        /// <summary>样式名称</summary>
        [DisplayName("样式名称")]
        [Description("样式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ClassName", "样式名称", "")]
        public String ClassName { get => _ClassName; set { if (OnPropertyChanging(__.ClassName, value)) { _ClassName = value; OnPropertyChanged(__.ClassName); } } }

        private String _BannerImg;
        /// <summary>banner图片</summary>
        [DisplayName("banner图片")]
        [Description("banner图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("BannerImg", "banner图片", "")]
        public String BannerImg { get => _BannerImg; set { if (OnPropertyChanging(__.BannerImg, value)) { _BannerImg = value; OnPropertyChanged(__.BannerImg); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get => _Pic; set { if (OnPropertyChanging(__.Pic, value)) { _Pic = value; OnPropertyChanged(__.Pic); } } }

        private Int32 _AdsId;
        /// <summary>广告ID</summary>
        [DisplayName("广告ID")]
        [Description("广告ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AdsId", "广告ID", "")]
        public Int32 AdsId { get => _AdsId; set { if (OnPropertyChanging(__.AdsId, value)) { _AdsId = value; OnPropertyChanged(__.AdsId); } } }

        private String _Tags;
        /// <summary>TAG</summary>
        [DisplayName("TAG")]
        [Description("TAG")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Tags", "TAG", "")]
        public String Tags { get => _Tags; set { if (OnPropertyChanging(__.Tags, value)) { _Tags = value; OnPropertyChanged(__.Tags); } } }

        private String _ItemImg;
        /// <summary>更多图片</summary>
        [DisplayName("更多图片")]
        [Description("更多图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ItemImg", "更多图片", "")]
        public String ItemImg { get => _ItemImg; set { if (OnPropertyChanging(__.ItemImg, value)) { _ItemImg = value; OnPropertyChanged(__.ItemImg); } } }

        private String _Service;
        /// <summary>售后服务</summary>
        [DisplayName("售后服务")]
        [Description("售后服务")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Service", "售后服务", "")]
        public String Service { get => _Service; set { if (OnPropertyChanging(__.Service, value)) { _Service = value; OnPropertyChanged(__.Service); } } }

        private Int32 _AuthorId;
        /// <summary>添加管理员ID</summary>
        [DisplayName("添加管理员ID")]
        [Description("添加管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AuthorId", "添加管理员ID", "")]
        public Int32 AuthorId { get => _AuthorId; set { if (OnPropertyChanging(__.AuthorId, value)) { _AuthorId = value; OnPropertyChanged(__.AuthorId); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _UpdateTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "")]
        public String Location { get => _Location; set { if (OnPropertyChanging(__.Location, value)) { _Location = value; OnPropertyChanged(__.Location); } } }

        private String _FilePath;
        /// <summary>存放目录</summary>
        [DisplayName("存放目录")]
        [Description("存放目录")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("FilePath", "存放目录", "")]
        public String FilePath { get => _FilePath; set { if (OnPropertyChanging(__.FilePath, value)) { _FilePath = value; OnPropertyChanged(__.FilePath); } } }

        private String _FileName;
        /// <summary>文件名称</summary>
        [DisplayName("文件名称")]
        [Description("文件名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FileName", "文件名称", "")]
        public String FileName { get => _FileName; set { if (OnPropertyChanging(__.FileName, value)) { _FileName = value; OnPropertyChanged(__.FileName); } } }
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
                    case __.Id: return _Id;
                    case __.KId: return _KId;
                    case __.BId: return _BId;
                    case __.ShopId: return _ShopId;
                    case __.CId: return _CId;
                    case __.SupportId: return _SupportId;
                    case __.Title: return _Title;
                    case __.ItemNO: return _ItemNO;
                    case __.SubTitle: return _SubTitle;
                    case __.Unit: return _Unit;
                    case __.Spec: return _Spec;
                    case __.Color: return _Color;
                    case __.Weight: return _Weight;
                    case __.Price: return _Price;
                    case __.MarketPrice: return _MarketPrice;
                    case __.SpecialPrice: return _SpecialPrice;
                    case __.Fare: return _Fare;
                    case __.Discount: return _Discount;
                    case __.Material: return _Material;
                    case __.Front: return _Front;
                    case __.Credits: return _Credits;
                    case __.Stock: return _Stock;
                    case __.WarnStock: return _WarnStock;
                    case __.IsSubProduct: return _IsSubProduct;
                    case __.PPId: return _PPId;
                    case __.Content: return _Content;
                    case __.Parameters: return _Parameters;
                    case __.Keyword: return _Keyword;
                    case __.Description: return _Description;
                    case __.LinkURL: return _LinkURL;
                    case __.TitleColor: return _TitleColor;
                    case __.TemplateFile: return _TemplateFile;
                    case __.IsRecommend: return _IsRecommend;
                    case __.IsHide: return _IsHide;
                    case __.IsLock: return _IsLock;
                    case __.IsDel: return _IsDel;
                    case __.IsTop: return _IsTop;
                    case __.IsBest: return _IsBest;
                    case __.IsComment: return _IsComment;
                    case __.IsMember: return _IsMember;
                    case __.IsNew: return _IsNew;
                    case __.IsSpecial: return _IsSpecial;
                    case __.IsPromote: return _IsPromote;
                    case __.IsHotSales: return _IsHotSales;
                    case __.IsBreakup: return _IsBreakup;
                    case __.IsShelves: return _IsShelves;
                    case __.IsVerify: return _IsVerify;
                    case __.Hits: return _Hits;
                    case __.IsGift: return _IsGift;
                    case __.IsPart: return _IsPart;
                    case __.Sequence: return _Sequence;
                    case __.CommentCount: return _CommentCount;
                    case __.Icon: return _Icon;
                    case __.ClassName: return _ClassName;
                    case __.BannerImg: return _BannerImg;
                    case __.Pic: return _Pic;
                    case __.AdsId: return _AdsId;
                    case __.Tags: return _Tags;
                    case __.ItemImg: return _ItemImg;
                    case __.Service: return _Service;
                    case __.AuthorId: return _AuthorId;
                    case __.AddTime: return _AddTime;
                    case __.UpdateTime: return _UpdateTime;
                    case __.Location: return _Location;
                    case __.FilePath: return _FilePath;
                    case __.FileName: return _FileName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id: _Id = value.ToInt(); break;
                    case __.KId: _KId = value.ToInt(); break;
                    case __.BId: _BId = value.ToInt(); break;
                    case __.ShopId: _ShopId = value.ToInt(); break;
                    case __.CId: _CId = value.ToInt(); break;
                    case __.SupportId: _SupportId = value.ToInt(); break;
                    case __.Title: _Title = Convert.ToString(value); break;
                    case __.ItemNO: _ItemNO = Convert.ToString(value); break;
                    case __.SubTitle: _SubTitle = Convert.ToString(value); break;
                    case __.Unit: _Unit = Convert.ToString(value); break;
                    case __.Spec: _Spec = Convert.ToString(value); break;
                    case __.Color: _Color = Convert.ToString(value); break;
                    case __.Weight: _Weight = Convert.ToString(value); break;
                    case __.Price: _Price = Convert.ToDecimal(value); break;
                    case __.MarketPrice: _MarketPrice = Convert.ToDecimal(value); break;
                    case __.SpecialPrice: _SpecialPrice = Convert.ToDecimal(value); break;
                    case __.Fare: _Fare = Convert.ToDecimal(value); break;
                    case __.Discount: _Discount = Convert.ToDecimal(value); break;
                    case __.Material: _Material = Convert.ToString(value); break;
                    case __.Front: _Front = Convert.ToString(value); break;
                    case __.Credits: _Credits = value.ToInt(); break;
                    case __.Stock: _Stock = value.ToInt(); break;
                    case __.WarnStock: _WarnStock = value.ToInt(); break;
                    case __.IsSubProduct: _IsSubProduct = value.ToInt(); break;
                    case __.PPId: _PPId = value.ToInt(); break;
                    case __.Content: _Content = Convert.ToString(value); break;
                    case __.Parameters: _Parameters = Convert.ToString(value); break;
                    case __.Keyword: _Keyword = Convert.ToString(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.LinkURL: _LinkURL = Convert.ToString(value); break;
                    case __.TitleColor: _TitleColor = Convert.ToString(value); break;
                    case __.TemplateFile: _TemplateFile = Convert.ToString(value); break;
                    case __.IsRecommend: _IsRecommend = value.ToInt(); break;
                    case __.IsHide: _IsHide = value.ToInt(); break;
                    case __.IsLock: _IsLock = value.ToInt(); break;
                    case __.IsDel: _IsDel = value.ToInt(); break;
                    case __.IsTop: _IsTop = value.ToInt(); break;
                    case __.IsBest: _IsBest = value.ToInt(); break;
                    case __.IsComment: _IsComment = value.ToInt(); break;
                    case __.IsMember: _IsMember = value.ToInt(); break;
                    case __.IsNew: _IsNew = value.ToInt(); break;
                    case __.IsSpecial: _IsSpecial = value.ToInt(); break;
                    case __.IsPromote: _IsPromote = value.ToInt(); break;
                    case __.IsHotSales: _IsHotSales = value.ToInt(); break;
                    case __.IsBreakup: _IsBreakup = value.ToInt(); break;
                    case __.IsShelves: _IsShelves = value.ToInt(); break;
                    case __.IsVerify: _IsVerify = value.ToInt(); break;
                    case __.Hits: _Hits = value.ToInt(); break;
                    case __.IsGift: _IsGift = value.ToInt(); break;
                    case __.IsPart: _IsPart = value.ToInt(); break;
                    case __.Sequence: _Sequence = value.ToInt(); break;
                    case __.CommentCount: _CommentCount = value.ToInt(); break;
                    case __.Icon: _Icon = Convert.ToString(value); break;
                    case __.ClassName: _ClassName = Convert.ToString(value); break;
                    case __.BannerImg: _BannerImg = Convert.ToString(value); break;
                    case __.Pic: _Pic = Convert.ToString(value); break;
                    case __.AdsId: _AdsId = value.ToInt(); break;
                    case __.Tags: _Tags = Convert.ToString(value); break;
                    case __.ItemImg: _ItemImg = Convert.ToString(value); break;
                    case __.Service: _Service = Convert.ToString(value); break;
                    case __.AuthorId: _AuthorId = value.ToInt(); break;
                    case __.AddTime: _AddTime = value.ToDateTime(); break;
                    case __.UpdateTime: _UpdateTime = value.ToDateTime(); break;
                    case __.Location: _Location = Convert.ToString(value); break;
                    case __.FilePath: _FilePath = Convert.ToString(value); break;
                    case __.FileName: _FileName = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>栏目ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>品牌ID</summary>
            public static readonly Field BId = FindByName(__.BId);

            /// <summary>店铺ID</summary>
            public static readonly Field ShopId = FindByName(__.ShopId);

            /// <summary>颜色ID</summary>
            public static readonly Field CId = FindByName(__.CId);

            /// <summary>供货商ID</summary>
            public static readonly Field SupportId = FindByName(__.SupportId);

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>编号</summary>
            public static readonly Field ItemNO = FindByName(__.ItemNO);

            /// <summary>副标题</summary>
            public static readonly Field SubTitle = FindByName(__.SubTitle);

            /// <summary>商品单位</summary>
            public static readonly Field Unit = FindByName(__.Unit);

            /// <summary>商品规格尺寸</summary>
            public static readonly Field Spec = FindByName(__.Spec);

            /// <summary>颜色</summary>
            public static readonly Field Color = FindByName(__.Color);

            /// <summary>重量</summary>
            public static readonly Field Weight = FindByName(__.Weight);

            /// <summary>价格</summary>
            public static readonly Field Price = FindByName(__.Price);

            /// <summary>市场价格</summary>
            public static readonly Field MarketPrice = FindByName(__.MarketPrice);

            /// <summary>特价，如有特价，以特价为准</summary>
            public static readonly Field SpecialPrice = FindByName(__.SpecialPrice);

            /// <summary>运费</summary>
            public static readonly Field Fare = FindByName(__.Fare);

            /// <summary>折扣</summary>
            public static readonly Field Discount = FindByName(__.Discount);

            /// <summary>材料</summary>
            public static readonly Field Material = FindByName(__.Material);

            /// <summary>封面</summary>
            public static readonly Field Front = FindByName(__.Front);

            /// <summary>积分</summary>
            public static readonly Field Credits = FindByName(__.Credits);

            /// <summary>库存</summary>
            public static readonly Field Stock = FindByName(__.Stock);

            /// <summary>警告库存</summary>
            public static readonly Field WarnStock = FindByName(__.WarnStock);

            /// <summary>是否为子商品</summary>
            public static readonly Field IsSubProduct = FindByName(__.IsSubProduct);

            /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
            public static readonly Field PPId = FindByName(__.PPId);

            /// <summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>商品参数</summary>
            public static readonly Field Parameters = FindByName(__.Parameters);

            /// <summary>描述</summary>
            public static readonly Field Keyword = FindByName(__.Keyword);

            /// <summary>介绍</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>跳转链接</summary>
            public static readonly Field LinkURL = FindByName(__.LinkURL);

            /// <summary>类别名称颜色</summary>
            public static readonly Field TitleColor = FindByName(__.TitleColor);

            /// <summary>模板</summary>
            public static readonly Field TemplateFile = FindByName(__.TemplateFile);

            /// <summary>是否推荐</summary>
            public static readonly Field IsRecommend = FindByName(__.IsRecommend);

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>是否锁定，不允许删除</summary>
            public static readonly Field IsLock = FindByName(__.IsLock);

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName(__.IsDel);

            /// <summary>是否置顶</summary>
            public static readonly Field IsTop = FindByName(__.IsTop);

            /// <summary>是否精华</summary>
            public static readonly Field IsBest = FindByName(__.IsBest);

            /// <summary>是否允许评论</summary>
            public static readonly Field IsComment = FindByName(__.IsComment);

            /// <summary>是否会员栏目</summary>
            public static readonly Field IsMember = FindByName(__.IsMember);

            /// <summary>是否新品</summary>
            public static readonly Field IsNew = FindByName(__.IsNew);

            /// <summary>是否特价</summary>
            public static readonly Field IsSpecial = FindByName(__.IsSpecial);

            /// <summary>是否促销</summary>
            public static readonly Field IsPromote = FindByName(__.IsPromote);

            /// <summary>是否热销</summary>
            public static readonly Field IsHotSales = FindByName(__.IsHotSales);

            /// <summary>是否缺货</summary>
            public static readonly Field IsBreakup = FindByName(__.IsBreakup);

            /// <summary>是否下架</summary>
            public static readonly Field IsShelves = FindByName(__.IsShelves);

            /// <summary>是否审核，1为已经审核前台显示</summary>
            public static readonly Field IsVerify = FindByName(__.IsVerify);

            /// <summary>点击数量</summary>
            public static readonly Field Hits = FindByName(__.Hits);

            /// <summary>是否为礼品商品</summary>
            public static readonly Field IsGift = FindByName(__.IsGift);

            /// <summary>是否为配件</summary>
            public static readonly Field IsPart = FindByName(__.IsPart);

            /// <summary>排序</summary>
            public static readonly Field Sequence = FindByName(__.Sequence);

            /// <summary>评论数量</summary>
            public static readonly Field CommentCount = FindByName(__.CommentCount);

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName(__.Icon);

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName(__.ClassName);

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName(__.BannerImg);

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName(__.Pic);

            /// <summary>广告ID</summary>
            public static readonly Field AdsId = FindByName(__.AdsId);

            /// <summary>TAG</summary>
            public static readonly Field Tags = FindByName(__.Tags);

            /// <summary>更多图片</summary>
            public static readonly Field ItemImg = FindByName(__.ItemImg);

            /// <summary>售后服务</summary>
            public static readonly Field Service = FindByName(__.Service);

            /// <summary>添加管理员ID</summary>
            public static readonly Field AuthorId = FindByName(__.AuthorId);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName(__.Location);

            /// <summary>存放目录</summary>
            public static readonly Field FilePath = FindByName(__.FilePath);

            /// <summary>文件名称</summary>
            public static readonly Field FileName = FindByName(__.FileName);

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

    /// <summary>商品接口</summary>
    public partial interface IProduct
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>栏目ID</summary>
        Int32 KId { get; set; }

        /// <summary>品牌ID</summary>
        Int32 BId { get; set; }

        /// <summary>店铺ID</summary>
        Int32 ShopId { get; set; }

        /// <summary>颜色ID</summary>
        Int32 CId { get; set; }

        /// <summary>供货商ID</summary>
        Int32 SupportId { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>编号</summary>
        String ItemNO { get; set; }

        /// <summary>副标题</summary>
        String SubTitle { get; set; }

        /// <summary>商品单位</summary>
        String Unit { get; set; }

        /// <summary>商品规格尺寸</summary>
        String Spec { get; set; }

        /// <summary>颜色</summary>
        String Color { get; set; }

        /// <summary>重量</summary>
        String Weight { get; set; }

        /// <summary>价格</summary>
        Decimal Price { get; set; }

        /// <summary>市场价格</summary>
        Decimal MarketPrice { get; set; }

        /// <summary>特价，如有特价，以特价为准</summary>
        Decimal SpecialPrice { get; set; }

        /// <summary>运费</summary>
        Decimal Fare { get; set; }

        /// <summary>折扣</summary>
        Decimal Discount { get; set; }

        /// <summary>材料</summary>
        String Material { get; set; }

        /// <summary>封面</summary>
        String Front { get; set; }

        /// <summary>积分</summary>
        Int32 Credits { get; set; }

        /// <summary>库存</summary>
        Int32 Stock { get; set; }

        /// <summary>警告库存</summary>
        Int32 WarnStock { get; set; }

        /// <summary>是否为子商品</summary>
        Int32 IsSubProduct { get; set; }

        /// <summary>父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能</summary>
        Int32 PPId { get; set; }

        /// <summary>内容</summary>
        String Content { get; set; }

        /// <summary>商品参数</summary>
        String Parameters { get; set; }

        /// <summary>描述</summary>
        String Keyword { get; set; }

        /// <summary>介绍</summary>
        String Description { get; set; }

        /// <summary>跳转链接</summary>
        String LinkURL { get; set; }

        /// <summary>类别名称颜色</summary>
        String TitleColor { get; set; }

        /// <summary>模板</summary>
        String TemplateFile { get; set; }

        /// <summary>是否推荐</summary>
        Int32 IsRecommend { get; set; }

        /// <summary>是否隐藏</summary>
        Int32 IsHide { get; set; }

        /// <summary>是否锁定，不允许删除</summary>
        Int32 IsLock { get; set; }

        /// <summary>是否删除,已经删除到回收站</summary>
        Int32 IsDel { get; set; }

        /// <summary>是否置顶</summary>
        Int32 IsTop { get; set; }

        /// <summary>是否精华</summary>
        Int32 IsBest { get; set; }

        /// <summary>是否允许评论</summary>
        Int32 IsComment { get; set; }

        /// <summary>是否会员栏目</summary>
        Int32 IsMember { get; set; }

        /// <summary>是否新品</summary>
        Int32 IsNew { get; set; }

        /// <summary>是否特价</summary>
        Int32 IsSpecial { get; set; }

        /// <summary>是否促销</summary>
        Int32 IsPromote { get; set; }

        /// <summary>是否热销</summary>
        Int32 IsHotSales { get; set; }

        /// <summary>是否缺货</summary>
        Int32 IsBreakup { get; set; }

        /// <summary>是否下架</summary>
        Int32 IsShelves { get; set; }

        /// <summary>是否审核，1为已经审核前台显示</summary>
        Int32 IsVerify { get; set; }

        /// <summary>点击数量</summary>
        Int32 Hits { get; set; }

        /// <summary>是否为礼品商品</summary>
        Int32 IsGift { get; set; }

        /// <summary>是否为配件</summary>
        Int32 IsPart { get; set; }

        /// <summary>排序</summary>
        Int32 Sequence { get; set; }

        /// <summary>评论数量</summary>
        Int32 CommentCount { get; set; }

        /// <summary>图标</summary>
        String Icon { get; set; }

        /// <summary>样式名称</summary>
        String ClassName { get; set; }

        /// <summary>banner图片</summary>
        String BannerImg { get; set; }

        /// <summary>图片</summary>
        String Pic { get; set; }

        /// <summary>广告ID</summary>
        Int32 AdsId { get; set; }

        /// <summary>TAG</summary>
        String Tags { get; set; }

        /// <summary>更多图片</summary>
        String ItemImg { get; set; }

        /// <summary>售后服务</summary>
        String Service { get; set; }

        /// <summary>添加管理员ID</summary>
        Int32 AuthorId { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>路径</summary>
        String Location { get; set; }

        /// <summary>存放目录</summary>
        String FilePath { get; set; }

        /// <summary>文件名称</summary>
        String FileName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}