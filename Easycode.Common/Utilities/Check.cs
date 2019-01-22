using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Utilities
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
        internal static string GetEmptyMessage(string message, string argumentName)
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
                    throw new ArgumentException(argumentName, GetEmptyMessage(message, argumentName));
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



        }
    }
}
