using Hey.Lottery.Api.Config;
using Hey.Lottery.Api.Filters;
using Owin;
using System.Web.Http;

namespace Hey.Lottery.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // 创建 Web API 的配置
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            SwaggerConfig.Register(config);

            config.Filters.Add(new JwtAuthFilterAttribute());

            config.Filters.Add(new ModelValidationFilterAttribute());

            config.Filters.Add(new CustomExceptionFilterAttribute());
          
            // 将路由配置附加到 appBuilder
            appBuilder.UseWebApi(config);
        }
    }
}
