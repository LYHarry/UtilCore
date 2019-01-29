using System;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时周期任务触发条件配置
    /// </summary>
    [Serializable]
    public class CrontabConfig
    {
        /// <summary>
        /// 任务执行时间(00:00:00)
        /// <para>TimeSpan 时分秒格式</para>
        /// </summary>
        public string FireTime { get; set; }

        /// <summary>
        /// 执行间隔的天数
        /// </summary>
        public string Interval { set; get; }
    }
}
