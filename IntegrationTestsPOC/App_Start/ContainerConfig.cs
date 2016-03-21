using System;
using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using IntegrationTestsPOC.DataAccess;

namespace IntegrationTestsPOC
{
    public static class ContainerConfig
    {
        public static void ConfigureDependencyResolver(HttpConfiguration configuration, IEnumerable<Module> modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterType<PocContext>();

            foreach (Module module in modules)
            {
                builder.RegisterModule(module);
            }

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}