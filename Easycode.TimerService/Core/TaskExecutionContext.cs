using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    public class TaskExecutionContext : ITaskExecutionContext
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public object Result { get; set; }

        public int FireCount { get; set; }

        public int Interval { get; set; }

        public object ExtraData { get; set; }

        public DateTime LastFireTime { get; set; }
    }
}
