using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 具体任务触发条件配置
    /// </summary>
    [Serializable]
    public class TaskTriggerConfig
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CrontabConfig Crontab { get; set; }

        public CycleTaskConfig CycleTask { get; set; }
    }
}
