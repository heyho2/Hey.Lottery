using Huach.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hey.Lottery.Api.Controllers
{
    public class PrizeController : BaseApiController
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult)), HttpPost]
        public IHttpActionResult Get()
        {
            return Succeed();
        }
    }
}
