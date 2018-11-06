using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region 商品栏目
        /// <summary>
        /// 商品栏目列表
        /// </summary>
        /// <returns></returns>
        [MyAuthorize("viewlist", "category")]
        public IActionResult CategoryList()
        {
            IList<Category> list = Category.GetListTree(0, -1, false, true);
            Core.Admin.WriteLogActions("查看商品栏目列表页面；");
            return View(list);
        }

        [MyAuthorize("add", "category")]
        public IActionResult AddCategory()
        {
            //获取上级栏目
            IList<Category> list = Category.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            //获取模板
            //List<string> listtpl = COMCMS.Common.IOHelper.GetDirFiles(new DirectoryInfo(Server.MapPath("~/Views/article")));
            //ViewBag.ListTpl = listtpl;
            Core.Admin.WriteLogActions("查看添加商品栏目页面；");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "category", "JSON")]
        public IActionResult AddCategory(Category model)
        {
            //获取上级栏目
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "商品栏目标题不能为空！";
                return Json(tip);
            }
            model.Insert();
            Core.Admin.WriteLogActions("添加商品栏目(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加商品栏目成功";
            tip.ReturnUrl = "close";

            return Json(tip);
        }

        //编辑栏目
        [MyAuthorize("edit", "category")]
        public IActionResult EditCategory(int id)
        {
            Category entity = Category.Find(Category._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            //获取上级栏目
            IList<Category> list = Category.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            //获取模板
            //List<string> listtpl = COMCMS.Common.IOHelper.GetDirFiles(new DirectoryInfo(Server.MapPath("~/Views/article")));
            //ViewBag.ListTpl = listtpl;
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
                Json(tip);
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
                Json(tip);
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
                //修改商品的location
                IList<Product> alist = Product.FindAll(Product._.KId == model.Id, null, null, 0, 0);
                if (alist != null && alist.Count > 0)
                {
                    foreach (var a in alist)
                    {
                        a.Location = entity.Location + "," + entity.Id;
                        //a.FilePath = entity.FilePath;
                    }
                    alist.Save();
                }
            }
            //赋值
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
            entity.KindInfo = model.KindInfo;
            entity.KindDomain = model.KindDomain;
            entity.Rank = model.Rank;
            entity.Pic = model.Pic;


            entity.Save();
            Core.Admin.WriteLogActions("修改商品栏目(id:" + entity.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改商品栏目成功";
            tip.ReturnUrl = "close";

            return Json(tip);
        }

        //删除栏目
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
            //删除商品
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
        #endregion

        #region 商品列表
        [MyAuthorize("viewlist", "product")]
        public IActionResult ProductList()
        {
            //获取上级栏目
            IList<Category> list = Category.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看商品列表;");
            return View();
        }

        [MyAuthorize("viewlist", "product", "JSON")]
        public IActionResult GetProductList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Product._.Id > 0;

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
                //获取栏目
                if (isshowsub == "1")
                {
                    List<int> kids = new List<int>();
                    kids.Add(int.Parse(kid));
                    IList<Category> subkinds = Category.FindByParentID(int.Parse(kid));
                    if (subkinds != null && subkinds.Count > 0)
                    {
                        foreach (var item in subkinds)
                        {
                            kids.Add(item.Id);
                        }
                        ex &= Product._.KId.In(kids);
                    }
                }
                else
                {
                    ex &= Product._.KId == int.Parse(kid);
                }
            }

            IList<Product> list = Product.FindAll(ex, Product._.Sequence.Asc().And(Product._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Product.FindCount(ex, Product._.Sequence.Asc().And(Product._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加商品
        [HttpGet]
        [MyAuthorize("add", "product")]
        public IActionResult AddProduct()
        {
            IList<Category> list = Category.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看添加商品页面;");
            Product entity = new Product();
            entity.AddTime = DateTime.Now;
            entity.Sequence = 999;
            return View(entity);
        }

        //执行添加商品
        [HttpPost]
        [MyAuthorize("edit", "product", "JSON")]
        public IActionResult AddProduct(Product model)
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
            //处理商品更多图片
            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;//更多图片的时候用到
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                    {
                        morIMG += s + "|||";//使用“|||”分隔图片
                    }
                }
            }
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = ThumbnailHelper.SaveRemoteImgForContent(content);
                model.Content = content;
            }
            model.ItemImg = morIMG;
            model.AuthorId = Core.Admin.GetMyInfo().Id;
            model.Insert();
            Category.UpdateDetailCount(model.KId);
            //添加TAG
            //Tag.InsertTags(model.Tags, RTType.RatuoModule.Product, model.Id, model.Title);
            Core.Admin.WriteLogActions("添加商品(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加商品成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 修改商品详情
        //编辑文章
        [HttpGet]
        [MyAuthorize("edit", "product")]
        public IActionResult EditProduct(int id)
        {
            IList<Category> list = Category.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;

            Product entity = Product.Find(Product._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin.WriteLogActions("查看/编辑商品详情(" + id + ");");

            return View(entity);
        }

        //执行添加商品
        [HttpPost]
        [MyAuthorize("add", "product", "JSON")]
        public IActionResult EditProduct(Product model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                Json(tip);
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
            //处理商品更多图片
            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;//更多图片的时候用到
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                    {
                        morIMG += s + "|||";//使用“|||”分隔图片
                    }
                }
            }
            //处理三个勾选属性的
            string isnew = Request.Form["IsNew"];
            model.IsNew = isnew == "1" ? 1 : 0;


            string IsRecommend = Request.Form["IsRecommend"];
            model.IsRecommend = IsRecommend == "1" ? 1 : 0;

            string IsHide = Request.Form["IsHide"];
            model.IsHide = IsHide == "1" ? 1 : 0;
            model.IsHotSales = Request.Form["IsHotSales"] == "1" ? 1 : 0;
            model.Hits = entity.Hits;
            model.UpdateTime = DateTime.Now;

            model.ItemImg = morIMG;
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = ThumbnailHelper.SaveRemoteImgForContent(content);
                model.Content = content;
            }
            //model.AuthorId = Core.Admin.GetMyInfo().Id;
            model.Save();
            //添加TAG
            //Tag.InsertTags(model.Tags, RTType.RatuoModule.Product, model.Id, model.Title);
            Core.Admin.WriteLogActions("编辑商品(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑商品成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 删除商品
        //删除文章
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
            int kid = entity.KId;
            //删除TAG
            //Tag.DeleteTag(RTType.RatuoModule.Article, entity.Id);
            Core.Admin.WriteLogActions("删除商品(id:" + entity.Id + ");");
            entity.Delete();
            //更新栏目商品数量
            Category.UpdateDetailCount(kid);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除商品成功";
            return Json(tip);
        }
        #endregion
    }
}