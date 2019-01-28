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
            while (true)
            {
                // 加载任务触发条件
                Task.Run(() => { LoadTaskTriggerCondition(TaskFireFilePath); }).Wait();
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
                // 得到当前任务执行上下文
                TaskExecutionContext context = null;
                if (execContextDict.ContainsKey(jobTask.Name))
                    context = execContextDict[jobTask.Name];
                if (context == null)
                {
                    context = new TaskExecutionContext()
                    {
                        Name = jobTask.Name,
                        Description = UtilHelper.Description(jobTask),
                        Result = null,
                        Interval = 0,
                    };
                    execContextDict.Add(jobTask.Name, context);
                }
                // 判断任务触发条件

                // 得到当前任务设置的触发条件
                var triggerConfig = scheduleConfig.SpecifyTaskConfig.TaskTrigger.Where(p => p.Name == context.Name).FirstOrDefault();
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
                if (triggerConfig.Crontab == null && triggerConfig.CycleTask == null)
                    throw new Exception($"未设置触发条件");
                if (triggerConfig.Crontab == null) // 循环任务
                {
                    if (!int.TryParse(triggerConfig.CycleTask.RepeatCount, out int execCount))
                        throw new ArgumentException("重复执行次数设置出错");
                    if (execCount != -1) // -1表示无限循环，不需要判断已执行次数
                    {
                        if (execCount < 0)
                            throw new ArgumentException("重复执行次数设置出错");
                        if (context.FireCount >= execCount)
                            throw new ArgumentException($"已重复执行{execCount}次，无需再次执行");
                    }
                    if (!long.TryParse(triggerConfig.CycleTask.Interval, out long execInterval))
                        throw new ArgumentException("执行间隔时间设置出错");
                    if (execInterval < 1 || execInterval > long.MaxValue)
                        throw new ArgumentException("执行间隔时间设置出错");
                    context.LastFireTime = DateTime.Now;
                }
                if (triggerConfig.CycleTask == null) // 每天间隔任务
                {
                    if (!TimeSpan.TryParse(triggerConfig.Crontab.FireTime, out TimeSpan fireTime))
                        throw new ArgumentException("执行时间设置出错");
                    if (!int.TryParse(triggerConfig.Crontab.Interval, out int execInterval))
                        throw new ArgumentException("执行间隔时间设置出错");
                    if (execInterval < 0 || execInterval > int.MaxValue)
                        throw new ArgumentException("执行间隔时间设置出错");

                    //通过上次执行时间加上执行间隔天数等天下次执行时间
                    DateTime nextTime = context.LastFireTime.AddDays(execInterval);
                    if (DateTime.Now < nextTime)
                        throw new Exception("未到执行时间");
                    if (DateTime.Now > nextTime.AddMinutes(20)) // 充许20分钟时差
                        throw new Exception("执行时间已过");
                    context.LastFireTime = nextTime;
                }
                //当前任务执行次数加1
                context.FireCount++;
                //  创建具体的工作任务(JobTask)实例
                ConstructorInfo ci = jobTask.GetConstructor(Type.EmptyTypes);
                if (!(ci.Invoke(new object[0]) is ITask task))
                    throw new Exception("实例必须为 ITask 派生类");
                // 执行具体任务
                task.Execute(context);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                UtilHelper.RecordLog($"执行【{jobTask.Name}】任务出错：此任务{ex.Message}");
                Thread.Sleep(3000);
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
