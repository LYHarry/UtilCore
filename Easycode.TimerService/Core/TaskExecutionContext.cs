using System;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public class TaskExecutionContext : ITaskExecutionContext
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 任务执行结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 任务执行次数
        /// </summary>
        public int FireCount { get; set; }

        /// <summary>
        /// 任务附加参数数据
        /// </summary>
        public object ExtraData { get; set; }

        /// <summary>
        /// 任务上次执行时间
        /// </summary>
        public DateTime? LastFireTime { get; set; }
    }
}
