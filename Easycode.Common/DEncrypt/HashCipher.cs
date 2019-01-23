using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Easycode.Common.DEncrypt
{
    /// <summary>
    /// Hash 哈希加密算法
    /// </summary>
    public class HashCipher
    {
        /// <summary>
        /// SHA128 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string SHA128(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            using (SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(source);
                byte[] byteArr = SHA1.ComputeHash(buffer);
                source = BitConverter.ToString(byteArr);
                return source.Replace("-", "").ToLower();
            }
        }


        /// <summary>
        /// SHA256 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string SHA256(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(source);
                byte[] byteArr = SHA256.ComputeHash(buffer);
                source = BitConverter.ToString(byteArr);
                return source.Replace("-", "").ToLower();
            }
        }


        /// <summary>
        /// SHA384 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string SHA384(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            using (SHA384CryptoServiceProvider SHA384 = new SHA384CryptoServiceProvider())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(source);
                byte[] byteArr = SHA384.ComputeHash(buffer);
                source = BitConverter.ToString(byteArr);
                return source.Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// SHA512 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string SHA512(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            using (SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(source);
                byte[] byteArr = SHA512.ComputeHash(buffer);
                source = BitConverter.ToString(byteArr);
                return source.Replace("-", "").ToLower();
            }
        }
    }
}
