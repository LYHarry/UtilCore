using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.TimerService
{
    public interface ITaskScheduler
    {
        string TaskProcessName { get; }

        string TaskFireFilePath { get; }

        Task Start();

        Task Stop();

        Task Stop(string taskName);
    }
}
