using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using COMCMS.Common.Security;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Controllers.api.v1
{
    [ApiController]
    [Route("api/v1/files")]
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer + "," + AuthenticationSchemes.MemberCookie + "," + AuthenticationSchemes.AdminCookie)]
    public sealed class FilesController : ControllerBase
    {
        private static readonly IReadOnlyDictionary<string, string> MediaTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [".pdf"] = "application/pdf",
            [".txt"] = "text/plain",
            [".csv"] = "text/csv",
            [".mp3"] = "audio/mpeg",
            [".mp4"] = "video/mp4",
            [".wav"] = "audio/wav"
        };
        private readonly PrivateFileStorage _storage;

        public FilesController(PrivateFileStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("{*storageKey}")]
        public IActionResult Download(string storageKey, [FromQuery] string name)
        {
            if (!IsAuthorizedForStorageKey(User, storageKey))
                return Forbid();

            if (!_storage.TryResolve(storageKey, out var fullPath) || !System.IO.File.Exists(fullPath))
                return NotFound();

            Response.Headers["X-Content-Type-Options"] = "nosniff";
            Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";
            var extension = Path.GetExtension(fullPath);
            var contentType = MediaTypes.TryGetValue(extension, out var known) ? known : "application/octet-stream";
            var safeName = SanitizeDownloadName(string.IsNullOrWhiteSpace(name) ? Path.GetFileName(fullPath) : name, Path.GetFileName(fullPath));
            return PhysicalFile(fullPath, contentType, safeName, enableRangeProcessing: true);
        }

        internal static bool IsAuthorizedForStorageKey(ClaimsPrincipal user, string storageKey)
        {
            if (user?.Identity?.IsAuthenticated != true || string.IsNullOrWhiteSpace(storageKey))
                return false;

            var subjectType = user.FindFirstValue(ComCmsClaimTypes.SubjectType);
            if (string.Equals(subjectType, "admin", StringComparison.Ordinal))
                return true;

            if (!string.Equals(subjectType, "member", StringComparison.Ordinal))
                return false;

            var memberId = ComCmsClaimTypes.GetSubjectId(user);
            if (memberId <= 0) return false;

            var normalized = storageKey.TrimStart('/', '\\').Replace('\\', '/');
            if (normalized.Split('/', StringSplitOptions.RemoveEmptyEntries).Any(seg => seg == ".." || seg == "."))
                return false;
            var expectedPrefix = $"members/{memberId}/";
            return normalized.StartsWith(expectedPrefix, StringComparison.OrdinalIgnoreCase) &&
                   normalized.Length > expectedPrefix.Length;
        }

        internal static string SanitizeDownloadName(string requestedName, string fallbackName)
        {
            var baseName = Path.GetFileName((requestedName ?? string.Empty).Replace('\\', '/'));
            var cleaned = new string(baseName.Where(c => !char.IsControl(c) && c != '"' && c != ';' && c != '/' && c != '\\').ToArray()).Trim();
            return string.IsNullOrWhiteSpace(cleaned) ? fallbackName : cleaned;
        }
    }
}
