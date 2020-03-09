using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class WeixinResponseContent : IWeixinResponseContent
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _OpenId;
        /// <summary>用户OpenId</summary>
        [DisplayName("用户OpenId")]
        [Description("用户OpenId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenId", "用户OpenId", "")]
        public String OpenId { get { return _OpenId; } set { if (OnPropertyChanging(__.OpenId, value)) { _OpenId = value; OnPropertyChanged(__.OpenId); } } }

        private String _RequestType;
        /// <summary>请求类型</summary>
        [DisplayName("请求类型")]
        [Description("请求类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RequestType", "请求类型", "")]
        public String RequestType { get { return _RequestType; } set { if (OnPropertyChanging(__.RequestType, value)) { _RequestType = value; OnPropertyChanged(__.RequestType); } } }

        private String _RequestContent;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("RequestContent", "内容", "")]
        public String RequestContent { get { return _RequestContent; } set { if (OnPropertyChanging(__.RequestContent, value)) { _RequestContent = value; OnPropertyChanged(__.RequestContent); } } }

        private String _ResponseType;
        /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
        [DisplayName("回复的类型文本消息：text图片消息:image地理位置消息:location链接消息:link")]
        [Description("回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ResponseType", "回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link", "")]
        public String ResponseType { get { return _ResponseType; } set { if (OnPropertyChanging(__.ResponseType, value)) { _ResponseType = value; OnPropertyChanged(__.ResponseType); } } }

        private String _ReponseContent;
        /// <summary>系统回复的内容</summary>
        [DisplayName("系统回复的内容")]
        [Description("系统回复的内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ReponseContent", "系统回复的内容", "")]
        public String ReponseContent { get { return _ReponseContent; } set { if (OnPropertyChanging(__.ReponseContent, value)) { _ReponseContent = value; OnPropertyChanged(__.ReponseContent); } } }

        private String _XmlContent;
        /// <summary>xml原始内容</summary>
        [DisplayName("xml原始内容")]
        [Description("xml原始内容")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("XmlContent", "xml原始内容", "")]
        public String XmlContent { get { return _XmlContent; } set { if (OnPropertyChanging(__.XmlContent, value)) { _XmlContent = value; OnPropertyChanged(__.XmlContent); } } }

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
                    case __.OpenId : return _OpenId;
                    case __.RequestType : return _RequestType;
                    case __.RequestContent : return _RequestContent;
                    case __.ResponseType : return _ResponseType;
                    case __.ReponseContent : return _ReponseContent;
                    case __.XmlContent : return _XmlContent;
                    case __.UpdateTime : return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = value.ToInt(); break;
                    case __.OpenId : _OpenId = Convert.ToString(value); break;
                    case __.RequestType : _RequestType = Convert.ToString(value); break;
                    case __.RequestContent : _RequestContent = Convert.ToString(value); break;
                    case __.ResponseType : _ResponseType = Convert.ToString(value); break;
                    case __.ReponseContent : _ReponseContent = Convert.ToString(value); break;
                    case __.XmlContent : _XmlContent = Convert.ToString(value); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户OpenId</summary>
            public static readonly Field OpenId = FindByName(__.OpenId);

            /// <summary>请求类型</summary>
            public static readonly Field RequestType = FindByName(__.RequestType);

            /// <summary>内容</summary>
            public static readonly Field RequestContent = FindByName(__.RequestContent);

            /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
            public static readonly Field ResponseType = FindByName(__.ResponseType);

            /// <summary>系统回复的内容</summary>
            public static readonly Field ReponseContent = FindByName(__.ReponseContent);

            /// <summary>xml原始内容</summary>
            public static readonly Field XmlContent = FindByName(__.XmlContent);

            /// <summary>修改时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>公众平台回复信息接口</summary>
    public partial interface IWeixinResponseContent
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户OpenId</summary>
        String OpenId { get; set; }

        /// <summary>请求类型</summary>
        String RequestType { get; set; }

        /// <summary>内容</summary>
        String RequestContent { get; set; }

        /// <summary>回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link</summary>
        String ResponseType { get; set; }

        /// <summary>系统回复的内容</summary>
        String ReponseContent { get; set; }

        /// <summary>xml原始内容</summary>
        String XmlContent { get; set; }

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