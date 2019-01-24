using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 工作任务接口
    /// </summary>
    public interface IJobTask : ITask
    {
        /// <summary>
        /// 执行异常处理方法
        /// </summary>
        /// <param name="ex">异常信息</param>
        void OnException(Exception ex);
    }
}
