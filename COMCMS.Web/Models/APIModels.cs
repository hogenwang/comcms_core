using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMCMS.Web.Models
{
    public class APIModels
    {
        #region 修改我的个人资料Model
        public class APIModMyInfo
        {
            public string RealName { get; set; }
            public string Tel { get; set; }
            public string Weixin { get; set; }
            public string Alipay { get; set; }
            public string BankCardNO { get; set; }
            public string Bank { get; set; }
            public string SessionKey { get; set; }
        }
        #endregion

        #region 小程序获得用户信息
        /// <summary>
        /// 小程序获得用户信息
        /// </summary>
        public class WXAppUserInfo
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string nickName { get; set; }
            /// <summary>
            /// 性别 0 未知，1 男，2女
            /// </summary>
            public int gender { get; set; }
            /// <summary>
            /// 语言
            /// </summary>
            public string language { get; set; }
            /// <summary>
            /// 城市
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 省份
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 国家
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 用户头像
            /// </summary>
            public string avatarUrl { get; set; }
        }

        #endregion
    }
}
