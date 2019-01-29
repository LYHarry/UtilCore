using System;

namespace Easycode.TimerService
{
    /// <summary>
    /// 工作任务接口
    /// <para>该接口包含异常处理方法</para>
    /// </summary>
    public interface IJobTask : ITask
    {
        /// <summary>
        /// 异常处理方法
        /// </summary>
        /// <param name="ex">异常信息</param>
        void OnException(Exception ex);
    }
}
