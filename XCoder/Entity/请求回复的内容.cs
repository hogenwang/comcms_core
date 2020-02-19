using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>请求回复的内容</summary>
    [Serializable]
    [DataObject]
    [Description("请求回复的内容")]
    [BindTable("WeixinRequestContent", Description = "请求回复的内容", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WeixinRequestContent : IWeixinRequestContent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _RuleId;
        /// <summary>规则名称</summary>
        [DisplayName("规则名称")]
        [Description("规则名称")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RuleId", "规则名称", "")]
        public Int32 RuleId { get { return _RuleId; } set { if (OnPropertyChanging(__.RuleId, value)) { _RuleId = value; OnPropertyChanged(__.RuleId); } } }

        private String _Title;
        /// <summary>回复标题</summary>
        [DisplayName("回复标题")]
        [Description("回复标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "回复标题", "", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Content;
        /// <summary>回复内容</summary>
        [DisplayName("回复内容")]
        [Description("回复内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "回复内容", "")]
        public String Content { get { return _Content; } set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private String _LinkUrl;
        /// <summary>详情链接地址</summary>
        [DisplayName("详情链接地址")]
        [Description("详情链接地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkUrl", "详情链接地址", "")]
        public String LinkUrl { get { return _LinkUrl; } set { if (OnPropertyChanging(__.LinkUrl, value)) { _LinkUrl = value; OnPropertyChanged(__.LinkUrl); } } }

        private String _ImgURL;
        /// <summary>图片地址</summary>
        [DisplayName("图片地址")]
        [Description("图片地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("ImgURL", "图片地址", "")]
        public String ImgURL { get { return _ImgURL; } set { if (OnPropertyChanging(__.ImgURL, value)) { _ImgURL = value; OnPropertyChanged(__.ImgURL); } } }

        private String _MediaURL;
        /// <summary>语音或视频地址</summary>
        [DisplayName("语音或视频地址")]
        [Description("语音或视频地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("MediaURL", "语音或视频地址", "")]
        public String MediaURL { get { return _MediaURL; } set { if (OnPropertyChanging(__.MediaURL, value)) { _MediaURL = value; OnPropertyChanged(__.MediaURL); } } }

        private String _MeidaHdURL;
        /// <summary>高清语音或者视频地址</summary>
        [DisplayName("高清语音或者视频地址")]
        [Description("高清语音或者视频地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("MeidaHdURL", "高清语音或者视频地址", "")]
        public String MeidaHdURL { get { return _MeidaHdURL; } set { if (OnPropertyChanging(__.MeidaHdURL, value)) { _MeidaHdURL = value; OnPropertyChanged(__.MeidaHdURL); } } }

        private String _MediaID;
        /// <summary>返回的素材ID</summary>
        [DisplayName("返回的素材ID")]
        [Description("返回的素材ID")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MediaID", "返回的素材ID", "")]
        public String MediaID { get { return _MediaID; } set { if (OnPropertyChanging(__.MediaID, value)) { _MediaID = value; OnPropertyChanged(__.MediaID); } } }

        private String _WXAppAppId;
        /// <summary>小程序APPId</summary>
        [DisplayName("小程序APPId")]
        [Description("小程序APPId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppAppId", "小程序APPId", "")]
        public String WXAppAppId { get { return _WXAppAppId; } set { if (OnPropertyChanging(__.WXAppAppId, value)) { _WXAppAppId = value; OnPropertyChanged(__.WXAppAppId); } } }

        private String _WXAppPage;
        /// <summary>小程序页面</summary>
        [DisplayName("小程序页面")]
        [Description("小程序页面")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppPage", "小程序页面", "")]
        public String WXAppPage { get { return _WXAppPage; } set { if (OnPropertyChanging(__.WXAppPage, value)) { _WXAppPage = value; OnPropertyChanged(__.WXAppPage); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get { return _Rank; } set { if (OnPropertyChanging(__.Rank, value)) { _Rank = value; OnPropertyChanged(__.Rank); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private DateTime _UpdateTime;
        /// <summary>修改时间</summary>
        [DisplayName("修改时间")]
        [Description("修改时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "修改时间", "")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }
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
                    case __.RuleId : return _RuleId;
                    case __.Title : return _Title;
                    case __.Content : return _Content;
                    case __.LinkUrl : return _LinkUrl;
                    case __.ImgURL : return _ImgURL;
                    case __.MediaURL : return _MediaURL;
                    case __.MeidaHdURL : return _MeidaHdURL;
                    case __.MediaID : return _MediaID;
                    case __.WXAppAppId : return _WXAppAppId;
                    case __.WXAppPage : return _WXAppPage;
                    case __.Rank : return _Rank;
                    case __.AddTime : return _AddTime;
                    case __.UpdateTime : return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = value.ToInt(); break;
                    case __.RuleId : _RuleId = value.ToInt(); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.LinkUrl : _LinkUrl = Convert.ToString(value); break;
                    case __.ImgURL : _ImgURL = Convert.ToString(value); break;
                    case __.MediaURL : _MediaURL = Convert.ToString(value); break;
                    case __.MeidaHdURL : _MeidaHdURL = Convert.ToString(value); break;
                    case __.MediaID : _MediaID = Convert.ToString(value); break;
                    case __.WXAppAppId : _WXAppAppId = Convert.ToString(value); break;
                    case __.WXAppPage : _WXAppPage = Convert.ToString(value); break;
                    case __.Rank : _Rank = value.ToInt(); break;
                    case __.AddTime : _AddTime = value.ToDateTime(); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得请求回复的内容字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>规则名称</summary>
            public static readonly Field RuleId = FindByName(__.RuleId);

            /// <summary>回复标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>回复内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>详情链接地址</summary>
            public static readonly Field LinkUrl = FindByName(__.LinkUrl);

            /// <summary>图片地址</summary>
            public static readonly Field ImgURL = FindByName(__.ImgURL);

            /// <summary>语音或视频地址</summary>
            public static readonly Field MediaURL = FindByName(__.MediaURL);

            /// <summary>高清语音或者视频地址</summary>
            public static readonly Field MeidaHdURL = FindByName(__.MeidaHdURL);

            /// <summary>返回的素材ID</summary>
            public static readonly Field MediaID = FindByName(__.MediaID);

            /// <summary>小程序APPId</summary>
            public static readonly Field WXAppAppId = FindByName(__.WXAppAppId);

            /// <summary>小程序页面</summary>
            public static readonly Field WXAppPage = FindByName(__.WXAppPage);

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName(__.Rank);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>修改时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得请求回复的内容字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>规则名称</summary>
            public const String RuleId = "RuleId";

            /// <summary>回复标题</summary>
            public const String Title = "Title";

            /// <summary>回复内容</summary>
            public const String Content = "Content";

            /// <summary>详情链接地址</summary>
            public const String LinkUrl = "LinkUrl";

            /// <summary>图片地址</summary>
            public const String ImgURL = "ImgURL";

            /// <summary>语音或视频地址</summary>
            public const String MediaURL = "MediaURL";

            /// <summary>高清语音或者视频地址</summary>
            public const String MeidaHdURL = "MeidaHdURL";

            /// <summary>返回的素材ID</summary>
            public const String MediaID = "MediaID";

            /// <summary>小程序APPId</summary>
            public const String WXAppAppId = "WXAppAppId";

            /// <summary>小程序页面</summary>
            public const String WXAppPage = "WXAppPage";

            /// <summary>排序</summary>
            public const String Rank = "Rank";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>修改时间</summary>
            public const String UpdateTime = "UpdateTime";
        }
        #endregion
    }

    /// <summary>请求回复的内容接口</summary>
    public partial interface IWeixinRequestContent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>规则名称</summary>
        Int32 RuleId { get; set; }

        /// <summary>回复标题</summary>
        String Title { get; set; }

        /// <summary>回复内容</summary>
        String Content { get; set; }

        /// <summary>详情链接地址</summary>
        String LinkUrl { get; set; }

        /// <summary>图片地址</summary>
        String ImgURL { get; set; }

        /// <summary>语音或视频地址</summary>
        String MediaURL { get; set; }

        /// <summary>高清语音或者视频地址</summary>
        String MeidaHdURL { get; set; }

        /// <summary>返回的素材ID</summary>
        String MediaID { get; set; }

        /// <summary>小程序APPId</summary>
        String WXAppAppId { get; set; }

        /// <summary>小程序页面</summary>
        String WXAppPage { get; set; }

        /// <summary>排序</summary>
        Int32 Rank { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>修改时间</summary>
        DateTime UpdateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}