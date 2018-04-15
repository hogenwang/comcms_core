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
}
