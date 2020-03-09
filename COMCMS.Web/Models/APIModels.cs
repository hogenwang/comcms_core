using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMCMS.Web.Models
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

    #region 通用返回信息
    public class ReJson
    {
        public ReJson(string message)
        {
            this.message = message;
            code = 0;
        }

        public ReJson(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public ReJson(int code, string message, int isReload)
        {
            this.code = code;
            this.message = message;
            this.isReload = isReload;
        }

        public ReJson(int code, string message, object detail)
        {
            this.code = code;
            this.message = message;
            this.detail = detail;
        }

        /// <summary>
        /// 返回代码 0 为正确
        /// </summary>
        public int code { get; set; } = 40000;

        /// <summary>
        /// 提示语
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 是否重新加载
        /// </summary>
        public int isReload { get; set; } = 0;

        /// <summary>
        /// 数据详情 object
        /// </summary>
        public object detail { get; set; }
    }
    #endregion
}
