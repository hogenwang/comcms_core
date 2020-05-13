using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Filter;
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
        [CheckFilter]
        public object Get()
        {
            Config cfg = Config.GetSystemConfig();
            return cfg;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }

        #region 测试验签
        /// <summary>
        /// 测试验签
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        [CheckFilter]
        public ReJson CheckSign(string id)
        {
            return new ReJson("验签成功！");
        }

        //POST验证
        [HttpPost]
        [CheckFilter]
        public ReJson ChekSignWidhPost(int id)
        {
            return new ReJson("验证通过！");
        }

        #endregion

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
