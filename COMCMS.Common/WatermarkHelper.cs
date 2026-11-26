using System;
using System.IO;
using SkiaSharp;

namespace COMCMS.Common
{
    /// <summary>
    /// 水印帮助类
    /// </summary>
    public class WatermarkHelper
    {
        /// <summary>
        /// 加图片水印（完整释放 SkiaSharp 原生资源并正确应用透明度与压缩质量）
        /// </summary>
        public static void AddImageSignPic(string copyfullpath, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            using var img = SKBitmap.Decode(copyfullpath);
            if (img == null) return;

            using var watermarkBitmap = SKBitmap.Decode(watermarkFilename);
            if (watermarkBitmap == null) return;

            using var watermark = SKImage.FromBitmap(watermarkBitmap);
            if (watermark == null || watermark.Height >= img.Height || watermark.Width >= img.Width)
                return;

            using var canvas = new SKCanvas(img);

            int xpos = 0;
            int ypos = 0;
            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * 0.01f);
                    ypos = (int)(img.Height * 0.01f);
                    break;
                case 2:
                    xpos = (int)((img.Width * 0.50f) - (watermark.Width / 2f));
                    ypos = (int)(img.Height * 0.01f);
                    break;
                case 3:
                    xpos = (int)((img.Width * 0.99f) - watermark.Width);
                    ypos = (int)(img.Height * 0.01f);
                    break;
                case 4:
                    xpos = (int)(img.Width * 0.01f);
                    ypos = (int)((img.Height * 0.50f) - (watermark.Height / 2f));
                    break;
                case 5:
                    xpos = (int)((img.Width * 0.50f) - (watermark.Width / 2f));
                    ypos = (int)((img.Height * 0.50f) - (watermark.Height / 2f));
                    break;
                case 6:
                    xpos = (int)((img.Width * 0.99f) - watermark.Width);
                    ypos = (int)((img.Height * 0.50f) - (watermark.Height / 2f));
                    break;
                case 7:
                    xpos = (int)(img.Width * 0.01f);
                    ypos = (int)((img.Height * 0.99f) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * 0.50f) - (watermark.Width / 2f));
                    ypos = (int)((img.Height * 0.99f) - watermark.Height);
                    break;
                case 9:
                default:
                    xpos = (int)((img.Width * 0.99f) - watermark.Width);
                    ypos = (int)((img.Height * 0.99f) - watermark.Height);
                    break;
            }

            byte alpha = watermarkTransparency is >= 1 and <= 10
                ? (byte)Math.Clamp(watermarkTransparency * 25, 25, 255)
                : (byte)255;

            using var paint = new SKPaint
            {
                Color = SKColors.White.WithAlpha(alpha)
            };
            canvas.DrawImage(watermark, new SKPoint(Math.Max(0, xpos), Math.Max(0, ypos)), SKSamplingOptions.Default, paint);
            canvas.Flush();

            int safeQuality = quality is >= 1 and <= 100 ? quality : 90;
            SKEncodedImageFormat imgType = Utils.GetFileExtName(filename).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => SKEncodedImageFormat.Jpeg,
                ".png" => SKEncodedImageFormat.Png,
                ".gif" => SKEncodedImageFormat.Gif,
                ".webp" => SKEncodedImageFormat.Webp,
                ".bmp" => SKEncodedImageFormat.Bmp,
                _ => SKEncodedImageFormat.Png
            };

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            img.Encode(fs, imgType, safeQuality);
            fs.Flush();
        }
    }
}
