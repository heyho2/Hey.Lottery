using Huach.Framework.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Huach.Framework.Controllers
{
    public class BaseApiController : ApiController
    {
        protected HttpResponseMessage GetFile(HttpContent content, string mediaType)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = content
            };
            return httpResponseMessage;
        }
        protected HttpResponseMessage GetHtmlFile(string fullPath)
        {
            return this.GetFile(new StringContent(File.ReadAllText(fullPath), Encoding.UTF8), "text/html");
        }
        protected HttpResponseMessage GetJsFile(string fullPath)
        {
            return this.GetFile(new StringContent(File.ReadAllText(fullPath), Encoding.UTF8), "text/javascript");
        }
        protected HttpResponseMessage GetCssFile(string fullPath)
        {
            return this.GetFile(new StringContent(File.ReadAllText(fullPath), Encoding.UTF8), "text/css");
        }
        protected HttpResponseMessage GetImageFile(string fullPath)
        {
            return this.GetFile(new StreamContent(new FileStream(fullPath, FileMode.Open)), "image/*");
        }
        protected virtual ActionResult Fail(string msg = null)
        {
            return new ActionResult
            {
                Code = 0,
                Msg = msg
            };
        }
        protected virtual ActionResult<TData> Succeed<TData>(TData data, string msg = null)
        {
            return new ActionResult<TData>
            {
                Data = data,
                Msg = msg
            };
        }
        protected virtual ActionResult<TData> Succeed<TData>(string msg = null)
        {
            return new ActionResult<TData>
            {
                Data = default(TData),
                Msg = msg
            };
        }
        protected virtual ActionResult Succeed(string msg = null)
        {
            return new ActionResult
            {
                Msg = msg
            };
        }
    }
}
