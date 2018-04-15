using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class WXAppSession : IWXAppSession
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _IP;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("IP", "登录IP", "nvarchar(20)")]
        public String IP { get { return _IP; } set { if (OnPropertyChanging(__.IP, value)) { _IP = value; OnPropertyChanged(__.IP); } } }

        private String _Key;
        /// <summary>系统生成Key</summary>
        [DisplayName("系统生成Key")]
        [Description("系统生成Key")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Key", "系统生成Key", "nvarchar(50)")]
        public String Key { get { return _Key; } set { if (OnPropertyChanging(__.Key, value)) { _Key = value; OnPropertyChanged(__.Key); } } }

        private String _SessionKey;
        /// <summary>微信SessionKey</summary>
        [DisplayName("微信SessionKey")]
        [Description("微信SessionKey")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SessionKey", "微信SessionKey", "nvarchar(50)")]
        public String SessionKey { get { return _SessionKey; } set { if (OnPropertyChanging(__.SessionKey, value)) { _SessionKey = value; OnPropertyChanged(__.SessionKey); } } }

        private String _OpenId;
        /// <summary>微信小程序OpenId</summary>
        [DisplayName("微信小程序OpenId")]
        [Description("微信小程序OpenId")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenId", "微信小程序OpenId", "nvarchar(50)")]
        public String OpenId { get { return _OpenId; } set { if (OnPropertyChanging(__.OpenId, value)) { _OpenId = value; OnPropertyChanged(__.OpenId); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }
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
                    case __.UId : return _UId;
                    case __.IP : return _IP;
                    case __.Key : return _Key;
                    case __.SessionKey : return _SessionKey;
                    case __.OpenId : return _OpenId;
                    case __.AddTime : return _AddTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.IP : _IP = Convert.ToString(value); break;
                    case __.Key : _Key = Convert.ToString(value); break;
                    case __.SessionKey : _SessionKey = Convert.ToString(value); break;
                    case __.OpenId : _OpenId = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>登录IP</summary>
            public static readonly Field IP = FindByName(__.IP);

            /// <summary>系统生成Key</summary>
            public static readonly Field Key = FindByName(__.Key);

            /// <summary>微信SessionKey</summary>
            public static readonly Field SessionKey = FindByName(__.SessionKey);

            /// <summary>微信小程序OpenId</summary>
            public static readonly Field OpenId = FindByName(__.OpenId);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得小程序Session字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>登录IP</summary>
            public const String IP = "IP";

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

    /// <summary>小程序Session接口</summary>
    public partial interface IWXAppSession
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>登录IP</summary>
        String IP { get; set; }

        /// <summary>系统生成Key</summary>
        String Key { get; set; }

        /// <summary>微信SessionKey</summary>
        String SessionKey { get; set; }

        /// <summary>微信小程序OpenId</summary>
        String OpenId { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}