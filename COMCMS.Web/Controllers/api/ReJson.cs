using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewLife.Log;

namespace COMCMS.Web.Controllers.api
{
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