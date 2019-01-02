using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using COMCMS.Core.Models;
using Newtonsoft.Json;
using COMCMS.Common;

namespace COMCMS.Core
{
    /// <summary>广告详情</summary>
    public partial class Ads : Entity<Ads>
    {
        #region 对象操作
        static Ads()
        {
            // 累加字段
            //Meta.Factory.AdditionalFields.Add(__.Logins);

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Ads[广告详情]数据……");

        //    var entity = new Ads();
        //    entity.Id = 0;
        //    entity.Title = "abc";
        //    entity.Content = "abc";
        //    entity.KId = 0;
        //    entity.TId = 0;
        //    entity.StartTime = DateTime.Now;
        //    entity.EndTime = DateTime.Now;
        //    entity.IsDisabled = true;
        //    entity.Sequence = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Ads[广告详情]数据！"
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        private AdsKind _AdsKind;
        /// <summary>广告栏目</summary>
        public AdsKind AdsKind
        {
            get
            {
                _AdsKind = AdsKind.FindById(KId);
                return _AdsKind;
            }
            set { _AdsKind = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据Id查找</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Ads FindById(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.Id, id);
            else // 实体缓存
                return Meta.Cache.Entities.FirstOrDefault(x => x.Id == id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion

        #region 业务
        public enum AdsType
        {
            /// <summary>
            /// 代码广告
            /// </summary>
            Script =0,
            /// <summary>文字广告</summary>
            Text=1,
            /// <summary>图片广告</summary>
            Image = 2,
            /// <summary>Flash广告</summary>
            Flash = 3,
            /// <summary>幻灯片广告</summary>
            Slides = 4,
            /// <summary>漂浮广告</summary>
            Float = 5,
            /// <summary>对联广告</summary>
            Couplet = 6,
            /// <summary>视频广告</summary>
            Video = 7
        }
        /// <summary>
        /// 获取广告类型
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static string GetAdsType(int tid)
        {
            string tname = "代码广告";
            switch (tid)
            {
                case 0:
                    break;
                case 1:
                    tname = "文字广告";
                    break;
                case 2:
                    tname = "图片广告";
                    break;
                case 3:
                    tname = "Flash广告";
                    break;
                case 4:
                    tname = "幻灯片广告";
                    break;
                case 5:
                    tname = "漂浮广告";
                    break;
                case 6:
                    tname = "对联广告";
                    break;
                case 7:
                    tname = "视频广告";
                    break;
            }
            return tname;
        }
        /*
 * 获取广告 GetAds(广告ID)、GetAdsByKinds(广告类别ID)
 * 先读取广告实体，再根据广告类型进行相关的处理
 * 
 * 
 * 
 */

        /// <summary>
        /// 获取单条广告代码
        /// </summary>
        /// <param name="aid">广告ID</param>
        /// <returns></returns>
        public static string GetAds(int aid)
        {
            StringBuilder str = new StringBuilder();
            Ads model = new Ads();
            model = Find(_.Id == aid);
            if (model != null && DateTime.Compare(DateTime.Now, model.StartTime) > 0 && DateTime.Compare(model.EndTime, DateTime.Now) > 0 && !model.IsDisabled)
            {
                switch (model.TId)
                {
                    case 0://0为代码广告
                        ScriptAds script = new ScriptAds();
                        script = JsonConvert.DeserializeObject<ScriptAds>(model.Content);
                        str.Append(script.content);
                        break;
                    case 1://1为文字广告
                        TextAds txt = new TextAds();
                        txt = JsonConvert.DeserializeObject<TextAds>(model.Content);
                        string txttpl = "<a href=\"{0}\" target=\"_blank\"><font style=\"{1}\">{2}</font></a>";
                        str.Append(string.Format(txttpl, txt.link, txt.style, txt.txt));
                        break;
                    case 2://2为图片广告
                        ImgAds img = new ImgAds();
                        img = JsonConvert.DeserializeObject<ImgAds>(model.Content);
                        if (!string.IsNullOrEmpty(img.link) && img.link != "#")
                            str.Append("<a href =\"" + img.link + "\" title=\"" + img.alt + "\" target=\"_blank\">");
                        str.Append("<img style=\"");
                        if (img.width > 0)
                        {
                            str.Append("width:" + img.width.ToString() + "px;");
                        }
                        if (img.height > 0)
                        {
                            str.Append("height:" + img.height.ToString() + "px;");
                        }
                        str.Append("\" src=\"" + img.img + "\" alt=\"" + img.alt + "\" />");
                        if (!string.IsNullOrEmpty(img.link) && img.link != "#")
                            str.Append("</a>");
                        break;
                    case 3://3为flash广告
                        FlashAds flash = new FlashAds();
                        flash = JsonConvert.DeserializeObject<FlashAds>(model.Content);
                        str.Append("<object classid=\"clsid:D27CDB6E-AE6D-11CF-96B8-444553540000\" id=\"swfobj" + model.Id.ToString() + "\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,40,0\" border=\"0\" ");
                        if (flash.width > 0)
                        {
                            str.Append("width=\"" + flash.width.ToString() + "\" ");
                        }
                        if (flash.height > 0)
                        {
                            str.Append("height=\"" + flash.height.ToString() + "\" ");
                        }
                        str.Append(">");
                        str.Append("<param name=\"movie\" value=\"" + flash.swf + "\">");
                        str.Append("<param name=\"quality\" value=\"high\">");
                        str.Append("<param name=\"wmode\" value=\"transparent\">");
                        str.Append("<param name=\"menu\" value=\"false\">");
                        str.Append("<embed src=\"" + flash.swf + "\"  pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" name=\"swfobj" + model.Id.ToString() + "\"  quality=\"High\" wmode=\"transparent\" ");
                        if (flash.width > 0)
                        {
                            str.Append("width=\"" + flash.width.ToString() + "\" ");
                        }
                        if (flash.height > 0)
                        {
                            str.Append("height=\"" + flash.height.ToString() + "\" ");
                        }
                        str.Append("></embed>");
                        str.Append("</object>");
                        break;
                    case 4://4为幻灯片广告
                        List<ImgAds> listimg = new List<ImgAds>();
                        listimg = JsonConvert.DeserializeObject<List<ImgAds>>(model.Content);
                        if (listimg != null && listimg.Count > 0)
                        {
                            //图片数量不止1才显示：
                            str.Append("<script type=\"text/javascript\" src=\"/static/js/swfobject.js\"></script>");//swfobject js
                            str.Append("<div id=\"SlideAds_" + model.Id.ToString() + "\"></div>");//div
                            str.Append("<script type=\"text/javascript\">swfobject.embedSWF(\"/static/images/common/bcastr4.swf?xml=/Tools/slidexml/" + model.Id.ToString() + "\", \"SlideAds_" + model.Id.ToString() + "\", \"" + listimg[0].width.ToString() + "\", \"" + listimg[0].height.ToString() + "\", \"9.0.0\",null,null,{wmode:'transparent'});</script>");//js调用内容
                        }
                        break;
                    default:
                        break;
                }
            }
            return str.ToString();
        }
        /// <summary>
        /// 获取图片广告
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static ImgAds GetImgAds(int aid)
        {
            Ads model = new Ads();
            model = Find(_.Id == aid);
            if (model != null && DateTime.Compare(DateTime.Now, model.StartTime) > 0 && DateTime.Compare(model.EndTime, DateTime.Now) > 0 && !model.IsDisabled && model.TId == 2)
            {
                ImgAds img = new ImgAds();
                img = JsonConvert.DeserializeObject<ImgAds>(model.Content);
                return img;
            }
            else
                return null;
        }
        /// <summary>
        /// 获取幻灯片广告
        /// </summary>
        /// <param name="aid">广告id</param>
        /// <returns></returns>
        public static List<ImgAds> GetSlideAds(int aid)
        {
            Ads model = new Ads();
            model = Find(_.Id == aid);
            if (model!=null && model.TId == 4)
            {
                List<ImgAds> listimg = new List<ImgAds>();
                listimg = JsonConvert.DeserializeObject<List<ImgAds>>(model.Content);
                return listimg;
            }
            return null;
        }

        /// <summary>
        /// 获取类别广告，随机显示类别下一条广告
        /// </summary>
        /// <param name="kid">类别ID</param>
        /// <returns></returns>
        public static string GetAdsByKinds(int kid)
        {
            string str = string.Empty;
            //DataTable dt = GetVisibleList("and kid=" + kid.ToString()).Tables[0];
            IList<Ads> dt = FindAll(Ads._.KId == kid, Ads._.Id.Desc(), null, 0, 0);
            if (dt != null && dt.Count > 0)
            {
                int rndRow = Utils.GetRND(0, dt.Count - 1);
                str = GetAds(dt[rndRow].Id);
            }
            return str;
        }
        #endregion
    }
}