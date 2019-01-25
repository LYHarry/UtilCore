using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    internal class LogProvider : ILog
    {
        public bool Log(LogLevel logLevel, string message, Exception exception = null, params object[] formatParameters)
        {
            string info = string.Empty;
            if (formatParameters != null && formatParameters.Length > 0)
                info = string.Format(message, formatParameters);
            if (exception != null)
                info += $"错误信息：{exception.Message}";
            Console.WriteLine($"{logLevel.ToString()}|{info}");
            return true;
        }
    }
}
