using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels.Base
{
    /// <summary>
    /// 响应基类
    /// </summary>
    public abstract class BaseResponse
    {
    }
    /// <summary>
    /// 纯列表
    /// </summary>
    public class BaseListResponse<T> where T : BaseItemResponse
    {
        /// <summary>
        /// 记录集合
        /// </summary>
        public List<T> Items { get; set; }
    }
    /// <summary>
    /// 分页响应
    /// </summary>
    public class BasePagingResponse<T> where T : BaseItemResponse
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 记录集合
        /// </summary>
        public List<T> Items { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int Size { get; set; }
    }
    /// <summary>
    /// 响应列表项
    /// </summary>
    public abstract class BaseItemResponse
    {

    }
}
