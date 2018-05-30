using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using XCode;
using System.Web;
using NewLife.Log;
using static COMCMS.Web.Models.APIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace COMCMS.Web.Controllers.api
{
    public class UserController : APIBaseController
    {
        #region 用户登录
        /// <summary>
        /// 使用code直接登录，如果登录返回信息，如果没有则注册
        /// </summary>
        /// <param name="code"></param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        public object UserLogin(string code, string random = "", string timeStamp = "", string signature = "")
        {
            if (!AutoCheckQueryStringSignature())
            {
                return reJson;
            }
            if (string.IsNullOrEmpty(code))
            {
                reJson.message = "请允许用户授权信息！";
                return reJson;
            }
            Config cfg = Config.GetSystemConfig();
            string appid = cfg.WXAppId;// ConfigurationManager.AppSettings["wxappAppId"];
            string appsecrect = cfg.WXAppSecret; //ConfigurationManager.AppSettings["wxappAppSecrect"];
                                                 //请求地址
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appid}&secret={appsecrect}&js_code={code}&grant_type=authorization_code";
            string result = Utils.HttpGet(url);
            XTrace.WriteLine("用户Code登录回调结果：" + result);
            //反序列化为json
            dynamic json = JToken.Parse(result) as dynamic;
            if (!string.IsNullOrEmpty(json.errcode))
            {
                XTrace.WriteLine("微信服务器回调失败：" + json.errcode);
                reJson.message = "注册失败，请联系管理员。" + json.errcode;
                return reJson;
            }
            if (Member.FindCount(Member._.WeixinAppOpenId == json.openid, null, null, 0, 0) > 0)
            {
                Member my = Member.Find(Member._.WeixinAppOpenId == json.openid);
                if (my == null)
                {
                    XTrace.WriteLine("登录成功，但是系统找不到用户：");
                    reJson.code = 5003;
                    reJson.message = "系统找不到本用户！";
                    return reJson;
                }

                //如果存在，则返回个人信息
                WXAppSession wxas = WXAppSession.Find(WXAppSession._.SessionKey == json.session_key & WXAppSession._.OpenId == json.openid);
                string mysessionkey = "";
                if (wxas == null)
                {
                    //登录过期了。重新写一个登录和session key
                    string openid = json.openid;
                    string session_key = json.session_key;
                    WXAppSession entity = new WXAppSession();
                    entity.OpenId = openid;
                    entity.SessionKey = session_key;
                    entity.Key = Utils.GetRandomChar(20);
                    entity.AddTime = DateTime.Now;
                    entity.UId = my.Id;
                    entity.Insert();
                    XTrace.WriteLine("登录成功，过期重写session");
                    mysessionkey = entity.Key;
                    //return new { code = 0, message = "登录成功", sessionkey = entity.Key };
                }
                else
                {
                    mysessionkey = wxas.Key;
                }
                string nickName = my.Nickname;
                if (!string.IsNullOrEmpty(nickName)) nickName = HttpUtility.UrlDecode(nickName);
                reJson.code = 0;
                reJson.message = "登录成功！";
                reJson.detail = new { sessionkey = mysessionkey, username = nickName, userimg = my.UserImg };
                return reJson;
                //return new { code = 0, message = "登录成功", sessionkey = mysessionkey, username = nickName, userimg = my.UserImg };
            }
            else
            {
                XTrace.WriteLine($"用户{json.openid} 开始执行注册{DateTime.Now}...");
                //执行注册
                string salt = Utils.GetRandomChar(10);
                Member m = new Member();
                m.UserName = "wx" + Utils.GetRandomChar(12);//user.nickName;
                m.PassWord = Utils.MD5(salt + Utils.GetRandomChar(20));
                m.Salt = salt;
                m.UserImg = "";
                m.Nickname = "";
                m.RoleId = 3;//默认是普通会员
                m.Sex = 0;
                m.RegIP = Utils.GetIP();
                m.RegTime = DateTime.Now;
                m.WeixinAppOpenId = json.openid;
                //如果邀请人id不为空，则处理下级
                int parent = 0;
                int grandfather = 0;
                m.Parent = parent;
                m.Grandfather = grandfather;
                m.Insert();

                //然后执行登录
                string openid = json.openid;
                string session_key = json.session_key;
                WXAppSession entity = new WXAppSession();
                entity.OpenId = openid;
                entity.SessionKey = session_key;
                entity.Key = Utils.GetRandomChar(20);
                entity.AddTime = DateTime.Now;
                entity.UId = m.Id;
                entity.Insert();
                reJson.code = 0;
                reJson.message = "注册成功！";
                reJson.detail = new { sessionkey = entity.Key, username = "", userimg = m.UserImg };
                return reJson;
                //return new { code = 0, message = "注册成功！", sessionkey = entity.Key, username = "", userimg = m.UserImg };
            }
        }
        #endregion

        #region 用户授权信息同步
        /// <summary>
        /// 同步微信信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object SyncWeixinInfo(string sessionkey, string userInfo, int inviteid = 0, string random = "", string timeStamp = "", string signature = "")
        {
            if (!AutoCheckQueryStringSignature())
            {
                return reJson;
            }
            if (string.IsNullOrEmpty(sessionkey))
            {
                reJson.code = 5003;
                reJson.message = "登录状态失败";
                return reJson;
            }
            WXAppSession entity = WXAppSession.Find(WXAppSession._.Key == sessionkey);
            if (entity == null)
            {
                reJson.message = "未找到登录状态！";
                return reJson;
            }
            if (string.IsNullOrEmpty(userInfo))
            {
                reJson.message = "用户信息错误！";
                return reJson;
            }
            WXAppUserInfo info = JsonConvert.DeserializeObject<WXAppUserInfo>(userInfo);
            if (string.IsNullOrEmpty(info.avatarUrl))
            {
                reJson.message = "微信用户信息错误！";
                return reJson;
            }
            //获取用户
            Member my = Member.FindById(entity.UId);
            if (my == null)
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本用户！";
                return reJson;
            }
            //同步用户信息
            my.Country = info.country;
            my.UserImg = info.avatarUrl;
            my.City = info.city;
            my.Province = info.province;
            my.Sex = info.gender;
            my.Nickname = HttpUtility.UrlEncode(info.nickName);
            my.LastLoginTime = DateTime.Now;
            if (inviteid > 0 & my.Parent == 0)
            {
                Member inviter = Member.Find(Member._.Id == inviteid);
                if (inviter != null)
                {
                    my.Parent = inviter.Id;
                    my.Grandfather = inviter.Parent;
                }
            }
            my.Update();

            reJson.code = 0;
            reJson.message = "同步信息成功！";
            reJson.detail = new { sessionkey = entity.Key, username = info.nickName, userimg = info.avatarUrl };
            return reJson;
        }
        #endregion

        #region 测试
        [HttpGet]
        public object Test()
        {
            
            reJson.code = 0;
            reJson.message = "测试信息，成功！";
            return reJson;
        }
        #endregion
    }
}