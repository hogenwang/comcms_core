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
    /// <summary>后台菜单</summary>
    public partial class AdminMenu : Entity<AdminMenu>
    {
        #region 对象操作
        static AdminMenu()
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

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化AdminMenu[后台菜单]数据……");

        //    var entity = new AdminMenu();
        //    entity.Id = 0;
        //    entity.MenuKey = "abc";
        //    entity.MenuName = "abc";
        //    entity.PermissionKey = "abc";
        //    entity.Description = "abc";
        //    entity.Link = "abc";
        //    entity.PId = 0;
        //    entity.Level = 0;
        //    entity.Location = "abc";
        //    entity.IsHide = 0;
        //    entity.Rank = 0;
        //    entity.Icon = "abc";
        //    entity.ClassName = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化AdminMenu[后台菜单]数据！"
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
        protected override Int32 OnInsert()
        {
            Location = GetNewLocation(PId);
            Level = GetNewLevel(PId);
            return base.OnInsert();
        }
        #endregion

        #region 扩展属性
        private IList<AdminMenuEvent> _ListEvent;
        /// <summary>
        /// 后台菜单权限详情
        /// </summary>
        public IList<AdminMenuEvent> ListEvent
        {
            get
            {
                if (_ListEvent == null && Id > 0 && !Dirtys["ListEvent_" + Id])
                {
                    _ListEvent = AdminMenuEvent.FindAll(AdminMenuEvent._.MenuId == Id, AdminMenuEvent._.Id.Asc(), null, 0, 0);
                    Dirtys["ListEvent_" + Id] = true;
                }
                return _ListEvent;
            }
            set { _ListEvent = value; }
        }
        #endregion

        #region 扩展查询
        /// <summary>根据Id查找</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AdminMenu FindById(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.Id, id);
            else // 实体缓存
                return Meta.Cache.Entities.FirstOrDefault(x => x.Id == id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }
        /// <summary>
        /// 根据父ID查询所有子项
        /// </summary>
        /// <param name="ID">父ID</param>
        /// <returns>所有子栏目，按Rank正序排列</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IList<AdminMenu> FindByParentID(Int32 ID)
        {

            IList<AdminMenu> list = Meta.Cache.Entities.Where(x => x.PId == ID).OrderBy(x => x.Id).OrderBy(x => x.Rank).ToList();
            if (list == null || list.Count < 1) return null;

            //list.Sort(_.Rank, true);
            //list.OrderBy(x => x.Rank);

            return list;
        }

        /// <summary>
        /// 获取所有子栏目(递归)
        /// </summary>
        /// <param name="ID">栏目ID</param>
        /// <returns>所有子栏目</returns>
        public static IList<AdminMenu> FindAllSubKinds(Int32 ID)
        {
            List<AdminMenu> alllist = new List<AdminMenu>();
            IList<AdminMenu> list = FindByParentID(ID);
            if (list != null && list.Count > 0)
            {
                alllist.AddRange(list);
                foreach (AdminMenu ak in list)
                {
                    if (AdminMenu.FindByParentID(ak.Id) != null)
                    {
                        alllist.AddRange(FindAllSubKinds(ak.Id));
                    }
                }
            }
            return alllist;
        }
        #endregion

        #region 高级查询
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="PId"></param>
        /// <returns></returns>
        public static String GetNewLocation(int PId)
        {
            if (PId == 0)
                return "0,";
            else
            {
                var pa = FindById(PId);
                if (pa != null)
                {
                    return GetNewLocation(pa.PId) + pa.Id + ",";
                }
                else
                {
                    return pa.Id + ",";
                }
            }
        }
        /// <summary>
        /// 获取Level
        /// </summary>
        /// <param name="PId"></param>
        /// <returns></returns>
        public static int GetNewLevel(int PId)
        {
            if (PId == 0)
                return 0;
            else
            {
                var pa = FindById(PId);
                if (pa != null)
                {
                    return GetNewLevel(pa.PId) + 1;
                }
                else
                {
                    return pa.Level + 1;
                }
            }
        }
        #endregion

        #region 业务操作
        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="pid">上级ID，0为全部</param>
        /// <param name="maxLevel">最大级别0为1级类别；-1为所有下级</param>
        /// <param name="isIndentation">是否缩进</param>
        /// <param name="showhide">是否显示隐藏菜单</param>
        /// <returns></returns>
        public static IList<AdminMenu> GetListTree(int pid, int maxLevel, bool isIndentation, bool showhide = true)
        {
            List<AdminMenu> listTree = new List<AdminMenu>();
            IList<AdminMenu> list = FindByParentID(pid);
            if (list != null && list.Count > 0)
            {
                foreach (AdminMenu p in list)
                {
                    if (!showhide && p.IsHide == 1)
                    {
                        continue;
                    }
                    else
                    {
                        listTree.Add(p.CloneEntity());
                        if ((maxLevel == -1 || p.Level < maxLevel) && AdminMenu.FindCount(_.PId == p.Id, null, null, 0, 0) > 0)
                        {
                            IList<AdminMenu> listtmp = GetListTree(p.Id, maxLevel, false);
                            foreach (AdminMenu a in listtmp)
                            {
                                if (!showhide && a.IsHide == 1) continue;//如果不显示隐藏，跳过
                                AdminMenu tmpa = a.CloneEntity();
                                if (a.Level > 0 && isIndentation)
                                {
                                    tmpa.MenuName = new string('　', tmpa.Level * 2) + tmpa.MenuName;
                                }
                                listTree.Add(tmpa);
                            }
                        }
                    }
                }
            }
            return listTree;
        }

        /// <summary>
        /// 获取下拉菜单树Option不包含 select
        /// </summary>
        /// <param name="pid">父ID</param>
        /// <param name="selectedId">默认选择ID</param>
        /// <param name="strpre">前缀，为空即可</param>
        /// <param name="myid">本身ID，0为所有</param>
        /// <returns></returns>
        public static string GetMenuSelectString(int pid, int selectedId, string strpre, int myid)
        {
            StringBuilder strOption = new StringBuilder();
            string strtpl = "<option value=\"{0}\" {2}>{1}</option>";
            IList<AdminMenu> list = AdminMenu.FindByParentID(pid);
            if (list != null)
            {
                foreach (AdminMenu menu in list)
                {
                    if (menu.Id != myid)
                    {
                        if (menu.Id == selectedId)
                            strOption.Append(string.Format(strtpl, menu.Id, strpre + menu.MenuName, "selected=\"selected\""));
                        else
                            strOption.Append(string.Format(strtpl, menu.Id, strpre + menu.MenuName, ""));
                        if (AdminMenu.FindByParentID(menu.Id) != null)
                        {
                            string tmp = strpre + "-- ";
                            strOption.Append(GetMenuSelectString(menu.Id, selectedId, tmp, myid));
                        }
                    }
                }
            }
            return strOption.ToString();
        }
        #endregion
    }
}