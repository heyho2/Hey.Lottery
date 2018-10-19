using Hey.Lottery.Models;
using Hey.Lottery.Service.Business;
using Hey.Lottery.ViewModels.Business;
using Huach.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hey.Lottery.Api.Controllers
{
    /// <summary>
    /// 抽奖
    /// </summary>
    public class PrizeController : BaseApiController
    {
        readonly PrizeService _prizeService = new PrizeService();
        readonly EmployeeService _employeeService = new EmployeeService();
        readonly PrizeTypeService _prizeTypeService = new PrizeTypeService();
        /// <summary>
        /// 获取抽奖名单
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetPrizeListResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetListAsync([Required][FromUri]GetPrizeListRequest request)
        {
            var prizeList = await _prizeService.GetListAsync(request.Type);

            return Succeed(new GetPrizeListResponse
            {
                Items = prizeList
            }, "加载成功");
        }

        /// <summary>
        /// 获取抽奖名单
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetPrizePagingResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetListPagingAsync([Required]GetPrizePagingRequest request)
        {
            var result = await _prizeService.GetListPagingAsync(request);
            return Succeed(result, "加载成功");
        }
        /// <summary>
        /// 获取未中奖得员工
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetPrizePagingResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetNotWonEmployeeListAsync()
        {
            var prizes = await _prizeService.GetListAsync(a => a.EmployeeId, a => a.Disable == (short)BaseModel.DisableEnum.Normal);
            var result = await _employeeService.GetListAsync(a => new GetNotWonEmployeeListItem
            {
                Name = a.Name,
                Department = a.Department,
            }, a => !prizes.Contains(a.Id));

            return Succeed(new GetNotWonEmployeeListResponse
            {
                Items = result
            }, "加载成功");
        }
        /// <summary>
        /// 随机员工
        /// </summary>
        private int Suiji(Random rand, List<int> pEmployeeIds, List<int> notWonEmployeeIds)
        {
            int index = rand.Next(notWonEmployeeIds.Count());//随机数员工索引
            var employeeId = notWonEmployeeIds[index];
            if (pEmployeeIds.Any(a => a == employeeId))
            {
                return Suiji(rand, pEmployeeIds, notWonEmployeeIds);
            }
            else
            {
                return employeeId;
            }
        }
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<LotteryResponse>)), HttpPost]
        public async Task<IHttpActionResult> LotteryAsync([Required]LotteryRequest request)
        {
            //中奖名单
            var wonEmployee = await _prizeService.GetListAsync(a => a.EmployeeId, a => a.Disable == (short)BaseModel.DisableEnum.Normal);

            var notWonEmployee = await _employeeService.GetListAsync(a => a.Id, a => !wonEmployee.Contains(a.Id));

            var prizeType = await _prizeTypeService.FirstOrDefaultAsync(a => a.Id == request.Type);
            //当前类型奖品的中奖人数
            var typeCount = await _prizeService.CountAsync(a => a.PrizeTypeId == request.Type);

            if (typeCount >= prizeType.Count)
            {
                return Fail("奖品已经抽完");
            }
            int xunhuanr = 0;
            if ((prizeType.Count - typeCount) <= prizeType.Num)
            {
                xunhuanr = (prizeType.Count - typeCount);
            }
            else
            {
                xunhuanr = prizeType.Num;
            }
            Random rand = new Random();
            List<int> plist = new List<int>();
            for (int i = 0; i < xunhuanr; i++)
            {

                var employeeId = Suiji(rand, plist, notWonEmployee);
                plist.Add(employeeId);

            }
            var result = await _prizeService.AddAsync(plist.Select(a => new Prize
            {
                EmployeeId = a,
                PrizeTypeId = prizeType.Id
            }).ToList());
            return Succeed(new LotteryResponse
            {
                EmployeeIds = plist,
                Rounds = (prizeType.Count % prizeType.Num) == 0 ? (prizeType.Count / prizeType.Num) : (prizeType.Count / prizeType.Num + 1),
                RoundsIndex = (typeCount % prizeType.Num) == 0 ? (typeCount / prizeType.Num) : (typeCount / prizeType.Num + 1)
            }, "抽取成功");
        }
        /// <summary>
        /// 清除抽奖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult)), HttpPost]
        public async Task<IHttpActionResult> ClearLotteryAsync([Required]ClearLotteryRequest request)
        {

            var result = await _prizeService.DeleteAsync(a => a.PrizeTypeId == request.PrizeTypeId);
            if (result > 0)
            {
                return Succeed("清除成功");
            }
            else
            {
                return Fail("清除失败");
            }
        }
    }
}
