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
                    assembly = null;
                    if (File.Exists(item))
                        assembly = Assembly.LoadFile(item);
                }
                catch { assembly = null; }
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
            Check.Argument.HasExistDir(path);
            var dllfiles = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
            return LoadFiles(dllfiles);
        }

    }
}
