using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewLife.Log;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COMCMS.Web.Controllers.api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [WebAPIHandleError]
    [IgnoreAntiforgeryToken]
    public class APIBaseController : Controller
    {
        public APIBaseController()
        {
            string url = MyHttpContext.Current.Request.Path;
            string querystring = MyHttpContext.Current.Request.QueryString.ToString();
            //XTrace.WriteLine($"访问API：{url}?{querystring}");
        }
        #region 通用信息
        public ReJSON reJson = new ReJSON() { code = 40000 };
        #endregion

    }

    #region 通用返回信息
    public class ReJSON
    {
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
