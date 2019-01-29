using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂
    /// </summary>
    public class TimerServiceFactory : ITimerServiceFactory
    {
        /// <summary>
        /// 得到工作任务调度器
        /// </summary>
        /// <param name="dllName">工作任务所在的项目名称</param>
        /// <param name="fireFileName">触发条件配置文件名称</param>
        /// <returns></returns>
        public Task<ITaskScheduler> GetScheduler(string dllName, string fireFileName)
        {
            ITaskScheduler ts = new TaskScheduler
            {
                TaskProcessName = UtilHelper.ToTrim(dllName),
                TaskFireFileName = UtilHelper.ToTrim(fireFileName)
            };
            return Task.FromResult(ts);
        }

        /// <summary>
        /// 得到工作任务调度器
        /// </summary>
        /// <param name="dllName">工作任务所在的项目名称</param>
        /// <returns></returns>
        public Task<ITaskScheduler> GetScheduler(string dllName)
        {
            return GetScheduler(dllName, "TaskFire.config");
        }
    }
}
