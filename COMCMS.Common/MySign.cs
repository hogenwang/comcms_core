using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMCMS.Common
{
    public class MySign
    {
        #region 验证签名
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static bool CheckSign(SortedDictionary<string, string> requestData, string signature)
        {
            //判断
            if (!requestData.ContainsKey("timeStamp")) return false;
            if (!requestData.ContainsKey("random")) return false;
            if (requestData.ContainsKey("signature"))
            {
                requestData.Remove("signature");
            }
            string signdata = CreateSignString(requestData);
            //NewLife.Log.XTrace.WriteLine("拼接字符：" + signdata);
            string mysign = Utils.MD5(signdata + Utils.SIGNSALT);
            //NewLife.Log.XTrace.WriteLine("签名：" + mysign);
            return mysign.ToUpper() == signature.ToUpper();
        }
        #endregion

        #region 根据字典拼接
        public static string CreateSignString(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Value);
            }
            return prestr.ToString();
        }
        #endregion
    }
}
