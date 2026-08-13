using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewLife.Log;

namespace COMCMS.Web.Services
{
    public sealed class JwtKeyProvider : IDisposable
    {
        private readonly RSA _rsa;
        private readonly List<RSA> _previousRsa = new();

        public JwtKeyProvider(IOptions<AuthenticationSettings> options, IWebHostEnvironment environment)
        {
            var settings = options.Value;
            if (string.IsNullOrWhiteSpace(settings.Issuer) || string.IsNullOrWhiteSpace(settings.Audience) ||
                string.IsNullOrWhiteSpace(settings.KeyId) || settings.AccessTokenMinutes is < 1 or > 60 ||
                settings.RefreshTokenDays is < 1 or > 365)
                throw new InvalidOperationException("Authentication issuer, audience, key id and token lifetimes must be configured within the supported range.");
            if (!environment.IsDevelopment() && string.Equals(settings.KeyId, "configure-by-environment", StringComparison.Ordinal))
                throw new InvalidOperationException("Authentication:KeyId must be configured outside Development.");
            var privatePem = ReadKey(settings.PrivateKeyPem, settings.PrivateKeyPath, environment.ContentRootPath);
            var publicPem = ReadKey(settings.PublicKeyPem, settings.PublicKeyPath, environment.ContentRootPath);

            _rsa = RSA.Create(3072);
            if (!string.IsNullOrWhiteSpace(privatePem))
            {
                _rsa.ImportFromPem(privatePem);
                CanSign = true;
            }
            else if (!string.IsNullOrWhiteSpace(publicPem))
            {
                _rsa.ImportFromPem(publicPem);
            }
            else if (environment.IsDevelopment())
            {
                CanSign = true;
                XTrace.WriteLine("JWT signing key is ephemeral in Development. Configure Authentication:PrivateKeyPem or PrivateKeyPath for stable tokens.");
            }
            else
            {
                throw new InvalidOperationException("A JWT RSA public/private key must be configured outside Development.");
            }

            SecurityKey = new RsaSecurityKey(_rsa) { KeyId = settings.KeyId };
            var validationKeys = new List<SecurityKey> { SecurityKey };
            var keyIds = new HashSet<string>(StringComparer.Ordinal) { settings.KeyId };
            foreach (var previous in settings.PreviousVerificationKeys ?? Array.Empty<JwtVerificationKeySettings>())
            {
                if (string.IsNullOrWhiteSpace(previous?.KeyId) || !keyIds.Add(previous.KeyId))
                    throw new InvalidOperationException("Every JWT verification key must have a unique key id.");
                var pem = ReadKey(previous.PublicKeyPem, previous.PublicKeyPath, environment.ContentRootPath);
                if (string.IsNullOrWhiteSpace(pem))
                    throw new InvalidOperationException($"JWT verification key '{previous.KeyId}' has no public key.");
                var rsa = RSA.Create();
                rsa.ImportFromPem(pem);
                _previousRsa.Add(rsa);
                validationKeys.Add(new RsaSecurityKey(rsa) { KeyId = previous.KeyId });
            }
            ValidationKeys = validationKeys;
        }

        public RsaSecurityKey SecurityKey { get; }
        public IReadOnlyCollection<SecurityKey> ValidationKeys { get; }
        public bool CanSign { get; }

        public SigningCredentials CreateSigningCredentials()
        {
            if (!CanSign) throw new InvalidOperationException("The configured JWT key cannot sign tokens.");
            return new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256);
        }

        public TokenValidationParameters CreateValidationParameters(AuthenticationSettings settings) => new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = ValidationKeys,
            ValidateIssuer = true,
            ValidIssuer = settings.Issuer,
            ValidateAudience = true,
            ValidAudience = settings.Audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = System.Security.Claims.ClaimTypes.Name,
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,
            ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 }
        };

        public void Dispose()
        {
            _rsa.Dispose();
            foreach (var rsa in _previousRsa) rsa.Dispose();
        }

        private static string ReadKey(string inlinePem, string path, string contentRoot)
        {
            if (!string.IsNullOrWhiteSpace(inlinePem)) return inlinePem.Replace("\\n", "\n");
            if (string.IsNullOrWhiteSpace(path)) return null;
            var fullPath = Path.IsPathRooted(path) ? path : Path.Combine(contentRoot, path);
            return File.ReadAllText(fullPath);
        }
    }
}
