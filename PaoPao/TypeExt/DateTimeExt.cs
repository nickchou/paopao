using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoPao.TypeExt
{
    public static class DateTimeExt
    {
        /// <summary>
        /// 获取中文的时间部分格式 HH:mm:ss.fff
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CnTime(this DateTime source)
        {
            return source.ToString("HH:mm:ss.fff");
        }
        /// <summary>
        /// 获取中文的日期+时间部分格式yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CnDate(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
