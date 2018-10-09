using Hey.Lottery.Api.Filters;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hey.Lottery.Api.SwaggerExtensions
{
    public class SwaggerHttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuth = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is JwtAuthFilterAttribute);
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAuth && !allowAnonymous)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "token",
                    @in = "header",
                    description = "访问接口的令牌",
                    required = true,
                    type = "string"
                });
            }
        }
    }
}