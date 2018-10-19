using Hey.Lottery.Models;
using Hey.Lottery.ViewModels.Business;
using Huach.Framework.Extend;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Hey.Lottery.Repository.Business
{
    public class PrizeRepository : BaseRepository<Prize>
    {
        public async Task<List<GetPrizeListItem>> GetListAsync(int type)
        {
            var result = await DbContext.Set<Prize>().Where(a => a.Disable == (short)BaseModel.DisableEnum.Normal && a.PrizeTypeId == type).Select(a => new
            {
                PrizeType = DbContext.Set<PrizeType>().FirstOrDefault(),
                Employee = DbContext.Set<Employee>().FirstOrDefault(),
            }).Select(a => new GetPrizeListItem
            {
                PrizeName = a.PrizeType.PrizeName,
                Department = a.Employee.Department,
                EmployeeName = a.Employee.Name
            }).ToListAsync();
            return result;
        }
        public async Task<GetPrizePagingResponse> GetListPagingAsync(GetPrizePagingRequest request)
        {
            var result = await DbContext.Set<Prize>().Where(a => a.Disable == (short)BaseModel.DisableEnum.Normal && a.PrizeTypeId == request.Type).Select(a => new
            {
                PrizeType = DbContext.Set<PrizeType>().FirstOrDefault(),
                Employee = DbContext.Set<Employee>().FirstOrDefault(),
            }).PagingAsync(a => new GetPrizePagingItem
            {
                PrizeName = a.PrizeType.PrizeName,
                Department = a.Employee.Department,
                EmployeeName = a.Employee.Name
            }, request.PageIndex, request.PageSize, request.SortField, request.Direction);
            return new GetPrizePagingResponse
            {
                Count = result.Count,
                Index = result.Index,
                Items = result.Items,
                Size = result.Size,
                Total = result.Total,
            };
        }
    }
}
