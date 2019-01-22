using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Easycode.Common
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectHelper
    {
        /// <summary>
        /// 得到特性对象信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(ICustomAttributeProvider provider) where T : Attribute
        {
            var attr = provider.GetCustomAttributes(true).FirstOrDefault();
            return (T)attr;
        }
    }
}
