using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 循环任务调度配置
    /// </summary>
    public class CycleTaskConfig
    {
        /// <summary>
        /// 重复执行次数
        /// <para> -1表示无限制重复执行 </para>
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 重复间隔时间
        /// <para>单位：秒</para>
        /// </summary>
        public int Interval { get; set; }
    }
}
