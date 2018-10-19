using Hey.Lottery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels.Business
{
    /// <summary>
    /// 添加奖品类型请求
    /// </summary>
    public class AddPrizeTypeRequest : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 奖品数量
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
        /// <summary>
        /// 每轮抽奖个数
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Num { get; set; }

    }
    /// <summary>
    /// 添加响应
    /// </summary>
    public class AddPrizeTypeResponse : BaseResponse
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
