using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Huach.Framework.Models
{
    public class ActionResult : ActionResult<object>
    {
        public static ActionResult Fail(string msg = null)
        {
            return new ActionResult
            {
                Code = 0,
                Msg = msg
            };
        }
        public static ActionResult Succeed(object data = null, string msg = null)
        {
            return new ActionResult
            {
                Data = data,
                Msg = msg
            };
        }
    }
    public class ActionResult<T> : IHttpActionResult
    {
        public ActionResult()
        {
            this.HttpStatusCode = HttpStatusCode.OK;
            this.Code = 1;
        }
        public virtual string Msg { get; set; }
        public virtual T Data { get; set; }
        public virtual int Code { get; set; }
        public virtual bool IsSucceed { get; set; } = true;
        public virtual HttpStatusCode HttpStatusCode { get; set; }
        public int SubHttpStatusCode { get; internal set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.ToHttpResponseMessage());
        }
        public virtual HttpResponseMessage ToHttpResponseMessage()
        {
            return new HttpResponseMessage
            {
                StatusCode = this.HttpStatusCode,
                Content = new ObjectContent(typeof(ActionResult<T>), this, this.GetMediaTypeFormatter())
            };
        }
        protected virtual MediaTypeFormatter GetMediaTypeFormatter()
        {
            JsonMediaTypeFormatter expr = new JsonMediaTypeFormatter();
            expr.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss:ffff";
            return expr;
        }
    }
}