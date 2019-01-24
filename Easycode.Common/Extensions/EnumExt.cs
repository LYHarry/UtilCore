using Easycode.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easycode.Extensions
{
    /// <summary>
    /// 枚举扩展帮助类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 得到枚举 DescriptionAttribute 特性内容
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns>返回 Description 没有 DescriptionAttribute 特性返回枚举名</returns>
        public static string Description(this Enum e)
        {
            FieldInfo[] fields = e.GetType().GetFields();
            if (fields == null || fields.Length < 1)
                return string.Empty;
            string name = e.Name(); //得到枚举名称           
            var info = fields.Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (info == null)
                return name;
            // 得到 Description 特性值
            var showText = info.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (string.IsNullOrWhiteSpace(showText))
                return name;
            return showText;
        }

        /// <summary>
        /// 得到枚举 DisplayNameAttribute 特性内容
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns>返回 DisplayName 没有 DisplayNameAttribute 特性返回枚举名</returns>
        public static string DisplayName(this Enum e)
        {
            FieldInfo[] fields = e.GetType().GetFields();
            if (fields == null || fields.Length < 1)
                return string.Empty;
            string name = e.Name(); //得到枚举名称
            var info = fields.Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (info == null)
                return name;
            // 得到 DisplayName 特性值
            var showText = info.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
            if (string.IsNullOrWhiteSpace(showText))
                return name;
            return showText;
        }

        /// <summary>
        /// 得到枚举名称
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns></returns>
        public static string Name(this Enum e)
        {
            string name = Enum.GetName(e.GetType(), e);
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            return name;
        }

        /// <summary>
        /// 得到枚举值
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns></returns>
        public static int Value(this Enum e)
        {
            return Convert.ToInt32(e);
        }

        /// <summary>
        /// 得到枚举所有的名称和值
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns>返回枚举名称和值列表</returns>
        public static NameValueCollection ToList(this Enum e)
        {
            NameValueCollection result = new NameValueCollection();
            Array valueList = Enum.GetValues(e.GetType());
            if (valueList == null || valueList.Length < 1)
                return result;
            foreach (var item in valueList)
            {
                result.Add(item.ToString(), Convert.ToInt32(item).ToString());
            }
            return result;
        }

        /// <summary>
        /// 得到枚举描述摘要
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <returns></returns>
        public static List<EnumDescriptions> Items(this Enum e)
        {
            List<EnumDescriptions> result = new List<EnumDescriptions>();
            //得到枚举对象的所有字段
            FieldInfo[] fields = e.GetType().GetFields();
            if (fields == null || fields.Length < 1)
                return result;
            //得到枚举 DefaultValueAttribute 默认值特性
            var defaultAttr = e.GetType().GetCustomAttribute<DefaultValueAttribute>();
            //得到当前枚举值
            int currVal = e.Value();
            DescriptionAttribute descAttr = null;
            DisplayNameAttribute nameAttr = null;
            int enumValue = 0;
            foreach (var item in fields)
            {
                if (!int.TryParse(item.GetValue(e)?.ToString(), out enumValue))
                    continue;
                descAttr = item.GetCustomAttribute<DescriptionAttribute>(true);
                nameAttr = item.GetCustomAttribute<DisplayNameAttribute>(true);
                result.Add(new EnumDescriptions()
                {
                    Name = item.Name,
                    Value = enumValue,
                    Description = descAttr == null ? string.Empty : descAttr.Description,
                    ShowName = nameAttr == null ? string.Empty : nameAttr.DisplayName,
                    IsDefault = defaultAttr == null ? false : ((Convert.ToInt32(defaultAttr.Value) == enumValue) ? true : false),
                    IsSelected = (enumValue == currVal) ? true : false
                });
            }
            return result;
        }

        /// <summary>
        /// 得到 Select 选项
        /// </summary>
        /// <param name="e">枚举对象</param>
        /// <param name="ignoreEnum">忽略枚举选项</param>
        /// <returns></returns>
        public static List<EnumDescriptions> Options(this Enum e, params Enum[] ignoreEnum)
        {
            List<EnumDescriptions> result = new List<EnumDescriptions>();
            var enumList = e.Items();
            if (enumList == null || enumList.Count < 1)
                return result;
            foreach (var item in enumList)
            {
                if (ignoreEnum.Where(p => p.Name() == item.Name).FirstOrDefault() != null)
                    continue;
                if (item.ShowName.HasValue())
                    item.Name = item.ShowName;
                else if (item.Description.HasValue())
                    item.Name = item.Description;
                result.Add(item);
            }
            return result;
        }


    }
}
