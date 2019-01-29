using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    public class TaskScheduler : ITaskScheduler
    {
        private List<Type> TaskModules { get; set; } = new List<Type>();
        private TaskScheduleConfig scheduleConfig;
        private Dictionary<string, TaskExecutionContext> execContextDict = new Dictionary<string, TaskExecutionContext>();

        /// <summary>
        /// 工作任务所在的项目名称
        /// </summary>
        public string TaskProcessName { get; set; }

        /// <summary>
        /// 任务触发配置文件路径
        /// </summary>
        public string TaskFireFilePath { get; set; }

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            // 加载工作任务模块
            Task.Run(() => { LoadModules(TaskProcessName); }).Wait();
            // 加载任务触发条件
            Task.Run(() => { LoadTaskTriggerCondition(TaskFireFilePath); }).Wait();

            while (true)
            {
                if (TaskModules.Count > 0)
                {
                    if (scheduleConfig == null)
                    {
                        UtilHelper.RecordLog("未加载到任务触发条件，等待1秒");
                        Thread.Sleep(1000);
                        continue;
                    }
                    foreach (var item in TaskModules)
                    {
                        // 执行工作任务
                        Task.Run(() => { ExecuteTask(item); }).Wait();
                    }
                    continue;
                }
                UtilHelper.RecordLog("没有需要执行的任务，等待1秒");
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public Task Stop()
        {
            TaskModules.Clear();
            return Task.CompletedTask;
        }


        public Task Stop(string taskName)
        {
            var task = TaskModules.Where(p => p.Name == taskName).FirstOrDefault();
            if (task != null)
                TaskModules.Remove(task);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="jobTask"></param>
        /// <returns></returns>
        private Task ExecuteTask(Type jobTask)
        {
            UtilHelper.RecordStartLog($"执行【{jobTask.Name}】任务", out DateTime stime);
            try
            {
                #region 得到任务执行上下文

                TaskExecutionContext context = null;
                if (execContextDict.ContainsKey(jobTask.Name))
                    context = execContextDict[jobTask.Name];
                if (context == null)
                {
                    context = new TaskExecutionContext()
                    {
                        Name = jobTask.Name,
                        Description = UtilHelper.Description(jobTask),
                    };
                    execContextDict.Add(jobTask.Name, context);
                }

                #endregion

                #region 判断任务触发条件

                // 得到当前任务设置的触发条件
                var triggerConfig = scheduleConfig.SpecifyTaskConfig?.TaskTrigger?.Where(p => p.Name == context.Name).FirstOrDefault();
                if (triggerConfig == null) //没有配置单个任务触发条件
                {
                    triggerConfig = new TaskTriggerConfig()
                    {
                        Name = context.Name,
                        Description = context.Description,
                        Crontab = new CrontabConfig()
                        {
                            FireTime = scheduleConfig.FireTime,
                            Interval = "1"
                        }
                    };
                }
                if (string.IsNullOrWhiteSpace(triggerConfig.Crontab?.FireTime))
                    triggerConfig.Crontab = null;
                if (string.IsNullOrWhiteSpace(triggerConfig.CycleTask?.RepeatCount))
                    triggerConfig.CycleTask = null;
                if (triggerConfig.Crontab == null && triggerConfig.CycleTask == null)
                    throw new Exception($"未设置触发条件");
                // 循环任务
                if (triggerConfig.Crontab == null)
                {
                    if (!int.TryParse(triggerConfig.CycleTask.RepeatCount, out int execCount))
                        throw new ArgumentException("重复执行次数设置出错");
                    if (execCount != -1) // -1表示无限循环，不需要判断已执行次数
                    {
                        if (execCount < 0)
                            throw new ArgumentException("重复执行次数设置出错");
                        if (context.FireCount >= execCount)
                            throw new ArgumentException($"已重复执行【{execCount}】次，无需再次执行");
                    }
                    if (!long.TryParse(triggerConfig.CycleTask.Interval, out long execInterval))
                        throw new ArgumentException("执行间隔时间设置出错");
                    if (execInterval < 1 || execInterval > long.MaxValue)
                        throw new ArgumentException("执行间隔时间设置出错");
                    if (context.LastFireTime.HasValue)
                    {
                        var currTime = DateTime.Now;
                        var execTime = context.LastFireTime.Value.AddSeconds(execInterval);
                        if (!UtilHelper.IsEqualTime(currTime, execTime))
                            throw new Exception($"未到执行时间【{execTime}】");
                        if (!(currTime.Second >= execTime.Second && currTime.Second <= (execTime.Second + 10)))
                            throw new Exception($"未到执行时间【{execTime}】"); //充许10秒时差
                    }
                    context.LastFireTime = DateTime.Now;
                }
                // 每天间隔任务
                if (triggerConfig.CycleTask == null)
                {
                    if (!TimeSpan.TryParse(triggerConfig.Crontab.FireTime, out TimeSpan fireTime))
                        throw new ArgumentException("执行时间设置出错");
                    if (!int.TryParse(triggerConfig.Crontab.Interval, out int execInterval))
                        throw new ArgumentException("执行间隔时间设置出错");
                    if (execInterval < 0 || execInterval > int.MaxValue)
                        throw new ArgumentException("执行间隔时间设置出错");
                    //服务启动就执行一次
                    if (context.LastFireTime.HasValue)
                    {
                        var currTime = DateTime.Now.TimeOfDay;// 充许1分钟时差
                        if (currTime.Hours != fireTime.Hours || currTime.Minutes != fireTime.Minutes)
                            throw new Exception($"未到执行时间【{fireTime}】");
                        var lastExecTime = context.LastFireTime.Value.TimeOfDay;
                        if (lastExecTime.Hours == currTime.Hours && lastExecTime.Minutes == currTime.Minutes)
                        {
                            var execDate = context.LastFireTime.Value.AddDays(execInterval).Date;
                            if (execDate != DateTime.Now.Date)
                                throw new Exception($"未到执行时间【{execDate.ToShortDateString()} {fireTime}】");
                        }
                        //else 表示上次不是在指定时间执行的，仍然执行一次
                    }
                    context.LastFireTime = DateTime.Now;
                }

                #endregion

                #region 执行具体任务

                //当前任务执行次数加1
                context.FireCount++;

                //创建具体的工作任务(JobTask)实例
                ConstructorInfo ci = jobTask.GetConstructor(Type.EmptyTypes);
                if (!(ci.Invoke(new object[0]) is ITask task))
                    throw new Exception("实例必须为 ITask 派生类");
                try
                {
                    // 执行具体任务
                    task.Execute(context);
                }
                catch (Exception ex)
                {
                    if (task is IJobTask extask)
                    {
                        extask.OnException(new Exception($"执行【{jobTask.Name}】任务出错", ex));
                        return Task.CompletedTask;
                    }
                    UtilHelper.RecordLog($"执行【{jobTask.Name}】任务出错：此任务{ex.Message}");
                }

                #endregion
            }
            catch (Exception ex)
            {
                UtilHelper.RecordLog($"【{jobTask.Name}】任务{ex.Message}");
            }
            finally
            {
                UtilHelper.RecordEndLog($"执行【{jobTask.Name}】任务", stime);
            }
            return Task.CompletedTask;
        }



        /// <summary>
        /// 加载工作任务模块
        /// </summary>
        /// <param name="dllName">工作任务所在的dll名称</param>
        /// <returns></returns>
        private Task LoadModules(string dllName)
        {
            UtilHelper.RecordStartLog($"加载【{dllName}】项目下工作任务", out DateTime stime);
            try
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                //得到项目路径下所有的dll文件
                var loadResult = UtilHelper.LoadFiles(dir, dllName);
                if (loadResult == null || loadResult.Count < 1)
                    throw new FileNotFoundException($"{dir}目录没有可执行dll文件");

                List<Type> moduleTypes = null;
                foreach (Assembly assembly in loadResult)
                {
                    moduleTypes = assembly.GetTypes()
                                          .Where(m => m.GetInterface(nameof(ITask), true) != null && m.IsAbstract == false)
                                          .ToList();
                    if (moduleTypes.Count < 1) { continue; }
                    TaskModules.AddRange(moduleTypes);
                }
            }
            catch (Exception ex)
            {
                UtilHelper.RecordLog($"加载工作任务出错：{ex.Message}");
            }
            finally
            {
                UtilHelper.RecordEndLog("加载工作任务", stime);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 加载任务触发条件
        /// </summary>
        /// <param name="filePath">任务触发条件配置文件路径</param>
        /// <returns></returns>
        private Task LoadTaskTriggerCondition(string filePath)
        {
            UtilHelper.RecordStartLog("加载任务触发条件", out DateTime stime);
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new ArgumentNullException("未设置任务触发条件配置文件名");
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("未创建任务触发配置文件", filePath);
                string xml = UtilHelper.LoadFile(filePath);
                scheduleConfig = UtilHelper.Deserialize<TaskScheduleConfig>(xml);
                if (scheduleConfig == null)
                    throw new FileLoadException("任务触发配置文件序列化失败");
            }
            catch (Exception ex)
            {
                UtilHelper.RecordLog($"加载任务配置文件出错：{ex.Message}");
            }
            finally
            {
                UtilHelper.RecordEndLog("加载任务触发条件", stime);
            }
            return Task.CompletedTask;
        }

    }
}
