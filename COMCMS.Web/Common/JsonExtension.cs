using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace COMCMS.Web.Common
{
    public static class JsonExtension
    {
        public static string ToJson<TEntity>(this TEntity entity) where TEntity : class
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
