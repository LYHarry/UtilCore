using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Easycode.Common.DEncrypt
{
    /// <summary>
    /// DES 加密算法
    /// </summary>
    public class DESCipher
    {
        /// <summary> 
        /// 加密
        /// </summary> 
        /// <param name="source">待加密字符串</param> 
        /// <param name="keys">加密密钥</param> 
        /// <returns>返回加密后字符串</returns> 
        public static string Encrypt(string source, string keys)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            Check.Argument.IsEmpty(keys, "加密密钥");
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //MD5加密密钥
                keys = MD5Cipher.Encrypt(keys);
                des.Key = Encoding.ASCII.GetBytes(keys);
                des.IV = des.Key;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(source);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }


        /// <summary> 
        /// 解密
        /// </summary> 
        /// <param name="source">待解密字符串</param> 
        /// <param name="keys">解密密钥</param> 
        /// <returns>返回解密后字符串</returns> 
        public static string Decrypt(string source, string keys)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            Check.Argument.IsEmpty(keys, "解密密钥");
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //MD5加密密钥
                keys = MD5Cipher.Encrypt(keys);
                des.Key = Encoding.ASCII.GetBytes(keys);
                des.IV = des.Key;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputByteArray = Convert.FromBase64String(source);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            };
        }


    }
}
