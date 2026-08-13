using System;
using System.IdentityModel.Tokens.Jwt;
using COMCMS.Web;
using COMCMS.Web.Models;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class JwtValidationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly JwtKeyProvider _keys;
        private readonly AuthenticationSettings _settings;

        public JwtValidationTests(WebApplicationFactory<Program> factory)
        {
            _keys = factory.Services.GetRequiredService<JwtKeyProvider>();
            _settings = factory.Services.GetRequiredService<IOptions<AuthenticationSettings>>().Value;
        }

        [Fact]
        public void WrongIssuer_IsRejected()
        {
            var token = CreateToken("wrong-issuer", _settings.Audience, DateTime.UtcNow.AddMinutes(-1), DateTime.UtcNow.AddMinutes(2));

            Assert.Throws<SecurityTokenInvalidIssuerException>(() => Validate(token));
        }

        [Fact]
        public void WrongAudience_IsRejected()
        {
            var token = CreateToken(_settings.Issuer, "wrong-audience", DateTime.UtcNow.AddMinutes(-1), DateTime.UtcNow.AddMinutes(2));

            Assert.Throws<SecurityTokenInvalidAudienceException>(() => Validate(token));
        }

        [Fact]
        public void ExpiredToken_IsRejected()
        {
            var token = CreateToken(_settings.Issuer, _settings.Audience, DateTime.UtcNow.AddMinutes(-3), DateTime.UtcNow.AddMinutes(-2));

            Assert.Throws<SecurityTokenExpiredException>(() => Validate(token));
        }

        private string CreateToken(string issuer, string audience, DateTime notBefore, DateTime expires)
        {
            var token = new JwtSecurityToken(issuer, audience, notBefore: notBefore, expires: expires,
                signingCredentials: _keys.CreateSigningCredentials());
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void Validate(string token) =>
            new JwtSecurityTokenHandler().ValidateToken(token, _keys.CreateValidationParameters(_settings), out _);
    }
}
