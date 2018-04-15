using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COMCMS.Common;

namespace COMCMS.Core.Models
{
    /// <summary>
    /// 附件配置
    /// </summary>
    public class AttachConfigEntity
    {
        public AttachConfigEntity()
        { }
        #region Model
        private string _attachpatch = "userfiles";
        private int _savetype = 0;
        private int _iscreatethum = 0;
        private int _thumqty = 80;
        private int _thummaxwidth = 200;
        private int _thummaxheight = 200;
        private int _iswatermark = 0;
        private int _watermarktype = 0;
        private int _watermarkminwidth = 400;
        private int _watermarkminheight = 400;
        private string _watermarkimg = "";
        private string _watermarktext = string.Empty;
        private string _watermarktextcolor = "#0FF";
        private int _watermarkplace = 9;
        private int _watermarkqty = 80;
        private int _watermarkdiaphaneity = 80;

        private int _siteid = 0;
        /// <summary>
        /// 网站ID
        /// </summary>
        public int SiteId
        {
            get { return _siteid; }
            set { _siteid = value; }
        }

        /// <summary>
        /// 附件目录,图片自动在这个目录后面加上images/、文件files/、flash:flash/、多媒体:media/
        /// </summary>
        public string AttachPatch
        {
            set { _attachpatch = value; }
            get { return _attachpatch; }
        }
        /// <summary>
        /// 存放类型，0统一目录，1按月份（推荐），2按天
        /// </summary>
        public int SaveType
        {
            set { _savetype = value; }
            get { return _savetype; }
        }
        /// <summary>
        /// 是否生成缩略图，0不生成，1生成不超过指定大小的缩略图，2生成固定大小的缩略图 缩略图存放在附件目录/_thumbs目录
        /// </summary>
        public int IsCreateThum
        {
            set { _iscreatethum = value; }
            get { return _iscreatethum; }
        }
        /// <summary>
        /// 缩略图质量 100最高
        /// </summary>
        public int ThumQty
        {
            set { _thumqty = value; }
            get { return _thumqty; }
        }
        /// <summary>
        /// 缩略图最大宽度
        /// </summary>
        public int ThumMaxWidth
        {
            set { _thummaxwidth = value; }
            get { return _thummaxwidth; }
        }
        /// <summary>
        /// 缩略图最大高度
        /// </summary>
        public int ThumMaxHeight
        {
            set { _thummaxheight = value; }
            get { return _thummaxheight; }
        }
        /// <summary>
        /// 是否开启图片加水印
        /// </summary>
        public int IsWaterMark
        {
            set { _iswatermark = value; }
            get { return _iswatermark; }
        }
        /// <summary>
        /// 水印类型，0文字，1 GIF，2 JPG ，3 PNG
        /// </summary>
        public int WaterMarkType
        {
            set { _watermarktype = value; }
            get { return _watermarktype; }
        }
        /// <summary>
        /// 打水印最小宽度
        /// </summary>
        public int WaterMarkMinWidth
        {
            set { _watermarkminwidth = value; }
            get { return _watermarkminwidth; }
        }
        /// <summary>
        /// 打水印最小高度
        /// </summary>
        public int WaterMarkMinHeight
        {
            set { _watermarkminheight = value; }
            get { return _watermarkminheight; }
        }
        /// <summary>
        /// 水印图片
        /// </summary>
        public string WaterMarkImg
        {
            set { _watermarkimg = value; }
            get { return _watermarkimg; }
        }
        /// <summary>
        /// 文字水印文字
        /// </summary>
        public string WaterMarkText
        {
            set { _watermarktext = value; }
            get { return _watermarktext; }
        }
        /// <summary>
        /// 水印文字颜色
        /// </summary>
        public string WaterMarkTextColor
        {
            set { _watermarktextcolor = value; }
            get { return _watermarktextcolor; }
        }
        /// <summary>
        /// 水印位置：0～9
        /// </summary>
        public int WaterMarkPlace
        {
            set { _watermarkplace = value; }
            get { return _watermarkplace; }
        }
        /// <summary>
        /// 水印后图片质量 0-100
        /// </summary>
        public int WaterMarkQty
        {
            set { _watermarkqty = value; }
            get { return _watermarkqty; }
        }
        /// <summary>
        /// 水印图片透明度 0-100
        /// </summary>
        public int WaterMarkDiaphaneity
        {
            set { _watermarkdiaphaneity = value; }
            get { return _watermarkdiaphaneity; }
        }

        /// <summary>
        /// 是否随机命名上传文件名
        /// </summary>
        public int IsRandomFileName { get; set; } = 0;

        /// <summary>
        /// 图片最大宽度
        /// </summary>
        public int ImgMaxWidth { get; set; } = 1920;
        /// <summary>
        /// 图片最大高度
        /// </summary>
        public int ImgMaxHeight { get; set; } = 2000;

        #endregion Model
    }
}
