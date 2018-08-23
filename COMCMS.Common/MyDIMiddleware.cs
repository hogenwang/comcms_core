using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace COMCMS.Common
{
    public static class Extensions
    {

        public static IApplicationBuilder UseMyMVCDI(this IApplicationBuilder builder)
        {
            DI.ServiceProvider = builder.ApplicationServices;
            return builder;
        }
    }
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DI
    {
        public static IServiceProvider ServiceProvider
        {
            get;set;
        }
    }
}
