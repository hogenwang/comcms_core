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
            XTrace.WriteException(context.Exception);
            context.HttpContext.Response.StatusCode = 500;
            ReJson model = new ReJson(5001, "服务器内部错误，请使用 TraceId 联系管理员。", new { traceId = context.HttpContext.TraceIdentifier });
            context.Result = new JsonResult(model);
            base.OnException(context);
        }
    }
}
