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
using COMCMS.Web.Services;
using COMCMS.Common.Security;
using COMCMS.Core;
using Lib.Core.MiddlewareExtension.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using ComCmsAuthSchemes = COMCMS.Common.Security.AuthenticationSchemes;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using NewLife.Caching;
using NewLife.Caching.Services;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using XCode.DataAccessLayer;

namespace COMCMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加Configuration到静态变量
            Utils.AddUtils(Configuration);

            // XCode can load appsettings.json independently of the host configuration. Register
            // the resolved connection explicitly so local and deployment overrides are honored.
            var dbConnectionString = Configuration["connectionStrings:dbconn:connectionString"];
            if (!string.IsNullOrWhiteSpace(dbConnectionString))
            {
                DAL.AddConnStr(
                    "dbconn",
                    dbConnectionString,
                    null,
                    Configuration["connectionStrings:dbconn:providerName"]);
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddMyHttpContextAccessor();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "RequestVerificationToken";
                options.Cookie.Name = "COMCMS.Antiforgery";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = Environment.IsDevelopment()
                    ? CookieSecurePolicy.SameAsRequest
                    : CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
            services.AddProblemDetails();
            services.AddHttpClient();
            services.AddHttpClient(RemoteImageService.ClientName, client => client.Timeout = TimeSpan.FromSeconds(10))
                .ConfigurePrimaryHttpMessageHandler(RemoteImageService.CreateHandler);
            services.AddHealthChecks();
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("login", context => RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(15),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    }));
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            var redisConnectionString = Configuration["RedisCache:ConnectionString"];
            if (Environment.IsDevelopment() || string.IsNullOrWhiteSpace(redisConnectionString))
            {
                if (!Environment.IsDevelopment())
                    throw new InvalidOperationException("RedisCache:ConnectionString is required outside Development.");
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                    options.InstanceName = Configuration["RedisCache:InstanceName"] ?? "COMCMS:";
                });
                var redis = ConnectionMultiplexer.Connect(redisConnectionString);
                services.AddSingleton<IConnectionMultiplexer>(redis);
                services.AddDataProtection()
                    .SetApplicationName("COMCMS")
                    .PersistKeysToStackExchangeRedis(redis, "COMCMS:DataProtectionKeys");
            }
            //添加Session 服务
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;

            });
            // 绑定缓存设置
            services.Configure<CacheSettings>(Configuration.GetSection("CacheSettings"));
            // 响应缓存
            services.AddResponseCaching();
            services.AddSingleton<ICacheProvider, RedisCacheProvider>();
            // 缓存服务封装
            services.AddSingleton<ICacheService, CacheService>();
            //部分系统配置
            services.Configure<SystemSetting>(Configuration.GetSection("SystemSetting"));
            services.Configure<AuthenticationSettings>(Configuration.GetSection("Authentication"));
            services.Configure<SecuritySettings>(Configuration.GetSection("Security"));
            services.AddSingleton<JwtKeyProvider>();
            services.AddScoped<TokenService>();
            services.AddScoped<PasswordRecoveryService>();
            services.AddScoped<RemoteImageService>();
            services.AddSingleton<PrivateFileStorage>();
            services.AddSingleton<PublicMediaStorage>();
            services.AddSingleton<PaymentIdempotencyService>();
            services.AddSingleton<LoginAttemptService>();
            services.AddSingleton<IContentSanitizer, ContentSanitizer>();
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationPolicyProvider, AdminPermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, AdminPermissionHandler>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AdminAuthorizationResultHandler>();

            services
                .AddControllersWithViews(options =>
                {
                    //记录错误
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ComCmsAuthSchemes.Bearer;
                options.DefaultChallengeScheme = ComCmsAuthSchemes.Bearer;
            })
            .AddCookie(ComCmsAuthSchemes.AdminCookie, options =>
            {
                options.Cookie.Name = "COMCMS.Admin";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.LoginPath = "/AdminCP/Login";
                options.AccessDeniedPath = "/AdminCP/Index/NotAuthorize";
                options.Events.OnValidatePrincipal = ValidateAdminCookie;
                options.Events.OnRedirectToLogin = RedirectCookieLogin;
                options.Events.OnRedirectToAccessDenied = RedirectCookieAccessDenied;
            })
            .AddCookie(ComCmsAuthSchemes.MemberCookie, options =>
            {
                options.Cookie.Name = "COMCMS.Member";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Events.OnValidatePrincipal = ValidateMemberCookie;
                options.Events.OnRedirectToLogin = RedirectCookieLogin;
                options.Events.OnRedirectToAccessDenied = RedirectCookieAccessDenied;
            })
            .AddJwtBearer(ComCmsAuthSchemes.Bearer, options =>
            {
                options.RequireHttpsMetadata = !Environment.IsDevelopment();
                options.SaveToken = false;
            });

            services.AddOptions<JwtBearerOptions>(ComCmsAuthSchemes.Bearer)
                .Configure<JwtKeyProvider, IOptions<AuthenticationSettings>>((options, keyProvider, authOptions) =>
                {
                    var settings = authOptions.Value;
                    options.TokenValidationParameters = keyProvider.CreateValidationParameters(settings);
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = ValidateBearerToken
                    };
                });

            // 设置表单内容限制
            services.Configure<FormOptions>(options =>
            {
                //formOptions.ValueLengthLimit = int.MaxValue; // 表单内容大小限制，默认4194304，单位byte
                //formOptions.MultipartBodyLengthLimit = int.MaxValue; // 如果是multipart，默认134217728
                options.ValueCountLimit = 4096;
                options.ValueLengthLimit = 4 * 1024 * 1024;
                options.KeyLengthLimit = 2048;
                options.MultipartBodyLengthLimit = Configuration.GetValue<long?>("Security:MaxUploadBytes") ?? 52_428_800;
                options.MultipartBoundaryLengthLimit = 256;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp, IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            _ = svp.GetRequiredService<JwtKeyProvider>();
            var forwardedOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                ForwardLimit = 2
            };
            foreach (var proxy in Configuration.GetSection("Security:KnownProxies").Get<string[]>() ?? Array.Empty<string>())
            {
                if (IPAddress.TryParse(proxy, out var address)) forwardedOptions.KnownProxies.Add(address);
            }
            app.UseForwardedHeaders(forwardedOptions);
            app.UseMiddlewareExtension(new ResultExceptionHandler());

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
                context.Response.Headers.Append("Permissions-Policy", "camera=(), microphone=(), geolocation=()");
                context.Response.Headers.Append("Content-Security-Policy-Report-Only", "default-src 'self'; object-src 'none'; base-uri 'self'; frame-ancestors 'self'");
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


            app.UseStatusCodePages(async statusContext =>
            {
                var response = statusContext.HttpContext.Response;
                var request = statusContext.HttpContext.Request;
                var isApi = request.Path.StartsWithSegments("/api");
                if (!isApi && HttpMethods.IsGet(request.Method) &&
                    request.GetTypedHeaders().Accept?.Any(item => item.MediaType.Value.Contains("text/html", StringComparison.OrdinalIgnoreCase)) == true)
                {
                    response.Redirect($"/StatusCode/{response.StatusCode}");
                    return;
                }

                response.ContentType = "application/problem+json";
                await response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = response.StatusCode,
                    Title = ReasonPhrases.GetReasonPhrase(response.StatusCode),
                    Extensions = { ["traceId"] = statusContext.HttpContext.TraceIdentifier }
                });
            });
            //启用Session
            app.UseSession();
            app.UseMyMVCDI();
            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value?.EndsWith(".swf", StringComparison.OrdinalIgnoreCase) == true)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }
                await next();
            });
            if (!env.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {
                    if (context.Request.Path.StartsWithSegments("/install") ||
                        context.Request.Path.StartsWithSegments("/Home/Install"))
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }
                    await next();
                });
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticHttpContext();
            app.UseRouting();
            app.UseRateLimiter();

            // 启用响应缓存
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
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

            IRegisterService register = RegisterService.Start(senparcSetting.Value).UseSenparcGlobal();
            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value);//微信全局注册，必须！
            //加入HttpContext
            //MyHttpContext.ServiceProvider = svp;
        }

        private static Task ValidateAdminCookie(CookieValidatePrincipalContext context)
        {
            if (!TryValidateAbsoluteLifetime(context.Principal, TimeSpan.FromHours(8)))
            {
                context.RejectPrincipal();
                return context.HttpContext.SignOutAsync(ComCmsAuthSchemes.AdminCookie);
            }

            var admin = Admin.Find(Admin._.Id == ComCmsClaimTypes.GetSubjectId(context.Principal));
            var actualStamp = context.Principal.FindFirstValue(ComCmsClaimTypes.SecurityStamp);
            var expectedStamp = admin == null ? null : SecurityStampService.Compute("admin", admin.Id, admin.PassWord, admin.RoleId, admin.IsLock);
            if (admin == null || admin.IsLock == 1 || !SecurityStampService.Equals(expectedStamp, actualStamp))
            {
                context.RejectPrincipal();
                return context.HttpContext.SignOutAsync(ComCmsAuthSchemes.AdminCookie);
            }
            return Task.CompletedTask;
        }

        private static Task ValidateMemberCookie(CookieValidatePrincipalContext context)
        {
            if (!TryValidateAbsoluteLifetime(context.Principal, TimeSpan.FromHours(8)))
            {
                context.RejectPrincipal();
                return context.HttpContext.SignOutAsync(ComCmsAuthSchemes.MemberCookie);
            }
            var tokenService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
            if (!tokenService.ValidateActiveSession(context.Principal))
            {
                context.RejectPrincipal();
                return context.HttpContext.SignOutAsync(ComCmsAuthSchemes.MemberCookie);
            }
            return Task.CompletedTask;
        }

        private static Task ValidateBearerToken(TokenValidatedContext context)
        {
            var tokenService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
            if (!tokenService.ValidateActiveSession(context.Principal))
                context.Fail("The account session is no longer valid.");
            return Task.CompletedTask;
        }

        private static Task RedirectCookieLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            if (IsApiOrAjax(context.Request))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            }
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        }

        private static Task RedirectCookieAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            if (IsApiOrAjax(context.Request))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        }

        private static bool IsApiOrAjax(HttpRequest request) =>
            request.Path.StartsWithSegments("/api") ||
            string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);

        private static bool TryValidateAbsoluteLifetime(ClaimsPrincipal principal, TimeSpan lifetime)
        {
            return long.TryParse(principal?.FindFirstValue("auth_time"), out var seconds) &&
                   DateTimeOffset.FromUnixTimeSeconds(seconds).Add(lifetime) > DateTimeOffset.UtcNow;
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
