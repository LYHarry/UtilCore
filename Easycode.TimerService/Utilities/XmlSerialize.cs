using System.IO;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    /// <summary>
    /// XML序列化帮助类
    /// </summary>
    internal class XmlSerialize
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="xml">xml内容</param>
        /// <returns></returns>
        internal static object Deserialize<T>(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                return xmldes.Deserialize(sr);
            }
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="stream">xml流</param>
        /// <returns></returns>
        internal static object Deserialize<T>(Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return xmldes.Deserialize(stream);
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        internal static string Serializer<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                //序列化对象
                xml.Serialize(stream, obj);
                stream.Position = 0;
                using (StreamReader sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

    }
}
