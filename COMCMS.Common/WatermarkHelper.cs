using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace COMCMS.Common
{
    /// <summary>
    /// 水印帮助类
    /// </summary>
    public class WatermarkHelper
    {

        #region 加图片水印
        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <p
        /// <param name="filename">文件名</param>
        /// <param name="watermarkFilename">水印文件名</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void AddImageSignPic(string copyfullpath, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            var img = SKBitmap.Decode(copyfullpath);
            using SKCanvas canvas = new(img);

            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var watermarkBitmap = SKBitmap.Decode(watermarkFilename);
            SKImage watermark = SKImage.FromBitmap(watermarkBitmap);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
                return;

            int xpos = 0;
            int ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
            }
            SKPoint point = new SKPoint()
            {
                X = xpos,
                Y = ypos
            };

            canvas.DrawImage(watermark, point);

            //var newImg = SKImage.fo originalImage.Resize(new SKSizeI(towidth, toheight), SKFilterQuality.Medium);
            //canvas.
            using var fs = new FileStream(filename, FileMode.Create);
            SKEncodedImageFormat imgType = new SKEncodedImageFormat();
            string sFullExtension = Utils.GetFileExtName(filename).ToLower();//扩展名
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
            img.Encode(fs, imgType, 100);
            fs.Flush();


            ////g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

            //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            //ImageCodecInfo ici = null;
            //foreach (ImageCodecInfo codec in codecs)
            //{
            //    if (codec.MimeType.IndexOf("jpeg") > -1)
            //        ici = codec;
            //}
            //EncoderParameters encoderParams = new EncoderParameters();
            //long[] qualityParam = new long[1];
            //if (quality < 0 || quality > 100)
            //    quality = 80;

            //qualityParam[0] = quality;

            //EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            //encoderParams.Param[0] = encoderParam;

            //if (ici != null)
            //    img.Save(filename, ici, encoderParams);
            //else
            //    img.Save(filename);

            img.Dispose();
            watermark.Dispose();
        }

        #endregion

    }
}
