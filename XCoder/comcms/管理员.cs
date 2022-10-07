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
    /// <summary>管理员</summary>
    [Serializable]
    [DataObject]
    [Description("管理员")]
    [BindTable("Admin", Description = "管理员", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Admin
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("UserName", "用户名", "", Master = true)]
        public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

        private String _PassWord;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PassWord", "密码", "")]
        public String PassWord { get => _PassWord; set { if (OnPropertyChanging("PassWord", value)) { _PassWord = value; OnPropertyChanged("PassWord"); } } }

        private String _Salt;
        /// <summary>盐值</summary>
        [DisplayName("盐值")]
        [Description("盐值")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Salt", "盐值", "")]
        public String Salt { get => _Salt; set { if (OnPropertyChanging("Salt", value)) { _Salt = value; OnPropertyChanged("Salt"); } } }

        private String _RealName;
        /// <summary>姓名</summary>
        [DisplayName("姓名")]
        [Description("姓名")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RealName", "姓名", "")]
        public String RealName { get => _RealName; set { if (OnPropertyChanging("RealName", value)) { _RealName = value; OnPropertyChanged("RealName"); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "电话", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "", Master = true)]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private Int32 _UserLevel;
        /// <summary>级别</summary>
        [DisplayName("级别")]
        [Description("级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UserLevel", "级别", "")]
        public Int32 UserLevel { get => _UserLevel; set { if (OnPropertyChanging("UserLevel", value)) { _UserLevel = value; OnPropertyChanged("UserLevel"); } } }

        private Int32 _RoleId;
        /// <summary>管理组</summary>
        [DisplayName("管理组")]
        [Description("管理组")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleId", "管理组", "", Master = true)]
        public Int32 RoleId { get => _RoleId; set { if (OnPropertyChanging("RoleId", value)) { _RoleId = value; OnPropertyChanged("RoleId"); } } }

        private Int32 _GroupId;
        /// <summary>用户组</summary>
        [DisplayName("用户组")]
        [Description("用户组")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GroupId", "用户组", "", Master = true)]
        public Int32 GroupId { get => _GroupId; set { if (OnPropertyChanging("GroupId", value)) { _GroupId = value; OnPropertyChanged("GroupId"); } } }

        private DateTime _LastLoginTime;
        /// <summary>最后登录时间</summary>
        [DisplayName("最后登录时间")]
        [Description("最后登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastLoginTime", "最后登录时间", "")]
        public DateTime LastLoginTime { get => _LastLoginTime; set { if (OnPropertyChanging("LastLoginTime", value)) { _LastLoginTime = value; OnPropertyChanged("LastLoginTime"); } } }

        private String _LastLoginIP;
        /// <summary>上次登录IP</summary>
        [DisplayName("上次登录IP")]
        [Description("上次登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LastLoginIP", "上次登录IP", "")]
        public String LastLoginIP { get => _LastLoginIP; set { if (OnPropertyChanging("LastLoginIP", value)) { _LastLoginIP = value; OnPropertyChanged("LastLoginIP"); } } }

        private DateTime _ThisLoginTime;
        /// <summary>本次登录时间</summary>
        [DisplayName("本次登录时间")]
        [Description("本次登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ThisLoginTime", "本次登录时间", "")]
        public DateTime ThisLoginTime { get => _ThisLoginTime; set { if (OnPropertyChanging("ThisLoginTime", value)) { _ThisLoginTime = value; OnPropertyChanged("ThisLoginTime"); } } }

        private String _ThisLoginIP;
        /// <summary>本次登录IP</summary>
        [DisplayName("本次登录IP")]
        [Description("本次登录IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ThisLoginIP", "本次登录IP", "")]
        public String ThisLoginIP { get => _ThisLoginIP; set { if (OnPropertyChanging("ThisLoginIP", value)) { _ThisLoginIP = value; OnPropertyChanged("ThisLoginIP"); } } }

        private Int32 _IsLock;
        /// <summary>是否是锁定</summary>
        [DisplayName("是否是锁定")]
        [Description("是否是锁定")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsLock", "是否是锁定", "")]
        public Int32 IsLock { get => _IsLock; set { if (OnPropertyChanging("IsLock", value)) { _IsLock = value; OnPropertyChanged("IsLock"); } } }
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
                    case "UserName": return _UserName;
                    case "PassWord": return _PassWord;
                    case "Salt": return _Salt;
                    case "RealName": return _RealName;
                    case "Tel": return _Tel;
                    case "Email": return _Email;
                    case "UserLevel": return _UserLevel;
                    case "RoleId": return _RoleId;
                    case "GroupId": return _GroupId;
                    case "LastLoginTime": return _LastLoginTime;
                    case "LastLoginIP": return _LastLoginIP;
                    case "ThisLoginTime": return _ThisLoginTime;
                    case "ThisLoginIP": return _ThisLoginIP;
                    case "IsLock": return _IsLock;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "UserName": _UserName = Convert.ToString(value); break;
                    case "PassWord": _PassWord = Convert.ToString(value); break;
                    case "Salt": _Salt = Convert.ToString(value); break;
                    case "RealName": _RealName = Convert.ToString(value); break;
                    case "Tel": _Tel = Convert.ToString(value); break;
                    case "Email": _Email = Convert.ToString(value); break;
                    case "UserLevel": _UserLevel = value.ToInt(); break;
                    case "RoleId": _RoleId = value.ToInt(); break;
                    case "GroupId": _GroupId = value.ToInt(); break;
                    case "LastLoginTime": _LastLoginTime = value.ToDateTime(); break;
                    case "LastLoginIP": _LastLoginIP = Convert.ToString(value); break;
                    case "ThisLoginTime": _ThisLoginTime = value.ToDateTime(); break;
                    case "ThisLoginIP": _ThisLoginIP = Convert.ToString(value); break;
                    case "IsLock": _IsLock = value.ToInt(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName("UserName");

            /// <summary>密码</summary>
            public static readonly Field PassWord = FindByName("PassWord");

            /// <summary>盐值</summary>
            public static readonly Field Salt = FindByName("Salt");

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName("RealName");

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>级别</summary>
            public static readonly Field UserLevel = FindByName("UserLevel");

            /// <summary>管理组</summary>
            public static readonly Field RoleId = FindByName("RoleId");

            /// <summary>用户组</summary>
            public static readonly Field GroupId = FindByName("GroupId");

            /// <summary>最后登录时间</summary>
            public static readonly Field LastLoginTime = FindByName("LastLoginTime");

            /// <summary>上次登录IP</summary>
            public static readonly Field LastLoginIP = FindByName("LastLoginIP");

            /// <summary>本次登录时间</summary>
            public static readonly Field ThisLoginTime = FindByName("ThisLoginTime");

            /// <summary>本次登录IP</summary>
            public static readonly Field ThisLoginIP = FindByName("ThisLoginIP");

            /// <summary>是否是锁定</summary>
            public static readonly Field IsLock = FindByName("IsLock");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}