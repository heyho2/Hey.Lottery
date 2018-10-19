using Hey.Lottery.ViewModels.Base;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 添加员工
    /// </summary>
    public class AddEmployeeRequest : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
    }
    /// <summary>
    /// 响应
    /// </summary>
    public class AddEmployeeResponse : BaseResponse
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}