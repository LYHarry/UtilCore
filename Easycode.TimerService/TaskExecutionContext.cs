using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public class TaskExecutionContext
    {
        TaskModule TaskModule { get; set; }

        object Result { get; set; }

        int RefireCount { get; }

        TaskTriggerConfig Trigger { get; }
    }
}
