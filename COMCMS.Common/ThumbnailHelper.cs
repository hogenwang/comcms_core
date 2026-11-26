using System;
using System.IO;
using SkiaSharp;

namespace COMCMS.Common
{
    public class ThumbnailHelper
    {
        public enum CutMode
        {
            HW = 0,
            W = 1,
            H = 2,
            Cut = 3
        }

        /// <summary>
        /// 生成缩略图（完整释放 SkiaSharp 原生资源并正确执行居中裁剪）
        /// </summary>
        public static void MakeThumbnailImage(string fileName, string newFileName, int width, int height, CutMode mode)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(newFileName)) return;
            if (width <= 0 && height <= 0) return;

            using var originalImage = SKBitmap.Decode(fileName);
            if (originalImage == null || originalImage.Width <= 0 || originalImage.Height <= 0) return;

            int towidth = width > 0 ? width : originalImage.Width;
            int toheight = height > 0 ? height : originalImage.Height;
            int x = 0, y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case CutMode.HW:
                case CutMode.Cut:
                    if ((double)originalImage.Width / originalImage.Height > (double)towidth / toheight)
                    {
                        oh = originalImage.Height;
                        ow = Math.Max(1, originalImage.Height * towidth / toheight);
                        y = 0;
                        x = Math.Max(0, (originalImage.Width - ow) / 2);
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = Math.Max(1, originalImage.Width * toheight / towidth);
                        x = 0;
                        y = Math.Max(0, (originalImage.Height - oh) / 2);
                    }
                    break;
                case CutMode.W:
                    toheight = Math.Max(1, originalImage.Height * towidth / originalImage.Width);
                    break;
                case CutMode.H:
                    towidth = Math.Max(1, originalImage.Width * toheight / originalImage.Height);
                    break;
            }

            var targetInfo = new SKImageInfo(Math.Max(1, towidth), Math.Max(1, toheight));
            using var surface = SKSurface.Create(targetInfo);
            if (surface == null) return;

            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);
            var srcRect = new SKRect(x, y, x + ow, y + oh);
            var dstRect = new SKRect(0, 0, targetInfo.Width, targetInfo.Height);
            using var sourceImage = SKImage.FromBitmap(originalImage);
            canvas.DrawImage(sourceImage, srcRect, dstRect, new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear));
            canvas.Flush();

            var imgType = ResolveImageFormat(fileName);
            using var snapshot = surface.Snapshot();
            using var encoded = snapshot.Encode(imgType, 90);
            if (encoded == null) return;

            using var fs = new FileStream(newFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            encoded.SaveTo(fs);
            fs.Flush();
        }

        private static SKEncodedImageFormat ResolveImageFormat(string fileName)
        {
            return Utils.GetFileExtName(fileName).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => SKEncodedImageFormat.Jpeg,
                ".png" => SKEncodedImageFormat.Png,
                ".gif" => SKEncodedImageFormat.Gif,
                ".webp" => SKEncodedImageFormat.Webp,
                ".bmp" => SKEncodedImageFormat.Bmp,
                _ => SKEncodedImageFormat.Png
            };
        }
    }
}
