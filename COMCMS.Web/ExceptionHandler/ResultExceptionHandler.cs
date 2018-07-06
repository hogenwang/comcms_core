using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Web.Common;
using COMCMS.Web.Controllers.api;
using Lib.Core.MiddlewareExtension;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.ExceptionHandler
{
    public class ResultExceptionHandler : IExceptionHandler
    {
        public async Task ExceptionHandle(HttpContext context, Exception exception)
        {
            string message = exception.Message;

            ReJson error = new ReJson(message);
            context.Response.ContentType = "text/json";
            await context.Response.WriteAsync(error.ToJson());
        }
    }
}
