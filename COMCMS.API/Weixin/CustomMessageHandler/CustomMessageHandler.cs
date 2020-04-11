using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using COMCMS.Core;
using System.IO;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Entities;
using Senparc.NeuChar.Entities;
using System.Threading.Tasks;
using Senparc.NeuChar.Entities.Request;
using System.Linq;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.MP;
using NewLife.Log;
using Senparc.NeuChar.Helpers;

namespace COMCMS.API.Weixin.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>  /*如果不需要自定义，可以直接使用：
        MessageHandler<DefaultMpMessageContext> */
    {
        private Config cfg = Config.GetSystemConfig();
        /// <summary>
        /// 模板消息集合（Key：checkCode，Value：OpenId）
        /// 注意：这里只做测试，只适用于单服务器
        /// </summary>
        public static Dictionary<string, string> TemplateMessageCollection = new Dictionary<string, string>();

        private string appId = "";
        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, CustomMessageHandler> GenerateMessageHandler = (stream, postModel, maxRecordCount)
                        => new CustomMessageHandler(stream, postModel, maxRecordCount, false /* 是否只允许处理加密消息，以提高安全性 */);
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEcryptMessage = false)
    : base(inputStream, postModel, maxRecordCount, onlyAllowEcryptMessage)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalGlobalMessageContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;

            //OnlyAllowEcryptMessage = true;//是否只允许接收加密消息，默认为 false

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }
            else
                appId = cfg.AppId;

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
            * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
            * 只需要在这里统一发出委托请求，如：
            * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            * return responseMessage;
            */

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            var defaultResponseMessage = base.CreateResponseMessage<ResponseMessageText>();
            IResponseMessageBase IR = null;
            Common.MessageFunction cmfun = new Common.MessageFunction();
            int ruleId = 0;

            var requestHandler = await requestMessage.StartHandler().Default(async () =>
            {
                string keywords = requestMessage.Content;
                if (!string.IsNullOrEmpty(keywords)) keywords = keywords.Trim();

                var currentMessageContext = await base.GetCurrentMessageContext();

                //先精确匹配，再模糊匹配
                Core.WeixinRequestRule rule = Core.WeixinRequestRule.Find(Core.WeixinRequestRule._.Keywords == keywords & Core.WeixinRequestRule._.RequestType == (int)Core.WeixinRequestRule.XRequestType.Text);

                if (rule == null)
                {
                    //模糊匹配
                    IList<Core.WeixinRequestRule> listRule = Core.WeixinRequestRule.FindAll(Core.WeixinRequestRule._.RequestType == (int)Core.WeixinRequestRule.XRequestType.Text & Core.WeixinRequestRule._.IsLikeQuery == 1, null, null, 0, 0);
                    if (listRule != null && listRule.Count > 0)
                    {
                        int lenKeyword = keywords.Length;
                        foreach (var item in listRule)
                        {
                            if (lenKeyword >= item.Keywords.Length && keywords.IndexOf(item.Keywords) >= 0)
                            {
                                rule = item;//规则
                                break;
                            }
                        }
                    }
                }
                if (rule == null)
                {
                    Core.WeixinRequestRule notFindKeywordRule = Core.WeixinRequestRule.Find(Core.WeixinRequestRule._.RequestType == (int)Core.WeixinRequestRule.XRequestType.NotFindKeyword);
                    if (notFindKeywordRule == null)
                    {
                        //找不到规则
                        Core.WeixinResponseContent entity = new Core.WeixinResponseContent()
                        {
                            OpenId = requestMessage.FromUserName,
                            RequestType = requestMessage.MsgType.ToString(),
                            RequestContent = requestMessage.Content,
                            ResponseType = "none",
                            ReponseContent = "未取到关键词对应的数据",
                            XmlContent = requestMessage.ToUserName
                        };
                        entity.Insert();
                        return cmfun.GetResponseMessageTxtByContent(requestMessage, "未找到匹配的关键词", 0);
                    }
                    else
                    {
                        ruleId = notFindKeywordRule.Id;
                        switch (notFindKeywordRule.ResponseType)
                        {
                            case (int)Core.WeixinRequestRule.XResponseType.Text:
                                //发送纯文字
                                IR = cmfun.GetResponseMessageTxt(requestMessage, ruleId, 0);
                                break;
                            case (int)Core.WeixinRequestRule.XResponseType.Image:
                                //发送多图文
                                IR = cmfun.GetResponseMessageNews(requestMessage, ruleId, 0);
                                break;
                            case (int)Core.WeixinRequestRule.XResponseType.Voice:
                                //发送语音
                                IR = cmfun.GetResponseMessageeMusic(requestMessage, ruleId, 0);
                                break;
                            case (int)Core.WeixinRequestRule.XResponseType.SingleImage:
                                IR = cmfun.GetResponseMessageSingleImagesContent(requestMessage, ruleId, 0);
                                break;
                            default:
                                break;
                        }
                        return IR;
                    }
                }
                Core.WeixinResponseContent model = new Core.WeixinResponseContent()
                {
                    OpenId = requestMessage.FromUserName,
                    RequestType = requestMessage.MsgType.ToString(),
                    RequestContent = requestMessage.Content,
                    ResponseType = Core.WeixinResponseContent.GetResponseType((int)requestMessage.MsgType),
                    ReponseContent = requestMessage.ToString(),
                    XmlContent = string.Empty
                };
                model.Insert();


                return IR;
            });

            return requestHandler.GetResponseMessage() as IResponseMessageBase;
        }


        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLocationRequestAsync(RequestMessageLocation requestMessage)
        {
            return await Task.Run(() =>
            {
                var locationService = new LocationService();
                var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
                return responseMessage;
            });

        }


        public override async Task<IResponseMessageBase> OnShortVideoRequestAsync(RequestMessageShortVideo requestMessage)
        {
            return await Task.Run(() =>
            {
                var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "您刚才发送的是小视频";
                return responseMessage;
            });

        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {
            //一隔一返回News或Image格式
            if (base.GlobalMessageContext.GetMessageContext(requestMessage).RequestMessages.Count() % 2 == 0)
            {
                var responseMessage = CreateResponseMessage<ResponseMessageNews>();

                responseMessage.Articles.Add(new Senparc.NeuChar.Entities.Article()
                {
                    Title = "您刚才发送了图片信息",
                    Description = "您发送的图片将会显示在边上",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });
                responseMessage.Articles.Add(new Senparc.NeuChar.Entities.Article()
                {
                    Title = "第二条",
                    Description = "第二条带连接的内容",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });
                return await Task.Run(() =>
                {
                    return responseMessage;
                });
                //return responseMessage;
            }
            else
            {
                var responseMessage = CreateResponseMessage<ResponseMessageImage>();
                responseMessage.Image.MediaId = requestMessage.MediaId;
                return await Task.Run(() =>
                {
                    return responseMessage;
                });
                //return responseMessage;
            }
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVoiceRequestAsync(RequestMessageVoice requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
            //上传缩略图
            //var accessToken = Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.image,
                                                         ServerUtility.ContentRootMapPath("~/Images/Logo.jpg"));

            //设置音乐信息
            responseMessage.Music.Title = "天籁之音";
            responseMessage.Music.Description = "播放您上传的语音";
            responseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.ThumbMediaId = uploadResult.media_id;

            //推送一条客服消息
            try
            {
                CustomApi.SendText(appId, OpenId, "本次上传的音频MediaId：" + requestMessage.MediaId);

            }
            catch
            {
            }
            return await Task.Run(() =>
            {
                return responseMessage;
            });

        }
        /// <summary>
        /// 处理视频请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVideoRequestAsync(RequestMessageVideo requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了一条视频信息，ID：" + requestMessage.MediaId;

            #region 上传素材并推送到客户端

            //Task.Factory.StartNew(async () =>
            //{
            //    //上传素材
            //    var dir = ServerUtility.ContentRootMapPath("~/App_Data/TempVideo/");
            //    var file = await MediaApi.GetAsync(appId, requestMessage.MediaId, dir);
            //    var uploadResult = await MediaApi.UploadTemporaryMediaAsync(appId, UploadMediaFileType.video, file, 50000);
            //    await CustomApi.SendVideoAsync(appId, base.WeixinOpenId, uploadResult.media_id, "这是您刚才发送的视频", "这是一条视频消息");
            //}).ContinueWith(async task =>
            //{
            //    if (task.Exception != null)
            //    {
            //        WeixinTrace.Log("OnVideoRequest()储存Video过程发生错误：", task.Exception.Message);


            //        var msg = string.Format("上传素材出错：{0}\r\n{1}",
            //                   task.Exception.Message,
            //                   task.Exception.InnerException != null
            //                       ? task.Exception.InnerException.Message
            //                       : null);
            //        await CustomApi.SendTextAsync(appId, base.WeixinOpenId, msg);
            //    }
            //});

            #endregion
            return await Task.Run(() =>
            {
                return responseMessage;
            });
        }

        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLinkRequestAsync(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = string.Format(@"您发送了一条连接信息：
Title：{0}
Description:{1}
Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return await Task.Run(() =>
            {
                return responseMessage;
            });
        }

        public override async Task<IResponseMessageBase> OnFileRequestAsync(RequestMessageFile requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format(@"您发送了一个文件：
文件名：{0}
说明:{1}
大小：{2}
MD5:{3}", requestMessage.Title, requestMessage.Description, requestMessage.FileTotalLen, requestMessage.FileMd5);
            return await Task.Run(() =>
            {
                return responseMessage;
            });
        }

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = await base.OnEventRequestAsync(requestMessage);//对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
                                                                                      //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }

        public override async Task<IResponseMessageBase> OnUnknownTypeRequestAsync(RequestMessageUnknownType requestMessage)
        {
            /*
             * 此方法用于应急处理SDK没有提供的消息类型，
             * 原始XML可以通过requestMessage.RequestDocument（或this.RequestDocument）获取到。
             * 如果不重写此方法，遇到未知的请求类型将会抛出异常（v14.8.3 之前的版本就是这么做的）
             */
            var msgType = Senparc.NeuChar.Helpers.MsgTypeHelper.GetRequestMsgTypeString(requestMessage.RequestDocument);
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "未知消息类型：" + msgType;
            XTrace.WriteLine("未知请求消息类型", requestMessage.RequestDocument.ToString());
            //WeixinTrace.SendCustomLog("未知请求消息类型", requestMessage.RequestDocument.ToString());//记录到日志中

            return await Task.Run(() =>
            {
                return responseMessage;
            });
        }

    }
}
