using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Common;
using COMCMS.Web.ExceptionHandler;
using COMCMS.Web.Models;
using Lib.Core.MiddlewareExtension.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;

namespace COMCMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加Configuration到静态变量
            Utils.AddUtils(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMyHttpContextAccessor();

            services.AddDistributedMemoryCache();
            //添加Session 服务
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;

            });
            //部分系统配置
            services.Configure<SystemSetting>(Configuration.GetSection("SystemSetting"));

            services
                .AddControllersWithViews(options =>
                {
                    //记录错误
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                })
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            //防止汉字被自动编码
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddSenparcGlobalServices(Configuration)//Senparc.CO2NET 全局注册
            .AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册（如果使用Senparc.Weixin SDK则添加）

            //注册Cookie认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            // 配置JWT 验证
            var appSettingsSection = Configuration.GetSection("SystemSetting");
            var appSettings = appSettingsSection.Get<SystemSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // 设置表单内容限制
            services.Configure<FormOptions>(options =>
            {
                //formOptions.ValueLengthLimit = int.MaxValue; // 表单内容大小限制，默认4194304，单位byte
                //formOptions.MultipartBodyLengthLimit = int.MaxValue; // 如果是multipart，默认134217728
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.KeyLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartBoundaryLengthLimit = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp, IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });
            
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            //其他错误页面处理
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            //启用Session
            app.UseSession();
            app.UseMyMVCDI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticHttpContext();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Index}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "index",
                pattern: "/index.html",
                defaults: new { controller = "Home", action = "Index" }
                );

                //endpoints.MapControllerRoute(
                //name: "article",
                //pattern: "{title}/index.html",
                //defaults: new { controller = "Home", action = "Article" }
                //);

                //endpoints.MapControllerRoute(
                //name: "article2",
                //pattern: "{title}/",
                //defaults: new { controller = "Home", action = "Article" }
                //);
            });

            //使用环境变量
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMiddlewareExtension(new ResultExceptionHandler());
            IRegisterService register = RegisterService.Start(senparcSetting.Value).UseSenparcGlobal();
            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value);//微信全局注册，必须！
            //加入HttpContext
            //MyHttpContext.ServiceProvider = svp;
        }

        #region Senparc 缓存扩展策略
        /// <summary>
        /// 获取Container扩展缓存策略
        /// </summary>
        /// <returns></returns>
        private IList<IDomainExtensionCacheStrategy> GetExContainerCacheStrategies()
        {
            var exContainerCacheStrategies = new List<IDomainExtensionCacheStrategy>();

            //如果有配置，可以去掉下面注释

            ////判断Redis是否可用
            //var redisConfiguration = ConfigurationManager.AppSettings["Cache_Redis_Configuration"];
            //if ((!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置"))
            //{
            //    exContainerCacheStrategies.Add(RedisContainerCacheStrategy.Instance);
            //}

            ////判断Memcached是否可用
            //var memcachedConfiguration = ConfigurationManager.AppSettings["Cache_Memcached_Configuration"];
            //if ((!string.IsNullOrEmpty(memcachedConfiguration) && memcachedConfiguration != "Memcached配置"))
            //{
            //    exContainerCacheStrategies.Add(MemcachedContainerCacheStrategy.Instance);
            //}

            //也可扩展自定义的缓存策略

            return exContainerCacheStrategies;
        }
        #endregion
    }
}
