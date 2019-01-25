using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂实现类
    /// </summary>
    public class TimerServiceFactory : ITimerServiceFactory
    {
        public ConcurrentQueue<ITask> TaskModules { get; set; } = new ConcurrentQueue<ITask>();


        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Stop(TaskModule task)
        {
            throw new NotImplementedException();
        }


        private void LoadModules()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var assPaths = Directory.GetFiles(dir, "*.dll");
            //得到项目路径下所有的dll文件
            var loadResult = UtilHelper.LoadFiles(assPaths);
            if (loadResult == null || loadResult.Count < 1)
                throw new FileNotFoundException($"{dir}目录没有可执行dll文件");

            List<Type> moduleTypes = null;
            foreach (Assembly assembly in loadResult)
            {
                moduleTypes = assembly.GetTypes()
                                      .Where(m => m.GetInterface(nameof(ITask), true) != null && m.IsAbstract == false)
                                      .ToList();
                foreach (Type moduleType in moduleTypes)
                {
                    var module = Activator.CreateInstance(moduleType) as ITask;
                    if (module == null) { continue; }
                    TaskModules.Enqueue(module);
                }
            }
        }


        internal void RunLoop()
        {
            if (TaskModules.Count < 1) { return; }

            TaskModules.TryDequeue(out ITask module);
            var desc = module.Description();
            if (desc.DataType != typeof(ModuleConfig)) { return; }

            ModuleConfig config = desc.Data as ModuleConfig;
            if (config == null) { return; }
            if (config.Hour != DateTime.Now.Hour || config.Minute != DateTime.Now.Minute)
            {
                Console.WriteLine("{0}（{1}）未到执行时间{2}:{3}", desc.ModuleCnName, desc.ModuleEnName, config.Hour, config.Minute);
                Modules.Enqueue(module);
                return;
            }
            try
            {
                var loadResult = module.Load();
            }
            catch (Exception exception)
            {
                module.OnLoadModuleException(exception);
            }
        }

    }
}
