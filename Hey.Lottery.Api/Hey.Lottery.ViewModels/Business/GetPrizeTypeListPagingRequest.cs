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
    public class GetPrizeTypeListPagingResponse : BasePagingResponse<GetPrizeTypeListPagingItem>
    {

    }
    /// <summary>
    /// 项
    /// </summary>
    public class GetPrizeTypeListPagingItem : BaseItemResponse
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
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 每轮数量
        /// </summary>
        public int Num { get; set; }
    }
    /// <summary>
    /// 请求
    /// </summary>
    public class GetPrizeTypeListPagingRequest : BasePagingRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
