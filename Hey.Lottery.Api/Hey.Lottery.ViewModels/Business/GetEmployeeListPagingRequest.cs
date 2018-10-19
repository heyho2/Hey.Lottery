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
    public class GetEmployeeListPagingResponse : BasePagingResponse<GetEmployeeListPagingItem>
    {

    }
    /// <summary>
    /// 项
    /// </summary>
    public class GetEmployeeListPagingItem : BaseItemResponse
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
    /// <summary>
    /// 请求
    /// </summary>
    public class GetEmployeeListPagingRequest : BasePagingRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
    }
}
