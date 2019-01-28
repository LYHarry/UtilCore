using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂实现类
    /// </summary>
    public class TimerServiceFactory : ITimerServiceFactory
    {
        public Task<ITaskScheduler> GetScheduler(string dllName, string fireFilePath)
        {
            ITaskScheduler ts = new TaskScheduler
            {
                TaskProcessName = UtilHelper.ToTrim(dllName),
                TaskFireFilePath = UtilHelper.ToTrim(fireFilePath)
            };
            return Task.FromResult(ts);
        }

        public Task<ITaskScheduler> GetScheduler(string dllName)
        {
            return GetScheduler(dllName, "TaskFire.config");
        }
    }
}
