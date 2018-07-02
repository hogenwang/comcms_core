using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Lib.Core.MiddlewareExtension.Extension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddlewareExtension(this IApplicationBuilder app, IExceptionHandler exceptionHandler)
        {
             app.UseMiddleware<ExceptionHandlingMiddleware>(exceptionHandler);         

            return app;
        }
    }
}
