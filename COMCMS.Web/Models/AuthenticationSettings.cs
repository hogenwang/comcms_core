namespace COMCMS.Web.Models
{
    public sealed class AuthenticationSettings
    {
        public string Issuer { get; set; } = "COMCMS";
        public string Audience { get; set; } = "COMCMS.API";
        public string KeyId { get; set; } = "comcms-dev";
        public string PrivateKeyPem { get; set; }
        public string PublicKeyPem { get; set; }
        public string PrivateKeyPath { get; set; }
        public string PublicKeyPath { get; set; }
        public JwtVerificationKeySettings[] PreviousVerificationKeys { get; set; } = System.Array.Empty<JwtVerificationKeySettings>();
        public int AccessTokenMinutes { get; set; } = 10;
        public int RefreshTokenDays { get; set; } = 30;
        public string PasswordResetUrl { get; set; }
    }

    public sealed class JwtVerificationKeySettings
    {
        public string KeyId { get; set; }
        public string PublicKeyPem { get; set; }
        public string PublicKeyPath { get; set; }
    }

    public sealed class SecuritySettings
    {
        public long MaxUploadBytes { get; set; } = 52_428_800;
        public string LegacyPasswordMigrationEndsUtc { get; set; }
        public string PrivateUploadRoot { get; set; }
        public string[] KnownProxies { get; set; } = System.Array.Empty<string>();
    }
}
