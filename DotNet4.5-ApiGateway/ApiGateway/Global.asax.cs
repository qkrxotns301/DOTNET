using ApiGateway.Infrastructure;
using System;
using System.Web.Http;

namespace ApiGateway
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Logger.Log.Error(exception, "Api Gateway has thrown an unhandled exception");
        }
    }
}
