using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Easycode.Common.DEncrypt
{
    /// <summary>
    /// MD5 加密算法
    /// </summary>
    public class MD5Cipher
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <param name="encode">编码</param>
        /// <returns>返回小写加密字符串</returns>
        public static string Encrypt(string source, Encoding encode)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytResult = md5.ComputeHash(encode.GetBytes(source));
                source = BitConverter.ToString(bytResult);
                //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符
                return source.Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>返回小写加密字符串</returns>
        public static string Encrypt(string source) => Encrypt(source, Encoding.UTF8);

    }
}
