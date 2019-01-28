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
            ITaskScheduler task = factory.GetScheduler("Easycode.Common.Test").Result;
            task.Start().Wait();


            Console.ReadKey();
        }
    }
}
