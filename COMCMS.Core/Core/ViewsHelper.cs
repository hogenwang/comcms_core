using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Core;
using COMCMS.Common;
using XCode;


namespace COMCMS.Core
{
    /// <summary>
    /// 识图方法
    /// </summary>
    public class ViewsHelper
    {
        #region 获取分类链接
        /// <summary>
        /// 文章分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoURL(ArticleCategory model)
        {
            if (model == null)
                return "javascript:;";
            if (!string.IsNullOrEmpty(model.FilePath))
                return model.FilePath + "/index.html";
            else
                return $"/article/index/{model.Id}";
        }
        /// <summary>
        /// 商品分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoURL(Category model)
        {
            if (model == null)
                return "javascript:;";
            if (!string.IsNullOrEmpty(model.FilePath))
                return model.FilePath + "/index.html";
            else
                return $"/product/index/{model.Id}";
        }
        /// <summary>
        /// 生成文章详情地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoURL(Article model)
        {
            if (model == null)
                return "javascript:;";

            ArticleCategory categoty = ArticleCategory.FindById(model.KId);

            if (categoty != null && !string.IsNullOrEmpty(categoty.FilePath))
            {
                string url = $"{categoty.FilePath}/{model.Id}.html";
                if (!string.IsNullOrEmpty(model.FileName))
                {
                    url = $"{categoty.FilePath}/{model.FileName}";
                }
                return url;
            }
            else
                return $"/article/detail/{model.Id}";

        }
        /// <summary>
        /// 商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoURL(Product model)
        {
            return $"/product/detail/{model.Id}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string EchoURL(Utils.CMSType ctype, int id)
        {
            string url = "/";
            switch (ctype)
            {
                case Utils.CMSType.ArticleCategory:
                    ArticleCategory ac = ArticleCategory.FindById(id);
                    url = EchoURL(ac);
                    break;
                case Utils.CMSType.ProductCategory:
                    Category pc = Category.FindById(id);
                    url = EchoURL(pc);
                    break;
                case Utils.CMSType.Article:
                    Article a = Article.FindById(id);
                    url = EchoURL(a);
                    break;
                case Utils.CMSType.Product:
                    Product p = Product.FindById(id);
                    url = EchoURL(p);
                    break;
                default:
                    url = "/";
                    break;
            }

            return url;
        }
        #endregion

        #region 生成详情地址
        /// <summary>
        /// 生成文章详情地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoArticleURL(Article model)
        {
            if (model == null)
                return "javascript:;";

            if (!string.IsNullOrEmpty(model.ArticleKind.FilePath))
            {
                string url = $"{model.ArticleKind.FilePath}/{model.Id}.html";
                if (!string.IsNullOrEmpty(model.FileName))
                {
                    url = $"{model.ArticleKind.FilePath}/{model.FileName}";
                }
                return url;
            }
            else
                return $"/article/detail/{model.Id}";
        }
        /// <summary>
        /// 生成文章详情地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EchoArticleURL(int id)
        {
            Article model = Article.FindById(id);
            return EchoArticleURL(model);
        }
        #endregion

    }
}
