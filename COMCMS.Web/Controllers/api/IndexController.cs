using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using COMCMS.Web.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COMCMS.Web.Controllers.api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [WebAPIHandleError]
    public class IndexController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public object Get()
        {
            Config cfg = Config.GetSystemConfig();
            return new
            {
                cfg.SiteName,
                cfg.SiteUrl,
                cfg.SiteLogo,
                cfg.SiteEmail,
                cfg.SiteTel,
                cfg.Copyright,
                cfg.Keyword,
                cfg.Description
            };
        }

        //#region 测试出错
        //[HttpGet]
        //public object GetTest()
        //{
        //    int a = 1,b=0;
        //    var c = a / b;
        //    return reJson;
        //}
        //#endregion
    }
}
