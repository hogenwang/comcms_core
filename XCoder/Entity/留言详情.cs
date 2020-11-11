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
    /// <summary>留言详情</summary>
    [Serializable]
    [DataObject]
    [Description("留言详情")]
    [BindTable("Guestbook", Description = "留言详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Guestbook
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private Int32 _KId;
        /// <summary>分类ID</summary>
        [DisplayName("分类ID")]
        [Description("分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "分类ID", "")]
        public Int32 KId { get => _KId; set { if (OnPropertyChanging("KId", value)) { _KId = value; OnPropertyChanged("KId"); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "")]
        public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

        private String _Title;
        /// <summary>留言标题</summary>
        [DisplayName("留言标题")]
        [Description("留言标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "留言标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _Content;
        /// <summary>详细介绍</summary>
        [DisplayName("详细介绍")]
        [Description("详细介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "详细介绍", "")]
        public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "")]
        public DateTime AddTime { get => _AddTime; set { if (OnPropertyChanging("AddTime", value)) { _AddTime = value; OnPropertyChanged("AddTime"); } } }

        private Int32 _IsVerify;
        /// <summary>是否审核通过</summary>
        [DisplayName("是否审核通过")]
        [Description("是否审核通过")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否审核通过", "")]
        public Int32 IsVerify { get => _IsVerify; set { if (OnPropertyChanging("IsVerify", value)) { _IsVerify = value; OnPropertyChanged("IsVerify"); } } }

        private Int32 _IsRead;
        /// <summary>是否阅读</summary>
        [DisplayName("是否阅读")]
        [Description("是否阅读")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRead", "是否阅读", "")]
        public Int32 IsRead { get => _IsRead; set { if (OnPropertyChanging("IsRead", value)) { _IsRead = value; OnPropertyChanged("IsRead"); } } }

        private Int32 _IsDel;
        /// <summary>是否删除,已经删除到回收站</summary>
        [DisplayName("是否删除")]
        [Description("是否删除,已经删除到回收站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除,已经删除到回收站", "")]
        public Int32 IsDel { get => _IsDel; set { if (OnPropertyChanging("IsDel", value)) { _IsDel = value; OnPropertyChanged("IsDel"); } } }

        private String _Ip;
        /// <summary>用户IP</summary>
        [DisplayName("用户IP")]
        [Description("用户IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Ip", "用户IP", "")]
        public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

        private String _Nickname;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Nickname", "昵称", "")]
        public String Nickname { get => _Nickname; set { if (OnPropertyChanging("Nickname", value)) { _Nickname = value; OnPropertyChanged("Nickname"); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "")]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Tel", "电话", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Qq;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Qq", "QQ", "")]
        public String Qq { get => _Qq; set { if (OnPropertyChanging("Qq", value)) { _Qq = value; OnPropertyChanged("Qq"); } } }

        private String _Skype;
        /// <summary>Skype</summary>
        [DisplayName("Skype")]
        [Description("Skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "Skype", "")]
        public String Skype { get => _Skype; set { if (OnPropertyChanging("Skype", value)) { _Skype = value; OnPropertyChanged("Skype"); } } }

        private String _HomePage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("HomePage", "主页", "")]
        public String HomePage { get => _HomePage; set { if (OnPropertyChanging("HomePage", value)) { _HomePage = value; OnPropertyChanged("HomePage"); } } }

        private String _Address;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "地址", "")]
        public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Company", "公司", "")]
        public String Company { get => _Company; set { if (OnPropertyChanging("Company", value)) { _Company = value; OnPropertyChanged("Company"); } } }

        private String _ReplyTitle;
        /// <summary>回复标题</summary>
        [DisplayName("回复标题")]
        [Description("回复标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("ReplyTitle", "回复标题", "")]
        public String ReplyTitle { get => _ReplyTitle; set { if (OnPropertyChanging("ReplyTitle", value)) { _ReplyTitle = value; OnPropertyChanged("ReplyTitle"); } } }

        private String _ReplyContent;
        /// <summary>回复的详情</summary>
        [DisplayName("回复的详情")]
        [Description("回复的详情")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ReplyContent", "回复的详情", "")]
        public String ReplyContent { get => _ReplyContent; set { if (OnPropertyChanging("ReplyContent", value)) { _ReplyContent = value; OnPropertyChanged("ReplyContent"); } } }

        private DateTime _ReplyAddTime;
        /// <summary>回复时间</summary>
        [DisplayName("回复时间")]
        [Description("回复时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ReplyAddTime", "回复时间", "")]
        public DateTime ReplyAddTime { get => _ReplyAddTime; set { if (OnPropertyChanging("ReplyAddTime", value)) { _ReplyAddTime = value; OnPropertyChanged("ReplyAddTime"); } } }

        private String _ReplyIP;
        /// <summary>用户回复IP</summary>
        [DisplayName("用户回复IP")]
        [Description("用户回复IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ReplyIP", "用户回复IP", "")]
        public String ReplyIP { get => _ReplyIP; set { if (OnPropertyChanging("ReplyIP", value)) { _ReplyIP = value; OnPropertyChanged("ReplyIP"); } } }

        private Int32 _ReplyAdminId;
        /// <summary>用户的管理员ID</summary>
        [DisplayName("用户的管理员ID")]
        [Description("用户的管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ReplyAdminId", "用户的管理员ID", "")]
        public Int32 ReplyAdminId { get => _ReplyAdminId; set { if (OnPropertyChanging("ReplyAdminId", value)) { _ReplyAdminId = value; OnPropertyChanged("ReplyAdminId"); } } }

        private String _ReplyNickName;
        /// <summary>回复者昵称</summary>
        [DisplayName("回复者昵称")]
        [Description("回复者昵称")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ReplyNickName", "回复者昵称", "")]
        public String ReplyNickName { get => _ReplyNickName; set { if (OnPropertyChanging("ReplyNickName", value)) { _ReplyNickName = value; OnPropertyChanged("ReplyNickName"); } } }
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
                    case "KId": return _KId;
                    case "UId": return _UId;
                    case "Title": return _Title;
                    case "Content": return _Content;
                    case "AddTime": return _AddTime;
                    case "IsVerify": return _IsVerify;
                    case "IsRead": return _IsRead;
                    case "IsDel": return _IsDel;
                    case "Ip": return _Ip;
                    case "Nickname": return _Nickname;
                    case "Email": return _Email;
                    case "Tel": return _Tel;
                    case "Qq": return _Qq;
                    case "Skype": return _Skype;
                    case "HomePage": return _HomePage;
                    case "Address": return _Address;
                    case "Company": return _Company;
                    case "ReplyTitle": return _ReplyTitle;
                    case "ReplyContent": return _ReplyContent;
                    case "ReplyAddTime": return _ReplyAddTime;
                    case "ReplyIP": return _ReplyIP;
                    case "ReplyAdminId": return _ReplyAdminId;
                    case "ReplyNickName": return _ReplyNickName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "KId": _KId = value.ToInt(); break;
                    case "UId": _UId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "Content": _Content = Convert.ToString(value); break;
                    case "AddTime": _AddTime = value.ToDateTime(); break;
                    case "IsVerify": _IsVerify = value.ToInt(); break;
                    case "IsRead": _IsRead = value.ToInt(); break;
                    case "IsDel": _IsDel = value.ToInt(); break;
                    case "Ip": _Ip = Convert.ToString(value); break;
                    case "Nickname": _Nickname = Convert.ToString(value); break;
                    case "Email": _Email = Convert.ToString(value); break;
                    case "Tel": _Tel = Convert.ToString(value); break;
                    case "Qq": _Qq = Convert.ToString(value); break;
                    case "Skype": _Skype = Convert.ToString(value); break;
                    case "HomePage": _HomePage = Convert.ToString(value); break;
                    case "Address": _Address = Convert.ToString(value); break;
                    case "Company": _Company = Convert.ToString(value); break;
                    case "ReplyTitle": _ReplyTitle = Convert.ToString(value); break;
                    case "ReplyContent": _ReplyContent = Convert.ToString(value); break;
                    case "ReplyAddTime": _ReplyAddTime = value.ToDateTime(); break;
                    case "ReplyIP": _ReplyIP = Convert.ToString(value); break;
                    case "ReplyAdminId": _ReplyAdminId = value.ToInt(); break;
                    case "ReplyNickName": _ReplyNickName = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得留言详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName("KId");

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName("UId");

            /// <summary>留言标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>详细介绍</summary>
            public static readonly Field Content = FindByName("Content");

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName("AddTime");

            /// <summary>是否审核通过</summary>
            public static readonly Field IsVerify = FindByName("IsVerify");

            /// <summary>是否阅读</summary>
            public static readonly Field IsRead = FindByName("IsRead");

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName("IsDel");

            /// <summary>用户IP</summary>
            public static readonly Field Ip = FindByName("Ip");

            /// <summary>昵称</summary>
            public static readonly Field Nickname = FindByName("Nickname");

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>QQ</summary>
            public static readonly Field Qq = FindByName("Qq");

            /// <summary>Skype</summary>
            public static readonly Field Skype = FindByName("Skype");

            /// <summary>主页</summary>
            public static readonly Field HomePage = FindByName("HomePage");

            /// <summary>地址</summary>
            public static readonly Field Address = FindByName("Address");

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName("Company");

            /// <summary>回复标题</summary>
            public static readonly Field ReplyTitle = FindByName("ReplyTitle");

            /// <summary>回复的详情</summary>
            public static readonly Field ReplyContent = FindByName("ReplyContent");

            /// <summary>回复时间</summary>
            public static readonly Field ReplyAddTime = FindByName("ReplyAddTime");

            /// <summary>用户回复IP</summary>
            public static readonly Field ReplyIP = FindByName("ReplyIP");

            /// <summary>用户的管理员ID</summary>
            public static readonly Field ReplyAdminId = FindByName("ReplyAdminId");

            /// <summary>回复者昵称</summary>
            public static readonly Field ReplyNickName = FindByName("ReplyNickName");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得留言详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>分类ID</summary>
            public const String KId = "KId";

            /// <summary>用户ID</summary>
            public const String UId = "UId";

            /// <summary>留言标题</summary>
            public const String Title = "Title";

            /// <summary>详细介绍</summary>
            public const String Content = "Content";

            /// <summary>添加时间</summary>
            public const String AddTime = "AddTime";

            /// <summary>是否审核通过</summary>
            public const String IsVerify = "IsVerify";

            /// <summary>是否阅读</summary>
            public const String IsRead = "IsRead";

            /// <summary>是否删除,已经删除到回收站</summary>
            public const String IsDel = "IsDel";

            /// <summary>用户IP</summary>
            public const String Ip = "Ip";

            /// <summary>昵称</summary>
            public const String Nickname = "Nickname";

            /// <summary>邮箱</summary>
            public const String Email = "Email";

            /// <summary>电话</summary>
            public const String Tel = "Tel";

            /// <summary>QQ</summary>
            public const String Qq = "Qq";

            /// <summary>Skype</summary>
            public const String Skype = "Skype";

            /// <summary>主页</summary>
            public const String HomePage = "HomePage";

            /// <summary>地址</summary>
            public const String Address = "Address";

            /// <summary>公司</summary>
            public const String Company = "Company";

            /// <summary>回复标题</summary>
            public const String ReplyTitle = "ReplyTitle";

            /// <summary>回复的详情</summary>
            public const String ReplyContent = "ReplyContent";

            /// <summary>回复时间</summary>
            public const String ReplyAddTime = "ReplyAddTime";

            /// <summary>用户回复IP</summary>
            public const String ReplyIP = "ReplyIP";

            /// <summary>用户的管理员ID</summary>
            public const String ReplyAdminId = "ReplyAdminId";

            /// <summary>回复者昵称</summary>
            public const String ReplyNickName = "ReplyNickName";
        }
        #endregion
    }
}