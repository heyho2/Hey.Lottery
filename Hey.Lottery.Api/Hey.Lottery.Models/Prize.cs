using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Models
{
    /// <summary>
    /// 奖品记录
    /// </summary>
    public class Prize : BaseModel
    {
        /// <summary>
        /// 员工
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 奖品类型
        /// </summary>
        public int PrizeTypeId { get; set; }
    }
}
