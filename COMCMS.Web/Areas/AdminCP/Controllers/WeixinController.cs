using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Mvc;
using NewLife.Log;
using Newtonsoft.Json;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("微信公众号系统")]
    [Description("微信公众号系统，包括关注自动回复、自定义菜单、关键字回复")]
    public class WeixinController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeixinController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region 自定义菜单
        [MyAuthorize("view", "wxmenu")]
        [HttpGet]
        public IActionResult Menu()
        {
            Config cfg = Config.GetSystemConfig();
            if (string.IsNullOrEmpty(cfg.AppId) || string.IsNullOrEmpty(cfg.AppSecret))
            {
                return EchoTipPage("请先到【系统设置】> 【基本设置】 设置公众号AppId和AppSecret！");
            }

            string accessToken;
            try
            {
                accessToken = AccessTokenContainer.TryGetAccessToken(cfg.AppId, cfg.AppSecret);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return EchoTipPage("获取微信 AccessToken 失败，请检查服务端日志与公众号配置！");
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                return EchoTipPage("AccessToken 获取错误，请确认微信配置没错，或者服务器已加入白名单！");
            }

            GetMenuResult result = new GetMenuResult(new ButtonGroup());
            try
            {
                result = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu(accessToken) ?? new GetMenuResult(new ButtonGroup());
            }
            catch (Exception ex)
            {
                result = new GetMenuResult(new ButtonGroup());
                XTrace.WriteException(ex);
            }

            Core.Admin.WriteLogActions("查看公众号菜单;");
            return View(result);
        }

        [HttpPost]
        [MyAuthorize("edit", "wxmenu", "JSON")]
        public async Task<IActionResult> DoPostMenuData(string allmenu)
        {
            Config cfg = Config.GetSystemConfig();
            string accessToken;
            try
            {
                accessToken = AccessTokenContainer.TryGetAccessToken(cfg.AppId, cfg.AppSecret);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                tip.Message = "获取微信 AccessToken 失败，请检查配置与服务端日志！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                tip.Message = "AccessToken 获取错误，请确认微信配置没错，或者服务器已加入白名单！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(allmenu))
            {
                tip.Message = "请填写自定义菜单";
                return Json(tip);
            }

            string posturl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={Uri.EscapeDataString(accessToken)}";
            using HttpContent content = new StringContent(allmenu);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using var re = await _httpClientFactory.CreateClient().PostAsync(posturl, content, HttpContext.RequestAborted);
            string responseString = await re.Content.ReadAsStringAsync(HttpContext.RequestAborted);
            XTrace.WriteLine("创建公众号菜单回传：" + responseString);

            dynamic rejson = string.IsNullOrWhiteSpace(responseString) ? null : Newtonsoft.Json.Linq.JToken.Parse(responseString);
            if (rejson != null && rejson.errcode == 0)
            {
                tip.Status = JsonTip.SUCCESS;
                tip.Message = "创建/修改公众号菜单成功！";
                Core.Admin.WriteLogActions("修改公众号菜单;");
                return Json(tip);
            }
            tip.Message = rejson?.errmsg != null ? (string)rejson.errmsg : "修改公众号菜单失败！";
            Core.Admin.WriteLogActions("修改公众号菜单失败;");
            return Json(tip);
        }
        #endregion

        #region 关注自动回复
        [HttpGet]
        [MyAuthorize("edit", "wxautoreply")]
        public IActionResult SubscribeReply()
        {
            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Flow);
            if (ruleModel == null)
            {
                ruleModel = new WeixinRequestRule
                {
                    RequestType = (int)WeixinRequestRule.XRequestType.Flow,
                    ResponseType = (int)WeixinRequestRule.XResponseType.Text,
                    RuleName = "关注自动回复",
                    IsDefault = 1
                };
                ruleModel.Insert();
                new WeixinRequestContent
                {
                    RuleId = ruleModel.Id,
                    Content = "关注公众号自动回复文本"
                }.Insert();
                ruleModel = WeixinRequestRule.FindById(ruleModel.Id);
            }

            PopulateRuleViewBag(ruleModel);
            Admin.WriteLogActions("查看公众号关注自动回复;");
            return View(ruleModel);
        }

        [HttpPost]
        [MyAuthorize("edit", "wxautoreply", "JSON")]
        public IActionResult SubscribeReply(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Flow);
            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.IsDefault = 1;
            int defaultDetailId = Utils.StrToInt(Request.Form["hdfTID"], 0);
            if (!TryBuildRuleContents(model, defaultDetailId, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: false);
            tip.Message = "修改成功！";
            tip.Status = JsonTip.SUCCESS;
            Core.Admin.WriteLogActions("修改公众号关注自动回复;");
            return Json(tip);
        }
        #endregion

        #region 自定义回复规则列表
        [MyAuthorize("viewlist", "wxkeywordreply")]
        public IActionResult ReplyRule()
        {
            return View();
        }

        [MyAuthorize("viewlist", "wxkeywordreply")]
        public IActionResult GetReplyRuleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 200);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                    ex &= (WeixinRequestRule._.Id == int.Parse(keyword) | WeixinRequestRule._.Keywords.Contains(keyword));
                else
                    ex &= WeixinRequestRule._.Keywords.Contains(keyword);
            }

            IList<WeixinRequestRule> list = WeixinRequestRule.FindAll(ex, WeixinRequestRule._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = WeixinRequestRule.FindCount(ex, null, null, 0, 0);
            Admin.WriteLogActions($"查看公众号自定义回复规则({currentPage});");
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

        [HttpPost]
        [MyAuthorize("add", "wxkeywordreply", "JSON")]
        public IActionResult AddTextReplyRule(WeixinRequestRule model)
        {
            model.IsDefault = 1;
            model.RequestType = (int)WeixinRequestRule.XRequestType.Text;
            if (!TryBuildRuleContents(model, 0, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: true);
            tip.Message = "添加文本关键字回复规则成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions("添加文本关键字回复规则;");
            return Json(tip);
        }
        #endregion

        #region 修改文本回复详情
        [MyAuthorize("edit", "wxkeywordreply")]
        public IActionResult EditTextReplyRule(int id = 0)
        {
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text;
            if (id > 0)
                ex &= WeixinRequestRule._.Id == id;

            WeixinRequestRule ruleModel = WeixinRequestRule.Find(ex);
            if (ruleModel == null)
                return EchoTipPage("系统找不到本记录！");

            PopulateRuleViewBag(ruleModel);
            Core.Admin.WriteLogActions($"查看文本回复规则({ruleModel.Id});");
            return View(ruleModel);
        }

        [HttpPost]
        [MyAuthorize("edit", "wxkeywordreply", "JSON")]
        public IActionResult EditTextReplyRule(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(
                WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text &
                WeixinRequestRule._.Id == entity.Id);

            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.Keywords = entity.Keywords;
            model.IsLikeQuery = entity.IsLikeQuery;
            model.IsDefault = 1;
            int defaultDetailId = Utils.StrToInt(Request.Form["hdfTID"], 0);
            if (!TryBuildRuleContents(model, defaultDetailId, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: false);
            tip.Message = "修改文本自动回复成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions($"修改文本自动回复({model.Id});");
            return Json(tip);
        }
        #endregion

        #region 删除关键字回复规则
        [HttpPost]
        [MyAuthorize("del", "wxkeywordreply", "JSON")]
        public IActionResult DelTextReplyRule(int id)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(
                WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.Text &
                WeixinRequestRule._.Id == id);
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
        [MyAuthorize("viewlist", "clickreplyrule")]
        public ActionResult ClickReplyRule()
        {
            return View();
        }

        [MyAuthorize("viewlist", "clickreplyrule")]
        public ActionResult GetClickReplyRuleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 200);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                    ex &= (WeixinRequestRule._.Id == int.Parse(keyword) | WeixinRequestRule._.Keywords.Contains(keyword));
                else
                    ex &= WeixinRequestRule._.Keywords.Contains(keyword);
            }

            IList<WeixinRequestRule> list = WeixinRequestRule.FindAll(ex, WeixinRequestRule._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = WeixinRequestRule.FindCount(ex, null, null, 0, 0);
            Admin.WriteLogActions($"查看公众号点击事件自定义回复规则({currentPage});");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }
        #endregion

        #region 添加点击事件规则
        [MyAuthorize("add", "clickreplyrule")]
        public IActionResult AddClickReplyRule()
        {
            WeixinRequestRule model = new WeixinRequestRule
            {
                RequestType = (int)WeixinRequestRule.XRequestType.CustomMenu
            };
            Core.Admin.WriteLogActions("查看添加点击菜单事件回复规则;");
            return View(model);
        }

        [HttpPost]
        [MyAuthorize("add", "clickreplyrule", "JSON")]
        public IActionResult AddClickReplyRule(WeixinRequestRule model)
        {
            model.IsDefault = 1;
            model.RequestType = (int)WeixinRequestRule.XRequestType.CustomMenu;
            if (!TryBuildRuleContents(model, 0, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: true);
            tip.Message = "添加自定义菜单点击事件回复规则成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions("添加自定义菜单点击事件回复规则;");
            return Json(tip);
        }
        #endregion

        #region 修改点击事件回复详情
        [MyAuthorize("edit", "clickreplyrule")]
        public IActionResult EditClickReplyRule(int id = 0)
        {
            Expression ex = WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu;
            if (id > 0)
                ex &= WeixinRequestRule._.Id == id;

            WeixinRequestRule ruleModel = WeixinRequestRule.Find(ex);
            if (ruleModel == null)
                return EchoTipPage("系统找不到本记录！");

            PopulateRuleViewBag(ruleModel);
            Core.Admin.WriteLogActions($"查看点击菜单事件回复规则({ruleModel.Id});");
            return View(ruleModel);
        }

        [HttpPost]
        [MyAuthorize("edit", "clickreplyrule", "JSON")]
        public IActionResult EditClickReplyRule(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(
                WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.CustomMenu &
                WeixinRequestRule._.Id == entity.Id);

            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.Keywords = entity.Keywords;
            model.IsDefault = 1;
            int defaultDetailId = Utils.StrToInt(Request.Form["hdfTID"], 0);
            if (!TryBuildRuleContents(model, defaultDetailId, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: false);
            tip.Message = "修改点击自定义菜单事件自动回复成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "close";
            Admin.WriteLogActions($"修改点击自定义菜单事件自动回复({model.Id});");
            return Json(tip);
        }
        #endregion

        #region 未找到关键字自动回复
        [HttpGet]
        [MyAuthorize("view", "wxnotfindkeywordautoreply")]
        public ActionResult NotFindKeywordAutoReply()
        {
            WeixinRequestRule ruleModel = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.NotFindKeyword);
            if (ruleModel == null)
            {
                ruleModel = new WeixinRequestRule
                {
                    RequestType = (int)WeixinRequestRule.XRequestType.NotFindKeyword,
                    ResponseType = (int)WeixinRequestRule.XResponseType.Text,
                    RuleName = "未找到关键词自动回复",
                    IsDefault = 1
                };
                ruleModel.Insert();
                new WeixinRequestContent
                {
                    RuleId = ruleModel.Id,
                    Content = "未找到关键词自动回复"
                }.Insert();
                ruleModel = WeixinRequestRule.FindById(ruleModel.Id);
            }

            PopulateRuleViewBag(ruleModel);
            Core.Admin.WriteLogActions("查看公众号未找到关键字自动回复;");
            return View(ruleModel);
        }

        [HttpPost]
        [MyAuthorize("edit", "wxnotfindkeywordautoreply", "JSON")]
        public IActionResult NotFindKeywordAutoReply(WeixinRequestRule entity)
        {
            WeixinRequestRule model = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == (int)WeixinRequestRule.XRequestType.NotFindKeyword);
            if (model == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }

            model.ResponseType = entity.ResponseType;
            model.IsDefault = 1;
            int defaultDetailId = Utils.StrToInt(Request.Form["hdfTID"], 0);
            if (!TryBuildRuleContents(model, defaultDetailId, out var ls, out var errorMessage))
            {
                tip.Message = errorMessage;
                return Json(tip);
            }

            SaveRuleAndContents(model, ls, isInsert: false);
            tip.Message = "修改成功！";
            tip.Status = JsonTip.SUCCESS;
            Admin.WriteLogActions("修改公众号未找到关键字自动回复;");
            return Json(tip);
        }
        #endregion

        #region 规则辅助方法
        private void PopulateRuleViewBag(WeixinRequestRule ruleModel)
        {
            if (ruleModel?.ListContent == null || ruleModel.ListContent.Count == 0)
                return;

            var first = ruleModel.ListContent[0];
            switch (ruleModel.ResponseType)
            {
                case 0:
                    ViewBag.txtContent = first.Content;
                    break;
                case 1:
                    break;
                case 2:
                    ViewBag.txtSoundTitle = first.Title;
                    ViewBag.txtSoundUrl = first.MediaURL;
                    ViewBag.txtSoundContent = first.Content;
                    break;
                case 8:
                    ViewBag.txtSingleImage = first.ImgURL;
                    break;
            }
        }

        private bool TryBuildRuleContents(WeixinRequestRule model, int defaultDetailId, out IList<WeixinRequestContent> contents, out string errorMessage)
        {
            contents = new List<WeixinRequestContent>();
            errorMessage = null;
            int responseType = model.ResponseType;

            if (responseType == 0)
            {
                string txtContent = Request.Form["txtContent"];
                if (string.IsNullOrEmpty(txtContent))
                {
                    errorMessage = "文本回复，文本不能为空！";
                    return false;
                }
                model.ResponseType = (int)WeixinRequestRule.XResponseType.Text;
                var item = new WeixinRequestContent { RuleId = model.Id, Content = txtContent.Trim() };
                if (defaultDetailId > 0) item.Id = defaultDetailId;
                contents.Add(item);
                return true;
            }

            if (responseType == (int)WeixinRequestRule.XResponseType.Image)
            {
                string[] itemIdArr = Request.Form["item_id"];
                string[] itemTitleArr = Request.Form["item_title"];
                string[] itemContentArr = Request.Form["item_imginfo"];
                string[] itemImgUrlArr = Request.Form["item_imgurl"];
                string[] itemLinkUrlArr = Request.Form["item_linkurl"];

                if (itemIdArr != null && itemTitleArr != null && itemImgUrlArr != null && itemLinkUrlArr != null && itemContentArr != null &&
                    itemIdArr.Length == itemTitleArr.Length &&
                    itemTitleArr.Length == itemImgUrlArr.Length &&
                    itemImgUrlArr.Length == itemLinkUrlArr.Length &&
                    itemLinkUrlArr.Length == itemContentArr.Length)
                {
                    for (int i = 0; i < itemIdArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(itemTitleArr[i]))
                        {
                            contents.Add(new WeixinRequestContent
                            {
                                Id = Utils.StrToInt(itemIdArr[i].Trim(), 0),
                                Title = itemTitleArr[i].Trim(),
                                Content = itemContentArr[i].Trim(),
                                ImgURL = itemImgUrlArr[i].Trim(),
                                LinkUrl = itemLinkUrlArr[i].Trim(),
                                RuleId = model.Id
                            });
                        }
                    }
                }

                if (contents.Count < 1)
                {
                    errorMessage = "图文列表，图文项目不能为空！";
                    return false;
                }
                return true;
            }

            if (responseType == (int)WeixinRequestRule.XResponseType.Video)
            {
                string txtSoundTitle = Request.Form["txtSoundTitle"];
                if (string.IsNullOrEmpty(txtSoundTitle))
                {
                    errorMessage = "语音回复，语音标题不能为空！";
                    return false;
                }
                string txtSoundUrl = Request.Form["txtSoundUrl"];
                if (string.IsNullOrEmpty(txtSoundUrl))
                {
                    errorMessage = "语音回复，语音地址不能为空！";
                    return false;
                }
                string txtSoundContent = Request.Form["txtSoundContent"];
                var item = new WeixinRequestContent
                {
                    RuleId = model.Id,
                    MediaURL = txtSoundUrl.Trim(),
                    Title = txtSoundTitle,
                    Content = txtSoundContent
                };
                if (defaultDetailId > 0) item.Id = defaultDetailId;
                contents.Add(item);
                return true;
            }

            if (responseType == (int)WeixinRequestRule.XResponseType.SingleImage)
            {
                string txtSingleImage = Request.Form["txtSingleImage"];
                if (string.IsNullOrEmpty(txtSingleImage))
                {
                    errorMessage = "单图片回复，图片地址不能为空！";
                    return false;
                }
                var item = new WeixinRequestContent
                {
                    RuleId = model.Id,
                    ImgURL = txtSingleImage.Trim()
                };
                if (defaultDetailId > 0) item.Id = defaultDetailId;
                contents.Add(item);
                return true;
            }

            return true;
        }

        private static void SaveRuleAndContents(WeixinRequestRule model, IList<WeixinRequestContent> ls, bool isInsert)
        {
            if (isInsert)
                model.Insert();
            else
                model.Save();

            IList<WeixinRequestContent> oldwc = model.ListContent;
            if (oldwc != null && oldwc.Count > 0)
            {
                foreach (WeixinRequestContent p in ls)
                {
                    var match = oldwc.Find(s => s.Id == p.Id);
                    if (match != null)
                        oldwc.Remove(match);
                }
                if (oldwc.Count > 0)
                    oldwc.Delete();
            }

            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                    item.RuleId = model.Id;
                ls.Save();
            }
        }
        #endregion
    }
}
