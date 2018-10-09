using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
