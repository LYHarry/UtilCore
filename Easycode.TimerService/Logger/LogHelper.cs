using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 日记帮助类
    /// </summary>
    public class LogHelper
    {
        private readonly static ILog _log = new LogProvider();

        public static void Info(string message)
        {
            _log.Log(LogLevel.Info, message);
        }

        public static void InfoFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Info, message, null, args);
        }

        public static void InfoException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Info, message, exception, formatParams);
        }

        public static void Debug(string message)
        {
            _log.Log(LogLevel.Debug, message);
        }

        public static void DebugFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Debug, message, null, args);
        }

        public static void DebugException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Debug, message, exception, formatParams);
        }

        public static void Warn(string message)
        {
            _log.Log(LogLevel.Warn, message);
        }

        public static void WarnFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Warn, message, null, args);
        }

        public static void WarnException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Trace, message, exception, formatParams);
        }

        public static void Error(string message)
        {
            _log.Log(LogLevel.Error, message);
        }

        public static void ErrorFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Error, message, null, args);
        }

        public static void ErrorException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Error, message, exception, formatParams);
        }

        public static void Fatal(string message)
        {
            _log.Log(LogLevel.Fatal, message);
        }

        public static void FatalFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Fatal, message, null, args);
        }

        public static void FatalException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Fatal, message, exception, formatParams);
        }

        public static void Trace(string message)
        {
            _log.Log(LogLevel.Trace, message);
        }

        public static void TraceFormat(string message, params object[] args)
        {
            _log.Log(LogLevel.Trace, message, null, args);
        }

        public static void TraceException(string message, Exception exception, params object[] formatParams)
        {
            _log.Log(LogLevel.Trace, message, exception, formatParams);
        }

    }
}
