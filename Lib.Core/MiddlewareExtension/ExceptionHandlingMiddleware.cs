using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lib.Core.MiddlewareExtension
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;

        public ExceptionHandlingMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "ExceptionHandlingMiddleware");

                await _exceptionHandler.ExceptionHandle(context, ex);
            }
        }
    }
}
