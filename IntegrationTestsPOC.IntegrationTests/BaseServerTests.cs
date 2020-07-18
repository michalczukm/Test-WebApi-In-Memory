using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Owin.Testing;
using Owin;

namespace IntegrationTestsPOC.IntegrationTests
{
    public class BaseServerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        protected HttpClient Client => _client;

        public BaseServerTests()
        {
            _server = TestServer.Create(app =>
            {
                var configuration = new HttpConfiguration();
                WebApiConfig.Register(configuration);
                ContainerConfig.ConfigureDependencyResolver(configuration, new List<Module>
                {
                    new IntegrationTestsModule()
                });

                app.UseWebApi(configuration);
            });

            _client = _server.HttpClient;
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        protected HttpRequestMessage CreateRequest(string url, string mimeType, HttpMethod method, HttpContent content)
        {
            var request = new HttpRequestMessage(method, url) {Content = content};
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mimeType));
            return request;
        }

        protected async Task<HttpResponseMessage> GetAsync(string uri, string mimeType)
        {
            using (HttpRequestMessage request = CreateRequest(uri, mimeType, HttpMethod.Get, null))
            {
                return await Client.SendAsync(request);
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(string uri, string mimeType, object content)
        {
            using (HttpRequestMessage request = CreateRequest(uri, mimeType, HttpMethod.Post,
                new ObjectContent(typeof(object), content, new JsonMediaTypeFormatter())))
            {
                return await Client.SendAsync(request);
            }
        }
    }
}