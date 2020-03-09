using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>文章栏目</summary>
    [Serializable]
    [DataObject]
    [Description("文章栏目")]
    [BindTable("ArticleCategory", Description = "文章栏目", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class ArticleCategory : IArticleCategory
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _KindName;
        /// <summary>栏目名称</summary>
        [DisplayName("栏目名称")]
        [Description("栏目名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindName", "栏目名称", "")]
        public String KindName { get { return _KindName; } set { if (OnPropertyChanging(__.KindName, value)) { _KindName = value; OnPropertyChanged(__.KindName); } } }

        private String _SubTitle;
        /// <summary>栏目副标题</summary>
        [DisplayName("栏目副标题")]
        [Description("栏目副标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SubTitle", "栏目副标题", "")]
        public String SubTitle { get { return _SubTitle; } set { if (OnPropertyChanging(__.SubTitle, value)) { _SubTitle = value; OnPropertyChanged(__.SubTitle); } } }

        private String _KindTitle;
        /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
        [DisplayName("栏目标题")]
        [Description("栏目标题，填写则在浏览器替换此标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindTitle", "栏目标题，填写则在浏览器替换此标题", "")]
        public String KindTitle { get { return _KindTitle; } set { if (OnPropertyChanging(__.KindTitle, value)) { _KindTitle = value; OnPropertyChanged(__.KindTitle); } } }

        private String _Keyword;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "描述", "")]
        public String Keyword { get { return _Keyword; } set { if (OnPropertyChanging(__.Keyword, value)) { _Keyword = value; OnPropertyChanged(__.Keyword); } } }

        private String _Description;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "介绍", "")]
        public String Description { get { return _Description; } set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private String _LinkURL;
        /// <summary>跳转链接</summary>
        [DisplayName("跳转链接")]
        [Description("跳转链接")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkURL", "跳转链接", "")]
        public String LinkURL { get { return _LinkURL; } set { if (OnPropertyChanging(__.LinkURL, value)) { _LinkURL = value; OnPropertyChanged(__.LinkURL); } } }

        private String _TitleColor;
        /// <summary>类别名称颜色</summary>
        [DisplayName("类别名称颜色")]
        [Description("类别名称颜色")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TitleColor", "类别名称颜色", "")]
        public String TitleColor { get { return _TitleColor; } set { if (OnPropertyChanging(__.TitleColor, value)) { _TitleColor = value; OnPropertyChanged(__.TitleColor); } } }

        private String _TemplateFile;
        /// <summary>模板</summary>
        [DisplayName("模板")]
        [Description("模板")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("TemplateFile", "模板", "")]
        public String TemplateFile { get { return _TemplateFile; } set { if (OnPropertyChanging(__.TemplateFile, value)) { _TemplateFile = value; OnPropertyChanged(__.TemplateFile); } } }

        private String _DetailTemplateFile;
        /// <summary>详情模板</summary>
        [DisplayName("详情模板")]
        [Description("详情模板")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("DetailTemplateFile", "详情模板", "")]
        public String DetailTemplateFile { get { return _DetailTemplateFile; } set { if (OnPropertyChanging(__.DetailTemplateFile, value)) { _DetailTemplateFile = value; OnPropertyChanged(__.DetailTemplateFile); } } }

        private String _KindDomain;
        /// <summary>类别域名（保留）</summary>
        [DisplayName("类别域名")]
        [Description("类别域名（保留）")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindDomain", "类别域名（保留）", "")]
        public String KindDomain { get { return _KindDomain; } set { if (OnPropertyChanging(__.KindDomain, value)) { _KindDomain = value; OnPropertyChanged(__.KindDomain); } } }

        private Int32 _IsList;
        /// <summary>是否为列表页面</summary>
        [DisplayName("是否为列表页面")]
        [Description("是否为列表页面")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsList", "是否为列表页面", "")]
        public Int32 IsList { get { return _IsList; } set { if (OnPropertyChanging(__.IsList, value)) { _IsList = value; OnPropertyChanged(__.IsList); } } }

        private Int32 _PageSize;
        /// <summary>每页显示数量</summary>
        [DisplayName("每页显示数量")]
        [Description("每页显示数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PageSize", "每页显示数量", "")]
        public Int32 PageSize { get { return _PageSize; } set { if (OnPropertyChanging(__.PageSize, value)) { _PageSize = value; OnPropertyChanged(__.PageSize); } } }

        private Int32 _PId;
        /// <summary>上级ID</summary>
        [DisplayName("上级ID")]
        [Description("上级ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "上级ID", "")]
        public Int32 PId { get { return _PId; } set { if (OnPropertyChanging(__.PId, value)) { _PId = value; OnPropertyChanged(__.PId); } } }

        private Int32 _Level;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Level", "级别", "")]
        public Int32 Level { get { return _Level; } set { if (OnPropertyChanging(__.Level, value)) { _Level = value; OnPropertyChanged(__.Level); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "")]
        public String Location { get { return _Location; } set { if (OnPropertyChanging(__.Location, value)) { _Location = value; OnPropertyChanged(__.Location); } } }

        private Int32 _IsHide;
        /// <summary>是否隐藏</summary>
        [DisplayName("是否隐藏")]
        [Description("是否隐藏")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏", "")]
        public Int32 IsHide { get { return _IsHide; } set { if (OnPropertyChanging(__.IsHide, value)) { _IsHide = value; OnPropertyChanged(__.IsHide); } } }

        private Int32 _IsLock;
        /// <summary>是否锁定，不允许删除</summary>
        [DisplayName("是否锁定")]
        [Description("是否锁定，不允许删除")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLock", "是否锁定，不允许删除", "")]
        public Int32 IsLock { get { return _IsLock; } set { if (OnPropertyChanging(__.IsLock, value)) { _IsLock = value; OnPropertyChanged(__.IsLock); } } }

        private Int32 _IsDel;
        /// <summary>是否删除,已经删除到回收站</summary>
        [DisplayName("是否删除")]
        [Description("是否删除,已经删除到回收站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除,已经删除到回收站", "")]
        public Int32 IsDel { get { return _IsDel; } set { if (OnPropertyChanging(__.IsDel, value)) { _IsDel = value; OnPropertyChanged(__.IsDel); } } }

        private Int32 _IsComment;
        /// <summary>是否允许评论</summary>
        [DisplayName("是否允许评论")]
        [Description("是否允许评论")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsComment", "是否允许评论", "")]
        public Int32 IsComment { get { return _IsComment; } set { if (OnPropertyChanging(__.IsComment, value)) { _IsComment = value; OnPropertyChanged(__.IsComment); } } }

        private Int32 _IsMember;
        /// <summary>是否会员栏目</summary>
        [DisplayName("是否会员栏目")]
        [Description("是否会员栏目")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsMember", "是否会员栏目", "")]
        public Int32 IsMember { get { return _IsMember; } set { if (OnPropertyChanging(__.IsMember, value)) { _IsMember = value; OnPropertyChanged(__.IsMember); } } }

        private Int32 _IsShowSubDetail;
        /// <summary>是否显示下级栏目内容</summary>
        [DisplayName("是否显示下级栏目内容")]
        [Description("是否显示下级栏目内容")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShowSubDetail", "是否显示下级栏目内容", "")]
        public Int32 IsShowSubDetail { get { return _IsShowSubDetail; } set { if (OnPropertyChanging(__.IsShowSubDetail, value)) { _IsShowSubDetail = value; OnPropertyChanged(__.IsShowSubDetail); } } }

        private Int32 _CatalogId;
        /// <summary>模型ID</summary>
        [DisplayName("模型ID")]
        [Description("模型ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CatalogId", "模型ID", "")]
        public Int32 CatalogId { get { return _CatalogId; } set { if (OnPropertyChanging(__.CatalogId, value)) { _CatalogId = value; OnPropertyChanged(__.CatalogId); } } }

        private Int32 _Counts;
        /// <summary>详情数量，缓存</summary>
        [DisplayName("详情数量")]
        [Description("详情数量，缓存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Counts", "详情数量，缓存", "")]
        public Int32 Counts { get { return _Counts; } set { if (OnPropertyChanging(__.Counts, value)) { _Counts = value; OnPropertyChanged(__.Counts); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private String _Icon;
        /// <summary>图标</summary>
        [DisplayName("图标")]
        [Description("图标")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Icon", "图标", "")]
        public String Icon { get { return _Icon; } set { if (OnPropertyChanging(__.Icon, value)) { _Icon = value; OnPropertyChanged(__.Icon); } } }

        private String _ClassName;
        /// <summary>样式名称</summary>
        [DisplayName("样式名称")]
        [Description("样式名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ClassName", "样式名称", "")]
        public String ClassName { get { return _ClassName; } set { if (OnPropertyChanging(__.ClassName, value)) { _ClassName = value; OnPropertyChanged(__.ClassName); } } }

        private String _BannerImg;
        /// <summary>banner图片</summary>
        [DisplayName("banner图片")]
        [Description("banner图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("BannerImg", "banner图片", "")]
        public String BannerImg { get { return _BannerImg; } set { if (OnPropertyChanging(__.BannerImg, value)) { _BannerImg = value; OnPropertyChanged(__.BannerImg); } } }

        private String _KindInfo;
        /// <summary>栏目详细介绍</summary>
        [DisplayName("栏目详细介绍")]
        [Description("栏目详细介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("KindInfo", "栏目详细介绍", "")]
        public String KindInfo { get { return _KindInfo; } set { if (OnPropertyChanging(__.KindInfo, value)) { _KindInfo = value; OnPropertyChanged(__.KindInfo); } } }

        private String _Pic;
        /// <summary>图片</summary>
        [DisplayName("图片")]
        [Description("图片")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Pic", "图片", "")]
        public String Pic { get { return _Pic; } set { if (OnPropertyChanging(__.Pic, value)) { _Pic = value; OnPropertyChanged(__.Pic); } } }

        private Int32 _AdsId;
        /// <summary>广告ID</summary>
        [DisplayName("广告ID")]
        [Description("广告ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AdsId", "广告ID", "")]
        public Int32 AdsId { get { return _AdsId; } set { if (OnPropertyChanging(__.AdsId, value)) { _AdsId = value; OnPropertyChanged(__.AdsId); } } }

        private String _FilePath;
        /// <summary>目录路径</summary>
        [DisplayName("目录路径")]
        [Description("目录路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("FilePath", "目录路径", "")]
        public String FilePath { get { return _FilePath; } set { if (OnPropertyChanging(__.FilePath, value)) { _FilePath = value; OnPropertyChanged(__.FilePath); } } }
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
                    case __.KindName : return _KindName;
                    case __.SubTitle : return _SubTitle;
                    case __.KindTitle : return _KindTitle;
                    case __.Keyword : return _Keyword;
                    case __.Description : return _Description;
                    case __.LinkURL : return _LinkURL;
                    case __.TitleColor : return _TitleColor;
                    case __.TemplateFile : return _TemplateFile;
                    case __.DetailTemplateFile : return _DetailTemplateFile;
                    case __.KindDomain : return _KindDomain;
                    case __.IsList : return _IsList;
                    case __.PageSize : return _PageSize;
                    case __.PId : return _PId;
                    case __.Level : return _Level;
                    case __.Location : return _Location;
                    case __.IsHide : return _IsHide;
                    case __.IsLock : return _IsLock;
                    case __.IsDel : return _IsDel;
                    case __.IsComment : return _IsComment;
                    case __.IsMember : return _IsMember;
                    case __.IsShowSubDetail : return _IsShowSubDetail;
                    case __.CatalogId : return _CatalogId;
                    case __.Counts : return _Counts;
                    case __.Rank : return _Rank;
                    case __.Icon : return _Icon;
                    case __.ClassName : return _ClassName;
                    case __.BannerImg : return _BannerImg;
                    case __.KindInfo : return _KindInfo;
                    case __.Pic : return _Pic;
                    case __.AdsId : return _AdsId;
                    case __.FilePath : return _FilePath;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = value.ToInt(); break;
                    case __.KindName : _KindName = Convert.ToString(value); break;
                    case __.SubTitle : _SubTitle = Convert.ToString(value); break;
                    case __.KindTitle : _KindTitle = Convert.ToString(value); break;
                    case __.Keyword : _Keyword = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.LinkURL : _LinkURL = Convert.ToString(value); break;
                    case __.TitleColor : _TitleColor = Convert.ToString(value); break;
                    case __.TemplateFile : _TemplateFile = Convert.ToString(value); break;
                    case __.DetailTemplateFile : _DetailTemplateFile = Convert.ToString(value); break;
                    case __.KindDomain : _KindDomain = Convert.ToString(value); break;
                    case __.IsList : _IsList = value.ToInt(); break;
                    case __.PageSize : _PageSize = value.ToInt(); break;
                    case __.PId : _PId = value.ToInt(); break;
                    case __.Level : _Level = value.ToInt(); break;
                    case __.Location : _Location = Convert.ToString(value); break;
                    case __.IsHide : _IsHide = value.ToInt(); break;
                    case __.IsLock : _IsLock = value.ToInt(); break;
                    case __.IsDel : _IsDel = value.ToInt(); break;
                    case __.IsComment : _IsComment = value.ToInt(); break;
                    case __.IsMember : _IsMember = value.ToInt(); break;
                    case __.IsShowSubDetail : _IsShowSubDetail = value.ToInt(); break;
                    case __.CatalogId : _CatalogId = value.ToInt(); break;
                    case __.Counts : _Counts = value.ToInt(); break;
                    case __.Rank : _Rank = value.ToInt(); break;
                    case __.Icon : _Icon = Convert.ToString(value); break;
                    case __.ClassName : _ClassName = Convert.ToString(value); break;
                    case __.BannerImg : _BannerImg = Convert.ToString(value); break;
                    case __.KindInfo : _KindInfo = Convert.ToString(value); break;
                    case __.Pic : _Pic = Convert.ToString(value); break;
                    case __.AdsId : _AdsId = value.ToInt(); break;
                    case __.FilePath : _FilePath = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得文章栏目字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>栏目名称</summary>
            public static readonly Field KindName = FindByName(__.KindName);

            /// <summary>栏目副标题</summary>
            public static readonly Field SubTitle = FindByName(__.SubTitle);

            /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
            public static readonly Field KindTitle = FindByName(__.KindTitle);

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

            /// <summary>详情模板</summary>
            public static readonly Field DetailTemplateFile = FindByName(__.DetailTemplateFile);

            /// <summary>类别域名（保留）</summary>
            public static readonly Field KindDomain = FindByName(__.KindDomain);

            /// <summary>是否为列表页面</summary>
            public static readonly Field IsList = FindByName(__.IsList);

            /// <summary>每页显示数量</summary>
            public static readonly Field PageSize = FindByName(__.PageSize);

            /// <summary>上级ID</summary>
            public static readonly Field PId = FindByName(__.PId);

            /// <summary>级别</summary>
            public static readonly Field Level = FindByName(__.Level);

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName(__.Location);

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>是否锁定，不允许删除</summary>
            public static readonly Field IsLock = FindByName(__.IsLock);

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName(__.IsDel);

            /// <summary>是否允许评论</summary>
            public static readonly Field IsComment = FindByName(__.IsComment);

            /// <summary>是否会员栏目</summary>
            public static readonly Field IsMember = FindByName(__.IsMember);

            /// <summary>是否显示下级栏目内容</summary>
            public static readonly Field IsShowSubDetail = FindByName(__.IsShowSubDetail);

            /// <summary>模型ID</summary>
            public static readonly Field CatalogId = FindByName(__.CatalogId);

            /// <summary>详情数量，缓存</summary>
            public static readonly Field Counts = FindByName(__.Counts);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName(__.Icon);

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName(__.ClassName);

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName(__.BannerImg);

            /// <summary>栏目详细介绍</summary>
            public static readonly Field KindInfo = FindByName(__.KindInfo);

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName(__.Pic);

            /// <summary>广告ID</summary>
            public static readonly Field AdsId = FindByName(__.AdsId);

            /// <summary>目录路径</summary>
            public static readonly Field FilePath = FindByName(__.FilePath);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文章栏目字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>栏目名称</summary>
            public const String KindName = "KindName";

            /// <summary>栏目副标题</summary>
            public const String SubTitle = "SubTitle";

            /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
            public const String KindTitle = "KindTitle";

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

            /// <summary>详情模板</summary>
            public const String DetailTemplateFile = "DetailTemplateFile";

            /// <summary>类别域名（保留）</summary>
            public const String KindDomain = "KindDomain";

            /// <summary>是否为列表页面</summary>
            public const String IsList = "IsList";

            /// <summary>每页显示数量</summary>
            public const String PageSize = "PageSize";

            /// <summary>上级ID</summary>
            public const String PId = "PId";

            /// <summary>级别</summary>
            public const String Level = "Level";

            /// <summary>路径</summary>
            public const String Location = "Location";

            /// <summary>是否隐藏</summary>
            public const String IsHide = "IsHide";

            /// <summary>是否锁定，不允许删除</summary>
            public const String IsLock = "IsLock";

            /// <summary>是否删除,已经删除到回收站</summary>
            public const String IsDel = "IsDel";

            /// <summary>是否允许评论</summary>
            public const String IsComment = "IsComment";

            /// <summary>是否会员栏目</summary>
            public const String IsMember = "IsMember";

            /// <summary>是否显示下级栏目内容</summary>
            public const String IsShowSubDetail = "IsShowSubDetail";

            /// <summary>模型ID</summary>
            public const String CatalogId = "CatalogId";

            /// <summary>详情数量，缓存</summary>
            public const String Counts = "Counts";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>图标</summary>
            public const String Icon = "Icon";

            /// <summary>样式名称</summary>
            public const String ClassName = "ClassName";

            /// <summary>banner图片</summary>
            public const String BannerImg = "BannerImg";

            /// <summary>栏目详细介绍</summary>
            public const String KindInfo = "KindInfo";

            /// <summary>图片</summary>
            public const String Pic = "Pic";

            /// <summary>广告ID</summary>
            public const String AdsId = "AdsId";

            /// <summary>目录路径</summary>
            public const String FilePath = "FilePath";
        }
        #endregion
    }

    /// <summary>文章栏目接口</summary>
    public partial interface IArticleCategory
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>栏目名称</summary>
        String KindName { get; set; }

        /// <summary>栏目副标题</summary>
        String SubTitle { get; set; }

        /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
        String KindTitle { get; set; }

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

        /// <summary>详情模板</summary>
        String DetailTemplateFile { get; set; }

        /// <summary>类别域名（保留）</summary>
        String KindDomain { get; set; }

        /// <summary>是否为列表页面</summary>
        Int32 IsList { get; set; }

        /// <summary>每页显示数量</summary>
        Int32 PageSize { get; set; }

        /// <summary>上级ID</summary>
        Int32 PId { get; set; }

        /// <summary>级别</summary>
        Int32 Level { get; set; }

        /// <summary>路径</summary>
        String Location { get; set; }

        /// <summary>是否隐藏</summary>
        Int32 IsHide { get; set; }

        /// <summary>是否锁定，不允许删除</summary>
        Int32 IsLock { get; set; }

        /// <summary>是否删除,已经删除到回收站</summary>
        Int32 IsDel { get; set; }

        /// <summary>是否允许评论</summary>
        Int32 IsComment { get; set; }

        /// <summary>是否会员栏目</summary>
        Int32 IsMember { get; set; }

        /// <summary>是否显示下级栏目内容</summary>
        Int32 IsShowSubDetail { get; set; }

        /// <summary>模型ID</summary>
        Int32 CatalogId { get; set; }

        /// <summary>详情数量，缓存</summary>
        Int32 Counts { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>图标</summary>
        String Icon { get; set; }

        /// <summary>样式名称</summary>
        String ClassName { get; set; }

        /// <summary>banner图片</summary>
        String BannerImg { get; set; }

        /// <summary>栏目详细介绍</summary>
        String KindInfo { get; set; }

        /// <summary>图片</summary>
        String Pic { get; set; }

        /// <summary>广告ID</summary>
        Int32 AdsId { get; set; }

        /// <summary>目录路径</summary>
        String FilePath { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}