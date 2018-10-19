using Hey.Lottery.ViewModels.Base;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 删除
    /// </summary>
    public class DeletePrizeTypeRequest : BaseRequest
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get;  set; }
    }
}