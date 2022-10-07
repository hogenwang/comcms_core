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
using System.Web.Script.Serialization;
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
using XCode.Shards;

namespace COMCMS.Core
{
    /// <summary>系统配置</summary>
    public partial class Config : Entity<Config>
    {
        #region 对象操作
        static Config()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(IsRewrite));

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会做一些统一处理
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行修正
            // 货币保留6位小数
            DefaultFare = Math.Round(DefaultFare, 6);
            MaxFreeFare = Math.Round(MaxFreeFare, 6);
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Config[系统配置]数据……");

        //    var entity = new Config();
        //    entity.SiteName = "abc";
        //    entity.SiteUrl = "abc";
        //    entity.SiteLogo = "abc";
        //    entity.Icp = "abc";
        //    entity.SiteEmail = "abc";
        //    entity.SiteTel = "abc";
        //    entity.Copyright = "abc";
        //    entity.IsCloseSite = true;
        //    entity.CloseReason = "abc";
        //    entity.CountScript = "abc";
        //    entity.WeiboQRCode = "abc";
        //    entity.WinxinQRCode = "abc";
        //    entity.Keyword = "abc";
        //    entity.Description = "abc";
        //    entity.IndexTitle = "abc";
        //    entity.IsRewrite = 0;
        //    entity.SearchMinTime = 0;
        //    entity.OnlineQQ = "abc";
        //    entity.OnlineSkype = "abc";
        //    entity.OnlineWangWang = "abc";
        //    entity.SkinName = "abc";
        //    entity.OfficialName = "abc";
        //    entity.OfficialDecsription = "abc";
        //    entity.OfficialOriginalId = "abc";
        //    entity.WexinAccount = "abc";
        //    entity.Token = "abc";
        //    entity.AppId = "abc";
        //    entity.AppSecret = "abc";
        //    entity.FllowTipPageURL = "abc";
        //    entity.OfficialType = 0;
        //    entity.EncodingAESKey = "abc";
        //    entity.DEType = 0;
        //    entity.OfficialQRCode = "abc";
        //    entity.OfficialImg = "abc";
        //    entity.LastUpdateTime = DateTime.Now;
        //    entity.LastCacheTime = DateTime.Now;
        //    entity.MCHId = "abc";
        //    entity.MCHKey = "abc";
        //    entity.DefaultFare = 0.0;
        //    entity.MaxFreeFare = 0.0;
        //    entity.WXAppId = "abc";
        //    entity.WXAppSecret = "abc";
        //    entity.IsResetData = 0;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Config[系统配置]数据！");
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
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Config FindById(Int32 id)
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

        // Select Count(Id) as Id,Category From Config Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<Config> _CategoryCache = new FieldCache<Config>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
        #endregion

        #region 业务操作
        #endregion
    }
}