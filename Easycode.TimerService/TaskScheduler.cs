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

namespace Easycode.TimerService
{
    public class TaskScheduler : ITaskScheduler
    {
        private List<JobTaskDetail> TaskModules { get; set; } = new List<JobTaskDetail>();

        /// <summary>
        /// 工作任务所在的项目名称
        /// </summary>
        public string TaskProcessName { get; set; }

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            //加载工作任务模块
            Task.Factory.StartNew(() => { LoadModules(TaskProcessName); });
            Task.Run(async () =>
            {
                while (true)
                {
                    if (TaskModules.Count > 0)
                    {
                        foreach (var item in TaskModules)
                        {
                            await ExecuteTask(item);
                            //Task.Factory.StartNew(async () => { await ExecuteTask(item); });
                        }
                        continue;
                    }
                    Console.WriteLine($"没有需要执行的任务，等待1秒。当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】");
                    Thread.Sleep(1000);
                }
            });
            return Task.CompletedTask;
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

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="jobTask"></param>
        /// <returns></returns>
        private Task ExecuteTask(JobTaskDetail jobTask)
        {
            Console.WriteLine();
            RecordStartLog($"执行【{jobTask.Name}】任务", out DateTime stime);
            try
            {
                Thread.Sleep(2000);
            }
            catch { throw; }
            finally
            {
                RecordEndLog($"执行【{jobTask.Name}】任务", stime);
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
            RecordStartLog($"加载【{dllName}】项目下工作任务", out DateTime stime);
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
                    foreach (Type module in moduleTypes)
                    {
                        TaskModules.Add(new JobTaskDetail()
                        {
                            Name = module.Name,
                            CName = module.Description(),
                            FullName = module.FullName,
                            Namespace = module.Namespace
                        });
                    }
                }
            }
            catch { throw; }
            finally
            {
                RecordEndLog("加载工作任务", stime);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 记录程序开始的日志
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="stime">返回当前开始时间</param>
        private void RecordStartLog(string message, out DateTime stime)
        {
            stime = DateTime.Now;
            StringBuilder startLog = new StringBuilder("【开始】");
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
        private void RecordEndLog(string message, DateTime startTime)
        {
            StringBuilder endLog = new StringBuilder("【结束】");
            endLog.Append(message + "。");
            endLog.Append($"当前线程ID:【{Thread.CurrentThread.ManagedThreadId}】,");
            endLog.Append($"当前时间:【{DateTime.Now}】,");
            endLog.Append($"运行时间:【{(DateTime.Now - startTime).TotalMilliseconds}毫秒】");
            Console.WriteLine(endLog);
        }

    }
}
