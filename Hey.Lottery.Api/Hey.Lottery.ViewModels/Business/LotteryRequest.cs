using Hey.Lottery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 列表响应
    /// </summary>
    public class LotteryRequest : BaseRequest
    {
        /// <summary>
        /// 奖品类型
        /// </summary>
        public int Type { get; set; }
    }
    /// <summary>
    /// 响应
    /// </summary>
    public class LotteryResponse : BaseResponse
    {
        /// <summary>
        /// 中奖员工Id
        /// </summary>
        public List<int> EmployeeIds { get; set; }
        /// <summary>
        /// 轮数
        /// </summary>
        public object Rounds { get; set; }
        /// <summary>
        /// 当前轮数
        /// </summary>
        public object RoundsIndex { get; set; }
    }
}
