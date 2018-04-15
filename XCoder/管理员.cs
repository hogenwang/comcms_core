using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>管理员</summary>
    [Serializable]
    [DataObject]
    [Description("管理员")]
    [BindTable("Admin", Description = "管理员", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Admin : IAdmin
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

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

        private String _Salt;
        /// <summary>盐值</summary>
        [DisplayName("盐值")]
        [Description("盐值")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Salt", "盐值", "nvarchar(20)")]
        public String Salt { get { return _Salt; } set { if (OnPropertyChanging(__.Salt, value)) { _Salt = value; OnPropertyChanged(__.Salt); } } }

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RealName", "姓名", "nvarchar(20)")]
        public String RealName { get { return _RealName; } set { if (OnPropertyChanging(__.RealName, value)) { _RealName = value; OnPropertyChanged(__.RealName); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "电话", "nvarchar(20)")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "nvarchar(100)", Master = true)]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private Int32 _UserLevel;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UserLevel", "级别", "int")]
        public Int32 UserLevel { get { return _UserLevel; } set { if (OnPropertyChanging(__.UserLevel, value)) { _UserLevel = value; OnPropertyChanged(__.UserLevel); } } }

        private Int32 _RoleId;
        /// <summary>管理组</summary>
        [DisplayName("管理组")]
        [Description("管理组")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleId", "管理组", "int", Master = true)]
        public Int32 RoleId { get { return _RoleId; } set { if (OnPropertyChanging(__.RoleId, value)) { _RoleId = value; OnPropertyChanged(__.RoleId); } } }

        private Int32 _GroupId;
        /// <summary>用户组</summary>
        [DisplayName("用户组")]
        [Description("用户组")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GroupId", "用户组", "int", Master = true)]
        public Int32 GroupId { get { return _GroupId; } set { if (OnPropertyChanging(__.GroupId, value)) { _GroupId = value; OnPropertyChanged(__.GroupId); } } }

        private DateTime _LastLoginTime;
        /// <summary>最后登录时间</summary>
        [DisplayName("最后登录时间")]
        [Description("最后登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastLoginTime", "最后登录时间", "datetime")]
        public DateTime LastLoginTime { get { return _LastLoginTime; } set { if (OnPropertyChanging(__.LastLoginTime, value)) { _LastLoginTime = value; OnPropertyChanged(__.LastLoginTime); } } }

        private String _LastLoginIP;
        /// <summary>上次登录IP</summary>
        [DisplayName("上次登录IP")]
        [Description("上次登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LastLoginIP", "上次登录IP", "nvarchar(20)")]
        public String LastLoginIP { get { return _LastLoginIP; } set { if (OnPropertyChanging(__.LastLoginIP, value)) { _LastLoginIP = value; OnPropertyChanged(__.LastLoginIP); } } }

        private DateTime _ThisLoginTime;
        /// <summary>本次登录时间</summary>
        [DisplayName("本次登录时间")]
        [Description("本次登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ThisLoginTime", "本次登录时间", "datetime")]
        public DateTime ThisLoginTime { get { return _ThisLoginTime; } set { if (OnPropertyChanging(__.ThisLoginTime, value)) { _ThisLoginTime = value; OnPropertyChanged(__.ThisLoginTime); } } }

        private String _ThisLoginIP;
        /// <summary>本次登录IP</summary>
        [DisplayName("本次登录IP")]
        [Description("本次登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ThisLoginIP", "本次登录IP", "nvarchar(20)")]
        public String ThisLoginIP { get { return _ThisLoginIP; } set { if (OnPropertyChanging(__.ThisLoginIP, value)) { _ThisLoginIP = value; OnPropertyChanged(__.ThisLoginIP); } } }

        private Int32 _IsLock;
        /// <summary>是否是锁定</summary>
        [DisplayName("是否是锁定")]
        [Description("是否是锁定")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLock", "是否是锁定", "int")]
        public Int32 IsLock { get { return _IsLock; } set { if (OnPropertyChanging(__.IsLock, value)) { _IsLock = value; OnPropertyChanged(__.IsLock); } } }
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
                    case __.UserName : return _UserName;
                    case __.PassWord : return _PassWord;
                    case __.Salt : return _Salt;
                    case __.RealName : return _RealName;
                    case __.Tel : return _Tel;
                    case __.Email : return _Email;
                    case __.UserLevel : return _UserLevel;
                    case __.RoleId : return _RoleId;
                    case __.GroupId : return _GroupId;
                    case __.LastLoginTime : return _LastLoginTime;
                    case __.LastLoginIP : return _LastLoginIP;
                    case __.ThisLoginTime : return _ThisLoginTime;
                    case __.ThisLoginIP : return _ThisLoginIP;
                    case __.IsLock : return _IsLock;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.PassWord : _PassWord = Convert.ToString(value); break;
                    case __.Salt : _Salt = Convert.ToString(value); break;
                    case __.RealName : _RealName = Convert.ToString(value); break;
                    case __.Tel : _Tel = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.UserLevel : _UserLevel = Convert.ToInt32(value); break;
                    case __.RoleId : _RoleId = Convert.ToInt32(value); break;
                    case __.GroupId : _GroupId = Convert.ToInt32(value); break;
                    case __.LastLoginTime : _LastLoginTime = Convert.ToDateTime(value); break;
                    case __.LastLoginIP : _LastLoginIP = Convert.ToString(value); break;
                    case __.ThisLoginTime : _ThisLoginTime = Convert.ToDateTime(value); break;
                    case __.ThisLoginIP : _ThisLoginIP = Convert.ToString(value); break;
                    case __.IsLock : _IsLock = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得管理员字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>密码</summary>
            public static readonly Field PassWord = FindByName(__.PassWord);

            /// <summary>盐值</summary>
            public static readonly Field Salt = FindByName(__.Salt);

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName(__.RealName);

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>级别</summary>
            public static readonly Field UserLevel = FindByName(__.UserLevel);

            /// <summary>管理组</summary>
            public static readonly Field RoleId = FindByName(__.RoleId);

            /// <summary>用户组</summary>
            public static readonly Field GroupId = FindByName(__.GroupId);

            /// <summary>最后登录时间</summary>
            public static readonly Field LastLoginTime = FindByName(__.LastLoginTime);

            /// <summary>上次登录IP</summary>
            public static readonly Field LastLoginIP = FindByName(__.LastLoginIP);

            /// <summary>本次登录时间</summary>
            public static readonly Field ThisLoginTime = FindByName(__.ThisLoginTime);

            /// <summary>本次登录IP</summary>
            public static readonly Field ThisLoginIP = FindByName(__.ThisLoginIP);

            /// <summary>是否是锁定</summary>
            public static readonly Field IsLock = FindByName(__.IsLock);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得管理员字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>用户名</summary>
            public const String UserName = "UserName";

            /// <summary>密码</summary>
            public const String PassWord = "PassWord";

            /// <summary>盐值</summary>
            public const String Salt = "Salt";

            /// <summary>姓名</summary>
            public const String RealName = "RealName";

            /// <summary>电话</summary>
            public const String Tel = "Tel";

            /// <summary>邮件</summary>
            public const String Email = "Email";

            /// <summary>级别</summary>
            public const String UserLevel = "UserLevel";

            /// <summary>管理组</summary>
            public const String RoleId = "RoleId";

            /// <summary>用户组</summary>
            public const String GroupId = "GroupId";

            /// <summary>最后登录时间</summary>
            public const String LastLoginTime = "LastLoginTime";

            /// <summary>上次登录IP</summary>
            public const String LastLoginIP = "LastLoginIP";

            /// <summary>本次登录时间</summary>
            public const String ThisLoginTime = "ThisLoginTime";

            /// <summary>本次登录IP</summary>
            public const String ThisLoginIP = "ThisLoginIP";

            /// <summary>是否是锁定</summary>
            public const String IsLock = "IsLock";
        }
        #endregion
    }

    /// <summary>管理员接口</summary>
    public partial interface IAdmin
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>密码</summary>
        String PassWord { get; set; }

        /// <summary>盐值</summary>
        String Salt { get; set; }

        /// <summary>姓名</summary>
        String RealName { get; set; }

        /// <summary>电话</summary>
        String Tel { get; set; }

        /// <summary>邮件</summary>
        String Email { get; set; }

        /// <summary>级别</summary>
        Int32 UserLevel { get; set; }

        /// <summary>管理组</summary>
        Int32 RoleId { get; set; }

        /// <summary>用户组</summary>
        Int32 GroupId { get; set; }

        /// <summary>最后登录时间</summary>
        DateTime LastLoginTime { get; set; }

        /// <summary>上次登录IP</summary>
        String LastLoginIP { get; set; }

        /// <summary>本次登录时间</summary>
        DateTime ThisLoginTime { get; set; }

        /// <summary>本次登录IP</summary>
        String ThisLoginIP { get; set; }

        /// <summary>是否是锁定</summary>
        Int32 IsLock { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}