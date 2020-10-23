using System;
using System.Collections.Generic;
using System.Text;

namespace COMCMS.Common
{
    public class Pages
    {

        #region 列表分页
        /// <summary>
        /// 显示分页2
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="page">当前页码</param>
        /// <param name="url">分页文件名字，静态：index.html；动态List.aspx</param>
        /// <param name="isRewrite">系统urlReWrite方案；99为ajax连接</param>
        /// <param name="maxShowPage">最大显示分页数量</param>
        /// <param name="filepath">目录名称（系统urlReWrite方案为5必填）</param>
        /// <returns></returns>
        public static string GetPageStr(int pageSize, int recordCount, int page, string url, int isRewrite, int maxShowPage, string filepath = "")
        {
            if (recordCount < 1)
            {
                return "";
            }
            int pageCount = recordCount / pageSize;//总页数
            if (recordCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount < 2)
            {
                return "";
            }
            if (page > pageCount) page = pageCount;
            StringBuilder strPage = new StringBuilder();
            string pageLink = "";// url + "?id=" + HttpContext.Current.Request.QueryString["id"] + "&page={0}";
            string strQueryString = MyHttpContext.Current.Request.QueryString.ToString().ToLower();
            if (isRewrite != 5 && isRewrite != 99)
                url = url.ToLower();
            pageLink = url + "?" + strQueryString;
            string currPage = MyHttpContext.Current.Request.Query["page"];
            string id = MyHttpContext.Current.Request.Query["id"];

            if (isRewrite == 5)
            {
                url = $"/{filepath}/index.html";
            }

            switch (isRewrite)
            {
                case 1:
                    url = url.Replace(".html", ".aspx");//避免设置错之后出现问题
                    url = url.Replace(".", "-" + id + ".");
                    pageLink = url.Replace(".", "-{0}.");
                    break;
                case 2:
                    url = url.Replace(".aspx", ".html");//避免设置错之后出现问题
                    if (!string.IsNullOrEmpty(id))
                        url = url.Replace(".", "-" + id + ".");
                    else
                        url = url.Replace(".", "-0.");
                    pageLink = url.Replace(".", "-{0}.");
                    break;
                case 3:
                    if(url.EndsWith(".html") || url.EndsWith(".aspx"))
                    {
                        url = url.Replace(".aspx", ".html");//避免设置错之后出现问题
                        pageLink = url.Replace(".", "-{0}.");
                    }
                    else
                    {
                        pageLink = url + "?page={0}";
                    }
                    break;
                case 5:
                    pageLink = url.Replace(".", "-{0}.");
                    break;
                case 99://调用ajax 连接
                    pageLink = url;
                    url = string.Format(pageLink, "1");
                    break;
                case 0:
                default:
                    //动态链接
                    if (pageLink.IndexOf("page=") > 0)
                    {
                        pageLink = pageLink.Replace("page=" + currPage, "page={0}");
                    }
                    else
                    {
                        pageLink = pageLink + "&page={0}";
                    }
                    url = string.Format(pageLink, "1");
                    break;
            }
            strPage.Append("<div class=\"pages\">");
            if (page != 1)//显示[首页]
            {
                //strPage.AppendFormat("<span class=\"pageFirst\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", url, Lang.GetPagesLangVal("firstpage"));
                strPage.AppendFormat("<span class=\"prev\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", string.Format(pageLink, 1), "首页");
            }

            if (page > 1)//上一页
            {
                if (page == 2)//[首页]要index.html
                {
                    //strPage.AppendFormat("<span class=\"prev\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", url, Lang.GetPagesLangVal("prevpage"));
                    strPage.AppendFormat("<span class=\"prev\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", string.Format(pageLink, (page - 1).ToString()), "上一页");
                }
                else
                {
                    strPage.AppendFormat("<span class=\"prev\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", string.Format(pageLink, (page - 1).ToString()), "上一页");
                }
            }
            //显示中间所有页数，如果是当前页，则外加一个样式
            string strAllPage = "<span {2}><a href=\"{0}\" title=\"{1}\">[{1}]</a></span>";

            //显示当前的多少页
            int cureSize = maxShowPage;
            int sizes = cureSize / 2;//中间
            int minPage = page - sizes;//最小起始页码
            int maxPage = page + sizes;//最大页码
            if (minPage <= 0)
            {
                minPage = 1;
                maxPage = cureSize;
            }
            if (maxPage >= pageCount)
            {
                minPage = pageCount - cureSize + 1;
                maxPage = pageCount;
            }

            for (int i = 1; i <= pageCount; i++)
            {
                if (i >= minPage && i <= maxPage)//循环最小到最大页码中间所有页数
                {
                    if (i == page)
                    {
                        strPage.Append(string.Format(strAllPage, "javascritp:;", i.ToString(), "class=\"curr\""));
                    }
                    else
                    {
                        if (i == 1)//第一页不能显示成index-1.html
                        {
                            if (isRewrite == 4)
                            {
                                strPage.Append(string.Format(strAllPage, string.Format(pageLink, i.ToString()), i.ToString(), ""));
                            }
                            else
                                strPage.Append(string.Format(strAllPage, url, "1", ""));
                        }
                        else
                        {
                            strPage.Append(string.Format(strAllPage, string.Format(pageLink, i.ToString()), i.ToString(), ""));
                        }
                    }
                }
            }
            if (page < pageCount)//显示[下一页]/[尾页]
            {
                strPage.AppendFormat("<span class=\"next\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", string.Format(pageLink, (page + 1).ToString()), "下一页");
                strPage.AppendFormat("<span class=\"pageEnd\"><a href=\"{0}\" title=\"{1}\">{1}</a></span>", string.Format(pageLink, pageCount.ToString()), "尾页");
            }
            strPage.Append("</div>");
            return strPage.ToString().Replace("[", "").Replace("]", "");
        }
        #endregion
    }
}
