using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.TimerService
{
    /// <summary>
    /// 定时任务调试配置
    /// </summary>
    public class CrontabConfig
    {
        /// <summary>
        /// 开始执行的年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 开始执行的月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 开始执行的天数
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 开始执行的小时
        /// </summary>
        public int Hour { set; get; }

        /// <summary>
        /// 开始执行的分钟
        /// </summary>
        public int Minute { set; get; }

        /// <summary>
        /// 开始执行的秒
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 执行间隔的天数
        /// </summary>
        public int Interval { set; get; }
    }
}
