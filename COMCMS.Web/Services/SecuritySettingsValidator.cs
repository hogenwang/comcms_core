using System;
using System.IO;
using System.Net;
using COMCMS.Common.Security;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace COMCMS.Web.Services
{
    public sealed class SecuritySettingsValidator : IValidateOptions<SecuritySettings>
    {
        private const long MinimumUploadBytes = 1L * 1024 * 1024;
        private const long MaximumUploadBytes = 100L * 1024 * 1024;
        private readonly IWebHostEnvironment _environment;

        public SecuritySettingsValidator(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public ValidateOptionsResult Validate(string name, SecuritySettings settings)
        {
            if (settings == null)
                return ValidateOptionsResult.Fail("Security configuration is required.");
            if (settings.MaxUploadBytes is < MinimumUploadBytes or > MaximumUploadBytes)
                return ValidateOptionsResult.Fail("Security:MaxUploadBytes must be between 1 MiB and 100 MiB.");
            if (!DateTimeOffset.TryParse(settings.LegacyPasswordMigrationEndsUtc, out _))
                return ValidateOptionsResult.Fail("Security:LegacyPasswordMigrationEndsUtc must be a valid timestamp with an explicit offset.");

            foreach (var proxy in settings.KnownProxies ?? Array.Empty<string>())
            {
                if (!IPAddress.TryParse(proxy, out _))
                    return ValidateOptionsResult.Fail($"Security:KnownProxies contains an invalid IP address: '{proxy}'.");
            }

            var uploadRoot = ResolveUploadRoot(settings.PrivateUploadRoot);
            if (IsWithin(uploadRoot, _environment.WebRootPath))
                return ValidateOptionsResult.Fail("Security:PrivateUploadRoot must be outside WebRoot.");

            if (!_environment.IsDevelopment() &&
                PasswordHashService.IsLegacyMigrationAllowed(settings.LegacyPasswordMigrationEndsUtc, DateTimeOffset.UtcNow) &&
                DateTimeOffset.Parse(settings.LegacyPasswordMigrationEndsUtc).ToUniversalTime() > DateTimeOffset.UtcNow.AddDays(180))
                return ValidateOptionsResult.Fail("The legacy password migration deadline must not be more than 180 days in the future.");

            return ValidateOptionsResult.Success;
        }

        private string ResolveUploadRoot(string configured) => Path.GetFullPath(
            string.IsNullOrWhiteSpace(configured)
                ? Path.Combine(_environment.ContentRootPath, "App_Data", "uploads")
                : Path.IsPathRooted(configured) ? configured : Path.Combine(_environment.ContentRootPath, configured));

        private static bool IsWithin(string candidate, string parent)
        {
            if (string.IsNullOrWhiteSpace(parent)) return false;
            var relative = Path.GetRelativePath(Path.GetFullPath(parent), Path.GetFullPath(candidate));
            return relative == "." || (!Path.IsPathRooted(relative) && relative != ".." &&
                !relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal));
        }
    }
}
