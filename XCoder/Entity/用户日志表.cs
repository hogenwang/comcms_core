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
    /// <summary>用户日志表</summary>
    [Serializable]
    [DataObject]
    [Description("用户日志表")]
    [BindTable("MemberLog", Description = "用户日志表", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class MemberLog
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "", Master = true)]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _UId;
        /// <summary>管理员ID</summary>
        [DisplayName("管理员ID")]
        [Description("管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "管理员ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _Guid;
        /// <summary>唯一ID</summary>
        [DisplayName("唯一ID")]
        [Description("唯一ID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Guid", "唯一ID", "")]
        public String Guid { get => _Guid; set { if (OnPropertyChanging("Guid", value)) { _Guid = value; OnPropertyChanged("Guid"); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("UserName", "用户名", "")]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

        private String _PassWord;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PassWord", "密码", "")]
        public String PassWord { get => _PassWord; set { if (OnPropertyChanging("PassWord", value)) { _PassWord = value; OnPropertyChanged("PassWord"); } } }

        private DateTime _LoginTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LoginTime", "登录时间", "")]
        public DateTime LoginTime { get => _LoginTime; set { if (OnPropertyChanging("LoginTime", value)) { _LoginTime = value; OnPropertyChanged("LoginTime"); } } }

        private String _LoginIP;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LoginIP", "登录IP", "")]
        public String LoginIP { get => _LoginIP; set { if (OnPropertyChanging("LoginIP", value)) { _LoginIP = value; OnPropertyChanged("LoginIP"); } } }

        private Int32 _IsLoginOK;
        /// <summary>是否登录成功</summary>
        [DisplayName("是否登录成功")]
        [Description("是否登录成功")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLoginOK", "是否登录成功", "")]
        public Int32 IsLoginOK { get => _IsLoginOK; set { if (OnPropertyChanging("IsLoginOK", value)) { _IsLoginOK = value; OnPropertyChanged("IsLoginOK"); } } }

        private String _Actions;
        /// <summary>记录</summary>
        [DisplayName("记录")]
        [Description("记录")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Actions", "记录", "")]
        public String Actions { get => _Actions; set { if (OnPropertyChanging("Actions", value)) { _Actions = value; OnPropertyChanged("Actions"); } } }

        private DateTime _LastUpdateTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "登录时间", "")]
        public DateTime LastUpdateTime { get => _LastUpdateTime; set { if (OnPropertyChanging("LastUpdateTime", value)) { _LastUpdateTime = value; OnPropertyChanged("LastUpdateTime"); } } }
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
                    case "Guid": return _Guid;
                    case "UserName": return _UserName;
                    case "PassWord": return _PassWord;
                    case "LoginTime": return _LoginTime;
                    case "LoginIP": return _LoginIP;
                    case "IsLoginOK": return _IsLoginOK;
                    case "Actions": return _Actions;
                    case "LastUpdateTime": return _LastUpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "Guid": _Guid = Convert.ToString(value); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "PassWord": _PassWord = Convert.ToString(value); break;
                    case "LoginTime": _LoginTime = value.ToDateTime(); break;
                    case "LoginIP": _LoginIP = Convert.ToString(value); break;
                    case "IsLoginOK": _IsLoginOK = value.ToInt(); break;
                    case "Actions": _Actions = Convert.ToString(value); break;
                    case "LastUpdateTime": _LastUpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户日志表字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>管理员ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>唯一ID</summary>
            public static readonly Field Guid = FindByName("Guid");

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>密码</summary>
            public static readonly Field PassWord = FindByName("PassWord");

            /// <summary>登录时间</summary>
            public static readonly Field LoginTime = FindByName("LoginTime");

            /// <summary>登录IP</summary>
            public static readonly Field LoginIP = FindByName("LoginIP");

            /// <summary>是否登录成功</summary>
            public static readonly Field IsLoginOK = FindByName("IsLoginOK");

            /// <summary>记录</summary>
            public static readonly Field Actions = FindByName("Actions");

            /// <summary>登录时间</summary>
            public static readonly Field LastUpdateTime = FindByName("LastUpdateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户日志表字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>管理员ID</summary>
            public const String UId = "UId";

            /// <summary>唯一ID</summary>
            public const String Guid = "Guid";

            /// <summary>用户名</summary>
            public const String UserName = "UserName";

            /// <summary>密码</summary>
            public const String PassWord = "PassWord";

            /// <summary>登录时间</summary>
            public const String LoginTime = "LoginTime";

            /// <summary>登录IP</summary>
            public const String LoginIP = "LoginIP";

            /// <summary>是否登录成功</summary>
            public const String IsLoginOK = "IsLoginOK";

            /// <summary>记录</summary>
            public const String Actions = "Actions";

            /// <summary>登录时间</summary>
            public const String LastUpdateTime = "LastUpdateTime";
        }
        #endregion
    }
}