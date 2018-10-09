using System;
using System.Collections.Generic;
using System.Linq;

namespace Huach.Framework.Helper
{
    public class NpgsqlArray
    {
        /// <summary>
        /// 返回npgsql数组格式字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Get(string value)
        {
            return $"{{{value}}}";
        }
        /// <summary>
        /// 针对npgsql中的数组解析
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<string> Deserialize(string values)
        {
            if (string.IsNullOrWhiteSpace(values) || values.Trim() == "{}" || values.Trim() == "[]")
            {
                return new string[0];
            }
            var result = values.Trim().Trim('{', '}').Split(',');
            return result;
        }
        /// <summary>
        /// 针对npgsql中的数组序列化
        /// </summary>
        /// <returns></returns>
        public static string Serialize(IEnumerable<string> values)
        {

            if (values == null)
            {
                return "{}";
            }
            return $"{{{string.Join(",", values)}}}";
        }
        public static string Add(string values, string value)
        {
            var array = Deserialize(values).ToList();
            array.Add(value);
            return Serialize(array);
        }
        /// <summary>
        /// 添加并去重
        /// </summary>
        /// <param name="values">数据库值</param>
        /// <param name="value">添加的值</param>
        /// <returns></returns>
        public static string AddAndDistinct(string values, string value)
        {
            var array = Deserialize(values).ToList();
            array.Add(value);
            return Serialize(array.Distinct());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="values">数据库值</param>
        /// <param name="value">删除的值</param>
        /// <returns></returns>
        public static string Remove(string values, string value)
        {
            var array = Deserialize(values).ToList();
            array.Remove(value);
            return Serialize(array);
        }
        public static string Operating(string values, Func<IEnumerable<string>, IEnumerable<string>> func)
        {
            var array = Deserialize(values).ToList();
            return Serialize(func(array));
        }
    }
}
