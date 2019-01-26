using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 工作任务详细信息
    /// </summary>
    public class JobTaskDetail
    {
        /// <summary>
        /// 英文名称(类名)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 中文名称
        /// <para>取类 DescriptionAttribute 特性内容</para>
        /// </summary>
        public string CName { get; set; }      

        /// <summary>
        /// 完整的类名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

    }
}
