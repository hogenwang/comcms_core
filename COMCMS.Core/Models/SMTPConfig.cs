using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMCMS.Core.Models
{
    /// <summary>
    /// STMP配置
    /// </summary>
    public class SMTPConfigEntity
    {
        public SMTPConfigEntity()
        { }
        #region Model
        private string _smtpemail = string.Empty;
        private string _smtphost = string.Empty;
        private string _smtpprot = "25";
        private string _smtpemailpwd = string.Empty;
        private string _postusername = string.Empty;

        private int _siteid = 0;
        /// <summary>
        /// 网站ID
        /// </summary>
        public int SiteId
        {
            get { return _siteid; }
            set { _siteid = value; }
        }

        /// <summary>
        /// SMTP Email 帐号
        /// </summary>
        public string SmtpEmail
        {
            set { _smtpemail = value; }
            get { return _smtpemail; }
        }
        /// <summary>
        /// SMTP 服务器
        /// </summary>
        public string SmtpHost
        {
            set { _smtphost = value; }
            get { return _smtphost; }
        }
        /// <summary>
        /// SMTP 服务器端口
        /// </summary>
        public string SmtpProt
        {
            set { _smtpprot = value; }
            get { return _smtpprot; }
        }
        /// <summary>
        /// SMTP Email 密码
        /// </summary>
        public string SmtpEmailPwd
        {
            set { _smtpemailpwd = value; }
            get { return _smtpemailpwd; }
        }
        /// <summary>
        /// 发信人
        /// </summary>
        public string PostUserName
        {
            set { _postusername = value; }
            get { return _postusername; }
        }
        /// <summary>
        /// 是否采用SSL发信
        /// </summary>
        public int IsSSL { get; set; } = 0;
        #endregion Model
    }

}
