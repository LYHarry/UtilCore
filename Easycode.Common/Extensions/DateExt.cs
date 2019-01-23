using Easycode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Extensions
{
    /// <summary>
    /// DateTime 时间扩展帮助类
    /// </summary>
    public static class DateExtension
    {
        /// <summary>
        /// 得到时间戳格式
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="isBeMsec">是否包含毫秒数</param>
        /// <returns>包含毫秒数返回13位时间戳，否则返回10位时间戳</returns>
        public static long ToUnixTime(this DateTime dt, bool isBeMsec = false)
        {
            //备注：
            //JavaScript时间戳是总毫秒数，Unix时间戳是总秒数
            //格林威治时间GMT以1970/01/01 00:00:00为起始时间
            //北京时间以1970/01/01 08:00:00为起始时间，北京时间与GMT时间相差8小时
            if (RadixHelper.IsEmpty(dt))
                throw new OverflowException("时间溢出");
            //得到当地时区时间 以1970/01/01 8:00:00为起始时间
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            double time = (dt - startTime).TotalSeconds; //相差秒数
            if (isBeMsec)
                time = (dt - startTime).TotalMilliseconds;//相差毫秒数
            return Convert.ToInt64(time);
        }



    }
}
