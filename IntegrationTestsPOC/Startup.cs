using System;
using System.Linq;
using System.Web.Http;
using Autofac;
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
            ContainerConfig.ConfigureDependencyResolver(configuration, Enumerable.Empty<Module>());

            app.UseWebApi(configuration);
        }
    }
}
