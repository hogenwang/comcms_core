using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewLife.Log;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace COMCMS.Web.Common
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly IWebHostEnvironment _env;
        public HttpGlobalExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {

            //ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ExceptionContext));
            XTrace.WriteException(context.Exception);
            //log.Error(context.Exception.Message, context.Exception);

            //var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            //    logger.LogError(new EventId(context.Exception.HResult),
            //    context.Exception,
            //    context.Exception.Message);

            //var json = new ErrorResponse("未知错误,请重试");

            //if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

            //context.Result = new ApplicationErrorResult(json);
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //context.ExceptionHandled = true;
        }


        public class ApplicationErrorResult : ObjectResult
        {
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        public class ErrorResponse
        {
            public ErrorResponse(string msg)
            {
                Message = msg;
            }
            public string Message { get; set; }
            public object DeveloperMessage { get; set; }
        }
    }
}
