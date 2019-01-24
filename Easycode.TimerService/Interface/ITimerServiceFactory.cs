using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂接口
    /// </summary>
    public interface ITimerServiceFactory
    {
        void Start();

        void Stop();

        void Stop(TaskModule task);
    }
}
