using Huach.Framework.Redis;
using System;
using System.Web;

namespace Huach.Framework.Helper
{
    public class RedisSession
    {
        public readonly static RedisSession Session = new RedisSession();
        /// <summary>
        /// cookie名称
        /// </summary>
        public static string CookieName => "HuaqiSessionId";
        /// <summary>
        /// session过期时间
        /// 默认30分钟
        /// </summary>
        private const Double SessionTimeOut = 30;

        public object this[string name]
        {
            get
            {
                using (var service = new RedisHashService())
                {
                    return service.Get<object>(RedisKey, name);
                }
            }
            set
            {
                using (var service = new RedisHashService())
                {
                    service.Set(RedisKey, name, value.ToString());
                    service.ExpireEntryAt(RedisKey, DateTime.Now.AddMinutes(SessionTimeOut));
                }
            }
        }

        public void Remove(string name)
        {
            using (var service = new RedisHashService())
            {
                service.RemoveEntryFromHash(RedisKey, name);
            }
        }

        public void Add<T>(string name, T value)
        {
            this[name] = value;
        }
        public T Get<T>(string name)
        {
            using (var service = new RedisHashService())
            {
                return service.Get<T>(RedisKey, name);
            };
        }
        /// <summary>
        /// session id
        /// </summary>
        private static string SessionId
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                if (cookie != null)
                {
                    return cookie.Value;
                }
                else
                {
                    string sessionID = $"{DateTime.Now.ToString("MMdd")}{Guid.NewGuid().ToString("N")}";
                    cookie = new HttpCookie(CookieName, sessionID)
                    {
                        Expires = DateTime.Now.AddMinutes(SessionTimeOut)
                    };
                    HttpContext.Current.Response.Cookies.Set(cookie);
                    return sessionID;
                }
            }
        }
        /// <summary>
        ///清除session
        /// </summary>
        /// <returns></returns>
        public static bool Destroy()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Set(cookie);
                using (var service = new RedisHashService())
                {
                    service.Remove(RedisKey);
                }
            }
            return false;
        }
        public static string RedisKey => $"Session:{CookieName}:{SessionId}";
    }
}
