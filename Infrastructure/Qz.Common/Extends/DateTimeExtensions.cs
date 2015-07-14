
namespace Qz.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DateTimeExtensions
    {
        /// <summary>
        /// 获得指定日期与当前日期间隔的秒数 
        /// </summary>
        /// <param name="dt">指定日期</param>
        /// <returns>间隔的秒数</returns>
        public static string Interval(this DateTime dt)
        {
            return (DateTime.Now.Subtract(dt).TotalMilliseconds / 1000.0) + " s";
        }

    }
}
