using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.TimerService
{
    public interface ITaskScheduler
    {
        string TaskProcessName { get; set; }

        Task Start();

        Task Stop();
    }
}
