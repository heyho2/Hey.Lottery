using Hey.Lottery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 响应
    /// </summary>
    public class GetPrizePagingResponse : BasePagingResponse<GetPrizePagingItem>
    {

    }
    /// <summary>
    /// 项
    /// </summary>
    public class GetPrizePagingItem : BaseItemResponse
    {
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
    /// <summary>
    /// 请求
    /// </summary>
    public class GetPrizePagingRequest : BasePagingRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
