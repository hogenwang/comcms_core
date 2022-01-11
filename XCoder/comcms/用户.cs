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
    /// <summary>用户</summary>
    [Serializable]
    [DataObject]
    [Description("用户")]
    [BindTable("Member", Description = "用户", ConnName = "dbconn", DbType = DatabaseType.SqlServer)]
    public partial class Member
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
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "")]
        public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "", Master = true)]
        public String Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

        private Int32 _RoleId;
        /// <summary>会员组，代理级别</summary>
        [DisplayName("会员组")]
        [Description("会员组，代理级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleId", "会员组，代理级别", "", Master = true)]
        public Int32 RoleId { get => _RoleId; set { if (OnPropertyChanging("RoleId", value)) { _RoleId = value; OnPropertyChanged("RoleId"); } } }

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

        private String _Nickname;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Nickname", "昵称", "")]
        public String Nickname { get => _Nickname; set { if (OnPropertyChanging("Nickname", value)) { _Nickname = value; OnPropertyChanged("Nickname"); } } }

        private String _UserImg;
        /// <summary>头像</summary>
        [DisplayName("头像")]
        [Description("头像")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("UserImg", "头像", "")]
        public String UserImg { get => _UserImg; set { if (OnPropertyChanging("UserImg", value)) { _UserImg = value; OnPropertyChanged("UserImg"); } } }

        private Int32 _Sex;
        /// <summary>性别 0 保密 1 男 2女</summary>
        [DisplayName("性别0保密1男2女")]
        [Description("性别 0 保密 1 男 2女")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sex", "性别 0 保密 1 男 2女", "")]
        public Int32 Sex { get => _Sex; set { if (OnPropertyChanging("Sex", value)) { _Sex = value; OnPropertyChanged("Sex"); } } }

        private DateTime _Birthday;
        /// <summary>生日</summary>
        [DisplayName("生日")]
        [Description("生日")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Birthday", "生日", "")]
        public DateTime Birthday { get => _Birthday; set { if (OnPropertyChanging("Birthday", value)) { _Birthday = value; OnPropertyChanged("Birthday"); } } }

        private String _Phone;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Phone", "电话", "")]
        public String Phone { get => _Phone; set { if (OnPropertyChanging("Phone", value)) { _Phone = value; OnPropertyChanged("Phone"); } } }

        private String _Fax;
        /// <summary>传真</summary>
        [DisplayName("传真")]
        [Description("传真")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Fax", "传真", "")]
        public String Fax { get => _Fax; set { if (OnPropertyChanging("Fax", value)) { _Fax = value; OnPropertyChanged("Fax"); } } }

        private String _Qq;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Qq", "QQ", "")]
        public String Qq { get => _Qq; set { if (OnPropertyChanging("Qq", value)) { _Qq = value; OnPropertyChanged("Qq"); } } }

        private String _Weixin;
        /// <summary>微信</summary>
        [DisplayName("微信")]
        [Description("微信")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weixin", "微信", "")]
        public String Weixin { get => _Weixin; set { if (OnPropertyChanging("Weixin", value)) { _Weixin = value; OnPropertyChanged("Weixin"); } } }

        private String _Alipay;
        /// <summary>支付宝</summary>
        [DisplayName("支付宝")]
        [Description("支付宝")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Alipay", "支付宝", "")]
        public String Alipay { get => _Alipay; set { if (OnPropertyChanging("Alipay", value)) { _Alipay = value; OnPropertyChanged("Alipay"); } } }

        private String _Skype;
        /// <summary>skype</summary>
        [DisplayName("skype")]
        [Description("skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "skype", "")]
        public String Skype { get => _Skype; set { if (OnPropertyChanging("Skype", value)) { _Skype = value; OnPropertyChanged("Skype"); } } }

        private String _Homepage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Homepage", "主页", "")]
        public String Homepage { get => _Homepage; set { if (OnPropertyChanging("Homepage", value)) { _Homepage = value; OnPropertyChanged("Homepage"); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Company", "公司", "")]
        public String Company { get => _Company; set { if (OnPropertyChanging("Company", value)) { _Company = value; OnPropertyChanged("Company"); } } }

        private String _Idno;
        /// <summary>身份证</summary>
        [DisplayName("身份证")]
        [Description("身份证")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Idno", "身份证", "")]
        public String Idno { get => _Idno; set { if (OnPropertyChanging("Idno", value)) { _Idno = value; OnPropertyChanged("Idno"); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "")]
        public String Country { get => _Country; set { if (OnPropertyChanging("Country", value)) { _Country = value; OnPropertyChanged("Country"); } } }

        private String _Province;
        /// <summary>省</summary>
        [DisplayName("省")]
        [Description("省")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省", "")]
        public String Province { get => _Province; set { if (OnPropertyChanging("Province", value)) { _Province = value; OnPropertyChanged("Province"); } } }

        private String _City;
        /// <summary>市</summary>
        [DisplayName("市")]
        [Description("市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "市", "")]
        public String City { get => _City; set { if (OnPropertyChanging("City", value)) { _City = value; OnPropertyChanged("City"); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "")]
        public String District { get => _District; set { if (OnPropertyChanging("District", value)) { _District = value; OnPropertyChanged("District"); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "")]
        public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

        private String _Postcode;
        /// <summary>邮政编码</summary>
        [DisplayName("邮政编码")]
        [Description("邮政编码")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Postcode", "邮政编码", "")]
        public String Postcode { get => _Postcode; set { if (OnPropertyChanging("Postcode", value)) { _Postcode = value; OnPropertyChanged("Postcode"); } } }

        private String _RegIP;
        /// <summary>注册IP</summary>
        [DisplayName("注册IP")]
        [Description("注册IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RegIP", "注册IP", "")]
        public String RegIP { get => _RegIP; set { if (OnPropertyChanging("RegIP", value)) { _RegIP = value; OnPropertyChanged("RegIP"); } } }

        private DateTime _RegTime;
        /// <summary>注册时间</summary>
        [DisplayName("注册时间")]
        [Description("注册时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RegTime", "注册时间", "")]
        public DateTime RegTime { get => _RegTime; set { if (OnPropertyChanging("RegTime", value)) { _RegTime = value; OnPropertyChanged("RegTime"); } } }

        private Int32 _LoginCount;
        /// <summary>登录次数</summary>
        [DisplayName("登录次数")]
        [Description("登录次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LoginCount", "登录次数", "")]
        public Int32 LoginCount { get => _LoginCount; set { if (OnPropertyChanging("LoginCount", value)) { _LoginCount = value; OnPropertyChanged("LoginCount"); } } }

        private String _RndNum;
        /// <summary>随机字符</summary>
        [DisplayName("随机字符")]
        [Description("随机字符")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RndNum", "随机字符", "")]
        public String RndNum { get => _RndNum; set { if (OnPropertyChanging("RndNum", value)) { _RndNum = value; OnPropertyChanged("RndNum"); } } }

        private DateTime _RePassWordTime;
        /// <summary>找回密码时间</summary>
        [DisplayName("找回密码时间")]
        [Description("找回密码时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RePassWordTime", "找回密码时间", "")]
        public DateTime RePassWordTime { get => _RePassWordTime; set { if (OnPropertyChanging("RePassWordTime", value)) { _RePassWordTime = value; OnPropertyChanged("RePassWordTime"); } } }

        private String _Question;
        /// <summary>保密问题</summary>
        [DisplayName("保密问题")]
        [Description("保密问题")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Question", "保密问题", "")]
        public String Question { get => _Question; set { if (OnPropertyChanging("Question", value)) { _Question = value; OnPropertyChanged("Question"); } } }

        private String _Answer;
        /// <summary>保密答案</summary>
        [DisplayName("保密答案")]
        [Description("保密答案")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Answer", "保密答案", "")]
        public String Answer { get => _Answer; set { if (OnPropertyChanging("Answer", value)) { _Answer = value; OnPropertyChanged("Answer"); } } }

        private Decimal _Balance;
        /// <summary>可用余额</summary>
        [DisplayName("可用余额")]
        [Description("可用余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Balance", "可用余额", "")]
        public Decimal Balance { get => _Balance; set { if (OnPropertyChanging("Balance", value)) { _Balance = value; OnPropertyChanged("Balance"); } } }

        private Decimal _GiftBalance;
        /// <summary>赠送余额</summary>
        [DisplayName("赠送余额")]
        [Description("赠送余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GiftBalance", "赠送余额", "")]
        public Decimal GiftBalance { get => _GiftBalance; set { if (OnPropertyChanging("GiftBalance", value)) { _GiftBalance = value; OnPropertyChanged("GiftBalance"); } } }

        private Decimal _Rebate;
        /// <summary>返利，分销提成</summary>
        [DisplayName("返利")]
        [Description("返利，分销提成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rebate", "返利，分销提成", "")]
        public Decimal Rebate { get => _Rebate; set { if (OnPropertyChanging("Rebate", value)) { _Rebate = value; OnPropertyChanged("Rebate"); } } }

        private String _Bank;
        /// <summary>银行</summary>
        [DisplayName("银行")]
        [Description("银行")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Bank", "银行", "")]
        public String Bank { get => _Bank; set { if (OnPropertyChanging("Bank", value)) { _Bank = value; OnPropertyChanged("Bank"); } } }

        private String _BankCardNO;
        /// <summary>银行卡号</summary>
        [DisplayName("银行卡号")]
        [Description("银行卡号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankCardNO", "银行卡号", "")]
        public String BankCardNO { get => _BankCardNO; set { if (OnPropertyChanging("BankCardNO", value)) { _BankCardNO = value; OnPropertyChanged("BankCardNO"); } } }

        private String _BankBranch;
        /// <summary>支行名称</summary>
        [DisplayName("支行名称")]
        [Description("支行名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankBranch", "支行名称", "")]
        public String BankBranch { get => _BankBranch; set { if (OnPropertyChanging("BankBranch", value)) { _BankBranch = value; OnPropertyChanged("BankBranch"); } } }

        private String _BankRealname;
        /// <summary>银行卡姓名</summary>
        [DisplayName("银行卡姓名")]
        [Description("银行卡姓名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankRealname", "银行卡姓名", "")]
        public String BankRealname { get => _BankRealname; set { if (OnPropertyChanging("BankRealname", value)) { _BankRealname = value; OnPropertyChanged("BankRealname"); } } }

        private Decimal _YearsPerformance;
        /// <summary>年业务量</summary>
        [DisplayName("年业务量")]
        [Description("年业务量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("YearsPerformance", "年业务量", "")]
        public Decimal YearsPerformance { get => _YearsPerformance; set { if (OnPropertyChanging("YearsPerformance", value)) { _YearsPerformance = value; OnPropertyChanged("YearsPerformance"); } } }

        private Decimal _ExtCredits1;
        /// <summary>备用积分1</summary>
        [DisplayName("备用积分1")]
        [Description("备用积分1")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits1", "备用积分1", "")]
        public Decimal ExtCredits1 { get => _ExtCredits1; set { if (OnPropertyChanging("ExtCredits1", value)) { _ExtCredits1 = value; OnPropertyChanged("ExtCredits1"); } } }

        private Decimal _ExtCredits2;
        /// <summary>备用积分2</summary>
        [DisplayName("备用积分2")]
        [Description("备用积分2")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits2", "备用积分2", "")]
        public Decimal ExtCredits2 { get => _ExtCredits2; set { if (OnPropertyChanging("ExtCredits2", value)) { _ExtCredits2 = value; OnPropertyChanged("ExtCredits2"); } } }

        private Decimal _ExtCredits3;
        /// <summary>备用积分3</summary>
        [DisplayName("备用积分3")]
        [Description("备用积分3")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits3", "备用积分3", "")]
        public Decimal ExtCredits3 { get => _ExtCredits3; set { if (OnPropertyChanging("ExtCredits3", value)) { _ExtCredits3 = value; OnPropertyChanged("ExtCredits3"); } } }

        private Decimal _ExtCredits4;
        /// <summary>备用积分4</summary>
        [DisplayName("备用积分4")]
        [Description("备用积分4")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits4", "备用积分4", "")]
        public Decimal ExtCredits4 { get => _ExtCredits4; set { if (OnPropertyChanging("ExtCredits4", value)) { _ExtCredits4 = value; OnPropertyChanged("ExtCredits4"); } } }

        private Decimal _ExtCredits5;
        /// <summary>备用积分5</summary>
        [DisplayName("备用积分5")]
        [Description("备用积分5")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits5", "备用积分5", "")]
        public Decimal ExtCredits5 { get => _ExtCredits5; set { if (OnPropertyChanging("ExtCredits5", value)) { _ExtCredits5 = value; OnPropertyChanged("ExtCredits5"); } } }

        private Decimal _TotalCredits;
        /// <summary>总积分</summary>
        [DisplayName("总积分")]
        [Description("总积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalCredits", "总积分", "")]
        public Decimal TotalCredits { get => _TotalCredits; set { if (OnPropertyChanging("TotalCredits", value)) { _TotalCredits = value; OnPropertyChanged("TotalCredits"); } } }

        private Int32 _Parent;
        /// <summary>父级用户ID</summary>
        [DisplayName("父级用户ID")]
        [Description("父级用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Parent", "父级用户ID", "")]
        public Int32 Parent { get => _Parent; set { if (OnPropertyChanging("Parent", value)) { _Parent = value; OnPropertyChanged("Parent"); } } }

        private Int32 _Grandfather;
        /// <summary>爷级用户ID</summary>
        [DisplayName("爷级用户ID")]
        [Description("爷级用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Grandfather", "爷级用户ID", "")]
        public Int32 Grandfather { get => _Grandfather; set { if (OnPropertyChanging("Grandfather", value)) { _Grandfather = value; OnPropertyChanged("Grandfather"); } } }

        private Int32 _IsSellers;
        /// <summary>是否是分销商</summary>
        [DisplayName("是否是分销商")]
        [Description("是否是分销商")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSellers", "是否是分销商", "")]
        public Int32 IsSellers { get => _IsSellers; set { if (OnPropertyChanging("IsSellers", value)) { _IsSellers = value; OnPropertyChanged("IsSellers"); } } }

        private Int32 _IsVerifySellers;
        /// <summary>是否已经认证的分销商，缴纳费用</summary>
        [DisplayName("是否已经认证的分销商")]
        [Description("是否已经认证的分销商，缴纳费用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerifySellers", "是否已经认证的分销商，缴纳费用", "")]
        public Int32 IsVerifySellers { get => _IsVerifySellers; set { if (OnPropertyChanging("IsVerifySellers", value)) { _IsVerifySellers = value; OnPropertyChanged("IsVerifySellers"); } } }

        private String _WeixinOpenId;
        /// <summary>微信公众号OpenId</summary>
        [DisplayName("微信公众号OpenId")]
        [Description("微信公众号OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeixinOpenId", "微信公众号OpenId", "")]
        public String WeixinOpenId { get => _WeixinOpenId; set { if (OnPropertyChanging("WeixinOpenId", value)) { _WeixinOpenId = value; OnPropertyChanged("WeixinOpenId"); } } }

        private String _WeixinAppOpenId;
        /// <summary>微信OpenId</summary>
        [DisplayName("微信OpenId")]
        [Description("微信OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeixinAppOpenId", "微信OpenId", "")]
        public String WeixinAppOpenId { get => _WeixinAppOpenId; set { if (OnPropertyChanging("WeixinAppOpenId", value)) { _WeixinAppOpenId = value; OnPropertyChanged("WeixinAppOpenId"); } } }

        private String _QQOpenId;
        /// <summary>QQ OpenId</summary>
        [DisplayName("QQOpenId")]
        [Description("QQ OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("QQOpenId", "QQ OpenId", "")]
        public String QQOpenId { get => _QQOpenId; set { if (OnPropertyChanging("QQOpenId", value)) { _QQOpenId = value; OnPropertyChanged("QQOpenId"); } } }

        private String _WeiboOpenId;
        /// <summary>微博OpenId</summary>
        [DisplayName("微博OpenId")]
        [Description("微博OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeiboOpenId", "微博OpenId", "")]
        public String WeiboOpenId { get => _WeiboOpenId; set { if (OnPropertyChanging("WeiboOpenId", value)) { _WeiboOpenId = value; OnPropertyChanged("WeiboOpenId"); } } }
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
                    case "RoleId": return _RoleId;
                    case "LastLoginTime": return _LastLoginTime;
                    case "LastLoginIP": return _LastLoginIP;
                    case "ThisLoginTime": return _ThisLoginTime;
                    case "ThisLoginIP": return _ThisLoginIP;
                    case "IsLock": return _IsLock;
                    case "Nickname": return _Nickname;
                    case "UserImg": return _UserImg;
                    case "Sex": return _Sex;
                    case "Birthday": return _Birthday;
                    case "Phone": return _Phone;
                    case "Fax": return _Fax;
                    case "Qq": return _Qq;
                    case "Weixin": return _Weixin;
                    case "Alipay": return _Alipay;
                    case "Skype": return _Skype;
                    case "Homepage": return _Homepage;
                    case "Company": return _Company;
                    case "Idno": return _Idno;
                    case "Country": return _Country;
                    case "Province": return _Province;
                    case "City": return _City;
                    case "District": return _District;
                    case "Address": return _Address;
                    case "Postcode": return _Postcode;
                    case "RegIP": return _RegIP;
                    case "RegTime": return _RegTime;
                    case "LoginCount": return _LoginCount;
                    case "RndNum": return _RndNum;
                    case "RePassWordTime": return _RePassWordTime;
                    case "Question": return _Question;
                    case "Answer": return _Answer;
                    case "Balance": return _Balance;
                    case "GiftBalance": return _GiftBalance;
                    case "Rebate": return _Rebate;
                    case "Bank": return _Bank;
                    case "BankCardNO": return _BankCardNO;
                    case "BankBranch": return _BankBranch;
                    case "BankRealname": return _BankRealname;
                    case "YearsPerformance": return _YearsPerformance;
                    case "ExtCredits1": return _ExtCredits1;
                    case "ExtCredits2": return _ExtCredits2;
                    case "ExtCredits3": return _ExtCredits3;
                    case "ExtCredits4": return _ExtCredits4;
                    case "ExtCredits5": return _ExtCredits5;
                    case "TotalCredits": return _TotalCredits;
                    case "Parent": return _Parent;
                    case "Grandfather": return _Grandfather;
                    case "IsSellers": return _IsSellers;
                    case "IsVerifySellers": return _IsVerifySellers;
                    case "WeixinOpenId": return _WeixinOpenId;
                    case "WeixinAppOpenId": return _WeixinAppOpenId;
                    case "QQOpenId": return _QQOpenId;
                    case "WeiboOpenId": return _WeiboOpenId;
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
                    case "RoleId": _RoleId = value.ToInt(); break;
                    case "LastLoginTime": _LastLoginTime = value.ToDateTime(); break;
                    case "LastLoginIP": _LastLoginIP = Convert.ToString(value); break;
                    case "ThisLoginTime": _ThisLoginTime = value.ToDateTime(); break;
                    case "ThisLoginIP": _ThisLoginIP = Convert.ToString(value); break;
                    case "IsLock": _IsLock = value.ToInt(); break;
                    case "Nickname": _Nickname = Convert.ToString(value); break;
                    case "UserImg": _UserImg = Convert.ToString(value); break;
                    case "Sex": _Sex = value.ToInt(); break;
                    case "Birthday": _Birthday = value.ToDateTime(); break;
                    case "Phone": _Phone = Convert.ToString(value); break;
                    case "Fax": _Fax = Convert.ToString(value); break;
                    case "Qq": _Qq = Convert.ToString(value); break;
                    case "Weixin": _Weixin = Convert.ToString(value); break;
                    case "Alipay": _Alipay = Convert.ToString(value); break;
                    case "Skype": _Skype = Convert.ToString(value); break;
                    case "Homepage": _Homepage = Convert.ToString(value); break;
                    case "Company": _Company = Convert.ToString(value); break;
                    case "Idno": _Idno = Convert.ToString(value); break;
                    case "Country": _Country = Convert.ToString(value); break;
                    case "Province": _Province = Convert.ToString(value); break;
                    case "City": _City = Convert.ToString(value); break;
                    case "District": _District = Convert.ToString(value); break;
                    case "Address": _Address = Convert.ToString(value); break;
                    case "Postcode": _Postcode = Convert.ToString(value); break;
                    case "RegIP": _RegIP = Convert.ToString(value); break;
                    case "RegTime": _RegTime = value.ToDateTime(); break;
                    case "LoginCount": _LoginCount = value.ToInt(); break;
                    case "RndNum": _RndNum = Convert.ToString(value); break;
                    case "RePassWordTime": _RePassWordTime = value.ToDateTime(); break;
                    case "Question": _Question = Convert.ToString(value); break;
                    case "Answer": _Answer = Convert.ToString(value); break;
                    case "Balance": _Balance = Convert.ToDecimal(value); break;
                    case "GiftBalance": _GiftBalance = Convert.ToDecimal(value); break;
                    case "Rebate": _Rebate = Convert.ToDecimal(value); break;
                    case "Bank": _Bank = Convert.ToString(value); break;
                    case "BankCardNO": _BankCardNO = Convert.ToString(value); break;
                    case "BankBranch": _BankBranch = Convert.ToString(value); break;
                    case "BankRealname": _BankRealname = Convert.ToString(value); break;
                    case "YearsPerformance": _YearsPerformance = Convert.ToDecimal(value); break;
                    case "ExtCredits1": _ExtCredits1 = Convert.ToDecimal(value); break;
                    case "ExtCredits2": _ExtCredits2 = Convert.ToDecimal(value); break;
                    case "ExtCredits3": _ExtCredits3 = Convert.ToDecimal(value); break;
                    case "ExtCredits4": _ExtCredits4 = Convert.ToDecimal(value); break;
                    case "ExtCredits5": _ExtCredits5 = Convert.ToDecimal(value); break;
                    case "TotalCredits": _TotalCredits = Convert.ToDecimal(value); break;
                    case "Parent": _Parent = value.ToInt(); break;
                    case "Grandfather": _Grandfather = value.ToInt(); break;
                    case "IsSellers": _IsSellers = value.ToInt(); break;
                    case "IsVerifySellers": _IsVerifySellers = value.ToInt(); break;
                    case "WeixinOpenId": _WeixinOpenId = Convert.ToString(value); break;
                    case "WeixinAppOpenId": _WeixinAppOpenId = Convert.ToString(value); break;
                    case "QQOpenId": _QQOpenId = Convert.ToString(value); break;
                    case "WeiboOpenId": _WeiboOpenId = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户字段信息的快捷方式</summary>
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

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName("Tel");

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName("Email");

            /// <summary>会员组，代理级别</summary>
            public static readonly Field RoleId = FindByName("RoleId");

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

            /// <summary>昵称</summary>
            public static readonly Field Nickname = FindByName("Nickname");

            /// <summary>头像</summary>
            public static readonly Field UserImg = FindByName("UserImg");

            /// <summary>性别 0 保密 1 男 2女</summary>
            public static readonly Field Sex = FindByName("Sex");

            /// <summary>生日</summary>
            public static readonly Field Birthday = FindByName("Birthday");

            /// <summary>电话</summary>
            public static readonly Field Phone = FindByName("Phone");

            /// <summary>传真</summary>
            public static readonly Field Fax = FindByName("Fax");

            /// <summary>QQ</summary>
            public static readonly Field Qq = FindByName("Qq");

            /// <summary>微信</summary>
            public static readonly Field Weixin = FindByName("Weixin");

            /// <summary>支付宝</summary>
            public static readonly Field Alipay = FindByName("Alipay");

            /// <summary>skype</summary>
            public static readonly Field Skype = FindByName("Skype");

            /// <summary>主页</summary>
            public static readonly Field Homepage = FindByName("Homepage");

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName("Company");

            /// <summary>身份证</summary>
            public static readonly Field Idno = FindByName("Idno");

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName("Country");

            /// <summary>省</summary>
            public static readonly Field Province = FindByName("Province");

            /// <summary>市</summary>
            public static readonly Field City = FindByName("City");

            /// <summary>区</summary>
            public static readonly Field District = FindByName("District");

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName("Address");

            /// <summary>邮政编码</summary>
            public static readonly Field Postcode = FindByName("Postcode");

            /// <summary>注册IP</summary>
            public static readonly Field RegIP = FindByName("RegIP");

            /// <summary>注册时间</summary>
            public static readonly Field RegTime = FindByName("RegTime");

            /// <summary>登录次数</summary>
            public static readonly Field LoginCount = FindByName("LoginCount");

            /// <summary>随机字符</summary>
            public static readonly Field RndNum = FindByName("RndNum");

            /// <summary>找回密码时间</summary>
            public static readonly Field RePassWordTime = FindByName("RePassWordTime");

            /// <summary>保密问题</summary>
            public static readonly Field Question = FindByName("Question");

            /// <summary>保密答案</summary>
            public static readonly Field Answer = FindByName("Answer");

            /// <summary>可用余额</summary>
            public static readonly Field Balance = FindByName("Balance");

            /// <summary>赠送余额</summary>
            public static readonly Field GiftBalance = FindByName("GiftBalance");

            /// <summary>返利，分销提成</summary>
            public static readonly Field Rebate = FindByName("Rebate");

            /// <summary>银行</summary>
            public static readonly Field Bank = FindByName("Bank");

            /// <summary>银行卡号</summary>
            public static readonly Field BankCardNO = FindByName("BankCardNO");

            /// <summary>支行名称</summary>
            public static readonly Field BankBranch = FindByName("BankBranch");

            /// <summary>银行卡姓名</summary>
            public static readonly Field BankRealname = FindByName("BankRealname");

            /// <summary>年业务量</summary>
            public static readonly Field YearsPerformance = FindByName("YearsPerformance");

            /// <summary>备用积分1</summary>
            public static readonly Field ExtCredits1 = FindByName("ExtCredits1");

            /// <summary>备用积分2</summary>
            public static readonly Field ExtCredits2 = FindByName("ExtCredits2");

            /// <summary>备用积分3</summary>
            public static readonly Field ExtCredits3 = FindByName("ExtCredits3");

            /// <summary>备用积分4</summary>
            public static readonly Field ExtCredits4 = FindByName("ExtCredits4");

            /// <summary>备用积分5</summary>
            public static readonly Field ExtCredits5 = FindByName("ExtCredits5");

            /// <summary>总积分</summary>
            public static readonly Field TotalCredits = FindByName("TotalCredits");

            /// <summary>父级用户ID</summary>
            public static readonly Field Parent = FindByName("Parent");

            /// <summary>爷级用户ID</summary>
            public static readonly Field Grandfather = FindByName("Grandfather");

            /// <summary>是否是分销商</summary>
            public static readonly Field IsSellers = FindByName("IsSellers");

            /// <summary>是否已经认证的分销商，缴纳费用</summary>
            public static readonly Field IsVerifySellers = FindByName("IsVerifySellers");

            /// <summary>微信公众号OpenId</summary>
            public static readonly Field WeixinOpenId = FindByName("WeixinOpenId");

            /// <summary>微信OpenId</summary>
            public static readonly Field WeixinAppOpenId = FindByName("WeixinAppOpenId");

            /// <summary>QQ OpenId</summary>
            public static readonly Field QQOpenId = FindByName("QQOpenId");

            /// <summary>微博OpenId</summary>
            public static readonly Field WeiboOpenId = FindByName("WeiboOpenId");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得用户字段名称的快捷方式</summary>
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

            /// <summary>手机</summary>
            public const String Tel = "Tel";

            /// <summary>邮件</summary>
            public const String Email = "Email";

            /// <summary>会员组，代理级别</summary>
            public const String RoleId = "RoleId";

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

            /// <summary>昵称</summary>
            public const String Nickname = "Nickname";

            /// <summary>头像</summary>
            public const String UserImg = "UserImg";

            /// <summary>性别 0 保密 1 男 2女</summary>
            public const String Sex = "Sex";

            /// <summary>生日</summary>
            public const String Birthday = "Birthday";

            /// <summary>电话</summary>
            public const String Phone = "Phone";

            /// <summary>传真</summary>
            public const String Fax = "Fax";

            /// <summary>QQ</summary>
            public const String Qq = "Qq";

            /// <summary>微信</summary>
            public const String Weixin = "Weixin";

            /// <summary>支付宝</summary>
            public const String Alipay = "Alipay";

            /// <summary>skype</summary>
            public const String Skype = "Skype";

            /// <summary>主页</summary>
            public const String Homepage = "Homepage";

            /// <summary>公司</summary>
            public const String Company = "Company";

            /// <summary>身份证</summary>
            public const String Idno = "Idno";

            /// <summary>国家</summary>
            public const String Country = "Country";

            /// <summary>省</summary>
            public const String Province = "Province";

            /// <summary>市</summary>
            public const String City = "City";

            /// <summary>区</summary>
            public const String District = "District";

            /// <summary>详细地址</summary>
            public const String Address = "Address";

            /// <summary>邮政编码</summary>
            public const String Postcode = "Postcode";

            /// <summary>注册IP</summary>
            public const String RegIP = "RegIP";

            /// <summary>注册时间</summary>
            public const String RegTime = "RegTime";

            /// <summary>登录次数</summary>
            public const String LoginCount = "LoginCount";

            /// <summary>随机字符</summary>
            public const String RndNum = "RndNum";

            /// <summary>找回密码时间</summary>
            public const String RePassWordTime = "RePassWordTime";

            /// <summary>保密问题</summary>
            public const String Question = "Question";

            /// <summary>保密答案</summary>
            public const String Answer = "Answer";

            /// <summary>可用余额</summary>
            public const String Balance = "Balance";

            /// <summary>赠送余额</summary>
            public const String GiftBalance = "GiftBalance";

            /// <summary>返利，分销提成</summary>
            public const String Rebate = "Rebate";

            /// <summary>银行</summary>
            public const String Bank = "Bank";

            /// <summary>银行卡号</summary>
            public const String BankCardNO = "BankCardNO";

            /// <summary>支行名称</summary>
            public const String BankBranch = "BankBranch";

            /// <summary>银行卡姓名</summary>
            public const String BankRealname = "BankRealname";

            /// <summary>年业务量</summary>
            public const String YearsPerformance = "YearsPerformance";

            /// <summary>备用积分1</summary>
            public const String ExtCredits1 = "ExtCredits1";

            /// <summary>备用积分2</summary>
            public const String ExtCredits2 = "ExtCredits2";

            /// <summary>备用积分3</summary>
            public const String ExtCredits3 = "ExtCredits3";

            /// <summary>备用积分4</summary>
            public const String ExtCredits4 = "ExtCredits4";

            /// <summary>备用积分5</summary>
            public const String ExtCredits5 = "ExtCredits5";

            /// <summary>总积分</summary>
            public const String TotalCredits = "TotalCredits";

            /// <summary>父级用户ID</summary>
            public const String Parent = "Parent";

            /// <summary>爷级用户ID</summary>
            public const String Grandfather = "Grandfather";

            /// <summary>是否是分销商</summary>
            public const String IsSellers = "IsSellers";

            /// <summary>是否已经认证的分销商，缴纳费用</summary>
            public const String IsVerifySellers = "IsVerifySellers";

            /// <summary>微信公众号OpenId</summary>
            public const String WeixinOpenId = "WeixinOpenId";

            /// <summary>微信OpenId</summary>
            public const String WeixinAppOpenId = "WeixinAppOpenId";

            /// <summary>QQ OpenId</summary>
            public const String QQOpenId = "QQOpenId";

            /// <summary>微博OpenId</summary>
            public const String WeiboOpenId = "WeiboOpenId";
        }
        #endregion
    }
}