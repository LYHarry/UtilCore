using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时器服务工厂接口
    /// </summary>
    public interface ITimerServiceFactory
    {
        /// <summary>
        /// 得到工作任务调度器
        /// </summary>
        /// <param name="dllName">工作任务所在的项目名称</param>
        /// <returns></returns>
        Task<ITaskScheduler> GetScheduler(string dllName);

        /// <summary>
        /// 得到工作任务调度器
        /// </summary>
        /// <param name="dllName">工作任务所在的项目名称</param>
        /// <param name="fireFileName">触发条件配置文件名称</param>
        /// <returns></returns>
        Task<ITaskScheduler> GetScheduler(string dllName, string fireFileName);
    }
}
