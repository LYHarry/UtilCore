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
        public string FireTime { get; set; }

        public SpecifyTaskMapConfig SpecifyTaskConfig { get; set; }
    }
}
