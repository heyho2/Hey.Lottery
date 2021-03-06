﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Models
{
    /// <summary>
    /// 奖品类型
    /// </summary>
    public class PrizeType : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(30)]
        public string PrizeName { get; set; }
        /// <summary>
        /// 奖品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        [StringLength(30)]
        public string Type { get; set; }
        /// <summary>
        /// 每轮抽奖个数
        /// </summary>
        public int Num { get; set; }
    }
}
