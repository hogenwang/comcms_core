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
    /// <summary>请求回复的内容</summary>
    [Serializable]
    [DataObject]
    [Description("请求回复的内容")]
    [BindTable("WeixinRequestContent", Description = "请求回复的内容", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WeixinRequestContent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _RuleId;
        /// <summary>规则名称</summary>
        [DisplayName("规则名称")]
        [Description("规则名称")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RuleId", "规则名称", "")]
        public Int32 RuleId { get => _RuleId; set { if (OnPropertyChanging("RuleId", value)) { _RuleId = value; OnPropertyChanged("RuleId"); } } }

        private String _Title;
        /// <summary>回复标题</summary>
        [DisplayName("回复标题")]
        [Description("回复标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "回复标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _Content;
        /// <summary>回复内容</summary>
        [DisplayName("回复内容")]
        [Description("回复内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "回复内容", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

        private String _LinkUrl;
        /// <summary>详情链接地址</summary>
        [DisplayName("详情链接地址")]
        [Description("详情链接地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkUrl", "详情链接地址", "")]
        public String LinkUrl { get => _LinkUrl; set { if (OnPropertyChanging("LinkUrl", value)) { _LinkUrl = value; OnPropertyChanged("LinkUrl"); } } }

        private String _ImgURL;
        /// <summary>图片地址</summary>
        [DisplayName("图片地址")]
        [Description("图片地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("ImgURL", "图片地址", "")]
        public String ImgURL { get => _ImgURL; set { if (OnPropertyChanging("ImgURL", value)) { _ImgURL = value; OnPropertyChanged("ImgURL"); } } }

        private String _MediaURL;
        /// <summary>语音或视频地址</summary>
        [DisplayName("语音或视频地址")]
        [Description("语音或视频地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("MediaURL", "语音或视频地址", "")]
        public String MediaURL { get => _MediaURL; set { if (OnPropertyChanging("MediaURL", value)) { _MediaURL = value; OnPropertyChanged("MediaURL"); } } }

        private String _MeidaHdURL;
        /// <summary>高清语音或者视频地址</summary>
        [DisplayName("高清语音或者视频地址")]
        [Description("高清语音或者视频地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("MeidaHdURL", "高清语音或者视频地址", "")]
        public String MeidaHdURL { get => _MeidaHdURL; set { if (OnPropertyChanging("MeidaHdURL", value)) { _MeidaHdURL = value; OnPropertyChanged("MeidaHdURL"); } } }

        private String _MediaID;
        /// <summary>返回的素材ID</summary>
        [DisplayName("返回的素材ID")]
        [Description("返回的素材ID")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MediaID", "返回的素材ID", "")]
        public String MediaID { get => _MediaID; set { if (OnPropertyChanging("MediaID", value)) { _MediaID = value; OnPropertyChanged("MediaID"); } } }

        private String _WXAppAppId;
        /// <summary>小程序APPId</summary>
        [DisplayName("小程序APPId")]
        [Description("小程序APPId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppAppId", "小程序APPId", "")]
        public String WXAppAppId { get => _WXAppAppId; set { if (OnPropertyChanging("WXAppAppId", value)) { _WXAppAppId = value; OnPropertyChanged("WXAppAppId"); } } }

        private String _WXAppPage;
        /// <summary>小程序页面</summary>
        [DisplayName("小程序页面")]
        [Description("小程序页面")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppPage", "小程序页面", "")]
        public String WXAppPage { get => _WXAppPage; set { if (OnPropertyChanging("WXAppPage", value)) { _WXAppPage = value; OnPropertyChanged("WXAppPage"); } } }

        private Int32 _Rank;
        /// <summary>排序</summary>
        [DisplayName("排序")]
        [Description("排序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rank", "排序", "")]
        public Int32 Rank { get => _Rank; set { if (OnPropertyChanging("Rank", value)) { _Rank = value; OnPropertyChanged("Rank"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private DateTime _UpdateTime;
        /// <summary>修改时间</summary>
        [DisplayName("修改时间")]
        [Description("修改时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "修改时间", "")]
        public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }
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
                    case "RuleId": return _RuleId;
                    case "Title": return _Title;
                    case "Content": return _Content;
                    case "LinkUrl": return _LinkUrl;
                    case "ImgURL": return _ImgURL;
                    case "MediaURL": return _MediaURL;
                    case "MeidaHdURL": return _MeidaHdURL;
                    case "MediaID": return _MediaID;
                    case "WXAppAppId": return _WXAppAppId;
                    case "WXAppPage": return _WXAppPage;
                    case "Rank": return _Rank;
                    case "AddTime": return _AddTime;
                    case "UpdateTime": return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "RuleId": _RuleId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Content": _Content = Convert.ToString(value); break;
                    case "LinkUrl": _LinkUrl = Convert.ToString(value); break;
                    case "ImgURL": _ImgURL = Convert.ToString(value); break;
                    case "MediaURL": _MediaURL = Convert.ToString(value); break;
                    case "MeidaHdURL": _MeidaHdURL = Convert.ToString(value); break;
                    case "MediaID": _MediaID = Convert.ToString(value); break;
                    case "WXAppAppId": _WXAppAppId = Convert.ToString(value); break;
                    case "WXAppPage": _WXAppPage = Convert.ToString(value); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>规则名称</summary>
            public static readonly Field RuleId = FindByName("RuleId");

            /// <summary>回复标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>回复内容</summary>
            public static readonly Field Content = FindByName("Content");

            /// <summary>详情链接地址</summary>
            public static readonly Field LinkUrl = FindByName("LinkUrl");

            /// <summary>图片地址</summary>
            public static readonly Field ImgURL = FindByName("ImgURL");

            /// <summary>语音或视频地址</summary>
            public static readonly Field MediaURL = FindByName("MediaURL");

            /// <summary>高清语音或者视频地址</summary>
            public static readonly Field MeidaHdURL = FindByName("MeidaHdURL");

            /// <summary>返回的素材ID</summary>
            public static readonly Field MediaID = FindByName("MediaID");

            /// <summary>小程序APPId</summary>
            public static readonly Field WXAppAppId = FindByName("WXAppAppId");

            /// <summary>小程序页面</summary>
            public static readonly Field WXAppPage = FindByName("WXAppPage");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>修改时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}