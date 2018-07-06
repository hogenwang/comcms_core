using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using NewLife.Log;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Web.Controllers.api;

namespace COMCMS.Web.Common
{
    public class WebAPIHandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Request.ContentType = "application/json";
            context.HttpContext.Request.Headers["Accept"] = "application/json";
            ReJson model = new ReJson(5001, "执行错误：" + context.Exception.Message);
            context.Result = new JsonResult(model);
            base.OnException(context);
        }
    }
}
