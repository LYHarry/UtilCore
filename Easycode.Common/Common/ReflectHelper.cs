using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.IO;

namespace Easycode.Common
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectHelper
    {
        ///// <summary>
        ///// 得到特性对象信息
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="provider"></param>
        ///// <returns></returns>
        //public static T GetAttribute<T>(ICustomAttributeProvider provider) where T : Attribute
        //{
        //    if (provider == null)
        //        return default(T);
        //    var attr = provider.GetCustomAttributes(true).FirstOrDefault();
        //    return (T)attr;
        //}


        /// <summary>
        /// 加载汇编文件内容
        /// </summary>
        /// <param name="filePaths">dll文件路径(物理绝对路径)</param>
        /// <returns></returns>
        public static List<Assembly> LoadFiles(string[] filePaths)
        {
            List<Assembly> result = new List<Assembly>();//保存结果
            if (filePaths == null || filePaths.Length < 1)
                return result;
            Assembly assembly = null;
            foreach (var item in filePaths)
            {
                try
                {
                    assembly = Assembly.LoadFile(item);
                }
                catch (Exception)
                {
                    assembly = null;
                }
                if (assembly != null)
                    result.Add(assembly);
            }
            return result;
        }

        /// <summary>
        /// 加载指定路径下的所有汇编文件内容
        /// </summary>
        /// <param name="path">路径(物理绝对路径)</param>
        /// <returns></returns>
        public static List<Assembly> LoadFiles(string path)
        {
            var dllfiles = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
            return LoadFiles(dllfiles);
        }

    }
}
