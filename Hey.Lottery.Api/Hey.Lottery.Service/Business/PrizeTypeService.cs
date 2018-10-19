using Hey.Lottery.Models;
using Hey.Lottery.Repository.Business;
using Hey.Lottery.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Service.Business
{
    public class PrizeTypeService : BaseService<PrizeType>
    {
        public PrizeTypeService()
        {
            _repository = new PrizeTypeRepository();
        }
    }
}
