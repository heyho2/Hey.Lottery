using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Linq;

namespace Huach.Framework.Helper
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Util
    {

        /// <summary>
        /// 获取指定时间到格林威治时间的秒数 (对应php中的time()方法)
        /// UTC：格林威治时间1970年01月01日00时00分00秒（UTC+8北京时间1970年01月01日08时00分00秒）
        /// </summary>
        /// <returns></returns>
        public static long Time(DateTime? dateTime = null)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)((dateTime ?? DateTime.Now) - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime Time(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime newTime = dtStart.AddSeconds(timeStamp);
            return newTime;
        }
        /// <summary>
        /// DateTime转换为Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static double MicroTime(DateTime? dateTime = null)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return ((dateTime ?? DateTime.Now) - startTime).TotalSeconds;
        }

        /// <summary>
        /// 获取类的表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>() where T : class
        {
            Type type = typeof(T);
            if (type.IsDefined(typeof(TableAttribute), true))
            {
                TableAttribute tableattribute = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true)[0];
                return tableattribute.Name;
            }
            else
                return type.Name;
        }
    }
}
