using Huach.Framework.Filters;
using Huach.Framework.Log;
using System.Web.Http.Filters;

namespace Hey.Lottery.Api.Filters
{
    class CustomExceptionFilterAttribute : BaseExceptionFilterAttribute
    {
        private Logger logger = Logger.CreateLogger(typeof(CustomExceptionFilterAttribute));
        protected override void OnExceptionBefore(HttpActionExecutedContext actionExecutedContext)
        {
            string controllerName = (string)actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"];
            string actionName = (string)actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["action"];
            string msgTemplate = $" 在执行 controller【{controllerName}】 的 action【{actionName}】时产生异常,异常信息：{actionExecutedContext.Exception.Message}";
            logger.Error(msgTemplate, actionExecutedContext.Exception);
        }
    }
}