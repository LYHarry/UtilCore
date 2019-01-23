using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;


namespace Easycode.Common
{
    /// <summary>
    /// 文件(目录)帮助类
    /// <para>路径必须为物理绝对路径</para>
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 文件操作
        /// </summary>
        public class Fi
        {
            /// <summary>
            /// 读取文件内容到流中
            /// </summary>
            /// <param name="filePath">文件路径</param>
            /// <returns>返回 Stream</returns>
            public static Stream ToStream(string filePath)
            {
                Check.Argument.HasExistFile(filePath);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    return (new MemoryStream(bytes));
                }
            }

            /// <summary>
            /// 把Stream数据写入文件
            /// </summary>
            /// <param name="stream">Stream</param>
            /// <param name="filePath">文件路径</param>
            /// <returns></returns>
            public static bool SaveByStream(Stream stream, string filePath)
            {
                Check.Argument.ExistFile(filePath);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(bytes);
                }
                return true;
            }

            /// <summary>
            /// 把字节数组数据写入文件
            /// </summary>
            /// <param name="buffer">二进制流数据</param>
            /// <param name="filePath">文件路径</param>
            /// <returns></returns>
            public static bool SaveByBytes(byte[] buffer, string filePath)
            {
                Check.Argument.ExistFile(filePath);
                FileInfo file = new FileInfo(filePath);
                using (FileStream fs = file.Create())
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
                return true;
            }

            /// <summary>
            /// 备份(复制)文件
            /// </summary>
            /// <param name="sourceFilePath">源文件路径</param>
            /// <param name="toDirPath">目标目录路径</param>
            /// <param name="overWrite">是否覆盖</param>
            /// <returns>是否复制成功(如果目标目录存在同名文件并且不充许覆盖返回 false)</returns>
            public static bool CopyFile(string sourceFilePath, string toDirPath, bool overWrite = false)
            {
                Check.Argument.HasExistFile(sourceFilePath);
                Check.Argument.HasExistDir(toDirPath);
                string filePath = Path.GetFileName(sourceFilePath);
                filePath = Path.Combine(toDirPath, filePath);
                //目标目录存在该文件，并不充许覆盖
                if (File.Exists(filePath) && !overWrite)
                    return false;
                File.Copy(sourceFilePath, filePath, overWrite);
                return true;
            }

            /// <summary>
            /// 移动文件到指定目录
            /// </summary>
            /// <param name="sourceFilePath">源文件路径</param>
            /// <param name="toDirPath">目标目录路径</param>
            /// <returns></returns>
            public static bool MoveFile(string sourceFilePath, string toDirPath)
            {
                Check.Argument.HasExistFile(sourceFilePath);
                Check.Argument.HasExistDir(toDirPath);
                //获取源文件的名称
                string sourceFileName = Path.GetFileName(sourceFilePath);
                //得到文件在新文件夹的物理路径
                string newFilePath = Path.Combine(toDirPath, sourceFileName);
                Check.Argument.HasExistFile(newFilePath);
                Dir.CreateDir(toDirPath);
                //将文件移动到指定目录
                File.Move(sourceFilePath, newFilePath);
                return true;
            }

            /// <summary>
            /// 计算文件大小(保留两位小数)
            /// </summary>
            /// <param name="size">文件字节大小</param>
            /// <returns></returns>
            public static string CountFileSize(long size)
            {
                string m_strSize = string.Empty;
                if (size < 1024.00)
                    m_strSize = size.ToString("F2") + " 字节";
                else if (size >= 1024.00 && size < 1048576)
                    m_strSize = (size / 1024.00).ToString("F2") + " KB";
                else if (size >= 1048576 && size < 1073741824)
                    m_strSize = (size / 1024.00 / 1024.00).ToString("F2") + " MB";
                else if (size >= 1073741824)
                    m_strSize = (size / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";

                return m_strSize;
            }

            /// <summary>
            /// 得到文件大小(单位为GB/MB/KB)
            /// </summary>
            /// <param name="filePath">文件路径</param>
            /// <returns>返回文件大小</returns>
            public static string GetFileSize(string filePath)
            {
                Check.Argument.HasExistFile(filePath);
                FileInfo fileinfo = new FileInfo(filePath);
                return CountFileSize(fileinfo.Length);
            }

            /// <summary>
            /// 是否为指定类型文件
            /// </summary>
            /// <param name="ext">文件扩展名</param>
            /// <param name="type">文件类型</param>
            /// <returns>是返回 true 否则 false </returns>
            public static bool IsSpecifyType(string ext, params string[] type)
            {
                Check.Argument.IsEmpty(ext, nameof(ext));
                Check.Argument.IsEmpty(type, nameof(type));
                if (type.Contains(ext))
                    return true;
                return false;
            }
            
            /// <summary>
            /// 得到文件扩展名
            /// </summary>
            /// <param name="fileName">文件名称</param>
            /// <param name="isBeDot">是否包含圆点(.)</param>
            /// <returns>返回小写扩展名</returns>
            public static string GetExtName(string fileName, bool isBeDot)
            {
                Check.Argument.IsEmpty(fileName, nameof(fileName));
                fileName = Path.GetExtension(fileName).ToLower();
                if (!isBeDot)
                    fileName = fileName.Substring(1);
                return fileName;
            }

            /// <summary>
            /// 得到文件扩展名
            /// </summary>
            /// <param name="fileName">文件名称</param>
            /// <returns>返回小写并带圆点(.)扩展名</returns>
            public static string GetExtName(string fileName)
            {
                return GetExtName(fileName, true);
            }
        }

        /// <summary>
        /// 目录(文件夹)操作
        /// </summary>
        public class Dir
        {
            /// <summary>
            /// 创建目录
            /// </summary>
            /// <param name="path">目录路径</param>
            public static void CreateDir(string path)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

            /// <summary>
            /// 是否为空目录
            /// </summary>
            /// <param name="path">目录路径</param>
            /// <returns>为空返回 true 否则 false </returns>
            public static bool IsEmptyDir(string path)
            {
                Check.Argument.HasExistDir(path);
                //判断是否存在文件
                string[] fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                if (fileNames != null && fileNames.Length > 0)
                    return false;
                //判断是否存在文件夹
                string[] directoryNames = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);
                if (directoryNames != null && directoryNames.Length > 0)
                    return false;
                return true;
            }

        }
    }
}
