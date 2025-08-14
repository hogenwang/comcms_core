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
    /// <summary>文章</summary>
    public partial class Article : Entity<Article>
    {
        #region 对象操作
        static Article()
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

            //if (isNew && !Dirtys[__.AddTime]) AddTime = DateTime.Now;
            //if (!Dirtys[__.UpDatetime]) UpDatetime = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Article[文章]数据……");

        //    var entity = new Article();
        //    entity.Id = 0;
        //    entity.KId = 0;
        //    entity.Title = "abc";
        //    entity.SubTitle = "abc";
        //    entity.Content = "abc";
        //    entity.Keyword = "abc";
        //    entity.Description = "abc";
        //    entity.LinkURL = "abc";
        //    entity.TitleColor = "abc";
        //    entity.TemplateFile = "abc";
        //    entity.IsRecommend = 0;
        //    entity.IsBest = 0;
        //    entity.IsHide = 0;
        //    entity.IsLock = 0;
        //    entity.IsDel = 0;
        //    entity.IsTop = 0;
        //    entity.IsBest = 0;
        //    entity.IsComment = 0;
        //    entity.IsMember = 0;
        //    entity.Hits = 0;
        //    entity.Sequence = 0;
        //    entity.CommentCount = 0;
        //    entity.Icon = "abc";
        //    entity.ClassName = "abc";
        //    entity.BannerImg = "abc";
        //    entity.Pic = "abc";
        //    entity.AdsId = 0;
        //    entity.Tags = "abc";
        //    entity.Origin = "abc";
        //    entity.OriginURL = "abc";
        //    entity.ItemImg = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Article[文章]数据！");
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
        private ArticleCategory _ArticleKind;
        /// <summary>该文章所对应的栏目</summary>
        public ArticleCategory ArticleKind
        {
            get
            {
                //if (_ArticleKind == null && KId > 0 && !Dirtys.ContainsKey("ArticleKind_" + KId))
                //{
                //    _ArticleKind = ArticleKind.Find("Id", KId);
                //    Dirtys["ArticleKind_" + KId] = true;
                //}
                //return _ArticleKind;

                _ArticleKind = ArticleCategory.FindById(KId);
                return _ArticleKind;

            }
            set { _ArticleKind = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Article FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.Id == id);
        }
        #endregion

        #region 高级查询
        /// <summary>
        /// 通过FileName查找
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="kid"></param>
        /// <returns></returns>
        public static Article FindByFileName(string filename,int kid=0)
        {
            var where = _.FileName == filename;
            if (kid > 0) where &= _.KId == kid;
            return Find(where);
        }


        #endregion

        #region 业务操作


        /// <summary>
        /// 获取上一条记录/前一条记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static Article GetPrev(int id)
        {
            Article entity = Article.FindById(id);
            if (entity != null)
            {
                //获取上一条记录
                IList<Article> list = Article.FindAll(_.Id > entity.Id & _.KId == entity.KId & _.Sequence <= entity.Sequence, string.Format("{0} Desc,{1} Asc", _.Sequence, _.Id), null, 0, 1);
                if (list != null && list.Count > 0)
                    return list[0];
                else
                {
                    list = Article.FindAll(_.Id != entity.Id & _.KId == entity.KId & _.Sequence < entity.Sequence, string.Format("{0} Desc,{1} Asc", _.Sequence, _.Id), null, 0, 1);
                    if (list != null && list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                        return null;
                }

            }
            else
                return null;
        }

        /// <summary>
        /// 获取下一条记录/后一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Article GetNext(int id)
        {
            Article entity = Article.FindById(id);
            if (entity != null)
            {
                //获取上一条记录
                IList<Article> list = Article.FindAll(_.Id != entity.Id & _.KId == entity.KId & _.Sequence > entity.Sequence, string.Format("{0} Asc,{1} Desc", _.Sequence, _.Id), null, 0, 1);
                if (list != null && list.Count > 0)
                    return list[0];
                else
                {
                    list = Article.FindAll(_.Id < entity.Id & _.KId == entity.KId & _.Sequence >= entity.Sequence, string.Format("{0} Asc,{1} Desc", _.Sequence, _.Id), null, 0, 1);
                    if (list != null && list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                        return null;
                }

            }
            else
                return null;
        }


        /// <summary>
        /// 获取前几条文章
        /// </summary>
        /// <param name="kid">栏目id</param>
        /// <param name="records">条数</param>
        /// <returns></returns>
        public static IList<Article> FindTopList(int kid, int records, bool isShowSub = false)
        {
            if (!isShowSub)
                return FindAll(_.KId == kid & _.IsHide == 0, _.Id.Desc(), null, 0, records);
            else
            {
                Expression ex = _.IsHide == 0;
                List<int> kids = new List<int>();
                kids.Add(kid);
                IList<ArticleCategory> subkinds = ArticleCategory.FindByParentID(kid);
                if (subkinds != null && subkinds.Count > 0)
                {
                    foreach (var item in subkinds)
                    {
                        kids.Add(item.Id);
                    }
                }
                ex &= Article._.KId.In(kids);

                return FindAll(ex, _.Id.Desc(), null, 0, records);
            }
        }
        #endregion
    }
}