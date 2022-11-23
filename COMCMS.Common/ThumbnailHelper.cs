using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NewLife.Log;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Security.Policy;

namespace COMCMS.Common
{
    public class ThumbnailHelper
    {
        /// <summary>
        /// 目录分隔符
        /// windows 下是 \ linux 下面是 \
        /// </summary>
        private static string DirectorySeparatorChar = Path.DirectorySeparatorChar.ToString();
        /// <summary>
        /// 程序绝对目录
        /// </summary>
        private static string _ContentRootPath = Environment.CurrentDirectory;
        /// <summary>
        /// 静态文件
        /// </summary>
        private static string _WebRootPath = _ContentRootPath + DirectorySeparatorChar + "wwwroot" + DirectorySeparatorChar;

        #region 方式
        public enum CutMode
        {
            /// <summary>
            /// 指定高宽缩放（补白）
            /// </summary>
            HW = 0,
            /// <summary>
            /// 指定宽，高按比例  
            /// </summary>
            W = 1,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H = 2,
            /// <summary>
            /// 指定高宽裁减（不变形）
            /// </summary>
            Cut = 3
        }
        #endregion

        #region 2012-2-19 新增生成图片缩略图方法
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="fileName">源图路径（绝对路径）</param>
        /// <param name="newFileName">缩略图路径（绝对路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnailImage(string fileName, string newFileName, int width, int height, CutMode mode)
        {
            //Image originalImage = Image.FromFile(fileName);
            var originalImage = SKBitmap.Decode(fileName);
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case CutMode.HW://指定高宽缩放（补白）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    else
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    break;
                case CutMode.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case CutMode.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case CutMode.Cut://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }


            var newImg = originalImage.Resize(new SKSizeI(towidth, toheight), SKFilterQuality.Medium);
            using var fs = new FileStream(newFileName, FileMode.Create);
            //判断类型
            SKEncodedImageFormat imgType = new SKEncodedImageFormat();
            string sFullExtension = Utils.GetFileExtName(fileName).ToLower();//扩展名
            switch (sFullExtension)
            {
                case ".jpg":
                case ".jpeg":
                    imgType = SKEncodedImageFormat.Jpeg;
                    break;
                case ".png":
                    imgType = SKEncodedImageFormat.Png;
                    break;
                case ".gif":
                    imgType = SKEncodedImageFormat.Gif;
                    break;
                case ".webp":
                    imgType = SKEncodedImageFormat.Webp;
                    break;
                case ".bmp":
                    imgType = SKEncodedImageFormat.Bmp;
                    break;
                default:
                    imgType = SKEncodedImageFormat.Png;
                    break;
            }
            newImg.Encode(fs, imgType, 100);
            fs.Flush();

            //新建一个bmp图片
            //Bitmap b = new Bitmap(towidth, toheight);
            //try
            //{
            //    //新建一个画板
            //    Graphics g = Graphics.FromImage(b);
            //    //设置高质量插值法
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    //设置高质量,低速度呈现平滑程度
            //    g.SmoothingMode = SmoothingMode.AntiAlias;
            //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //    //清空画布并以透明背景色填充
            //    g.Clear(Color.White);
            //    //g.Clear(Color.Transparent);
            //    //在指定位置并且按指定大小绘制原图片的指定部分
            //    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            //    SaveImage(b, newFileName, GetCodecInfo("image/" + GetFormat(newFileName).ToString().ToLower()));
            //}
            //catch (System.Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    originalImage.Dispose();
            //    b.Dispose();
            //}
        }
        #endregion


        #region 帮助方法
        #endregion

        #region 内容远程保存图片
        /// <summary>
        /// 保存内容种远程图片到本地
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SaveRemoteImgForContent(string content)
        {
            string recontent = content;
            if (string.IsNullOrEmpty(content))
                return "";
            //WebClient client = new WebClient();
            var _httpClient = new HttpClient();
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(content);
            foreach (Match math in m)
            {
                string imgUrl = math.Groups[1].Value;
                Regex regName = new Regex(@"\w+(?:jpg|gif|bmp|png|jpeg)", RegexOptions.IgnoreCase);//不按点。这样可以兼容复制公众号文章
                string imgName = imgUrl.Substring(imgUrl.LastIndexOf("/") + 1, imgUrl.Length - imgUrl.LastIndexOf("/") - 1);
                //判断是否是远程图片
                if (imgUrl.ToLower().StartsWith("http://") || imgUrl.ToLower().StartsWith("https://") || imgUrl.ToLower().StartsWith("ftp://"))
                {
                    string imgsrc = imgName.ToLower();
                    string ext = "jpg";
                    if (imgsrc.EndsWith("gif")) ext = "gif";
                    else if (imgsrc.EndsWith("png")) ext = "png";
                    else if (imgsrc.EndsWith("jpeg")) ext = "jpeg";
                    else if (imgsrc.EndsWith("bmp")) ext = "bmp";
                    string strNewImgName = Utils.GetOrderNum() + "." + ext;
                    string savepath = $"{_WebRootPath}{DirectorySeparatorChar}userfiles{DirectorySeparatorChar}images{DirectorySeparatorChar}auto{DirectorySeparatorChar}{DateTime.Now.Year.ToString()}\\{DateTime.Now.ToString("MM")}";// 
                    string imgNewSrc = $"/userfiles/images/auto/{DateTime.Now.Year.ToString()}/{DateTime.Now.ToString("MM")}/{strNewImgName}";//保存后路径
                    //判断路径
                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);
                    string fullpath = Path.Combine(savepath, strNewImgName);//图片
                    try
                    {
                        System.IO.FileStream fs;
                        byte[] urlContents = _httpClient.GetByteArrayAsync(imgUrl).Result;
                        fs = new System.IO.FileStream(fullpath, FileMode.CreateNew);
                        fs.Write(urlContents, 0, urlContents.Length);
                        //保存图片
                        //client.DownloadFile(imgUrl, fullpath);
                        //_httpClient
                        recontent = recontent.Replace(imgUrl, imgNewSrc);
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception(ex.Message);
                        XTrace.WriteLine($"远程保存图片保存图片：{imgUrl},错误：{ex.Message}");
                    }
                }

            }

            return recontent;
        }
        #endregion

    }


}
