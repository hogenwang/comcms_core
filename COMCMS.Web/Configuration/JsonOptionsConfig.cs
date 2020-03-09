using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XCode;

namespace COMCMS.Web.Configuration
{
    public class JsonOptionsConfig
    {
        public static void ConfigJsonOptions(MvcNewtonsoftJsonOptions options)
        {
            var settings = options.SerializerSettings;
            //日期类型默认格式化处理
            // //setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            //// 空值处理
            //setting.NullValueHandling = NullValueHandling.Ignore;

            // 最大序列化深度，包括请求传参的序列化深度，不可设置太小了
            settings.MaxDepth = 5;

            // 忽略循环引用
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // 全局设置要排除的字段
            settings.ContractResolver = new LimitPropsContractResolver(new[] { "password", "extends" }, false);
        }
    }

    /// <summary>
    /// 限制某些属性输出或排除，设置为保留时，只输出保留属性，其他属性全部忽略。
    /// 设置为排除，只排除设置的属性
    /// </summary>
    public class LimitPropsContractResolver : CamelCasePropertyNamesContractResolver// DefaultContractResolver 
    {
        private readonly string[] _props = null;

        private readonly bool _retain;

        /// <inheritdoc />
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = false)
        {
            //指定要序列化属性的清单
            this._props = props;

            this._retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var list = base.CreateProperties(type, memberSerialization);

            // 非实体类型直接返回
            if (!typeof(EntityBase).IsAssignableFrom(type))
            {
                return list;
            }

            //只保留清单有列出的属性
            list = list.Where(p =>
            {
                var has = _props.Any(a => a.Equals(p.PropertyName, StringComparison.OrdinalIgnoreCase));
                if (_retain)
                {
                    return has;
                }

                return !has;
            }).ToList();
            return list;
        }
    }
}
