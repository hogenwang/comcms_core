using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Web.Common;
using COMCMS.Web.Controllers.api;
using Lib.Core.MiddlewareExtension;
using Microsoft.AspNetCore.Http;
using NewLife.Log;
using System.Text.Json;

namespace COMCMS.Web.ExceptionHandler
{
    public class ResultExceptionHandler : IExceptionHandler
    {
        public async Task ExceptionHandle(HttpContext context, Exception exception)
        {
            XTrace.WriteException(exception);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            var problem = new
            {
                type = "https://httpstatuses.com/500",
                title = "服务器内部错误",
                status = 500,
                traceId = context.TraceIdentifier
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
