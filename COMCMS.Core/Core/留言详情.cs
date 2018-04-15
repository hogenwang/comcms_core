using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Guestbook : IGuestbook
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private Int32 _KId;
        /// <summary>分类ID</summary>
        [DisplayName("分类ID")]
        [Description("分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("KId", "分类ID", "int")]
        public Int32 KId { get { return _KId; } set { if (OnPropertyChanging(__.KId, value)) { _KId = value; OnPropertyChanged(__.KId); } } }

        private Int32 _UId;
        /// <summary>用户ID</summary>
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UId", "用户ID", "int")]
        public Int32 UId { get { return _UId; } set { if (OnPropertyChanging(__.UId, value)) { _UId = value; OnPropertyChanged(__.UId); } } }

        private String _Title;
        /// <summary>留言标题</summary>
        [DisplayName("留言标题")]
        [Description("留言标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "留言标题", "nvarchar(250)", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Content;
        /// <summary>详细介绍</summary>
        [DisplayName("详细介绍")]
        [Description("详细介绍")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Content", "详细介绍", "ntext")]
        public String Content { get { return _Content; } set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private DateTime _AddTime;
        /// <summary>添加时间</summary>
        [DisplayName("添加时间")]
        [Description("添加时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AddTime", "添加时间", "datetime")]
        public DateTime AddTime { get { return _AddTime; } set { if (OnPropertyChanging(__.AddTime, value)) { _AddTime = value; OnPropertyChanged(__.AddTime); } } }

        private Int32 _IsVerify;
        /// <summary>是否审核通过</summary>
        [DisplayName("是否审核通过")]
        [Description("是否审核通过")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerify", "是否审核通过", "int")]
        public Int32 IsVerify { get { return _IsVerify; } set { if (OnPropertyChanging(__.IsVerify, value)) { _IsVerify = value; OnPropertyChanged(__.IsVerify); } } }

        private Int32 _IsRead;
        /// <summary>是否阅读</summary>
        [DisplayName("是否阅读")]
        [Description("是否阅读")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRead", "是否阅读", "int")]
        public Int32 IsRead { get { return _IsRead; } set { if (OnPropertyChanging(__.IsRead, value)) { _IsRead = value; OnPropertyChanged(__.IsRead); } } }

        private Int32 _IsDel;
        /// <summary>是否删除,已经删除到回收站</summary>
        [DisplayName("是否删除")]
        [Description("是否删除,已经删除到回收站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsDel", "是否删除,已经删除到回收站", "int")]
        public Int32 IsDel { get { return _IsDel; } set { if (OnPropertyChanging(__.IsDel, value)) { _IsDel = value; OnPropertyChanged(__.IsDel); } } }

        private String _IP;
        /// <summary>用户IP</summary>
        [DisplayName("用户IP")]
        [Description("用户IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("IP", "用户IP", "nvarchar(20)")]
        public String IP { get { return _IP; } set { if (OnPropertyChanging(__.IP, value)) { _IP = value; OnPropertyChanged(__.IP); } } }

        private String _Nickname;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Nickname", "昵称", "nvarchar(50)")]
        public String Nickname { get { return _Nickname; } set { if (OnPropertyChanging(__.Nickname, value)) { _Nickname = value; OnPropertyChanged(__.Nickname); } } }

        private String _Email;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮箱", "nvarchar(100)")]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private String _Tel;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Tel", "电话", "nvarchar(50)")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _QQ;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("QQ", "QQ", "nvarchar(20)")]
        public String QQ { get { return _QQ; } set { if (OnPropertyChanging(__.QQ, value)) { _QQ = value; OnPropertyChanged(__.QQ); } } }

        private String _Skype;
        /// <summary>Skype</summary>
        [DisplayName("Skype")]
        [Description("Skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "Skype", "nvarchar(100)")]
        public String Skype { get { return _Skype; } set { if (OnPropertyChanging(__.Skype, value)) { _Skype = value; OnPropertyChanged(__.Skype); } } }

        private String _HomePage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("HomePage", "主页", "nvarchar(250)")]
        public String HomePage { get { return _HomePage; } set { if (OnPropertyChanging(__.HomePage, value)) { _HomePage = value; OnPropertyChanged(__.HomePage); } } }

        private String _Address;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "地址", "nvarchar(250)")]
        public String Address { get { return _Address; } set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Company", "公司", "nvarchar(250)")]
        public String Company { get { return _Company; } set { if (OnPropertyChanging(__.Company, value)) { _Company = value; OnPropertyChanged(__.Company); } } }

        private String _ReplyTitle;
        /// <summary>回复标题</summary>
        [DisplayName("回复标题")]
        [Description("回复标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("ReplyTitle", "回复标题", "nvarchar(250)")]
        public String ReplyTitle { get { return _ReplyTitle; } set { if (OnPropertyChanging(__.ReplyTitle, value)) { _ReplyTitle = value; OnPropertyChanged(__.ReplyTitle); } } }

        private String _ReplyContent;
        /// <summary>回复的详情</summary>
        [DisplayName("回复的详情")]
        [Description("回复的详情")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("ReplyContent", "回复的详情", "ntext")]
        public String ReplyContent { get { return _ReplyContent; } set { if (OnPropertyChanging(__.ReplyContent, value)) { _ReplyContent = value; OnPropertyChanged(__.ReplyContent); } } }

        private DateTime _ReplyAddTime;
        /// <summary>回复时间</summary>
        [DisplayName("回复时间")]
        [Description("回复时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("ReplyAddTime", "回复时间", "datetime")]
        public DateTime ReplyAddTime { get { return _ReplyAddTime; } set { if (OnPropertyChanging(__.ReplyAddTime, value)) { _ReplyAddTime = value; OnPropertyChanged(__.ReplyAddTime); } } }

        private String _ReplyIP;
        /// <summary>用户回复IP</summary>
        [DisplayName("用户回复IP")]
        [Description("用户回复IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ReplyIP", "用户回复IP", "nvarchar(20)")]
        public String ReplyIP { get { return _ReplyIP; } set { if (OnPropertyChanging(__.ReplyIP, value)) { _ReplyIP = value; OnPropertyChanged(__.ReplyIP); } } }

        private Int32 _ReplyAdminId;
        /// <summary>用户的管理员ID</summary>
        [DisplayName("用户的管理员ID")]
        [Description("用户的管理员ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ReplyAdminId", "用户的管理员ID", "int")]
        public Int32 ReplyAdminId { get { return _ReplyAdminId; } set { if (OnPropertyChanging(__.ReplyAdminId, value)) { _ReplyAdminId = value; OnPropertyChanged(__.ReplyAdminId); } } }

        private String _ReplyNickName;
        /// <summary>回复者昵称</summary>
        [DisplayName("回复者昵称")]
        [Description("回复者昵称")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("ReplyNickName", "回复者昵称", "nvarchar(20)")]
        public String ReplyNickName { get { return _ReplyNickName; } set { if (OnPropertyChanging(__.ReplyNickName, value)) { _ReplyNickName = value; OnPropertyChanged(__.ReplyNickName); } } }
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
                    case __.KId : return _KId;
                    case __.UId : return _UId;
                    case __.Title : return _Title;
                    case __.Content : return _Content;
                    case __.AddTime : return _AddTime;
                    case __.IsVerify : return _IsVerify;
                    case __.IsRead : return _IsRead;
                    case __.IsDel : return _IsDel;
                    case __.IP : return _IP;
                    case __.Nickname : return _Nickname;
                    case __.Email : return _Email;
                    case __.Tel : return _Tel;
                    case __.QQ : return _QQ;
                    case __.Skype : return _Skype;
                    case __.HomePage : return _HomePage;
                    case __.Address : return _Address;
                    case __.Company : return _Company;
                    case __.ReplyTitle : return _ReplyTitle;
                    case __.ReplyContent : return _ReplyContent;
                    case __.ReplyAddTime : return _ReplyAddTime;
                    case __.ReplyIP : return _ReplyIP;
                    case __.ReplyAdminId : return _ReplyAdminId;
                    case __.ReplyNickName : return _ReplyNickName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.KId : _KId = Convert.ToInt32(value); break;
                    case __.UId : _UId = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.AddTime : _AddTime = Convert.ToDateTime(value); break;
                    case __.IsVerify : _IsVerify = Convert.ToInt32(value); break;
                    case __.IsRead : _IsRead = Convert.ToInt32(value); break;
                    case __.IsDel : _IsDel = Convert.ToInt32(value); break;
                    case __.IP : _IP = Convert.ToString(value); break;
                    case __.Nickname : _Nickname = Convert.ToString(value); break;
                    case __.Email : _Email = Convert.ToString(value); break;
                    case __.Tel : _Tel = Convert.ToString(value); break;
                    case __.QQ : _QQ = Convert.ToString(value); break;
                    case __.Skype : _Skype = Convert.ToString(value); break;
                    case __.HomePage : _HomePage = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Company : _Company = Convert.ToString(value); break;
                    case __.ReplyTitle : _ReplyTitle = Convert.ToString(value); break;
                    case __.ReplyContent : _ReplyContent = Convert.ToString(value); break;
                    case __.ReplyAddTime : _ReplyAddTime = Convert.ToDateTime(value); break;
                    case __.ReplyIP : _ReplyIP = Convert.ToString(value); break;
                    case __.ReplyAdminId : _ReplyAdminId = Convert.ToInt32(value); break;
                    case __.ReplyNickName : _ReplyNickName = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>用户ID</summary>
            public static readonly Field UId = FindByName(__.UId);

            /// <summary>留言标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>详细介绍</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>添加时间</summary>
            public static readonly Field AddTime = FindByName(__.AddTime);

            /// <summary>是否审核通过</summary>
            public static readonly Field IsVerify = FindByName(__.IsVerify);

            /// <summary>是否阅读</summary>
            public static readonly Field IsRead = FindByName(__.IsRead);

            /// <summary>是否删除,已经删除到回收站</summary>
            public static readonly Field IsDel = FindByName(__.IsDel);

            /// <summary>用户IP</summary>
            public static readonly Field IP = FindByName(__.IP);

            /// <summary>昵称</summary>
            public static readonly Field Nickname = FindByName(__.Nickname);

            /// <summary>邮箱</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>电话</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>QQ</summary>
            public static readonly Field QQ = FindByName(__.QQ);

            /// <summary>Skype</summary>
            public static readonly Field Skype = FindByName(__.Skype);

            /// <summary>主页</summary>
            public static readonly Field HomePage = FindByName(__.HomePage);

            /// <summary>地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName(__.Company);

            /// <summary>回复标题</summary>
            public static readonly Field ReplyTitle = FindByName(__.ReplyTitle);

            /// <summary>回复的详情</summary>
            public static readonly Field ReplyContent = FindByName(__.ReplyContent);

            /// <summary>回复时间</summary>
            public static readonly Field ReplyAddTime = FindByName(__.ReplyAddTime);

            /// <summary>用户回复IP</summary>
            public static readonly Field ReplyIP = FindByName(__.ReplyIP);

            /// <summary>用户的管理员ID</summary>
            public static readonly Field ReplyAdminId = FindByName(__.ReplyAdminId);

            /// <summary>回复者昵称</summary>
            public static readonly Field ReplyNickName = FindByName(__.ReplyNickName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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
            public const String IP = "IP";

            /// <summary>昵称</summary>
            public const String Nickname = "Nickname";

            /// <summary>邮箱</summary>
            public const String Email = "Email";

            /// <summary>电话</summary>
            public const String Tel = "Tel";

            /// <summary>QQ</summary>
            public const String QQ = "QQ";

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

    /// <summary>留言详情接口</summary>
    public partial interface IGuestbook
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>分类ID</summary>
        Int32 KId { get; set; }

        /// <summary>用户ID</summary>
        Int32 UId { get; set; }

        /// <summary>留言标题</summary>
        String Title { get; set; }

        /// <summary>详细介绍</summary>
        String Content { get; set; }

        /// <summary>添加时间</summary>
        DateTime AddTime { get; set; }

        /// <summary>是否审核通过</summary>
        Int32 IsVerify { get; set; }

        /// <summary>是否阅读</summary>
        Int32 IsRead { get; set; }

        /// <summary>是否删除,已经删除到回收站</summary>
        Int32 IsDel { get; set; }

        /// <summary>用户IP</summary>
        String IP { get; set; }

        /// <summary>昵称</summary>
        String Nickname { get; set; }

        /// <summary>邮箱</summary>
        String Email { get; set; }

        /// <summary>电话</summary>
        String Tel { get; set; }

        /// <summary>QQ</summary>
        String QQ { get; set; }

        /// <summary>Skype</summary>
        String Skype { get; set; }

        /// <summary>主页</summary>
        String HomePage { get; set; }

        /// <summary>地址</summary>
        String Address { get; set; }

        /// <summary>公司</summary>
        String Company { get; set; }

        /// <summary>回复标题</summary>
        String ReplyTitle { get; set; }

        /// <summary>回复的详情</summary>
        String ReplyContent { get; set; }

        /// <summary>回复时间</summary>
        DateTime ReplyAddTime { get; set; }

        /// <summary>用户回复IP</summary>
        String ReplyIP { get; set; }

        /// <summary>用户的管理员ID</summary>
        Int32 ReplyAdminId { get; set; }

        /// <summary>回复者昵称</summary>
        String ReplyNickName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}