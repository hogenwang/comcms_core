using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    /// <summary>友情连接详情</summary>
    [Serializable]
    [DataObject]
    [Description("友情连接详情")]
    [BindTable("Link", Description = "友情连接详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Link : ILink
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

        private String _Title;
        /// <summary>站点标题</summary>
        [DisplayName("站点标题")]
        [Description("站点标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "站点标题", "nvarchar(250)", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _LinkURL;
        /// <summary>站点连接</summary>
        [DisplayName("站点连接")]
        [Description("站点连接")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkURL", "站点连接", "nvarchar(250)")]
        public String LinkURL { get { return _LinkURL; } set { if (OnPropertyChanging(__.LinkURL, value)) { _LinkURL = value; OnPropertyChanged(__.LinkURL); } } }

        private String _Info;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Info", "介绍", "nvarchar(250)")]
        public String Info { get { return _Info; } set { if (OnPropertyChanging(__.Info, value)) { _Info = value; OnPropertyChanged(__.Info); } } }

        private String _Logo;
        /// <summary>站点LOGO</summary>
        [DisplayName("站点LOGO")]
        [Description("站点LOGO")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Logo", "站点LOGO", "nvarchar(250)")]
        public String Logo { get { return _Logo; } set { if (OnPropertyChanging(__.Logo, value)) { _Logo = value; OnPropertyChanged(__.Logo); } } }

        private Boolean _IsHide;
        /// <summary>是否隐藏友情链接</summary>
        [DisplayName("是否隐藏友情链接")]
        [Description("是否隐藏友情链接")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏友情链接", "bit")]
        public Boolean IsHide { get { return _IsHide; } set { if (OnPropertyChanging(__.IsHide, value)) { _IsHide = value; OnPropertyChanged(__.IsHide); } } }

        private Int32 _Sequence;
        /// <summary>排序，默认999</summary>
        [DisplayName("排序")]
        [Description("排序，默认999")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序，默认999", "int")]
        public Int32 Sequence { get { return _Sequence; } set { if (OnPropertyChanging(__.Sequence, value)) { _Sequence = value; OnPropertyChanged(__.Sequence); } } }
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
                    case __.Title : return _Title;
                    case __.LinkURL : return _LinkURL;
                    case __.Info : return _Info;
                    case __.Logo : return _Logo;
                    case __.IsHide : return _IsHide;
                    case __.Sequence : return _Sequence;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.KId : _KId = Convert.ToInt32(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.LinkURL : _LinkURL = Convert.ToString(value); break;
                    case __.Info : _Info = Convert.ToString(value); break;
                    case __.Logo : _Logo = Convert.ToString(value); break;
                    case __.IsHide : _IsHide = Convert.ToBoolean(value); break;
                    case __.Sequence : _Sequence = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得友情连接详情字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName(__.KId);

            /// <summary>站点标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>站点连接</summary>
            public static readonly Field LinkURL = FindByName(__.LinkURL);

            /// <summary>介绍</summary>
            public static readonly Field Info = FindByName(__.Info);

            /// <summary>站点LOGO</summary>
            public static readonly Field Logo = FindByName(__.Logo);

            /// <summary>是否隐藏友情链接</summary>
            public static readonly Field IsHide = FindByName(__.IsHide);

            /// <summary>排序，默认999</summary>
            public static readonly Field Sequence = FindByName(__.Sequence);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得友情连接详情字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>分类ID</summary>
            public const String KId = "KId";

            /// <summary>站点标题</summary>
            public const String Title = "Title";

            /// <summary>站点连接</summary>
            public const String LinkURL = "LinkURL";

            /// <summary>介绍</summary>
            public const String Info = "Info";

            /// <summary>站点LOGO</summary>
            public const String Logo = "Logo";

            /// <summary>是否隐藏友情链接</summary>
            public const String IsHide = "IsHide";

            /// <summary>排序，默认999</summary>
            public const String Sequence = "Sequence";
        }
        #endregion
    }

    /// <summary>友情连接详情接口</summary>
    public partial interface ILink
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>分类ID</summary>
        Int32 KId { get; set; }

        /// <summary>站点标题</summary>
        String Title { get; set; }

        /// <summary>站点连接</summary>
        String LinkURL { get; set; }

        /// <summary>介绍</summary>
        String Info { get; set; }

        /// <summary>站点LOGO</summary>
        String Logo { get; set; }

        /// <summary>是否隐藏友情链接</summary>
        Boolean IsHide { get; set; }

        /// <summary>排序，默认999</summary>
        Int32 Sequence { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}