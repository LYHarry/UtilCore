using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Easycode.Common.Extensions
{
    /// <summary>
    /// 字符串扩展帮助类
    /// <para>html/url编码</para>
    /// </summary>
    public static partial class StrExtension
    {
        /// <summary>
        /// 返回HTML字符串的编码结果
        /// </summary>
        /// <param name="content">需要编码字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(this string content)
        {
            return HttpUtility.HtmlEncode(content);
        }


        /// <summary>
        /// 返回HTML字符串的解码结果
        /// </summary>
        /// <param name="content">需要解码字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(this string content)
        {
            return HttpUtility.HtmlDecode(content);
        }


        /// <summary>
        /// 返回URL字符串的编码结果
        /// </summary>
        /// <param name="content">需要编码字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(this string content)
        {
            return HttpUtility.UrlEncode(content);
        }

        /// <summary>
        /// 返回URL字符串的编码结果
        /// </summary>
        /// <param name="content">需要解码字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(this string content)
        {
            return HttpUtility.UrlDecode(content);
        }


    }
}
