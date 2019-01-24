﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Easycode.TimerService
{
    internal class Log4NetLogProvider: LogProviderBase
    {
            private readonly Func<Assembly, string, object> _getLoggerByNameDelegate;
            private static bool s_providerIsAvailableOverride = true;

            public Log4NetLogProvider()
            {
                if (!IsLoggerAvailable())
                {
                    throw new InvalidOperationException("log4net.LogManager not found");
                }
                _getLoggerByNameDelegate = GetGetLoggerMethodCall();
            }

            public static bool ProviderIsAvailableOverride
            {
                get { return s_providerIsAvailableOverride; }
                set { s_providerIsAvailableOverride = value; }
            }

            public override Logger GetLogger(string name)
            {

            return new Log4NetLogger(_getLoggerByNameDelegate(
                typeof(ILog).GetType().GetTypeInfo().Assembly, name)).Log;

                return new Log4NetLogger(_getLoggerByNameDelegate(
                    typeof(ILog).GetType().Assembly, name)).Log;

            }

            internal static bool IsLoggerAvailable()
            {
                return ProviderIsAvailableOverride && GetLogManagerType() != null;
            }

            protected override OpenNdc GetOpenNdcMethod()
            {
                Type logicalThreadContextType = Type.GetType("log4net.LogicalThreadContext, log4net");
                PropertyInfo stacksProperty = logicalThreadContextType.GetPropertyPortable("Stacks");
                Type logicalThreadContextStacksType = stacksProperty.PropertyType;
                PropertyInfo stacksIndexerProperty = logicalThreadContextStacksType.GetPropertyPortable("Item");
                Type stackType = stacksIndexerProperty.PropertyType;
                MethodInfo pushMethod = stackType.GetMethodPortable("Push");

                ParameterExpression messageParameter =
                    Expression.Parameter(typeof(string), "message");

                // message => LogicalThreadContext.Stacks.Item["NDC"].Push(message);
                MethodCallExpression callPushBody =
                    Expression.Call(
                        Expression.Property(Expression.Property(null, stacksProperty),
                            stacksIndexerProperty,
                            Expression.Constant("NDC")),
                        pushMethod,
                        messageParameter);

                OpenNdc result =
                    Expression.Lambda<OpenNdc>(callPushBody, messageParameter)
                        .Compile();

                return result;
            }

            protected override OpenMdc GetOpenMdcMethod()
            {
                Type logicalThreadContextType = Type.GetType("log4net.LogicalThreadContext, log4net");
                PropertyInfo propertiesProperty = logicalThreadContextType.GetPropertyPortable("Properties");
                Type logicalThreadContextPropertiesType = propertiesProperty.PropertyType;
                PropertyInfo propertiesIndexerProperty = logicalThreadContextPropertiesType.GetPropertyPortable("Item");

                MethodInfo removeMethod = logicalThreadContextPropertiesType.GetMethodPortable("Remove");

                ParameterExpression keyParam = Expression.Parameter(typeof(string), "key");
                ParameterExpression valueParam = Expression.Parameter(typeof(string), "value");

                MemberExpression propertiesExpression = Expression.Property(null, propertiesProperty);

                // (key, value) => LogicalThreadContext.Properties.Item[key] = value;
                BinaryExpression setProperties = Expression.Assign(Expression.Property(propertiesExpression, propertiesIndexerProperty, keyParam), valueParam);

                // key => LogicalThreadContext.Properties.Remove(key);
                MethodCallExpression removeMethodCall = Expression.Call(propertiesExpression, removeMethod, keyParam);

                Action<string, string> set = Expression
                    .Lambda<Action<string, string>>(setProperties, keyParam, valueParam)
                    .Compile();

                Action<string> remove = Expression
                    .Lambda<Action<string>>(removeMethodCall, keyParam)
                    .Compile();

                return (key, value) =>
                {
                    set(key, value);
                    return new DisposableAction(() => remove(key));
                };
            }

            private static Type GetLogManagerType()
            {
                return Type.GetType("log4net.LogManager, log4net");
            }

            private static Func<Assembly, string, object> GetGetLoggerMethodCall()
            {
                Type logManagerType = GetLogManagerType();
                MethodInfo method = logManagerType.GetMethodPortable("GetLogger", typeof(Assembly), typeof(string));
                ParameterExpression assemblyParam = Expression.Parameter(typeof(Assembly), "repositoryAssembly");
                ParameterExpression nameParam = Expression.Parameter(typeof(string), "name");
                MethodCallExpression methodCall = Expression.Call(null, method, assemblyParam, nameParam);
                return Expression.Lambda<Func<Assembly, string, object>>(methodCall, assemblyParam, nameParam).Compile();
            }

            internal class Log4NetLogger
            {
                private readonly dynamic _logger;

                private static readonly object _levelDebug;
                private static readonly object _levelInfo;
                private static readonly object _levelWarn;
                private static readonly object _levelError;
                private static readonly object _levelFatal;
                private static readonly Func<object, object, bool> _isEnabledForDelegate;
                private static readonly Action<object, object> _logDelegate;
                private static readonly Func<object, Type, object, string, Exception, object> _createLoggingEvent;
                private static readonly Action<object, string, object> _loggingEventPropertySetter;

                static Log4NetLogger()
                {
                    var logEventLevelType = Type.GetType("log4net.Core.Level, log4net");
                    if (logEventLevelType == null)
                    {
                        throw new InvalidOperationException("Type log4net.Core.Level was not found.");
                    }

                    var levelFields = logEventLevelType.GetFieldsPortable().ToList();
                    _levelDebug = levelFields.First(x => x.Name == "Debug").GetValue(null);
                    _levelInfo = levelFields.First(x => x.Name == "Info").GetValue(null);
                    _levelWarn = levelFields.First(x => x.Name == "Warn").GetValue(null);
                    _levelError = levelFields.First(x => x.Name == "Error").GetValue(null);
                    _levelFatal = levelFields.First(x => x.Name == "Fatal").GetValue(null);

                    // Func<object, object, bool> isEnabledFor = (logger, level) => { return ((log4net.Core.ILogger)logger).IsEnabled(level); }
                    var loggerType = Type.GetType("log4net.Core.ILogger, log4net");
                    if (loggerType == null)
                    {
                        throw new InvalidOperationException("Type log4net.Core.ILogger, was not found.");
                    }
                    ParameterExpression instanceParam = Expression.Parameter(typeof(object));
                    UnaryExpression instanceCast = Expression.Convert(instanceParam, loggerType);
                    ParameterExpression levelParam = Expression.Parameter(typeof(object));
                    UnaryExpression levelCast = Expression.Convert(levelParam, logEventLevelType);
                    _isEnabledForDelegate = GetIsEnabledFor(loggerType, logEventLevelType, instanceCast, levelCast, instanceParam, levelParam);

                    Type loggingEventType = Type.GetType("log4net.Core.LoggingEvent, log4net");

                    _createLoggingEvent = GetCreateLoggingEvent(instanceParam, instanceCast, levelParam, levelCast, loggingEventType);

                    _logDelegate = GetLogDelegate(loggerType, loggingEventType, instanceCast, instanceParam);

                    _loggingEventPropertySetter = GetLoggingEventPropertySetter(loggingEventType);
                }

                internal Log4NetLogger(dynamic logger)
                {
                    _logger = logger.Logger;
                }

                private static Action<object, object> GetLogDelegate(Type loggerType, Type loggingEventType, UnaryExpression instanceCast,
                    ParameterExpression instanceParam)
                {
                    //Action<object, object, string, Exception> Log =
                    //(logger, callerStackBoundaryDeclaringType, level, message, exception) => { ((ILogger)logger).Log(new LoggingEvent(callerStackBoundaryDeclaringType, logger.Repository, logger.Name, level, message, exception)); }
                    MethodInfo writeExceptionMethodInfo = loggerType.GetMethodPortable("Log",
                        loggingEventType);

                    ParameterExpression loggingEventParameter =
                        Expression.Parameter(typeof(object), "loggingEvent");

                    UnaryExpression loggingEventCasted =
                        Expression.Convert(loggingEventParameter, loggingEventType);

                    var writeMethodExp = Expression.Call(
                        instanceCast,
                        writeExceptionMethodInfo,
                        loggingEventCasted);

                    var logDelegate = Expression.Lambda<Action<object, object>>(
                        writeMethodExp,
                        instanceParam,
                        loggingEventParameter).Compile();

                    return logDelegate;
                }

                private static Func<object, Type, object, string, Exception, object> GetCreateLoggingEvent(ParameterExpression instanceParam, UnaryExpression instanceCast, ParameterExpression levelParam, UnaryExpression levelCast, Type loggingEventType)
                {
                    ParameterExpression callerStackBoundaryDeclaringTypeParam = Expression.Parameter(typeof(Type));
                    ParameterExpression messageParam = Expression.Parameter(typeof(string));
                    ParameterExpression exceptionParam = Expression.Parameter(typeof(Exception));

                    PropertyInfo repositoryProperty = loggingEventType.GetPropertyPortable("Repository");
                    PropertyInfo levelProperty = loggingEventType.GetPropertyPortable("Level");

                    ConstructorInfo loggingEventConstructor =
                        loggingEventType.GetConstructorPortable(typeof(Type), repositoryProperty.PropertyType, typeof(string), levelProperty.PropertyType, typeof(object), typeof(Exception));

                    //Func<object, object, string, Exception, object> Log =
                    //(logger, callerStackBoundaryDeclaringType, level, message, exception) => new LoggingEvent(callerStackBoundaryDeclaringType, ((ILogger)logger).Repository, ((ILogger)logger).Name, (Level)level, message, exception); }
                    NewExpression newLoggingEventExpression =
                        Expression.New(loggingEventConstructor,
                            callerStackBoundaryDeclaringTypeParam,
                            Expression.Property(instanceCast, "Repository"),
                            Expression.Property(instanceCast, "Name"),
                            levelCast,
                            messageParam,
                            exceptionParam);

                    var createLoggingEvent =
                        Expression.Lambda<Func<object, Type, object, string, Exception, object>>(
                                newLoggingEventExpression,
                                instanceParam,
                                callerStackBoundaryDeclaringTypeParam,
                                levelParam,
                                messageParam,
                                exceptionParam)
                            .Compile();

                    return createLoggingEvent;
                }

                private static Func<object, object, bool> GetIsEnabledFor(Type loggerType, Type logEventLevelType,
                    UnaryExpression instanceCast,
                    UnaryExpression levelCast,
                    ParameterExpression instanceParam,
                    ParameterExpression levelParam)
                {
                    MethodInfo isEnabledMethodInfo = loggerType.GetMethodPortable("IsEnabledFor", logEventLevelType);
                    MethodCallExpression isEnabledMethodCall = Expression.Call(instanceCast, isEnabledMethodInfo, levelCast);

                    Func<object, object, bool> result =
                        Expression.Lambda<Func<object, object, bool>>(isEnabledMethodCall, instanceParam, levelParam)
                            .Compile();

                    return result;
                }

                private static Action<object, string, object> GetLoggingEventPropertySetter(Type loggingEventType)
                {
                    ParameterExpression loggingEventParameter = Expression.Parameter(typeof(object), "loggingEvent");
                    ParameterExpression keyParameter = Expression.Parameter(typeof(string), "key");
                    ParameterExpression valueParameter = Expression.Parameter(typeof(object), "value");

                    PropertyInfo propertiesProperty = loggingEventType.GetPropertyPortable("Properties");
                    PropertyInfo item = propertiesProperty.PropertyType.GetPropertyPortable("Item");

                    // ((LoggingEvent)loggingEvent).Properties[key] = value;
                    var body =
                        Expression.Assign(
                            Expression.Property(
                                Expression.Property(Expression.Convert(loggingEventParameter, loggingEventType),
                                    propertiesProperty), item, keyParameter), valueParameter);

                    Action<object, string, object> result =
                        Expression.Lambda<Action<object, string, object>>
                            (body, loggingEventParameter, keyParameter,
                                valueParameter)
                            .Compile();

                    return result;
                }

                public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
                {
                    if (messageFunc == null)
                    {
                        return IsLogLevelEnable(logLevel);
                    }

                    if (!IsLogLevelEnable(logLevel))
                    {
                        return false;
                    }

                    string formattedMessage =
                        LogMessageFormatter.FormatStructuredMessage(messageFunc(),
                            formatParameters,
                            out var patternMatches);

                    Type callerStackBoundaryType = typeof(Log4NetLogger);

                    var methodType = messageFunc.Method.DeclaringType;
                    if (methodType == typeof(LogExtensions) || (methodType != null && methodType.DeclaringType == typeof(LogExtensions)))
                    {
                        callerStackBoundaryType = typeof(LogExtensions);
                    }
                    else if (methodType == typeof(LoggerExecutionWrapper) || (methodType != null && methodType.DeclaringType == typeof(LoggerExecutionWrapper)))
                    {
                        callerStackBoundaryType = typeof(LoggerExecutionWrapper);
                    }


                    var translatedLevel = TranslateLevel(logLevel);

                    object loggingEvent = _createLoggingEvent(_logger, callerStackBoundaryType, translatedLevel, formattedMessage, exception);

                    PopulateProperties(loggingEvent, patternMatches, formatParameters);

                    _logDelegate(_logger, loggingEvent);

                    return true;
                }

                private void PopulateProperties(object loggingEvent, IEnumerable<string> patternMatches, object[] formatParameters)
                {
                    if (patternMatches.Count() > 0)
                    {
                        IEnumerable<KeyValuePair<string, object>> keyToValue =
                        patternMatches.Zip(formatParameters,
                                           (key, value) => new KeyValuePair<string, object>(key, value));

                        foreach (KeyValuePair<string, object> keyValuePair in keyToValue)
                        {
                            _loggingEventPropertySetter(loggingEvent, keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                }

                private bool IsLogLevelEnable(LogLevel logLevel)
                {
                    var level = TranslateLevel(logLevel);
                    return _isEnabledForDelegate(_logger, level);
                }

                private object TranslateLevel(LogLevel logLevel)
                {
                    switch (logLevel)
                    {
                        case LogLevel.Trace:
                        case LogLevel.Debug:
                            return _levelDebug;
                        case LogLevel.Info:
                            return _levelInfo;
                        case LogLevel.Warn:
                            return _levelWarn;
                        case LogLevel.Error:
                            return _levelError;
                        case LogLevel.Fatal:
                            return _levelFatal;
                        default:
                            throw new ArgumentOutOfRangeException("logLevel", logLevel, null);
                    }
                }
            }
        
    }
}
