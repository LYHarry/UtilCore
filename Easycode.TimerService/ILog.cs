using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    internal interface ILog
    {
        bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters);
    }
}
