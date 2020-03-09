using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;
using COMCMS.Core.Models;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("其它")]
    public class OtherController : AdminBaseController
    {
        #region 广告分类管理
        //查看&编辑广告分类（同一个页面执行）
        [MyAuthorize("viewlist", "adskinds")]
        [DisplayName("广告分类")]
        public IActionResult AdsCategoryList()
        {
            IList<AdsKind> list = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看广告分类列表页面；");
            return View(list);
        }

        //执行添加操作
        [HttpPost]
        [MyAuthorize("add", "adskinds", "JSON")]
        public IActionResult AddAdsKind(AdsKind model)
        {
            if (string.IsNullOrEmpty(model.KindName))
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

        //执行编辑操作
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
            if (string.IsNullOrEmpty(model.KindName))
            {
                tip.Message = "广告分类名称不能为空！";
                return Json(tip);
            }
            model.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑广告分类成功";
            Core.Admin.WriteLogActions("编辑广告栏目(" + model.Id + ")；");
            return Json(tip);
        }
        //执行删除操作
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
            //删除旗下广告
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
            IList<AdsKind> list = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看广告列表;");
            return View();
        }
        //获取分页
        [MyAuthorize("viewlist", "ads", "JSON")]
        public IActionResult GetAdsList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Ads._.Id > 0;
            if (kid > 0)
                ex &= Ads._.KId == kid;

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Ads._.Title.Contains(keyword);

            IList<Ads> list = Ads.FindAll(ex, Ads._.Sequence.Asc().And(Ads._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Ads.FindCount(ex, Ads._.Sequence.Asc().And(Ads._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        //添加广告页面
        [MyAuthorize("add", "ads")]
        public IActionResult AddAds()
        {
            IList<AdsKind> list = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看添加广告页面;");
            return View();
        }

        //执行添加广告
        [HttpPost]
        
        [MyAuthorize("add", "ads", "JSON")]
        public IActionResult AddAds(Ads model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                tip.Message = "广告标题不能为空！";
                return Json(tip);
            }
            string content = "";
            DateTime StartTime, EndTime;
            StartTime = Convert.ToDateTime("2000-01-01");
            EndTime = Convert.ToDateTime("2999-01-01");
            #region 获取数据
            switch (model.TId)
            {
                case 0://代码
                    ScriptAds script = new ScriptAds();
                    script.content = Request.Form["txtScript"];
                    if (string.IsNullOrEmpty(script.content))
                    {
                        tip.Message = "代码不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(script);
                    break;
                case 1://文字
                    TextAds text = new TextAds();
                    text.txt = Request.Form["txt_Txt"];
                    text.link = Request.Form["txt_Link"];
                    text.style = Request.Form["txt_Style"];
                    if (string.IsNullOrEmpty(text.txt))
                    {
                        tip.Message = "文字内容不能为空！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(text.link))
                    {
                        tip.Message = "文字链接不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(text);
                    break;
                case 2://图片
                    ImgAds img = new ImgAds();
                    img.img = Request.Form["img_Img"];
                    img.link = Request.Form["img_Link"];
                    img.alt = Request.Form["img_Alt"];
                    string iw = Request.Form["img_Width"];
                    string ih = Request.Form["img_Height"];
                    if (!string.IsNullOrEmpty(iw) && Utils.IsInt(iw))
                        img.width = int.Parse(iw);
                    if (!string.IsNullOrEmpty(ih) && Utils.IsInt(ih))
                        img.height = int.Parse(ih);
                    if (string.IsNullOrEmpty(img.img))
                    {
                        tip.Message = "图片地址不能为空！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(img.link))
                    {
                        tip.Message = "图片链接不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(img);
                    break;
                case 3://Flash
                    FlashAds flash = new FlashAds();
                    flash.swf = Request.Form["flash_Swf"];
                    string fw = Request.Form["flash_Width"];
                    string fh = Request.Form["flash_Height"];
                    if (string.IsNullOrEmpty(flash.swf))
                    {
                        tip.Message = "Flash 地址不能为空！";
                        return Json(tip);
                    }

                    if (string.IsNullOrEmpty(fw) || !Utils.IsInt(fw))
                    {
                        tip.Message = "Flash 宽度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    else
                    {
                        flash.width = int.Parse(fw);
                    }
                    if (string.IsNullOrEmpty(fh) || !Utils.IsInt(fh))
                    {
                        tip.Message = "Flash 高度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    else
                    {
                        flash.height = int.Parse(fh);
                    }
                    content = JsonConvert.SerializeObject(flash);
                    break;
                case 4://幻灯片
                    string sw = Request.Form["slide_Width"];
                    string sh = Request.Form["slide_Height"];
                    if (string.IsNullOrEmpty(sw) || !Utils.IsInt(sw))
                    {
                        tip.Message = "幻灯片宽度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(sh) || !Utils.IsInt(sh))
                    {
                        tip.Message = "幻灯片高度不能为空或者不是整数！";
                        return Json(tip); ;
                    }
                    string[] arrsimg = Request.Form["slide_Img"];// SImg.Split(new string[] { "," }, StringSplitOptions.None);
                    string[] arrslink = Request.Form["slide_Link"]; //SLink.Split(new string[] { "," }, StringSplitOptions.None);
                    string[] arralt = Request.Form["slide_Alt"]; //SAlt.Split(new string[] { "," }, StringSplitOptions.None);
                    List<ImgAds> listimg = new List<ImgAds>();
                    for (int i = 0; i < arrsimg.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(arrsimg[i]) && !string.IsNullOrEmpty(arrslink[i]))//图片跟地址都不为空
                        {
                            ImgAds tplimgads = new ImgAds();
                            tplimgads.img = arrsimg[i];
                            tplimgads.link = arrslink[i];
                            tplimgads.height = int.Parse(sh);
                            tplimgads.width = int.Parse(sw);
                            tplimgads.alt = arralt[i];
                            listimg.Add(tplimgads);
                        }
                    }
                    if (listimg == null || listimg.Count <= 0)
                    {
                        tip.Message = "幻灯片请至少添加一张图片！";
                        return Json(tip); ;
                    }
                    content = JsonConvert.SerializeObject(listimg);
                    break;
                case 7://视频广告
                    VideoAds video = new VideoAds();
                    video.poster = Request.Form["video_Poster"];
                    video.src = Request.Form["video_Src"];
                    video.title = Request.Form["video_Title"];
                    string vw = Request.Form["video_Width"];
                    string vh = Request.Form["video_Height"];
                    if (!string.IsNullOrEmpty(vw) && Utils.IsInt(vw))
                        video.width = int.Parse(vw);
                    if (!string.IsNullOrEmpty(vh) && Utils.IsInt(vh))
                        video.height = int.Parse(vh);
                    if (string.IsNullOrEmpty(video.src))
                    {
                        tip.Message = "视频广告地址不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(video);
                    break;
            }
            #endregion
            model.StartTime = StartTime;
            model.EndTime = EndTime;
            model.Content = content;
            model.Insert();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加广告成功";
            tip.ReturnUrl = "close";

            Core.Admin.WriteLogActions("添加广告(" + model.Id + ");");
            return Json(tip);
        }
        //编辑广告
        [MyAuthorize("edit", "ads")]
        public IActionResult EditAds(int id)
        {
            IList<AdsKind> list = AdsKind.FindAll(null, AdsKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;

            Ads entity = Ads.Find(Ads._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin.WriteLogActions("查看/编辑广告(" + id + ");");

            return View(entity);
        }

        //执行编辑广告
        [HttpPost]
        
        [MyAuthorize("edit", "ads", "JSON")]
        public IActionResult EditAds(Ads model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                tip.Message = "广告标题不能为空！";
                return Json(tip);
            }
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                Json(tip);
            }
            Ads entity = Ads.Find(Ads._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }

            string content = "";
            DateTime StartTime, EndTime;
            StartTime = Convert.ToDateTime("2000-01-01");
            EndTime = Convert.ToDateTime("2999-01-01");

            #region 获取数据
            switch (model.TId)
            {
                case 0://代码
                    ScriptAds script = new ScriptAds();
                    script.content = Request.Form["txtScript"];
                    if (string.IsNullOrEmpty(script.content))
                    {
                        tip.Message = "代码不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(script);
                    break;
                case 1://文字
                    TextAds text = new TextAds();
                    text.txt = Request.Form["txt_Txt"];
                    text.link = Request.Form["txt_Link"];
                    text.style = Request.Form["txt_Style"];
                    if (string.IsNullOrEmpty(text.txt))
                    {
                        tip.Message = "文字内容不能为空！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(text.link))
                    {
                        tip.Message = "文字链接不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(text);
                    break;
                case 2://图片
                    ImgAds img = new ImgAds();
                    img.img = Request.Form["img_Img"];
                    img.link = Request.Form["img_Link"];
                    img.alt = Request.Form["img_Alt"];
                    string iw = Request.Form["img_Width"];
                    string ih = Request.Form["img_Height"];
                    if (!string.IsNullOrEmpty(iw) && Utils.IsInt(iw))
                        img.width = int.Parse(iw);
                    if (!string.IsNullOrEmpty(ih) && Utils.IsInt(ih))
                        img.height = int.Parse(ih);
                    if (string.IsNullOrEmpty(img.img))
                    {
                        tip.Message = "图片地址不能为空！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(img.link))
                    {
                        tip.Message = "图片链接不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(img);
                    break;
                case 3://Flash
                    FlashAds flash = new FlashAds();
                    flash.swf = Request.Form["flash_Swf"];
                    string fw = Request.Form["flash_Width"];
                    string fh = Request.Form["flash_Height"];
                    if (string.IsNullOrEmpty(flash.swf))
                    {
                        tip.Message = "Flash 地址不能为空！";
                        return Json(tip);
                    }

                    if (string.IsNullOrEmpty(fw) || !Utils.IsInt(fw))
                    {
                        tip.Message = "Flash 宽度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    else
                    {
                        flash.width = int.Parse(fw);
                    }
                    if (string.IsNullOrEmpty(fh) || !Utils.IsInt(fh))
                    {
                        tip.Message = "Flash 高度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    else
                    {
                        flash.height = int.Parse(fh);
                    }
                    content = JsonConvert.SerializeObject(flash);
                    break;
                case 4://幻灯片
                    string sw = Request.Form["slide_Width"];
                    string sh = Request.Form["slide_Height"];
                    if (string.IsNullOrEmpty(sw) || !Utils.IsInt(sw))
                    {
                        tip.Message = "幻灯片宽度不能为空或者不是整数！";
                        return Json(tip);
                    }
                    if (string.IsNullOrEmpty(sh) || !Utils.IsInt(sh))
                    {
                        tip.Message = "幻灯片高度不能为空或者不是整数！";
                        return Json(tip); ;
                    }
                    string[] arrsimg = Request.Form["slide_Img"];// SImg.Split(new string[] { "," }, StringSplitOptions.None);
                    string[] arrslink = Request.Form["slide_Link"]; //SLink.Split(new string[] { "," }, StringSplitOptions.None);
                    string[] arralt = Request.Form["slide_Alt"]; //SAlt.Split(new string[] { "," }, StringSplitOptions.None);
                    List<ImgAds> listimg = new List<ImgAds>();
                    for (int i = 0; i < arrsimg.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(arrsimg[i]) && !string.IsNullOrEmpty(arrslink[i]))//图片跟地址都不为空
                        {
                            ImgAds tplimgads = new ImgAds();
                            tplimgads.img = arrsimg[i];
                            tplimgads.link = arrslink[i];
                            tplimgads.height = int.Parse(sh);
                            tplimgads.width = int.Parse(sw);
                            tplimgads.alt = arralt[i];
                            listimg.Add(tplimgads);
                        }
                    }
                    if (listimg == null || listimg.Count <= 0)
                    {
                        tip.Message = "幻灯片请至少添加一张图片！";
                        return Json(tip); ;
                    }
                    content = JsonConvert.SerializeObject(listimg);
                    break;
                case 7://视频广告
                    VideoAds video = new VideoAds();
                    video.poster = Request.Form["video_Poster"];
                    video.src = Request.Form["video_Src"];
                    video.title = Request.Form["video_Title"];
                    string vw = Request.Form["video_Width"];
                    string vh = Request.Form["video_Height"];
                    if (!string.IsNullOrEmpty(vw) && Utils.IsInt(vw))
                        video.width = int.Parse(vw);
                    if (!string.IsNullOrEmpty(vh) && Utils.IsInt(vh))
                        video.height = int.Parse(vh);
                    if (string.IsNullOrEmpty(video.src))
                    {
                        tip.Message = "视频广告地址不能为空！";
                        return Json(tip);
                    }
                    content = JsonConvert.SerializeObject(video);
                    break;
            }
            #endregion

            model.StartTime = StartTime;
            model.EndTime = EndTime;
            model.Content = content;
            model.Update();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑广告成功";
            tip.ReturnUrl = "close";

            Core.Admin.WriteLogActions("编辑广告(" + model.Id + ");");
            return Json(tip);
        }
        //删除广告
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

        

        #endregion

        #region 友情链接分类管理
        //查看&编辑友情链接分类（同一个页面执行）
        [MyAuthorize("viewlist", "adskinds")]
        [DisplayName("友情链接分类")]
        public IActionResult LinkCategoryList()
        {
            IList<LinkKind> list = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看友情链接分类列表页面；");
            return View(list);
        }

        //执行添加操作
        [HttpPost]
        [MyAuthorize("add", "adskinds", "JSON")]
        public IActionResult AddLinkKind(LinkKind model)
        {
            if (string.IsNullOrEmpty(model.KindName))
            {
                tip.Message = "广告分类名称不能为空！";
                return Json(tip);
            }
            
            model.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加友情链接分类成功";
            Core.Admin.WriteLogActions("添加友情链接分类(" + model.Id + ")；");
            return Json(tip);
        }

        //执行编辑操作
        [HttpPost]
        [MyAuthorize("edit", "adskinds", "JSON")]
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
            if (string.IsNullOrEmpty(model.KindName))
            {
                tip.Message = "友情链接分类名称不能为空！";
                return Json(tip);
            }
            model.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑友情链接分类成功";
            Core.Admin.WriteLogActions("编辑友情链接分类(" + model.Id + ")；");
            return Json(tip);
        }
        //执行删除操作
        [HttpPost]
        [MyAuthorize("del", "adskinds", "JSON")]
        public JsonResult DelLinkKind(int id)
        {
            LinkKind entity = LinkKind.Find(LinkKind._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本友情链接分类！";
                return Json(tip);
            }
            //删除下属友情链接
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
        //查看友情链接列表
        [MyAuthorize("viewlist", "link")]
        [DisplayName("友情链接列表")]
        
        public IActionResult LinkList()
        {
            IList<LinkKind> list = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看友情链接列表;");
            return View();
        }

        [MyAuthorize("viewlist", "link", "JSON")]
        public IActionResult GetLinkList(string keyword, int page = 1, int limit = 20, int kid = 0)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Link._.Id > 0;
            if (kid > 0)
                ex &= Link._.KId == kid;

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Link._.Title.Contains(keyword);

            IList<Link> list = Link.FindAll(ex, Link._.Sequence.Asc().And(Link._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Link.FindCount(ex, Link._.Sequence.Asc().And(Link._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        //添加友情链接
        [MyAuthorize("add", "link")]
        public IActionResult AddLink()
        {
            IList<LinkKind> list = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看添加友情链接页面;");
            return View();
        }

        //执行添加友情链接
        [HttpPost]
        [MyAuthorize("add", "link", "JSON")]
        public IActionResult AddLink(Link model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                tip.Message = "网站标题不能为空！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.LinkURL))
            {
                tip.Message = "网站链接不能为空！";
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

        //编辑广告
        [MyAuthorize("edit", "link")]
        public IActionResult EditLink(int id)
        {
            IList<LinkKind> list = LinkKind.FindAll(null, LinkKind._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;

            Link entity = Link.Find(Link._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin.WriteLogActions("查看/编辑友情链接(" + id + ");");

            return View(entity);
        }


        //执行编辑广告
        [HttpPost]
        [MyAuthorize("add", "link", "JSON")]
        public IActionResult EditLink(Link model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                tip.Message = "网站标题不能为空！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.LinkURL))
            {
                tip.Message = "网站链接不能为空！";
                return Json(tip);
            }
            Link entity = Link.Find(Link._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            model.IsHide = Request.Form["IsHide"] == "1";
            model.Update();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑友情链接成功";
            tip.ReturnUrl = "close";

            Core.Admin.WriteLogActions("编辑友情链接(" + model.Id + ");");
            return Json(tip);
        }

        //删除友情链接
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