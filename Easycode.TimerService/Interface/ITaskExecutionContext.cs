using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public interface ITaskExecutionContext
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        string Description { get; }

        object Result { get; set; }

        int FireCount { get; }
        
        object ExtraData { get; }

        DateTime? LastFireTime { get; }

    }
}
