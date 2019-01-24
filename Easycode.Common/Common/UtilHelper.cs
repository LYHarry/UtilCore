using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Easycode.Common
{
    /// <summary>
    /// 其他帮助类
    /// </summary>
    public class UtilHelper
    {
        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="content">待截取字符串</param>
        /// <param name="len">截取长度</param>
        /// <param name="dots">省略符(...)</param>
        /// <returns>如果字符串长度小于截取长度则返回原字符串，否则返回截取字符串加省略符的字符串</returns>
        public static string CutOut(string content, int len, string dots)
        {
            Check.Argument.IsEmpty(content, nameof(content));
            Check.Argument.IsEmpty(dots, nameof(dots));
            Check.Argument.OutOfRange(len, nameof(len), 1, null);
            if (content.Length <= len)
                return content;
            return content.Substring(0, len) + dots;
        }

        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="content">待截取字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns>如果字符串长度小于截取长度则返回原字符串，否则返回截取字符串加省略符(...)的字符串</returns>
        public static string CutOut(string content, int len) => CutOut(content, len, "...");

        /// <summary>
        /// 为字符串添加掩码
        /// <para>如：182****4512</para>
        /// </summary>
        /// <param name="content">待添加掩码字符串</param>
        /// <param name="index">开始下标(从0开始)</param>
        /// <param name="maskLen">掩码长度</param>
        /// <param name="dots">掩码(*)</param>
        /// <returns>返回添加了掩码的字符串</returns>
        public static string AddMask(string content, int index, int maskLen, string dots)
        {
            Check.Argument.IsEmpty(content, nameof(content));
            Check.Argument.IsEmpty(dots, nameof(dots));
            Check.Argument.OutOfRange(maskLen, nameof(maskLen), 1, content.Length);
            if (index < 1) { index = 0; }
            if (index >= content.Length) { return content; }
            string res = string.Empty;
            for (int i = 0, j = 1; i < content.Length; i++)
            {
                if (i >= index)
                {
                    if (j > maskLen)
                    {
                        res += content[i].ToString();
                        continue;
                    }
                    res += dots;
                    j++;
                    continue;
                }
                res += content[i].ToString();
            }
            return res;
        }

        /// <summary>
        /// 为字符串添加掩码
        /// </summary>
        /// <param name="content">待添加掩码字符串</param>
        /// <param name="index">开始下标(从0开始)</param>
        /// <param name="maskLen">掩码长度</param>
        /// <returns>返回添加了(*)掩码的字符串</returns>
        public static string AddMask(string content, int index, int maskLen) => AddMask(content, index, maskLen, "*");

        /// <summary>
        /// 为字符串添加掩码
        /// </summary>
        /// <param name="content">待添加掩码字符串</param>
        /// <param name="index">开始下标(从0开始)</param>
        /// <returns>返回添加了4位(*)掩码的字符串</returns>
        public static string AddMask(string content, int index) => AddMask(content, index, 4, "*");

    }
}
