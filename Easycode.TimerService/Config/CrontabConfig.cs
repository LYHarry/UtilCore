using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时任务调试配置
    /// </summary>
    public class CrontabConfig
    {
        public string FireTime { get; set; }

        /// <summary>
        /// 执行间隔的天数
        /// </summary>
        public string Interval { set; get; }
    }
}
