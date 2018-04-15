using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMCMS.Core.Models
{
    #region 0:代码型广告
    /// <summary>
    /// 实体类ScriptAds 代码型广告
    /// </summary>
    [Serializable]
    public class ScriptAds
    {
        public ScriptAds()
        { }
        private string _content;
        /// <summary>
        /// 广告代码
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
    }
    #endregion

    #region 1:文字型广告
    /// <summary>
    /// 实体类 TextAds 文字型广告
    /// </summary>
    [Serializable]
    public class TextAds
    {
        public TextAds()
        { }
        private string _txt;
        private string _link;
        private string _style;
        /// <summary>
        /// 文字内容
        /// </summary>
        public string txt
        {
            set { _txt = value; }
            get { return _txt; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 文字样式名称
        /// </summary>
        public string style
        {
            set { _style = value; }
            get { return _style; }
        }
    }
    #endregion

    #region 2:图片广告
    /// <summary>
    /// 实体类 ImgAds 图片类型广告
    /// </summary>
    [Serializable]
    public class ImgAds
    {
        public ImgAds()
        { }
        private string _img;
        private string _link;
        private int _width = 0;
        private int _height = 0;
        private string _alt;
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 图片宽度，0为不设置
        /// </summary>
        public int width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 图片高度，0为不设置
        /// </summary>
        public int height
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 替换文字
        /// </summary>
        public string alt
        {
            set { _alt = value; }
            get { return _alt; }
        }
    }

    #endregion

    #region 3:Flash广告
    /// <summary>
    /// 实体类 FlashAds，Flash类型广告
    /// </summary>
    [Serializable]
    public class FlashAds
    {
        public FlashAds()
        { }
        private string _swf;
        private int _width = 0;
        private int _height = 0;
        /// <summary>
        /// Flash地址
        /// </summary>
        public string swf
        {
            set { _swf = value; }
            get { return _swf; }
        }
        /// <summary>
        /// 图片宽度，0为不设置
        /// </summary>
        public int width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 图片高度，0为不设置
        /// </summary>
        public int height
        {
            set { _height = value; }
            get { return _height; }
        }

    }
    #endregion

    // 4:幻灯片广告 即图片广告列表
    // 5:漂浮广告 图片广告一样

    #region 6:对联广告
    /// <summary>
    /// 对联漂浮广告实体类 TwoFloatAds
    /// </summary>
    [Serializable]
    public class TwoFloatAds
    {
        public TwoFloatAds()
        { }
        //左边
        private string _limg;
        private string _llink;
        private int _lwidth = 0;
        private int _lheight = 0;
        private string _lalt;
        //右边
        private string _rimg;
        private string _rlink;
        private int _rwidth = 0;
        private int _rheight = 0;
        private string _ralt;

        /// <summary>
        /// 左边图片地址
        /// </summary>
        public string limg
        {
            set { _limg = value; }
            get { return _limg; }
        }
        /// <summary>
        /// 左边链接地址
        /// </summary>
        public string llink
        {
            set { _llink = value; }
            get { return _llink; }
        }
        /// <summary>
        /// 左边图片宽度，0为不设置
        /// </summary>
        public int lwidth
        {
            set { _lwidth = value; }
            get { return _lwidth; }
        }
        /// <summary>
        /// 左边图片高度，0为不设置
        /// </summary>
        public int lheight
        {
            set { _lheight = value; }
            get { return _lheight; }
        }
        /// <summary>
        /// 左边图片替换文字
        /// </summary>
        public string lalt
        {
            set { _lalt = value; }
            get { return _lalt; }
        }
        //-------右边-------------------
        /// <summary>
        /// 右边图片地址
        /// </summary>
        public string rimg
        {
            set { _rimg = value; }
            get { return _rimg; }
        }
        /// <summary>
        /// 右边链接地址
        /// </summary>
        public string rlink
        {
            set { _rlink = value; }
            get { return _rlink; }
        }
        /// <summary>
        /// 右边图片宽度，0为不设置
        /// </summary>
        public int rwidth
        {
            set { _rwidth = value; }
            get { return _rwidth; }
        }
        /// <summary>
        /// 右边图片高度，0为不设置
        /// </summary>
        public int rheight
        {
            set { _rheight = value; }
            get { return _rheight; }
        }
        /// <summary>
        /// 右边图片替换文字
        /// </summary>
        public string ralt
        {
            set { _ralt = value; }
            get { return _ralt; }
        }
    }
    #endregion

    #region 7:视频广告
    /// <summary>
    /// 视频广告
    /// </summary>
    [Serializable]
    public class VideoAds
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string poster { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; set; } = 0;
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; set; } = 0;
    }
    #endregion
}
