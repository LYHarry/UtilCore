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
        /// 得到枚举 DescriptionAttribute 特性内容
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
        /// 记录程序开始的日志
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="stime">返回当前开始时间</param>
        internal static void RecordStartLog(string message, out DateTime stime)
        {
            stime = DateTime.Now;
            StringBuilder startLog = new StringBuilder();
            startLog.AppendLine();
            startLog.AppendLine();
            startLog.Append("【开始】");
            startLog.Append(message + "。");
            startLog.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】,");
            startLog.Append($"当前时间:【{stime}】");
            Console.WriteLine(startLog);
        }


        /// <summary>
        /// 记录程序结束的日志
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="startTime">程序开始的时间</param>
        internal static void RecordEndLog(string message, DateTime startTime)
        {
            StringBuilder endLog = new StringBuilder();
            endLog.Append("【结束】");
            endLog.Append(message + "。");
            endLog.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】,");
            endLog.Append($"当前时间:【{DateTime.Now}】,");
            endLog.Append($"运行时间:【{(DateTime.Now - startTime).TotalMilliseconds}毫秒】");
            Console.WriteLine(endLog);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">提示信息</param>
        internal static void RecordLog(string message)
        {
            StringBuilder log = new StringBuilder();          
            log.Append(message + "。");
            log.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine(log);
        }


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
