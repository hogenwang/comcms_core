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
    /// <summary>公众平台回复信息</summary>
    [Serializable]
    [DataObject]
    [Description("公众平台回复信息")]
    [BindTable("WeixinResponseContent", Description = "公众平台回复信息", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WeixinResponseContent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _OpenId;
        /// <summary>用户OpenId</summary>
        [DisplayName("用户OpenId")]
        [Description("用户OpenId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenId", "用户OpenId", "")]
        public String OpenId { get => _OpenId; set { if (OnPropertyChanging("OpenId", value)) { _OpenId = value; OnPropertyChanged("OpenId"); } } }

        private String _RequestType;
        /// <summary>请求类型</summary>
        [DisplayName("请求类型")]
        [Description("请求类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RequestType", "请求类型", "")]
        public String RequestType { get => _RequestType; set { if (OnPropertyChanging("RequestType", value)) { _RequestType = value; OnPropertyChanged("RequestType"); } } }

        private String _RequestContent;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("RequestContent", "内容", "")]
        public String RequestContent { get => _RequestContent; set { if (OnPropertyChanging("RequestContent", value)) { _RequestContent = value; OnPropertyChanged("RequestContent"); } } }

        private String _ResponseType;
        /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
        [DisplayName("回复的类型文本消息：text图片消息:image地理位置消息:location链接消息:link")]
        [Description("回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ResponseType", "回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link", "")]
        public String ResponseType { get => _ResponseType; set { if (OnPropertyChanging("ResponseType", value)) { _ResponseType = value; OnPropertyChanged("ResponseType"); } } }

        private String _ReponseContent;
        /// <summary>系统回复的内容</summary>
        [DisplayName("系统回复的内容")]
        [Description("系统回复的内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ReponseContent", "系统回复的内容", "")]
        public String ReponseContent { get => _ReponseContent; set { if (OnPropertyChanging("ReponseContent", value)) { _ReponseContent = value; OnPropertyChanged("ReponseContent"); } } }

        private String _XmlContent;
        /// <summary>xml原始内容</summary>
        [DisplayName("xml原始内容")]
        [Description("xml原始内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("XmlContent", "xml原始内容", "")]
        public String XmlContent { get => _XmlContent; set { if (OnPropertyChanging("XmlContent", value)) { _XmlContent = value; OnPropertyChanged("XmlContent"); } } }

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
                    case "OpenId": return _OpenId;
                    case "RequestType": return _RequestType;
                    case "RequestContent": return _RequestContent;
                    case "ResponseType": return _ResponseType;
                    case "ReponseContent": return _ReponseContent;
                    case "XmlContent": return _XmlContent;
                    case "UpdateTime": return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "OpenId": _OpenId = Convert.ToString(value); break;
                    case "RequestType": _RequestType = Convert.ToString(value); break;
                    case "RequestContent": _RequestContent = Convert.ToString(value); break;
                    case "ResponseType": _ResponseType = Convert.ToString(value); break;
                    case "ReponseContent": _ReponseContent = Convert.ToString(value); break;
                    case "XmlContent": _XmlContent = Convert.ToString(value); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得公众平台回复信息字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户OpenId</summary>
            public static readonly Field OpenId = FindByName("OpenId");

            /// <summary>请求类型</summary>
            public static readonly Field RequestType = FindByName("RequestType");

            /// <summary>内容</summary>
            public static readonly Field RequestContent = FindByName("RequestContent");

            /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
            public static readonly Field ResponseType = FindByName("ResponseType");

            /// <summary>系统回复的内容</summary>
            public static readonly Field ReponseContent = FindByName("ReponseContent");

            /// <summary>xml原始内容</summary>
            public static readonly Field XmlContent = FindByName("XmlContent");

            /// <summary>修改时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得公众平台回复信息字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户OpenId</summary>
            public const String OpenId = "OpenId";

            /// <summary>请求类型</summary>
            public const String RequestType = "RequestType";

            /// <summary>内容</summary>
            public const String RequestContent = "RequestContent";

            /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
            public const String ResponseType = "ResponseType";

            /// <summary>系统回复的内容</summary>
            public const String ReponseContent = "ReponseContent";

            /// <summary>xml原始内容</summary>
            public const String XmlContent = "XmlContent";

            /// <summary>修改时间</summary>
            public const String UpdateTime = "UpdateTime";
        }
        #endregion
    }
}