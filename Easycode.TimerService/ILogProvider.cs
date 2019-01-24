using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    delegate bool Logger(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters);

    interface ILogProvider
    {
        Logger GetLogger(string name);

        IDisposable OpenNestedContext(string message);

        IDisposable OpenMappedContext(string key, string value);
    }
}
