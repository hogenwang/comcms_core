using System;
using System.Security.Cryptography;
using System.Text;
using SkiaSharp;

namespace COMCMS.Common
{
    public sealed class VerifyCodeHelper
    {
        #region 单例模式
        public readonly string SESSION_KEY = "comcms_verifycode";
        private static readonly object _ObjLock = new object();
        private static VerifyCodeHelper _VerifyCodeHelper = null;
        private VerifyCodeHelper() { }

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

        private static readonly char[] DigitDictionary = "0123456789".ToCharArray();
        private static readonly char[] LetterDictionary = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz".ToCharArray();
        private static readonly char[] MixDictionary = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789abcdefghjkmnpqrstuvwxyz".ToCharArray();

        private static string CreateFromDictionary(char[] dictionary, int length)
        {
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = dictionary[RandomNumberGenerator.GetInt32(dictionary.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// 产生密码学安全随机验证码（4-5位）
        /// </summary>
        public string CreateVerifyCode(VerifyCodeType type)
        {
            int length = RandomNumberGenerator.GetInt32(4, 6);
            return type switch
            {
                VerifyCodeType.NumberVerifyCode => CreateFromDictionary(DigitDictionary, length),
                VerifyCodeType.AbcVerifyCode => CreateFromDictionary(LetterDictionary, length),
                _ => CreateFromDictionary(MixDictionary, length)
            };
        }
        #endregion

        #region 图片验证码
        public byte[] GetVerifyCode(string text, int width, int height)
        {
            using SKBitmap image = new(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            using SKCanvas canvas = new(image);
            canvas.DrawColor(SKColors.White);

            // 背景噪音线
            int bgLines = (int)(width * height * 0.015);
            for (int i = 0; i < bgLines; i++)
            {
                using SKPaint drawStyle = new()
                {
                    Color = new SKColor(
                        (byte)RandomNumberGenerator.GetInt32(256),
                        (byte)RandomNumberGenerator.GetInt32(256),
                        (byte)RandomNumberGenerator.GetInt32(256),
                        160)
                };
                canvas.DrawLine(
                    RandomNumberGenerator.GetInt32(width),
                    RandomNumberGenerator.GetInt32(height),
                    RandomNumberGenerator.GetInt32(width),
                    RandomNumberGenerator.GetInt32(height),
                    drawStyle);
            }

            // 验证码文字
            using SKFont textFont = new() { Size = 22 };
            using SKShader shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(width, height),
                new[] { SKColors.Blue, SKColors.DarkRed },
                SKShaderTileMode.Clamp);
            using SKPaint textPaint = new()
            {
                IsAntialias = true,
                Shader = shader
            };
            canvas.DrawText(text ?? string.Empty, 4, 24, SKTextAlign.Left, textFont, textPaint);

            // 前景干扰线
            for (int i = 0; i < 5; i++)
            {
                using SKPaint linePaint = new()
                {
                    Color = SKColor.FromHsv(
                        RandomNumberGenerator.GetInt32(256),
                        RandomNumberGenerator.GetInt32(256),
                        RandomNumberGenerator.GetInt32(256))
                };
                canvas.DrawLine(
                    new SKPoint(RandomNumberGenerator.GetInt32(width), RandomNumberGenerator.GetInt32(height)),
                    new SKPoint(RandomNumberGenerator.GetInt32(width), RandomNumberGenerator.GetInt32(height)),
                    linePaint);
            }

            // 随机颜色点
            for (int i = 0; i < 100; i++)
            {
                canvas.DrawPoint(
                    new SKPoint(RandomNumberGenerator.GetInt32(width), RandomNumberGenerator.GetInt32(height)),
                    SKColor.FromHsv(
                        RandomNumberGenerator.GetInt32(256),
                        RandomNumberGenerator.GetInt32(256),
                        RandomNumberGenerator.GetInt32(256)));
            }

            using var img = SKImage.FromBitmap(image);
            using SKData p = img.Encode(SKEncodedImageFormat.Png, 100);
            return p.ToArray();
        }
        #endregion

        #region 验证验证码
        /// <summary>
        /// 一次性验证当前会话中的验证码（验证后立即清除，不允许复用与跨会话共享）
        /// </summary>
        public bool VerifyCodeIsOK(string code)
        {
            string expected = SessionHelper.GetSession(SESSION_KEY)?.ToString();
            // 无论验证成功还是失败，立即作废当前会话验证码，防止重放与暴力猜测
            SessionHelper.RemoveSession(SESSION_KEY);
            return VerifyCodeValue(expected, code);
        }

        /// <summary>
        /// 常量时间比对验证码（供单元测试与内部调用）
        /// </summary>
        public static bool VerifyCodeValue(string expected, string actual)
        {
            if (string.IsNullOrWhiteSpace(expected) || string.IsNullOrWhiteSpace(actual))
                return false;

            var left = Encoding.UTF8.GetBytes(expected.Trim().ToUpperInvariant());
            var right = Encoding.UTF8.GetBytes(actual.Trim().ToUpperInvariant());
            return left.Length == right.Length && CryptographicOperations.FixedTimeEquals(left, right);
        }
        #endregion
    }
}
