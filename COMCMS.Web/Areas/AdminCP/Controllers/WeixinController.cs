using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("微信公众号系统")]
    [Description("微信公众号系统，包括关注自动回复、自定义菜单、关键字回复")]
    public class WeixinController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}