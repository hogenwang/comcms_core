using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Config : IConfig
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get { return _Id; } set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _SiteName;
        /// <summary>站点名称</summary>
        [DisplayName("站点名称")]
        [Description("站点名称")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SiteName", "站点名称", "nvarchar(250)")]
        public String SiteName { get { return _SiteName; } set { if (OnPropertyChanging(__.SiteName, value)) { _SiteName = value; OnPropertyChanged(__.SiteName); } } }

        private String _SiteUrl;
        /// <summary>站点URL</summary>
        [DisplayName("站点URL")]
        [Description("站点URL")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SiteUrl", "站点URL", "nvarchar(50)")]
        public String SiteUrl { get { return _SiteUrl; } set { if (OnPropertyChanging(__.SiteUrl, value)) { _SiteUrl = value; OnPropertyChanged(__.SiteUrl); } } }

        private String _SiteLogo;
        /// <summary>站点LOGO</summary>
        [DisplayName("站点LOGO")]
        [Description("站点LOGO")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("SiteLogo", "站点LOGO", "nvarchar(250)")]
        public String SiteLogo { get { return _SiteLogo; } set { if (OnPropertyChanging(__.SiteLogo, value)) { _SiteLogo = value; OnPropertyChanged(__.SiteLogo); } } }

        private String _ICP;
        /// <summary>ICP备案</summary>
        [DisplayName("ICP备案")]
        [Description("ICP备案")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ICP", "ICP备案", "nvarchar(50)")]
        public String ICP { get { return _ICP; } set { if (OnPropertyChanging(__.ICP, value)) { _ICP = value; OnPropertyChanged(__.ICP); } } }

        private String _SiteEmail;
        /// <summary>联系我们Email</summary>
        [DisplayName("联系我们Email")]
        [Description("联系我们Email")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("SiteEmail", "联系我们Email", "nvarchar(100)")]
        public String SiteEmail { get { return _SiteEmail; } set { if (OnPropertyChanging(__.SiteEmail, value)) { _SiteEmail = value; OnPropertyChanged(__.SiteEmail); } } }

        private String _SiteTel;
        /// <summary>网站电话</summary>
        [DisplayName("网站电话")]
        [Description("网站电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SiteTel", "网站电话", "nvarchar(50)")]
        public String SiteTel { get { return _SiteTel; } set { if (OnPropertyChanging(__.SiteTel, value)) { _SiteTel = value; OnPropertyChanged(__.SiteTel); } } }

        private String _Copyright;
        /// <summary>版权所有</summary>
        [DisplayName("版权所有")]
        [Description("版权所有")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("Copyright", "版权所有", "ntext")]
        public String Copyright { get { return _Copyright; } set { if (OnPropertyChanging(__.Copyright, value)) { _Copyright = value; OnPropertyChanged(__.Copyright); } } }

        private Boolean _IsCloseSite;
        /// <summary>是否关闭网站</summary>
        [DisplayName("是否关闭网站")]
        [Description("是否关闭网站")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsCloseSite", "是否关闭网站", "bit")]
        public Boolean IsCloseSite { get { return _IsCloseSite; } set { if (OnPropertyChanging(__.IsCloseSite, value)) { _IsCloseSite = value; OnPropertyChanged(__.IsCloseSite); } } }

        private String _CloseReason;
        /// <summary>关闭原因</summary>
        [DisplayName("关闭原因")]
        [Description("关闭原因")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("CloseReason", "关闭原因", "ntext")]
        public String CloseReason { get { return _CloseReason; } set { if (OnPropertyChanging(__.CloseReason, value)) { _CloseReason = value; OnPropertyChanged(__.CloseReason); } } }

        private String _CountScript;
        /// <summary>统计代码</summary>
        [DisplayName("统计代码")]
        [Description("统计代码")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn("CountScript", "统计代码", "ntext")]
        public String CountScript { get { return _CountScript; } set { if (OnPropertyChanging(__.CountScript, value)) { _CountScript = value; OnPropertyChanged(__.CountScript); } } }

        private String _WeiboQRCode;
        /// <summary>微博二维码</summary>
        [DisplayName("微博二维码")]
        [Description("微博二维码")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("WeiboQRCode", "微博二维码", "nvarchar(250)")]
        public String WeiboQRCode { get { return _WeiboQRCode; } set { if (OnPropertyChanging(__.WeiboQRCode, value)) { _WeiboQRCode = value; OnPropertyChanged(__.WeiboQRCode); } } }

        private String _WinxinQRCode;
        /// <summary>微信二维码</summary>
        [DisplayName("微信二维码")]
        [Description("微信二维码")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("WinxinQRCode", "微信二维码", "nvarchar(250)")]
        public String WinxinQRCode { get { return _WinxinQRCode; } set { if (OnPropertyChanging(__.WinxinQRCode, value)) { _WinxinQRCode = value; OnPropertyChanged(__.WinxinQRCode); } } }

        private String _Keyword;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Keyword", "关键字", "nvarchar(250)")]
        public String Keyword { get { return _Keyword; } set { if (OnPropertyChanging(__.Keyword, value)) { _Keyword = value; OnPropertyChanged(__.Keyword); } } }

        private String _Description;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Description", "描述", "nvarchar(250)")]
        public String Description { get { return _Description; } set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }

        private String _IndexTitle;
        /// <summary>首页标题</summary>
        [DisplayName("首页标题")]
        [Description("首页标题")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("IndexTitle", "首页标题", "nvarchar(250)")]
        public String IndexTitle { get { return _IndexTitle; } set { if (OnPropertyChanging(__.IndexTitle, value)) { _IndexTitle = value; OnPropertyChanged(__.IndexTitle); } } }

        private Int32 _IsRewrite;
        /// <summary>是否URL地址重写</summary>
        [DisplayName("是否URL地址重写")]
        [Description("是否URL地址重写")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRewrite", "是否URL地址重写", "int")]
        public Int32 IsRewrite { get { return _IsRewrite; } set { if (OnPropertyChanging(__.IsRewrite, value)) { _IsRewrite = value; OnPropertyChanged(__.IsRewrite); } } }

        private Int32 _SearchMinTime;
        /// <summary>搜索最小时间间距 秒</summary>
        [DisplayName("搜索最小时间间距秒")]
        [Description("搜索最小时间间距 秒")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SearchMinTime", "搜索最小时间间距 秒", "int")]
        public Int32 SearchMinTime { get { return _SearchMinTime; } set { if (OnPropertyChanging(__.SearchMinTime, value)) { _SearchMinTime = value; OnPropertyChanged(__.SearchMinTime); } } }

        private String _OnlineQQ;
        /// <summary>在线QQ</summary>
        [DisplayName("在线QQ")]
        [Description("在线QQ")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineQQ", "在线QQ", "nvarchar(250)")]
        public String OnlineQQ { get { return _OnlineQQ; } set { if (OnPropertyChanging(__.OnlineQQ, value)) { _OnlineQQ = value; OnPropertyChanged(__.OnlineQQ); } } }

        private String _OnlineSkype;
        /// <summary>在线Skype</summary>
        [DisplayName("在线Skype")]
        [Description("在线Skype")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineSkype", "在线Skype", "nvarchar(250)")]
        public String OnlineSkype { get { return _OnlineSkype; } set { if (OnPropertyChanging(__.OnlineSkype, value)) { _OnlineSkype = value; OnPropertyChanged(__.OnlineSkype); } } }

        private String _OnlineWangWang;
        /// <summary>在线阿里旺旺</summary>
        [DisplayName("在线阿里旺旺")]
        [Description("在线阿里旺旺")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OnlineWangWang", "在线阿里旺旺", "nvarchar(250)")]
        public String OnlineWangWang { get { return _OnlineWangWang; } set { if (OnPropertyChanging(__.OnlineWangWang, value)) { _OnlineWangWang = value; OnPropertyChanged(__.OnlineWangWang); } } }

        private String _SkinName;
        /// <summary>站点URL</summary>
        [DisplayName("站点URL")]
        [Description("站点URL")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SkinName", "站点URL", "nvarchar(50)")]
        public String SkinName { get { return _SkinName; } set { if (OnPropertyChanging(__.SkinName, value)) { _SkinName = value; OnPropertyChanged(__.SkinName); } } }

        private String _OfficialName;
        /// <summary>公众号名称</summary>
        [DisplayName("公众号名称")]
        [Description("公众号名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OfficialName", "公众号名称", "nvarchar(50)")]
        public String OfficialName { get { return _OfficialName; } set { if (OnPropertyChanging(__.OfficialName, value)) { _OfficialName = value; OnPropertyChanged(__.OfficialName); } } }

        private String _OfficialDecsription;
        /// <summary>公众号介绍</summary>
        [DisplayName("公众号介绍")]
        [Description("公众号介绍")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialDecsription", "公众号介绍", "nvarchar(250)")]
        public String OfficialDecsription { get { return _OfficialDecsription; } set { if (OnPropertyChanging(__.OfficialDecsription, value)) { _OfficialDecsription = value; OnPropertyChanged(__.OfficialDecsription); } } }

        private String _OfficialOriginalId;
        /// <summary>公众号原始ID</summary>
        [DisplayName("公众号原始ID")]
        [Description("公众号原始ID")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("OfficialOriginalId", "公众号原始ID", "nvarchar(100)")]
        public String OfficialOriginalId { get { return _OfficialOriginalId; } set { if (OnPropertyChanging(__.OfficialOriginalId, value)) { _OfficialOriginalId = value; OnPropertyChanged(__.OfficialOriginalId); } } }

        private String _WexinAccount;
        /// <summary>微信名称</summary>
        [DisplayName("微信名称")]
        [Description("微信名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("WexinAccount", "微信名称", "nvarchar(50)")]
        public String WexinAccount { get { return _WexinAccount; } set { if (OnPropertyChanging(__.WexinAccount, value)) { _WexinAccount = value; OnPropertyChanged(__.WexinAccount); } } }

        private String _Token;
        /// <summary>Token</summary>
        [DisplayName("Token")]
        [Description("Token")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Token", "Token", "nvarchar(100)")]
        public String Token { get { return _Token; } set { if (OnPropertyChanging(__.Token, value)) { _Token = value; OnPropertyChanged(__.Token); } } }

        private String _AppId;
        /// <summary>AppId</summary>
        [DisplayName("AppId")]
        [Description("AppId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("AppId", "AppId", "nvarchar(100)")]
        public String AppId { get { return _AppId; } set { if (OnPropertyChanging(__.AppId, value)) { _AppId = value; OnPropertyChanged(__.AppId); } } }

        private String _AppSecret;
        /// <summary>AppSecret</summary>
        [DisplayName("AppSecret")]
        [Description("AppSecret")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("AppSecret", "AppSecret", "nvarchar(100)")]
        public String AppSecret { get { return _AppSecret; } set { if (OnPropertyChanging(__.AppSecret, value)) { _AppSecret = value; OnPropertyChanged(__.AppSecret); } } }

        private String _FllowTipPageURL;
        /// <summary>引导关注素材地址</summary>
        [DisplayName("引导关注素材地址")]
        [Description("引导关注素材地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("FllowTipPageURL", "引导关注素材地址", "nvarchar(250)")]
        public String FllowTipPageURL { get { return _FllowTipPageURL; } set { if (OnPropertyChanging(__.FllowTipPageURL, value)) { _FllowTipPageURL = value; OnPropertyChanged(__.FllowTipPageURL); } } }

        private Int32 _OfficialType;
        /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
        [DisplayName("公众号类型：0普通订阅号1普通服务号2认证订阅号3认证服务号4企业号5认证企业号")]
        [Description("公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("OfficialType", "公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号", "int")]
        public Int32 OfficialType { get { return _OfficialType; } set { if (OnPropertyChanging(__.OfficialType, value)) { _OfficialType = value; OnPropertyChanged(__.OfficialType); } } }

        private String _EncodingAESKey;
        /// <summary>EncodingAESKey</summary>
        [DisplayName("EncodingAESKey")]
        [Description("EncodingAESKey")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("EncodingAESKey", "EncodingAESKey", "nvarchar(250)")]
        public String EncodingAESKey { get { return _EncodingAESKey; } set { if (OnPropertyChanging(__.EncodingAESKey, value)) { _EncodingAESKey = value; OnPropertyChanged(__.EncodingAESKey); } } }

        private Int32 _DEType;
        /// <summary>解密方式0,明文</summary>
        [DisplayName("解密方式0")]
        [Description("解密方式0,明文")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DEType", "解密方式0,明文", "int")]
        public Int32 DEType { get { return _DEType; } set { if (OnPropertyChanging(__.DEType, value)) { _DEType = value; OnPropertyChanged(__.DEType); } } }

        private String _OfficialQRCode;
        /// <summary>公众号二维码地址</summary>
        [DisplayName("公众号二维码地址")]
        [Description("公众号二维码地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialQRCode", "公众号二维码地址", "nvarchar(250)")]
        public String OfficialQRCode { get { return _OfficialQRCode; } set { if (OnPropertyChanging(__.OfficialQRCode, value)) { _OfficialQRCode = value; OnPropertyChanged(__.OfficialQRCode); } } }

        private String _OfficialImg;
        /// <summary>公众号头像</summary>
        [DisplayName("公众号头像")]
        [Description("公众号头像")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("OfficialImg", "公众号头像", "nvarchar(250)")]
        public String OfficialImg { get { return _OfficialImg; } set { if (OnPropertyChanging(__.OfficialImg, value)) { _OfficialImg = value; OnPropertyChanged(__.OfficialImg); } } }

        private DateTime _LastUpdateTime;
        /// <summary>最后更新时间</summary>
        [DisplayName("最后更新时间")]
        [Description("最后更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastUpdateTime", "最后更新时间", "datetime")]
        public DateTime LastUpdateTime { get { return _LastUpdateTime; } set { if (OnPropertyChanging(__.LastUpdateTime, value)) { _LastUpdateTime = value; OnPropertyChanged(__.LastUpdateTime); } } }

        private DateTime _LastCacheTime;
        /// <summary>最后缓存时间</summary>
        [DisplayName("最后缓存时间")]
        [Description("最后缓存时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastCacheTime", "最后缓存时间", "datetime")]
        public DateTime LastCacheTime { get { return _LastCacheTime; } set { if (OnPropertyChanging(__.LastCacheTime, value)) { _LastCacheTime = value; OnPropertyChanged(__.LastCacheTime); } } }

        private String _MCHId;
        /// <summary>微信商家MCHId</summary>
        [DisplayName("微信商家MCHId")]
        [Description("微信商家MCHId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MCHId", "微信商家MCHId", "nvarchar(100)")]
        public String MCHId { get { return _MCHId; } set { if (OnPropertyChanging(__.MCHId, value)) { _MCHId = value; OnPropertyChanged(__.MCHId); } } }

        private String _MCHKey;
        /// <summary>微信商家key</summary>
        [DisplayName("微信商家key")]
        [Description("微信商家key")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("MCHKey", "微信商家key", "nvarchar(100)")]
        public String MCHKey { get { return _MCHKey; } set { if (OnPropertyChanging(__.MCHKey, value)) { _MCHKey = value; OnPropertyChanged(__.MCHKey); } } }

        private Decimal _DefaultFare;
        /// <summary>默认运费</summary>
        [DisplayName("默认运费")]
        [Description("默认运费")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DefaultFare", "默认运费", "money")]
        public Decimal DefaultFare { get { return _DefaultFare; } set { if (OnPropertyChanging(__.DefaultFare, value)) { _DefaultFare = value; OnPropertyChanged(__.DefaultFare); } } }

        private Decimal _MaxFreeFare;
        /// <summary>最大免运费金额</summary>
        [DisplayName("最大免运费金额")]
        [Description("最大免运费金额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxFreeFare", "最大免运费金额", "money")]
        public Decimal MaxFreeFare { get { return _MaxFreeFare; } set { if (OnPropertyChanging(__.MaxFreeFare, value)) { _MaxFreeFare = value; OnPropertyChanged(__.MaxFreeFare); } } }

        private String _WXAppId;
        /// <summary>微信小程序AppId</summary>
        [DisplayName("微信小程序AppId")]
        [Description("微信小程序AppId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppId", "微信小程序AppId", "nvarchar(100)")]
        public String WXAppId { get { return _WXAppId; } set { if (OnPropertyChanging(__.WXAppId, value)) { _WXAppId = value; OnPropertyChanged(__.WXAppId); } } }

        private String _WXAppSecret;
        /// <summary>微信小程序AppSecret</summary>
        [DisplayName("微信小程序AppSecret")]
        [Description("微信小程序AppSecret")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WXAppSecret", "微信小程序AppSecret", "nvarchar(100)")]
        public String WXAppSecret { get { return _WXAppSecret; } set { if (OnPropertyChanging(__.WXAppSecret, value)) { _WXAppSecret = value; OnPropertyChanged(__.WXAppSecret); } } }

        private Int32 _IsResetData;
        /// <summary>小程序首页是否显示清除数据按钮</summary>
        [DisplayName("小程序首页是否显示清除数据按钮")]
        [Description("小程序首页是否显示清除数据按钮")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsResetData", "小程序首页是否显示清除数据按钮", "int")]
        public Int32 IsResetData { get { return _IsResetData; } set { if (OnPropertyChanging(__.IsResetData, value)) { _IsResetData = value; OnPropertyChanged(__.IsResetData); } } }
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
                    case __.SiteName : return _SiteName;
                    case __.SiteUrl : return _SiteUrl;
                    case __.SiteLogo : return _SiteLogo;
                    case __.ICP : return _ICP;
                    case __.SiteEmail : return _SiteEmail;
                    case __.SiteTel : return _SiteTel;
                    case __.Copyright : return _Copyright;
                    case __.IsCloseSite : return _IsCloseSite;
                    case __.CloseReason : return _CloseReason;
                    case __.CountScript : return _CountScript;
                    case __.WeiboQRCode : return _WeiboQRCode;
                    case __.WinxinQRCode : return _WinxinQRCode;
                    case __.Keyword : return _Keyword;
                    case __.Description : return _Description;
                    case __.IndexTitle : return _IndexTitle;
                    case __.IsRewrite : return _IsRewrite;
                    case __.SearchMinTime : return _SearchMinTime;
                    case __.OnlineQQ : return _OnlineQQ;
                    case __.OnlineSkype : return _OnlineSkype;
                    case __.OnlineWangWang : return _OnlineWangWang;
                    case __.SkinName : return _SkinName;
                    case __.OfficialName : return _OfficialName;
                    case __.OfficialDecsription : return _OfficialDecsription;
                    case __.OfficialOriginalId : return _OfficialOriginalId;
                    case __.WexinAccount : return _WexinAccount;
                    case __.Token : return _Token;
                    case __.AppId : return _AppId;
                    case __.AppSecret : return _AppSecret;
                    case __.FllowTipPageURL : return _FllowTipPageURL;
                    case __.OfficialType : return _OfficialType;
                    case __.EncodingAESKey : return _EncodingAESKey;
                    case __.DEType : return _DEType;
                    case __.OfficialQRCode : return _OfficialQRCode;
                    case __.OfficialImg : return _OfficialImg;
                    case __.LastUpdateTime : return _LastUpdateTime;
                    case __.LastCacheTime : return _LastCacheTime;
                    case __.MCHId : return _MCHId;
                    case __.MCHKey : return _MCHKey;
                    case __.DefaultFare : return _DefaultFare;
                    case __.MaxFreeFare : return _MaxFreeFare;
                    case __.WXAppId : return _WXAppId;
                    case __.WXAppSecret : return _WXAppSecret;
                    case __.IsResetData : return _IsResetData;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Id : _Id = Convert.ToInt32(value); break;
                    case __.SiteName : _SiteName = Convert.ToString(value); break;
                    case __.SiteUrl : _SiteUrl = Convert.ToString(value); break;
                    case __.SiteLogo : _SiteLogo = Convert.ToString(value); break;
                    case __.ICP : _ICP = Convert.ToString(value); break;
                    case __.SiteEmail : _SiteEmail = Convert.ToString(value); break;
                    case __.SiteTel : _SiteTel = Convert.ToString(value); break;
                    case __.Copyright : _Copyright = Convert.ToString(value); break;
                    case __.IsCloseSite : _IsCloseSite = Convert.ToBoolean(value); break;
                    case __.CloseReason : _CloseReason = Convert.ToString(value); break;
                    case __.CountScript : _CountScript = Convert.ToString(value); break;
                    case __.WeiboQRCode : _WeiboQRCode = Convert.ToString(value); break;
                    case __.WinxinQRCode : _WinxinQRCode = Convert.ToString(value); break;
                    case __.Keyword : _Keyword = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.IndexTitle : _IndexTitle = Convert.ToString(value); break;
                    case __.IsRewrite : _IsRewrite = Convert.ToInt32(value); break;
                    case __.SearchMinTime : _SearchMinTime = Convert.ToInt32(value); break;
                    case __.OnlineQQ : _OnlineQQ = Convert.ToString(value); break;
                    case __.OnlineSkype : _OnlineSkype = Convert.ToString(value); break;
                    case __.OnlineWangWang : _OnlineWangWang = Convert.ToString(value); break;
                    case __.SkinName : _SkinName = Convert.ToString(value); break;
                    case __.OfficialName : _OfficialName = Convert.ToString(value); break;
                    case __.OfficialDecsription : _OfficialDecsription = Convert.ToString(value); break;
                    case __.OfficialOriginalId : _OfficialOriginalId = Convert.ToString(value); break;
                    case __.WexinAccount : _WexinAccount = Convert.ToString(value); break;
                    case __.Token : _Token = Convert.ToString(value); break;
                    case __.AppId : _AppId = Convert.ToString(value); break;
                    case __.AppSecret : _AppSecret = Convert.ToString(value); break;
                    case __.FllowTipPageURL : _FllowTipPageURL = Convert.ToString(value); break;
                    case __.OfficialType : _OfficialType = Convert.ToInt32(value); break;
                    case __.EncodingAESKey : _EncodingAESKey = Convert.ToString(value); break;
                    case __.DEType : _DEType = Convert.ToInt32(value); break;
                    case __.OfficialQRCode : _OfficialQRCode = Convert.ToString(value); break;
                    case __.OfficialImg : _OfficialImg = Convert.ToString(value); break;
                    case __.LastUpdateTime : _LastUpdateTime = Convert.ToDateTime(value); break;
                    case __.LastCacheTime : _LastCacheTime = Convert.ToDateTime(value); break;
                    case __.MCHId : _MCHId = Convert.ToString(value); break;
                    case __.MCHKey : _MCHKey = Convert.ToString(value); break;
                    case __.DefaultFare : _DefaultFare = Convert.ToDecimal(value); break;
                    case __.MaxFreeFare : _MaxFreeFare = Convert.ToDecimal(value); break;
                    case __.WXAppId : _WXAppId = Convert.ToString(value); break;
                    case __.WXAppSecret : _WXAppSecret = Convert.ToString(value); break;
                    case __.IsResetData : _IsResetData = Convert.ToInt32(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>站点名称</summary>
            public static readonly Field SiteName = FindByName(__.SiteName);

            /// <summary>站点URL</summary>
            public static readonly Field SiteUrl = FindByName(__.SiteUrl);

            /// <summary>站点LOGO</summary>
            public static readonly Field SiteLogo = FindByName(__.SiteLogo);

            /// <summary>ICP备案</summary>
            public static readonly Field ICP = FindByName(__.ICP);

            /// <summary>联系我们Email</summary>
            public static readonly Field SiteEmail = FindByName(__.SiteEmail);

            /// <summary>网站电话</summary>
            public static readonly Field SiteTel = FindByName(__.SiteTel);

            /// <summary>版权所有</summary>
            public static readonly Field Copyright = FindByName(__.Copyright);

            /// <summary>是否关闭网站</summary>
            public static readonly Field IsCloseSite = FindByName(__.IsCloseSite);

            /// <summary>关闭原因</summary>
            public static readonly Field CloseReason = FindByName(__.CloseReason);

            /// <summary>统计代码</summary>
            public static readonly Field CountScript = FindByName(__.CountScript);

            /// <summary>微博二维码</summary>
            public static readonly Field WeiboQRCode = FindByName(__.WeiboQRCode);

            /// <summary>微信二维码</summary>
            public static readonly Field WinxinQRCode = FindByName(__.WinxinQRCode);

            /// <summary>关键字</summary>
            public static readonly Field Keyword = FindByName(__.Keyword);

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName(__.Description);

            /// <summary>首页标题</summary>
            public static readonly Field IndexTitle = FindByName(__.IndexTitle);

            /// <summary>是否URL地址重写</summary>
            public static readonly Field IsRewrite = FindByName(__.IsRewrite);

            /// <summary>搜索最小时间间距 秒</summary>
            public static readonly Field SearchMinTime = FindByName(__.SearchMinTime);

            /// <summary>在线QQ</summary>
            public static readonly Field OnlineQQ = FindByName(__.OnlineQQ);

            /// <summary>在线Skype</summary>
            public static readonly Field OnlineSkype = FindByName(__.OnlineSkype);

            /// <summary>在线阿里旺旺</summary>
            public static readonly Field OnlineWangWang = FindByName(__.OnlineWangWang);

            /// <summary>站点URL</summary>
            public static readonly Field SkinName = FindByName(__.SkinName);

            /// <summary>公众号名称</summary>
            public static readonly Field OfficialName = FindByName(__.OfficialName);

            /// <summary>公众号介绍</summary>
            public static readonly Field OfficialDecsription = FindByName(__.OfficialDecsription);

            /// <summary>公众号原始ID</summary>
            public static readonly Field OfficialOriginalId = FindByName(__.OfficialOriginalId);

            /// <summary>微信名称</summary>
            public static readonly Field WexinAccount = FindByName(__.WexinAccount);

            /// <summary>Token</summary>
            public static readonly Field Token = FindByName(__.Token);

            /// <summary>AppId</summary>
            public static readonly Field AppId = FindByName(__.AppId);

            /// <summary>AppSecret</summary>
            public static readonly Field AppSecret = FindByName(__.AppSecret);

            /// <summary>引导关注素材地址</summary>
            public static readonly Field FllowTipPageURL = FindByName(__.FllowTipPageURL);

            /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
            public static readonly Field OfficialType = FindByName(__.OfficialType);

            /// <summary>EncodingAESKey</summary>
            public static readonly Field EncodingAESKey = FindByName(__.EncodingAESKey);

            /// <summary>解密方式0,明文</summary>
            public static readonly Field DEType = FindByName(__.DEType);

            /// <summary>公众号二维码地址</summary>
            public static readonly Field OfficialQRCode = FindByName(__.OfficialQRCode);

            /// <summary>公众号头像</summary>
            public static readonly Field OfficialImg = FindByName(__.OfficialImg);

            /// <summary>最后更新时间</summary>
            public static readonly Field LastUpdateTime = FindByName(__.LastUpdateTime);

            /// <summary>最后缓存时间</summary>
            public static readonly Field LastCacheTime = FindByName(__.LastCacheTime);

            /// <summary>微信商家MCHId</summary>
            public static readonly Field MCHId = FindByName(__.MCHId);

            /// <summary>微信商家key</summary>
            public static readonly Field MCHKey = FindByName(__.MCHKey);

            /// <summary>默认运费</summary>
            public static readonly Field DefaultFare = FindByName(__.DefaultFare);

            /// <summary>最大免运费金额</summary>
            public static readonly Field MaxFreeFare = FindByName(__.MaxFreeFare);

            /// <summary>微信小程序AppId</summary>
            public static readonly Field WXAppId = FindByName(__.WXAppId);

            /// <summary>微信小程序AppSecret</summary>
            public static readonly Field WXAppSecret = FindByName(__.WXAppSecret);

            /// <summary>小程序首页是否显示清除数据按钮</summary>
            public static readonly Field IsResetData = FindByName(__.IsResetData);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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
            public const String ICP = "ICP";

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

    /// <summary>系统配置接口</summary>
    public partial interface IConfig
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 Id { get; set; }

        /// <summary>站点名称</summary>
        String SiteName { get; set; }

        /// <summary>站点URL</summary>
        String SiteUrl { get; set; }

        /// <summary>站点LOGO</summary>
        String SiteLogo { get; set; }

        /// <summary>ICP备案</summary>
        String ICP { get; set; }

        /// <summary>联系我们Email</summary>
        String SiteEmail { get; set; }

        /// <summary>网站电话</summary>
        String SiteTel { get; set; }

        /// <summary>版权所有</summary>
        String Copyright { get; set; }

        /// <summary>是否关闭网站</summary>
        Boolean IsCloseSite { get; set; }

        /// <summary>关闭原因</summary>
        String CloseReason { get; set; }

        /// <summary>统计代码</summary>
        String CountScript { get; set; }

        /// <summary>微博二维码</summary>
        String WeiboQRCode { get; set; }

        /// <summary>微信二维码</summary>
        String WinxinQRCode { get; set; }

        /// <summary>关键字</summary>
        String Keyword { get; set; }

        /// <summary>描述</summary>
        String Description { get; set; }

        /// <summary>首页标题</summary>
        String IndexTitle { get; set; }

        /// <summary>是否URL地址重写</summary>
        Int32 IsRewrite { get; set; }

        /// <summary>搜索最小时间间距 秒</summary>
        Int32 SearchMinTime { get; set; }

        /// <summary>在线QQ</summary>
        String OnlineQQ { get; set; }

        /// <summary>在线Skype</summary>
        String OnlineSkype { get; set; }

        /// <summary>在线阿里旺旺</summary>
        String OnlineWangWang { get; set; }

        /// <summary>站点URL</summary>
        String SkinName { get; set; }

        /// <summary>公众号名称</summary>
        String OfficialName { get; set; }

        /// <summary>公众号介绍</summary>
        String OfficialDecsription { get; set; }

        /// <summary>公众号原始ID</summary>
        String OfficialOriginalId { get; set; }

        /// <summary>微信名称</summary>
        String WexinAccount { get; set; }

        /// <summary>Token</summary>
        String Token { get; set; }

        /// <summary>AppId</summary>
        String AppId { get; set; }

        /// <summary>AppSecret</summary>
        String AppSecret { get; set; }

        /// <summary>引导关注素材地址</summary>
        String FllowTipPageURL { get; set; }

        /// <summary>公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号</summary>
        Int32 OfficialType { get; set; }

        /// <summary>EncodingAESKey</summary>
        String EncodingAESKey { get; set; }

        /// <summary>解密方式0,明文</summary>
        Int32 DEType { get; set; }

        /// <summary>公众号二维码地址</summary>
        String OfficialQRCode { get; set; }

        /// <summary>公众号头像</summary>
        String OfficialImg { get; set; }

        /// <summary>最后更新时间</summary>
        DateTime LastUpdateTime { get; set; }

        /// <summary>最后缓存时间</summary>
        DateTime LastCacheTime { get; set; }

        /// <summary>微信商家MCHId</summary>
        String MCHId { get; set; }

        /// <summary>微信商家key</summary>
        String MCHKey { get; set; }

        /// <summary>默认运费</summary>
        Decimal DefaultFare { get; set; }

        /// <summary>最大免运费金额</summary>
        Decimal MaxFreeFare { get; set; }

        /// <summary>微信小程序AppId</summary>
        String WXAppId { get; set; }

        /// <summary>微信小程序AppSecret</summary>
        String WXAppSecret { get; set; }

        /// <summary>小程序首页是否显示清除数据按钮</summary>
        Int32 IsResetData { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}