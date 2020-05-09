using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>文章</summary>
    [Serializable]
    [DataObject]
    [Description("文章")]
    [BindTable("Article", Description = "文章", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Article : IArticle
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

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _SubTitle;
        /// <summary>副标题</summary>
        [DisplayName("副标题")]
        [Description("副标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SubTitle", "副标题", "")]
        public String SubTitle { get => _SubTitle; set { if (OnPropertyChanging(__.SubTitle, value)) { _SubTitle = value; OnPropertyChanged(__.SubTitle); } } }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "内容", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

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

        private Int32 _IsNew;
        /// <summary>是否最新</summary>
        [DisplayName("是否最新")]
        [Description("是否最新")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsNew", "是否最新", "")]
        public Int32 IsNew { get => _IsNew; set { if (OnPropertyChanging(__.IsNew, value)) { _IsNew = value; OnPropertyChanged(__.IsNew); } } }

        private Int32 _IsBest;
        /// <summary>是否推荐</summary>
        [DisplayName("是否推荐")]
        [Description("是否推荐")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsBest", "是否推荐", "")]
        public Int32 IsBest { get => _IsBest; set { if (OnPropertyChanging(__.IsBest, value)) { _IsBest = value; OnPropertyChanged(__.IsBest); } } }

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

        private Int32 _Hits;
        /// <summary>点击数量</summary>
        [DisplayName("点击数量")]
        [Description("点击数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Hits", "点击数量", "")]
        public Int32 Hits { get => _Hits; set { if (OnPropertyChanging(__.Hits, value)) { _Hits = value; OnPropertyChanged(__.Hits); } } }

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

        private String _Origin;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Origin", "来源", "")]
        public String Origin { get => _Origin; set { if (OnPropertyChanging(__.Origin, value)) { _Origin = value; OnPropertyChanged(__.Origin); } } }

        private String _OriginURL;
        /// <summary>来源地址</summary>
        [DisplayName("来源地址")]
        [Description("来源地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OriginURL", "来源地址", "")]
        public String OriginURL { get => _OriginURL; set { if (OnPropertyChanging(__.OriginURL, value)) { _OriginURL = value; OnPropertyChanged(__.OriginURL); } } }

        private String _ItemImg;
        /// <summary>更多图片</summary>
        [DisplayName("更多图片")]
        [Description("更多图片")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ItemImg", "更多图片", "")]
        public String ItemImg { get => _ItemImg; set { if (OnPropertyChanging(__.ItemImg, value)) { _ItemImg = value; OnPropertyChanged(__.ItemImg); } } }

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
                    case __.Title: return _Title;
                    case __.SubTitle: return _SubTitle;
                    case __.Content: return _Content;
                    case __.Keyword: return _Keyword;
                    case __.Description: return _Description;
                    case __.LinkURL: return _LinkURL;
                    case __.TitleColor: return _TitleColor;
                    case __.TemplateFile: return _TemplateFile;
                    case __.IsRecommend: return _IsRecommend;
                    case __.IsNew: return _IsNew;
                    case __.IsBest: return _IsBest;
                    case __.IsHide: return _IsHide;
                    case __.IsLock: return _IsLock;
                    case __.IsDel: return _IsDel;
                    case __.IsTop: return _IsTop;
                    case __.IsComment: return _IsComment;
                    case __.IsMember: return _IsMember;
                    case __.Hits: return _Hits;
                    case __.Sequence: return _Sequence;
                    case __.CommentCount: return _CommentCount;
                    case __.Icon: return _Icon;
                    case __.ClassName: return _ClassName;
                    case __.BannerImg: return _BannerImg;
                    case __.Pic: return _Pic;
                    case __.AdsId: return _AdsId;
                    case __.Tags: return _Tags;
                    case __.Origin: return _Origin;
                    case __.OriginURL: return _OriginURL;
                    case __.ItemImg: return _ItemImg;
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
                    case __.Title: _Title = Convert.ToString(value); break;
                    case __.SubTitle: _SubTitle = Convert.ToString(value); break;
                    case __.Content: _Content = Convert.ToString(value); break;
                    case __.Keyword: _Keyword = Convert.ToString(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.LinkURL: _LinkURL = Convert.ToString(value); break;
                    case __.TitleColor: _TitleColor = Convert.ToString(value); break;
                    case __.TemplateFile: _TemplateFile = Convert.ToString(value); break;
                    case __.IsRecommend: _IsRecommend = value.ToInt(); break;
                    case __.IsNew: _IsNew = value.ToInt(); break;
                    case __.IsBest: _IsBest = value.ToInt(); break;
                    case __.IsHide: _IsHide = value.ToInt(); break;
                    case __.IsLock: _IsLock = value.ToInt(); break;
                    case __.IsDel: _IsDel = value.ToInt(); break;
                    case __.IsTop: _IsTop = value.ToInt(); break;
                    case __.IsComment: _IsComment = value.ToInt(); break;
                    case __.IsMember: _IsMember = value.ToInt(); break;
                    case __.Hits: _Hits = value.ToInt(); break;
                    case __.Sequence: _Sequence = value.ToInt(); break;
                    case __.CommentCount: _CommentCount = value.ToInt(); break;
                    case __.Icon: _Icon = Convert.ToString(value); break;
                    case __.ClassName: _ClassName = Convert.ToString(value); break;
                    case __.BannerImg: _BannerImg = Convert.ToString(value); break;
                    case __.Pic: _Pic = Convert.ToString(value); break;
                    case __.AdsId: _AdsId = value.ToInt(); break;
                    case __.Tags: _Tags = Convert.ToString(value); break;
                    case __.Origin: _Origin = Convert.ToString(value); break;
                    case __.OriginURL: _OriginURL = Convert.ToString(value); break;
                    case __.ItemImg: _ItemImg = Convert.ToString(value); break;
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
        /// <summary>取得文章字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>栏目ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>副标题</summary>
            public static readonly Field SubTitle = FindByName(__.SubTitle);

            /// <summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

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

            /// <summary>是否最新</summary>
            public static readonly Field IsNew = FindByName(__.IsNew);

            /// <summary>是否推荐</summary>
            public static readonly Field IsBest = FindByName(__.IsBest);

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>是否锁定，不允许删除</summary>
            public static readonly Field IsLock = FindByName(__.IsLock);

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName(__.IsDel);

            /// <summary>是否置顶</summary>
            public static readonly Field IsTop = FindByName(__.IsTop);

            /// <summary>是否允许评论</summary>
            public static readonly Field IsComment = FindByName(__.IsComment);

            /// <summary>是否会员栏目</summary>
            public static readonly Field IsMember = FindByName(__.IsMember);

            /// <summary>点击数量</summary>
            public static readonly Field Hits = FindByName(__.Hits);

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

            /// <summary>来源</summary>
            public static readonly Field Origin = FindByName(__.Origin);

            /// <summary>来源地址</summary>
            public static readonly Field OriginURL = FindByName(__.OriginURL);

            /// <summary>更多图片</summary>
            public static readonly Field ItemImg = FindByName(__.ItemImg);

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

        /// <summary>取得文章字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>栏目ID</summary>
            public const String KId = "KId";

            /// <summary>标题</summary>
            public const String Title = "Title";

            /// <summary>副标题</summary>
            public const String SubTitle = "SubTitle";

            /// <summary>内容</summary>
            public const String Content = "Content";

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

            /// <summary>是否最新</summary>
            public const String IsNew = "IsNew";

            /// <summary>是否推荐</summary>
            public const String IsBest = "IsBest";

            /// <summary>是否隐藏</summary>
            public const String IsHide = "IsHide";

            /// <summary>是否锁定，不允许删除</summary>
            public const String IsLock = "IsLock";

            /// <summary>是否删除,已经删除到回收站</summary>
            public const String IsDel = "IsDel";

            /// <summary>是否置顶</summary>
            public const String IsTop = "IsTop";

            /// <summary>是否允许评论</summary>
            public const String IsComment = "IsComment";

            /// <summary>是否会员栏目</summary>
            public const String IsMember = "IsMember";

            /// <summary>点击数量</summary>
            public const String Hits = "Hits";

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

            /// <summary>来源</summary>
            public const String Origin = "Origin";

            /// <summary>来源地址</summary>
            public const String OriginURL = "OriginURL";

            /// <summary>更多图片</summary>
            public const String ItemImg = "ItemImg";

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

    /// <summary>文章接口</summary>
    public partial interface IArticle
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>栏目ID</summary>
        Int32 KId { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>副标题</summary>
        String SubTitle { get; set; }

        /// <summary>内容</summary>
        String Content { get; set; }

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

        /// <summary>是否最新</summary>
        Int32 IsNew { get; set; }

        /// <summary>是否推荐</summary>
        Int32 IsBest { get; set; }

        /// <summary>是否隐藏</summary>
        Int32 IsHide { get; set; }

        /// <summary>是否锁定，不允许删除</summary>
        Int32 IsLock { get; set; }

        /// <summary>是否删除,已经删除到回收站</summary>
        Int32 IsDel { get; set; }

        /// <summary>是否置顶</summary>
        Int32 IsTop { get; set; }

        /// <summary>是否允许评论</summary>
        Int32 IsComment { get; set; }

        /// <summary>是否会员栏目</summary>
        Int32 IsMember { get; set; }

        /// <summary>点击数量</summary>
        Int32 Hits { get; set; }

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

        /// <summary>来源</summary>
        String Origin { get; set; }

        /// <summary>来源地址</summary>
        String OriginURL { get; set; }

        /// <summary>更多图片</summary>
        String ItemImg { get; set; }

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