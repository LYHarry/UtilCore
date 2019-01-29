using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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


        /// <summary>
        /// 记录程序开始的日志
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="stime">返回当前开始时间</param>
        internal static void RecordStartLog(string message, out DateTime stime)
        {
            stime = DateTime.Now;
            StringBuilder startLog = new StringBuilder();
            startLog.AppendLine();
            startLog.AppendLine();
            startLog.Append("【开始】");
            startLog.Append(message + "。");
            startLog.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】,");
            startLog.Append($"当前时间:【{stime}】");
            Console.WriteLine(startLog);
        }


        /// <summary>
        /// 记录程序结束的日志
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="startTime">程序开始的时间</param>
        internal static void RecordEndLog(string message, DateTime startTime)
        {
            StringBuilder endLog = new StringBuilder();
            endLog.Append("【结束】");
            endLog.Append(message + "。");
            endLog.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】,");
            endLog.Append($"当前时间:【{DateTime.Now}】,");
            endLog.Append($"运行时间:【{(DateTime.Now - startTime).TotalMilliseconds}毫秒】");
            Console.WriteLine(endLog);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">提示信息</param>
        internal static void RecordLog(string message)
        {
            StringBuilder log = new StringBuilder();
            log.Append(message + "。");
            log.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】");
            Console.WriteLine(log);
        }

    }
}
