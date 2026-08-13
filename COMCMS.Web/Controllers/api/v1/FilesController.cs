using System;
using System.Collections.Generic;
using System.IO;
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
            if (!_storage.TryResolve(storageKey, out var fullPath) || !System.IO.File.Exists(fullPath)) return NotFound();
            Response.Headers["X-Content-Type-Options"] = "nosniff";
            var extension = Path.GetExtension(fullPath);
            var contentType = MediaTypes.TryGetValue(extension, out var known) ? known : "application/octet-stream";
            var safeName = Path.GetFileName(string.IsNullOrWhiteSpace(name) ? Path.GetFileName(fullPath) : name);
            return PhysicalFile(fullPath, contentType, safeName, enableRangeProcessing: true);
        }
    }
}
