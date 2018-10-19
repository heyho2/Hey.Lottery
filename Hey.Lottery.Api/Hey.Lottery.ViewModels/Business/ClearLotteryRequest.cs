using Hey.Lottery.ViewModels.Base;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 添加员工
    /// </summary>
    public class ClearLotteryRequest : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public int PrizeTypeId { get; set; }
    }
}