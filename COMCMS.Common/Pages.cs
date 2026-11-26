using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;

namespace COMCMS.Common
{
    public class Pages
    {
        private static readonly Regex SafeSegmentRegex = new(@"^[a-zA-Z0-9/_-]+$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));

        #region 列表分页
        /// <summary>
        /// 生成分页 HTML（严格编码输出，避免反射型 XSS 与 string.Format 崩溃）
        /// </summary>
        public static string GetPageStr(int pageSize, int recordCount, int page, string url, int isRewrite, int maxShowPage, string filepath = "")
        {
            if (pageSize <= 0 || recordCount < 1)
                return "";

            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize > 0)
                pageCount += 1;
            if (pageCount < 2)
                return "";

            if (page < 1) page = 1;
            if (page > pageCount) page = pageCount;
            if (maxShowPage < 1) maxShowPage = 5;

            var query = MyHttpContext.Current?.Request?.QueryString.Value ?? string.Empty;
            var rawId = MyHttpContext.Current?.Request?.Query["id"].ToString() ?? string.Empty;
            return BuildPageHtml(pageCount, page, url, isRewrite, maxShowPage, filepath, query, rawId);
        }

        /// <summary>
        /// 纯函数分页生成器（便于单元测试）
        /// </summary>
        public static string BuildPageHtml(int pageCount, int page, string url, int isRewrite, int maxShowPage, string filepath, string rawQueryString, string rawId)
        {
            if (pageCount < 2) return "";
            if (page < 1) page = 1;
            if (page > pageCount) page = pageCount;
            if (maxShowPage < 1) maxShowPage = 5;

            string safeBaseUrl = SanitizeUrlBase(url);
            string safeId = int.TryParse(rawId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedId)
                ? parsedId.ToString(CultureInfo.InvariantCulture)
                : "0";
            string safeFilePath = !string.IsNullOrWhiteSpace(filepath) && SafeSegmentRegex.IsMatch(filepath)
                ? filepath.Trim('/')
                : string.Empty;

            // 解析并规范化查询参数，防止任意注入与 format 占位符冲突
            var parsedQuery = QueryHelpers.ParseQuery(rawQueryString ?? string.Empty);

            string BuildLink(int targetPage)
            {
                string pageStr = targetPage.ToString(CultureInfo.InvariantCulture);
                switch (isRewrite)
                {
                    case 1:
                    {
                        var baseAspx = safeBaseUrl.ToLowerInvariant().Replace(".html", ".aspx", StringComparison.Ordinal);
                        var withId = ReplaceLastDot(baseAspx, $"-{safeId}.");
                        return targetPage == 1 ? withId : ReplaceLastDot(withId, $"-{pageStr}.");
                    }
                    case 2:
                    {
                        var baseHtml = safeBaseUrl.ToLowerInvariant().Replace(".aspx", ".html", StringComparison.Ordinal);
                        var withId = ReplaceLastDot(baseHtml, $"-{safeId}.");
                        return targetPage == 1 ? withId : ReplaceLastDot(withId, $"-{pageStr}.");
                    }
                    case 3:
                    {
                        var lower = safeBaseUrl.ToLowerInvariant();
                        if (lower.EndsWith(".html", StringComparison.Ordinal) || lower.EndsWith(".aspx", StringComparison.Ordinal))
                        {
                            var baseHtml = lower.Replace(".aspx", ".html", StringComparison.Ordinal);
                            return targetPage == 1 ? baseHtml : ReplaceLastDot(baseHtml, $"-{pageStr}.");
                        }
                        return QueryHelpers.AddQueryString(safeBaseUrl, "page", pageStr);
                    }
                    case 5:
                    {
                        var indexUrl = string.IsNullOrEmpty(safeFilePath) ? "/index.html" : $"/{safeFilePath}/index.html";
                        return targetPage == 1 ? indexUrl : ReplaceLastDot(indexUrl, $"-{pageStr}.");
                    }
                    case 99:
                    {
                        // Ajax 模板模式，安全替换 {0} 而不调用 string.Format
                        return safeBaseUrl.Replace("{0}", pageStr, StringComparison.Ordinal);
                    }
                    case 0:
                    default:
                    {
                        var queryDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var kv in parsedQuery)
                        {
                            if (string.Equals(kv.Key, "page", StringComparison.OrdinalIgnoreCase)) continue;
                            if (!string.IsNullOrEmpty(kv.Key))
                                queryDict[kv.Key] = kv.Value.ToString();
                        }
                        queryDict["page"] = pageStr;
                        return QueryHelpers.AddQueryString(safeBaseUrl, queryDict);
                    }
                }
            }

            var encoder = HtmlEncoder.Default;
            var strPage = new StringBuilder();
            strPage.Append("<div class=\"pages\">");

            if (page != 1)
            {
                strPage.Append("<span class=\"prev\"><a href=\"")
                    .Append(encoder.Encode(BuildLink(1)))
                    .Append("\" title=\"首页\">首页</a></span>");
            }

            if (page > 1)
            {
                strPage.Append("<span class=\"prev\"><a href=\"")
                    .Append(encoder.Encode(BuildLink(page - 1)))
                    .Append("\" title=\"上一页\">上一页</a></span>");
            }

            int sizes = maxShowPage / 2;
            int minPage = page - sizes;
            int maxPage = page + sizes;
            if (minPage <= 0)
            {
                minPage = 1;
                maxPage = maxShowPage;
            }
            if (maxPage >= pageCount)
            {
                minPage = Math.Max(1, pageCount - maxShowPage + 1);
                maxPage = pageCount;
            }

            for (int i = minPage; i <= Math.Min(maxPage, pageCount); i++)
            {
                string label = i.ToString(CultureInfo.InvariantCulture);
                if (i == page)
                {
                    strPage.Append("<span class=\"curr\"><a href=\"javascript:;\" title=\"")
                        .Append(label)
                        .Append("\">")
                        .Append(label)
                        .Append("</a></span>");
                }
                else
                {
                    strPage.Append("<span><a href=\"")
                        .Append(encoder.Encode(BuildLink(i)))
                        .Append("\" title=\"")
                        .Append(label)
                        .Append("\">")
                        .Append(label)
                        .Append("</a></span>");
                }
            }

            if (page < pageCount)
            {
                strPage.Append("<span class=\"next\"><a href=\"")
                    .Append(encoder.Encode(BuildLink(page + 1)))
                    .Append("\" title=\"下一页\">下一页</a></span>");
                strPage.Append("<span class=\"pageEnd\"><a href=\"")
                    .Append(encoder.Encode(BuildLink(pageCount)))
                    .Append("\" title=\"尾页\">尾页</a></span>");
            }

            strPage.Append("</div>");
            return strPage.ToString();
        }

        private static string SanitizeUrlBase(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "/";
            var trimmed = url.Trim();
            if (trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
            {
                return "/";
            }
            return trimmed;
        }

        private static string ReplaceLastDot(string input, string replacement)
        {
            var index = input.LastIndexOf('.');
            if (index < 0) return input;
            return input.Substring(0, index) + replacement + input.Substring(index + 1);
        }
        #endregion
    }
}
