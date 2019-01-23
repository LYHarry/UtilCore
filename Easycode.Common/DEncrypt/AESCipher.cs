using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Easycode.Common.DEncrypt
{
    /// <summary>
    /// AES 加密算法
    /// </summary>
    public class AESCipher
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
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Encoding.ASCII.GetBytes(keys);
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform crypto = aes.CreateEncryptor())
                {
                    byte[] inputBuffer = Encoding.UTF8.GetBytes(source);
                    byte[] result = crypto.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
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
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Encoding.ASCII.GetBytes(keys);
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform crypto = aes.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] result = crypto.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    return Encoding.UTF8.GetString(result);
                }
            }
        }
    }
}
