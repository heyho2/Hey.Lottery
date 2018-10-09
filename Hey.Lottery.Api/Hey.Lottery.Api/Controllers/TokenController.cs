using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Api.Controllers
{
    public class TokenController : BaseApiController
    {
        // GET api
        public string Get(string id)
        {
            return id;
        }
        // PUT api
        public void Put(int id, string value)
        {
        }
        // DELETE api
        public void Delete(int id)
        {
        }
    }
}
