using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Easycode.Common
{
    /// <summary>
    /// Object基类帮助类
    /// </summary>
    public class RadixHelper
    {
        /// <summary>
        /// 转换对象中String类型的属性值
        /// <para>返回将null值转换为string.Empty后的对象</para>
        /// </summary>
        /// <param name="source">待转换对象</param>
        public static void ConvertEmpty(object source)
        {
            if (source == null) return;
            var props = source.GetType().GetProperties()
                              .Where(p => p.PropertyType == typeof(string)).ToList();
            if (props.Count < 1) return;
            foreach (var item in props)
            {
                if (item.GetValue(source) == null)
                    item.SetValue(source, string.Empty);
            }
        }

        /// <summary>
        /// 判断对象是否为空
        /// <para>判断规则：</para>
        /// <para>1. null 类型，返回 true</para>
        /// <para>2. String 类型，去掉两边空格后是否为 String.Empty</para>
        /// <para>3. DateTime 类型，是否为默认时间(0001/1/1 0:00:00)</para>
        /// <para>4. TimeSpan 类型，是否为默认时间(0:00:00)</para>
        /// <para>5. DBNull 类型，返回 true</para>
        /// <para>6. Guid 类型，是否为 Guid.Empty</para>
        /// <para>7. Object 对象类型，是否有属性(pulic或private属性)</para>
        /// </summary>
        /// <param name="source">待判断对象</param>
        /// <returns>为空返回 true 否则 false </returns>
        public static bool IsEmpty(object source)
        {
            if (source == null) return true;
            var type = source.GetType();
            //字符串类型
            if (type == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(source.ToString().Trim()))
                    return true;
            }
            //时间类型默认时间 0001/1/1 0:00:00
            if (type == typeof(DateTime))
            {
                if (!DateTime.TryParse(source.ToString(), out DateTime dt))
                    return true;
                if (dt.Year == 1 && dt.Month == 1 && dt.Day == 1)
                    return true;
            }
            //TimeSpan 类型默认时间 0:00:00
            if (type == typeof(TimeSpan))
            {
                if (!TimeSpan.TryParse(source.ToString(), out TimeSpan ts))
                    return true;
                if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes == 0 && ts.Seconds == 0)
                    return true;
            }
            //DBNull 类型
            if (type == typeof(DBNull)) return true;
            // Guid 类型
            if (type == typeof(Guid))
            {
                if (!Guid.TryParse(source.ToString(), out Guid guid))
                    return true;
                if (guid == Guid.Empty)
                    return true;
            }
            //对象类型
            //得到公共的和非公共的实例属性
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (properties == null || properties.Length < 1)
                return true;

            return false;
        }

        /// <summary>
        /// 判断对象是否有值
        /// </summary>
        /// <param name="source">待判断对象</param>
        /// <returns>不为空返回 true 否则 false </returns>
        public static bool HasValue(object source)
        {
            return !IsEmpty(source);
        }

    }
}
