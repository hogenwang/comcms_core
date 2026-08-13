using System;
using System.Security.Cryptography;
using System.Text;

namespace COMCMS.Common.Security
{
    public static class OneTimeTokenService
    {
        public static string CreateToken() => Base64UrlEncode(RandomNumberGenerator.GetBytes(32));

        public static string HashToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return string.Empty;
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }

        private static string Base64UrlEncode(byte[] value) => Convert.ToBase64String(value)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
