using Hey.Lottery.ViewModels.Base;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 删除部门
    /// </summary>
    public class EmployeeDeleteRequest : BaseRequest
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get;  set; }
    }
}