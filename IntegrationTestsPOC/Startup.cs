using System.Web.Http;
using IntegrationTestsPOC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace IntegrationTestsPOC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);

            app.UseWebApi(configuration);
        }
    }
}
