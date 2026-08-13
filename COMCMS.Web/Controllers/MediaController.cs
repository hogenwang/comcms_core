using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Controllers
{
    [ApiController]
    [Route("media")]
    public sealed class MediaController : ControllerBase
    {
        private readonly PublicMediaStorage _storage;

        public MediaController(PublicMediaStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("{*storageKey}")]
        [ResponseCache(Duration = 604800, Location = ResponseCacheLocation.Client)]
        public IActionResult Stream(string storageKey)
        {
            if (!_storage.TryResolve(storageKey, out var fullPath, out var contentType) ||
                !System.IO.File.Exists(fullPath))
                return NotFound();

            Response.Headers["X-Content-Type-Options"] = "nosniff";
            Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";
            return PhysicalFile(fullPath, contentType, enableRangeProcessing: true);
        }
    }
}
