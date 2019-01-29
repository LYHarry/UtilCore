using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    /// <summary>
    /// 工具助手类
    /// </summary>
    internal class UtilHelper
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
        /// 得到 DescriptionAttribute 特性内容
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string Description(Type type)
        {
            if (type == null)
                return string.Empty;
            var text = type.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            return text;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="xml">xml内容</param>
        /// <returns></returns>
        internal static T Deserialize<T>(string xml) where T : class
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                return xmldes.Deserialize(sr) as T;
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        internal static string LoadFile(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 去掉两边空格
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static string ToTrim(string content)
        {
            return (content ?? "").Trim();
        }

        /// <summary>
        /// 判断两个时间是否相等
        /// <para>不判断秒和毫秒</para>
        /// </summary>
        /// <param name="dt1">时间1</param>
        /// <param name="dt2">时间2</param>
        /// <returns></returns>
        internal static bool IsEqualTime(DateTime dt1, DateTime dt2)
        {
            if (dt1.Year != dt2.Year)
                return false;
            if (dt1.Month != dt2.Month)
                return false;
            if (dt1.Day != dt2.Day)
                return false;
            if (dt1.Hour != dt2.Hour)
                return false;
            if (dt1.Minute != dt2.Minute)
                return false;

            return true;
        }

    }
}
