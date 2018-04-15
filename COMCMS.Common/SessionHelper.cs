using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using NewLife.Log;

namespace COMCMS.Common
{
    public class SessionHelper
    {
        #region 获取Session
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static object GetSession(string strName)
        {
            byte[] result;
            try
            {
                MyHttpContext.Current.Session.TryGetValue(strName, out result);
                if (result == null || result.Length <= 0)
                    return "";
                object strResult = ByteConvertHelper.Bytes2Object<object>(result);
                if (strResult == null)
                {
                    return "";
                }
                return strResult;
            }
            catch(Exception ex)
            {
                XTrace.WriteLine($"获取Session出错：{ex.Message}");
                return "";
            }

        }
        #endregion

        #region 写入Session
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="value">值</param>
        public static void WriteSession(string strName, object value)
        {
            MyHttpContext.Current.Session.Set(strName, ByteConvertHelper.Object2Bytes(value));
        }
        #endregion

    }
}
