using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Common.Security;
using COMCMS.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace COMCMS.Web.Common
{
    public sealed record AdminPermissionRequirement(string MenuKey, string EventKey) : IAuthorizationRequirement;

    public sealed class AdminPermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallback;

        public AdminPermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _fallback = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallback.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => _fallback.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(MyAuthorizeAttribute.PolicyPrefix, StringComparison.Ordinal))
                return _fallback.GetPolicyAsync(policyName);

            var values = policyName[MyAuthorizeAttribute.PolicyPrefix.Length..].Split(':', 2);
            if (values.Length != 2 || values.Any(string.IsNullOrWhiteSpace))
                return Task.FromResult<AuthorizationPolicy>(null);

            var policy = new AuthorizationPolicyBuilder(AuthenticationSchemes.AdminCookie)
                .RequireAuthenticatedUser()
                .AddRequirements(new AdminPermissionRequirement(values[0], values[1]))
                .Build();
            return Task.FromResult(policy);
        }
    }

    public sealed class AdminPermissionHandler : AuthorizationHandler<AdminPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminPermissionRequirement requirement)
        {
            if (!string.Equals(context.User.FindFirst(ComCmsClaimTypes.SubjectType)?.Value, "admin", StringComparison.Ordinal))
                return Task.CompletedTask;

            var admin = Admin.Find(Admin._.Id == ComCmsClaimTypes.GetSubjectId(context.User));
            if (admin == null || admin.IsLock == 1) return Task.CompletedTask;

            var role = admin.Roles;
            if (role?.IsSuperAdmin == 1)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (string.IsNullOrWhiteSpace(role?.Powers)) return Task.CompletedTask;
            try
            {
                var permissions = JsonConvert.DeserializeObject<IList<AdminMenuEvent>>(role.Powers);
                if (permissions?.Any(item =>
                    string.Equals(item.MenuKey, requirement.MenuKey, StringComparison.Ordinal) &&
                    string.Equals(item.EventKey, requirement.EventKey, StringComparison.Ordinal)) == true)
                {
                    context.Succeed(requirement);
                }
            }
            catch (JsonException)
            {
                // Invalid permission data denies access.
            }
            return Task.CompletedTask;
        }
    }

    public sealed class AdminAuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _fallback = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult result)
        {
            var permission = context.GetEndpoint()?.Metadata.GetOrderedMetadata<MyAuthorizeAttribute>().LastOrDefault();
            if (!result.Forbidden || permission == null)
            {
                await _fallback.HandleAsync(next, context, policy, result);
                return;
            }

            if (permission.ReturnType == "JSON")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new JsonTip
                {
                    Id = 0,
                    Message = "您没有权限执行此操作！",
                    ReturnUrl = "/AdminCP/Login"
                });
                return;
            }

            context.Response.Redirect("/AdminCP/Index/NotAuthorize");
        }
    }
}
