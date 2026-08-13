using System;
using COMCMS.Common;
using COMCMS.Common.Security;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class PasswordHashServiceTests
    {
        [Fact]
        public void LegacyMd5_CorrectPassword_ReturnsPbkdf2Upgrade()
        {
            const string salt = "legacy-salt";
            const string password = "Correct Horse Battery Staple";
            var legacyHash = Utils.MD5(salt + password);

            var valid = PasswordHashService.Verify(legacyHash, salt, password, out var upgradedHash);

            Assert.True(valid);
            Assert.False(PasswordHashService.IsLegacyMd5(upgradedHash));
            Assert.True(PasswordHashService.Verify(upgradedHash, null, password, out _));
        }

        [Fact]
        public void LegacyMd5_WrongPassword_DoesNotUpgrade()
        {
            var legacyHash = Utils.MD5("salt" + "expected-password");

            var valid = PasswordHashService.Verify(legacyHash, "salt", "wrong-password", out var upgradedHash);

            Assert.False(valid);
            Assert.Null(upgradedHash);
        }

        [Fact]
        public void Pbkdf2_WrongPassword_IsRejected()
        {
            var hash = PasswordHashService.HashPassword("a sufficiently long password");

            Assert.False(PasswordHashService.Verify(hash, null, "another password", out _));
        }

        [Fact]
        public void SecurityStamp_ChangesWithPasswordRoleOrLockState()
        {
            var original = SecurityStampService.Compute("member", 7, "hash-one", 2, 0);

            Assert.NotEqual(original, SecurityStampService.Compute("member", 7, "hash-two", 2, 0));
            Assert.NotEqual(original, SecurityStampService.Compute("member", 7, "hash-one", 3, 0));
            Assert.NotEqual(original, SecurityStampService.Compute("member", 7, "hash-one", 2, 1));
            Assert.True(SecurityStampService.Equals(original, original));
        }

        [Fact]
        public void OneTimeTokens_AreRandomAndOnlyHashIsStable()
        {
            var first = OneTimeTokenService.CreateToken();
            var second = OneTimeTokenService.CreateToken();

            Assert.NotEqual(first, second);
            Assert.Equal(64, OneTimeTokenService.HashToken(first).Length);
            Assert.Equal(OneTimeTokenService.HashToken(first), OneTimeTokenService.HashToken(first));
            Assert.DoesNotContain(first, OneTimeTokenService.HashToken(first));
        }

        [Fact]
        public void LegacyMigrationDeadline_RejectsAtAndAfterDeadline()
        {
            var deadline = new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero);

            Assert.True(PasswordHashService.IsLegacyMigrationAllowed(deadline.ToString("O"), deadline.AddSeconds(-1)));
            Assert.False(PasswordHashService.IsLegacyMigrationAllowed(deadline.ToString("O"), deadline));
            Assert.False(PasswordHashService.IsLegacyMigrationAllowed(deadline.ToString("O"), deadline.AddDays(1)));
        }
    }
}
