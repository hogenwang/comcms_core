using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Core;
using COMCMS.Common;
using XCode;


namespace COMCMS.Web.Common
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
                return "";
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
                return "";
            if (!string.IsNullOrEmpty(model.FilePath))
                return model.FilePath + "/index.html";
            else
                return $"/product/index/{model.Id}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string EchoURL(Utils.CMSType ctype,int id)
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
                default:
                    url= "/";
                    break;
            }

            return url;
        }
        #endregion

    }
}
