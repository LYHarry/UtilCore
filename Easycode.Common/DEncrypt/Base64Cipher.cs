using System;
using System.Collections.Generic;
using System.Text;

namespace Official.Infrastructure.Utils.DEncrypt
{
    public class Base64Cipher
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">待加密字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Encrypt(string content, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string content) => Encrypt(content, Encoding.UTF8);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">待解密字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Decrypt(string content, Encoding encoding)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">待解密字符串</param>
        /// <returns></returns>
        public static string Decrypt(string content) => Decrypt(content, Encoding.UTF8);

    }
}
