using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IntegrationTestsPOC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureJsonFormatting(config);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureJsonFormatting(HttpConfiguration configuration)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.Indented
            };

            var jsonFormatter = configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = settings;
        }
    }
}
