using System.Web.Http;

namespace ApiGateway
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*url}",
                defaults: null,
                constraints: null,
                handler: new GatewayHttpControllerDispatcher()
            );
        }
    }
}
