using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Common;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("商品")]
    public class ProductController : AdminBaseController
    {
        private readonly IContentSanitizer _contentSanitizer;
        private readonly RemoteImageService _remoteImages;

        public ProductController(IContentSanitizer contentSanitizer, RemoteImageService remoteImages)
        {
            _contentSanitizer = contentSanitizer;
            _remoteImages = remoteImages;
        }

        #region 商品栏目
        [MyAuthorize("viewlist", "category")]
        [DisplayName("商品栏目")]
        public IActionResult CategoryList()
        {
            IList<Category> list = Category.GetListTree(0, -1, false, true);
            Core.Admin.WriteLogActions("查看商品栏目列表页面；");
            return View(list);
        }

        [MyAuthorize("add", "category")]
        public IActionResult AddCategory()
        {
            ViewBag.ListTree = Category.GetListTree(0, -1, true, false);
            ViewBag.ListTpl = GetProductTemplateNames();
            Core.Admin.WriteLogActions("查看添加商品栏目页面；");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "category", "JSON")]
        public IActionResult AddCategory(Category model)
        {
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "商品栏目标题不能为空！";
                return Json(tip);
            }
            if (!ValidateCategoryFilePath(model.FilePath, 0, out var pathError))
            {
                tip.Message = pathError;
                return Json(tip);
            }

            model.PageSize = model.PageSize <= 0 ? 15 : model.PageSize;
            model.KindInfo = _contentSanitizer.Sanitize(model.KindInfo);
            model.Insert();
            Core.Admin.WriteLogActions("添加商品栏目(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加商品栏目成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [MyAuthorize("edit", "category")]
        public IActionResult EditCategory(int id)
        {
            Category entity = Category.Find(Category._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            ViewBag.ListTree = Category.GetListTree(0, -1, true, false);
            ViewBag.ListTpl = GetProductTemplateNames();
            Core.Admin.WriteLogActions("查看/编辑商品栏目（id:" + id + "）页面；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "category", "JSON")]
        public IActionResult EditCategory(Category model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "商品栏目标题不能为空！";
                return Json(tip);
            }

            Category entity = Category.Find(Category._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (!ValidateCategoryFilePath(model.FilePath, entity.Id, out var pathError))
            {
                tip.Message = pathError;
                return Json(tip);
            }

            if (entity.PId != model.PId)
            {
                if (entity.Id == model.PId)
                {
                    tip.Message = "上级栏目不能选择本身！";
                    return Json(tip);
                }
                entity.PId = model.PId;
                entity.Location = Category.GetNewLocation(model.PId);
                entity.Level = Category.GetNewLevel(model.PId);
                IList<Product> alist = Product.FindAll(Product._.KId == model.Id, null, null, 0, 0);
                if (alist != null && alist.Count > 0)
                {
                    foreach (var a in alist)
                    {
                        a.Location = entity.Location + "," + entity.Id;
                    }
                    alist.Save();
                }
            }

            entity.PId = model.PId;
            entity.KindName = model.KindName;
            entity.SubTitle = model.SubTitle;
            entity.KindTitle = model.KindTitle;
            entity.Keyword = model.Keyword;
            entity.Description = model.Description;
            entity.LinkURL = model.LinkURL;
            entity.TitleColor = model.TitleColor;
            entity.TemplateFile = model.TemplateFile;
            entity.DetailTemplateFile = model.DetailTemplateFile;
            entity.IsList = model.IsList;
            entity.PageSize = model.PageSize <= 0 ? 15 : model.PageSize;
            entity.IsLock = model.IsLock;
            entity.IsHide = model.IsHide;
            entity.IsShowSubDetail = model.IsShowSubDetail;
            entity.BannerImg = model.BannerImg;
            entity.Pic = model.Pic;
            entity.KindInfo = _contentSanitizer.Sanitize(model.KindInfo);
            entity.KindDomain = model.KindDomain;
            entity.Rank = model.Rank;
            entity.FilePath = model.FilePath;

            entity.Save();
            Core.Admin.WriteLogActions("修改商品栏目(id:" + entity.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改商品栏目成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "category", "JSON")]
        public JsonResult DelCategory(int id)
        {
            Category entity = Category.Find(Category._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本商品栏目！";
                return Json(tip);
            }
            if (Category.FindCount(Category._.PId == entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "本栏目有下级栏目，不允许删除！";
                return Json(tip);
            }
            IList<Product> list = Product.FindAll(Product._.KId == entity.Id, null, null, 0, 0);
            if (list != null && list.Count > 0)
            {
                list.Delete();
            }
            Core.Admin.WriteLogActions("删除商品栏目(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除商品栏目成功";
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("edit", "category", "JSON")]
        public IActionResult DoEditCategoryRank(int id, int rank)
        {
            Category entity = Category.Find(Category._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            entity.Rank = rank;
            entity.Update();
            Admin.WriteLogActions($"修改商品分类排序(id:{id},排序:{rank});");
            tip.Message = "修改商品分类排序成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 商品列表
        [MyAuthorize("viewlist", "product")]
        [DisplayName("商品列表")]
        public IActionResult ProductList()
        {
            ViewBag.ListKinds = Category.GetListTree(0, -1, true, false);
            Core.Admin.WriteLogActions("查看商品列表;");
            return View();
        }

        [MyAuthorize("viewlist", "product", "JSON")]
        public IActionResult GetProductList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = new Expression();

            Admin my = Admin.GetMyInfo();
            if (my?.Roles != null && my.Roles.IsSuperAdmin != 1)
            {
                var aclist = ParseAuthorizedIds(my.Roles.AuthorizedCagegory);
                if (aclist.Count > 0)
                {
                    ex &= Product._.KId.In(aclist);
                }
                if (my.Roles.OnlyEditMyselfProduct == 1)
                    ex &= Product._.AuthorId == my.Id;
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (Product._.Id == int.Parse(keyword) | Product._.Title.Contains(keyword));
                }
                else
                {
                    ex &= Product._.Title.Contains(keyword);
                }
            }
            string kid = Request.Query["kid"];
            string isshowsub = Request.Query["isshowsub"];
            if (Utils.IsInt(kid) && int.Parse(kid) > 0)
            {
                int kidVal = int.Parse(kid);
                if (isshowsub == "1")
                {
                    List<int> kids = new List<int> { kidVal };
                    IList<Category> subkinds = Category.FindByParentID(kidVal);
                    if (subkinds != null && subkinds.Count > 0)
                    {
                        kids.AddRange(subkinds.Select(s => s.Id));
                    }
                    ex &= Product._.KId.In(kids);
                }
                else
                {
                    ex &= Product._.KId == kidVal;
                }
            }

            IList<Product> list = Product.FindAll(ex, Product._.Sequence.Asc().And(Product._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Product.FindCount(ex, Product._.Sequence.Asc().And(Product._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "application/json");
        }
        #endregion

        #region 修改商品排序
        [HttpPost]
        [MyAuthorize("edit", "product", "JSON")]
        public IActionResult DoEditProductSequence(int id, int rank)
        {
            Product entity = Product.Find(Product._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            Admin my = Admin.GetMyInfo();
            if (!CanAccessProduct(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }
            entity.Sequence = rank;
            entity.Update();
            Admin.WriteLogActions($"修改商品排序(id:{id},排序:{rank});");
            tip.Message = "修改排序成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 添加商品
        [HttpGet]
        [MyAuthorize("add", "product")]
        public IActionResult AddProduct()
        {
            ViewBag.ListKinds = Category.GetListTree(0, -1, true, false);
            Core.Admin.WriteLogActions("查看添加商品页面;");
            Product entity = new Product
            {
                AddTime = DateTime.Now,
                Sequence = 999
            };
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("add", "product", "JSON")]
        public async Task<IActionResult> AddProduct(Product model)
        {
            if (model.KId <= 0)
            {
                tip.Message = "请选择一个商品分类";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "请填写商品标题";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                tip.Message = "请填写商品介绍";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Pic) && !Utils.IsImgFilename(model.Pic))
            {
                tip.Message = "商品图片填写的不是图片格式！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            if (!CanAccessProduct(my, model.KId, authorId: null, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                        morIMG += s + "|||";
                }
            }
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = await _remoteImages.ImportAsync(content, HttpContext.RequestAborted);
                model.Content = content;
            }
            model.Content = _contentSanitizer.Sanitize(model.Content);
            model.ItemImg = morIMG;
            model.AuthorId = my.Id;
            model.Insert();
            Category.UpdateDetailCount(model.KId);
            Core.Admin.WriteLogActions("添加商品(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加商品成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 修改商品详情
        [HttpGet]
        [MyAuthorize("edit", "product")]
        public IActionResult EditProduct(int id)
        {
            Product entity = Product.Find(Product._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Admin my = Admin.GetMyInfo();
            if (!CanAccessProduct(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                return EchoTipPage(deniedReason);
            }
            ViewBag.ListKinds = Category.GetListTree(0, -1, true, false);
            Core.Admin.WriteLogActions("查看/编辑商品详情(" + id + ");");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "product", "JSON")]
        public async Task<IActionResult> EditProduct(Product model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            Product entity = Product.Find(Product._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (model.KId <= 0)
            {
                tip.Message = "请选择一个商品分类";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "请填写商品标题";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                tip.Message = "请填写商品介绍";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Pic) && !Utils.IsImgFilename(model.Pic))
            {
                tip.Message = "商品图片填写的不是图片格式！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            if (!CanAccessProduct(my, entity.KId, entity.AuthorId, out var deniedReason) ||
                !CanAccessProduct(my, model.KId, authorId: null, out deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                        morIMG += s + "|||";
                }
            }

            model.IsNew = Request.Form["IsNew"] == "1" ? 1 : 0;
            model.IsRecommend = Request.Form["IsRecommend"] == "1" ? 1 : 0;
            model.IsHide = Request.Form["IsHide"] == "1" ? 1 : 0;
            model.IsHotSales = Request.Form["IsHotSales"] == "1" ? 1 : 0;
            model.Hits = entity.Hits;
            model.UpdateTime = DateTime.Now;
            model.ItemImg = morIMG;

            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = await _remoteImages.ImportAsync(content, HttpContext.RequestAborted);
                model.Content = content;
            }
            model.Content = _contentSanitizer.Sanitize(model.Content);

            entity.SupportId = model.SupportId;
            entity.BannerImg = model.BannerImg;
            entity.BId = model.BId;
            entity.Color = model.Color;
            entity.Content = model.Content;
            entity.Credits = model.Credits;
            entity.Description = model.Description;
            entity.Discount = model.Discount;
            entity.Fare = model.Fare;
            entity.FilePath = model.FilePath;
            entity.Front = model.Front;
            entity.Hits = model.Hits;
            entity.Icon = model.Icon;
            entity.IsBest = model.IsBest;
            entity.IsBreakup = model.IsBreakup;
            entity.IsComment = model.IsComment;
            entity.IsGift = model.IsGift;
            entity.IsHide = model.IsHide;
            entity.IsHotSales = model.IsHotSales;
            entity.IsLock = model.IsLock;
            entity.IsMember = model.IsMember;
            entity.IsNew = model.IsNew;
            entity.IsPart = model.IsPart;
            entity.IsPromote = model.IsPromote;
            entity.IsRecommend = model.IsRecommend;
            entity.IsShelves = model.IsShelves;
            entity.IsSpecial = model.IsSpecial;
            entity.IsSubProduct = model.IsSubProduct;
            entity.IsTop = model.IsTop;
            entity.ItemImg = model.ItemImg;
            entity.ItemNO = model.ItemNO;
            entity.Keyword = model.Keyword;
            entity.KId = model.KId;
            entity.LinkURL = model.LinkURL;
            entity.MarketPrice = model.MarketPrice;
            entity.Material = model.Material;
            entity.Parameters = model.Parameters;
            entity.Pic = model.Pic;
            entity.PPId = model.PPId;
            entity.Price = model.Price;
            entity.Sequence = model.Sequence;
            entity.Service = model.Service;
            entity.ShopId = model.ShopId;
            entity.Spec = model.Spec;
            entity.SpecialPrice = model.SpecialPrice;
            entity.Stock = model.Stock;
            entity.SubTitle = model.SubTitle;
            entity.Tags = model.Tags;
            entity.TemplateFile = model.TemplateFile;
            entity.Title = model.Title;
            entity.TitleColor = model.TitleColor;
            entity.Unit = model.Unit;
            entity.UpdateTime = DateTime.Now;
            entity.WarnStock = model.WarnStock;
            entity.Weight = model.Weight;

            entity.Update();
            Core.Admin.WriteLogActions("编辑商品(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑商品成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 删除商品
        [HttpPost]
        [MyAuthorize("del", "product", "JSON")]
        public JsonResult DelProduct(int id)
        {
            Product entity = Product.Find(Product._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本商品！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            if (!CanAccessProduct(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            int kid = entity.KId;
            Core.Admin.WriteLogActions("删除商品(id:" + entity.Id + ");");
            entity.Delete();
            Category.UpdateDetailCount(kid);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除商品成功";
            return Json(tip);
        }
        #endregion

        #region 权限与校验辅助方法
        internal static bool CanAccessProduct(Admin admin, int categoryId, int? authorId, out string reason)
        {
            reason = null;
            if (admin?.Roles == null)
            {
                reason = "当前登录状态无效！";
                return false;
            }
            if (admin.Roles.IsSuperAdmin == 1) return true;

            var aclist = ParseAuthorizedIds(admin.Roles.AuthorizedCagegory);
            if (aclist.Count > 0 && !aclist.Contains(categoryId))
            {
                reason = "当前选择栏目不存在，或者您没有该栏目的操作权限！";
                return false;
            }
            if (authorId.HasValue && admin.Roles.OnlyEditMyselfProduct == 1 && authorId.Value != admin.Id)
            {
                reason = "系统限制，您无法操作非自己添加的商品！";
                return false;
            }
            return true;
        }

        private static List<int> ParseAuthorizedIds(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return new List<int>();
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
            }
            catch (JsonException)
            {
                return new List<int>();
            }
        }

        private static bool ValidateCategoryFilePath(string filePath, int excludeId, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(filePath)) return true;
            if (!filePath.StartsWith("/", StringComparison.Ordinal))
            {
                error = "栏目路径请以/开头！";
                return false;
            }
            if (filePath.EndsWith("/", StringComparison.Ordinal))
            {
                error = "栏目路径结尾不用加上/";
                return false;
            }
            if (filePath.Count(x => x == '/') > 4)
            {
                error = "最多只能四级路径！";
                return false;
            }
            if (!AdminUtils.CheckFilePathIsOK(filePath, excludeId, 1))
            {
                error = "栏目路径不可用，请重新填写！";
                return false;
            }
            return true;
        }

        private static readonly Lazy<List<string>> CachedProductTemplates = new(() =>
        {
            var listTpls = new List<string>();
            foreach (var asmItem in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asmItem.GetName().Name?.Contains("COMCMS.Web", StringComparison.OrdinalIgnoreCase) != true) continue;
                foreach (var type in asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Product", StringComparison.Ordinal)))
                {
                    listTpls.Add(type.Name.Replace("Views_Product_", "") + ".cshtml");
                }
            }
            return listTpls;
        });

        private static List<string> GetProductTemplateNames() => CachedProductTemplates.Value;
        #endregion
    }
}
