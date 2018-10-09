using Hey.Lottery.Api.Config;
using Owin;
using Swashbuckle.Application;
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

            // 设置默认的启动页为Swagger主页
            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );

            // 将路由配置附加到 appBuilder
            appBuilder.UseWebApi(config);
        }
    }
}
