using Hey.Lottery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels.Business
{

    /// <summary>
    /// 抽奖名单
    /// </summary>
    public class GetPrizeTypeAllResponse : BaseListResponse<GetPrizeTypeAllItem>
    {

    }
    /// <summary>
    /// 列表
    /// </summary>
    public class GetPrizeTypeAllItem : BaseItemResponse
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 奖品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 每轮数量
        /// </summary>
        public int Num { get; set; }
    }
}
