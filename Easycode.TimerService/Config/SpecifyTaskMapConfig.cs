using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Easycode.TimerService
{
    /// <summary>
    /// 指定任务配置
    /// </summary>
    [Serializable]
    public class SpecifyTaskMapConfig
    {
        [XmlElement(ElementName = "TaskTrigger")]
        public List<TaskTriggerConfig> TaskTrigger { get; set; }
    }
}
