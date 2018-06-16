using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>管理日志表</summary>
    [Serializable]
    [DataObject]
    [Description("管理日志表")]
    [BindTable("AdminLog", Description = "管理日志表", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class AdminLog : IAdminLog
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
        /// <summary>管理员ID</summary>
        [DisplayName("管理员ID")]
        [Description("管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "管理员ID", "int", Master = true)]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _Guid;
        /// <summary>唯一ID</summary>
        [DisplayName("唯一ID")]
        [Description("唯一ID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Guid", "唯一ID", "nvarchar(50)")]
        public String Guid { get { return _Guid; } set { if (OnPropertyChanging(__.Guid, value)) { _Guid = value; OnPropertyChanged(__.Guid); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("UserName", "用户名", "nvarchar(20)", Master = true)]
        public String UserName { get { return _UserName; } set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } } }

        private String _PassWord;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PassWord", "密码", "nvarchar(50)")]
        public String PassWord { get { return _PassWord; } set { if (OnPropertyChanging(__.PassWord, value)) { _PassWord = value; OnPropertyChanged(__.PassWord); } } }

        private DateTime _LoginTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LoginTime", "登录时间", "datetime")]
        public DateTime LoginTime { get { return _LoginTime; } set { if (OnPropertyChanging(__.LoginTime, value)) { _LoginTime = value; OnPropertyChanged(__.LoginTime); } } }

        private String _LoginIP;
        /// <summary>登录IP</summary>
        [DisplayName("登录IP")]
        [Description("登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LoginIP", "登录IP", "nvarchar(20)")]
        public String LoginIP { get { return _LoginIP; } set { if (OnPropertyChanging(__.LoginIP, value)) { _LoginIP = value; OnPropertyChanged(__.LoginIP); } } }

        private Int32 _IsLoginOK;
        /// <summary>是否登录成功</summary>
        [DisplayName("是否登录成功")]
        [Description("是否登录成功")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLoginOK", "是否登录成功", "int")]
        public Int32 IsLoginOK { get { return _IsLoginOK; } set { if (OnPropertyChanging(__.IsLoginOK, value)) { _IsLoginOK = value; OnPropertyChanged(__.IsLoginOK); } } }

        private String _Actions;
        /// <summary>记录</summary>
        [DisplayName("记录")]
        [Description("记录")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Actions", "记录", "ntext", Master = true)]
        public String Actions { get { return _Actions; } set { if (OnPropertyChanging(__.Actions, value)) { _Actions = value; OnPropertyChanged(__.Actions); } } }

        private DateTime _LastUpdateTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "登录时间", "datetime")]
        public DateTime LastUpdateTime { get { return _LastUpdateTime; } set { if (OnPropertyChanging(__.LastUpdateTime, value)) { _LastUpdateTime = value; OnPropertyChanged(__.LastUpdateTime); } } }
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
                    case __.Guid : return _Guid;
                    case __.UserName : return _UserName;
                    case __.PassWord : return _PassWord;
                    case __.LoginTime : return _LoginTime;
                    case __.LoginIP : return _LoginIP;
                    case __.IsLoginOK : return _IsLoginOK;
                    case __.Actions : return _Actions;
                    case __.LastUpdateTime : return _LastUpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.Guid : _Guid = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.PassWord : _PassWord = Convert.ToString(value); break;
                    case __.LoginTime : _LoginTime = Convert.ToDateTime(value); break;
                    case __.LoginIP : _LoginIP = Convert.ToString(value); break;
                    case __.IsLoginOK : _IsLoginOK = Convert.ToInt32(value); break;
                    case __.Actions : _Actions = Convert.ToString(value); break;
                    case __.LastUpdateTime : _LastUpdateTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得管理日志表字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>管理员ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>唯一ID</summary>
            public static readonly Field Guid = FindByName(__.Guid);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>密码</summary>
            public static readonly Field PassWord = FindByName(__.PassWord);

            /// <summary>登录时间</summary>
            public static readonly Field LoginTime = FindByName(__.LoginTime);

            /// <summary>登录IP</summary>
            public static readonly Field LoginIP = FindByName(__.LoginIP);

            /// <summary>是否登录成功</summary>
            public static readonly Field IsLoginOK = FindByName(__.IsLoginOK);

            /// <summary>记录</summary>
            public static readonly Field Actions = FindByName(__.Actions);

            /// <summary>登录时间</summary>
            public static readonly Field LastUpdateTime = FindByName(__.LastUpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得管理日志表字段名称的快捷方式</summary>
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

    /// <summary>管理日志表接口</summary>
    public partial interface IAdminLog
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>管理员ID</summary>
        Int32 UId { get; set; }

        /// <summary>唯一ID</summary>
        String Guid { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>密码</summary>
        String PassWord { get; set; }

        /// <summary>登录时间</summary>
        DateTime LoginTime { get; set; }

        /// <summary>登录IP</summary>
        String LoginIP { get; set; }

        /// <summary>是否登录成功</summary>
        Int32 IsLoginOK { get; set; }

        /// <summary>记录</summary>
        String Actions { get; set; }

        /// <summary>登录时间</summary>
        DateTime LastUpdateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}