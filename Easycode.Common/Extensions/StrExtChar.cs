using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Extensions
{
    /// <summary>
    /// 字符串扩展帮助类
    /// <para>处理特殊字符串</para>
    /// </summary>
    public static partial class StrExtension
    {
        /// <summary>
        /// 截取掉两边空格
        /// </summary>
        /// <param name="source">待截取字符串</param>
        /// <returns>返回截取掉两边空格后的字符串</returns>
        public static string ToTrim(this string source)
        {
            return (source ?? string.Empty).Trim();
        }

        /// <summary>
        /// 是否为空字符串
        /// </summary>
        /// <param name="source">待判断字符串</param>
        /// <returns>为空返回 true 否则 false </returns>
        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(ToTrim(source));
        }

        /// <summary>
        /// 是否有值
        /// </summary>
        /// <param name="source">待判断字符串</param>
        /// <returns>不为空返回 true 否则 false </returns>
        public static bool HasValue(this string source) => !IsEmpty(source);


    }
}
