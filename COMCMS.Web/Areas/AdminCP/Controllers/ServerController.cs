using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class ServerController : AdminBaseController
    {
        #region 中文转拼音
        [HttpPost]
        public IActionResult Hanzi2Pinyin(string name,int pid=0,string t="")
        {
            string pinyin = "/";
            if(t == "articlecategory" && pid>0)
            {
                ArticleCategory pcategory = ArticleCategory.FindById(pid);
                if(pcategory !=null && !string.IsNullOrEmpty(pcategory.FilePath))
                {
                    pinyin = pcategory.FilePath+"/";
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                pinyin += PinYinHelper.GetPinyin(name).ToLower().Replace(" ","-");
            }

            tip.Status = JsonTip.SUCCESS;
            tip.Message = pinyin;
            return Json(tip);
        }
        #endregion
    }
}