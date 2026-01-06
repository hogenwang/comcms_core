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
        [MyAuthorize("viewlist", "attach", "JSON")]
        public IActionResult GetDirList(string subdir)
        {
            //string attachPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            List<DirInfo> list = getAllAttach(subdir);
            return Json(new { code = 0, message = "success", data = list });
        }
        #endregion

        #region 获取指定目录下的文件
        [HttpPost]
        [MyAuthorize("viewlist", "attach", "JSON")]
        public IActionResult GetFileList(string path)
        {
            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            string currentPath = basePath;
            if (!string.IsNullOrEmpty(path))
            {
                currentPath = Path.Combine(basePath, path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            }

            if (!Directory.Exists(currentPath))
            {
                return Json(new { code = 1, message = "目录不存在" });
            }

            List<AttachFileInfo> list = new List<AttachFileInfo>();
            DirectoryInfo dir = new DirectoryInfo(currentPath);

            // 获取子目录
            foreach (var d in dir.GetDirectories())
            {
                if (d.Name.Equals("_thumbs", StringComparison.OrdinalIgnoreCase)) continue; // 过滤缩略图目录
                list.Add(new AttachFileInfo
                {
                    name = d.Name,
                    path = (string.IsNullOrEmpty(path) ? "" : path + "/") + d.Name,
                    updateTime = d.LastWriteTime,
                    isDir = true,
                    type = "folder"
                });
            }

            // 获取文件
            foreach (var f in dir.GetFiles())
            {
                string ext = f.Extension.ToLower();
                string type = "file";
                if (IsImage(ext)) type = "image";
                else if (IsVideo(ext)) type = "video";

                list.Add(new AttachFileInfo
                {
                    name = f.Name,
                    path = (string.IsNullOrEmpty(path) ? "" : path + "/") + f.Name,
                    ext = ext,
                    size = f.Length,
                    updateTime = f.LastWriteTime,
                    isDir = false,
                    type = type
                });
            }

            return Json(new { code = 0, message = "success", data = list });
        }
        #endregion

        #region 创建文件夹
        [HttpPost]
        [MyAuthorize("add", "attach", "JSON")]
        public IActionResult CreateDir(string path, string name)
        {
            if (string.IsNullOrEmpty(name)) return Json(new { code = 1, message = "文件夹名称不能为空" });
            if (!IsSafeName(name)) return Json(new { code = 1, message = "文件夹名称包含非法字符" });

            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            string currentPath = basePath;
            if (!string.IsNullOrEmpty(path))
            {
                currentPath = Path.Combine(basePath, path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            }

            string newDirPath = Path.Combine(currentPath, name);
            if (Directory.Exists(newDirPath))
            {
                return Json(new { code = 1, message = "文件夹已存在" });
            }

            try
            {
                Directory.CreateDirectory(newDirPath);
                return Json(new { code = 0, message = "创建成功" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "创建失败：" + ex.Message });
            }
        }
        #endregion

        #region 删除文件或文件夹
        [HttpPost]
        [MyAuthorize("del", "attach", "JSON")]
        public IActionResult DeleteFile(string path, bool isDir)
        {
            if (string.IsNullOrEmpty(path)) return Json(new { code = 1, message = "路径不能为空" });

            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            string fullPath = Path.Combine(basePath, path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            // 安全检查，防止删除基础目录以外的文件
            if (!Path.GetFullPath(fullPath).StartsWith(Path.GetFullPath(basePath)))
            {
                return Json(new { code = 1, message = "非法路径" });
            }

            try
            {
                if (isDir)
                {
                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true); // 递归删除
                        return Json(new { code = 0, message = "删除成功" });
                    }
                }
                else
                {
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        return Json(new { code = 0, message = "删除成功" });
                    }
                }
                return Json(new { code = 1, message = "文件或目录不存在" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "删除失败：" + ex.Message });
            }
        }
        #endregion

        #region 重命名文件或文件夹
        [HttpPost]
        [MyAuthorize("edit", "attach", "JSON")]
        public IActionResult RenameFile(string path, string newName, bool isDir)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(newName)) return Json(new { code = 1, message = "参数不能为空" });
            if (!IsSafeName(newName)) return Json(new { code = 1, message = "新名称包含非法字符" });

            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            string fullPath = Path.Combine(basePath, path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            // 安全检查
            if (!Path.GetFullPath(fullPath).StartsWith(Path.GetFullPath(basePath)))
            {
                return Json(new { code = 1, message = "非法路径" });
            }

            try
            {
                string parentDir = Path.GetDirectoryName(fullPath);
                string newFullPath = Path.Combine(parentDir, newName);

                if (isDir)
                {
                    if (!Directory.Exists(fullPath)) return Json(new { code = 1, message = "原文件夹不存在" });
                    if (Directory.Exists(newFullPath)) return Json(new { code = 1, message = "目标文件夹已存在" });
                    Directory.Move(fullPath, newFullPath);
                }
                else
                {
                    if (!System.IO.File.Exists(fullPath)) return Json(new { code = 1, message = "原文件不存在" });
                    // 保持扩展名一致性检查（可选，这里简单处理，如果newName没有扩展名则加上原扩展名，或者要求用户输入完整文件名）
                    // 假设前端传来的 newName 是完整文件名
                    if (System.IO.File.Exists(newFullPath)) return Json(new { code = 1, message = "目标文件已存在" });
                    System.IO.File.Move(fullPath, newFullPath);
                }
                return Json(new { code = 0, message = "重命名成功" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "重命名失败：" + ex.Message });
            }
        }
        #endregion

        #region 上传文件
        [HttpPost]
        [MyAuthorize("add", "attach", "JSON")]
        public async Task<IActionResult> UploadFile(string path)
        {
            // 安全检查：禁止在根目录上传
            if (string.IsNullOrEmpty(path))
            {
                return Json(new { code = 1, message = "根目录不允许上传文件，请选择子目录" });
            }

            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
            {
                return Json(new { code = 1, message = "请选择文件" });
            }

            string basePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}";
            string currentPath = basePath;
            if (!string.IsNullOrEmpty(path))
            {
                currentPath = Path.Combine(basePath, path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            }

            // 安全检查：防止目录遍历
            if (!Path.GetFullPath(currentPath).StartsWith(Path.GetFullPath(basePath)))
            {
                return Json(new { code = 1, message = "非法路径" });
            }

            if (!Directory.Exists(currentPath))
            {
                return Json(new { code = 1, message = "目录不存在" });
            }

            int successCount = 0;
            string msg = "";

            foreach (var file in files)
            {
                string fileName = file.FileName;
                // 安全检查：文件名
                if (!IsSafeName(fileName))
                {
                    msg += $"文件 {fileName} 名称非法; ";
                    continue;
                }

                // 检查扩展名
                string ext = Path.GetExtension(fileName).ToLower();
                string allowedExts = _attachsetting.FileAllowedExtensions + "," + _attachsetting.ImageAllowedExtensions + "," + _attachsetting.MediaAllowedExtensions;
                if (!allowedExts.ToLower().Split(',').Contains(ext.TrimStart('.')))
                {
                    msg += $"文件 {fileName} 类型不允许; ";
                    continue;
                }

                string fullPath = Path.Combine(currentPath, fileName);
                // 如果文件存在，重命名
                if (System.IO.File.Exists(fullPath))
                {
                    string nameNoExt = Path.GetFileNameWithoutExtension(fileName);
                    fileName = $"{nameNoExt}_{Utils.GetOrderNum()}{ext}";
                    fullPath = Path.Combine(currentPath, fileName);
                }

                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    successCount++;
                }
                catch (Exception ex)
                {
                    msg += $"文件 {fileName} 上传失败: {ex.Message}; ";
                }
            }

            if (successCount > 0)
            {
                return Json(new { code = 0, message = $"成功上传 {successCount} 个文件. {msg}" });
            }
            else
            {
                return Json(new { code = 1, message = msg });
            }
        }
        #endregion

        #region 辅助方法
        private bool IsImage(string ext)
        {
            return new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" }.Contains(ext);
        }

        private bool IsVideo(string ext)
        {
            return new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" }.Contains(ext);
        }

        private bool IsSafeName(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            // 允许字母、数字、下划线、中划线、点、中文
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[\u4e00-\u9fa5a-zA-Z0-9_\-\.]+$");
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

            if (!Directory.Exists(basePath)) return list;

            string[] arrDirs = Directory.GetDirectories(basePath);

            if (arrDirs != null && arrDirs.Length > 0)
            {
                foreach (var item in arrDirs)
                {
                    string dirName = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    if (dirName.Equals("_thumbs", StringComparison.OrdinalIgnoreCase)) continue; // 过滤缩略图目录

                    string nextSubDir = string.IsNullOrEmpty(subdir) ? dirName : subdir + "/" + dirName;
                    var tpmList = getAllAttach(nextSubDir);
                    list.Add(new DirInfo()
                    {
                        name = dirName,
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

    public class DirInfo
    {
        public string name { get; set; }
        public string path { get; set; }
        public List<DirInfo> subList { get; set; }
    }

    public class AttachFileInfo
    {
        public string name { get; set; }
        public string path { get; set; }
        public string ext { get; set; }
        public long size { get; set; }
        public DateTime updateTime { get; set; }
        public bool isDir { get; set; }
        public string type { get; set; }
    }
}
