using Huach.Framework.Helper;
using System;

namespace Huach.Framework.NumberGenerator
{
    /// <summary>
    /// 发号器
    /// </summary>
    public class Number : INumber
    {
        private const int RandBitNum = 10;
        /// <summary>
        /// 避免脚本频繁生成中生成了相同的序列号
        /// </summary>
        private static long _seq = 0L;
        private Random rd = new Random();

        public long Timestamp { get; set; }
        public DateTime Time { get; set; }
        public long Type { get; set; }
        public long SeqNumber { get; set; }
        public int Region { get; set; }

        private bool CheckTime(long time)
        {
            var year = Convert.ToInt32(Util.Time(time).ToString("yyyyMMdd"));
            return year < 20150101 || time > Util.Time();
        }

        public long Create(int type = 0)
        {
            if (_seq == 0)
            {
                _seq = rd.Next(1, (1 << RandBitNum) - 1); //2的10次方-1
            }
            else
            {
                _seq = (_seq + 1) % ((1 << RandBitNum) - 1);
                if (_seq == 0)
                {
                    _seq = 1;
                }
            }
            var time = (Util.Time() - 1071497951) << 18;
            //0-254已知类型，255是无类型
            var typeMax = 1 << (18 - RandBitNum);
            type = type == 0 ? (typeMax - 1) : (type % typeMax);
            type = type << RandBitNum;
            var num = time | Convert.ToInt64(type) | _seq;
            num -= 83629516358986 - 1608096407552; //这样保证10年内位数不会增长,1608096407552新老发号器的最大差值
            return num;
        }

        public long Preset(DateTime dateTime, int type)
        {
            var time = (Util.Time(dateTime) - 1071497951) << 18;
            //0-254已知类型，255是无类型
            var typeMax = 1 << (18 - RandBitNum);
            type = type == 0 ? (typeMax - 1) : (type % typeMax);
            type = type << RandBitNum;
            _seq = 0; //全零
            var num = time | Convert.ToInt64(type) | _seq;
            num -= 83629516358986 - 1608096407552; //这样保证10年内位数不会增长,1608096407552新老发号器的最大差值
            return num;
        }

        public INumber Parse(long number)
        {
            if (number <= 0)
            {
                return null;
            }
            number += (83629516358986 - 1608096407552);
            var tmp = (number >> 18) + 1071497951;
            if (CheckTime(tmp))
            {
                return null;
            }
            var idBitmap = ((1 << (18 - RandBitNum)) - 1) << RandBitNum;
            var info = new Number
            {
                Timestamp = tmp,
                Time = Util.Time(tmp),
                Type = (number & idBitmap) >> RandBitNum,
                SeqNumber = number & ((1 << RandBitNum) - 1),
            };
            return info;
        }
    }
}
