using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Member : IMember
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
        /// <summary>手机</summary>
        [DisplayName("手机")]
        [Description("手机")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Tel", "手机", "nvarchar(20)")]
        public String Tel { get { return _Tel; } set { if (OnPropertyChanging(__.Tel, value)) { _Tel = value; OnPropertyChanged(__.Tel); } } }

        private String _Email;
        /// <summary>邮件</summary>
        [DisplayName("邮件")]
        [Description("邮件")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Email", "邮件", "nvarchar(100)", Master = true)]
        public String Email { get { return _Email; } set { if (OnPropertyChanging(__.Email, value)) { _Email = value; OnPropertyChanged(__.Email); } } }

        private Int32 _RoleId;
        /// <summary>会员组，代理级别</summary>
        [DisplayName("会员组")]
        [Description("会员组，代理级别")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RoleId", "会员组，代理级别", "int", Master = true)]
        public Int32 RoleId { get { return _RoleId; } set { if (OnPropertyChanging(__.RoleId, value)) { _RoleId = value; OnPropertyChanged(__.RoleId); } } }

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

        private String _Nickname;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Nickname", "昵称", "nvarchar(200)")]
        public String Nickname { get { return _Nickname; } set { if (OnPropertyChanging(__.Nickname, value)) { _Nickname = value; OnPropertyChanged(__.Nickname); } } }

        private String _UserImg;
        /// <summary>头像</summary>
        [DisplayName("头像")]
        [Description("头像")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("UserImg", "头像", "nvarchar(250)")]
        public String UserImg { get { return _UserImg; } set { if (OnPropertyChanging(__.UserImg, value)) { _UserImg = value; OnPropertyChanged(__.UserImg); } } }

        private Int32 _Sex;
        /// <summary>性别 0 保密 1 男 2女</summary>
        [DisplayName("性别0保密1男2女")]
        [Description("性别 0 保密 1 男 2女")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Sex", "性别 0 保密 1 男 2女", "int")]
        public Int32 Sex { get { return _Sex; } set { if (OnPropertyChanging(__.Sex, value)) { _Sex = value; OnPropertyChanged(__.Sex); } } }

        private DateTime _Birthday;
        /// <summary>生日</summary>
        [DisplayName("生日")]
        [Description("生日")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("Birthday", "生日", "datetime")]
        public DateTime Birthday { get { return _Birthday; } set { if (OnPropertyChanging(__.Birthday, value)) { _Birthday = value; OnPropertyChanged(__.Birthday); } } }

        private String _Phone;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Phone", "电话", "nvarchar(20)")]
        public String Phone { get { return _Phone; } set { if (OnPropertyChanging(__.Phone, value)) { _Phone = value; OnPropertyChanged(__.Phone); } } }

        private String _Fax;
        /// <summary>传真</summary>
        [DisplayName("传真")]
        [Description("传真")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Fax", "传真", "nvarchar(20)")]
        public String Fax { get { return _Fax; } set { if (OnPropertyChanging(__.Fax, value)) { _Fax = value; OnPropertyChanged(__.Fax); } } }

        private String _Qq;
        /// <summary>QQ</summary>
        [DisplayName("QQ")]
        [Description("QQ")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Qq", "QQ", "nvarchar(20)")]
        public String Qq { get { return _Qq; } set { if (OnPropertyChanging(__.Qq, value)) { _Qq = value; OnPropertyChanged(__.Qq); } } }

        private String _Weixin;
        /// <summary>微信</summary>
        [DisplayName("微信")]
        [Description("微信")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Weixin", "微信", "nvarchar(50)")]
        public String Weixin { get { return _Weixin; } set { if (OnPropertyChanging(__.Weixin, value)) { _Weixin = value; OnPropertyChanged(__.Weixin); } } }

        private String _Alipay;
        /// <summary>支付宝</summary>
        [DisplayName("支付宝")]
        [Description("支付宝")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Alipay", "支付宝", "nvarchar(50)")]
        public String Alipay { get { return _Alipay; } set { if (OnPropertyChanging(__.Alipay, value)) { _Alipay = value; OnPropertyChanged(__.Alipay); } } }

        private String _Skype;
        /// <summary>skype</summary>
        [DisplayName("skype")]
        [Description("skype")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Skype", "skype", "nvarchar(100)")]
        public String Skype { get { return _Skype; } set { if (OnPropertyChanging(__.Skype, value)) { _Skype = value; OnPropertyChanged(__.Skype); } } }

        private String _Homepage;
        /// <summary>主页</summary>
        [DisplayName("主页")]
        [Description("主页")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Homepage", "主页", "nvarchar(100)")]
        public String Homepage { get { return _Homepage; } set { if (OnPropertyChanging(__.Homepage, value)) { _Homepage = value; OnPropertyChanged(__.Homepage); } } }

        private String _Company;
        /// <summary>公司</summary>
        [DisplayName("公司")]
        [Description("公司")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Company", "公司", "nvarchar(100)")]
        public String Company { get { return _Company; } set { if (OnPropertyChanging(__.Company, value)) { _Company = value; OnPropertyChanged(__.Company); } } }

        private String _Idno;
        /// <summary>身份证</summary>
        [DisplayName("身份证")]
        [Description("身份证")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Idno", "身份证", "nvarchar(20)")]
        public String Idno { get { return _Idno; } set { if (OnPropertyChanging(__.Idno, value)) { _Idno = value; OnPropertyChanged(__.Idno); } } }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Country", "国家", "nvarchar(50)")]
        public String Country { get { return _Country; } set { if (OnPropertyChanging(__.Country, value)) { _Country = value; OnPropertyChanged(__.Country); } } }

        private String _Province;
        /// <summary>省</summary>
        [DisplayName("省")]
        [Description("省")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Province", "省", "nvarchar(50)")]
        public String Province { get { return _Province; } set { if (OnPropertyChanging(__.Province, value)) { _Province = value; OnPropertyChanged(__.Province); } } }

        private String _City;
        /// <summary>市</summary>
        [DisplayName("市")]
        [Description("市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("City", "市", "nvarchar(50)")]
        public String City { get { return _City; } set { if (OnPropertyChanging(__.City, value)) { _City = value; OnPropertyChanged(__.City); } } }

        private String _District;
        /// <summary>区</summary>
        [DisplayName("区")]
        [Description("区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("District", "区", "nvarchar(50)")]
        public String District { get { return _District; } set { if (OnPropertyChanging(__.District, value)) { _District = value; OnPropertyChanged(__.District); } } }

        private String _Address;
        /// <summary>详细地址</summary>
        [DisplayName("详细地址")]
        [Description("详细地址")]
        [DataObjectField(false, false, true, 250)]
        [BindColumn("Address", "详细地址", "nvarchar(250)")]
        public String Address { get { return _Address; } set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } } }

        private String _Postcode;
        /// <summary>邮政编码</summary>
        [DisplayName("邮政编码")]
        [Description("邮政编码")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("Postcode", "邮政编码", "nvarchar(20)")]
        public String Postcode { get { return _Postcode; } set { if (OnPropertyChanging(__.Postcode, value)) { _Postcode = value; OnPropertyChanged(__.Postcode); } } }

        private String _RegIP;
        /// <summary>注册IP</summary>
        [DisplayName("注册IP")]
        [Description("注册IP")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("RegIP", "注册IP", "nvarchar(20)")]
        public String RegIP { get { return _RegIP; } set { if (OnPropertyChanging(__.RegIP, value)) { _RegIP = value; OnPropertyChanged(__.RegIP); } } }

        private DateTime _RegTime;
        /// <summary>注册时间</summary>
        [DisplayName("注册时间")]
        [Description("注册时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RegTime", "注册时间", "datetime")]
        public DateTime RegTime { get { return _RegTime; } set { if (OnPropertyChanging(__.RegTime, value)) { _RegTime = value; OnPropertyChanged(__.RegTime); } } }

        private Int32 _LoginCount;
        /// <summary>登录次数</summary>
        [DisplayName("登录次数")]
        [Description("登录次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LoginCount", "登录次数", "int")]
        public Int32 LoginCount { get { return _LoginCount; } set { if (OnPropertyChanging(__.LoginCount, value)) { _LoginCount = value; OnPropertyChanged(__.LoginCount); } } }

        private String _RndNum;
        /// <summary>随机字符</summary>
        [DisplayName("随机字符")]
        [Description("随机字符")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RndNum", "随机字符", "nvarchar(50)")]
        public String RndNum { get { return _RndNum; } set { if (OnPropertyChanging(__.RndNum, value)) { _RndNum = value; OnPropertyChanged(__.RndNum); } } }

        private DateTime _RePassWordTime;
        /// <summary>找回密码时间</summary>
        [DisplayName("找回密码时间")]
        [Description("找回密码时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RePassWordTime", "找回密码时间", "datetime")]
        public DateTime RePassWordTime { get { return _RePassWordTime; } set { if (OnPropertyChanging(__.RePassWordTime, value)) { _RePassWordTime = value; OnPropertyChanged(__.RePassWordTime); } } }

        private String _Question;
        /// <summary>保密问题</summary>
        [DisplayName("保密问题")]
        [Description("保密问题")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Question", "保密问题", "nvarchar(50)")]
        public String Question { get { return _Question; } set { if (OnPropertyChanging(__.Question, value)) { _Question = value; OnPropertyChanged(__.Question); } } }

        private String _Answer;
        /// <summary>保密答案</summary>
        [DisplayName("保密答案")]
        [Description("保密答案")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Answer", "保密答案", "nvarchar(50)")]
        public String Answer { get { return _Answer; } set { if (OnPropertyChanging(__.Answer, value)) { _Answer = value; OnPropertyChanged(__.Answer); } } }

        private Decimal _Balance;
        /// <summary>可用余额</summary>
        [DisplayName("可用余额")]
        [Description("可用余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Balance", "可用余额", "money")]
        public Decimal Balance { get { return _Balance; } set { if (OnPropertyChanging(__.Balance, value)) { _Balance = value; OnPropertyChanged(__.Balance); } } }

        private Decimal _GiftBalance;
        /// <summary>赠送余额</summary>
        [DisplayName("赠送余额")]
        [Description("赠送余额")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("GiftBalance", "赠送余额", "money")]
        public Decimal GiftBalance { get { return _GiftBalance; } set { if (OnPropertyChanging(__.GiftBalance, value)) { _GiftBalance = value; OnPropertyChanged(__.GiftBalance); } } }

        private Decimal _Rebate;
        /// <summary>返利，分销提成</summary>
        [DisplayName("返利")]
        [Description("返利，分销提成")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rebate", "返利，分销提成", "money")]
        public Decimal Rebate { get { return _Rebate; } set { if (OnPropertyChanging(__.Rebate, value)) { _Rebate = value; OnPropertyChanged(__.Rebate); } } }

        private String _Bank;
        /// <summary>银行</summary>
        [DisplayName("银行")]
        [Description("银行")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Bank", "银行", "nvarchar(50)")]
        public String Bank { get { return _Bank; } set { if (OnPropertyChanging(__.Bank, value)) { _Bank = value; OnPropertyChanged(__.Bank); } } }

        private String _BankCardNO;
        /// <summary>银行卡号</summary>
        [DisplayName("银行卡号")]
        [Description("银行卡号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankCardNO", "银行卡号", "nvarchar(50)")]
        public String BankCardNO { get { return _BankCardNO; } set { if (OnPropertyChanging(__.BankCardNO, value)) { _BankCardNO = value; OnPropertyChanged(__.BankCardNO); } } }

        private String _BankBranch;
        /// <summary>支行名称</summary>
        [DisplayName("支行名称")]
        [Description("支行名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankBranch", "支行名称", "nvarchar(50)")]
        public String BankBranch { get { return _BankBranch; } set { if (OnPropertyChanging(__.BankBranch, value)) { _BankBranch = value; OnPropertyChanged(__.BankBranch); } } }

        private String _BankRealname;
        /// <summary>银行卡姓名</summary>
        [DisplayName("银行卡姓名")]
        [Description("银行卡姓名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BankRealname", "银行卡姓名", "nvarchar(50)")]
        public String BankRealname { get { return _BankRealname; } set { if (OnPropertyChanging(__.BankRealname, value)) { _BankRealname = value; OnPropertyChanged(__.BankRealname); } } }

        private Decimal _YearsPerformance;
        /// <summary>年业务量</summary>
        [DisplayName("年业务量")]
        [Description("年业务量")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("YearsPerformance", "年业务量", "money")]
        public Decimal YearsPerformance { get { return _YearsPerformance; } set { if (OnPropertyChanging(__.YearsPerformance, value)) { _YearsPerformance = value; OnPropertyChanged(__.YearsPerformance); } } }

        private Decimal _ExtCredits1;
        /// <summary>备用积分1</summary>
        [DisplayName("备用积分1")]
        [Description("备用积分1")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits1", "备用积分1", "money")]
        public Decimal ExtCredits1 { get { return _ExtCredits1; } set { if (OnPropertyChanging(__.ExtCredits1, value)) { _ExtCredits1 = value; OnPropertyChanged(__.ExtCredits1); } } }

        private Decimal _ExtCredits2;
        /// <summary>备用积分2</summary>
        [DisplayName("备用积分2")]
        [Description("备用积分2")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits2", "备用积分2", "money")]
        public Decimal ExtCredits2 { get { return _ExtCredits2; } set { if (OnPropertyChanging(__.ExtCredits2, value)) { _ExtCredits2 = value; OnPropertyChanged(__.ExtCredits2); } } }

        private Decimal _ExtCredits3;
        /// <summary>备用积分3</summary>
        [DisplayName("备用积分3")]
        [Description("备用积分3")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits3", "备用积分3", "money")]
        public Decimal ExtCredits3 { get { return _ExtCredits3; } set { if (OnPropertyChanging(__.ExtCredits3, value)) { _ExtCredits3 = value; OnPropertyChanged(__.ExtCredits3); } } }

        private Decimal _ExtCredits4;
        /// <summary>备用积分4</summary>
        [DisplayName("备用积分4")]
        [Description("备用积分4")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits4", "备用积分4", "money")]
        public Decimal ExtCredits4 { get { return _ExtCredits4; } set { if (OnPropertyChanging(__.ExtCredits4, value)) { _ExtCredits4 = value; OnPropertyChanged(__.ExtCredits4); } } }

        private Decimal _ExtCredits5;
        /// <summary>备用积分5</summary>
        [DisplayName("备用积分5")]
        [Description("备用积分5")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExtCredits5", "备用积分5", "money")]
        public Decimal ExtCredits5 { get { return _ExtCredits5; } set { if (OnPropertyChanging(__.ExtCredits5, value)) { _ExtCredits5 = value; OnPropertyChanged(__.ExtCredits5); } } }

        private Decimal _TotalCredits;
        /// <summary>总积分</summary>
        [DisplayName("总积分")]
        [Description("总积分")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("TotalCredits", "总积分", "money")]
        public Decimal TotalCredits { get { return _TotalCredits; } set { if (OnPropertyChanging(__.TotalCredits, value)) { _TotalCredits = value; OnPropertyChanged(__.TotalCredits); } } }

        private Int32 _Parent;
        /// <summary>父级用户ID</summary>
        [DisplayName("父级用户ID")]
        [Description("父级用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Parent", "父级用户ID", "int")]
        public Int32 Parent { get { return _Parent; } set { if (OnPropertyChanging(__.Parent, value)) { _Parent = value; OnPropertyChanged(__.Parent); } } }

        private Int32 _Grandfather;
        /// <summary>爷级用户ID</summary>
        [DisplayName("爷级用户ID")]
        [Description("爷级用户ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Grandfather", "爷级用户ID", "int")]
        public Int32 Grandfather { get { return _Grandfather; } set { if (OnPropertyChanging(__.Grandfather, value)) { _Grandfather = value; OnPropertyChanged(__.Grandfather); } } }

        private Int32 _IsSellers;
        /// <summary>是否是分销商</summary>
        [DisplayName("是否是分销商")]
        [Description("是否是分销商")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsSellers", "是否是分销商", "int")]
        public Int32 IsSellers { get { return _IsSellers; } set { if (OnPropertyChanging(__.IsSellers, value)) { _IsSellers = value; OnPropertyChanged(__.IsSellers); } } }

        private Int32 _IsVerifySellers;
        /// <summary>是否已经认证的分销商，缴纳费用</summary>
        [DisplayName("是否已经认证的分销商")]
        [Description("是否已经认证的分销商，缴纳费用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsVerifySellers", "是否已经认证的分销商，缴纳费用", "int")]
        public Int32 IsVerifySellers { get { return _IsVerifySellers; } set { if (OnPropertyChanging(__.IsVerifySellers, value)) { _IsVerifySellers = value; OnPropertyChanged(__.IsVerifySellers); } } }

        private String _WeixinOpenId;
        /// <summary>微信公众号OpenId</summary>
        [DisplayName("微信公众号OpenId")]
        [Description("微信公众号OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeixinOpenId", "微信公众号OpenId", "nvarchar(100)")]
        public String WeixinOpenId { get { return _WeixinOpenId; } set { if (OnPropertyChanging(__.WeixinOpenId, value)) { _WeixinOpenId = value; OnPropertyChanged(__.WeixinOpenId); } } }

        private String _WeixinAppOpenId;
        /// <summary>微信OpenId</summary>
        [DisplayName("微信OpenId")]
        [Description("微信OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeixinAppOpenId", "微信OpenId", "nvarchar(100)")]
        public String WeixinAppOpenId { get { return _WeixinAppOpenId; } set { if (OnPropertyChanging(__.WeixinAppOpenId, value)) { _WeixinAppOpenId = value; OnPropertyChanged(__.WeixinAppOpenId); } } }

        private String _QQOpenId;
        /// <summary>QQ OpenId</summary>
        [DisplayName("QQOpenId")]
        [Description("QQ OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("QQOpenId", "QQ OpenId", "nvarchar(100)")]
        public String QQOpenId { get { return _QQOpenId; } set { if (OnPropertyChanging(__.QQOpenId, value)) { _QQOpenId = value; OnPropertyChanged(__.QQOpenId); } } }

        private String _WeiboOpenId;
        /// <summary>微博OpenId</summary>
        [DisplayName("微博OpenId")]
        [Description("微博OpenId")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("WeiboOpenId", "微博OpenId", "nvarchar(100)")]
        public String WeiboOpenId { get { return _WeiboOpenId; } set { if (OnPropertyChanging(__.WeiboOpenId, value)) { _WeiboOpenId = value; OnPropertyChanged(__.WeiboOpenId); } } }
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
                    case __.RoleId : return _RoleId;
                    case __.LastLoginTime : return _LastLoginTime;
                    case __.LastLoginIP : return _LastLoginIP;
                    case __.ThisLoginTime : return _ThisLoginTime;
                    case __.ThisLoginIP : return _ThisLoginIP;
                    case __.IsLock : return _IsLock;
                    case __.Nickname : return _Nickname;
                    case __.UserImg : return _UserImg;
                    case __.Sex : return _Sex;
                    case __.Birthday : return _Birthday;
                    case __.Phone : return _Phone;
                    case __.Fax : return _Fax;
                    case __.Qq : return _Qq;
                    case __.Weixin : return _Weixin;
                    case __.Alipay : return _Alipay;
                    case __.Skype : return _Skype;
                    case __.Homepage : return _Homepage;
                    case __.Company : return _Company;
                    case __.Idno : return _Idno;
                    case __.Country : return _Country;
                    case __.Province : return _Province;
                    case __.City : return _City;
                    case __.District : return _District;
                    case __.Address : return _Address;
                    case __.Postcode : return _Postcode;
                    case __.RegIP : return _RegIP;
                    case __.RegTime : return _RegTime;
                    case __.LoginCount : return _LoginCount;
                    case __.RndNum : return _RndNum;
                    case __.RePassWordTime : return _RePassWordTime;
                    case __.Question : return _Question;
                    case __.Answer : return _Answer;
                    case __.Balance : return _Balance;
                    case __.GiftBalance : return _GiftBalance;
                    case __.Rebate : return _Rebate;
                    case __.Bank : return _Bank;
                    case __.BankCardNO : return _BankCardNO;
                    case __.BankBranch : return _BankBranch;
                    case __.BankRealname : return _BankRealname;
                    case __.YearsPerformance : return _YearsPerformance;
                    case __.ExtCredits1 : return _ExtCredits1;
                    case __.ExtCredits2 : return _ExtCredits2;
                    case __.ExtCredits3 : return _ExtCredits3;
                    case __.ExtCredits4 : return _ExtCredits4;
                    case __.ExtCredits5 : return _ExtCredits5;
                    case __.TotalCredits : return _TotalCredits;
                    case __.Parent : return _Parent;
                    case __.Grandfather : return _Grandfather;
                    case __.IsSellers : return _IsSellers;
                    case __.IsVerifySellers : return _IsVerifySellers;
                    case __.WeixinOpenId : return _WeixinOpenId;
                    case __.WeixinAppOpenId : return _WeixinAppOpenId;
                    case __.QQOpenId : return _QQOpenId;
                    case __.WeiboOpenId : return _WeiboOpenId;
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
                    case __.RoleId : _RoleId = Convert.ToInt32(value); break;
                    case __.LastLoginTime : _LastLoginTime = Convert.ToDateTime(value); break;
                    case __.LastLoginIP : _LastLoginIP = Convert.ToString(value); break;
                    case __.ThisLoginTime : _ThisLoginTime = Convert.ToDateTime(value); break;
                    case __.ThisLoginIP : _ThisLoginIP = Convert.ToString(value); break;
                    case __.IsLock : _IsLock = Convert.ToInt32(value); break;
                    case __.Nickname : _Nickname = Convert.ToString(value); break;
                    case __.UserImg : _UserImg = Convert.ToString(value); break;
                    case __.Sex : _Sex = Convert.ToInt32(value); break;
                    case __.Birthday : _Birthday = Convert.ToDateTime(value); break;
                    case __.Phone : _Phone = Convert.ToString(value); break;
                    case __.Fax : _Fax = Convert.ToString(value); break;
                    case __.Qq : _Qq = Convert.ToString(value); break;
                    case __.Weixin : _Weixin = Convert.ToString(value); break;
                    case __.Alipay : _Alipay = Convert.ToString(value); break;
                    case __.Skype : _Skype = Convert.ToString(value); break;
                    case __.Homepage : _Homepage = Convert.ToString(value); break;
                    case __.Company : _Company = Convert.ToString(value); break;
                    case __.Idno : _Idno = Convert.ToString(value); break;
                    case __.Country : _Country = Convert.ToString(value); break;
                    case __.Province : _Province = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.District : _District = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Postcode : _Postcode = Convert.ToString(value); break;
                    case __.RegIP : _RegIP = Convert.ToString(value); break;
                    case __.RegTime : _RegTime = Convert.ToDateTime(value); break;
                    case __.LoginCount : _LoginCount = Convert.ToInt32(value); break;
                    case __.RndNum : _RndNum = Convert.ToString(value); break;
                    case __.RePassWordTime : _RePassWordTime = Convert.ToDateTime(value); break;
                    case __.Question : _Question = Convert.ToString(value); break;
                    case __.Answer : _Answer = Convert.ToString(value); break;
                    case __.Balance : _Balance = Convert.ToDecimal(value); break;
                    case __.GiftBalance : _GiftBalance = Convert.ToDecimal(value); break;
                    case __.Rebate : _Rebate = Convert.ToDecimal(value); break;
                    case __.Bank : _Bank = Convert.ToString(value); break;
                    case __.BankCardNO : _BankCardNO = Convert.ToString(value); break;
                    case __.BankBranch : _BankBranch = Convert.ToString(value); break;
                    case __.BankRealname : _BankRealname = Convert.ToString(value); break;
                    case __.YearsPerformance : _YearsPerformance = Convert.ToDecimal(value); break;
                    case __.ExtCredits1 : _ExtCredits1 = Convert.ToDecimal(value); break;
                    case __.ExtCredits2 : _ExtCredits2 = Convert.ToDecimal(value); break;
                    case __.ExtCredits3 : _ExtCredits3 = Convert.ToDecimal(value); break;
                    case __.ExtCredits4 : _ExtCredits4 = Convert.ToDecimal(value); break;
                    case __.ExtCredits5 : _ExtCredits5 = Convert.ToDecimal(value); break;
                    case __.TotalCredits : _TotalCredits = Convert.ToDecimal(value); break;
                    case __.Parent : _Parent = Convert.ToInt32(value); break;
                    case __.Grandfather : _Grandfather = Convert.ToInt32(value); break;
                    case __.IsSellers : _IsSellers = Convert.ToInt32(value); break;
                    case __.IsVerifySellers : _IsVerifySellers = Convert.ToInt32(value); break;
                    case __.WeixinOpenId : _WeixinOpenId = Convert.ToString(value); break;
                    case __.WeixinAppOpenId : _WeixinAppOpenId = Convert.ToString(value); break;
                    case __.QQOpenId : _QQOpenId = Convert.ToString(value); break;
                    case __.WeiboOpenId : _WeiboOpenId = Convert.ToString(value); break;
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
            public static readonly Field Id = FindByName(__.Id);

            /// <summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            /// <summary>密码</summary>
            public static readonly Field PassWord = FindByName(__.PassWord);

            /// <summary>盐值</summary>
            public static readonly Field Salt = FindByName(__.Salt);

            /// <summary>姓名</summary>
            public static readonly Field RealName = FindByName(__.RealName);

            /// <summary>手机</summary>
            public static readonly Field Tel = FindByName(__.Tel);

            /// <summary>邮件</summary>
            public static readonly Field Email = FindByName(__.Email);

            /// <summary>会员组，代理级别</summary>
            public static readonly Field RoleId = FindByName(__.RoleId);

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

            /// <summary>昵称</summary>
            public static readonly Field Nickname = FindByName(__.Nickname);

            /// <summary>头像</summary>
            public static readonly Field UserImg = FindByName(__.UserImg);

            /// <summary>性别 0 保密 1 男 2女</summary>
            public static readonly Field Sex = FindByName(__.Sex);

            /// <summary>生日</summary>
            public static readonly Field Birthday = FindByName(__.Birthday);

            /// <summary>电话</summary>
            public static readonly Field Phone = FindByName(__.Phone);

            /// <summary>传真</summary>
            public static readonly Field Fax = FindByName(__.Fax);

            /// <summary>QQ</summary>
            public static readonly Field Qq = FindByName(__.Qq);

            /// <summary>微信</summary>
            public static readonly Field Weixin = FindByName(__.Weixin);

            /// <summary>支付宝</summary>
            public static readonly Field Alipay = FindByName(__.Alipay);

            /// <summary>skype</summary>
            public static readonly Field Skype = FindByName(__.Skype);

            /// <summary>主页</summary>
            public static readonly Field Homepage = FindByName(__.Homepage);

            /// <summary>公司</summary>
            public static readonly Field Company = FindByName(__.Company);

            /// <summary>身份证</summary>
            public static readonly Field Idno = FindByName(__.Idno);

            /// <summary>国家</summary>
            public static readonly Field Country = FindByName(__.Country);

            /// <summary>省</summary>
            public static readonly Field Province = FindByName(__.Province);

            /// <summary>市</summary>
            public static readonly Field City = FindByName(__.City);

            /// <summary>区</summary>
            public static readonly Field District = FindByName(__.District);

            /// <summary>详细地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            /// <summary>邮政编码</summary>
            public static readonly Field Postcode = FindByName(__.Postcode);

            /// <summary>注册IP</summary>
            public static readonly Field RegIP = FindByName(__.RegIP);

            /// <summary>注册时间</summary>
            public static readonly Field RegTime = FindByName(__.RegTime);

            /// <summary>登录次数</summary>
            public static readonly Field LoginCount = FindByName(__.LoginCount);

            /// <summary>随机字符</summary>
            public static readonly Field RndNum = FindByName(__.RndNum);

            /// <summary>找回密码时间</summary>
            public static readonly Field RePassWordTime = FindByName(__.RePassWordTime);

            /// <summary>保密问题</summary>
            public static readonly Field Question = FindByName(__.Question);

            /// <summary>保密答案</summary>
            public static readonly Field Answer = FindByName(__.Answer);

            /// <summary>可用余额</summary>
            public static readonly Field Balance = FindByName(__.Balance);

            /// <summary>赠送余额</summary>
            public static readonly Field GiftBalance = FindByName(__.GiftBalance);

            /// <summary>返利，分销提成</summary>
            public static readonly Field Rebate = FindByName(__.Rebate);

            /// <summary>银行</summary>
            public static readonly Field Bank = FindByName(__.Bank);

            /// <summary>银行卡号</summary>
            public static readonly Field BankCardNO = FindByName(__.BankCardNO);

            /// <summary>支行名称</summary>
            public static readonly Field BankBranch = FindByName(__.BankBranch);

            /// <summary>银行卡姓名</summary>
            public static readonly Field BankRealname = FindByName(__.BankRealname);

            /// <summary>年业务量</summary>
            public static readonly Field YearsPerformance = FindByName(__.YearsPerformance);

            /// <summary>备用积分1</summary>
            public static readonly Field ExtCredits1 = FindByName(__.ExtCredits1);

            /// <summary>备用积分2</summary>
            public static readonly Field ExtCredits2 = FindByName(__.ExtCredits2);

            /// <summary>备用积分3</summary>
            public static readonly Field ExtCredits3 = FindByName(__.ExtCredits3);

            /// <summary>备用积分4</summary>
            public static readonly Field ExtCredits4 = FindByName(__.ExtCredits4);

            /// <summary>备用积分5</summary>
            public static readonly Field ExtCredits5 = FindByName(__.ExtCredits5);

            /// <summary>总积分</summary>
            public static readonly Field TotalCredits = FindByName(__.TotalCredits);

            /// <summary>父级用户ID</summary>
            public static readonly Field Parent = FindByName(__.Parent);

            /// <summary>爷级用户ID</summary>
            public static readonly Field Grandfather = FindByName(__.Grandfather);

            /// <summary>是否是分销商</summary>
            public static readonly Field IsSellers = FindByName(__.IsSellers);

            /// <summary>是否已经认证的分销商，缴纳费用</summary>
            public static readonly Field IsVerifySellers = FindByName(__.IsVerifySellers);

            /// <summary>微信公众号OpenId</summary>
            public static readonly Field WeixinOpenId = FindByName(__.WeixinOpenId);

            /// <summary>微信OpenId</summary>
            public static readonly Field WeixinAppOpenId = FindByName(__.WeixinAppOpenId);

            /// <summary>QQ OpenId</summary>
            public static readonly Field QQOpenId = FindByName(__.QQOpenId);

            /// <summary>微博OpenId</summary>
            public static readonly Field WeiboOpenId = FindByName(__.WeiboOpenId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
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

    /// <summary>用户接口</summary>
    public partial interface IMember
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

        /// <summary>手机</summary>
        String Tel { get; set; }

        /// <summary>邮件</summary>
        String Email { get; set; }

        /// <summary>会员组，代理级别</summary>
        Int32 RoleId { get; set; }

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

        /// <summary>昵称</summary>
        String Nickname { get; set; }

        /// <summary>头像</summary>
        String UserImg { get; set; }

        /// <summary>性别 0 保密 1 男 2女</summary>
        Int32 Sex { get; set; }

        /// <summary>生日</summary>
        DateTime Birthday { get; set; }

        /// <summary>电话</summary>
        String Phone { get; set; }

        /// <summary>传真</summary>
        String Fax { get; set; }

        /// <summary>QQ</summary>
        String Qq { get; set; }

        /// <summary>微信</summary>
        String Weixin { get; set; }

        /// <summary>支付宝</summary>
        String Alipay { get; set; }

        /// <summary>skype</summary>
        String Skype { get; set; }

        /// <summary>主页</summary>
        String Homepage { get; set; }

        /// <summary>公司</summary>
        String Company { get; set; }

        /// <summary>身份证</summary>
        String Idno { get; set; }

        /// <summary>国家</summary>
        String Country { get; set; }

        /// <summary>省</summary>
        String Province { get; set; }

        /// <summary>市</summary>
        String City { get; set; }

        /// <summary>区</summary>
        String District { get; set; }

        /// <summary>详细地址</summary>
        String Address { get; set; }

        /// <summary>邮政编码</summary>
        String Postcode { get; set; }

        /// <summary>注册IP</summary>
        String RegIP { get; set; }

        /// <summary>注册时间</summary>
        DateTime RegTime { get; set; }

        /// <summary>登录次数</summary>
        Int32 LoginCount { get; set; }

        /// <summary>随机字符</summary>
        String RndNum { get; set; }

        /// <summary>找回密码时间</summary>
        DateTime RePassWordTime { get; set; }

        /// <summary>保密问题</summary>
        String Question { get; set; }

        /// <summary>保密答案</summary>
        String Answer { get; set; }

        /// <summary>可用余额</summary>
        Decimal Balance { get; set; }

        /// <summary>赠送余额</summary>
        Decimal GiftBalance { get; set; }

        /// <summary>返利，分销提成</summary>
        Decimal Rebate { get; set; }

        /// <summary>银行</summary>
        String Bank { get; set; }

        /// <summary>银行卡号</summary>
        String BankCardNO { get; set; }

        /// <summary>支行名称</summary>
        String BankBranch { get; set; }

        /// <summary>银行卡姓名</summary>
        String BankRealname { get; set; }

        /// <summary>年业务量</summary>
        Decimal YearsPerformance { get; set; }

        /// <summary>备用积分1</summary>
        Decimal ExtCredits1 { get; set; }

        /// <summary>备用积分2</summary>
        Decimal ExtCredits2 { get; set; }

        /// <summary>备用积分3</summary>
        Decimal ExtCredits3 { get; set; }

        /// <summary>备用积分4</summary>
        Decimal ExtCredits4 { get; set; }

        /// <summary>备用积分5</summary>
        Decimal ExtCredits5 { get; set; }

        /// <summary>总积分</summary>
        Decimal TotalCredits { get; set; }

        /// <summary>父级用户ID</summary>
        Int32 Parent { get; set; }

        /// <summary>爷级用户ID</summary>
        Int32 Grandfather { get; set; }

        /// <summary>是否是分销商</summary>
        Int32 IsSellers { get; set; }

        /// <summary>是否已经认证的分销商，缴纳费用</summary>
        Int32 IsVerifySellers { get; set; }

        /// <summary>微信公众号OpenId</summary>
        String WeixinOpenId { get; set; }

        /// <summary>微信OpenId</summary>
        String WeixinAppOpenId { get; set; }

        /// <summary>QQ OpenId</summary>
        String QQOpenId { get; set; }

        /// <summary>微博OpenId</summary>
        String WeiboOpenId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}