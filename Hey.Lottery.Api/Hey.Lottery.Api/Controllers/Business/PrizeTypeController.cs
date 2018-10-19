using Hey.Lottery.Models;
using Hey.Lottery.Service.Business;
using Hey.Lottery.ViewModels.Business;
using Huach.Framework.Extend;
using Huach.Framework.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hey.Lottery.Api.Controllers
{
    public class PrizeTypeController : BaseApiController
    {
        readonly PrizeTypeService _prizeTypeService = new PrizeTypeService();

        /// <summary>
        /// 奖品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetPrizeTypeListPagingResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetListPagingAsync([Required][FromUri]GetPrizeTypeListPagingRequest request)
        {
            Expression<Func<PrizeType, bool>> where = a => a.Disable == (short)BaseModel.DisableEnum.Normal;
            if (!string.IsNullOrWhiteSpace(request.Type))
            {
                where = where.And(a => a.Type.Contains(request.Type));
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                where = where.And(a => a.PrizeName.Contains(request.Name));
            }
            var result = await _prizeTypeService.GetListPagingAsync<GetPrizeTypeListPagingItem, GetPrizeTypeListPagingResponse>(a => new GetPrizeTypeListPagingItem
            {
                Id = a.Id,
                Count = a.Count,
                PrizeName = a.PrizeName,
                Type = a.Type,
                CreateDate = a.CreateDate,
                Num = a.Num,
            }, where, request);
            return Succeed(result, "加载成功");
        }
        /// <summary>
        /// 获取所有奖品
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetPrizeTypeAllResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _prizeTypeService.GetListAsync(a => new GetPrizeTypeAllItem
            {
                Id = a.Id,
                Count = a.Count,
                PrizeName = a.PrizeName,
                Type = a.Type,
                Num = a.Num,
            }, a => a.Disable == (short)BaseModel.DisableEnum.Normal);
            return Succeed(new GetPrizeTypeAllResponse
            {
                Items = result
            }, "加载成功");
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<AddPrizeTypeResponse>)), HttpPost]
        public async Task<IHttpActionResult> AddAsync([Required]AddPrizeTypeRequest request)
        {
            if (await _prizeTypeService.AnyAsync(a => a.Type == request.Type || a.PrizeName == request.PrizeName))
            {
                return Fail("存在重名");
            }
            var prizeType = new PrizeType
            {
                Type = request.Type,
                PrizeName = request.PrizeName,
                Count = request.Count,
                Num = request.Num,
            };
            var result = await _prizeTypeService.AddAsync(prizeType);
            if (result > 0)
            {
                return Succeed(new AddPrizeTypeResponse
                {
                    Id = prizeType.Id

                }, "添加成功");
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 修改（注意：没有修改的也要将原来的数据传回）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult)), HttpPost]
        public virtual async Task<IHttpActionResult> UpdateAsync([Required]UpdatePrizeTypeRequest request)
        {

            var entity = await _prizeTypeService.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (entity == null)
            {
                return Fail("找不到奖品类型");

            }
            entity.Id = request.Id;
            entity.Type = request.Type;
            entity.PrizeName = request.PrizeName;
            entity.Count = request.Count;
            entity.Num = request.Num;
            var result = await _prizeTypeService.UpdateAsync(entity);
            if (result > 0)
            {
                return Succeed("修改成功");
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<int>)), HttpGet]
        public virtual async Task<IHttpActionResult> DeleteAsync([FromUri][Required]DeletePrizeTypeRequest request)
        {
            var result = await _prizeTypeService.DeleteAsync(a => a.Id == request.Id);
            if (result > 0)
            {
                return Succeed(result, "删除成功");
            }
            else
            {
                return Fail("删除失败");
            }
        }
    }
}
