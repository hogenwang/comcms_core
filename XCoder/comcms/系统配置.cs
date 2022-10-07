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
    /// <summary>系统配置</summary>
    [Serializable]
    [DataObject]
    [Description("系统配置")]
    [BindTable("Config", Description = "系统配置", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Config
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _SiteName;
        /// <summary>站点名称</summary>
        [DisplayName("站点名称")]
        [Description("站点名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SiteName", "站点名称", "")]
        public String SiteName { get => _SiteName; set { if (OnPropertyChanging("SiteName", value)) { _SiteName = value; OnPropertyChanged("SiteName"); } } }

        private String _SiteUrl;
        /// <summary>站点URL</summary>
        [DisplayName("站点URL")]
        [Description("站点URL")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SiteUrl", "站点URL", "")]
        public String SiteUrl { get => _SiteUrl; set { if (OnPropertyChanging("SiteUrl", value)) { _SiteUrl = value; OnPropertyChanged("SiteUrl"); } } }

        private String _SiteLogo;
        /// <summary>站点LOGO</summary>
        [DisplayName("站点LOGO")]
        [Description("站点LOGO")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SiteLogo", "站点LOGO", "")]
        public String SiteLogo { get => _SiteLogo; set { if (OnPropertyChanging("SiteLogo", value)) { _SiteLogo = value; OnPropertyChanged("SiteLogo"); } } }

        private String _Icp;
        /// <summary>ICP备案</summary>
        [DisplayName("ICP备案")]
        [Description("ICP备案")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Icp", "ICP备案", "")]
        public String Icp { get => _Icp; set { if (OnPropertyChanging("Icp", value)) { _Icp = value; OnPropertyChanged("Icp"); } } }

        private String _SiteEmail;
        /// <summary>联系我们Email</summary>
        [DisplayName("联系我们Email")]
        [Description("联系我们Email")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("SiteEmail", "联系我们Email", "")]
        public String SiteEmail { get => _SiteEmail; set { if (OnPropertyChanging("SiteEmail", value)) { _SiteEmail = value; OnPropertyChanged("SiteEmail"); } } }

        private String _SiteTel;
        /// <summary>网站电话</summary>
        [DisplayName("网站电话")]
        [Description("网站电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SiteTel", "网站电话", "")]
        public String SiteTel { get => _SiteTel; set { if (OnPropertyChanging("SiteTel", value)) { _SiteTel = value; OnPropertyChanged("SiteTel"); } } }

        private String _Copyright;
        /// <summary>版权所有</summary>
        [DisplayName("版权所有")]
        [Description("版权所有")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Copyright", "版权所有", "")]
        public String Copyright { get => _Copyright; set { if (OnPropertyChanging("Copyright", value)) { _Copyright = value; OnPropertyChanged("Copyright"); } } }

        private Boolean _IsCloseSite;
        /// <summary>是否关闭网站</summary>
        [DisplayName("是否关闭网站")]
        [Description("是否关闭网站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsCloseSite", "是否关闭网站", "")]
        public Boolean IsCloseSite { get => _IsCloseSite; set { if (OnPropertyChanging("IsCloseSite", value)) { _IsCloseSite = value; OnPropertyChanged("IsCloseSite"); } } }

        private String _CloseReason;
        /// <summary>关闭原因</summary>
        [DisplayName("关闭原因")]
        [Description("关闭原因")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("CloseReason", "关闭原因", "")]
        public String CloseReason { get => _CloseReason; set { if (OnPropertyChanging("CloseReason", value)) { _CloseReason = value; OnPropertyChanged("CloseReason"); } } }

        private String _CountScript;
        /// <summary>统计代码</summary>
        [DisplayName("统计代码")]
        [Description("统计代码")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("CountScript", "统计代码", "")]
        public String CountScript { get => _CountScript; set { if (OnPropertyChanging("CountScript", value)) { _CountScript = value; OnPropertyChanged("CountScript"); } } }

        private String _WeiboQRCode;
        /// <summary>微博二维码</summary>
        [DisplayName("微博二维码")]
        [Description("微博二维码")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("WeiboQRCode", "微博二维码", "")]
        public String WeiboQRCode { get => _WeiboQRCode; set { if (OnPropertyChanging("WeiboQRCode", value)) { _WeiboQRCode = value; OnPropertyChanged("WeiboQRCode"); } } }

        private String _WinxinQRCode;
        /// <summary>微信二维码</summary>
        [DisplayName("微信二维码")]
        [Description("微信二维码")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("WinxinQRCode", "微信二维码", "")]
        public String WinxinQRCode { get => _WinxinQRCode; set { if (OnPropertyChanging("WinxinQRCode", value)) { _WinxinQRCode = value; OnPropertyChanged("WinxinQRCode"); } } }

        private String _Keyword;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "关键字", "")]
        public String Keyword { get => _Keyword; set { if (OnPropertyChanging("Keyword", value)) { _Keyword = value; OnPropertyChanged("Keyword"); } } }

        private String _Description;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "描述", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }

        private String _IndexTitle;
        /// <summary>首页标题</summary>
        [DisplayName("首页标题")]
        [Description("首页标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("IndexTitle", "首页标题", "")]
        public String IndexTitle { get => _IndexTitle; set { if (OnPropertyChanging("IndexTitle", value)) { _IndexTitle = value; OnPropertyChanged("IndexTitle"); } } }

        private Int32 _IsRewrite;
        /// <summary>是否URL地址重写</summary>
        [DisplayName("是否URL地址重写")]
        [Description("是否URL地址重写")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRewrite", "是否URL地址重写", "")]
        public Int32 IsRewrite { get => _IsRewrite; set { if (OnPropertyChanging("IsRewrite", value)) { _IsRewrite = value; OnPropertyChanged("IsRewrite"); } } }

        private Int32 _SearchMinTime;
        /// <summary>搜索最小时间间距 秒</summary>
        [DisplayName("搜索最小时间间距秒")]
        [Description("搜索最小时间间距 秒")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SearchMinTime", "搜索最小时间间距 秒", "")]
        public Int32 SearchMinTime { get => _SearchMinTime; set { if (OnPropertyChanging("SearchMinTime", value)) { _SearchMinTime = value; OnPropertyChanged("SearchMinTime"); } } }

        private String _OnlineQQ;
        /// <summary>在线QQ</summary>
        [DisplayName("在线QQ")]
        [Description("在线QQ")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineQQ", "在线QQ", "")]
        public String OnlineQQ { get => _OnlineQQ; set { if (OnPropertyChanging("OnlineQQ", value)) { _OnlineQQ = value; OnPropertyChanged("OnlineQQ"); } } }

        private String _OnlineSkype;
        /// <summary>在线Skype</summary>
        [DisplayName("在线Skype")]
        [Description("在线Skype")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineSkype", "在线Skype", "")]
        public String OnlineSkype { get => _OnlineSkype; set { if (OnPropertyChanging("OnlineSkype", value)) { _OnlineSkype = value; OnPropertyChanged("OnlineSkype"); } } }

        private String _OnlineWangWang;
        /// <summary>在线阿里旺旺</summary>
        [DisplayName("在线阿里旺旺")]
        [Description("在线阿里旺旺")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineWangWang", "在线阿里旺旺", "")]
        public String OnlineWangWang { get => _OnlineWangWang; set { if (OnPropertyChanging("OnlineWangWang", value)) { _OnlineWangWang = value; OnPropertyChanged("OnlineWangWang"); } } }

        private String _SkinName;
        /// <summary>站点URL</summary>
        [DisplayName("站点URL")]
        [Description("站点URL")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SkinName", "站点URL", "")]
        public String SkinName { get => _SkinName; set { if (OnPropertyChanging("SkinName", value)) { _SkinName = value; OnPropertyChanged("SkinName"); } } }

        private String _OfficialName;
        /// <summary>公众号名称</summary>
        [DisplayName("公众号名称")]
        [Description("公众号名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OfficialName", "公众号名称", "")]
        public String OfficialName { get => _OfficialName; set { if (OnPropertyChanging("OfficialName", value)) { _OfficialName = value; OnPropertyChanged("OfficialName"); } } }

        private String _OfficialDecsription;
        /// <summary>公众号介绍</summary>
        [DisplayName("公众号介绍")]
        [Description("公众号介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialDecsription", "公众号介绍", "")]
        public String OfficialDecsription { get => _OfficialDecsription; set { if (OnPropertyChanging("OfficialDecsription", value)) { _OfficialDecsription = value; OnPropertyChanged("OfficialDecsription"); } } }

        private String _OfficialOriginalId;
        /// <summary>公众号原始ID</summary>
        [DisplayName("公众号原始ID")]
        [Description("公众号原始ID")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("OfficialOriginalId", "公众号原始ID", "")]
        public String OfficialOriginalId { get => _OfficialOriginalId; set { if (OnPropertyChanging("OfficialOriginalId", value)) { _OfficialOriginalId = value; OnPropertyChanged("OfficialOriginalId"); } } }

        private String _WexinAccount;
        /// <summary>微信名称</summary>
        [DisplayName("微信名称")]
        [Description("微信名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("WexinAccount", "微信名称", "")]
        public String WexinAccount { get => _WexinAccount; set { if (OnPropertyChanging("WexinAccount", value)) { _WexinAccount = value; OnPropertyChanged("WexinAccount"); } } }

        private String _Token;
        /// <summary>Token</summary>
        [DisplayName("Token")]
        [Description("Token")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Token", "Token", "")]
        public String Token { get => _Token; set { if (OnPropertyChanging("Token", value)) { _Token = value; OnPropertyChanged("Token"); } } }

        private String _AppId;
        /// <summary>AppId</summary>
        [DisplayName("AppId")]
        [Description("AppId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("AppId", "AppId", "")]
        public String AppId { get => _AppId; set { if (OnPropertyChanging("AppId", value)) { _AppId = value; OnPropertyChanged("AppId"); } } }

        private String _AppSecret;
        /// <summary>AppSecret</summary>
        [DisplayName("AppSecret")]
        [Description("AppSecret")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("AppSecret", "AppSecret", "")]
        public String AppSecret { get => _AppSecret; set { if (OnPropertyChanging("AppSecret", value)) { _AppSecret = value; OnPropertyChanged("AppSecret"); } } }

        private String _FllowTipPageURL;
        /// <summary>引导关注素材地址</summary>
        [DisplayName("引导关注素材地址")]
        [Description("引导关注素材地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("FllowTipPageURL", "引导关注素材地址", "")]
        public String FllowTipPageURL { get => _FllowTipPageURL; set { if (OnPropertyChanging("FllowTipPageURL", value)) { _FllowTipPageURL = value; OnPropertyChanged("FllowTipPageURL"); } } }

        private Int32 _OfficialType;
        /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
        [DisplayName("公众号类型：0普通订阅号1普通服务号2认证订阅号3认证服务号4企业号5认证企业号")]
        [Description("公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OfficialType", "公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号", "")]
        public Int32 OfficialType { get => _OfficialType; set { if (OnPropertyChanging("OfficialType", value)) { _OfficialType = value; OnPropertyChanged("OfficialType"); } } }

        private String _EncodingAESKey;
        /// <summary>EncodingAESKey</summary>
        [DisplayName("EncodingAESKey")]
        [Description("EncodingAESKey")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("EncodingAESKey", "EncodingAESKey", "")]
        public String EncodingAESKey { get => _EncodingAESKey; set { if (OnPropertyChanging("EncodingAESKey", value)) { _EncodingAESKey = value; OnPropertyChanged("EncodingAESKey"); } } }

        private Int32 _DEType;
        /// <summary>解密方式0,明文</summary>
        [DisplayName("解密方式0")]
        [Description("解密方式0,明文")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DEType", "解密方式0,明文", "")]
        public Int32 DEType { get => _DEType; set { if (OnPropertyChanging("DEType", value)) { _DEType = value; OnPropertyChanged("DEType"); } } }

        private String _OfficialQRCode;
        /// <summary>公众号二维码地址</summary>
        [DisplayName("公众号二维码地址")]
        [Description("公众号二维码地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialQRCode", "公众号二维码地址", "")]
        public String OfficialQRCode { get => _OfficialQRCode; set { if (OnPropertyChanging("OfficialQRCode", value)) { _OfficialQRCode = value; OnPropertyChanged("OfficialQRCode"); } } }

        private String _OfficialImg;
        /// <summary>公众号头像</summary>
        [DisplayName("公众号头像")]
        [Description("公众号头像")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialImg", "公众号头像", "")]
        public String OfficialImg { get => _OfficialImg; set { if (OnPropertyChanging("OfficialImg", value)) { _OfficialImg = value; OnPropertyChanged("OfficialImg"); } } }

        private DateTime _LastUpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "最后更新时间", "")]
        public DateTime LastUpdateTime { get => _LastUpdateTime; set { if (OnPropertyChanging("LastUpdateTime", value)) { _LastUpdateTime = value; OnPropertyChanged("LastUpdateTime"); } } }

        private DateTime _LastCacheTime;
        /// <summary>最后缓存时间</summary>
        [DisplayName("最后缓存时间")]
        [Description("最后缓存时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastCacheTime", "最后缓存时间", "")]
        public DateTime LastCacheTime { get => _LastCacheTime; set { if (OnPropertyChanging("LastCacheTime", value)) { _LastCacheTime = value; OnPropertyChanged("LastCacheTime"); } } }

        private String _MCHId;
        /// <summary>微信商家MCHId</summary>
        [DisplayName("微信商家MCHId")]
        [Description("微信商家MCHId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MCHId", "微信商家MCHId", "")]
        public String MCHId { get => _MCHId; set { if (OnPropertyChanging("MCHId", value)) { _MCHId = value; OnPropertyChanged("MCHId"); } } }

        private String _MCHKey;
        /// <summary>微信商家key</summary>
        [DisplayName("微信商家key")]
        [Description("微信商家key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MCHKey", "微信商家key", "")]
        public String MCHKey { get => _MCHKey; set { if (OnPropertyChanging("MCHKey", value)) { _MCHKey = value; OnPropertyChanged("MCHKey"); } } }

        private Decimal _DefaultFare;
        /// <summary>默认运费</summary>
        [DisplayName("默认运费")]
        [Description("默认运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DefaultFare", "默认运费", "")]
        public Decimal DefaultFare { get => _DefaultFare; set { if (OnPropertyChanging("DefaultFare", value)) { _DefaultFare = value; OnPropertyChanged("DefaultFare"); } } }

        private Decimal _MaxFreeFare;
        /// <summary>最大免运费金额</summary>
        [DisplayName("最大免运费金额")]
        [Description("最大免运费金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxFreeFare", "最大免运费金额", "")]
        public Decimal MaxFreeFare { get => _MaxFreeFare; set { if (OnPropertyChanging("MaxFreeFare", value)) { _MaxFreeFare = value; OnPropertyChanged("MaxFreeFare"); } } }

        private String _WXAppId;
        /// <summary>微信小程序AppId</summary>
        [DisplayName("微信小程序AppId")]
        [Description("微信小程序AppId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppId", "微信小程序AppId", "")]
        public String WXAppId { get => _WXAppId; set { if (OnPropertyChanging("WXAppId", value)) { _WXAppId = value; OnPropertyChanged("WXAppId"); } } }

        private String _WXAppSecret;
        /// <summary>微信小程序AppSecret</summary>
        [DisplayName("微信小程序AppSecret")]
        [Description("微信小程序AppSecret")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppSecret", "微信小程序AppSecret", "")]
        public String WXAppSecret { get => _WXAppSecret; set { if (OnPropertyChanging("WXAppSecret", value)) { _WXAppSecret = value; OnPropertyChanged("WXAppSecret"); } } }

        private Int32 _IsResetData;
        /// <summary>小程序首页是否显示清除数据按钮</summary>
        [DisplayName("小程序首页是否显示清除数据按钮")]
        [Description("小程序首页是否显示清除数据按钮")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsResetData", "小程序首页是否显示清除数据按钮", "")]
        public Int32 IsResetData { get => _IsResetData; set { if (OnPropertyChanging("IsResetData", value)) { _IsResetData = value; OnPropertyChanged("IsResetData"); } } }
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
                    case "SiteName": return _SiteName;
                    case "SiteUrl": return _SiteUrl;
                    case "SiteLogo": return _SiteLogo;
                    case "Icp": return _Icp;
                    case "SiteEmail": return _SiteEmail;
                    case "SiteTel": return _SiteTel;
                    case "Copyright": return _Copyright;
                    case "IsCloseSite": return _IsCloseSite;
                    case "CloseReason": return _CloseReason;
                    case "CountScript": return _CountScript;
                    case "WeiboQRCode": return _WeiboQRCode;
                    case "WinxinQRCode": return _WinxinQRCode;
                    case "Keyword": return _Keyword;
                    case "Description": return _Description;
                    case "IndexTitle": return _IndexTitle;
                    case "IsRewrite": return _IsRewrite;
                    case "SearchMinTime": return _SearchMinTime;
                    case "OnlineQQ": return _OnlineQQ;
                    case "OnlineSkype": return _OnlineSkype;
                    case "OnlineWangWang": return _OnlineWangWang;
                    case "SkinName": return _SkinName;
                    case "OfficialName": return _OfficialName;
                    case "OfficialDecsription": return _OfficialDecsription;
                    case "OfficialOriginalId": return _OfficialOriginalId;
                    case "WexinAccount": return _WexinAccount;
                    case "Token": return _Token;
                    case "AppId": return _AppId;
                    case "AppSecret": return _AppSecret;
                    case "FllowTipPageURL": return _FllowTipPageURL;
                    case "OfficialType": return _OfficialType;
                    case "EncodingAESKey": return _EncodingAESKey;
                    case "DEType": return _DEType;
                    case "OfficialQRCode": return _OfficialQRCode;
                    case "OfficialImg": return _OfficialImg;
                    case "LastUpdateTime": return _LastUpdateTime;
                    case "LastCacheTime": return _LastCacheTime;
                    case "MCHId": return _MCHId;
                    case "MCHKey": return _MCHKey;
                    case "DefaultFare": return _DefaultFare;
                    case "MaxFreeFare": return _MaxFreeFare;
                    case "WXAppId": return _WXAppId;
                    case "WXAppSecret": return _WXAppSecret;
                    case "IsResetData": return _IsResetData;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "SiteName": _SiteName = Convert.ToString(value); break;
                    case "SiteUrl": _SiteUrl = Convert.ToString(value); break;
                    case "SiteLogo": _SiteLogo = Convert.ToString(value); break;
                    case "Icp": _Icp = Convert.ToString(value); break;
                    case "SiteEmail": _SiteEmail = Convert.ToString(value); break;
                    case "SiteTel": _SiteTel = Convert.ToString(value); break;
                    case "Copyright": _Copyright = Convert.ToString(value); break;
                    case "IsCloseSite": _IsCloseSite = value.ToBoolean(); break;
                    case "CloseReason": _CloseReason = Convert.ToString(value); break;
                    case "CountScript": _CountScript = Convert.ToString(value); break;
                    case "WeiboQRCode": _WeiboQRCode = Convert.ToString(value); break;
                    case "WinxinQRCode": _WinxinQRCode = Convert.ToString(value); break;
                    case "Keyword": _Keyword = Convert.ToString(value); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    case "IndexTitle": _IndexTitle = Convert.ToString(value); break;
                    case "IsRewrite": _IsRewrite = value.ToInt(); break;
                    case "SearchMinTime": _SearchMinTime = value.ToInt(); break;
                    case "OnlineQQ": _OnlineQQ = Convert.ToString(value); break;
                    case "OnlineSkype": _OnlineSkype = Convert.ToString(value); break;
                    case "OnlineWangWang": _OnlineWangWang = Convert.ToString(value); break;
                    case "SkinName": _SkinName = Convert.ToString(value); break;
                    case "OfficialName": _OfficialName = Convert.ToString(value); break;
                    case "OfficialDecsription": _OfficialDecsription = Convert.ToString(value); break;
                    case "OfficialOriginalId": _OfficialOriginalId = Convert.ToString(value); break;
                    case "WexinAccount": _WexinAccount = Convert.ToString(value); break;
                    case "Token": _Token = Convert.ToString(value); break;
                    case "AppId": _AppId = Convert.ToString(value); break;
                    case "AppSecret": _AppSecret = Convert.ToString(value); break;
                    case "FllowTipPageURL": _FllowTipPageURL = Convert.ToString(value); break;
                    case "OfficialType": _OfficialType = value.ToInt(); break;
                    case "EncodingAESKey": _EncodingAESKey = Convert.ToString(value); break;
                    case "DEType": _DEType = value.ToInt(); break;
                    case "OfficialQRCode": _OfficialQRCode = Convert.ToString(value); break;
                    case "OfficialImg": _OfficialImg = Convert.ToString(value); break;
                    case "LastUpdateTime": _LastUpdateTime = value.ToDateTime(); break;
                    case "LastCacheTime": _LastCacheTime = value.ToDateTime(); break;
                    case "MCHId": _MCHId = Convert.ToString(value); break;
                    case "MCHKey": _MCHKey = Convert.ToString(value); break;
                    case "DefaultFare": _DefaultFare = Convert.ToDecimal(value); break;
                    case "MaxFreeFare": _MaxFreeFare = Convert.ToDecimal(value); break;
                    case "WXAppId": _WXAppId = Convert.ToString(value); break;
                    case "WXAppSecret": _WXAppSecret = Convert.ToString(value); break;
                    case "IsResetData": _IsResetData = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得系统配置字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>站点名称</summary>
            public static readonly Field SiteName = FindByName("SiteName");

            /// <summary>站点URL</summary>
            public static readonly Field SiteUrl = FindByName("SiteUrl");

            /// <summary>站点LOGO</summary>
            public static readonly Field SiteLogo = FindByName("SiteLogo");

            /// <summary>ICP备案</summary>
            public static readonly Field Icp = FindByName("Icp");

            /// <summary>联系我们Email</summary>
            public static readonly Field SiteEmail = FindByName("SiteEmail");

            /// <summary>网站电话</summary>
            public static readonly Field SiteTel = FindByName("SiteTel");

            /// <summary>版权所有</summary>
            public static readonly Field Copyright = FindByName("Copyright");

            /// <summary>是否关闭网站</summary>
            public static readonly Field IsCloseSite = FindByName("IsCloseSite");

            /// <summary>关闭原因</summary>
            public static readonly Field CloseReason = FindByName("CloseReason");

            /// <summary>统计代码</summary>
            public static readonly Field CountScript = FindByName("CountScript");

            /// <summary>微博二维码</summary>
            public static readonly Field WeiboQRCode = FindByName("WeiboQRCode");

            /// <summary>微信二维码</summary>
            public static readonly Field WinxinQRCode = FindByName("WinxinQRCode");

            /// <summary>关键字</summary>
            public static readonly Field Keyword = FindByName("Keyword");

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName("Description");

            /// <summary>首页标题</summary>
            public static readonly Field IndexTitle = FindByName("IndexTitle");

            /// <summary>是否URL地址重写</summary>
            public static readonly Field IsRewrite = FindByName("IsRewrite");

            /// <summary>搜索最小时间间距 秒</summary>
            public static readonly Field SearchMinTime = FindByName("SearchMinTime");

            /// <summary>在线QQ</summary>
            public static readonly Field OnlineQQ = FindByName("OnlineQQ");

            /// <summary>在线Skype</summary>
            public static readonly Field OnlineSkype = FindByName("OnlineSkype");

            /// <summary>在线阿里旺旺</summary>
            public static readonly Field OnlineWangWang = FindByName("OnlineWangWang");

            /// <summary>站点URL</summary>
            public static readonly Field SkinName = FindByName("SkinName");

            /// <summary>公众号名称</summary>
            public static readonly Field OfficialName = FindByName("OfficialName");

            /// <summary>公众号介绍</summary>
            public static readonly Field OfficialDecsription = FindByName("OfficialDecsription");

            /// <summary>公众号原始ID</summary>
            public static readonly Field OfficialOriginalId = FindByName("OfficialOriginalId");

            /// <summary>微信名称</summary>
            public static readonly Field WexinAccount = FindByName("WexinAccount");

            /// <summary>Token</summary>
            public static readonly Field Token = FindByName("Token");

            /// <summary>AppId</summary>
            public static readonly Field AppId = FindByName("AppId");

            /// <summary>AppSecret</summary>
            public static readonly Field AppSecret = FindByName("AppSecret");

            /// <summary>引导关注素材地址</summary>
            public static readonly Field FllowTipPageURL = FindByName("FllowTipPageURL");

            /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
            public static readonly Field OfficialType = FindByName("OfficialType");

            /// <summary>EncodingAESKey</summary>
            public static readonly Field EncodingAESKey = FindByName("EncodingAESKey");

            /// <summary>解密方式0,明文</summary>
            public static readonly Field DEType = FindByName("DEType");

            /// <summary>公众号二维码地址</summary>
            public static readonly Field OfficialQRCode = FindByName("OfficialQRCode");

            /// <summary>公众号头像</summary>
            public static readonly Field OfficialImg = FindByName("OfficialImg");

            /// <summary>最后更新时间</summary>
            public static readonly Field LastUpdateTime = FindByName("LastUpdateTime");

            /// <summary>最后缓存时间</summary>
            public static readonly Field LastCacheTime = FindByName("LastCacheTime");

            /// <summary>微信商家MCHId</summary>
            public static readonly Field MCHId = FindByName("MCHId");

            /// <summary>微信商家key</summary>
            public static readonly Field MCHKey = FindByName("MCHKey");

            /// <summary>默认运费</summary>
            public static readonly Field DefaultFare = FindByName("DefaultFare");

            /// <summary>最大免运费金额</summary>
            public static readonly Field MaxFreeFare = FindByName("MaxFreeFare");

            /// <summary>微信小程序AppId</summary>
            public static readonly Field WXAppId = FindByName("WXAppId");

            /// <summary>微信小程序AppSecret</summary>
            public static readonly Field WXAppSecret = FindByName("WXAppSecret");

            /// <summary>小程序首页是否显示清除数据按钮</summary>
            public static readonly Field IsResetData = FindByName("IsResetData");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得系统配置字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>站点名称</summary>
            public const String SiteName = "SiteName";

            /// <summary>站点URL</summary>
            public const String SiteUrl = "SiteUrl";

            /// <summary>站点LOGO</summary>
            public const String SiteLogo = "SiteLogo";

            /// <summary>ICP备案</summary>
            public const String Icp = "Icp";

            /// <summary>联系我们Email</summary>
            public const String SiteEmail = "SiteEmail";

            /// <summary>网站电话</summary>
            public const String SiteTel = "SiteTel";

            /// <summary>版权所有</summary>
            public const String Copyright = "Copyright";

            /// <summary>是否关闭网站</summary>
            public const String IsCloseSite = "IsCloseSite";

            /// <summary>关闭原因</summary>
            public const String CloseReason = "CloseReason";

            /// <summary>统计代码</summary>
            public const String CountScript = "CountScript";

            /// <summary>微博二维码</summary>
            public const String WeiboQRCode = "WeiboQRCode";

            /// <summary>微信二维码</summary>
            public const String WinxinQRCode = "WinxinQRCode";

            /// <summary>关键字</summary>
            public const String Keyword = "Keyword";

            /// <summary>描述</summary>
            public const String Description = "Description";

            /// <summary>首页标题</summary>
            public const String IndexTitle = "IndexTitle";

            /// <summary>是否URL地址重写</summary>
            public const String IsRewrite = "IsRewrite";

            /// <summary>搜索最小时间间距 秒</summary>
            public const String SearchMinTime = "SearchMinTime";

            /// <summary>在线QQ</summary>
            public const String OnlineQQ = "OnlineQQ";

            /// <summary>在线Skype</summary>
            public const String OnlineSkype = "OnlineSkype";

            /// <summary>在线阿里旺旺</summary>
            public const String OnlineWangWang = "OnlineWangWang";

            /// <summary>站点URL</summary>
            public const String SkinName = "SkinName";

            /// <summary>公众号名称</summary>
            public const String OfficialName = "OfficialName";

            /// <summary>公众号介绍</summary>
            public const String OfficialDecsription = "OfficialDecsription";

            /// <summary>公众号原始ID</summary>
            public const String OfficialOriginalId = "OfficialOriginalId";

            /// <summary>微信名称</summary>
            public const String WexinAccount = "WexinAccount";

            /// <summary>Token</summary>
            public const String Token = "Token";

            /// <summary>AppId</summary>
            public const String AppId = "AppId";

            /// <summary>AppSecret</summary>
            public const String AppSecret = "AppSecret";

            /// <summary>引导关注素材地址</summary>
            public const String FllowTipPageURL = "FllowTipPageURL";

            /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
            public const String OfficialType = "OfficialType";

            /// <summary>EncodingAESKey</summary>
            public const String EncodingAESKey = "EncodingAESKey";

            /// <summary>解密方式0,明文</summary>
            public const String DEType = "DEType";

            /// <summary>公众号二维码地址</summary>
            public const String OfficialQRCode = "OfficialQRCode";

            /// <summary>公众号头像</summary>
            public const String OfficialImg = "OfficialImg";

            /// <summary>最后更新时间</summary>
            public const String LastUpdateTime = "LastUpdateTime";

            /// <summary>最后缓存时间</summary>
            public const String LastCacheTime = "LastCacheTime";

            /// <summary>微信商家MCHId</summary>
            public const String MCHId = "MCHId";

            /// <summary>微信商家key</summary>
            public const String MCHKey = "MCHKey";

            /// <summary>默认运费</summary>
            public const String DefaultFare = "DefaultFare";

            /// <summary>最大免运费金额</summary>
            public const String MaxFreeFare = "MaxFreeFare";

            /// <summary>微信小程序AppId</summary>
            public const String WXAppId = "WXAppId";

            /// <summary>微信小程序AppSecret</summary>
            public const String WXAppSecret = "WXAppSecret";

            /// <summary>小程序首页是否显示清除数据按钮</summary>
            public const String IsResetData = "IsResetData";
        }
        #endregion
    }
}