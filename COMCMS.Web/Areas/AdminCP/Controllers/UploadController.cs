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
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using NewLife.Log;
using Newtonsoft.Json;
using SkiaSharp;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    /// <summary>
    /// 后台上传文件帮助类
    /// </summary>
    [RequestSizeLimit(54_525_952)]
    [RequestFormLimits(MultipartBodyLengthLimit = 54_525_952)]
    public class UploadController : AdminBaseController
    {
        private static readonly HashSet<string> AllowedImageContentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpg", "image/png", "image/x-png", "image/gif", "image/bmp", "image/jpeg", "image/webp"
        };
        private static readonly Regex SafeBaseNameRegex = new(@"[^a-zA-Z0-9_\-\u4e00-\u9fa5]", RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));
        private static readonly (string EventKey, string MenuKey)[] UploadPermittedCapabilities =
        {
            ("add", "attach"),
            ("add", "article"),
            ("edit", "article"),
            ("add", "articlecategory"),
            ("edit", "articlecategory"),
            ("add", "product"),
            ("edit", "product"),
            ("add", "category"),
            ("edit", "category"),
            ("add", "ads"),
            ("edit", "ads"),
            ("add", "link"),
            ("edit", "link"),
            ("edit", "baseconfig"),
            ("edit", "attachconfig"),
            ("add", "department"),
            ("edit", "department"),
            ("edit", "wxautoreply"),
            ("add", "wxkeywordreply"),
            ("edit", "wxkeywordreply"),
            ("add", "clickreplyrule"),
            ("edit", "clickreplyrule")
        };

        private readonly SystemSetting _attachsetting;
        private readonly IWebHostEnvironment _env;
        private readonly AttachConfigEntity attach;
        private readonly PrivateFileStorage _privateFiles;
        private readonly PublicMediaStorage _publicMedia;

        public UploadController(
            IWebHostEnvironment env,
            IOptions<SystemSetting> attachsetting,
            PrivateFileStorage privateFiles,
            PublicMediaStorage publicMedia)
        {
            attach = Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _attachsetting = attachsetting.Value;
            _privateFiles = privateFiles;
            _publicMedia = publicMedia;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.Result != null) return;

            if (!HasUploadPermission())
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Result = new JsonResult(new { code = 403, status = 0, msg = "您没有上传文件权限！", message = "您没有上传文件权限！" });
                return;
            }

            if (!Request.HasFormContentType) return;
            foreach (var file in Request.Form.Files)
            {
                var fileName = Path.GetFileName((file.FileName ?? string.Empty).Replace('\\', '/'));
                if (file.Length <= 0 || file.Length > 52_428_800 || string.IsNullOrWhiteSpace(fileName))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
                    context.Result = new JsonResult(new { code = 413, status = 0, msg = "上传文件为空或超过 50 MiB。", message = "上传文件为空或超过 50 MiB。" });
                    return;
                }
                if (Utils.IsImgFilename(fileName))
                {
                    using var stream = file.OpenReadStream();
                    using var codec = SKCodec.Create(stream);
                    if (codec == null)
                    {
                        context.Result = new BadRequestObjectResult(new { code = 400, status = 0, msg = "上传内容不是有效图片。", message = "上传内容不是有效图片。" });
                        return;
                    }
                }
            }
        }

        #region Jodit 统一上传接口（智能路由图片 / 视频 / 附件）
        [HttpPost]
        public async Task<IActionResult> JoditUpload()
        {
            var upload = Request.Form.Files.FirstOrDefault();
            if (upload == null) return JoditError("请选择要上传的文件！");

            var safeOriginalName = Path.GetFileName((upload.FileName ?? string.Empty).Replace('\\', '/'));
            var ext = Utils.GetFileExtName(safeOriginalName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext))
                return JoditError("错误的文件类型！");

            if (Utils.IsImgFilename(safeOriginalName) && AllowedImageContentTypes.Contains(upload.ContentType ?? string.Empty))
                return await JoditUploadImage();

            if (ext is ".mp4" or ".webm" or ".ogv" or ".ogg" or ".mov")
                return await JoditUploadVideo();

            return await JoditUploadFile();
        }
        #endregion

        #region Jodit 上传图片
        [HttpPost]
        public async Task<IActionResult> JoditUploadImage()
        {
            var upload = Request.Form.Files.FirstOrDefault();
            if (upload == null) return JoditError("请选择一张图片！");

            var safeOriginalName = Path.GetFileName((upload.FileName ?? string.Empty).Replace('\\', '/'));
            if (!AllowedImageContentTypes.Contains(upload.ContentType ?? string.Empty) || !Utils.IsImgFilename(safeOriginalName))
            {
                return JoditError("请选择一张图片！");
            }

            if (!TrySavePublicImage(upload, safeOriginalName, "images", applyWatermark: true, out var savedUrl, out _, out _, out _))
            {
                return JoditError("图片上传失败，请使用 TraceId 联系管理员。");
            }

            return await Task.FromResult<IActionResult>(JoditSuccess(savedUrl, true));
        }
        #endregion

        #region Jodit 上传文件
        [HttpPost]
        public async Task<IActionResult> JoditUploadFile()
        {
            var upload = Request.Form.Files.FirstOrDefault();
            if (upload == null) return JoditError("请选择一个文件！");

            var safeOriginalName = Path.GetFileName((upload.FileName ?? string.Empty).Replace('\\', '/'));
            string sFullExtension = Utils.GetFileExtName(safeOriginalName);
            if (string.IsNullOrEmpty(sFullExtension))
            {
                return JoditError("错误的文件类型！");
            }

            if (!IsAllowedFileExtension(sFullExtension))
            {
                return JoditError($"{sFullExtension} 的文件类型不允许上传！");
            }

            bool isImage = Utils.IsImgFilename(safeOriginalName) && AllowedImageContentTypes.Contains(upload.ContentType ?? string.Empty);
            if (!isImage)
            {
                try
                {
                    var stored = await _privateFiles.SaveAsync(upload, sFullExtension);
                    return JoditSuccess(PrivateFileStorage.BuildUrl(stored), false);
                }
                catch (InvalidDataException)
                {
                    return JoditError("该文件包含不允许的主动或可执行内容。");
                }
            }

            if (!TrySavePublicImage(upload, safeOriginalName, "files", applyWatermark: true, out var savedUrl, out _, out _, out _))
            {
                return JoditError("文件上传失败，请使用 TraceId 联系管理员。");
            }
            return JoditSuccess(savedUrl, true);
        }
        #endregion

        #region Jodit 上传视频
        [HttpPost]
        public async Task<IActionResult> JoditUploadVideo()
        {
            var upload = Request.Form.Files.FirstOrDefault();
            if (upload == null) return JoditError("请选择一个视频！");

            try
            {
                var stored = await _publicMedia.SaveVideoAsync(upload);
                return JoditSuccess(PublicMediaStorage.BuildUrl(stored), false);
            }
            catch (InvalidDataException ex)
            {
                return JoditError(ex.Message);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return JoditError("视频上传失败，请使用 TraceId 联系管理员。");
            }
        }
        #endregion

        #region 后台通用上传接口（图片 / 附件 / 视频）
        [HttpPost]
        public Task<IActionResult> UploadImage()
        {
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";

            var data = Request.Form.Files.FirstOrDefault();
            if (data != null)
            {
                var safeOriginalName = Path.GetFileName((data.FileName ?? string.Empty).Replace('\\', '/'));
                if (!AllowedImageContentTypes.Contains(data.ContentType ?? string.Empty) || !Utils.IsImgFilename(safeOriginalName))
                {
                    msg = "只能上传有效的图片格式文件！";
                    return Task.FromResult<IActionResult>(Json(new { status, msg, name, path, thumb, size, ext }));
                }

                string notwater = Request.Query["notwater"];
                bool applyWatermark = !string.Equals(notwater, "1", StringComparison.Ordinal);

                if (TrySavePublicImage(data, safeOriginalName, "images", applyWatermark, out var savedUrl, out var savedThumbUrl, out var savedName, out var savedExt))
                {
                    status = 1;
                    name = savedName;
                    path = savedUrl;
                    thumb = savedThumbUrl;
                    size = data.Length.ToString();
                    ext = savedExt;
                    msg = "上传成功!";
                }
                else
                {
                    msg = "图片上传失败，请使用 TraceId 联系管理员。";
                }
            }
            else
            {
                msg = "请上传一张图片";
            }

            return Task.FromResult<IActionResult>(Json(new { status, msg, name, path, thumb, size, ext }));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";
            var data = Request.Form.Files.FirstOrDefault();
            if (data != null)
            {
                var safeOriginalName = Path.GetFileName((data.FileName ?? string.Empty).Replace('\\', '/'));
                string sFullExtension = Utils.GetFileExtName(safeOriginalName);
                if (!IsAllowedFileExtension(sFullExtension))
                {
                    msg = $"{sFullExtension} 的文件类型不允许上传！";
                    return Json(new { status, msg, name, path, thumb, size, ext });
                }

                try
                {
                    var stored = await _privateFiles.SaveAsync(data, sFullExtension);
                    status = 1;
                    name = stored.OriginalName;
                    path = PrivateFileStorage.BuildUrl(stored);
                    size = data.Length.ToString();
                    ext = sFullExtension;
                    msg = "上传成功!";
                }
                catch (InvalidDataException)
                {
                    msg = "该文件包含不允许的主动或可执行内容。";
                }
                catch (Exception ex)
                {
                    XTrace.WriteException(ex);
                    msg = "文件上传失败，请使用 TraceId 联系管理员。";
                }
            }
            else
            {
                msg = "请上传一个文件";
            }

            return Json(new { status, msg, name, path, thumb, size, ext });
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo()
        {
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";
            var data = Request.Form.Files.FirstOrDefault();
            if (data != null)
            {
                try
                {
                    var stored = await _publicMedia.SaveVideoAsync(data);
                    status = 1;
                    name = stored.OriginalName;
                    path = PublicMediaStorage.BuildUrl(stored);
                    size = data.Length.ToString();
                    ext = Path.GetExtension(stored.OriginalName ?? string.Empty);
                    msg = "上传成功!";
                }
                catch (InvalidDataException ex)
                {
                    msg = ex.Message;
                }
                catch (Exception ex)
                {
                    XTrace.WriteException(ex);
                    msg = "视频上传失败，请使用 TraceId 联系管理员。";
                }
            }
            else
            {
                msg = "请选择一个视频文件";
            }

            return Json(new { status, msg, name, path, thumb, size, ext });
        }

        [HttpPost]
        public Task<IActionResult> DoWebuploaderImage() => UploadImage();

        [HttpPost]
        public Task<IActionResult> DoWebuploaderFile() => UploadFile();
        #endregion

        #region 辅助方法
        private bool HasUploadPermission()
        {
            Admin my = Admin.GetMyInfo();
            if (my == null || my.IsLock == 1) return false;
            if (my.Roles?.IsSuperAdmin == 1) return true;
            if (string.IsNullOrWhiteSpace(my.Roles?.Powers)) return false;

            try
            {
                var permissions = JsonConvert.DeserializeObject<IList<AdminMenuEvent>>(my.Roles.Powers);
                if (permissions == null || permissions.Count == 0) return false;
                return UploadPermittedCapabilities.Any(cap =>
                    permissions.Any(p =>
                        string.Equals(p.MenuKey, cap.MenuKey, StringComparison.Ordinal) &&
                        string.Equals(p.EventKey, cap.EventKey, StringComparison.Ordinal)));
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private bool IsAllowedFileExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension)) return false;
            var normalizedExt = extension.ToLowerInvariant().TrimStart('.');
            var allowed = (_attachsetting?.FileAllowedExtensions ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLowerInvariant());
            return allowed.Contains(normalizedExt);
        }

        internal static string BuildSafeImageFileName(string originalFileName, int randomMode, string targetDirectory)
        {
            var safeFileName = Path.GetFileName((originalFileName ?? string.Empty).Replace('\\', '/'));
            var ext = Path.GetExtension(safeFileName).ToLowerInvariant();
            var rawBase = Path.GetFileNameWithoutExtension(safeFileName);
            var cleanBase = SafeBaseNameRegex.Replace(rawBase, "_").Trim('.', '_');
            if (string.IsNullOrWhiteSpace(cleanBase)) cleanBase = Utils.GetShortGUId();
            if (cleanBase.Length > 64) cleanBase = cleanBase[..64];

            switch (randomMode)
            {
                case 0:
                {
                    var candidate = cleanBase + ext;
                    if (System.IO.File.Exists(Path.Combine(targetDirectory, candidate)))
                    {
                        candidate = $"{cleanBase}_{Utils.GetShortGUId()}{ext}";
                    }
                    return candidate;
                }
                case 1:
                    return Utils.GetShortGUId() + ext;
                default:
                    return Utils.GetOrderNum() + "_" + Utils.GetShortGUId() + ext;
            }
        }

        private bool TrySavePublicImage(
            IFormFile data,
            string safeOriginalName,
            string categoryFolder,
            bool applyWatermark,
            out string publicUrl,
            out string thumbUrl,
            out string savedName,
            out string savedExt)
        {
            publicUrl = "";
            thumbUrl = "";
            savedName = "";
            savedExt = "";

            try
            {
                var attachFolder = SystemController.IsSafeAttachPatch(attach?.AttachPatch) ? attach.AttachPatch.Trim() : "userfiles";
                var webRoot = Path.GetFullPath(_env.WebRootPath);

                string dateSubPath = attach.SaveType switch
                {
                    1 => Path.Combine(DateTime.Now.Year.ToString(), DateTime.Now.ToString("MM")),
                    2 => Path.Combine(DateTime.Now.Year.ToString(), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd")),
                    _ => DateTime.Now.Year.ToString()
                };

                string filepath = Path.GetFullPath(Path.Combine(webRoot, attachFolder, categoryFolder, dateSubPath));
                string thumbsFilePath = Path.GetFullPath(Path.Combine(webRoot, attachFolder, "_thumbs", categoryFolder, dateSubPath));

                if (!IsWithinRoot(webRoot, filepath) || !IsWithinRoot(webRoot, thumbsFilePath))
                    return false;

                Directory.CreateDirectory(filepath);
                Directory.CreateDirectory(thumbsFilePath);

                string imgname = BuildSafeImageFileName(safeOriginalName, attach.IsRandomFileName, filepath);
                string sFullExtension = Path.GetExtension(imgname).ToLowerInvariant();
                string fullpath = Path.GetFullPath(Path.Combine(filepath, imgname));
                string fullThumbPath = Path.GetFullPath(Path.Combine(thumbsFilePath, imgname));

                if (!IsWithinRoot(filepath, fullpath) || !IsWithinRoot(thumbsFilePath, fullThumbPath))
                    return false;

                using (var fs = new FileStream(fullpath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, false))
                {
                    data.CopyTo(fs);
                }

                if (attach.IsCreateThum == 1 && attach.ThumMaxWidth > 0 && attach.ThumMaxHeight > 0)
                {
                    ThumbnailHelper.MakeThumbnailImage(fullpath, fullThumbPath, attach.ThumMaxWidth, attach.ThumMaxHeight, ThumbnailHelper.CutMode.Cut);
                }

                if (applyWatermark && attach.IsWaterMark == 1 && !string.IsNullOrEmpty(attach.WaterMarkImg))
                {
                    var wmNormalized = attach.WaterMarkImg.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
                    var watermarkimg = Path.GetFullPath(Path.Combine(webRoot, wmNormalized));
                    if (IsWithinRoot(webRoot, watermarkimg) && System.IO.File.Exists(watermarkimg))
                    {
                        string copyfullpath = Path.Combine(filepath, Path.GetFileNameWithoutExtension(imgname) + "_copy" + sFullExtension);
                        try
                        {
                            System.IO.File.Copy(fullpath, copyfullpath, overwrite: true);
                            using var waterpic = SKBitmap.Decode(watermarkimg);
                            using var srcPic = SKBitmap.Decode(copyfullpath);
                            if (waterpic != null && srcPic != null && waterpic.Width < srcPic.Width && waterpic.Height < srcPic.Height)
                            {
                                WatermarkHelper.AddImageSignPic(copyfullpath, fullpath, watermarkimg, attach.WaterMarkPlace, attach.WaterMarkQty, attach.WaterMarkDiaphaneity);
                            }
                        }
                        catch (Exception wmEx)
                        {
                            XTrace.WriteException(wmEx);
                        }
                        finally
                        {
                            if (System.IO.File.Exists(copyfullpath))
                            {
                                System.IO.File.Delete(copyfullpath);
                            }
                        }
                    }
                }

                var urlDatePath = dateSubPath.Replace('\\', '/');
                publicUrl = $"/{attachFolder}/{categoryFolder}/{urlDatePath}/{imgname}";
                thumbUrl = $"/{attachFolder}/_thumbs/{categoryFolder}/{urlDatePath}/{imgname}";
                savedName = imgname;
                savedExt = sFullExtension;
                return true;
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return false;
            }
        }

        private static bool IsWithinRoot(string root, string candidate)
        {
            var relative = Path.GetRelativePath(Path.GetFullPath(root), Path.GetFullPath(candidate));
            return !Path.IsPathRooted(relative) && relative != ".." && !relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal);
        }

        private JsonResult JoditSuccess(string url, bool isImage) => Json(new
        {
            success = true,
            data = new
            {
                files = new[] { url },
                isImages = new[] { isImage },
                path = string.Empty,
                baseurl = string.Empty,
                messages = Array.Empty<string>()
            }
        });

        private JsonResult JoditError(string message) => Json(new
        {
            success = false,
            data = new
            {
                files = Array.Empty<string>(),
                isImages = Array.Empty<bool>(),
                path = string.Empty,
                baseurl = string.Empty,
                messages = new[] { message }
            }
        });
        #endregion
    }
}
