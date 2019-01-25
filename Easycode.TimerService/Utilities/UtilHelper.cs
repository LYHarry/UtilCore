using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Easycode.TimerService
{
    internal class UtilHelper
    {
        /// <summary>
        /// 加载汇编文件内容
        /// </summary>
        /// <param name="filePaths">dll文件路径(物理绝对路径)</param>
        /// <returns></returns>
        internal static List<Assembly> LoadFiles(string[] filePaths)
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
    }
}
