using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using XCode;

namespace COMCMS.Web.Controllers.api
{
    public class UserController : APIBaseController
    {
        #region 用户登录
        [HttpGet]
        public object Test()
        {
            return "abc";
        }
        #endregion
    }
}