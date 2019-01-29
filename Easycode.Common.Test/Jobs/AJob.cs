using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.Common.Test.Jobs
{
    public class AJob : ITask
    {
        public Task Execute(ITaskExecutionContext context)
        {
            Console.WriteLine($"执行{context.Name}任务，描述:{context.Description}，执行次数:{context.FireCount}");
            throw new Exception("AJob.抛出自定义异常");
        }
    }
}
