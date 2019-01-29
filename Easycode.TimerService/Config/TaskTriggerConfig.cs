using System;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    /// <summary>
    /// 具体任务触发条件配置
    /// </summary>
    [Serializable]
    public class TaskTriggerConfig
    {
        /// <summary>
        /// 任务名称(工作任务类名)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述(获取 DescriptionAttribute 特性内容)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 定时周期任务触发条件配置
        /// </summary>
        public CrontabConfig Crontab { get; set; }

        /// <summary>
        /// 循环任务触发条件配置
        /// </summary>
        public CycleTaskConfig CycleTask { get; set; }

        /// <summary>
        /// 任务附加参数数据
        /// </summary>
        [XmlElement(ElementName = "ExtraData")]
        public object ExtraData { get; set; }
    }
}
