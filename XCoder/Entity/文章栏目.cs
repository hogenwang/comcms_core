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
    /// <summary>文章栏目</summary>
    [Serializable]
    [DataObject]
    [Description("文章栏目")]
    [BindTable("ArticleCategory", Description = "文章栏目", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class ArticleCategory
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _KindName;
        /// <summary>栏目名称</summary>
        [DisplayName("栏目名称")]
        [Description("栏目名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindName", "栏目名称", "")]
        public String KindName { get => _KindName; set { if (OnPropertyChanging("KindName", value)) { _KindName = value; OnPropertyChanged("KindName"); } } }

        private String _SubTitle;
        /// <summary>栏目副标题</summary>
        [DisplayName("栏目副标题")]
        [Description("栏目副标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SubTitle", "栏目副标题", "")]
        public String SubTitle { get => _SubTitle; set { if (OnPropertyChanging("SubTitle", value)) { _SubTitle = value; OnPropertyChanged("SubTitle"); } } }

        private String _KindTitle;
        /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
        [DisplayName("栏目标题")]
        [Description("栏目标题，填写则在浏览器替换此标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindTitle", "栏目标题，填写则在浏览器替换此标题", "")]
        public String KindTitle { get => _KindTitle; set { if (OnPropertyChanging("KindTitle", value)) { _KindTitle = value; OnPropertyChanged("KindTitle"); } } }

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

        private String _DetailTemplateFile;
        /// <summary>详情模板</summary>
        [DisplayName("详情模板")]
        [Description("详情模板")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("DetailTemplateFile", "详情模板", "")]
        public String DetailTemplateFile { get => _DetailTemplateFile; set { if (OnPropertyChanging("DetailTemplateFile", value)) { _DetailTemplateFile = value; OnPropertyChanged("DetailTemplateFile"); } } }

        private String _KindDomain;
        /// <summary>类别域名（保留）</summary>
        [DisplayName("类别域名")]
        [Description("类别域名（保留）")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("KindDomain", "类别域名（保留）", "")]
        public String KindDomain { get => _KindDomain; set { if (OnPropertyChanging("KindDomain", value)) { _KindDomain = value; OnPropertyChanged("KindDomain"); } } }

        private Int32 _IsList;
        /// <summary>是否为列表页面</summary>
        [DisplayName("是否为列表页面")]
        [Description("是否为列表页面")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsList", "是否为列表页面", "")]
        public Int32 IsList { get => _IsList; set { if (OnPropertyChanging("IsList", value)) { _IsList = value; OnPropertyChanged("IsList"); } } }

        private Int32 _PageSize;
        /// <summary>每页显示数量</summary>
        [DisplayName("每页显示数量")]
        [Description("每页显示数量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PageSize", "每页显示数量", "")]
        public Int32 PageSize { get => _PageSize; set { if (OnPropertyChanging("PageSize", value)) { _PageSize = value; OnPropertyChanged("PageSize"); } } }

        private Int32 _PId;
        /// <summary>上级ID</summary>
        [DisplayName("上级ID")]
        [Description("上级ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PId", "上级ID", "")]
        public Int32 PId { get => _PId; set { if (OnPropertyChanging("PId", value)) { _PId = value; OnPropertyChanged("PId"); } } }

        private Int32 _Level;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Level", "级别", "")]
        public Int32 Level { get => _Level; set { if (OnPropertyChanging("Level", value)) { _Level = value; OnPropertyChanged("Level"); } } }

        private String _Location;
        /// <summary>路径</summary>
        [DisplayName("路径")]
        [Description("路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Location", "路径", "")]
        public String Location { get => _Location; set { if (OnPropertyChanging("Location", value)) { _Location = value; OnPropertyChanged("Location"); } } }

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

        private Int32 _IsShowSubDetail;
        /// <summary>是否显示下级栏目内容</summary>
        [DisplayName("是否显示下级栏目内容")]
        [Description("是否显示下级栏目内容")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsShowSubDetail", "是否显示下级栏目内容", "")]
        public Int32 IsShowSubDetail { get => _IsShowSubDetail; set { if (OnPropertyChanging("IsShowSubDetail", value)) { _IsShowSubDetail = value; OnPropertyChanged("IsShowSubDetail"); } } }

        private Int32 _CatalogId;
        /// <summary>模型ID</summary>
        [DisplayName("模型ID")]
        [Description("模型ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CatalogId", "模型ID", "")]
        public Int32 CatalogId { get => _CatalogId; set { if (OnPropertyChanging("CatalogId", value)) { _CatalogId = value; OnPropertyChanged("CatalogId"); } } }

        private Int32 _Counts;
        /// <summary>详情数量，缓存</summary>
        [DisplayName("详情数量")]
        [Description("详情数量，缓存")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Counts", "详情数量，缓存", "")]
        public Int32 Counts { get => _Counts; set { if (OnPropertyChanging("Counts", value)) { _Counts = value; OnPropertyChanged("Counts"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }

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

        private String _KindInfo;
        /// <summary>栏目详细介绍</summary>
        [DisplayName("栏目详细介绍")]
        [Description("栏目详细介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("KindInfo", "栏目详细介绍", "")]
        public String KindInfo { get => _KindInfo; set { if (OnPropertyChanging("KindInfo", value)) { _KindInfo = value; OnPropertyChanged("KindInfo"); } } }

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

        private String _FilePath;
        /// <summary>目录路径</summary>
        [DisplayName("目录路径")]
        [Description("目录路径")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("FilePath", "目录路径", "")]
        public String FilePath { get => _FilePath; set { if (OnPropertyChanging("FilePath", value)) { _FilePath = value; OnPropertyChanged("FilePath"); } } }
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
                    case "KindName": return _KindName;
                    case "SubTitle": return _SubTitle;
                    case "KindTitle": return _KindTitle;
                    case "Keyword": return _Keyword;
                    case "Description": return _Description;
                    case "LinkURL": return _LinkURL;
                    case "TitleColor": return _TitleColor;
                    case "TemplateFile": return _TemplateFile;
                    case "DetailTemplateFile": return _DetailTemplateFile;
                    case "KindDomain": return _KindDomain;
                    case "IsList": return _IsList;
                    case "PageSize": return _PageSize;
                    case "PId": return _PId;
                    case "Level": return _Level;
                    case "Location": return _Location;
                    case "IsHide": return _IsHide;
                    case "IsLock": return _IsLock;
                    case "IsDel": return _IsDel;
                    case "IsComment": return _IsComment;
                    case "IsMember": return _IsMember;
                    case "IsShowSubDetail": return _IsShowSubDetail;
                    case "CatalogId": return _CatalogId;
                    case "Counts": return _Counts;
                    case "Rank": return _Rank;
                    case "Icon": return _Icon;
                    case "ClassName": return _ClassName;
                    case "BannerImg": return _BannerImg;
                    case "KindInfo": return _KindInfo;
                    case "Pic": return _Pic;
                    case "AdsId": return _AdsId;
                    case "FilePath": return _FilePath;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "KindName": _KindName = Convert.ToString(value); break;
                    case "SubTitle": _SubTitle = Convert.ToString(value); break;
                    case "KindTitle": _KindTitle = Convert.ToString(value); break;
                    case "Keyword": _Keyword = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "LinkURL": _LinkURL = Convert.ToString(value); break;
                    case "TitleColor": _TitleColor = Convert.ToString(value); break;
                    case "TemplateFile": _TemplateFile = Convert.ToString(value); break;
                    case "DetailTemplateFile": _DetailTemplateFile = Convert.ToString(value); break;
                    case "KindDomain": _KindDomain = Convert.ToString(value); break;
                    case "IsList": _IsList = value.ToInt(); break;
                    case "PageSize": _PageSize = value.ToInt(); break;
                    case "PId": _PId = value.ToInt(); break;
                    case "Level": _Level = value.ToInt(); break;
                    case "Location": _Location = Convert.ToString(value); break;
                    case "IsHide": _IsHide = value.ToInt(); break;
                    case "IsLock": _IsLock = value.ToInt(); break;
                    case "IsDel": _IsDel = value.ToInt(); break;
                    case "IsComment": _IsComment = value.ToInt(); break;
                    case "IsMember": _IsMember = value.ToInt(); break;
                    case "IsShowSubDetail": _IsShowSubDetail = value.ToInt(); break;
                    case "CatalogId": _CatalogId = value.ToInt(); break;
                    case "Counts": _Counts = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "Icon": _Icon = Convert.ToString(value); break;
                    case "ClassName": _ClassName = Convert.ToString(value); break;
                    case "BannerImg": _BannerImg = Convert.ToString(value); break;
                    case "KindInfo": _KindInfo = Convert.ToString(value); break;
                    case "Pic": _Pic = Convert.ToString(value); break;
                    case "AdsId": _AdsId = value.ToInt(); break;
                    case "FilePath": _FilePath = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>栏目名称</summary>
            public static readonly Field KindName = FindByName("KindName");

            /// <summary>栏目副标题</summary>
            public static readonly Field SubTitle = FindByName("SubTitle");

            /// <summary>栏目标题，填写则在浏览器替换此标题</summary>
            public static readonly Field KindTitle = FindByName("KindTitle");

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

            /// <summary>详情模板</summary>
            public static readonly Field DetailTemplateFile = FindByName("DetailTemplateFile");

            /// <summary>类别域名（保留）</summary>
            public static readonly Field KindDomain = FindByName("KindDomain");

            /// <summary>是否为列表页面</summary>
            public static readonly Field IsList = FindByName("IsList");

            /// <summary>每页显示数量</summary>
            public static readonly Field PageSize = FindByName("PageSize");

            /// <summary>上级ID</summary>
            public static readonly Field PId = FindByName("PId");

            /// <summary>级别</summary>
            public static readonly Field Level = FindByName("Level");

            /// <summary>路径</summary>
            public static readonly Field Location = FindByName("Location");

            /// <summary>是否隐藏</summary>
            public static readonly Field IsHide = FindByName("IsHide");

            /// <summary>是否锁定，不允许删除</summary>
            public static readonly Field IsLock = FindByName("IsLock");

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName("IsDel");

            /// <summary>是否允许评论</summary>
            public static readonly Field IsComment = FindByName("IsComment");

            /// <summary>是否会员栏目</summary>
            public static readonly Field IsMember = FindByName("IsMember");

            /// <summary>是否显示下级栏目内容</summary>
            public static readonly Field IsShowSubDetail = FindByName("IsShowSubDetail");

            /// <summary>模型ID</summary>
            public static readonly Field CatalogId = FindByName("CatalogId");

            /// <summary>详情数量，缓存</summary>
            public static readonly Field Counts = FindByName("Counts");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>图标</summary>
            public static readonly Field Icon = FindByName("Icon");

            /// <summary>样式名称</summary>
            public static readonly Field ClassName = FindByName("ClassName");

            /// <summary>banner图片</summary>
            public static readonly Field BannerImg = FindByName("BannerImg");

            /// <summary>栏目详细介绍</summary>
            public static readonly Field KindInfo = FindByName("KindInfo");

            /// <summary>图片</summary>
            public static readonly Field Pic = FindByName("Pic");

            /// <summary>广告ID</summary>
            public static readonly Field AdsId = FindByName("AdsId");

            /// <summary>目录路径</summary>
            public static readonly Field FilePath = FindByName("FilePath");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}