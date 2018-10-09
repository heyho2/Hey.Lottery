using System;
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
        [StringLength(20)]
        public string Name { get; set; }
    }
}
