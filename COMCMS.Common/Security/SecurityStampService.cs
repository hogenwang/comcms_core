using System;
using System.Security.Cryptography;
using System.Text;

namespace COMCMS.Common.Security
{
    public static class SecurityStampService
    {
        public static string Compute(string subjectType, int subjectId, string passwordHash, int roleId, int isLocked)
        {
            var source = $"{subjectType}|{subjectId}|{passwordHash}|{roleId}|{isLocked}";
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(source)));
        }

        public static bool Equals(string expected, string actual)
        {
            if (string.IsNullOrEmpty(expected) || string.IsNullOrEmpty(actual)) return false;
            var expectedBytes = Encoding.ASCII.GetBytes(expected);
            var actualBytes = Encoding.ASCII.GetBytes(actual);
            return expectedBytes.Length == actualBytes.Length && CryptographicOperations.FixedTimeEquals(expectedBytes, actualBytes);
        }
    }
}
