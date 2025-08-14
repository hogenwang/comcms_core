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
            List<DirInfo> all = getAllAttach("");
            string allJson = JsonConvert.SerializeObject(all);
            //string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";//基础目录
            ViewBag.folderJson = allJson;
            //string[] arrDirs = Directory.GetDirectories(basePath);
            return View();
            //return Content(JsonConvert.SerializeObject(allJson));
        }
        #endregion

        #region 获取附件所有目录
        [HttpPost]
        [MyAuthorize("viewlist", "attach","JSON")]
        public IActionResult GetDirList(string subdir)
        {
            //string attachPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";

            

            //Directory.GetDirectories()
            return Json(tip);
        }
        #endregion

        #region 获取附件目录和所有子目录树
        /// <summary>
        /// 所有所有附件目录树
        /// </summary>
        /// <param name="subdir">子目录，需要完整，如：images/2020</param>
        /// <returns></returns>
        public List<DirInfo> getAllAttach(string subdir)
        {
            List<DirInfo> list = new List<DirInfo>();
            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";//基础目录
            if (!string.IsNullOrEmpty(subdir))
            {
                basePath += subdir.Replace("/", Path.DirectorySeparatorChar.ToString());
            }

            string[] arrDirs = Directory.GetDirectories(basePath);
            
            if (arrDirs !=null && arrDirs.Length > 0)
            {
                foreach (var item in arrDirs)
                {
                    string nextSubDir = subdir + "/" + item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar)+1);
                    var tpmList = getAllAttach(nextSubDir);
                    list.Add(new DirInfo()
                    {
                        name = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar)+1),
                        path = nextSubDir,
                        //fullPath = item,
                        subList = tpmList
                    });
                }
            }

            return list;
        }
        #endregion
    }
}
