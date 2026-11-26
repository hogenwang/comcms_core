using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SkiaSharp;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class AttachController : AdminBaseController
    {
        private const int MaxEditedImageBytes = 20 * 1024 * 1024;
        private static readonly Regex SafeNameRegex = new(@"^[\u4e00-\u9fa5a-zA-Z0-9_\-\.]+$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));
        private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };
        private static readonly HashSet<string> VideoExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".webm", ".mov", ".ogv", ".ogg", ".avi", ".wmv", ".flv", ".mkv"
        };
        private static readonly HashSet<string> UploadableVideoExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".webm", ".mov"
        };
        private static readonly HashSet<string> SafeRenameExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".pdf", ".mp4", ".webm", ".mov"
        };

        private readonly SystemSetting _attachsetting;
        private readonly IWebHostEnvironment _env;
        private readonly AttachConfigEntity attach;

        public AttachController(IWebHostEnvironment env, IOptions<SystemSetting> attachsetting)
        {
            attach = Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _attachsetting = attachsetting.Value;
        }

        #region 附件管理主页（/AdminCP/Attach/Index）
        [HttpGet]
        [MyAuthorize("viewlist", "attach")]
        public IActionResult Index(string fieldId = "", string mode = "", string type = "", string callback = "", bool multiSelect = false)
        {
            List<DirInfo> all = getAllAttach("");
            string allJson = JsonConvert.SerializeObject(all, new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });
            ViewBag.folderJson = allJson;
            ViewBag.fieldId = fieldId ?? string.Empty;
            ViewBag.mode = mode ?? string.Empty;
            ViewBag.type = type ?? string.Empty;
            ViewBag.callback = callback ?? string.Empty;
            ViewBag.multiSelect = multiSelect;
            ViewBag.attachBaseUrl = "/" + GetAttachFolderName();
            return View("Index");
        }

        // 兼容历史菜单路由 /AdminCP/Attach/AttachList
        [HttpGet]
        [MyAuthorize("viewlist", "attach")]
        public IActionResult AttachList(string fieldId = "", string mode = "", string type = "", string callback = "", bool multiSelect = false)
        {
            return Index(fieldId, mode, type, callback, multiSelect);
        }
        #endregion

        #region PDF 在线预览
        [HttpGet]
        [MyAuthorize("viewlist", "attach")]
        public IActionResult PdfViewer(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("参数错误");

            if (!TryResolveAttachPath(path, out var fullPath, allowRoot: false))
                return BadRequest("非法路径");

            if (!System.IO.File.Exists(fullPath) || !IsPdf(Path.GetExtension(fullPath)))
                return NotFound("PDF 文件不存在");

            var normalizedRel = NormalizeRelativePath(path);
            ViewBag.pdfUrl = $"/{GetAttachFolderName()}/{normalizedRel}";
            ViewBag.fileName = Path.GetFileName(fullPath);
            return View();
        }
        #endregion

        #region 图片在线编辑器
        [HttpGet]
        [MyAuthorize("edit", "attach")]
        public IActionResult ImageEditor(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("参数错误");

            if (!TryResolveAttachPath(path, out var fullPath, allowRoot: false))
                return BadRequest("非法路径");

            var ext = Path.GetExtension(fullPath).ToLowerInvariant();
            if (!System.IO.File.Exists(fullPath) || !IsImage(ext))
                return NotFound("图片文件不存在");

            var normalizedRel = NormalizeRelativePath(path);
            ViewBag.imageUrl = $"/{GetAttachFolderName()}/{normalizedRel}";
            ViewBag.imagePath = normalizedRel;
            ViewBag.fileName = Path.GetFileNameWithoutExtension(fullPath);
            ViewBag.fileExt = ext;
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(30_000_000)]
        [MyAuthorize("edit", "attach", "JSON")]
        public IActionResult SaveEditedImage(string path, string fileName, string ext, string imageData, bool overwrite, string originalPath)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(ext) || string.IsNullOrWhiteSpace(imageData))
                return Json(new { code = 1, message = "参数不能为空" });

            var normalizedExt = ext.Trim().ToLowerInvariant();
            if (!normalizedExt.StartsWith('.'))
                normalizedExt = "." + normalizedExt;

            if (!AllowedImageExtensions.Contains(normalizedExt))
                return Json(new { code = 1, message = "仅允许保存为安全的静态图片格式" });

            var cleanFileName = fileName.Trim();
            var fullFileName = cleanFileName + normalizedExt;
            if (!IsSafeName(fullFileName) || fullFileName.StartsWith(".", StringComparison.Ordinal))
                return Json(new { code = 1, message = "文件名包含非法字符" });

            var dirPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(originalPath))
            {
                var normOrig = NormalizeRelativePath(originalPath);
                var lastSlash = normOrig.LastIndexOf('/');
                if (lastSlash > 0)
                    dirPath = normOrig[..lastSlash];
            }
            else if (!string.IsNullOrWhiteSpace(path))
            {
                dirPath = NormalizeRelativePath(path);
            }

            if (!TryResolveAttachPath(dirPath, out var currentPath, allowRoot: false))
                return Json(new { code = 1, message = "非法保存目录（不允许保存在根目录）" });

            if (!Directory.Exists(currentPath))
                return Json(new { code = 1, message = "目标目录不存在" });

            try
            {
                var base64Data = imageData;
                var commaIdx = base64Data.IndexOf(',');
                if (commaIdx >= 0)
                    base64Data = base64Data[(commaIdx + 1)..];

                var imageBytes = Convert.FromBase64String(base64Data);
                if (imageBytes.Length == 0 || imageBytes.Length > MaxEditedImageBytes)
                    return Json(new { code = 1, message = "图片数据为空或超过 20 MiB 限制" });

                using (var ms = new MemoryStream(imageBytes))
                using (var codec = SKCodec.Create(ms))
                {
                    if (codec == null)
                        return Json(new { code = 1, message = "提交的内容不是有效图片数据" });
                }

                var targetFullPath = Path.Combine(currentPath, fullFileName);
                if (!overwrite && System.IO.File.Exists(targetFullPath))
                {
                    fullFileName = $"{cleanFileName}_{Utils.GetOrderNum()}{normalizedExt}";
                    targetFullPath = Path.Combine(currentPath, fullFileName);
                }

                if (!TryResolveAttachPath(Path.GetRelativePath(GetAttachRoot(), targetFullPath), out targetFullPath, allowRoot: false))
                    return Json(new { code = 1, message = "非法目标路径" });

                System.IO.File.WriteAllBytes(targetFullPath, imageBytes);

                var savedPath = string.IsNullOrEmpty(dirPath) ? fullFileName : dirPath + "/" + fullFileName;
                var savedUrl = $"/{GetAttachFolderName()}/{savedPath}";

                Core.Admin.WriteLogActions($"编辑并保存图片({savedPath});");
                return Json(new { code = 0, message = "保存成功", data = new { path = savedPath, url = savedUrl, fileName = fullFileName } });
            }
            catch (FormatException)
            {
                return Json(new { code = 1, message = "图片编码格式无效" });
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                return Json(new { code = 1, message = "保存失败，请使用 TraceId 联系管理员。", traceId = HttpContext.TraceIdentifier });
            }
        }
        #endregion

        #region 获取附件所有目录
        [HttpPost]
        [MyAuthorize("viewlist", "attach", "JSON")]
        public IActionResult GetDirList(string subdir)
        {
            List<DirInfo> list = getAllAttach(subdir);
            return Json(new { code = 0, message = "success", data = list });
        }
        #endregion

        #region 获取指定目录下的文件
        [HttpPost]
        [MyAuthorize("viewlist", "attach", "JSON")]
        public IActionResult GetFileList(string path)
        {
            if (!TryResolveAttachPath(path, out var currentPath))
                return Json(new { code = 1, message = "非法路径" });

            if (!Directory.Exists(currentPath))
            {
                return Json(new { code = 1, message = "目录不存在" });
            }

            var normalizedPath = NormalizeRelativePath(path);
            var folderPrefix = "/" + GetAttachFolderName() + "/";
            var list = new List<AttachFileInfo>();
            var dir = new DirectoryInfo(currentPath);

            foreach (var d in dir.GetDirectories().OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase))
            {
                if (d.Name.Equals("_thumbs", StringComparison.OrdinalIgnoreCase)) continue;
                var relPath = string.IsNullOrEmpty(normalizedPath) ? d.Name : normalizedPath + "/" + d.Name;
                list.Add(new AttachFileInfo
                {
                    name = d.Name,
                    path = relPath,
                    url = folderPrefix + relPath,
                    updateTime = d.LastWriteTime,
                    isDir = true,
                    type = "folder"
                });
            }

            foreach (var f in dir.GetFiles().OrderByDescending(x => x.LastWriteTime))
            {
                string ext = f.Extension.ToLowerInvariant();
                string type = "file";
                if (IsImage(ext)) type = "image";
                else if (IsPdf(ext)) type = "pdf";
                else if (IsVideo(ext)) type = "video";

                var relPath = string.IsNullOrEmpty(normalizedPath) ? f.Name : normalizedPath + "/" + f.Name;
                list.Add(new AttachFileInfo
                {
                    name = f.Name,
                    path = relPath,
                    url = folderPrefix + relPath,
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
            if (!IsSafeName(name) || name.Contains('.')) return Json(new { code = 1, message = "文件夹名称包含非法字符" });

            if (!TryResolveAttachPath(path, out var currentPath))
                return Json(new { code = 1, message = "非法路径" });

            string newDirPath = Path.Combine(currentPath, name);
            if (!TryResolveAttachPath(Path.GetRelativePath(GetAttachRoot(), newDirPath), out newDirPath, allowRoot: false))
                return Json(new { code = 1, message = "非法路径" });

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
                NewLife.Log.XTrace.WriteException(ex);
                return Json(new { code = 1, message = "创建失败，请使用 TraceId 联系管理员。", traceId = HttpContext.TraceIdentifier });
            }
        }
        #endregion

        #region 删除文件或文件夹
        [HttpPost]
        [MyAuthorize("del", "attach", "JSON")]
        public IActionResult DeleteFile(string path, bool isDir)
        {
            if (string.IsNullOrEmpty(path)) return Json(new { code = 1, message = "路径不能为空" });

            if (!TryResolveAttachPath(path, out var fullPath, allowRoot: false))
            {
                return Json(new { code = 1, message = "非法路径" });
            }

            try
            {
                if (isDir)
                {
                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true);
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
                NewLife.Log.XTrace.WriteException(ex);
                return Json(new { code = 1, message = "删除失败，请使用 TraceId 联系管理员。", traceId = HttpContext.TraceIdentifier });
            }
        }
        #endregion

        #region 重命名文件或文件夹
        [HttpPost]
        [MyAuthorize("edit", "attach", "JSON")]
        public IActionResult RenameFile(string path, string newName, bool isDir)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(newName))
                return Json(new { code = 1, message = "参数不能为空" });
            if (!IsSafeName(newName))
                return Json(new { code = 1, message = "新名称包含非法字符" });

            if (!TryResolveAttachPath(path, out var fullPath, allowRoot: false))
            {
                return Json(new { code = 1, message = "非法路径" });
            }

            try
            {
                string parentDir = Path.GetDirectoryName(fullPath);
                string newFullPath = Path.Combine(parentDir, newName);
                if (!TryResolveAttachPath(Path.GetRelativePath(GetAttachRoot(), newFullPath), out newFullPath, allowRoot: false))
                    return Json(new { code = 1, message = "非法路径" });

                if (isDir)
                {
                    if (newName.Contains('.')) return Json(new { code = 1, message = "文件夹名称不允许包含点号" });
                    if (!Directory.Exists(fullPath)) return Json(new { code = 1, message = "原文件夹不存在" });
                    if (Directory.Exists(newFullPath)) return Json(new { code = 1, message = "目标文件夹已存在" });
                    Directory.Move(fullPath, newFullPath);
                }
                else
                {
                    if (!System.IO.File.Exists(fullPath)) return Json(new { code = 1, message = "原文件不存在" });
                    if (!IsSafeRenameTarget(fullPath, newName))
                    {
                        return Json(new { code = 1, message = "禁止修改文件扩展名或重命名为非安全静态媒体格式" });
                    }
                    if (System.IO.File.Exists(newFullPath)) return Json(new { code = 1, message = "目标文件已存在" });
                    System.IO.File.Move(fullPath, newFullPath);
                }
                return Json(new { code = 0, message = "重命名成功" });
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                return Json(new { code = 1, message = "重命名失败，请使用 TraceId 联系管理员。", traceId = HttpContext.TraceIdentifier });
            }
        }
        #endregion

        #region 上传文件（支持安全图片 / PDF / 常见视频）
        [HttpPost]
        [RequestSizeLimit(52_428_800)]
        [MyAuthorize("add", "attach", "JSON")]
        public async Task<IActionResult> UploadFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Json(new { code = 1, message = "根目录不允许上传文件，请选择子目录" });
            }

            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
            {
                return Json(new { code = 1, message = "请选择文件" });
            }

            if (!TryResolveAttachPath(path, out var currentPath, allowRoot: false))
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
                string fileName = Path.GetFileName((file.FileName ?? string.Empty).Replace('\\', '/'));
                if (!IsSafeName(fileName) || fileName.StartsWith(".", StringComparison.Ordinal) || file.Length <= 0 || file.Length > 52_428_800)
                {
                    msg += $"文件 {fileName} 名称非法或大小超限; ";
                    continue;
                }

                string ext = Path.GetExtension(fileName).ToLowerInvariant();
                string allowedExts = _attachsetting.FileAllowedExtensions + "," + _attachsetting.ImageAllowedExtensions + "," + _attachsetting.MediaAllowedExtensions;
                if (!allowedExts.ToLowerInvariant().Split(',').Select(x => x.Trim()).Contains(ext.TrimStart('.')))
                {
                    msg += $"文件 {fileName} 类型不允许; ";
                    continue;
                }

                if (IsImage(ext))
                {
                    using var imageStream = file.OpenReadStream();
                    using var codec = SKCodec.Create(imageStream);
                    if (codec == null)
                    {
                        msg += $"文件 {fileName} 不是有效图片; ";
                        continue;
                    }
                }
                else if (IsPdf(ext))
                {
                    using var pdfStream = file.OpenReadStream();
                    var header = new byte[5];
                    var read = await pdfStream.ReadAsync(header.AsMemory(0, 5));
                    if (read < 5 || header[0] != (byte)'%' || header[1] != (byte)'P' || header[2] != (byte)'D' || header[3] != (byte)'F' || header[4] != (byte)'-')
                    {
                        msg += $"文件 {fileName} 不是有效的 PDF 文档; ";
                        continue;
                    }
                }
                else if (!UploadableVideoExtensions.Contains(ext))
                {
                    msg += $"文件 {fileName} 不支持直接放入公开附件目录（仅支持图片、PDF、MP4/WebM/MOV）; ";
                    continue;
                }

                string fullPath = Path.Combine(currentPath, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    string nameNoExt = Path.GetFileNameWithoutExtension(fileName);
                    fileName = $"{nameNoExt}_{Utils.GetOrderNum()}{ext}";
                    fullPath = Path.Combine(currentPath, fileName);
                }

                try
                {
                    await using (var stream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true))
                    {
                        await file.CopyToAsync(stream);
                    }
                    successCount++;
                }
                catch (Exception ex)
                {
                    NewLife.Log.XTrace.WriteException(ex);
                    msg += $"文件 {fileName} 上传失败; ";
                }
            }

            if (successCount > 0)
            {
                return Json(new { code = 0, message = $"成功上传 {successCount} 个文件。{msg}".Trim() });
            }
            return Json(new { code = 1, message = string.IsNullOrWhiteSpace(msg) ? "上传失败" : msg });
        }
        #endregion

        #region 辅助方法
        internal static bool IsSafeRenameTarget(string originalFullPath, string newName)
        {
            if (string.IsNullOrWhiteSpace(originalFullPath) || string.IsNullOrWhiteSpace(newName))
                return false;
            if (!SafeNameRegex.IsMatch(newName))
                return false;
            if (newName.StartsWith(".", StringComparison.Ordinal) ||
                newName.Contains("..", StringComparison.Ordinal) ||
                string.Equals(newName, "web.config", StringComparison.OrdinalIgnoreCase))
                return false;

            string oldExt = Path.GetExtension(originalFullPath).ToLowerInvariant();
            string newExt = Path.GetExtension(newName).ToLowerInvariant();
            return !string.IsNullOrEmpty(newExt) &&
                   string.Equals(oldExt, newExt, StringComparison.OrdinalIgnoreCase) &&
                   SafeRenameExtensions.Contains(newExt);
        }

        private static bool IsImage(string ext) => AllowedImageExtensions.Contains(ext ?? string.Empty);

        private static bool IsVideo(string ext) => VideoExtensions.Contains(ext ?? string.Empty);

        private static bool IsPdf(string ext) => string.Equals(ext, ".pdf", StringComparison.OrdinalIgnoreCase);

        private static bool IsSafeName(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Contains("..", StringComparison.Ordinal)) return false;
            return SafeNameRegex.IsMatch(str);
        }

        private static string NormalizeRelativePath(string path) =>
            (path ?? string.Empty).Replace('\\', '/').Trim('/');

        private string GetAttachFolderName()
        {
            var folder = string.IsNullOrWhiteSpace(attach?.AttachPatch) ? "userfiles" : attach.AttachPatch.Trim().Trim('/', '\\');
            return SystemController.IsSafeAttachPatch(folder) ? folder : "userfiles";
        }

        private string GetAttachRoot()
        {
            var webRoot = Path.GetFullPath(_env.WebRootPath);
            var folder = GetAttachFolderName();
            var candidate = Path.GetFullPath(Path.Combine(webRoot, folder));
            var relative = Path.GetRelativePath(webRoot, candidate);
            if (Path.IsPathRooted(relative) || relative == ".." || relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            {
                return Path.GetFullPath(Path.Combine(webRoot, "userfiles"));
            }
            return candidate;
        }

        private bool TryResolveAttachPath(string relativePath, out string fullPath, bool allowRoot = true)
        {
            fullPath = null;
            try
            {
                var root = GetAttachRoot();
                var normalized = (relativePath ?? string.Empty).TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
                var candidate = Path.GetFullPath(Path.Combine(root, normalized));
                var relative = Path.GetRelativePath(root, candidate);
                if (Path.IsPathRooted(relative) || relative == ".." || relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                    return false;
                if (!allowRoot && string.Equals(candidate.TrimEnd(Path.DirectorySeparatorChar), root.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
                    return false;
                fullPath = candidate;
                return true;
            }
            catch
            {
                return false;
            }
        }

        [NonAction]
        private List<DirInfo> getAllAttach(string subdir)
        {
            var list = new List<DirInfo>();
            if (!TryResolveAttachPath(subdir, out var basePath))
                return list;

            if (!Directory.Exists(basePath)) return list;

            string[] arrDirs = Directory.GetDirectories(basePath);
            if (arrDirs != null && arrDirs.Length > 0)
            {
                Array.Sort(arrDirs, StringComparer.OrdinalIgnoreCase);
                foreach (var item in arrDirs)
                {
                    string dirName = Path.GetFileName(item);
                    if (dirName.Equals("_thumbs", StringComparison.OrdinalIgnoreCase)) continue;

                    string nextSubDir = string.IsNullOrEmpty(subdir) ? dirName : subdir.Trim('/') + "/" + dirName;
                    var tmpList = getAllAttach(nextSubDir);
                    list.Add(new DirInfo
                    {
                        name = dirName,
                        path = nextSubDir,
                        subList = tmpList
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
        public string url { get; set; }
        public string ext { get; set; }
        public long size { get; set; }
        public DateTime updateTime { get; set; }
        public bool isDir { get; set; }
        public string type { get; set; }
    }
}
