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
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public object Get()
        {
            Config cfg = Config.GetSystemConfig();
            return cfg;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
