using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easycode.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            ITimerServiceFactory factory = new TimerServiceFactory();
            ITaskScheduler task = factory.GetScheduler("Easycode.Common.Tests").Result;
            task.Start().Wait();


            Console.ReadKey();
        }



        private static void LoadModules(string dllName)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var assPaths = Directory.GetFiles(dir, $"*{dllName}*.dll");
            //得到项目路径下所有的dll文件
            var loadResult = ReflectHelper.LoadFiles(assPaths);
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
                    //var module = Activator.CreateInstance(moduleType) as ITask;
                    //if (module == null) { continue; }
                    //TaskModules.Enqueue(module);
                }
            }
        }



    }
}
