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
    /// <summary>友情连接详情</summary>
    [Serializable]
    [DataObject]
    [Description("友情连接详情")]
    [BindTable("Link", Description = "友情连接详情", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Link
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

        private String _Title;
        /// <summary>站点标题</summary>
        [DisplayName("站点标题")]
        [Description("站点标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Title", "站点标题", "", Master = true)]
        public String Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

        private String _LinkURL;
        /// <summary>站点连接</summary>
        [DisplayName("站点连接")]
        [Description("站点连接")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("LinkURL", "站点连接", "")]
        public String LinkURL { get => _LinkURL; set { if (OnPropertyChanging("LinkURL", value)) { _LinkURL = value; OnPropertyChanged("LinkURL"); } } }

        private String _Info;
        /// <summary>介绍</summary>
        [DisplayName("介绍")]
        [Description("介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Info", "介绍", "")]
        public String Info { get => _Info; set { if (OnPropertyChanging("Info", value)) { _Info = value; OnPropertyChanged("Info"); } } }

        private String _Logo;
        /// <summary>站点LOGO</summary>
        [DisplayName("站点LOGO")]
        [Description("站点LOGO")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Logo", "站点LOGO", "")]
        public String Logo { get => _Logo; set { if (OnPropertyChanging("Logo", value)) { _Logo = value; OnPropertyChanged("Logo"); } } }

        private Boolean _IsHide;
        /// <summary>是否隐藏友情链接</summary>
        [DisplayName("是否隐藏友情链接")]
        [Description("是否隐藏友情链接")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsHide", "是否隐藏友情链接", "")]
        public Boolean IsHide { get => _IsHide; set { if (OnPropertyChanging("IsHide", value)) { _IsHide = value; OnPropertyChanged("IsHide"); } } }

        private Int32 _Sequence;
        /// <summary>排序，默认999</summary>
        [DisplayName("排序")]
        [Description("排序，默认999")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sequence", "排序，默认999", "")]
        public Int32 Sequence { get => _Sequence; set { if (OnPropertyChanging("Sequence", value)) { _Sequence = value; OnPropertyChanged("Sequence"); } } }
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
                    case "Title": return _Title;
                    case "LinkURL": return _LinkURL;
                    case "Info": return _Info;
                    case "Logo": return _Logo;
                    case "IsHide": return _IsHide;
                    case "Sequence": return _Sequence;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "KId": _KId = value.ToInt(); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "LinkURL": _LinkURL = Convert.ToString(value); break;
                    case "Info": _Info = Convert.ToString(value); break;
                    case "Logo": _Logo = Convert.ToString(value); break;
                    case "IsHide": _IsHide = value.ToBoolean(); break;
                    case "Sequence": _Sequence = value.ToInt(); break;
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
            public static readonly Field Id = FindByName("Id");

            /// <summary>分类ID</summary>
            public static readonly Field KId = FindByName("KId");

            /// <summary>站点标题</summary>
            public static readonly Field Title = FindByName("Title");

            /// <summary>站点连接</summary>
            public static readonly Field LinkURL = FindByName("LinkURL");

            /// <summary>介绍</summary>
            public static readonly Field Info = FindByName("Info");

            /// <summary>站点LOGO</summary>
            public static readonly Field Logo = FindByName("Logo");

            /// <summary>是否隐藏友情链接</summary>
            public static readonly Field IsHide = FindByName("IsHide");

            /// <summary>排序，默认999</summary>
            public static readonly Field Sequence = FindByName("Sequence");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
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
}