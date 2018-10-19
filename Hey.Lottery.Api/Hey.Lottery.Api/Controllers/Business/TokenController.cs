using Hey.Lottery.ViewModels;
using Huach.Framework.Controllers;
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
    public class TokenController : BaseJwtAuthApiController
    {
        protected override string Secret => Contants.TokenSecret;

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ResponseType(typeof(ActionResult)), HttpPost]
        public override IHttpActionResult Login(string userName, string password)
        {
            return base.Login(userName, password);
        }
        protected override bool VerifyLogin(string userIdentification, string password, out IDictionary<string, object> jwtPayload)
        {
            jwtPayload = new Dictionary<string, object>();
            if (userIdentification != "Admin" || password != "Admin")
            {
                return false;
            }
            jwtPayload = new Dictionary<string, object>()
            {
                { "Id",1},
                { "Name","Admin"},
            };
            return true;
        }
    }
}
