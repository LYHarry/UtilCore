using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace Easycode.TimerService
{
    internal static class UtilHelper
    {

        /// <summary>
        /// 加载指定目录路径下dll文件
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="dllName">dll名称</param>
        /// <returns></returns>
        internal static List<Assembly> LoadFiles(string path, string dllName)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new DirectoryNotFoundException($"{path}目录不存在.");
            dllName = string.IsNullOrWhiteSpace(dllName) ? "*.dll" : $"*{dllName.Trim()}*.dll";
            var dllfiles = Directory.GetFiles(path, dllName, SearchOption.TopDirectoryOnly);

            List<Assembly> result = new List<Assembly>();//保存结果
            if (dllfiles == null || dllfiles.Length < 1)
                return result;
            Assembly assembly = null;
            foreach (var item in dllfiles)
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
        /// 得到枚举 DescriptionAttribute 特性内容
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Description(this Type type)
        {
            if (type == null)
                return string.Empty;
            var text = type.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            return text;
        }

    }
}
