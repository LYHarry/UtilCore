using System;
using System.Collections.Generic;
using System.IO;
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
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="source">待转换字符串</param>
        /// <returns></returns>
        public static T ToEnum<T>(string source) where T : struct
        {
            Check.Argument.IsEmpty(source, nameof(source));
            var type = typeof(T);
            if (!type.IsEnum && type.BaseType != typeof(Enum))
                throw new ArgumentException("泛型类型不正确，T 必须为枚举类型.");
            if (Enum.TryParse(source.Trim(), out T defValue))
                return defValue;
            throw new ArgumentException($"{source}不属于{type.Name}泛型.");
        }

        /// <summary>
        /// 转换为枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="source">待转换数值</param>
        /// <returns></returns>
        public static T ToEnum<T>(int source) where T : struct
        {
            return ToEnum<T>(source.ToString());
        }


        /// <summary>
        /// 时间戳字符串转换为时间(DateTime)类型
        /// </summary>
        /// <param name="timestamp">待转换时间戳字符串</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string timestamp)
        {
            Check.Argument.IsEmpty(timestamp, nameof(timestamp));
            if (!long.TryParse(timestamp.Trim(), out long time))
                throw new ArgumentException($"{timestamp}不能转换为时间戳类型数值.");
            //得到当地时区时间 以1970/01/01 8:00:00为起始时间
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dt = startTime.AddSeconds(time);
            //时间戳包含毫秒数
            if (timestamp.Length == 13)
                dt = startTime.AddMilliseconds(time);
            return dt;
        }

        /// <summary>
        /// 时间戳转换为时间(DateTime)类型
        /// </summary>
        /// <param name="timestamp">待转换时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(long timestamp)
        {
            return ToDateTime(timestamp.ToString());
        }

        /// <summary>
        /// 转换为 bool 型
        /// </summary>
        /// <param name="source">待转换字符串</param>
        /// <returns>true/1/是/"true"/开 返回 true 否则 false </returns>
        public static bool ToBool(string source)
        {
            source = (source ?? string.Empty).Trim().ToLower();
            if (bool.TryParse(source, out bool value))
                return value;
            switch (source)
            {
                case "true":
                case "1":
                case "是":
                case "开":
                    return true;
            }
            return false;
        }


    }
}
