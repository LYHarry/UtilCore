using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务执行条件配置
    /// </summary>
    public class TaskTriggerConfig
    {
        public CycleTaskConfig CycleTask { get; set; }

        public CrontabConfig Crontab { get; set; }
    }
}
