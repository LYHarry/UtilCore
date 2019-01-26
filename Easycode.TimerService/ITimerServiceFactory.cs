using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂接口
    /// </summary>
    public interface ITimerServiceFactory
    {
        Task<ITaskScheduler> GetScheduler(string dllName);
    }
}
