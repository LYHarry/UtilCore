using System.Threading.Tasks;

namespace Easycode.TimerService
{
    /// <summary>
    /// 工作任务接口
    /// <para>每个定时任务都需要继承该接口</para>
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns></returns>
        Task Execute(ITaskExecutionContext context);
    }
}
