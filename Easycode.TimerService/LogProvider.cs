using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Easycode.TimerService
{
    class LogProvider
    {

        private const string NullLogProvider = "Current Log Provider is not set. Call SetCurrentLogProvider " +
                                               "with a non-null value first.";
        private static dynamic s_currentLogProvider;
        private static Action<ILogProvider> s_onCurrentLogProviderSet;

        static LogProvider()
        {
            IsDisabled = false;
        }


        public static void SetCurrentLogProvider(ILogProvider logProvider)
        {
            s_currentLogProvider = logProvider;

            RaiseOnCurrentLogProviderSet();
        }


        public static bool IsDisabled { get; set; }

        internal static Action<ILogProvider> OnCurrentLogProviderSet
        {
            set
            {
                s_onCurrentLogProviderSet = value;
                RaiseOnCurrentLogProviderSet();
            }
        }

        internal static ILogProvider CurrentLogProvider
        {
            get
            {
                return s_currentLogProvider;
            }
        }


        internal static ILog For<T>()
        {
            return GetLogger(typeof(T));
        }


        internal static ILog GetCurrentClassLogger()
        {
            var stackFrame = new StackFrame(1, false);
            return GetLogger(stackFrame.GetMethod().DeclaringType);
        }



        internal static ILog GetLogger(Type type, string fallbackTypeName = "System.Object")
        {
            return GetLogger(type != null ? type.ToString() : fallbackTypeName);
        }


        internal static ILog GetLogger(string name)
        {
            ILogProvider logProvider = CurrentLogProvider ?? ResolveLogProvider();
            return logProvider == null
                ? NoOpLogger.Instance
                : (ILog)new LoggerExecutionWrapper(logProvider.GetLogger(name), () => IsDisabled);
        }


        internal static IDisposable OpenNestedContext(string message)
        {
            ILogProvider logProvider = CurrentLogProvider ?? ResolveLogProvider();

            return logProvider == null
                ? new DisposableAction(() => { })
                : logProvider.OpenNestedContext(message);
        }

        internal static IDisposable OpenMappedContext(string key, string value)
        {
            ILogProvider logProvider = CurrentLogProvider ?? ResolveLogProvider();

            return logProvider == null
                ? new DisposableAction(() => { })
                : logProvider.OpenMappedContext(key, value);
        }


        internal delegate bool IsLoggerAvailable();


        internal delegate ILogProvider CreateLogProvider();


        internal static readonly List<Tuple<IsLoggerAvailable, CreateLogProvider>> LogProviderResolvers =
                new List<Tuple<IsLoggerAvailable, CreateLogProvider>>
                {
                    new Tuple<IsLoggerAvailable, CreateLogProvider>(NLogLogProvider.IsLoggerAvailable, () => new NLogLogProvider()),
                    new Tuple<IsLoggerAvailable, CreateLogProvider>(Log4NetLogProvider.IsLoggerAvailable, () => new Log4NetLogProvider()),
                };

        private static void RaiseOnCurrentLogProviderSet()
        {
            if (s_onCurrentLogProviderSet != null)
            {
                s_onCurrentLogProviderSet(s_currentLogProvider);
            }
        }

        internal static ILogProvider ResolveLogProvider()
        {
            try
            {
                foreach (var providerResolver in LogProviderResolvers)
                {
                    if (providerResolver.Item1())
                    {
                        return providerResolver.Item2();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(

                "Exception occurred resolving a log provider. Logging for this assembly {0} is disabled. {1}",
                    typeof(LogProvider).GetAssemblyPortable().FullName,
                    ex);
            }
            return null;
        }


        internal class NoOpLogger : ILog
        {
            internal static readonly NoOpLogger Instance = new NoOpLogger();

            public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
            {
                return false;
            }


        }
    }
}
