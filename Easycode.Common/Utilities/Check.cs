﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easycode.Common
{
    /// <summary>
    /// 检查类
    /// </summary>
    internal class Check
    {
        /// <summary>
        /// 得到不能为空的错误提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="argumentName">参数名称</param>
        /// <returns></returns>
        private static string GetEmptyMessage(string message, string argumentName)
        {
            return $"{message} {argumentName} 不能为空.".Trim();
        }

        /// <summary>
        /// 参数检查类
        /// </summary>
        internal class Argument
        {
            /// <summary>
            /// 判断参数是否为空或空字符串
            /// </summary>
            /// <param name="argument">待判断参数</param>
            /// <param name="argumentName">参数名称</param>
            /// <param name="message">提示信息</param>
            internal static void IsEmpty(string argument, string argumentName, string message = "")
            {
                if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                    throw new ArgumentNullException(argumentName, GetEmptyMessage(message, argumentName));
            }

            /// <summary>
            /// 判断参数是否为空
            /// </summary>
            /// <param name="argument">待判断参数</param>
            /// <param name="argumentName">参数名称</param>
            /// <param name="message">提示消息</param>
            internal static void IsNull(object argument, string argumentName, string message = "")
            {
                if (argument == null)
                    throw new ArgumentNullException(argumentName, GetEmptyMessage(message, argumentName));
            }

            /// <summary>
            /// 列表集合参数是否为空
            /// </summary>
            /// <typeparam name="T">参数类型</typeparam>
            /// <param name="argument">参数</param>
            /// <param name="argumentName">参数名称</param>
            /// <param name="message">提示信息</param>
            internal static void IsEmpty<T>(ICollection<T> argument, string argumentName, string message = "")
            {
                if (argument == null || argument.Count < 1)
                    throw new ArgumentNullException(argumentName, GetEmptyMessage(message, argumentName));
            }

            /// <summary>
            /// 文件是否存在
            /// </summary>
            /// <param name="filePath">文件路径(物理路径)</param>
            internal static void HasExistFile(string filePath)
            {
                IsEmpty(filePath, "path", "路径");
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"{filePath} 文件不存在.");
            }

            /// <summary>
            /// 已存在文件
            /// </summary>
            /// <param name="filePath">文件路径(物理路径)</param>
            internal static void ExistFile(string filePath)
            {
                IsEmpty(filePath, "path", "路径");
                if (File.Exists(filePath))
                    throw new Exception($"{filePath} 文件已存在.");
            }

            /// <summary>
            /// 目录是否存在
            /// </summary>
            /// <param name="path">目录路径(物理路径)</param>
            internal static void HasExistDir(string path)
            {
                IsEmpty(path, "path", "路径");
                if (!Directory.Exists(path))
                    throw new FileNotFoundException($"{path} 目录不存在.");
            }

            /// <summary>
            /// 参数超出范围
            /// </summary>
            /// <param name="argument">参数</param>
            /// <param name="argumentName">参数名称</param>
            /// <param name="min">最小值</param>
            /// <param name="max">最大值</param>
            internal static void OutOfRange(int argument, string argumentName, int? min, int? max)
            {
                StringBuilder message = new StringBuilder();
                message.Append($"{argumentName}超出范围");
                if (min.HasValue && !(argument > min.Value))
                    message.Append($"，必须大于或等于{min.Value}");
                if (max.HasValue && !(argument < max.Value))
                    message.Append($"，必须小于或等于{max.Value}");
                throw new ArgumentOutOfRangeException(message.ToString());
            }


        }
    }
}
