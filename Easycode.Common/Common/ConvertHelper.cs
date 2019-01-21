using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Common
{
    /// <summary>
    /// 值转换帮助类
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// 转换为枚举类型
        /// </summary>
        /// <typeparam name="T">返回枚举类型</typeparam>
        /// <param name="strValue">需要转换的字符串</param>
        /// <returns></returns>
        public static T StrToEnum<T>(string strValue) where T : struct
        {
            if (typeof(T).BaseType != typeof(Enum))
                throw new ArgumentException("泛型类型不正确，请使用枚举类型");
            if (string.IsNullOrEmpty(strValue))
                return default(T);
            strValue = strValue.Trim();
            if (Enum.TryParse(strValue, out T defValue))
                return defValue;

            return defValue;
        }


        /// <summary>
        /// 通过时间戳字符串得到时间
        /// </summary>
        /// <param name="timestamp">时间戳字符串</param>
        /// <returns>错误返回当前时间</returns>
        public static DateTime UnixToDateTime(string timestamp)
        {
            if (string.IsNullOrEmpty(timestamp))
                return DateTime.Now;
            timestamp = timestamp.Trim();
            long time = 0;
            if (!long.TryParse(timestamp, out time))
                return DateTime.Now;
            //得到当地时区时间 以1970/01/01 8:00:00为起始时间
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dt = startTime.AddSeconds(time);
            //时间戳包含毫秒数
            if (timestamp.Length == 13)
            {
                dt = startTime.AddMilliseconds(time);
            }
            return dt;
        }


        /// <summary>
        /// 将字符串转换为byte数组
        /// </summary>
        /// <param name="content">要转换的字符串</param>
        /// <returns>转换之后的byte数组</returns>
        public static byte[] StrToBytes(string content)
        {
            return Encoding.UTF8.GetBytes(content);
        }
    
    }
}
