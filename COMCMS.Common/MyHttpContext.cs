using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Common
{
    /// <summary>
    /// 获取HttpContext
    /// </summary>
    public static class MyHttpContext
    {
        public static IServiceProvider ServiceProvider;
        static MyHttpContext()
        { }

        public static HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
        //需要在sartup中加入
    }
}
