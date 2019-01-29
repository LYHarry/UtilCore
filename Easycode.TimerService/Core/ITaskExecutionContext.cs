using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务执行上下文接口类
    /// </summary>
    public interface ITaskExecutionContext
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 任务描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 任务执行结果
        /// </summary>
        object Result { get; set; }

        /// <summary>
        /// 任务执行次数
        /// </summary>
        int FireCount { get; }

        /// <summary>
        /// 任务附加参数数据
        /// </summary>
        object ExtraData { get; }

        /// <summary>
        /// 任务上次执行时间
        /// </summary>
        DateTime? LastFireTime { get; }

    }
}
