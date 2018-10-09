using Huach.Framework.Jwt;
using Huach.Framework.Models;
using JWT;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Huach.Framework.Controllers
{
    public abstract class BaseJwtAuthApiController : BaseApiController
    {
        protected abstract string Secret { get; }
        /// <summary>
        /// 过期分钟
        /// </summary>
        protected virtual int ExpiredMinutes { get; }

        protected long GetUtcSeconds(DateTime time)
        {
            return (new DateTimeOffset(time).UtcTicks - 621355968000000000L) / 10000000;
        }

        protected abstract bool VerifyLogin(string userIdentification, string password, out IDictionary<string, object> jwtPayload);


        protected virtual IHttpActionResult Login(string userName, string password)
        {
            if (VerifyLogin(userName, password, out IDictionary<string, object> jwtPayload))
            {
                if (ExpiredMinutes > 0)
                {
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    var now = provider.GetNow();
                    var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // or use JwtValidator.UnixEpoch
                    var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

                    jwtPayload[JwtClaimName.exp.ToString()] = secondsSinceEpoch + ExpiredMinutes * 60;
                }

                string data = JwtHelper.Encode(jwtPayload, Secret);
                return Succeed(data, "获取访问令牌成功");
            }
            return Fail("用户标识或密码错误");
        }
        protected virtual IHttpActionResult Logout(IDictionary<string, object> jwtPayload)
        {
            if (ExpiredMinutes > 0)
            {
                IDateTimeProvider provider = new UtcDateTimeProvider();
                var now = provider.GetNow();
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // or use JwtValidator.UnixEpoch
                var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

                jwtPayload[JwtClaimName.exp.ToString()] = secondsSinceEpoch + ExpiredMinutes * 60;
            }
            string data = JwtHelper.Encode(jwtPayload, Secret);
            return Succeed(data, "已经退出登陆");
        }
    }
}
