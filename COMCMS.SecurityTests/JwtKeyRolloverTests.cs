using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using COMCMS.Web.Models;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class JwtKeyRolloverTests
    {
        [Fact]
        public void PreviousPublicKey_CanValidateUnexpiredToken()
        {
            using var previousRsa = RSA.Create(2048);
            var settings = new AuthenticationSettings
            {
                Issuer = "issuer",
                Audience = "audience",
                KeyId = "current",
                PreviousVerificationKeys = new[]
                {
                    new JwtVerificationKeySettings
                    {
                        KeyId = "previous",
                        PublicKeyPem = previousRsa.ExportSubjectPublicKeyInfoPem()
                    }
                }
            };
            using var provider = new JwtKeyProvider(Options.Create(settings), new TestEnvironment());
            var previousKey = new RsaSecurityKey(previousRsa) { KeyId = "previous" };
            var token = new JwtSecurityToken(settings.Issuer, settings.Audience,
                notBefore: DateTime.UtcNow.AddMinutes(-1), expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: new SigningCredentials(previousKey, SecurityAlgorithms.RsaSha256));

            var principal = new JwtSecurityTokenHandler().ValidateToken(
                new JwtSecurityTokenHandler().WriteToken(token), provider.CreateValidationParameters(settings), out _);

            Assert.NotNull(principal);
        }

        private sealed class TestEnvironment : IWebHostEnvironment
        {
            public string ApplicationName { get; set; } = "COMCMS.SecurityTests";
            public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
            public string WebRootPath { get; set; } = AppContext.BaseDirectory;
            public string EnvironmentName { get; set; } = "Development";
            public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        }
    }
}
