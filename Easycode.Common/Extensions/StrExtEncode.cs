using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Easycode.Extensions
{
    /// <summary>
    /// 字符串扩展帮助类
    /// <para>html/url编码</para>
    /// </summary>
    public static partial class StrExtension
    {
        /// <summary>
        /// 编码HTML字符串
        /// </summary>
        /// <param name="content">待编码字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string HtmlEncode(this string content)
        {
            return HttpUtility.HtmlEncode(content);
        }

        /// <summary>
        /// 解码HTML字符串
        /// </summary>
        /// <param name="content">待解码字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string HtmlDecode(this string content)
        {
            return HttpUtility.HtmlDecode(content);
        }


        /// <summary>
        /// 编码URL字符串
        /// </summary>
        /// <param name="content">待编码字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string UrlEncode(this string content)
        {
            return HttpUtility.UrlEncode(content);
        }

        /// <summary>
        /// 编码URL字符串
        /// </summary>
        /// <param name="content">待解码字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string UrlDecode(this string content)
        {
            return HttpUtility.UrlDecode(content);
        }


    }
}
