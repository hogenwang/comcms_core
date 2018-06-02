using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace COMCMS.Common
{
    /// <summary>
    /// 获取HttpContext
    /// </summary>
    public static class MyHttpContext
    {
        private static IHttpContextAccessor _accessor;
        public static HttpContext Current => _accessor.HttpContext;
        internal static void Configure(IHttpContextAccessor accessor)
        {

            _accessor = accessor;

        }

        //public static IServiceProvider ServiceProvider;

        //static MyHttpContext()
        //{ }

        //public static HttpContext Current
        //{
        //    get
        //    {
                
        //        object factory = ServiceProvider.GetService<IHttpContextAccessor>();// ServiceProvider.GetService(typeof(IHttpContextAccessor));
        //        HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
        //        return context;
        //    }
        //}
        //需要在sartup中加入
    }

    public static class StaticHttpContextExtensions
    {

        public static void AddMyHttpContextAccessor(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {

            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

            MyHttpContext.Configure(httpContextAccessor);

            return app;

        }
    }
}
