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
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using COMCMS.Core.Models;
using Microsoft.Extensions.Options;
using System.IO;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class AttachController : AdminBaseController
    {
        private readonly SystemSetting _attachsetting;
        private IWebHostEnvironment _env;
        private AttachConfigEntity attach;
        public AttachController(IWebHostEnvironment env, IOptions<SystemSetting> attachsetting)
        {
            attach = Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _attachsetting = attachsetting.Value;
        }



        #region 附件管理
        [HttpGet]
        [MyAuthorize("viewlist", "attach")]
        public IActionResult AttachList()
        {
            return View();
        }
        #endregion

        #region 获取附件所有目录
        [HttpPost]
        [MyAuthorize("viewlist", "attach","JSON")]
        public IActionResult GetDirList(string subdir)
        {
            string attachPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";

            

            //Directory.GetDirectories()
            return Json(tip);
        }
        #endregion
    }
}
