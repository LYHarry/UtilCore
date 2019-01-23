using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Easycode.Common.DEncrypt
{
    /// <summary>
    /// 3DES 加密算法
    /// </summary>
    public class TripleDESCipher
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
            using (var tripleDES = TripleDES.Create())
            {
                byte[] inputArray = Encoding.UTF8.GetBytes(source);
                var byteKey = Encoding.UTF8.GetBytes(keys);
                byte[] allKey = new byte[24];
                Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
                Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
                tripleDES.Key = allKey;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform crypto = tripleDES.CreateEncryptor())
                {
                    byte[] resultArray = crypto.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
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
            using (TripleDES tripleDES = TripleDES.Create())
            {
                byte[] inputArray = Convert.FromBase64String(source);
                var byteKey = Encoding.UTF8.GetBytes(keys);
                byte[] allKey = new byte[24];
                Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
                Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
                tripleDES.Key = allKey;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform crypto = tripleDES.CreateDecryptor())
                {
                    byte[] resultArray = crypto.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    return Encoding.UTF8.GetString(resultArray);
                }
            }
        }

    }
}
