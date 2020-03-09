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

namespace COMCMS.Core
{
    /// <summary>快餐</summary>
    public partial class Food : Entity<Food>
    {
        #region 对象操作
        static Food()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.KId);

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
            // 货币保留6位小数
            Price = Math.Round(Price, 6);
            MarketPrice = Math.Round(MarketPrice, 6);
            SpecialPrice = Math.Round(SpecialPrice, 6);
            Fare = Math.Round(Fare, 6);
            Discount = Math.Round(Discount, 6);
            //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Food[快餐]数据……");

        //    var entity = new Food();
        //    entity.Id = 0;
        //    entity.KId = 0;
        //    entity.BId = 0;
        //    entity.ShopId = 0;
        //    entity.CId = 0;
        //    entity.SupportId = 0;
        //    entity.Title = "abc";
        //    entity.ItemNO = "abc";
        //    entity.SubTitle = "abc";
        //    entity.Unit = "abc";
        //    entity.Spec = "abc";
        //    entity.Color = "abc";
        //    entity.Weight = "abc";
        //    entity.Price = 0.0;
        //    entity.MarketPrice = 0.0;
        //    entity.SpecialPrice = 0.0;
        //    entity.Fare = 0.0;
        //    entity.Discount = 0.0;
        //    entity.Material = "abc";
        //    entity.Front = "abc";
        //    entity.Credits = 0;
        //    entity.Stock = 0;
        //    entity.WarnStock = 0;
        //    entity.IsSubProduct = 0;
        //    entity.PPId = 0;
        //    entity.Content = "abc";
        //    entity.Parameters = "abc";
        //    entity.Keyword = "abc";
        //    entity.Description = "abc";
        //    entity.LinkURL = "abc";
        //    entity.TitleColor = "abc";
        //    entity.TemplateFile = "abc";
        //    entity.IsRecommend = 0;
        //    entity.IsHide = 0;
        //    entity.IsLock = 0;
        //    entity.IsDel = 0;
        //    entity.IsTop = 0;
        //    entity.IsBest = 0;
        //    entity.IsComment = 0;
        //    entity.IsMember = 0;
        //    entity.IsNew = 0;
        //    entity.IsSpecial = 0;
        //    entity.IsPromote = 0;
        //    entity.IsHotSales = 0;
        //    entity.IsBreakup = 0;
        //    entity.IsShelves = 0;
        //    entity.IsVerify = 0;
        //    entity.Hits = 0;
        //    entity.IsGift = 0;
        //    entity.IsPart = 0;
        //    entity.Sequence = 0;
        //    entity.CommentCount = 0;
        //    entity.Icon = "abc";
        //    entity.ClassName = "abc";
        //    entity.BannerImg = "abc";
        //    entity.Pic = "abc";
        //    entity.AdsId = 0;
        //    entity.Tags = "abc";
        //    entity.ItemImg = "abc";
        //    entity.Service = "abc";
        //    entity.AuthorId = 0;
        //    entity.AddTime = DateTime.Now;
        //    entity.UpdateTime = DateTime.Now;
        //    entity.Location = "abc";
        //    entity.FilePath = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Food[快餐]数据！");
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
        /// <summary>店铺ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        public Shop Shop { get { return Extends.Get(nameof(Shop), k => Shop.FindById(ShopId)); } }

        /// <summary>店铺ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        [DisplayName("店铺ID")]
        [Map(__.ShopId, typeof(Shop), "Id")]
        public Int32 ShopId { get { return Shop != null ? Shop.Id : 0; } }
        /// <summary>广告ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        public Ads Ads { get { return Extends.Get(nameof(Ads), k => Ads.FindById(AdsId)); } }

        /// <summary>广告ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        [DisplayName("广告ID")]
        [Map(__.AdsId, typeof(Ads), "Id")]
        public String AdsTitle { get { return Ads?.Title; } }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Food FindById(Int32 id)
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
    }
}