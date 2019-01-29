using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务计划安排配置
    /// </summary>
    [Serializable]
    public class TaskScheduleConfig
    {
        /// <summary>
        /// 全局任务执行时间(00:00:00)
        /// <para>TimeSpan 时分秒格式</para>
        /// </summary>
        public string FireTime { get; set; }

        /// <summary>
        /// 指定任务计划安排配置
        /// </summary>
        public SpecifyTaskMapConfig SpecifyTaskConfig { get; set; }
    }
}
