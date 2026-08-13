using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace COMCMS.Web.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public sealed class ExternalWebhookAttribute : Attribute, IAntiforgeryPolicy, IOrderedFilter
    {
        public int Order => 1000;
    }
}
