using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace COMCMS.Common.Security
{
    public static class PasswordHashService
    {
        private static readonly PasswordHasher<object> Hasher = new PasswordHasher<object>();

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            return Hasher.HashPassword(null, password);
        }

        public static bool Verify(string storedHash, string legacySalt, string password, out string upgradedHash)
        {
            upgradedHash = null;
            if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(password)) return false;

            if (IsLegacyMd5(storedHash))
            {
                var migrationEndValue = Utils.GetSetting("Security:LegacyPasswordMigrationEndsUtc");
                if (!IsLegacyMigrationAllowed(migrationEndValue, DateTimeOffset.UtcNow))
                    return false;
                var actual = Utils.MD5((legacySalt ?? string.Empty) + password);
                if (!FixedTimeEquals(storedHash, actual)) return false;

                upgradedHash = HashPassword(password);
                return true;
            }

            try
            {
                var result = Hasher.VerifyHashedPassword(null, storedHash, password);
                if (result == PasswordVerificationResult.Failed) return false;
                if (result == PasswordVerificationResult.SuccessRehashNeeded)
                    upgradedHash = HashPassword(password);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        public static bool IsLegacyMd5(string value)
        {
            if (value == null || value.Length != 32) return false;
            foreach (var character in value)
            {
                if (!Uri.IsHexDigit(character)) return false;
            }
            return true;
        }

        public static bool IsLegacyMigrationAllowed(string migrationEndValue, DateTimeOffset nowUtc)
        {
            return !DateTimeOffset.TryParse(migrationEndValue, out var migrationEnd) || nowUtc < migrationEnd.ToUniversalTime();
        }

        private static bool FixedTimeEquals(string left, string right)
        {
            var leftBytes = Encoding.ASCII.GetBytes(left.ToUpperInvariant());
            var rightBytes = Encoding.ASCII.GetBytes(right.ToUpperInvariant());
            return leftBytes.Length == rightBytes.Length && CryptographicOperations.FixedTimeEquals(leftBytes, rightBytes);
        }
    }
}
