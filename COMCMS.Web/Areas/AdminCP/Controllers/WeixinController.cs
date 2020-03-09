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
using Senparc.Weixin.MP.Entities;
using NewLife.Log;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using System.IO;
using System.Text;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("微信公众号系统")]
    [Description("微信公众号系统，包括关注自动回复、自定义菜单、关键字回复")]
    public class WeixinController : AdminBaseController
    {

        #region 自定义菜单
        //自定义菜单
        [MyAuthorize("view", "wxmenu")]
        [HttpGet]
        public IActionResult Menu()
        {

            Config cfg = Config.GetSystemConfig();
            if(string.IsNullOrEmpty(cfg.AppId) || string.IsNullOrEmpty(cfg.AppSecret))
            {
                return EchoTipPage("请先到【系统设置】> 【基本设置】 设置公众号AppId和AppSecret！");
            }
            string accessToken = "";
            try
            {
                accessToken = AccessTokenContainer.TryGetAccessToken(cfg.AppId, cfg.AppSecret);

            }
            catch (Exception ex)
            {
                return EchoTipPage(ex.Message);
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                return EchoTipPage("AccessToken 获取错误，请确认微信配置没错。或者服务器没加入白名单！");
            }
            //XTrace.WriteLine("当前AccessToken：" + accessToken);
            //获取菜单
            GetMenuResult result = new GetMenuResult(new ButtonGroup());
            try
            {
                result = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu(accessToken);
                if (result == null)
                {
                    result = new GetMenuResult(new ButtonGroup());
                }
            }
            catch (Exception ex)
            {
                result = new GetMenuResult(new ButtonGroup());
                XTrace.WriteLine("获取菜单出错：" + ex.Message);
            }
            Core.Admin.WriteLogActions("查看公众号菜单;");
            return View(result);
        }
        //修改菜单
        [HttpPost]
        [MyAuthorize("edit", "wxmenu", "JSON")]
        public IActionResult DoPostMenuData(string allmenu)
        {

            Config cfg = Config.GetSystemConfig();
            string accessToken = "";
            try
            {
                accessToken = AccessTokenContainer.TryGetAccessToken(cfg.AppId, cfg.AppSecret);

            }
            catch (Exception ex)
            {
                tip.Message = ex.Message;
                return Json(tip);
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                tip.Message = "AccessToken 获取错误，请确认微信配置没错。或者服务器没加入白名单！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(allmenu))
            {
                tip.Message = "请填写自定义菜单";
                return Json(tip);
            }
            //Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenu()
            string posturl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={accessToken}";

            Stream retunStream = Utils.HttpPost(posturl, allmenu);

            StreamReader reader = new StreamReader(retunStream, Encoding.UTF8);
            string value = reader.ReadToEnd();

            reader.Close();

            XTrace.WriteLine("创建公众号菜单回传：" + value);

            dynamic rejson = Newtonsoft.Json.Linq.JToken.Parse(value) as dynamic;
            if (rejson != null && rejson.errcode == 0)
            {
                tip.Status = JsonTip.SUCCESS;
                tip.Message = "创建/修改公众号菜单成功！";
                return Json(tip);
            }
            tip.Message = rejson.errmsg;
            Core.Admin.WriteLogActions("修改公众号菜单;");
            return Json(tip);
        }
        #endregion

        #region 关注自动回复
        [HttpGet]
        [MyAuthorize("edit", "wxautoreply")]
        public IActionResult SubscribeReply()
        {

            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Flow);//获取规则
            if (ruleModel == null)
            {
                ruleModel = new WeixinRequestRule();
                ruleModel.RequestType = (int)WeixinRequestRule.XRequestType.Flow;
                ruleModel.ResponseType = (int)WeixinRequestRule.XResponseType.Text;
                ruleModel.RuleName = "关注自动回复";
                ruleModel.IsDefault = 1;
                ruleModel.Insert();
                //增加Response
                WeixinRequestContent content = new WeixinRequestContent()
                {
                    RuleId = ruleModel.Id,
                    Content = "关注公众号自动回复文本"

                };
                content.Insert();
            }
            switch (ruleModel.ResponseType)
            {
                case 0://文本
                    ViewBag.txtContent = ruleModel.ListContent[0].Content;
                    break;
                case 1://多图
                    break;
                case 2://语音
                    ViewBag.txtSoundTitle = ruleModel.ListContent[0].Title;
                    ViewBag.txtSoundUrl = ruleModel.ListContent[0].MediaURL;
                    ViewBag.txtSoundContent = ruleModel.ListContent[0].Content;
                    break;
                case 8://单图
                    ViewBag.txtSingleImage = ruleModel.ListContent[0].ImgURL;
                    break;
            }
            Admin.WriteLogActions("查看公众号关注自动回复;");
            return View(ruleModel);
        }
        //编辑自动回复规则
        [HttpPost]
        [MyAuthorize("edit", "wxautoreply", "JSON")]
        public IActionResult SubscribeReply(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Flow);//获取规则
            model.ResponseType = entity.ResponseType;

            model.IsDefault = 1;
            int defaultDetailId = int.Parse(Request.Form["hdfTID"]);//默认一条的ID。肯定至少有一条
            int responseType = model.ResponseType;
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim(), Id = defaultDetailId});
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Id = defaultDetailId, Content = txtSoundContent });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim(), Id = defaultDetailId});
            }
            model.Save();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();
            tip.Message = "修改成功！";
            tip.Status = JsonTip.SUCCESS;
            //tip.ReturnUrl = "reload";
            Core.Admin.WriteLogActions("修改公众号关注自动回复;");
            return Json(tip);
        }
        #endregion

        #region 自定义回复规则列表
        //自定义规则
        [MyAuthorize("viewlist", "wxkeywordreply")]
        public IActionResult ReplyRule()
        {
            return View();
        }
        //自定义规则列表
        [MyAuthorize("viewlist", "wxkeywordreply")]
        public IActionResult GetReplyRuleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (WeixinRequestRule._.Id == int.Parse(keyword) | WeixinRequestRule._.Keywords.Contains(keyword));
                }
                else
                {
                    ex &= WeixinRequestRule._.Keywords.Contains(keyword);
                }
            }

            IList<WeixinRequestRule> list = WeixinRequestRule.FindAll(ex, null, null, startRowIndex, numPerPage);
            long totalCount = WeixinRequestRule.FindCount(ex, null, null, startRowIndex, numPerPage);
            Admin.WriteLogActions($"查看公众号自定义回复规则({page});");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }
        #endregion

        #region 添加自定义规则
        [MyAuthorize("add", "wxkeywordreply")]
        public IActionResult AddTextReplyRule()
        {
            WeixinRequestRule model = new WeixinRequestRule();
            Core.Admin.WriteLogActions("查看添加自定义回复规则;");
            return View(model);
        }

        //执行添加操作
        [HttpPost]
        [MyAuthorize("add",  "wxkeywordreply", "JSON")]
        public IActionResult AddTextReplyRule(WeixinRequestRule model)
        {

            model.IsDefault = 1;

            int responseType = model.ResponseType;
            model.RequestType = (int)WeixinRequestRule.XRequestType.Text;//文本请求
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim() });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Content = txtSoundContent});
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim() });
            }
            model.Insert();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();

            tip.Message = "添加文本关键字回复规则成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("添加文本关键字回复规则;");
            return Json(tip);
        }
        #endregion

        #region 修改文本回复详情
        [MyAuthorize("edit",  "wxkeywordreply")]
        public IActionResult EditTextReplyRule()
        {
            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text);//获取规则
            if (ruleModel == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            switch (ruleModel.ResponseType)
            {
                case 0://文本
                    ViewBag.txtContent = ruleModel.ListContent[0].Content;
                    break;
                case 1://多图
                    break;
                case 2://语音
                    ViewBag.txtSoundTitle = ruleModel.ListContent[0].Title;
                    ViewBag.txtSoundUrl = ruleModel.ListContent[0].MediaURL;
                    ViewBag.txtSoundContent = ruleModel.ListContent[0].Content;
                    break;
                case 8://单图
                    ViewBag.txtSingleImage = ruleModel.ListContent[0].ImgURL;
                    break;
            }
            Core.Admin.WriteLogActions($"查看文本回复规则;");
            return View(ruleModel);
        }
        [HttpPost]
        [MyAuthorize( "edit", "wxkeywordreply",  "JSON")]
        public IActionResult EditTextReplyRule(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text & WeixinRequestRule._.Id == entity.Id);//获取规则

            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.Keywords = entity.Keywords;
            model.IsLikeQuery = entity.IsLikeQuery;
            model.IsDefault = 1;
            int defaultDetailId = int.Parse(Request.Form["hdfTID"]);//默认一条的ID。肯定至少有一条
            int responseType = model.ResponseType;
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim(), Id = defaultDetailId });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Id = defaultDetailId, Content = txtSoundContent });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim(), Id = defaultDetailId });
            }
            model.Save();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();

            tip.Message = "修改文本自动回复成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions("修改文本自动回复成;");
            return Json(tip);
        }
        #endregion

        #region 删除关键字回复规则
        [HttpPost]
        [MyAuthorize("del", "wxkeywordreply", "JSON")]
        public IActionResult DelTextReplyRule(int id)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text & WeixinRequestRule._.Id == id);//获取规则
            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            Admin.WriteLogActions("删除关键字回复规则(id:" + model.Id + ");");
            model.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除规则成功";
            return Json(tip);
        }
        #endregion

        #region 公众号点击事件回复规则
        //自定义规则
        [MyAuthorize("viewlist", "clickreplyrule")]
        public ActionResult ClickReplyRule()
        {
            return View();
        }
        //自定义规则列表
        [MyAuthorize("viewlist", "clickreplyrule")]
        public ActionResult GetClickReplyRuleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu;//点击自定义菜单

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (WeixinRequestRule._.Id == int.Parse(keyword) | WeixinRequestRule._.Keywords.Contains(keyword));
                }
                else
                {
                    ex &= WeixinRequestRule._.Keywords.Contains(keyword);
                }
            }
            IList<WeixinRequestRule> list = WeixinRequestRule.FindAll(ex, null, null, startRowIndex, numPerPage);
            long totalCount = WeixinRequestRule.FindCount(ex, null, null, startRowIndex, numPerPage);
            Admin.WriteLogActions($"查看公众号点击事件自定义回复规则({page});");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }
        #endregion

        #region 添加自定义规则
        [MyAuthorize( "add", "clickreplyrule")]
        public IActionResult AddClickReplyRule()
        {
            WeixinRequestRule model = new WeixinRequestRule();
            model.RequestType = (int)WeixinRequestRule.XRequestType.CustomMenu;//文本请求
            Core.Admin.WriteLogActions("查看添加点击菜单事件回复规则;");
            return View(model);
        }

        //执行添加操作
        [HttpPost]
        [MyAuthorize( "add",  "clickreplyrule", "JSON")]
        public IActionResult AddClickReplyRule(WeixinRequestRule model)
        {
            model.IsDefault = 1;

            int responseType = model.ResponseType;
            model.RequestType = (int)WeixinRequestRule.XRequestType.CustomMenu;//文本请求
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim() });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Content = txtSoundContent });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim() });
            }
            model.Insert();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();

            tip.Message = "添加自定义菜单点击事件回复规则成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions("添加自定义菜单点击事件回复规则;");
            return Json(tip);
        }
        #endregion

        #region 修改文点击回复详情
        [MyAuthorize("edit", "clickreplyrule")]
        public IActionResult EditClickReplyRule()
        {

            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu);//获取规则
            if (ruleModel == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            switch (ruleModel.ResponseType)
            {
                case 0://文本
                    ViewBag.txtContent = ruleModel.ListContent[0].Content;
                    break;
                case 1://多图
                    break;
                case 2://语音
                    ViewBag.txtSoundTitle = ruleModel.ListContent[0].Title;
                    ViewBag.txtSoundUrl = ruleModel.ListContent[0].MediaURL;
                    ViewBag.txtSoundContent = ruleModel.ListContent[0].Content;
                    break;
                case 8://单图
                    ViewBag.txtSingleImage = ruleModel.ListContent[0].ImgURL;
                    break;
            }
            Core.Admin.WriteLogActions($"查看点击菜单事件回复规则;");
            return View(ruleModel);
        }

        [HttpPost]
        [MyAuthorize( "edit", "clickreplyrule", "JSON")]
        public IActionResult EditClickReplyRule(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu & WeixinRequestRule._.Id == entity.Id);//获取规则

            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.Keywords = entity.Keywords;
            model.IsDefault = 1;
            int defaultDetailId = int.Parse(Request.Form["hdfTID"]);//默认一条的ID。肯定至少有一条
            int responseType = model.ResponseType;
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim(), Id = defaultDetailId });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Id = defaultDetailId, Content = txtSoundContent });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim(), Id = defaultDetailId });
            }
            model.Save();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();

            tip.Message = "修改点击自定义菜单事件自动回复成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions("修改点击自定义菜单事件自动回复;");
            return Json(tip);
        }
        #endregion

        #region 未找到关键字自动回复
        [HttpGet]
        [MyAuthorize("view", "wxnotfindkeywordautoreply")]
        public ActionResult NotFindKeywordAutoReply()
        {

            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.NotFindKeyword);//获取规则
            if (ruleModel == null)
            {
                ruleModel = new WeixinRequestRule();
                ruleModel.RequestType = (int)WeixinRequestRule.XRequestType.NotFindKeyword;
                ruleModel.ResponseType = (int)WeixinRequestRule.XResponseType.Text;
                ruleModel.RuleName = "未找到关键词自动回复";
                ruleModel.IsDefault = 1;
                ruleModel.Insert();
                //增加Response
                WeixinRequestContent content = new WeixinRequestContent()
                {
                    RuleId = ruleModel.Id,
                    Content = "未找到关键词自动回复",

                };
                content.Insert();
                ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.NotFindKeyword);//重新获取规则
            }
            switch (ruleModel.ResponseType)
            {
                case 0://文本
                    ViewBag.txtContent = ruleModel.ListContent[0].Content;
                    break;
                case 1://多图
                    break;
                case 2://语音
                    ViewBag.txtSoundTitle = ruleModel.ListContent[0].Title;
                    ViewBag.txtSoundUrl = ruleModel.ListContent[0].MediaURL;
                    ViewBag.txtSoundContent = ruleModel.ListContent[0].Content;
                    break;
                case 8://单图
                    ViewBag.txtSingleImage = ruleModel.ListContent[0].ImgURL;
                    break;
            }

            Core.Admin.WriteLogActions("查看公众号未找到关键字自动回复;");
            return View(ruleModel);
        }
        //编辑自动回复规则
        [HttpPost]
        [MyAuthorize("edit", "wxnotfindkeywordautoreply", "JSON")]
        public IActionResult NotFindKeywordAutoReply(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.NotFindKeyword );//获取规则
            model.ResponseType = entity.ResponseType;

            model.IsDefault = 1;
            int defaultDetailId = int.Parse(Request.Form["hdfTID"]);//默认一条的ID。肯定至少有一条
            int responseType = model.ResponseType;
            IList<WeixinRequestContent> ls = new List<WeixinRequestContent>();
            if (responseType == 0) //纯文本
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    tip.Message = "文本回复，文本不能为空！";
                    return Json(tip);
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;//回复的类型:文本1，图文2，语音3，视频4,第三方接口5
                ls.Add(new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim(), Id = defaultDetailId });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Image) //图文
            {
                #region 赋值规则图片

                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];
                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null)
                {
                    if ((itemIdArr.Length == itemTitleArr.Length) && (itemTitleArr.Length == itemImgUrlArr.Length) && (itemImgUrlArr.Length == itemLinkUrlArr.Length))
                    {

                        for (int i = 0; i < itemIdArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemTitleArr[i]))
                            {
                                WeixinRequestContent modelt = new WeixinRequestContent();
                                modelt.Id = Utils.StrToInt(itemIdArr[i].Trim(), 0);
                                modelt.Title = itemTitleArr[i].Trim();
                                modelt.Content = itemContentArr[i].Trim();
                                modelt.ImgURL = itemImgUrlArr[i].Trim();
                                modelt.LinkUrl = itemLinkUrlArr[i].Trim();
                                modelt.RuleId = model.Id;
                                ls.Add(modelt);
                            }
                        }
                        //model.contents = ls;
                    }
                }
                #endregion
                if (ls == null || ls.Count < 1)
                {
                    tip.Message = "图文列表，图文项目不能为空！";
                    return Json(tip);
                }

            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.Video) //语音
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    tip.Message = "语音回复，语音标题不能为空！";
                    return Json(tip);
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    tip.Message = "语音回复，语音地址不能为空！";
                    return Json(tip);
                }
                string txtSoundContent = Request.Form["txtSoundContent"];

                ls.Add(new WeixinRequestContent { RuleId = model.Id, MediaURL = txtSoundUrl.Trim(), Title = txtSoundTitle, Id = defaultDetailId, Content = txtSoundContent });
            }
            else if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage) //单图片
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    tip.Message = "单图片回复，图片地址不能为空！";
                    return Json(tip);
                }
                ls.Add(new WeixinRequestContent { RuleId = model.Id, ImgURL = txtSingleImage.Trim(), Id = defaultDetailId });
            }
            model.Save();
            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    if (oldwc.Find(s => s.Id == p.Id) != null)
                    {
                        oldwc.Remove(oldwc.Find(s => s.Id == p.Id));
                    }
                }
                if (oldwc != null && oldwc.Count > 0)
                {
                    oldwc.Delete();
                }
            }
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    item.RuleId = model.Id;
                }
            }
            ls.Save();

            tip.Message = "修改成功！";
            tip.Status = JsonTip.SUCCESS;
            //tip.ReturnUrl = "reload";
            Admin.WriteLogActions("修改公众号未找到关键字自动回复;");
            return Json(tip);
        }
        #endregion


    }
}