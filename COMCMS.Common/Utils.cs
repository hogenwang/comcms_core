using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Reflection;
using System.Configuration;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace COMCMS.Common
{
    /// <summary>
    /// 常用函数帮助类
    /// </summary>
    public class Utils
    {

        public static IConfiguration Configuration { get; set; }

        static Utils()
        {
            ////ReloadOnChange = true 当appsettings.json被修改时重新加载            
            //Configuration = new ConfigurationBuilder()
            //.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            //.Build();
        }

        public static void AddUtils(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region 静态变量
        /// <summary>
        /// 得到正则编译参数设置
        /// </summary>
        /// <returns>参数设置</returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }


        public static string PrefixKey
        {
            get
            {
                string strPrefixKey = Configuration["SystemSetting:COMCMSPrefixKey"];// ConfigurationManager.AppSettings["COMCMSPrefixKey"];
                if (string.IsNullOrEmpty(strPrefixKey))
                {
                    strPrefixKey = "comcmsntc_";
                }
                return strPrefixKey;
            }
        }

        /// <summary>
        /// 与小程序验签的盐值
        /// </summary>
        public static string SIGNSALT
        {
            get
            {
                string strPrefixKey = Configuration["SystemSetting:SignSalt"];
                if (string.IsNullOrEmpty(strPrefixKey))
                {
                    strPrefixKey = "comcms";
                }
                return strPrefixKey;
            }
        }


        /// <summary>
        /// 订单状态 "未确认", "已确认", "已完成", "已取消" 
        /// </summary>
        public static string[] OrdersState = { "未确认", "已确认", "已完成", "已取消" };
        /// <summary>
        /// 支付状态 { "未支付", "已支付", "已退款" }
        /// </summary>
        public static string[] PaymentState = { "未支付", "已支付", "已退款" };
        /// <summary>
        /// 配送状态 "未配送", "配货中", "已配送", "已收到", "退货中", "已退货"
        /// </summary>
        public static string[] DeliverState = { "未配送", "配货中", "已配送", "已收到", "退货中", "已退货" };
        /// <summary>
        /// cms类型
        /// </summary>
        public enum CMSType
        {
            /// <summary>
            /// 文章分类
            /// </summary>
            ArticleCategory=0,
            /// <summary>
            /// 文章
            /// </summary>
            Article=1,
            /// <summary>
            /// 商品分类
            /// </summary>
            ProductCategory=2,
            /// <summary>
            /// 商品详情
            /// </summary>
            Product=3,
            /// <summary>
            /// 首页
            /// </summary>
            Home=99
        }
        #endregion

        #region 验证部分
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            //return Regex.IsMatch(str, @"^[0-9]*$");
            return Regex.IsMatch(str, @"^(-?)(\d+)$");
        }
        /// <summary>
        /// 验证是否为字母或者数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsWords(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 判断是否为正确的
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFilePath(string str)
        {
            //A-Z, a-z, 0-9, -, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9-\/]");
        }
        /// <summary>
        /// 判断静态化文件名是否正确
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool ChekHTMLFileNameIsOK(string filename)
        {
            return Regex.IsMatch(filename, @"^(?!index\b)[a-zA-Z0-9-]+(\.html)$");
        }

        /// <summary>
        /// 验证对象是否为货币格式
        /// </summary>
        /// <param name="obj">验证对象</param>
        /// <returns>货币格式返回true，否则返回false</returns>
        public static bool IsDecimal(object obj)
        {
            bool flag = true;
            if (obj.GetType() != decimal.Parse("10.11").GetType())
            {
                try
                {
                    decimal.Parse((string)obj);
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        /// <summary>
        /// 获取当前网站根目录网址 不带 /
        /// </summary>
        /// <returns></returns>
        public static string GetServerUrl()
        {
            string http = "http://";
            if (MyHttpContext.Current.Request.IsHttps) //判断是否是https
            {
                http = "https://";
            }
            string port = MyHttpContext.Current.Request.Host.Port.ToString();
            if (string.IsNullOrEmpty(port) || port == "80" || port == "443")
                return http + MyHttpContext.Current.Request.Host.Host;
            else
                return http + MyHttpContext.Current.Request.Host.Host + ":" + port;

        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        /// <summary>
        /// 验证是否为手机号码
        /// </summary>
        /// <param name="str">手机号码</param>
        /// <returns></returns>
        public static bool IsTel(string str)
        {
            if (str.Length != 11)
                return false;
            return Regex.IsMatch(str, @"^1[3|4|5|7|8|9][0-9]\d{4,8}$");
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] userip = Utils.SplitString(ip, @".");

            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = Utils.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                        return true;

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                            r++;
                        else
                            break;
                    }
                    else
                        break;
                }

                if (r == 4)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!string.IsNullOrWhiteSpace(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }
        /// <summary>
        /// 判断元素是否在列表中
        /// </summary>
        /// <param name="strin">判断字符</param>
        /// <param name="arraystring">使用,号隔开</param>
        /// <returns></returns>
        public static bool IsInArrayString(string strin, string arraystring)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(arraystring))
            {
                string[] srrids = arraystring.Split(new string[] { "," }, StringSplitOptions.None);
                List<string> kids = new List<string>();
                foreach (string s in srrids)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        kids.Add(s.Trim());//加入允许使用的商品列表
                    }
                }
                if (kids.FindIndex(s => s == strin) >= 0)
                    flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckCardId(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length != 18)
                return false;
            string number17 = str.Substring(0, 17);
            string number18 = str.Substring(17);
            string check = "10X98765432";
            int[] num = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            int sum = 0;
            for (int i = 0; i < number17.Length; i++)
            {
                sum += Convert.ToInt32(number17[i].ToString()) * num[i];
            }
            sum %= 11;
            if (number18.Equals(check[sum].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }
        #endregion

        #region 获取部分

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="path">路径，如：SystemSetting:COMCMSPrefixKey</param>
        /// <returns></returns>
        public static string GetSetting(string path)
        {
            string value = Configuration[path];

            return value;
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.UTF8.GetBytes(str).Length;
        }
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            using(var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var result = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        /// <summary>
        /// 获取指定文件的扩展名
        /// </summary>
        /// <param name="fileName">指定文件名</param>
        /// <returns>扩展名(.xxx)</returns>
        public static string GetFileExtName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || fileName.IndexOf('.') <= 0)
                return "";

            fileName = fileName.ToLower().Trim();

            return fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
        }

        private static Random rd = new Random();
        /// <summary>
        /// 获取随机整数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int GetRND(int min, int max)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(min, max);
        }
        /// <summary>
        /// 生成上传文件保存名字
        /// </summary>
        /// <returns>不带扩展名名字</returns>
        public static string CreateUploadSaveName()
        {
            string saveName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + GetRND(1000, 9999).ToString();
            return saveName;
        }
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns>订单号</returns>
        public static string GetOrderNum()
        {
            string ordernum = DateTime.Now.ToString("yyyyMMddHHmm") + GetRND(1000, 9999).ToString();
            return ordernum;
        }
        private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="strLength">字符长度</param>
        /// <returns>返回随机字符串</returns>
        public static string GetRandomChar(int strLength)
        {
            StringBuilder newRandom = new StringBuilder(62);
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < strLength; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 获取当前附近坐标点
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="aboutRadius">附近半径，千米，默认0.5=500米</param>
        /// <returns>一维数组：0：最小经度，1：最大经度；2：最小纬度；3：最大纬度</returns>
        public static double[] FindNeighPosition(double longitude, double latitude, double aboutRadius = 0.5)
        {
            double[] result = new double[4];
            //先计算查询点的经纬度范围  
            double r = 6371;//地球半径千米  
            double dis = aboutRadius;//0.5千米距离  
            double dlng = 2 * Math.Asin(Math.Sin(dis / (2 * r)) / Math.Cos(latitude * Math.PI / 180));
            dlng = dlng * 180 / Math.PI;//角度转为弧度  
            double dlat = dis / r;
            dlat = dlat * 180 / Math.PI;
            double minlat = latitude - dlat;
            double maxlat = latitude + dlat;
            double minlng = longitude - dlng;
            double maxlng = longitude + dlng;
            result[0] = minlng;
            result[1] = maxlng;
            result[2] = minlat;
            result[3] = maxlat;
            return result;
        }
        /// <summary>
        /// 获取文件名字，不带扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string fileName)
        {
            int length = fileName.Length - 1, dotPos = fileName.IndexOf(".");

            if (dotPos == -1)
                return fileName;

            return fileName.Substring(0, dotPos);
        }
        /// <summary>
        /// 创建一个短的GUID
        /// </summary>
        /// <returns>字符串类型</returns>
        public static string GetShortGUId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
                return false;

            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }

        /// <summary>
        /// 获取HTML里面首张图片
        /// </summary>
        /// <param name="html">html内容</param>
        /// <returns>首张图片地址，为空则图片不存在</returns>
        public static string GetHTML1stImgSrc(string html)
        {
            string imgsrc = "";
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(html);
            if (m.Count > 0)
            {
                for (int i = 0; i < m.Count; i++)
                {
                    if (!string.IsNullOrEmpty(m[i].Groups[1].Value) && IsImgFilename(m[i].Groups[1].Value.ToString()))
                    {
                        imgsrc = m[i].Groups[1].Value.ToString();
                        break;
                    }
                }
            }
            else
            {
                imgsrc = "";
            }
            return imgsrc;

        }
        /// <summary>
        /// 获取所有图片
        /// </summary>
        /// <param name="pic">图片字符串</param>
        /// <returns>图片地址</returns>
        public static List<string> GetAllPic(string pic)
        {
            if (!string.IsNullOrEmpty(pic))
            {
                List<string> list = new List<string>();
                string[] arrpic = pic.Split(new string[] { "|||" }, StringSplitOptions.None);
                foreach (string picurl in arrpic)
                    list.Add(picurl);
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex = startIndex - length;
                }

                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0)
                    return "";
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                        return "";
                }
            }

            if (str.Length - startIndex < length)
                length = str.Length - startIndex;

            return str.Substring(startIndex, length);
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="strInput">字符串</param>
        /// <param name="intLen">截取长度</param>
        /// <returns></returns>
        public static string CutString(string strInput, int intLen)
        {
            if (string.IsNullOrEmpty(strInput))
                return "";
            strInput = strInput.Trim();
            byte[] myByte = Encoding.UTF8.GetBytes(strInput);
            //Response.Write("cutString Function is::" + myByte.Length.ToString());
            if (myByte.Length > intLen + 2)
            {
                //截取操作
                string resultStr = "";
                for (int i = 0; i < strInput.Length; i++)
                {
                    byte[] tempByte = Encoding.UTF8.GetBytes(resultStr);
                    if (tempByte.Length < intLen)
                    {

                        resultStr += strInput.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return resultStr + "...";
            }
            else
            {
                return strInput;
            }
        }
        /// <summary>
        /// 判断是否是GUID
        /// </summary>
        /// <param name="strSrc"></param>
        /// <returns></returns>
        public static bool IsGuidByParse(string strSrc)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(strSrc, out g);
        }

        /// <summary>
        /// 获取广告类型
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static string GetAdsType(int tid)
        {
            string tname = "代码广告";
            switch (tid)
            {
                case 0:
                    break;
                case 1:
                    tname = "文字广告";
                    break;
                case 2:
                    tname = "图片广告";
                    break;
                case 3:
                    tname = "Flash广告";
                    break;
                case 4:
                    tname = "幻灯片广告";
                    break;
                case 5:
                    tname = "漂浮广告";
                    break;
                case 6:
                    tname = "对联广告";
                    break;
            }
            return tname;
        }
        /// <summary>
        /// 获取缩略图地址
        /// </summary>
        /// <param name="imgsrc"></param>
        /// <returns></returns>
        public static string GetThumbImg(string imgsrc)
        {
            if (!string.IsNullOrEmpty(imgsrc))
            {
                return imgsrc.Replace("/userfiles/", "/userfiles/_thumbs/");
            }
            else
                return "";
        }

        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns>用户IP地址</returns>
        public static string GetIP()
        {
            string userHostAddress = MyHttpContext.Current.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //2019-05-11 增加反向代理获取真实IP
            string xForwardedForAddress = MyHttpContext.Current.Request.Headers["X-Forwarded-For"];
            if (!string.IsNullOrEmpty(userHostAddress) && !string.IsNullOrEmpty(xForwardedForAddress) && userHostAddress != xForwardedForAddress)
            {
                return xForwardedForAddress;
            }
            if (!string.IsNullOrEmpty(userHostAddress) && Utils.IsIP(userHostAddress))
            {
                if (userHostAddress == "::1")
                    userHostAddress = "127.0.0.1";
                return userHostAddress;
            }
            return "127.0.0.1";
        }


        /// <summary>
        /// 时间戳转换成DateTime格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToDateTime(string timeStamp)
        {
            //TimeZoneInfo.ConvertTimeFromUtc()

            DateTime dateTimeStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>  
        /// 获取JS时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetJSTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 获取当前URL绝对地址
        /// </summary>
        /// <returns></returns>
        public static string GetNowURL()
        {
            string url = GetServerUrl() + MyHttpContext.Current.Request.Path;
            string querystring = MyHttpContext.Current.Request.QueryString.ToString();
            if (!string.IsNullOrWhiteSpace(querystring))
                url = url + querystring;

            return url;
        }

        /// <summary>
        /// 获取post表单金钱字段值
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        public static decimal GetPostFormMoney(string inputName)
        {
            string v = MyHttpContext.Current.Request.Form[inputName];
            decimal price = 0M;
            if (!IsDecimal(v) && decimal.Parse(v) < 0) v = "0";
            price = decimal.Parse(v);
            return price;
        }

        /// <summary>
        /// 获取get表单金钱字段值
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        public static decimal GetQueryStringMoney(string inputName)
        {
            string v = MyHttpContext.Current.Request.Query[inputName];
            decimal price = 0M;
            if (!IsDecimal(v) && decimal.Parse(v) < 0) v = "0";
            price = decimal.Parse(v);
            return price;
        }

        /// <summary>
        /// 获取当前系统运行平台
        /// </summary>
        /// <returns></returns>
        public static string GetOSPlatform()
        {
            string osPlatform = "Unknown";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                osPlatform = "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                osPlatform = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                osPlatform = "OSX";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                osPlatform = "FreeBSD";
            }

            return osPlatform;
        }

        #endregion

        #region 处理部分
        //常用处理函数
        /// <summary>
        /// 格式化处理SQL字符串
        /// </summary>
        /// <param name="str">替换单引号后的字符串</param>
        /// <returns>格式化后的SQL字符串</returns>
        public static string SqlStr(string str)
        {
            string re = "";
            try
            {
                re = str.Replace("'", "''");
            }
            catch
            {
                re = "";
            }
            return re;
        }
        /// <summary>
        ///一个处理危险HTML标签的方法
        /// </summary>
        /// <param name="M_Htmlstring">要处理的字符串</param>
        /// <returns></returns>
        public static string NoHTML(string M_Htmlstring) //去除HTML标记 
        {
            if (string.IsNullOrWhiteSpace(M_Htmlstring))
                return "";
            //删除脚本 
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"<script[^>]*?>.*? </script>", string.Empty, RegexOptions.IgnoreCase);
            //删除HTML 
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"<(.?[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);//空格换行
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"-->", string.Empty, RegexOptions.IgnoreCase);//注释
            M_Htmlstring = Regex.Replace(M_Htmlstring, @" <!--.*", string.Empty, RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(quot|34);", "\"", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(amp|38);", "&", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(lt|60);", " <", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(gt|62);", ">", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(nbsp|160);", " ", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(iexcl|161);", "\xa1", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(cent|162);", "\xa2", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(pound|163);", "\xa3", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"&(copy|169);", "\xa9", RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @" href *= *[\s\S]*script *:", string.Empty, RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @" on[\s\S]*=", string.Empty, RegexOptions.IgnoreCase);//过滤其它控件的on...事件
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"<iframe[\s\S]+</iframe *>", string.Empty, RegexOptions.IgnoreCase);//过滤iframe
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"<frameset[\s\S]+</frameset *>", string.Empty, RegexOptions.IgnoreCase);//过滤frameset
            M_Htmlstring = Regex.Replace(M_Htmlstring, @"<", string.Empty, RegexOptions.IgnoreCase);
            M_Htmlstring = Regex.Replace(M_Htmlstring, @">", string.Empty, RegexOptions.IgnoreCase);
            // M_Htmlstring.Replace("\r\n", string.Empty);
            M_Htmlstring = M_Htmlstring.Trim();
            return M_Htmlstring;
        }
        /// <summary>
        /// 普通过滤脚本、框架
        /// </summary>
        /// <param name="M_Html">要处理的字符串</param>
        /// <returns>去除脚本、框架、style样式标记</returns>
        public static string NoScriptAndIframe(string M_Html)
        {
            if (!string.IsNullOrEmpty(M_Html))
            {
                M_Html = Regex.Replace(M_Html, @"^<style\\s*[^>]*>([^>]|[~<])*<\\/style>$", string.Empty, RegexOptions.IgnoreCase);
                M_Html = Regex.Replace(M_Html, @"<script[^>]*?>.*? </script>", string.Empty, RegexOptions.IgnoreCase);
                M_Html = Regex.Replace(M_Html, @"<iframe[\s\S]+</iframe *>", string.Empty, RegexOptions.IgnoreCase);//过滤iframe
                M_Html = Regex.Replace(M_Html, @"<frameset[\s\S]+</frameset *>", string.Empty, RegexOptions.IgnoreCase);//过滤frameset
            }
            else
            {
                M_Html = "";
            }
            return M_Html.Trim();
        }
        /// <summary>    
        /// 过滤字符串中的html代码    
        /// </summary>    
        /// <param name="Str"></param>    
        /// <returns>返回过滤之后的字符串</returns>    
        public static string LostHTML(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return "";

            string Re_Str = "";
            string Pattern = "<\\/*[^<>]*>";
            Re_Str = Regex.Replace(Str, Pattern, "");
            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Proxy = null;
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// 后台发送POST请求
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="data">发送的数据</param>
        /// <returns></returns>
        public static Stream HttpPost(string url, string data)
        {
            try
            {
                //创建post请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                byte[] payload = Encoding.UTF8.GetBytes(data);
                request.ContentLength = payload.Length;


                //发送post的请求
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();

                //接受返回来的数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                return stream;
                //StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                //string value = reader.ReadToEnd();

                //reader.Close();
                //stream.Close();
                //response.Close();

                //return value;
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                return null;
            }
        }

        /// <summary>
        /// 后台发送POST请求
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="data">发送的数据</param>
        /// <returns></returns>
        public static string HttpPostAndReturnString(string url, string data)
        {
            try
            {
                //创建post请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                byte[] payload = Encoding.UTF8.GetBytes(data);
                request.ContentLength = payload.Length;


                //发送post的请求
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();

                //接受返回来的数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string value = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();

                return value;
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                return "";
            }
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }
        #endregion

        #region COMCMS类型

        /// <summary>
        /// 内置类型
        /// </summary>
        public enum MyType
        {
            文章 = 0,
            商品 = 1,
            订餐 = 2,
            酒店 = 3,
            分销商认证 = 999
        }
        #endregion

        private static String[] _Excludes = new[] { "Loopback", "VMware", "VBox", "Virtual", "Teredo", "Microsoft", "VPN", "VNIC", "IEEE" };
        /// <summary>获取所有网卡MAC地址</summary>
        /// <returns></returns>
        public static IEnumerable<Byte[]> GetMacs()
        {
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (_Excludes.Any(e => item.Description.Contains(e))) continue;
                if (item.Speed < 1_000_000) continue;

                var addrs = item.GetIPProperties().UnicastAddresses.Where(e => e.Address.AddressFamily == AddressFamily.InterNetwork).ToArray();
                if (addrs.All(e => IPAddress.IsLoopback(e.Address))) continue;

                var mac = item.GetPhysicalAddress()?.GetAddressBytes();
                if (mac != null && mac.Length == 6) yield return mac;
            }
        }

    }

    #region 常用扩展方法
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class CommExtension
    {
        #region 货币转换
        /// <summary>
        /// 数据库中金额换成元单位
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal ConvertToMoney(this int price)
        {
            decimal realPrice = (decimal)price / 100;
            return realPrice;
        }

        /// <summary>
        /// 元单位的金额换算成分
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static int ConvertMoneyToInt(this decimal price)
        {
            return (int)(price * 100);
        }
        #endregion

    }
    #endregion
}
