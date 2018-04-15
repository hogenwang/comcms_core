using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMCMS.Core.Models
{
    public class ShoppingCartDetail
    {
        #region Model
        private int _pid = 0;
        private int _qty = 0;
        private decimal _price = 0;
        private decimal _marketprice = 0;
        private string _itemimg;
        private int _tid = 0;

        /// <summary>
        /// 商品ID
        /// </summary>
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 商品号
        /// </summary>
        public string ItemNO
        {
            get;
            set;
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 商品市场价格
        /// </summary>
        public decimal MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string Pic
        {
            set { _itemimg = value; }
            get { return _itemimg; }
        }
        /// <summary>
        /// 规格尺寸
        /// </summary>
        public string Spec
        {
            get;
            set;
        }
        /// <summary>
        /// 商品颜色
        /// </summary>
        public string Color
        {
            get;
            set;
        }
        /// <summary>
        /// 其他属性，备用
        /// </summary>
        public string Attr
        {
            get;
            set;
        }
        private decimal _tax = 0M;
        /// <summary>
        /// 税
        /// </summary>
        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        private int _credit = 0;
        /// <summary>
        /// 积分
        /// </summary>
        public int Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        private int _Available = 0;
        /// <summary>
        /// 是否可用（可用才会真正结算）
        /// </summary>
        public int IAvailable
        {
            get { return _Available; }
            set { _Available = value; }
        }
        private int _shopid = 0;
        /// <summary>
        /// 商家ID，0为平台
        /// </summary>
        public int ShopId
        {
            get { return _shopid; }
            set { _shopid = value; }
        }
        private int _priceid = 0;
        /// <summary>
        /// 价格ID
        /// </summary>
        public int PriceId
        {
            get { return _priceid; }
            set { _priceid = value; }
        }
        /// <summary>
        /// 类别ID，Utils.MyType 
        /// </summary>
        public int TId
        {
            set { _tid = value; }
            get { return _tid; }
        }
        #endregion
    }
}
