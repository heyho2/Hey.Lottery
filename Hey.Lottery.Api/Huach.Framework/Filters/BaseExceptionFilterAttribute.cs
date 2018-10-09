using Huach.Framework.Models;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Web;
using System.Web.Http.Filters;

namespace Huach.Framework.Filters
{
    public abstract class BaseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        protected static ConcurrentDictionary<Type, Func<Exception, ActionResult>> CustomExceptionHandler
        {
            get;
            private set;
        }

        static BaseExceptionFilterAttribute()
        {
            CustomExceptionHandler = new ConcurrentDictionary<Type, Func<Exception, ActionResult>>();
            CustomExceptionHandler[typeof(HttpException)] = delegate (Exception exception)
            {
                HttpException ex = exception as HttpException;
                ActionResult actionResult = ActionResult.Fail(exception.Message);
                actionResult.HttpStatusCode = (HttpStatusCode)ex.GetHttpCode();
                return actionResult;
            };
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                OnExceptionBefore(actionExecutedContext);
                if (CustomExceptionHandler.ContainsKey(exception.GetType()))
                {
                    Func<Exception, ActionResult> func = CustomExceptionHandler[exception.GetType()];
                    if (func != null)
                    {
                        actionExecutedContext.Response = func(exception).ToHttpResponseMessage();
                    }
                }
                else
                {
                    ActionResult actionResult = ActionResult.Fail("内部服务器错误。");
                    actionResult.HttpStatusCode = HttpStatusCode.InternalServerError;
                    actionExecutedContext.Response = actionResult.ToHttpResponseMessage();
                }
            }
        }

        protected abstract void OnExceptionBefore(HttpActionExecutedContext actionExecutedContext);

    }
}
