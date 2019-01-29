using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 任务调度器接口
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// 工作任务所在的项目名称
        /// </summary>
        string TaskProcessName { get; }

        /// <summary>
        /// 任务触发配置文件名称
        /// </summary>
        string TaskFireFileName { get; }

        /// <summary>
        /// 开始执行任务
        /// </summary>
        /// <returns></returns>
        Task Start();

        /// <summary>
        /// 停止所有任务
        /// </summary>
        /// <returns></returns>
        Task Stop();

        /// <summary>
        /// 停止某个任务
        /// </summary>
        /// <param name="taskName">待停止的任务名称</param>
        /// <returns></returns>
        Task Stop(string taskName);
    }
}
