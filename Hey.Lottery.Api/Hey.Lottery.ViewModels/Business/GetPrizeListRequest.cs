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
    public class GetPrizeListRequest : BaseRequest
    {
        /// <summary>
        /// 奖品类型
        /// </summary>
        public int Type { get; set; }
    }
    /// <summary>
    /// 抽奖名单
    /// </summary>
    public class GetPrizeListResponse : BaseListResponse<GetPrizeListItem>
    {

    }
    /// <summary>
    /// 列表
    /// </summary>
    public class GetPrizeListItem : BaseItemResponse
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 奖品
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmployeeName { get; set; }
    }
}
