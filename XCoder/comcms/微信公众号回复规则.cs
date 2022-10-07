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
    /// <summary>微信公众号回复规则</summary>
    [Serializable]
    [DataObject]
    [Description("微信公众号回复规则")]
    [BindTable("WeixinRequestRule", Description = "微信公众号回复规则", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WeixinRequestRule
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _RuleName;
        /// <summary>规则名称</summary>
        [DisplayName("规则名称")]
        [Description("规则名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RuleName", "规则名称", "")]
        public String RuleName { get => _RuleName; set { if (OnPropertyChanging("RuleName", value)) { _RuleName = value; OnPropertyChanged("RuleName"); } } }

        private String _Keywords;
        /// <summary>请求关键词,逗号分隔</summary>
        [DisplayName("请求关键词")]
        [Description("请求关键词,逗号分隔")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Keywords", "请求关键词,逗号分隔", "")]
        public String Keywords { get => _Keywords; set { if (OnPropertyChanging("Keywords", value)) { _Keywords = value; OnPropertyChanged("Keywords"); } } }

        private Int32 _RequestType;
        /// <summary>请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）</summary>
        [DisplayName("请求类型")]
        [Description("请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RequestType", "请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）", "")]
        public Int32 RequestType { get => _RequestType; set { if (OnPropertyChanging("RequestType", value)) { _RequestType = value; OnPropertyChanged("RequestType"); } } }

        private Int32 _ResponseType;
        /// <summary>回复类型(1文本2图文3语音4视频5第三方接口)</summary>
        [DisplayName("回复类型")]
        [Description("回复类型(1文本2图文3语音4视频5第三方接口)")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ResponseType", "回复类型(1文本2图文3语音4视频5第三方接口)", "")]
        public Int32 ResponseType { get => _ResponseType; set { if (OnPropertyChanging("ResponseType", value)) { _ResponseType = value; OnPropertyChanged("ResponseType"); } } }

        private Int32 _IsLikeQuery;
        /// <summary>是否模糊查询 0 是完全匹配；1是模糊匹配</summary>
        [DisplayName("是否模糊查询0是完全匹配；1是模糊匹配")]
        [Description("是否模糊查询 0 是完全匹配；1是模糊匹配")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLikeQuery", "是否模糊查询 0 是完全匹配；1是模糊匹配", "")]
        public Int32 IsLikeQuery { get => _IsLikeQuery; set { if (OnPropertyChanging("IsLikeQuery", value)) { _IsLikeQuery = value; OnPropertyChanged("IsLikeQuery"); } } }

        private Int32 _IsDefault;
        /// <summary>是否是默认回复</summary>
        [DisplayName("是否是默认回复")]
        [Description("是否是默认回复")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDefault", "是否是默认回复", "")]
        public Int32 IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

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
                    case "RuleName": return _RuleName;
                    case "Keywords": return _Keywords;
                    case "RequestType": return _RequestType;
                    case "ResponseType": return _ResponseType;
                    case "IsLikeQuery": return _IsLikeQuery;
                    case "IsDefault": return _IsDefault;
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
                    case "RuleName": _RuleName = Convert.ToString(value); break;
                    case "Keywords": _Keywords = Convert.ToString(value); break;
                    case "RequestType": _RequestType = value.ToInt(); break;
                    case "ResponseType": _ResponseType = value.ToInt(); break;
                    case "IsLikeQuery": _IsLikeQuery = value.ToInt(); break;
                    case "IsDefault": _IsDefault = value.ToInt(); break;
                    case "Rank": _Rank = value.ToInt(); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得微信公众号回复规则字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>规则名称</summary>
            public static readonly Field RuleName = FindByName("RuleName");

            /// <summary>请求关键词,逗号分隔</summary>
            public static readonly Field Keywords = FindByName("Keywords");

            /// <summary>请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）</summary>
            public static readonly Field RequestType = FindByName("RequestType");

            /// <summary>回复类型(1文本2图文3语音4视频5第三方接口)</summary>
            public static readonly Field ResponseType = FindByName("ResponseType");

            /// <summary>是否模糊查询 0 是完全匹配；1是模糊匹配</summary>
            public static readonly Field IsLikeQuery = FindByName("IsLikeQuery");

            /// <summary>是否是默认回复</summary>
            public static readonly Field IsDefault = FindByName("IsDefault");

            /// <summary>排序</summary>
            public static readonly Field Rank = FindByName("Rank");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>修改时间</summary>
            public static readonly Field UpdateTime = FindByName("UpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得微信公众号回复规则字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>规则名称</summary>
            public const String RuleName = "RuleName";

            /// <summary>请求关键词,逗号分隔</summary>
            public const String Keywords = "Keywords";

            /// <summary>请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）</summary>
            public const String RequestType = "RequestType";

            /// <summary>回复类型(1文本2图文3语音4视频5第三方接口)</summary>
            public const String ResponseType = "ResponseType";

            /// <summary>是否模糊查询 0 是完全匹配；1是模糊匹配</summary>
            public const String IsLikeQuery = "IsLikeQuery";

            /// <summary>是否是默认回复</summary>
            public const String IsDefault = "IsDefault";

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