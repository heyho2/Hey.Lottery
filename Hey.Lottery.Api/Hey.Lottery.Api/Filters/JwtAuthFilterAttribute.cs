using Hey.Lottery.ViewModels;
using Huach.Framework.Jwt;
using Huach.Framework.Models;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Hey.Lottery.Api.Filters
{
    public class JwtAuthFilterAttribute: BaseAuthFilterAttribute
    {
        private static readonly string _secret;

        /// <summary>
        /// 设置tokenName
        /// </summary>
        protected string JwtTokenKeyName => "token";
        static JwtAuthFilterAttribute()
        {
            _secret = Contants.TokenSecret;
        }
        protected virtual string GetJwtToken(HttpActionContext actionContext)
        {
            return actionContext.Request.Headers.GetValues(JwtTokenKeyName).FirstOrDefault();
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAllowAnonymous(actionContext))
            {
                return;
            }
            var token = GetJwtToken(actionContext);
            if (string.IsNullOrWhiteSpace(token))
            {
                HandleUnauthenticatedRequest(actionContext, "Token为空。");
            }
            var jwtInfo = JwtHelper.Decode<UserInfo>(token, _secret);
            if (jwtInfo.IsSucceed)
            {
                if (IsAuthenticated(jwtInfo.Payload))
                {
                    base.OnAuthorization(actionContext);
                }
            }
            else
            {
                HandleUnauthenticatedRequest(actionContext, jwtInfo.Msg);
            }
        }
        protected bool IsAuthenticated(UserInfo userInfo)
        {
            if (HttpContext.Current.GetOwinContext().Get<UserInfo>(nameof(UserInfo)) == null)
            {
                HttpContext.Current.GetOwinContext().Set(nameof(UserInfo), userInfo);
            }
            //if (user == null)
            //    return false;
            //TODO 从缓存中获取用户信息
            return true;
        }
    }
}
