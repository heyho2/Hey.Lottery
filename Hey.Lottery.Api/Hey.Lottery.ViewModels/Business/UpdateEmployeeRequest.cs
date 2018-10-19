using Hey.Lottery.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 修改
    /// </summary>
    public class UpdateEmployeeRequest : BaseRequest
    {
        /// <summary>
        /// Id 必填
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}