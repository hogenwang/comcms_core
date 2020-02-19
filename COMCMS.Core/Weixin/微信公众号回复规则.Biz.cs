using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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

namespace COMCMS.Core
{
    /// <summary>微信公众号回复规则</summary>
    public partial class WeixinRequestRule : Entity<WeixinRequestRule>
    {
        #region 对象操作
        static WeixinRequestRule()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.RequestType);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<TimeModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化WeixinRequestRule[微信公众号回复规则]数据……");

        //    var entity = new WeixinRequestRule();
        //    entity.Id = 0;
        //    entity.RuleName = "abc";
        //    entity.Keywords = "abc";
        //    entity.RequestType = 0;
        //    entity.ResponseType = 0;
        //    entity.IsLikeQuery = 0;
        //    entity.IsDefault = 0;
        //    entity.Rank = 0;
        //    entity.AddTime = DateTime.Now;
        //    entity.UpdateTime = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化WeixinRequestRule[微信公众号回复规则]数据！");
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
        private IList<WeixinRequestContent> _ListContent;
        /// <summary>
        /// 详细规则列表
        /// </summary>
        public IList<WeixinRequestContent> ListContent
        {
            get
            {
                if (_ListContent == null && Id > 0 && !Dirtys["ListWechatRequestContent_" + Id])
                {
                    _ListContent = WeixinRequestContent.FindAll(WeixinRequestContent._.RuleId == Id, WeixinRequestContent._.Rank.Asc(), null, 0, 0);

                    Dirtys["ListWechatRequestContent_" + Id] = true;
                }
                return _ListContent;
            }
            set { _ListContent = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static WeixinRequestRule FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion

        #region 业务
        /// <summary>
        /// 微信请求类型
        /// 0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件
        /// </summary>
        public enum XRequestType
        {
            /// <summary>默认回复</summary>
            Default = 0,
            /// <summary>
            /// 文本请求
            /// </summary>
            Text = 1,
            /// <summary>
            /// 图片
            /// </summary>
            Image = 2,
            /// <summary>
            /// 语音
            /// </summary>
            Voice = 3,
            /// <summary>
            /// 链接
            /// </summary>
            Link = 4,
            /// <summary>
            /// 地理位置
            /// </summary>
            Location = 5,
            /// <summary>
            /// 关注
            /// </summary>
            Flow = 6,
            /// <summary>
            /// 取消关注
            /// </summary>
            Unflow = 7,
            /// <summary>
            /// 扫描二维码
            /// </summary>
            Scan = 8,
            /// <summary>
            /// 上报地理位置
            /// </summary>
            ReportingLocation = 9,
            /// <summary>
            /// 自定义菜单
            /// </summary>
            CustomMenu = 10,
            /// <summary>
            /// 没找到关键字
            /// </summary>
            NotFindKeyword = 11
        }
        /// <summary>
        /// 回复类型
        /// </summary>
        public enum XResponseType
        {
            /// <summary>
            /// 文本
            /// </summary>
            Text = 0,
            /// <summary>
            /// 地理位置
            /// </summary>
            Location = 1,
            /// <summary>
            /// 图片
            /// </summary>
            Image = 2,
            /// <summary>
            /// 语音
            /// </summary>
            Voice = 3,
            /// <summary>
            /// 视频
            /// </summary>
            Video = 4,
            /// <summary>
            /// 链接
            /// </summary>
            Link = 5,
            /// <summary>
            /// 短片
            /// </summary>
            ShortVideo = 6,
            /// <summary>
            /// 自定义事件
            /// </summary>
            Event = 7,
            /// <summary>
            /// 单张图片
            /// </summary>
            SingleImage = 8,
        }
        #endregion
    }
}