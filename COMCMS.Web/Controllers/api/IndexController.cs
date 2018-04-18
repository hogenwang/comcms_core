using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COMCMS.Web.Controllers.api
{
    [Route("api/[controller]/[action]")]
    public class IndexController : APIBaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public object Get()
        {
            Config cfg = Config.GetSystemConfig();
            return cfg;
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
        public object CheckSign(string id, string random = "", string timeStamp = "", string signature = "")
        {
            if (!AutoCheckQueryStringSignature())
            {
                reJson.message = "验签失败！";
                return reJson;
            }
            else
            {
                reJson.code = 0;
                reJson.message = "验签成功！";
                return reJson;
            }
        }
        #endregion
    }
}
