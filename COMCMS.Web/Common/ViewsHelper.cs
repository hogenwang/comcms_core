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
        public static string EchoURL(ArticleCategory model)
        {
            if (model == null)
                return "";
            if (!string.IsNullOrEmpty(model.FilePath))
                return model.FilePath + "/index.html";
            else
                return $"/article/index/{model.Id}";
        }
        #endregion

    }
}
