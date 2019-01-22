using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Easycode.Common
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 随机类放在外面，避免循环产生重复数据
        /// </summary>
        private readonly static Random _rand = new Random(Environment.TickCount);

        /// <summary>
        /// 0~9 数字
        /// </summary>
        public const string Number = "0123456789";

        /// <summary>
        /// a~z 小写字母
        /// </summary>
        public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// A~Z 大写字母
        /// </summary>
        public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 大小写字母表
        /// </summary>
        public static string Alphabet { get { return LowerCase + UpperCase; } }

        /// <summary>
        /// 大小写字母数字集
        /// </summary>
        public static string Alphanumeric { get { return Number + LowerCase + UpperCase; } }


        /// <summary>
        /// 随机得到字符串
        /// </summary>
        /// <param name="source">基数字符串（通过该字符串产生随机字符串）</param>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandString(string source, int len)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            if (len < 1)
                return string.Empty;
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < len; i++)
                res.Append(source[_rand.Next(source.Length)]);
            return res.ToString();
        }

        /// <summary>
        /// 随机得到数字字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机数字字符串</returns>
        public static string RandNumber(int len)
        {
            if (len < 1)
                return string.Empty;
            return RandString(Number, len);
        }


        /// <summary>
        /// 随机得到小写字母字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns></returns>
        public static string RandLowerCase(int len)
        {
            if (len < 1)
                return string.Empty;
            return RandString(LowerCase, len);
        }


        /// <summary>
        /// 随机得到大写字母字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns></returns>
        public static string RandUpperCase(int len)
        {
            if (len < 1)
                return string.Empty;
            return RandString(UpperCase, len);
        }


        /// <summary>
        /// 随机得到大小写字母字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns></returns>
        public static string RandAlphabet(int len)
        {
            if (len < 1)
                return string.Empty;
            return RandString(Alphabet, len);
        }

    }
}
