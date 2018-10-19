using Hey.Lottery.ViewModels.Base;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 添加员工
    /// </summary>
    public class GetNotWonEmployeeListResponse : BaseListResponse<GetNotWonEmployeeListItem>
    {

    }
    /// <summary>
    /// 数据项
    /// </summary>
    public class GetNotWonEmployeeListItem : BaseItemResponse
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
}