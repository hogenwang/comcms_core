using System;
using Microsoft.AspNetCore.Authorization;

namespace COMCMS.Web.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class MyAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "AdminPermission:";

        public MyAuthorizeAttribute(string eventKey, string menuKey, string returnType = "HTML")
        {
            EventKey = eventKey ?? string.Empty;
            MenuKey = menuKey ?? string.Empty;
            ReturnType = string.Equals(returnType, "JSON", StringComparison.OrdinalIgnoreCase) ? "JSON" : "HTML";
            Policy = $"{PolicyPrefix}{MenuKey}:{EventKey}";
        }

        public string MenuKey { get; }
        public string EventKey { get; }
        public string ReturnType { get; }
    }
}
