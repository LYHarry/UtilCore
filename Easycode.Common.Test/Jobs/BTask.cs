using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.Common.Test.Jobs
{
    public class BTask : IJobTask
    {
        public Task Execute(ITaskExecutionContext context)
        {
            Console.WriteLine($"执行{context.Name}任务，描述:{context.Description}，执行次数:{context.FireCount}");
            return Task.FromResult(true);
        }

        public void OnException(Exception ex)
        {
            Console.WriteLine("OnException:" + ex.Message);
        }
    }
}
