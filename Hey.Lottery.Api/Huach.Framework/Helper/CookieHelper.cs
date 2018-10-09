using System;
using System.Web;

namespace Huach.Framework.Helper
{
    /// <summary>
    /// 不支持 cookie values
    /// </summary>
    public class CookieHelper
    {

        #region 获取Cookie

        /// <summary> 
        /// 获得Cookie 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <returns></returns> 
        public static HttpCookie GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }

        #endregion

        #region 删除Cookie


        /// <summary> 
        /// 删除Cookie
        /// </summary> 
        /// <param name="cookieName"></param> 
        public static void RemoveCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            foreach (string item in request.Cookies.AllKeys)
            {
                if (item == cookieName)
                {
                    HttpCookie cookies = request.Cookies[item];
                    if (cookies != null)
                    {
                        cookies.Expires = DateTime.Now.AddDays(-1);
                        HttpContext.Current.Response.Cookies.Add(cookies);
                    }
                }
            }
        }

        #endregion

        #region 设置/修改Cookie
        /// <summary> 
        /// 设置Cookie 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        /// <param name="expires"></param> 
        public static void SetCookie(string cookieName, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = value;
            cookie.Expires = expires;
            //指定客户端脚本是否可以访问[默认为false] 
            cookie.HttpOnly = true;
            //指定统一的Path，比便能通存通取 
            cookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion
    }
}
