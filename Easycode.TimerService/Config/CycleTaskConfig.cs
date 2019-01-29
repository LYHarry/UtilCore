using System;

namespace Easycode.TimerService
{
    /// <summary>
    /// 循环任务触发条件配置
    /// </summary>
    [Serializable]
    public class CycleTaskConfig
    {
        /// <summary>
        /// 重复执行次数
        /// <para>-1 表示无限制重复执行</para>
        /// </summary>
        public string RepeatCount { get; set; }

        /// <summary>
        /// 执行间隔时间
        /// <para>单位：秒</para>
        /// </summary>
        public string Interval { get; set; }
    }
}
