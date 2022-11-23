using NewLife.Caching;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using NewLife.Log;
using SkiaSharp;
using NewLife.Security;
using static System.Net.Mime.MediaTypeNames;

namespace COMCMS.Common
{
    public sealed class VerifyCodeHelper
    {
        #region 单例模式
        //Session Key
        public readonly string SESSION_KEY = "comcms_verifycode";
        //创建私有化静态obj锁  
        private static readonly object _ObjLock = new object();
        //创建私有静态字段，接收类的实例化对象  
        private static VerifyCodeHelper _VerifyCodeHelper = null;
        //构造函数私有化  
        private VerifyCodeHelper() { }
        //创建单利对象资源并返回  
        public static VerifyCodeHelper GetSingleObj()
        {
            if (_VerifyCodeHelper == null)
            {
                lock (_ObjLock)
                {
                    if (_VerifyCodeHelper == null)
                        _VerifyCodeHelper = new VerifyCodeHelper();
                }
            }
            return _VerifyCodeHelper;
        }
        #endregion

        #region 生产验证码
        public enum VerifyCodeType { NumberVerifyCode, AbcVerifyCode, MixVerifyCode };

        /// <summary>
        /// 1.数字验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string CreateNumberVerifyCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值  
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字  
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字  
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码  
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 2.字母验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private string CreateAbcVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>
        /// 3.混合验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private string CreateMixVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>
        /// 产生验证码（随机产生4-6位）
        /// </summary>
        /// <param name="type">验证码类型：数字，字符，符合</param>
        /// <returns></returns>
        public string CreateVerifyCode(VerifyCodeType type)
        {
            string verifyCode = string.Empty;
            Random random = new Random();
            int length = random.Next(4, 6);
            switch (type)
            {
                case VerifyCodeType.NumberVerifyCode:
                    verifyCode = GetSingleObj().CreateNumberVerifyCode(length);
                    break;
                case VerifyCodeType.AbcVerifyCode:
                    verifyCode = GetSingleObj().CreateAbcVerifyCode(length);
                    break;
                case VerifyCodeType.MixVerifyCode:
                    verifyCode = GetSingleObj().CreateMixVerifyCode(length);
                    break;
            }

            //2019-03-02 增加存入缓存
            //var ic = new MemoryCache();
            //ic.Set<string>("code_" + verifyCode, verifyCode, 120);//2分钟自动过期
            //2019-03-04 增加Redis缓存
            Utils.MemoryCahce.Set<string>("code_" + verifyCode, verifyCode, 120);

            return verifyCode;
        }
        #endregion

        #region 验证码图片
        ///// <summary>
        ///// 验证码图片 => Bitmap
        ///// </summary>
        ///// <param name="verifyCode">验证码</param>
        ///// <param name="width">宽</param>
        ///// <param name="height">高</param>
        ///// <returns>Bitmap</returns>
        //public Bitmap CreateBitmapByImgVerifyCode(string verifyCode, int width, int height)
        //{
        //    Font font = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
        //    Brush brush;
        //    Bitmap bitmap = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    SizeF totalSizeF = g.MeasureString(verifyCode, font);
        //    SizeF curCharSizeF;
        //    PointF startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
        //    Random random = new Random(); //随机数产生器
        //    g.Clear(Color.White); //清空图片背景色  
        //    for (int i = 0; i < verifyCode.Length; i++)
        //    {
        //        brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
        //        g.DrawString(verifyCode[i].ToString(), font, brush, startPointF);
        //        curCharSizeF = g.MeasureString(verifyCode[i].ToString(), font);
        //        startPointF.X += curCharSizeF.Width;
        //    }

        //    //画图片的干扰线  
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int x1 = random.Next(bitmap.Width);
        //        int x2 = random.Next(bitmap.Width);
        //        int y1 = random.Next(bitmap.Height);
        //        int y2 = random.Next(bitmap.Height);
        //        g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        //    }

        //    //画图片的前景干扰点  
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = random.Next(bitmap.Width);
        //        int y = random.Next(bitmap.Height);
        //        bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
        //    }

        //    g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1); //画图片的边框线  
        //    g.Dispose();
        //    return bitmap;
        //}

        ///// <summary>
        ///// 验证码图片 => byte[]
        ///// </summary>
        ///// <param name="verifyCode">验证码</param>
        ///// <param name="width">宽</param>
        ///// <param name="height">高</param>
        ///// <returns>byte[]</returns>
        //public byte[] CreateByteByImgVerifyCode(string verifyCode, int width, int height)
        //{
        //    Font font = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
        //    Brush brush;
        //    Bitmap bitmap = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    SizeF totalSizeF = g.MeasureString(verifyCode, font);
        //    SizeF curCharSizeF;
        //    PointF startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
        //    Random random = new Random(); //随机数产生器
        //    g.Clear(Color.White); //清空图片背景色  
        //    for (int i = 0; i < verifyCode.Length; i++)
        //    {
        //        brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
        //        g.DrawString(verifyCode[i].ToString(), font, brush, startPointF);
        //        curCharSizeF = g.MeasureString(verifyCode[i].ToString(), font);
        //        startPointF.X += curCharSizeF.Width;
        //    }

        //    //画图片的干扰线  
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int x1 = random.Next(bitmap.Width);
        //        int x2 = random.Next(bitmap.Width);
        //        int y1 = random.Next(bitmap.Height);
        //        int y2 = random.Next(bitmap.Height);
        //        g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        //    }

        //    //画图片的前景干扰点  
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = random.Next(bitmap.Width);
        //        int y = random.Next(bitmap.Height);
        //        bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
        //    }

        //    g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1); //画图片的边框线  
        //    g.Dispose();

        //    //保存图片数据  
        //    MemoryStream stream = new MemoryStream();
        //    bitmap.Save(stream, ImageFormat.Jpeg);
        //    //输出图片流  
        //    return stream.ToArray();

        //}
        #endregion

        #region 图片验证码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public byte[] GetVerifyCode(string text, int width, int height)
        {

            Random random = new();

            //创建bitmap位图
            using SKBitmap image = new(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            //创建画笔
            using SKCanvas canvas = new(image);
            //填充背景颜色为白色
            canvas.DrawColor(SKColors.White);

            //画图片的背景噪音线
            for (int i = 0; i < (width * height * 0.015); i++)
            {
                using SKPaint drawStyle = new();
                drawStyle.Color = new(Convert.ToUInt32(random.Next(Int32.MaxValue)));

                canvas.DrawLine(random.Next(0, width), random.Next(0, height), random.Next(0, width), random.Next(0, height), drawStyle);
            }
            // 在图片上写验证码
            SKPaint textPaint = new SKPaint
            {
                IsAntialias = true,
                TextAlign = SKTextAlign.Left,
                Shader = SKShader.CreateLinearGradient(new SKPoint(0, 0), new SKPoint(width, height), new SKColor[] { SKColors.Blue, SKColors.DarkRed }, SKShaderTileMode.Clamp),
                TextSize = 22
            };
            canvas.DrawText(text, 4, 24, textPaint);

            //将文字写到画布上
            //using (SKPaint drawStyle = new())
            //{
            //    drawStyle.Color = SKColors.Red;
            //    drawStyle.TextSize = height;
            //    drawStyle.StrokeWidth = 1;

            //    float emHeight = height - (float)height * (float)0.14;
            //    float emWidth = ((float)width / text.Length) - ((float)width * (float)0.13);

            //    canvas.DrawText(text, emWidth, emHeight, drawStyle);
            //}
            for (int i = 0; i < 5; i++)
            {
                canvas.DrawLine(new SKPoint(random.Next(width), random.Next(height)), new SKPoint(random.Next(width), random.Next(height)), new SKPaint() { Color = SKColor.FromHsv(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)) });
            }

            // 在图片上随机画100个随机颜色点
            for (int i = 0; i < 100; i++)
            {
                canvas.DrawPoint(new SKPoint(random.Next(width), random.Next(height)), SKColor.FromHsv(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
            }

            //画图片的前景噪音点
            //for (int i = 0; i < (width * height * 0.6); i++)
            //{
            //    image.SetPixel(random.Next(0, width), random.Next(0, height), new SKColor(Convert.ToUInt32(random.Next(Int32.MaxValue))));
            //}

            using var img = SKImage.FromBitmap(image);
            using SKData p = img.Encode(SKEncodedImageFormat.Png, 100);
            return p.ToArray();
        }
        #endregion

        #region 验证验证码
        /// <summary>
        /// 验证验证码是否成功
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool VerifyCodeIsOK(string code)
        {
            string vc = SessionHelper.GetSession(SESSION_KEY)?.ToString();
            //2019-03-02 增加缓存判断
            if (string.IsNullOrEmpty(vc))
            {
                //如果Session为空，则使用缓存判断
                //var ic = new MemoryCache();
                //vc = ic.Get<string>("code_" + code);
                //var ic = Utils.RedisCahce;//Redis.Create(Utils.RedisServer, Utils.RedisIndex);
                vc = Utils.MemoryCahce.Get<string>("code_" + code);// ic.Get<string>("code_" + code);
                //XTrace.WriteLine($"从缓存中获取验证码：code_{code}:{vc}");
            }
            if (!string.IsNullOrEmpty(vc) && !string.IsNullOrEmpty(code))
            {
                if (vc.ToUpper() == code.ToUpper())
                    return true;
            }


            return false;
        }
        #endregion
    }
}
