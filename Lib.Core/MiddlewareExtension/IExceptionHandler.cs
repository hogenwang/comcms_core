using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lib.Core.MiddlewareExtension
{
    public interface IExceptionHandler
    {
        Task ExceptionHandle(HttpContext context, Exception exception);
    }
}