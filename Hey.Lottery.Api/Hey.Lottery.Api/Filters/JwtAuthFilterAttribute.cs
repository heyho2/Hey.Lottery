using Hey.Lottery.ViewModels;
using Huach.Framework.Jwt;
using Huach.Framework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Web.Http.Controllers;

namespace Hey.Lottery.Api.Filters
{
    public class JwtAuthFilterAttribute : BaseAuthFilterAttribute
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
            return actionContext.Request.Headers?.GetValues(JwtTokenKeyName)?.FirstOrDefault();
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
            var jwtInfo = JwtHelper.Decode<IDictionary<string, object>>(token, _secret);
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
        protected bool IsAuthenticated(IDictionary<string, object> userInfo)
        {
            CurrentUser.Info = new UserInfo
            {
                Id = Convert.ToInt32(userInfo["Id"]),
                Name = userInfo["Name"].ToString()
            };
            if (userInfo == null)
            {
                return false;
            }
            return true;
        }
    }
}
