using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace COMCMS.Web.Common
{
    /// <summary>
    /// 用于在 Razor &lt;script&gt; 块中安全嵌入 JSON，防止 &lt;/script&gt; 闭合及 XSS 注入。
    /// </summary>
    public static class SafeScriptJson
    {
        private static readonly JsonSerializerSettings SafeSettings = new()
        {
            StringEscapeHandling = StringEscapeHandling.EscapeHtml
        };

        /// <summary>
        /// 将对象序列化为可安全内联到 &lt;script&gt; 中的 JSON。
        /// </summary>
        public static IHtmlContent Serialize(object value)
        {
            var json = JsonConvert.SerializeObject(value, SafeSettings);
            return new HtmlString(EscapeScriptSeparators(json));
        }

        /// <summary>
        /// 将数据库中已存储的 JSON 字符串重新规范化并转义 HTML 敏感字符后再内联到 &lt;script&gt;。
        /// </summary>
        public static IHtmlContent FromRawJson(string rawJson, string fallback = "null")
        {
            if (string.IsNullOrWhiteSpace(rawJson))
                return new HtmlString(fallback);

            try
            {
                var token = JToken.Parse(rawJson);
                var safe = JsonConvert.SerializeObject(token, SafeSettings);
                return new HtmlString(EscapeScriptSeparators(safe));
            }
            catch (JsonException)
            {
                return new HtmlString(fallback);
            }
        }

        private static string EscapeScriptSeparators(string json)
        {
            return json
                .Replace("\u2028", "\\u2028")
                .Replace("\u2029", "\\u2029");
        }
    }
}
