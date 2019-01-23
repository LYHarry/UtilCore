using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Common
{
    /// <summary>
    /// 枚举描述摘要类
    /// </summary>
    [Serializable]
    public class EnumDescriptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 显示名称(DisplayNameAttribute特性值)
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 描述(DescriptionAttribute特性值)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否默认值(DefaultValueAttribute特性值)
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否当前选中项
        /// </summary>
        public bool IsSelected { get; set; }

    }
}
