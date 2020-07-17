using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMCMS.Core.Models
{
    public class APIModels
    {
    }

    #region 小程序商品购物车信息
    /// <summary>
    /// 小程序的商品购物车
    /// </summary>
    public class WXAppProductCart
    {
        public int id { get; set; } = 0;
        public int qty { get; set; } = 1;
        public string title { get; set; }
        public decimal price { get; set; }
        public string pic { get; set; }
        public bool selected { get; set; } = false;
    }
    /// <summary>
    /// 小程序提交订单购物车单项
    /// </summary>
    public class WXAppCheckoutProductCart
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int id { get; set; } = 0;
        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; } = 1;
    }
    #endregion

    #region 系统文件目录信息列表
    /// <summary>
    /// 系统文件目录信息
    /// </summary>
    public class DirInfo
    {
        /// <summary>
        /// 目录名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 目录详情
        /// </summary>
        public string fullPath { get; set; }
        /// <summary>
        /// 子目录
        /// </summary>
        public List<DirInfo> subList { get; set; }
    }
    #endregion
}
