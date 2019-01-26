using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.Common.Test.Jobs
{
    public class BTask : IJobTask
    {
        public Task Execute(TaskExecutionContext context)
        {
            Console.WriteLine("BTask:" + DateTime.Now);
            return Task.FromResult(true);
        }

        public void OnException(Exception ex)
        {
            Console.WriteLine("OnException:" + ex.Message);
        }
    }
}
