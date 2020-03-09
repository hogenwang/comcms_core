using COMCMS.Common;
using COMCMS.Core;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMCMS.API.Weixin.Common
{
    public class MessageFunction
    {
        Config siteConfig = Config.GetSystemConfig();

        #region 处理关注/取消/默认回复方法===========================
        /// <summary>
        /// 定阅事件的统一处理
        /// </summary>
        public IResponseMessageBase EventSubscribe(int type, RequestMessageEventBase requestMessage)
        {
            int accountId = GetAccountId(); //取得公众账户ID
            string EventName = "";

            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            //else if (requestMessage.EventKey != null)
            //{
            //    EventName += requestMessage.EventKey.ToString();
            //}


            if (!ExistsOriginalId(accountId, requestMessage.ToUserName))
            {
                //验证接收方是否为我们系统配置的帐号，即验证微帐号与微信原始帐号id是否一致，如果不一致，说明【1】配置错误，【2】数据来源有问题
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "none",
                    ReponseContent = "未取到关键词对应的数据",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();


                return GetResponseMessageTxtByContent(requestMessage, "验证微帐号与微信原始帐号id不一致，可能原因【1】系统配置错误，【2】非法的数据来源", accountId);
            }


            int responseType = 0, ruleId = 0;
            //int ruleId = new BLL.weixin_request_rule().GetRuleIdAndResponseType(accountId, "request_type=" + type, out responseType);
            WeixinRequestRule rule = WeixinRequestRule.Find(WeixinRequestRule._.RequestType == type);

            if (rule != null)
            {
                responseType = rule.ResponseType;
                ruleId = rule.Id;
            }

            if (ruleId <= 0)
            {

                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "none",
                    ReponseContent = "未取到关键词对应的数据",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
                return null;
            }

            IResponseMessageBase reponseMessage = null;
            switch (responseType)
            {
                case (int)WeixinRequestRule.XResponseType.Text:
                    //发送纯文字
                    reponseMessage = GetResponseMessageTxt(requestMessage, rule.Id, accountId);
                    break;
                case (int)WeixinRequestRule.XResponseType.Image:
                    //发送多图文
                    reponseMessage = GetResponseMessageNews(requestMessage, rule.Id, accountId);
                    break;
                case (int)WeixinRequestRule.XResponseType.Voice:
                    //发送语音
                    reponseMessage = GetResponseMessageeMusic(requestMessage, rule.Id, accountId);
                    break;
                case (int)WeixinRequestRule.XResponseType.SingleImage:
                    reponseMessage = GetResponseMessageSingleImage(requestMessage, rule.Id, accountId);
                    break;
                default:
                    break;
            }
            return reponseMessage;
        }
        #endregion

        #region 请求为文本的处理=====================================
        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageText requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            //responseMessage.Content = new BLL.weixin_request_content().GetContent(ruleId);
            WeixinRequestContent ctx = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (ctx != null)
            {
                responseMessage.Content = ctx.Content;
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = "text",
                    ReponseContent = responseMessage.Content,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "text", responseMessage.Content, requestMessage.ToUserName);
            return responseMessage;
        }

        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxtByContent(RequestMessageText requestMessage, string content, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = content;
            WeixinResponseContent entity = new WeixinResponseContent()
            {
                OpenId = requestMessage.FromUserName,
                RequestType = requestMessage.MsgType.ToString(),
                RequestContent = requestMessage.Content,
                ResponseType = "text",
                ReponseContent = "文字请求，推送纯粹文字，内容为：" + content,
                XmlContent = requestMessage.ToUserName
            };
            entity.Insert();
            //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "text", "文字请求，推送纯粹文字，内容为：" + content, requestMessage.ToUserName);
            return responseMessage;
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        public IResponseMessageBase GetResponseMessageeMusic(RequestMessageText requestMessage, int ruleId, int accountId)
        {
            //string EventName = "";
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
            //Model.weixin_request_content model = new BLL.weixin_request_content().GetModel(ruleId);
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = "music",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (model.MediaURL.Contains("http://"))
                {
                    responseMessage.Music.MusicUrl = model.MediaURL;

                }
                else
                {
                    //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                    responseMessage.Music.MusicUrl = Utils.GetServerUrl() + model.MediaURL;
                }
                responseMessage.Music.Title = model.Title;
                responseMessage.Music.Description = model.Content;
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "音乐链接：" + model.media_url + "|标题：" + model.title + "|描述：" + model.content, requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = "music",
                    ReponseContent = "音乐链接：" + model.MediaURL + "|标题：" + model.Title + "|描述：" + model.Content,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            return responseMessage;
        }

        /// <summary>
        /// 推送多图文
        /// </summary>
        public IResponseMessageBase GetResponseMessageNews(RequestMessageText requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();

            Senparc.NeuChar.Entities.Article article;
            List<Senparc.NeuChar.Entities.Article> artList = new List<Senparc.NeuChar.Entities.Article>();
            IList<WeixinRequestContent> twList = WeixinRequestContent.FindAll(WeixinRequestContent._.RuleId == ruleId, null, null, 0, 10);
            foreach (WeixinRequestContent modelt in twList)
            {
                article = new Senparc.NeuChar.Entities.Article();
                article.Title = modelt.Title;
                article.Description = modelt.Content;
                article.Url = GetWXApiUrl(modelt.LinkUrl, token, openid);
                if (string.IsNullOrEmpty(modelt.ImgURL))
                    article.PicUrl = string.Empty;
                else
                {
                    if (modelt.ImgURL.Contains("http://"))
                    {
                        article.PicUrl = modelt.ImgURL;

                    }
                    else
                    {
                        //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                        article.PicUrl = Utils.GetServerUrl() + "/" + modelt.ImgURL;
                    }
                }
                artList.Add(article);
            }
            if (artList.Count == 0)
            {
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = "txtpic",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "-1", requestMessage.ToUserName);
            }
            else
            {
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = "txtpic",
                    ReponseContent = "这次发了" + artList.Count + "条图文",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "这次发了" + artList.Count + "条图文", requestMessage.ToUserName);
            }
            responseMessage.Articles.AddRange(artList);

            //Article article;
            //List<Article> artList = new List<Article>();
            //IList<Model.weixin_request_content> twList = new BLL.weixin_request_content().GetModelList(10, ruleId, string.Empty);
            //foreach (Model.weixin_request_content modelt in twList)
            //{
            //    article = new Article();
            //    article.Title = modelt.title;
            //    article.Description = modelt.content;
            //    article.Url = GetWXApiUrl(modelt.link_url, token, openid);
            //    if (string.IsNullOrEmpty(modelt.img_url))
            //    {
            //        article.PicUrl = string.Empty;
            //    }
            //    else
            //    {
            //        if (modelt.img_url.Contains("http://"))
            //        {
            //            article.PicUrl = modelt.img_url;

            //        }
            //        else
            //        {
            //            //因为安装目录是以/开头，所以去掉，以免出现双斜杆
            //            article.PicUrl = siteConfig.weburl + "/" + siteConfig.webpath.Replace("/", "") + modelt.img_url;
            //        }
            //    }
            //    artList.Add(article);
            //}

            //if (artList.Count == 0)
            //{
            //    new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "-1", requestMessage.ToUserName);
            //}
            //else
            //{
            //    new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "这次发了" + artList.Count + "条图文", requestMessage.ToUserName);
            //}
            //responseMessage.Articles.AddRange(artList);
            return responseMessage;
        }


        #endregion

        #region 请求为图片的处理=====================================
        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageImage requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            //responseMessage.Content = new BLL.weixin_request_content().GetContent(ruleId);
            WeixinRequestContent ctx = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (ctx != null)
            {
                responseMessage.Content = ctx.Content;
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.PicUrl,
                    ResponseType = "text",
                    ReponseContent = responseMessage.Content,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            return responseMessage;
        }
        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxtByContent(RequestMessageImage requestMessage, string content, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = content;
            WeixinResponseContent entity = new WeixinResponseContent()
            {
                OpenId = requestMessage.FromUserName,
                RequestType = requestMessage.MsgType.ToString(),
                RequestContent = requestMessage.PicUrl,
                ResponseType = "text",
                ReponseContent = "文字请求，推送纯粹文字，内容为：" + content,
                XmlContent = requestMessage.ToUserName
            };
            entity.Insert();
            //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "text", "文字请求，推送纯粹文字，内容为：" + content, requestMessage.ToUserName);
            return responseMessage;
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        public IResponseMessageBase GetResponseMessageeMusic(RequestMessageImage requestMessage, int ruleId, int accountId)
        {
            //string EventName = "";
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
            //Model.weixin_request_content model = new BLL.weixin_request_content().GetModel(ruleId);
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.PicUrl,
                    ResponseType = "music",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (model.MediaURL.Contains("http://"))
                {
                    responseMessage.Music.MusicUrl = model.MediaURL;

                }
                else
                {
                    //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                    responseMessage.Music.MusicUrl = Utils.GetServerUrl() + model.MediaURL;
                }
                responseMessage.Music.Title = model.Title;
                responseMessage.Music.Description = model.Content;
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "音乐链接：" + model.media_url + "|标题：" + model.title + "|描述：" + model.content, requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.PicUrl,
                    ResponseType = "music",
                    ReponseContent = "音乐链接：" + model.MediaURL + "|标题：" + model.Title + "|描述：" + model.Content,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            return responseMessage;
        }

        /// <summary>
        /// 推送多图文
        /// </summary>
        public IResponseMessageBase GetResponseMessageNews(RequestMessageImage requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();

            Senparc.NeuChar.Entities.Article article;
            List<Senparc.NeuChar.Entities.Article> artList = new List<Senparc.NeuChar.Entities.Article>();
            IList<WeixinRequestContent> twList = WeixinRequestContent.FindAll(WeixinRequestContent._.RuleId == ruleId, null, null, 0, 10);
            foreach (WeixinRequestContent modelt in twList)
            {
                article = new Senparc.NeuChar.Entities.Article();
                article.Title = modelt.Title;
                article.Description = modelt.Content;
                article.Url = GetWXApiUrl(modelt.LinkUrl, token, openid);
                if (string.IsNullOrEmpty(modelt.ImgURL))
                    article.PicUrl = string.Empty;
                else
                {
                    if (modelt.ImgURL.Contains("http://"))
                    {
                        article.PicUrl = modelt.ImgURL;

                    }
                    else
                    {
                        //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                        article.PicUrl = Utils.GetServerUrl() + "/" + modelt.ImgURL;
                    }
                }
                artList.Add(article);
            }
            if (artList.Count == 0)
            {
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.PicUrl,
                    ResponseType = "txtpic",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "-1", requestMessage.ToUserName);
            }
            else
            {
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.PicUrl,
                    ResponseType = "txtpic",
                    ReponseContent = "这次发了" + artList.Count + "条图文",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "txtpic", "这次发了" + artList.Count + "条图文", requestMessage.ToUserName);
            }
            responseMessage.Articles.AddRange(artList);

            return responseMessage;
        }

        /// <summary>
        /// 推送单张图片
        /// </summary>
        public IResponseMessageBase GetResponseMessageSingleImagesContent(RequestMessageText requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageImage>(requestMessage);
            string EventName = "";
            var locationService = new LocationService();
            NewLife.Log.XTrace.WriteLine("相应单图片回复");
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {
                NewLife.Log.XTrace.WriteLine("没找到单图片回复规则");
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "image",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (string.IsNullOrEmpty(model.MediaURL))
                {
                    //如果没有mediaid 则先上传
                    string imgUrl = "~/wwwroot" + model.ImgURL;
                    var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(siteConfig.AppId, UploadMediaFileType.image,
                                                                     ServerUtility.ContentRootMapPath(imgUrl));
                    //NewLife.Log.XTrace.WriteLine("准备上传素材，imgurl:" + imgurl);

                    //var accessToken = AccessTokenContainer.TryGetAccessToken(siteConfig.AppId, siteConfig.AppSecret);// AccessTokenContainer.TryGetAccessToken(official.AppId, official.AppSecret, false);
                    //上传图片素材 imgurl 必须是本地绝对路径。。。。
                    //var upresult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadForeverMedia(accessToken, imgurl);
                    model.MediaURL = uploadResult.media_id;
                    model.Update();
                    NewLife.Log.XTrace.WriteLine("上传素材，返回mediaid:" + uploadResult.media_id);
                }
                Image img = new Image();
                img.MediaId = model.MediaURL;
                responseMessage.CreateTime = DateTime.Now;
                responseMessage.Image = img;

                NewLife.Log.XTrace.WriteLine("回复单图片，mediaid" + img.MediaId + ";回复用户openid：" + responseMessage.FromUserName);

                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "image",
                    ReponseContent = "图片：" + model.MediaURL + "|标题：" + model.Title,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }

            return responseMessage;
        }

        #endregion

        #region 请求为事件的处理=====================================
        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageEventBase requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            WeixinRequestContent rct = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            responseMessage.Content = rct != null ? rct.Content : "";// new BLL.weixin_request_content().GetContent(ruleId);

            NewLife.Log.XTrace.WriteLine("文本自动关注回复：" + ruleId + "||" + responseMessage.Content);

            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            //else if (requestMessage.EventKey != null)
            //{
            //    EventName += requestMessage.EventKey.ToString();
            //}

            //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "text", responseMessage.Content, requestMessage.ToUserName);
            WeixinResponseContent entity = new WeixinResponseContent()
            {
                OpenId = requestMessage.FromUserName,
                RequestType = requestMessage.MsgType.ToString(),
                RequestContent = EventName,
                ResponseType = "text",
                ReponseContent = responseMessage.Content,
                XmlContent = requestMessage.ToUserName
            };
            entity.Insert();

            return responseMessage;
        }

        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageTxtByContent(RequestMessageEventBase requestMessage, string content, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            var locationService = new LocationService();
            responseMessage.Content = content;
            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            //else if (requestMessage.EventKey != null)
            //{
            //    EventName += requestMessage.EventKey.ToString();
            //}
            //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "text", "事件：推送纯粹的文字，内容为:" + content, requestMessage.ToUserName);
            WeixinResponseContent entity = new WeixinResponseContent()
            {
                OpenId = requestMessage.FromUserName,
                RequestType = requestMessage.MsgType.ToString(),
                RequestContent = EventName,
                ResponseType = "text",
                ReponseContent = "事件：推送纯粹的文字，内容为:" + content,
                XmlContent = requestMessage.ToUserName
            };
            entity.Insert();
            return responseMessage;
        }


        /// <summary>
        /// 推送纯文字
        /// </summary>
        public IResponseMessageBase GetResponseMessageSingleImage(RequestMessageEventBase requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageImage>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();

            NewLife.Log.XTrace.WriteLine("相应单图片回复");
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {
                NewLife.Log.XTrace.WriteLine("没找到单图片回复规则");
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = "关注自动回复",
                    ResponseType = "image",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (string.IsNullOrEmpty(model.MediaURL))
                {
                    string imgUrl = "~/wwwroot" + model.ImgURL;
                    var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(siteConfig.AppId, UploadMediaFileType.image,
                                                                     ServerUtility.ContentRootMapPath(imgUrl));

                    model.MediaURL = uploadResult.media_id;
                    model.Update();
                    NewLife.Log.XTrace.WriteLine("上传素材，返回mediaid:" + uploadResult.media_id);
                }
                Image img = new Image();
                img.MediaId = model.MediaURL;
                responseMessage.CreateTime = DateTime.Now;
                responseMessage.Image = img;

                NewLife.Log.XTrace.WriteLine("回复单图片，mediaid" + img.MediaId + ";回复用户openid：" + responseMessage.FromUserName);

                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = "关注自动回复",
                    ResponseType = "image",
                    ReponseContent = "图片：" + model.MediaURL + "|标题：" + model.Title,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }

            return responseMessage;
        }

        /// <summary>
        /// 推送单张图片
        /// </summary>
        public IResponseMessageBase GetResponseMessageSingleImagesContent(RequestMessageEventBase requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageImage>(requestMessage);
            string EventName = "";
            var locationService = new LocationService();
            NewLife.Log.XTrace.WriteLine("相应单图片回复");
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {
                NewLife.Log.XTrace.WriteLine("没找到单图片回复规则");
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "image",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (string.IsNullOrEmpty(model.MediaURL))
                {
                    //如果没有mediaid 则先上传
                    string imgUrl = "~/wwwroot" + model.ImgURL;
                    var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(siteConfig.AppId, UploadMediaFileType.image,
                                                                     ServerUtility.ContentRootMapPath(imgUrl));
                    model.MediaURL = uploadResult.media_id;
                    model.Update();
                    NewLife.Log.XTrace.WriteLine("上传素材，返回mediaid:" + uploadResult.media_id);
                }
                Image img = new Image();
                img.MediaId = model.MediaURL;
                responseMessage.CreateTime = DateTime.Now;
                responseMessage.Image = img;

                NewLife.Log.XTrace.WriteLine("回复单图片，mediaid" + img.MediaId + ";回复用户openid：" + responseMessage.FromUserName);

                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "image",
                    ReponseContent = "图片：" + model.MediaURL + "|标题：" + model.Title,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }

            return responseMessage;
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        public IResponseMessageBase GetResponseMessageeMusic(RequestMessageEventBase requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            //else if (requestMessage.EventKey != null)
            //{
            //    EventName += requestMessage.EventKey.ToString();
            //}

            //Model.weixin_request_content model = new BLL.weixin_request_content().GetModel(ruleId);
            WeixinRequestContent model = WeixinRequestContent.Find(WeixinRequestContent._.RuleId == ruleId);
            if (model == null)
            {

                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "music",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                if (model.MediaURL.Contains("http://"))
                {
                    responseMessage.Music.MusicUrl = model.MediaURL;

                }
                else
                {
                    //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                    responseMessage.Music.MusicUrl = Utils.GetServerUrl() + model.MediaURL;
                }
                responseMessage.Music.Title = model.Title;
                responseMessage.Music.Description = model.Content;
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "music", "音乐链接：" + model.media_url + "|标题：" + model.title + "|描述：" + model.content, requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "music",
                    ReponseContent = "音乐链接：" + model.MediaURL + "|标题：" + model.Title + "|描述：" + model.Content,
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            return responseMessage;
        }

        /// <summary>
        /// 推送多图文
        /// </summary>
        public IResponseMessageBase GetResponseMessageNews(RequestMessageEventBase requestMessage, int ruleId, int accountId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();

            Senparc.NeuChar.Entities.Article article;
            List<Senparc.NeuChar.Entities.Article> artList = new List<Senparc.NeuChar.Entities.Article>();
            //IList<Model.weixin_request_content> twList = new BLL.weixin_request_content().GetModelList(10, ruleId, string.Empty);
            IList<WeixinRequestContent> twList = WeixinRequestContent.FindAll(WeixinRequestContent._.RuleId == ruleId, null, null, 0, 10);
            foreach (WeixinRequestContent modelt in twList)
            {
                article = new Senparc.NeuChar.Entities.Article();
                article.Title = modelt.Title;
                article.Description = modelt.Content;
                article.Url = GetWXApiUrl(modelt.LinkUrl, token, openid);
                if (string.IsNullOrEmpty(modelt.ImgURL))
                {
                    article.PicUrl = string.Empty;
                }
                else
                {
                    if (modelt.ImgURL.Contains("http://"))
                    {
                        article.PicUrl = modelt.ImgURL;

                    }
                    else
                    {
                        //因为安装目录是以/开头，所以去掉，以免出现双斜杆
                        article.PicUrl = Utils.GetServerUrl() + modelt.ImgURL;
                    }
                }
                artList.Add(article);
            }

            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            //else if (requestMessage.EventKey != null)
            //{
            //    EventName += requestMessage.EventKey.ToString();
            //}

            if (artList.Count == 0)
            {
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "txtpic", "-1", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "txtpic",
                    ReponseContent = "-1",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            else
            {
                //new BLL.weixin_response_content().Add(accountId, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "txtpic", "这次发了" + artList.Count + "条图文", requestMessage.ToUserName);
                WeixinResponseContent entity = new WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = EventName,
                    ResponseType = "txtpic",
                    ReponseContent = "这次发了" + artList.Count + "条图文",
                    XmlContent = requestMessage.ToUserName
                };
                entity.Insert();
            }
            responseMessage.Articles.AddRange(artList);
            return responseMessage;
        }
        #endregion

        #region 获取验证公众账户ID===================================
        /// <summary>
        /// 获取公众账户的ID
        /// </summary>
        public int GetAccountId()
        {
            if (string.IsNullOrEmpty(MyHttpContext.Current.Request.Query["uid"]))
            {
                return 0;
            }
            int tmpInt = 0;
            if (!int.TryParse(MyHttpContext.Current.Request.Query["uid"].ToString(), out tmpInt))
            {
                return 0;
            }
            int uid = int.Parse(MyHttpContext.Current.Request.Query["uid"].ToString());
            return uid;
        }

        /// <summary>
        /// 验证公众账户原始ID是否一致
        /// </summary>
        public bool ExistsOriginalId(int accountId, string originalId)
        {
            //if(siteConfig.WexinAccount == )
            //if (WechatOfficial.FindCount(WechatOfficial._.Id == accountId & WechatOfficial._.OriginalId == originalId, null, null, 0, 0) > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
            //return new BLL.weixin_account().ExistsOriginalId(accountId, originalId);
        }
        #endregion

        #region 常用的方法封装=======================================
        /// <summary>
        /// 拼接微信URL地址参数
        /// </summary>
        public string GetWXApiUrl(string url, string token, string openid)
        {
            if (url.Contains("?"))
            {
                return url + "&token=" + token + "&openid=" + openid;
            }
            return url + "?token=" + token + "&openid=" + openid;
        }

        /// <summary>
        /// 设置微信url地址的后缀
        /// </summary>
        /// <returns></returns>
        public string GetWxUrlSuffix()
        {
            return "wxref=mp.weixin.qq.com";
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        public long ConvertDateTimeInt(System.DateTime time)
        {
            //long intResult = 0;
            
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //intResult = (long)(time - startTime).TotalSeconds;
            return long.Parse(Utils.GetTimeStamp());
        }
        #endregion
    }
}
