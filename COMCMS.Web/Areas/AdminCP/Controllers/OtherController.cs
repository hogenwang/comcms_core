using System;
using System.Collections.Generic;
using System.ComponentModel;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("其它")]
    public class OtherController : AdminBaseController
    {
        #region 广告分类管理
        [MyAuthorize("viewlist", "adskinds")]
        [DisplayName("广告分类")]
        public IActionResult AdsCategoryList()
        {
            IList<AdsKind> list = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看广告分类列表页面；");
            return View(list);
        }

        [HttpPost]
        [MyAuthorize("add", "adskinds", "JSON")]
        public IActionResult AddAdsKind(AdsKind model)
        {
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "广告分类名称不能为空！";
                return Json(tip);
            }
            model.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加广告分类成功";
            Core.Admin.WriteLogActions("添加广告栏目(" + model.Id + ")；");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("edit", "adskinds", "JSON")]
        public IActionResult EditAdsKind(AdsKind model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            AdsKind oldentity = AdsKind.Find(AdsKind._.Id == model.Id);
            if (oldentity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "广告分类名称不能为空！";
                return Json(tip);
            }
            oldentity.KindName = model.KindName;
            oldentity.KindInfo = model.KindInfo;
            oldentity.Rank = model.Rank;
            oldentity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑广告分类成功";
            Core.Admin.WriteLogActions("编辑广告栏目(" + oldentity.Id + ")；");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "adskinds", "JSON")]
        public JsonResult DelAdsKind(int id)
        {
            AdsKind entity = AdsKind.Find(AdsKind._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本广告分类！";
                return Json(tip);
            }
            IList<Ads> list = Ads.FindAll(Ads._.KId == entity.Id, null, null, 0, 0);
            if (list != null && list.Count > 0)
            {
                list.Delete();
            }
            Core.Admin.WriteLogActions("删除广告分类(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除广告分类成功";
            return Json(tip);
        }
        #endregion

        #region 广告管理
        [MyAuthorize("viewlist", "ads")]
        [DisplayName("广告列表")]
        public IActionResult AdsList()
        {
            ViewBag.ListKinds = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看广告列表;");
            return View();
        }

        [MyAuthorize("viewlist", "ads", "JSON")]
        public IActionResult GetAdsList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

            Expression ex = Ads._.Id > 0;
            if (kid > 0)
                ex &= Ads._.KId == kid;
            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Ads._.Title.Contains(keyword);

            IList<Ads> list = Ads.FindAll(ex, Ads._.Sequence.Asc().And(Ads._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Ads.FindCount(ex, Ads._.Sequence.Asc().And(Ads._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "application/json");
        }

        [MyAuthorize("add", "ads")]
        public IActionResult AddAds()
        {
            ViewBag.ListKinds = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看添加广告页面;");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "ads", "JSON")]
        public IActionResult AddAds(Ads model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "广告标题不能为空！";
                return Json(tip);
            }
            if (!TryBuildAdsContent(model.TId, out var content, out var error))
            {
                tip.Message = error;
                return Json(tip);
            }

            model.StartTime = new DateTime(2000, 1, 1);
            model.EndTime = new DateTime(2999, 1, 1);
            model.Content = content;
            model.Insert();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加广告成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("添加广告(" + model.Id + ");");
            return Json(tip);
        }

        [MyAuthorize("edit", "ads")]
        public IActionResult EditAds(int id)
        {
            ViewBag.ListKinds = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            Ads entity = Ads.Find(Ads._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Core.Admin.WriteLogActions("查看/编辑广告(" + id + ");");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "ads", "JSON")]
        public IActionResult EditAds(Ads model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "广告标题不能为空！";
                return Json(tip);
            }
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            Ads entity = Ads.Find(Ads._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (!TryBuildAdsContent(model.TId, out var content, out var error))
            {
                tip.Message = error;
                return Json(tip);
            }

            entity.Title = model.Title;
            entity.KId = model.KId;
            entity.TId = model.TId;
            entity.IsDisabled = model.IsDisabled;
            entity.Sequence = model.Sequence;
            entity.StartTime = new DateTime(2000, 1, 1);
            entity.EndTime = new DateTime(2999, 1, 1);
            entity.Content = content;
            entity.Update();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑广告成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("编辑广告(" + entity.Id + ");");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "ads", "JSON")]
        public JsonResult DelAds(int id)
        {
            Ads entity = Ads.Find(Ads._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本广告！";
                return Json(tip);
            }
            Core.Admin.WriteLogActions("删除广告(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除广告成功";
            return Json(tip);
        }

        private bool TryBuildAdsContent(int tid, out string content, out string error)
        {
            content = "";
            error = null;
            if (tid == 3)
            {
                error = "Flash 广告已因安全原因禁用，请改用图片或视频。";
                return false;
            }

            switch (tid)
            {
                case 0: // 代码广告仅超级管理员可配置，降低存储型 XSS 风险
                {
                    Core.Admin caller = Core.Admin.GetMyInfo();
                    if (caller?.Roles?.IsSuperAdmin != 1)
                    {
                        error = "代码类型广告仅允许超级管理员编辑！";
                        return false;
                    }
                    ScriptAds script = new ScriptAds { content = Request.Form["txtScript"] };
                    if (string.IsNullOrWhiteSpace(script.content))
                    {
                        error = "代码不能为空！";
                        return false;
                    }
                    content = JsonConvert.SerializeObject(script);
                    return true;
                }
                case 1: // 文字
                {
                    TextAds text = new TextAds
                    {
                        txt = Request.Form["txt_Txt"],
                        link = Request.Form["txt_Link"],
                        style = Request.Form["txt_Style"]
                    };
                    if (string.IsNullOrWhiteSpace(text.txt))
                    {
                        error = "文字内容不能为空！";
                        return false;
                    }
                    if (!IsSafeHttpOrRelativeUrl(text.link))
                    {
                        error = "文字链接必须为合法的 http/https 或站内相对路径！";
                        return false;
                    }
                    content = JsonConvert.SerializeObject(text);
                    return true;
                }
                case 2: // 图片
                {
                    ImgAds img = new ImgAds
                    {
                        img = Request.Form["img_Img"],
                        link = Request.Form["img_Link"],
                        alt = Request.Form["img_Alt"]
                    };
                    if (int.TryParse(Request.Form["img_Width"], out var iw)) img.width = iw;
                    if (int.TryParse(Request.Form["img_Height"], out var ih)) img.height = ih;
                    if (!IsSafeHttpOrRelativeUrl(img.img))
                    {
                        error = "图片地址不合法！";
                        return false;
                    }
                    if (!IsSafeHttpOrRelativeUrl(img.link))
                    {
                        error = "图片链接必须为合法的 http/https 或站内相对路径！";
                        return false;
                    }
                    content = JsonConvert.SerializeObject(img);
                    return true;
                }
                case 4: // 幻灯片
                {
                    if (!int.TryParse(Request.Form["slide_Width"], out var sw))
                    {
                        error = "幻灯片宽度不能为空或者不是整数！";
                        return false;
                    }
                    if (!int.TryParse(Request.Form["slide_Height"], out var sh))
                    {
                        error = "幻灯片高度不能为空或者不是整数！";
                        return false;
                    }
                    string[] arrsimg = Request.Form["slide_Img"];
                    string[] arrslink = Request.Form["slide_Link"];
                    string[] arralt = Request.Form["slide_Alt"];
                    List<ImgAds> listimg = new List<ImgAds>();
                    int count = Math.Min(arrsimg?.Length ?? 0, arrslink?.Length ?? 0);
                    for (int i = 0; i < count; i++)
                    {
                        if (!string.IsNullOrEmpty(arrsimg[i]) && !string.IsNullOrEmpty(arrslink[i]))
                        {
                            if (!IsSafeHttpOrRelativeUrl(arrsimg[i]) || !IsSafeHttpOrRelativeUrl(arrslink[i]))
                            {
                                error = "幻灯片图片地址或跳转链接不合法！";
                                return false;
                            }
                            listimg.Add(new ImgAds
                            {
                                img = arrsimg[i],
                                link = arrslink[i],
                                height = sh,
                                width = sw,
                                alt = (arralt != null && i < arralt.Length) ? arralt[i] : string.Empty
                            });
                        }
                    }
                    if (listimg.Count == 0)
                    {
                        error = "幻灯片请至少添加一张图片！";
                        return false;
                    }
                    content = JsonConvert.SerializeObject(listimg);
                    return true;
                }
                case 7: // 视频广告
                {
                    VideoAds video = new VideoAds
                    {
                        poster = Request.Form["video_Poster"],
                        src = Request.Form["video_Src"],
                        title = Request.Form["video_Title"]
                    };
                    if (int.TryParse(Request.Form["video_Width"], out var vw)) video.width = vw;
                    if (int.TryParse(Request.Form["video_Height"], out var vh)) video.height = vh;
                    if (!IsSafeHttpOrRelativeUrl(video.src))
                    {
                        error = "视频广告地址不合法！";
                        return false;
                    }
                    if (!string.IsNullOrWhiteSpace(video.poster) && !IsSafeHttpOrRelativeUrl(video.poster))
                    {
                        error = "视频封面地址不合法！";
                        return false;
                    }
                    content = JsonConvert.SerializeObject(video);
                    return true;
                }
                default:
                    error = "不支持的广告类型！";
                    return false;
            }
        }

        internal static bool IsSafeHttpOrRelativeUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var trimmed = value.Trim();
            if (trimmed == "#") return true;
            if (trimmed.StartsWith("/", StringComparison.Ordinal) && !trimmed.StartsWith("//", StringComparison.Ordinal))
                return true;
            return Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
        #endregion

        #region 友情链接分类管理
        [MyAuthorize("viewlist", "linkkinds")]
        [DisplayName("友情链接分类")]
        public IActionResult LinkCategoryList()
        {
            IList<LinkKind> list = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看友情链接分类列表页面；");
            return View(list);
        }

        [HttpPost]
        [MyAuthorize("add", "linkkinds", "JSON")]
        public IActionResult AddLinkKind(LinkKind model)
        {
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "友情链接分类名称不能为空！";
                return Json(tip);
            }
            model.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加友情链接分类成功";
            Core.Admin.WriteLogActions("添加友情链接分类(" + model.Id + ")；");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("edit", "linkkinds", "JSON")]
        public IActionResult EditLinkKind(LinkKind model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            LinkKind oldentity = LinkKind.Find(LinkKind._.Id == model.Id);
            if (oldentity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "友情链接分类名称不能为空！";
                return Json(tip);
            }
            oldentity.KindName = model.KindName;
            oldentity.KindInfo = model.KindInfo;
            oldentity.Rank = model.Rank;
            oldentity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑友情链接分类成功";
            Core.Admin.WriteLogActions("编辑友情链接分类(" + oldentity.Id + ")；");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "linkkinds", "JSON")]
        public JsonResult DelLinkKind(int id)
        {
            LinkKind entity = LinkKind.Find(LinkKind._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本友情链接分类！";
                return Json(tip);
            }
            IList<Link> list = Link.FindAll(Link._.KId == entity.Id, null, null, 0, 0);
            if (list != null && list.Count > 0)
            {
                list.Delete();
            }
            Core.Admin.WriteLogActions("删除友情链接分类(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除友情链接分类成功";
            return Json(tip);
        }
        #endregion

        #region 友情链接管理
        [MyAuthorize("viewlist", "link")]
        [DisplayName("友情链接列表")]
        public IActionResult LinkList()
        {
            ViewBag.ListKinds = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看友情链接列表;");
            return View();
        }

        [MyAuthorize("viewlist", "link", "JSON")]
        public IActionResult GetLinkList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

            Expression ex = Link._.Id > 0;
            if (kid > 0)
                ex &= Link._.KId == kid;
            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Link._.Title.Contains(keyword);

            IList<Link> list = Link.FindAll(ex, Link._.Sequence.Asc().And(Link._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Link.FindCount(ex, Link._.Sequence.Asc().And(Link._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "application/json");
        }

        [MyAuthorize("add", "link")]
        public IActionResult AddLink()
        {
            ViewBag.ListKinds = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看添加友情链接页面;");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "link", "JSON")]
        public IActionResult AddLink(Link model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "网站标题不能为空！";
                return Json(tip);
            }
            if (!IsSafeHttpOrRelativeUrl(model.LinkURL))
            {
                tip.Message = "网站链接必须为合法的 http/https 或站内相对路径！";
                return Json(tip);
            }
            model.IsHide = Request.Form["IsHide"] == "1";
            model.Insert();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加友情链接成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("添加友情链接(" + model.Id + ");");
            return Json(tip);
        }

        [MyAuthorize("edit", "link")]
        public IActionResult EditLink(int id)
        {
            ViewBag.ListKinds = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            Link entity = Link.Find(Link._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Core.Admin.WriteLogActions("查看/编辑友情链接(" + id + ");");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "link", "JSON")]
        public IActionResult EditLink(Link model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "网站标题不能为空！";
                return Json(tip);
            }
            if (!IsSafeHttpOrRelativeUrl(model.LinkURL))
            {
                tip.Message = "网站链接必须为合法的 http/https 或站内相对路径！";
                return Json(tip);
            }
            Link entity = Link.Find(Link._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.Title = model.Title;
            entity.LinkURL = model.LinkURL;
            entity.KId = model.KId;
            entity.Info = model.Info;
            entity.Logo = model.Logo;
            entity.Sequence = model.Sequence;
            entity.IsHide = Request.Form["IsHide"] == "1";
            entity.Update();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑友情链接成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("编辑友情链接(" + entity.Id + ");");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "link", "JSON")]
        public JsonResult DelLink(int id)
        {
            Link entity = Link.Find(Link._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本友情链接！";
                return Json(tip);
            }
            Core.Admin.WriteLogActions("删除友情链接(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除友情链接成功";
            return Json(tip);
        }
        #endregion
    }
}
