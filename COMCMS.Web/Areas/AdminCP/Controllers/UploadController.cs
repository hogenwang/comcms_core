using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using COMCMS.Web.Models;
using Newtonsoft.Json;
using System.IO;
using COMCMS.Core.Models;
using System.DrawingCore;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NewLife.UserGroup.WebUploader;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    #region CKEditor错误返回实体类
    /// <summary>
    /// CKEditor错误返回实体类
    /// </summary>
    public class CKFileUploadError
    {
        public CKFileUploadErrorMessage error { get; set; } = new CKFileUploadErrorMessage();
    }
    public class CKFileUploadErrorMessage
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int number { get; set; } = 100;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }
    }
    #endregion
    /// <summary>
    /// 后台上传文件帮助类
    /// </summary>
    public class UploadController : AdminBaseController
    {
        private readonly SystemSetting _attachsetting;
        private IWebHostEnvironment _env;
        private AttachConfigEntity attach;
        public UploadController(IWebHostEnvironment env, IOptions<SystemSetting> attachsetting)
        {
            attach = Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _attachsetting = attachsetting.Value;
        }

        #region CKEditor 上传图片
        /// <summary>
        /// ckeditor 上传图片
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CKUploadImage()
        {

            string callback = Request.Query["CKEditorFuncNum"];//要求返回值
            var upload = Request.Form.Files[0];
            //string tpl = "<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");</script>";
            CKFileUploadError errorJson = new CKFileUploadError();
            if (upload == null)
            {

                errorJson.error.message = "请选择一张图片!";
                return Json(errorJson);
            }
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/jpg", "image/png", "image/x-png", "image/gif", "image/bmp", "image/jpeg" };
            if (imgtypelist.FindIndex(x => x == upload.ContentType) == -1)
            {
                errorJson.error.message = "请选择一张图片!";
                return Json(errorJson);
                //return Content(string.Format(tpl, "", callback, "请上传一张图片！"), "text/html");
            }

            var data = Request.Form.Files["upload"];
            string filepath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";
            string thumbsFilePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}_thumbs{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";

            //根据附件配置，设置上传图片目录
            string imgPath = DateTime.Now.Year.ToString();//默认按年
            switch (attach.SaveType)
            {
                case 1://按月份
                    imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}";
                    break;
                case 2:
                    imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("dd")}";
                    break;
            }
            filepath += imgPath;//存放路径
            thumbsFilePath += imgPath;//缩略图路径
            string sFileNameNoExt = Utils.GetFileNameWithoutExtension(upload.FileName);//文件名字，不带扩展名
            string sFullExtension = Utils.GetFileExtName(upload.FileName);//扩展名
            //图片名字
            string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upload.FileName);

            switch (attach.IsRandomFileName)
            {
                case 0://不随机
                    imgname = upload.FileName;
                    //判断是否存在
                    if (System.IO.File.Exists(Path.Combine(filepath, imgname)))
                    {
                        imgname = sFileNameNoExt + "(1)" + sFullExtension;
                    }
                    break;
                case 1://随机字符串
                    imgname = Utils.GetShortGUId() + sFullExtension;
                    break;
                case 2://时间
                    imgname = Utils.GetOrderNum() + sFullExtension;
                    break;
            }
            string fullpath = Path.Combine(filepath, imgname);//图片
            string fullThumbPath = Path.Combine(thumbsFilePath, imgname);//缩略图
            try
            {
                //判断路径
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                //缩略图路径
                if (!Directory.Exists(thumbsFilePath))
                    Directory.CreateDirectory(thumbsFilePath);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                        {
                            data.CopyTo(fs);
                        }
                    });
                    //生成缩略图
                    if (attach.IsCreateThum == 1)
                    {
                        ThumbnailHelper.MakeThumbnailImage(fullpath, fullThumbPath, attach.ThumMaxWidth, attach.ThumMaxHeight, ThumbnailHelper.CutMode.Cut);
                    }
                    //添加水印
                    if (attach.IsWaterMark == 1 && !string.IsNullOrEmpty(attach.WaterMarkImg))
                    {
                        string watermarkimg = _env.WebRootPath + attach.WaterMarkImg.Replace("/", Path.DirectorySeparatorChar.ToString());
                        if (System.IO.File.Exists(watermarkimg))
                        {
                            //先复制一张图片出来
                            string copyfullpath = fullpath.Replace(sFullExtension, "_copy" + sFullExtension);
                            System.IO.File.Copy(fullpath, copyfullpath);

                            Image waterpic = new Bitmap(watermarkimg);
                            Image srcPic = new Bitmap(copyfullpath);
                            if (waterpic.Width < srcPic.Width && waterpic.Height < srcPic.Height)
                            {
                                waterpic.Dispose();
                                //srcPic.Dispose();
                                try
                                {
                                    WatermarkHelper.AddImageSignPic(srcPic, fullpath, watermarkimg, attach.WaterMarkPlace, attach.WaterMarkQty, attach.WaterMarkDiaphaneity);
                                    srcPic.Dispose();
                                    System.IO.File.Delete(copyfullpath);
                                }
                                catch
                                {
                                    if (System.IO.File.Exists(copyfullpath))
                                    {
                                        System.IO.File.Delete(copyfullpath);
                                    }
                                }

                            }
                            else
                            {
                                waterpic.Dispose();
                                srcPic.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorJson.error.message = "图片上传失败，" + ex.Message + "!";
                return Json(errorJson);
                //return Content(string.Format(tpl, "", callback, "图片上传失败：" + ex.Message), "text/html");
            }
            dynamic successJson = new
            {
                fileName = imgname,
                uploaded = 1,
                url = $"/{attach.AttachPatch}/images/{imgPath.Replace("\\", "/")}/" + imgname
            };
            return Json(successJson);
            //{"fileName":"20180413145904.png","uploaded":1,"url":"\/userfiles\/files\/Public%20Folder\/20180413145904.png"}
            //return Content(string.Format(tpl, $"/{attach.AttachPatch}/images/{imgPath.Replace("\\", "/")}/" + imgname, callback, ""), "text/html");
        }
        #endregion

        #region CKEditor 上传文件
        public async Task<IActionResult> CKUploadFile()
        {
            string callback = Request.Query["CKEditorFuncNum"];//要求返回值
            var upload = Request.Form.Files[0];
            //string tpl = "<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");</script>";
            CKFileUploadError errorJson = new CKFileUploadError();
            if (upload == null)
            {
                errorJson.error.message = "请选择一个文件！";
                return Json(errorJson);
            }
            //return Content(string.Format(tpl, "", callback, "请选择一个文件！"), "text/html");
            string sFileNameNoExt = Utils.GetFileNameWithoutExtension(upload.FileName);//文件名字，不带扩展名
            string sFullExtension = Utils.GetFileExtName(upload.FileName);//扩展名
            if (string.IsNullOrEmpty(sFullExtension))
            {
                errorJson.error.message = "错误的文件类型！";
                return Json(errorJson);
                //return Content(string.Format(tpl, "", callback, $"错误的文件类型！"), "text/html");
            }
            //判断是否是允许文件扩展名
            string sAllowedExtensions = _attachsetting.FileAllowedExtensions;
            List<string> listAllowedExtensions = new List<string>();
            string[] arrAllowedExtensions = sAllowedExtensions.Split(new string[] { "," }, StringSplitOptions.None);
            if (arrAllowedExtensions != null && arrAllowedExtensions.Length > 0)
            {
                foreach (var s in arrAllowedExtensions)
                {
                    listAllowedExtensions.Add(s);
                }
            }
            if (listAllowedExtensions.Find(x => x == sFullExtension.ToLower().Replace(".", "")) == null)
            {
                errorJson.error.message = $"{sFullExtension}的文件类型，不允许上传！";
                return Json(errorJson);
                //return Content(string.Format(tpl, "", callback, $"{sFullExtension}的文件类型，不允许上传！"), "text/html");
            }
            //判断是否是图片，如果是图片，后面需要生成缩略图并可能的话，就加上水印
            bool isImage = false;
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/jpg", "image/png", "image/x-png", "image/gif", "image/bmp", "image/jpeg" };
            if (imgtypelist.FindIndex(x => x == upload.ContentType) >= 0)
            {
                isImage = true;
            }
            var data = Request.Form.Files["upload"];
            string filepath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}files{Path.DirectorySeparatorChar}";
            string thumbsFilePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}_thumbs{Path.DirectorySeparatorChar}files{Path.DirectorySeparatorChar}";
            //根据附件配置，设置上传图片目录
            string imgPath = DateTime.Now.Year.ToString();//默认按年
            switch (attach.SaveType)
            {
                case 1://按月份
                    imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}";
                    break;
                case 2:
                    imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("dd")}";
                    break;
            }
            filepath += imgPath;//存放路径
            thumbsFilePath += imgPath;//缩略图路径
            //文件名字
            string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upload.FileName);
            switch (attach.IsRandomFileName)
            {
                case 0://不随机
                    imgname = upload.FileName;
                    //判断是否存在
                    if (System.IO.File.Exists(Path.Combine(filepath, imgname)))
                    {
                        imgname = sFileNameNoExt + "(1)" + sFullExtension;
                    }
                    break;
                case 1://随机字符串
                    imgname = Utils.GetShortGUId() + sFullExtension;
                    break;
                case 2://时间
                    imgname = Utils.GetOrderNum() + sFullExtension;
                    break;
            }
            string fullpath = Path.Combine(filepath, imgname);//图片
            string fullThumbPath = Path.Combine(thumbsFilePath, imgname);//缩略图，可能的话
            try
            {
                //判断路径
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                //缩略图路径
                if (isImage && !Directory.Exists(thumbsFilePath))
                    Directory.CreateDirectory(thumbsFilePath);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                        {
                            data.CopyTo(fs);
                        }
                    });
                    //生成缩略图
                    if (isImage && attach.IsCreateThum == 1)
                    {
                        ThumbnailHelper.MakeThumbnailImage(fullpath, fullThumbPath, attach.ThumMaxWidth, attach.ThumMaxHeight, ThumbnailHelper.CutMode.Cut);
                    }
                    //添加水印
                    if (isImage && attach.IsWaterMark == 1 && !string.IsNullOrEmpty(attach.WaterMarkImg))
                    {
                        string watermarkimg = _env.WebRootPath + attach.WaterMarkImg.Replace("/", Path.DirectorySeparatorChar.ToString());
                        if (System.IO.File.Exists(watermarkimg))
                        {
                            //先复制一张图片出来
                            string copyfullpath = fullpath.Replace(sFullExtension, "_copy" + sFullExtension);
                            System.IO.File.Copy(fullpath, copyfullpath);

                            Image waterpic = new Bitmap(watermarkimg);
                            Image srcPic = new Bitmap(copyfullpath);
                            if (waterpic.Width < srcPic.Width && waterpic.Height < srcPic.Height)
                            {
                                waterpic.Dispose();
                                //srcPic.Dispose();
                                try
                                {
                                    WatermarkHelper.AddImageSignPic(srcPic, fullpath, watermarkimg, attach.WaterMarkPlace, attach.WaterMarkQty, attach.WaterMarkDiaphaneity);
                                    srcPic.Dispose();
                                    System.IO.File.Delete(copyfullpath);
                                }
                                catch
                                {
                                    if (System.IO.File.Exists(copyfullpath))
                                    {
                                        System.IO.File.Delete(copyfullpath);
                                    }
                                }

                            }
                            else
                            {
                                waterpic.Dispose();
                                srcPic.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorJson.error.message = "文件上传失败：" + ex.Message;
                return Json(errorJson);
                //return Content(string.Format(tpl, "", callback, "文件上传失败：" + ex.Message), "text/html");
            }
            dynamic successJson = new
            {
                fileName = imgname,
                uploaded = 1,
                url = $"/{attach.AttachPatch}/files/{imgPath.Replace("\\", "/")}/" + imgname
            };
            return Json(successJson);
            //return Content(string.Format(tpl, $"/{attach.AttachPatch}/fales/{imgPath.Replace("\\", "/")}/" + imgname, callback, ""), "text/html");

        }
        #endregion

        #region CKEditor 上传多媒体

        #endregion

        #region Webuploader 上传图片
        //public IActionResult WUUploadImage()
        //{
        //    string isbinary = Request.Query["isbinary"];
        //    switch (isbinary)
        //    {
        //        case "1"://二进制上传
        //            return DoWebuploaderImageByBinary();
        //        default://默认上传
        //            return DoWebuploaderImage();
        //    }
        //}

        #region 二进制上传图片
        /// <summary>
        /// 二进制上传图片（未实现）
        /// </summary>
        /// <returns></returns>
        public IActionResult DoWebuploaderImageByBinary()
        {
            var fileinfo = "";
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";
            try
            {
                string upname = Request.Query["name"];
                //判断是否是允许文件扩展名
                string sAllowedExtensions = _attachsetting.ImageAllowedExtensions;
                List<string> listAllowedExtensions = new List<string>();
                string[] arrAllowedExtensions = sAllowedExtensions.Split(new string[] { "," }, StringSplitOptions.None);

                string sFileNameNoExt = Utils.GetFileNameWithoutExtension(upname);//文件名字，不带扩展名
                string sFullExtension = Utils.GetFileExtName(upname);//扩展名

                if (arrAllowedExtensions != null && arrAllowedExtensions.Length > 0)
                {
                    foreach (var s in arrAllowedExtensions)
                    {
                        listAllowedExtensions.Add(s);
                    }
                }
                if (listAllowedExtensions.Find(x => x == sFullExtension.ToLower().Replace(".", "")) == null)
                {
                    return Json(new { status = status, msg = $"{sFullExtension}的文件类型，不允许上传，请上传一张图片！", name = name, path = path, thumb = thumb, size = size, ext = ext });
                }
                var req = HttpContext.Request;
                var f = Request.Form.Files[0];

                //Request.EnableRewind();
                Image image = null;
                using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                {
                    var bytes = ConvertToBinary(reader.BaseStream);
                    image = ReturnPic(bytes);
                    size = bytes.Length.ToString();
                }

                string filepath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";
                string thumbsFilePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}_thumbs{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";
                //根据附件配置，设置上传图片目录
                string imgPath = DateTime.Now.Year.ToString();//默认按年
                switch (attach.SaveType)
                {
                    case 1://按月份
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}";
                        break;
                    case 2:
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("dd")}";
                        break;
                }
                filepath += imgPath;//存放路径
                thumbsFilePath += imgPath;//缩略图路径
                //图片名字
                string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upname);
                switch (attach.IsRandomFileName)
                {
                    case 0://不随机
                        imgname = upname;
                        //判断是否存在
                        if (System.IO.File.Exists(Path.Combine(filepath, imgname)))
                        {
                            imgname = sFileNameNoExt + "(1)" + sFullExtension;
                        }
                        break;
                    case 1://随机字符串
                        imgname = Utils.GetShortGUId() + sFullExtension;
                        break;
                    case 2://时间
                        imgname = Utils.GetOrderNum() + sFullExtension;
                        break;
                }
                string fullpath = Path.Combine(filepath, imgname);//图片
                string fullThumbPath = Path.Combine(thumbsFilePath, imgname);//缩略图

                //判断路径
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                //缩略图路径
                if (!Directory.Exists(thumbsFilePath))
                    Directory.CreateDirectory(thumbsFilePath);

                //保存图片
                image.Save(filepath);
                //生成缩略图
                if (attach.IsCreateThum == 1)
                {
                    ThumbnailHelper.MakeThumbnailImage(fullpath, fullThumbPath, attach.ThumMaxWidth, attach.ThumMaxHeight, ThumbnailHelper.CutMode.Cut);
                }
                //添加水印
                if (attach.IsWaterMark == 1 && !string.IsNullOrEmpty(attach.WaterMarkImg))
                {
                    string watermarkimg = _env.WebRootPath + attach.WaterMarkImg.Replace("/", Path.DirectorySeparatorChar.ToString());
                    if (System.IO.File.Exists(watermarkimg))
                    {
                        //先复制一张图片出来
                        string copyfullpath = fullpath.Replace(sFullExtension, "_copy" + sFullExtension);
                        System.IO.File.Copy(fullpath, copyfullpath);

                        Image waterpic = new Bitmap(watermarkimg);
                        Image srcPic = new Bitmap(copyfullpath);
                        if (waterpic.Width < srcPic.Width && waterpic.Height < srcPic.Height)
                        {
                            waterpic.Dispose();
                            //srcPic.Dispose();
                            try
                            {
                                WatermarkHelper.AddImageSignPic(srcPic, fullpath, watermarkimg, attach.WaterMarkPlace, attach.WaterMarkQty, attach.WaterMarkDiaphaneity);
                                srcPic.Dispose();
                                System.IO.File.Delete(copyfullpath);
                            }
                            catch
                            {
                                if (System.IO.File.Exists(copyfullpath))
                                {
                                    System.IO.File.Delete(copyfullpath);
                                }
                            }

                        }
                        else
                        {
                            waterpic.Dispose();
                            srcPic.Dispose();
                        }
                    }
                }
                image.Dispose();
                status = 1;
                name = imgname;
                path = $"/{attach.AttachPatch}/images/{imgPath.Replace("\\", "/")}/" + imgname;
                thumb = $"/{attach.AttachPatch}/_thumbs/images/{imgPath.Replace("\\", "/")}/" + imgname;
                ext = sFullExtension;
                fileinfo = $"/{attach.AttachPatch}/images/{imgPath.Replace("\\", "/")}/" + imgname;
                msg = "上传成功!";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return Json(new { status = status, msg = msg, name = name, path = path, thumb = thumb, size = size, ext = ext });
        }
        #endregion

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DoWebuploaderImage()
        {
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";
            var data = Request.Form.Files[0];
            if (data != null)
            {
                //判断是否是图片类型
                List<string> imgtypelist = new List<string> { "image/jpg", "image/png", "image/x-png", "image/gif", "image/bmp", "image/jpeg" };
                if (imgtypelist.FindIndex(x => x == data.ContentType) == -1)
                {
                    msg = "只能上传一张图片格式文件！";
                    return Content(JsonConvert.SerializeObject(new { status = status, msg = msg, name = name, path = path, thumb = thumb, size = size, ext = ext }), "text/plain");
                }
                string filepath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";
                string thumbsFilePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}_thumbs{Path.DirectorySeparatorChar}images{Path.DirectorySeparatorChar}";
                //根据附件配置，设置上传图片目录
                string imgPath = DateTime.Now.Year.ToString();//默认按年
                switch (attach.SaveType)
                {
                    case 1://按月份
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}";
                        break;
                    case 2:
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("dd")}";
                        break;
                }
                filepath += imgPath;//存放路径
                thumbsFilePath += imgPath;//缩略图路径
                string sFileNameNoExt = Utils.GetFileNameWithoutExtension(data.FileName);//文件名字，不带扩展名
                string sFullExtension = Utils.GetFileExtName(data.FileName);//扩展名
                //图片名字
                string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(data.FileName);

                switch (attach.IsRandomFileName)
                {
                    case 0://不随机
                        imgname = data.FileName;
                        //判断是否存在
                        if (System.IO.File.Exists(Path.Combine(filepath, imgname)))
                        {
                            imgname = sFileNameNoExt + "(1)" + sFullExtension;
                        }
                        break;
                    case 1://随机字符串
                        imgname = Utils.GetShortGUId() + sFullExtension;
                        break;
                    case 2://时间
                        imgname = Utils.GetOrderNum() + sFullExtension;
                        break;
                }
                string fullpath = Path.Combine(filepath, imgname);//图片
                string fullThumbPath = Path.Combine(thumbsFilePath, imgname);//缩略图
                try
                {
                    //判断路径
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);
                    //缩略图路径
                    if (!Directory.Exists(thumbsFilePath))
                        Directory.CreateDirectory(thumbsFilePath);
                    if (data != null)
                    {
                        await Task.Run(() =>
                        {
                            using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                            {
                                data.CopyTo(fs);
                            }
                        });
                        //生成缩略图
                        if (attach.IsCreateThum == 1)
                        {
                            ThumbnailHelper.MakeThumbnailImage(fullpath, fullThumbPath, attach.ThumMaxWidth, attach.ThumMaxHeight, ThumbnailHelper.CutMode.Cut);
                        }
                        string notwater = Request.Query["notwater"];//是否强制不加水印
                        if (string.IsNullOrEmpty(notwater) || notwater != "1") notwater = "0";
                        //添加水印
                        if (notwater == "0" && attach.IsWaterMark == 1 && !string.IsNullOrEmpty(attach.WaterMarkImg))
                        {
                            string watermarkimg = _env.WebRootPath + attach.WaterMarkImg.Replace("/", Path.DirectorySeparatorChar.ToString());
                            if (System.IO.File.Exists(watermarkimg))
                            {
                                //先复制一张图片出来
                                string copyfullpath = fullpath.Replace(sFullExtension, "_copy" + sFullExtension);
                                System.IO.File.Copy(fullpath, copyfullpath);

                                Image waterpic = new Bitmap(watermarkimg);
                                Image srcPic = new Bitmap(copyfullpath);
                                if (waterpic.Width < srcPic.Width && waterpic.Height < srcPic.Height)
                                {
                                    waterpic.Dispose();
                                    //srcPic.Dispose();
                                    try
                                    {
                                        WatermarkHelper.AddImageSignPic(srcPic, fullpath, watermarkimg, attach.WaterMarkPlace, attach.WaterMarkQty, attach.WaterMarkDiaphaneity);
                                        srcPic.Dispose();
                                        System.IO.File.Delete(copyfullpath);
                                    }
                                    catch
                                    {
                                        if (System.IO.File.Exists(copyfullpath))
                                        {
                                            System.IO.File.Delete(copyfullpath);
                                        }
                                    }

                                }
                                else
                                {
                                    waterpic.Dispose();
                                    srcPic.Dispose();
                                }
                            }
                        }
                    }
                    status = 1;
                    name = imgname;
                    path = $"/{attach.AttachPatch}/images/{imgPath.Replace("\\", "/")}/" + imgname;
                    thumb = $"/{attach.AttachPatch}/_thumbs/images/{imgPath.Replace("\\", "/")}/" + imgname;
                    ext = sFullExtension;
                    msg = "上传成功!";
                }
                catch (Exception ex)
                {
                    msg = "图片上传失败：" + ex.Message;
                }
            }
            else
            {
                msg = "请上传一张图片";
            }

            return Content(JsonConvert.SerializeObject(new { status = status, msg = msg, name = name, path = path, thumb = thumb, size = size, ext = ext }), "text/plain");
        }


        #endregion

        #region Webuploader 上传文件
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DoWebuploaderFile()
        {
            var msg = "上传失败!";
            var status = 0;
            var name = "";
            var path = "";
            var thumb = "";
            var size = "";
            var ext = "";
            var data = Request.Form.Files[0];
            if (data != null)
            {
                string filepath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}files{Path.DirectorySeparatorChar}";
                string thumbsFilePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{attach.AttachPatch}{Path.DirectorySeparatorChar}_thumbs{Path.DirectorySeparatorChar}files{Path.DirectorySeparatorChar}";
                //根据附件配置，设置上传图片目录
                string imgPath = DateTime.Now.Year.ToString();//默认按年
                switch (attach.SaveType)
                {
                    case 1://按月份
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}";
                        break;
                    case 2:
                        imgPath = $"{DateTime.Now.Year.ToString()}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("MM")}{Path.DirectorySeparatorChar}{DateTime.Now.ToString("dd")}";
                        break;
                }
                filepath += imgPath;//存放路径
                thumbsFilePath += imgPath;//缩略图路径
                string sFileNameNoExt = Utils.GetFileNameWithoutExtension(data.FileName);//文件名字，不带扩展名
                string sFullExtension = Utils.GetFileExtName(data.FileName);//扩展名

                //判断是否是允许文件扩展名
                string sAllowedExtensions = _attachsetting.FileAllowedExtensions;
                List<string> listAllowedExtensions = new List<string>();
                string[] arrAllowedExtensions = sAllowedExtensions.Split(new string[] { "," }, StringSplitOptions.None);
                if (arrAllowedExtensions != null && arrAllowedExtensions.Length > 0)
                {
                    foreach (var s in arrAllowedExtensions)
                    {
                        listAllowedExtensions.Add(s);
                    }
                }
                if (listAllowedExtensions.Find(x => x == sFullExtension.ToLower().Replace(".", "")) == null)
                {
                    msg = $"{sFullExtension}的文件类型，不允许上传！";
                    return Content(JsonConvert.SerializeObject(new { status = status, msg = msg, name = name, path = path, thumb = thumb, size = size, ext = ext }), "text/plain");
                }

                //图片名字
                string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(data.FileName);

                switch (attach.IsRandomFileName)
                {
                    case 0://不随机
                        imgname = data.FileName;
                        //判断是否存在
                        if (System.IO.File.Exists(Path.Combine(filepath, imgname)))
                        {
                            imgname = sFileNameNoExt + "(1)" + sFullExtension;
                        }
                        break;
                    case 1://随机字符串
                        imgname = Utils.GetShortGUId() + sFullExtension;
                        break;
                    case 2://时间
                        imgname = Utils.GetOrderNum() + sFullExtension;
                        break;
                }
                string fullpath = Path.Combine(filepath, imgname);//图片
                string fullThumbPath = Path.Combine(thumbsFilePath, imgname);//缩略图
                try
                {
                    //判断路径
                    if (!Directory.Exists(filepath))
                        Directory.CreateDirectory(filepath);
                    //缩略图路径
                    if (!Directory.Exists(thumbsFilePath))
                        Directory.CreateDirectory(thumbsFilePath);
                    if (data != null)
                    {
                        await Task.Run(() =>
                        {
                            using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                            {
                                data.CopyTo(fs);
                            }
                        });
                    }
                    status = 1;
                    name = imgname;
                    path = $"/{attach.AttachPatch}/files/{imgPath.Replace("\\", "/")}/" + imgname;
                    thumb = $"/{attach.AttachPatch}/_thumbs/files/{imgPath.Replace("\\", "/")}/" + imgname;
                    ext = sFullExtension;
                    msg = "上传成功!";
                }
                catch (Exception ex)
                {
                    msg = "图片上传失败：" + ex.Message;
                }
            }
            else
            {
                msg = "请上传一个文件";
            }

            return Content(JsonConvert.SerializeObject(new { status = status, msg = msg, name = name, path = path, thumb = thumb, size = size, ext = ext }), "text/plain");
        }

        #region 分片上传文件，可断点续传

        /// <summary>
        /// 保存文件或者分块
        /// </summary>
        /// <param name="md5">文件md5</param>
        /// <param name="chunk">分块号</param>
        /// <param name="chunks">分块总数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveFile(string md5, int? chunk, int? chunks)
        {
            var tempDir = "UploadTemp"; // 缓存文件夹
            var targetDir = "UploadFile"; // 目标文件夹

            var file = Request.Form.Files[0];

            file.SaveFileOrChunkFile(targetDir, tempDir, md5, chunks, chunk);

            return Json(new
            {
                Data = new { md5 = md5, url = Path.Combine("/", targetDir, file.FileName) },
                Message = "ok",
                Result = true
            });
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="md5">文件md5</param>
        /// <param name="fileName">文件名</param>
        /// <param name="chunks">分块数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult MergeFile(string md5, string fileName, int chunks)
        {
            var tempDir = "UploadTemp";
            var targetDir = "UploadFile";

            var (res,msg) = tempDir.Merge(targetDir, fileName, md5, chunks);

            if (!res)
            {
                return Json(new
                {
                    Message = msg,
                    Result = false
                });
            }
            
            return Json(new
            {
                Data = new { md5 = md5, url = Path.Combine("/", targetDir, fileName) },
                Message = "ok",
                Result = true
            });
        }

        /// <summary>
        /// 检查文件或分块是否存在
        /// </summary>
        /// <param name="md5">文件md5</param>
        /// <param name="fileName">文件名</param>
        /// <param name="chunk">分块号</param>
        /// <returns></returns>
        public IActionResult CheckFile(string md5, string fileName, int? chunk)
        {
            var tempDir = "UploadTemp";
            var targetDir = "UploadFile";

            string filePath;

            //分片文件
            if (chunk != null)
            {
                filePath = Path.Combine(tempDir, md5, $"{chunk}.part");
            }
            else
            {
                filePath = Path.Combine(targetDir, fileName);
            }

            var exists = System.IO.File.Exists(filePath);

            return Json(new
            {
                Data = fileName!=null && exists ? (object)new { md5 = md5, url = Path.Combine("/", targetDir, fileName) } : null,
                Result = exists
            });
        }
        #endregion

        #endregion

        #region 帮助方法
        public byte[] ConvertToBinary(Stream stream)
        {
            // 保存为byte[] 
            byte[] byData = new byte[stream.Length];
            stream.Read(byData, 0, byData.Length);
            stream.Dispose();
            return byData;
        }
        /// <summary>
        /// 二进制转图片
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public Image ReturnPic(byte[] streamByte)
        {
            MemoryStream ms = new MemoryStream(streamByte);
            Image img = Image.FromStream(ms);
            return img;
        }
        #endregion
    }
}