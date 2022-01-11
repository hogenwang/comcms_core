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
    /// <summary>小程序Session</summary>
    [Serializable]
    [DataObject]
    [Description("小程序Session")]
    [BindTable("WXAppSession", Description = "小程序Session", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class WXAppSession
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _Ip;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "登录IP", "")]
        public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

        private String _Key;
        /// <summary>系统生成Key</summary>
        [DisplayName("系统生成Key")]
        [Description("系统生成Key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Key", "系统生成Key", "")]
        public String Key { get => _Key; set { if (OnPropertyChanging("Key", value)) { _Key = value; OnPropertyChanged("Key"); } } }

        private String _SessionKey;
        /// <summary>微信SessionKey</summary>
        [DisplayName("微信SessionKey")]
        [Description("微信SessionKey")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SessionKey", "微信SessionKey", "")]
        public String SessionKey { get => _SessionKey; set { if (OnPropertyChanging("SessionKey", value)) { _SessionKey = value; OnPropertyChanged("SessionKey"); } } }

        private String _OpenId;
        /// <summary>微信小程序OpenId</summary>
        [DisplayName("微信小程序OpenId")]
        [Description("微信小程序OpenId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenId", "微信小程序OpenId", "")]
        public String OpenId { get => _OpenId; set { if (OnPropertyChanging("OpenId", value)) { _OpenId = value; OnPropertyChanged("OpenId"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }
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
                    case "UId": return _UId;
                    case "Ip": return _Ip;
                    case "Key": return _Key;
                    case "SessionKey": return _SessionKey;
                    case "OpenId": return _OpenId;
                    case "AddTime": return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "Key": _Key = Convert.ToString(value); break;
                    case "SessionKey": _SessionKey = Convert.ToString(value); break;
                    case "OpenId": _OpenId = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得小程序Session字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>登录IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>系统生成Key</summary>
            public static readonly Field Key = FindByName("Key");

            /// <summary>微信SessionKey</summary>
            public static readonly Field SessionKey = FindByName("SessionKey");

            /// <summary>微信小程序OpenId</summary>
            public static readonly Field OpenId = FindByName("OpenId");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得小程序Session字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>登录IP</summary>
            public const String Ip = "Ip";

            /// <summary>系统生成Key</summary>
            public const String Key = "Key";

            /// <summary>微信SessionKey</summary>
            public const String SessionKey = "SessionKey";

            /// <summary>微信小程序OpenId</summary>
            public const String OpenId = "OpenId";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";
        }
        #endregion
    }
}