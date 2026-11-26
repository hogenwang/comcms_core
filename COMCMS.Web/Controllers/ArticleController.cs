using COMCMS.Web.Common;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Controllers
{
    public class ArticleController : HomeBaseController
    {
        private readonly PublicContentService _contentService;

        public ArticleController(PublicContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        public IActionResult Index(int id = 0, int page = 1)
        {
            return RenderContentResult(_contentService.BuildArticleCategory(id, page, cfg));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            return RenderContentResult(_contentService.BuildArticleDetail(id, cfg));
        }
    }
}
