using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMCMS.Web.Models
{
    /// <summary>
    /// 系统部分配置
    /// </summary>
    public class SystemSetting
    {
        /// <summary>
        /// 文件允许扩展名
        /// </summary>
        public string FileAllowedExtensions { get; set; }
        /// <summary>
        /// 图片允许扩展名
        /// </summary>
        public string ImageAllowedExtensions { get; set; }
        /// <summary>
        /// 多媒体允许扩展名
        /// </summary>
        public string MediaAllowedExtensions { get; set; }
        /// <summary>
        /// 图片文件扩展名（用于判断是否为图片）
        /// </summary>
        public string ImageExtensions { get; set; }
        /// <summary>
        /// JWT验证的密钥
        /// </summary>
        public string JwtSecret { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string COMCMSPrefixKey { get; set; }
        /// <summary>
        /// 签名盐
        /// </summary>
        public string SignSalt { get; set; }
        /// <summary>
        /// 密码强度
        /// 0:6位无要求
        /// 1:至少八个字符，至少一个字母和一个数字
        /// 2:至少八个字符，至少一个字母，一个数字和一个特殊字符
        /// 3:最少八个字符，至少一个大写字母，一个小写字母和一个数字
        /// 4:至少八个字符，至少一个大写字母，一个小写字母，一个数字和一个特殊字符
        /// </summary>
        public int PasswordStrength { get; set; } = 0;
    }
}
