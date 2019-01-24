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
        public static string Alphabet
        {
            get
            {
                return LowerCase + UpperCase;
            }
        }

        /// <summary>
        /// 大小写字母数字集
        /// </summary>
        public static string Alphanumeric
        {
            get
            {
                return Number + LowerCase + UpperCase;
            }
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="source">基数字符串(通过该字符串产生随机字符串)</param>
        /// <param name="len">字符串长度</param>
        /// <param name="filter">需要过滤的字符串</param>
        /// <returns>返回随机字符串</returns>
        public static string Generate(string source, int len, params string[] filter)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            if (len < 1)
                return string.Empty;
            if (filter != null && filter.Length > 0)
            {
                foreach (var item in filter)
                    source = source.Replace(item, "");
            }
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < len; i++)
                res.Append(source[_rand.Next(source.Length)]);
            return res.ToString();
        }


        /// <summary>
        /// 得到随机字符串
        /// </summary>
        /// <param name="source">基数字符串（通过该字符串产生随机字符串）</param>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string Generate(string source, int len)
        {
            return Generate(source, len, null);
        }

        /// <summary>
        /// 数字随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandNumber(int len)
        {
            return Generate(Number, len);
        }

        /// <summary>
        /// 小写字母随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandLowerCase(int len)
        {
            return Generate(LowerCase, len);
        }

        /// <summary>
        /// 大写字母随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandUpperCase(int len)
        {
            return Generate(UpperCase, len);
        }

        /// <summary>
        /// 大小写字母随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandAlphabet(int len)
        {
            return Generate(Alphabet, len);
        }

        /// <summary>
        /// 大小写字母加数字随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandAlphanumeric(int len)
        {
            return Generate(Alphanumeric, len);
        }

        /// <summary>
        /// 在一个范围中随机得到一个整型数字
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>返回随机整型数字</returns>
        public static int Next(int minValue, int maxValue)
        {
            return _rand.Next(minValue, maxValue);
        }

        /// <summary>
        /// 随机得到一个整型数字
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <returns>返回随机整型数字</returns>
        public static int Next(int maxValue)
        {
            return _rand.Next(maxValue);
        }

    }
}
