using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.Common.Test.Jobs
{
    public class AJob : ITask
    {
        public Task Execute(TaskExecutionContext context)
        {
            Console.WriteLine("AJob:" + DateTime.Now);
            return Task.FromResult(true);
        }
    }
}
