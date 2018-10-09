using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huach.Framework.NumberGenerator
{
    public interface INumber
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        long Timestamp { get; set; }
        /// <summary>
        /// 时间字符串（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        DateTime Time { get; set; }
        /// <summary>
        /// 发号器类型
        /// </summary>
        long Type { get; set; }
        /// <summary>
        /// 序列编号
        /// </summary>
        long SeqNumber { get; set; }
        /// <summary>
        /// 地区前缀
        /// </summary>
        int Region { get; set; }
        /// <summary>
        /// 发号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        long Create(int type = 0);
        /// <summary>
        /// 取号
        /// </summary>
        /// <returns></returns>
        long Preset(DateTime dateTime, int type);
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        INumber Parse(long number);
    }
}
