using COMCMS.Web.Common;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Controllers
{
    public class ProductController : HomeBaseController
    {
        private readonly PublicContentService _contentService;

        public ProductController(PublicContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        public IActionResult Index(int id = 0, int page = 1)
        {
            return RenderContentResult(_contentService.BuildProductCategory(id, page, cfg));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            return RenderContentResult(_contentService.BuildProductDetail(id, cfg));
        }
    }
}
