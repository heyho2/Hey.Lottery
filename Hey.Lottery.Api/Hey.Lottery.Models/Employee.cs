using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(20)]
        public string Department { get; set; }
    }
}
