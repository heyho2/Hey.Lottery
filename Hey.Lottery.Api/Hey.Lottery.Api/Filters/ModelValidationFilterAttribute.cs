using Huach.Framework.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hey.Lottery.Api.Filters
{
    /// <summary>
    /// 接口参数验证过滤器
    /// author：wolf
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //反射给参数特性验证
            var parameters = ((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo.GetParameters();
            foreach (ParameterInfo prop in parameters)
            {
                if (prop.IsDefined(typeof(ValidationAttribute), true))
                {
                    var attrs = prop.GetCustomAttributes(typeof(ValidationAttribute), true);
                    foreach (var attr in attrs)
                    {
                        var attribute = attr as ValidationAttribute;
                        var msg = attribute.ErrorMessage;
                        try
                        {
                            attribute.Validate(actionContext.ActionArguments[prop.Name], prop.Name);
                        }
                        catch (ValidationException vEx)
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(ActionResult.Fail(vEx.Message));
                            return;
                        }
                    }
                }
            }
            if (!actionContext.ModelState.IsValid)
            {
                var msg = string.Join(",", actionContext.ModelState.Select(a => string.IsNullOrWhiteSpace(a.Value.Errors.FirstOrDefault()?.ErrorMessage) ? a.Value.Errors.FirstOrDefault()?.Exception.Message : a.Value.Errors.FirstOrDefault()?.ErrorMessage).Where(a => !string.IsNullOrWhiteSpace(a)).ToArray());
                actionContext.Response = actionContext.Request.CreateResponse(ActionResult.Fail(msg));
                return;
            }
        }
    }
}