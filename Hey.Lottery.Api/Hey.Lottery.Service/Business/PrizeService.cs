using Hey.Lottery.Models;
using Hey.Lottery.Repository.Business;
using Hey.Lottery.ViewModels.Base;
using Hey.Lottery.ViewModels.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Service.Business
{
    public class PrizeService : BaseService<Prize>
    {
        readonly PrizeRepository _prizeRepository;
        public PrizeService()
        {
            _prizeRepository = new PrizeRepository();
            _repository = _prizeRepository;
        }
        public async Task<List<GetPrizeListItem>> GetListAsync(int type)
        {
            return await _prizeRepository.GetListAsync(type);
        }
        public async Task<GetPrizePagingResponse> GetListPagingAsync(GetPrizePagingRequest request)
        {
            var resrlt = await _prizeRepository.GetListPagingAsync(request);
            return resrlt;
        }
    }
}
