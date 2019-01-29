using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    /// <summary>
    /// 指定任务计划安排配置
    /// </summary>
    [Serializable]
    public class SpecifyTaskMapConfig
    {
        /// <summary>
        /// 指定任务配置列表
        /// </summary>
        [XmlElement(ElementName = "TaskTrigger")]
        public List<TaskTriggerConfig> TaskTrigger { get; set; }
    }
}
