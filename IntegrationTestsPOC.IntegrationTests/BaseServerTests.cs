using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace IntegrationTestsPOC.IntegrationTests
{
    public class BaseServerTests
    {
        private TestServer _server;
        private HttpClient _client;

        protected HttpClient Client => _client;

        [SetUp]
        public void Setup()
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

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        protected HttpRequestMessage CreateRequest(string url, string mimeType, HttpMethod method, HttpContent content)
        {
            var request = new HttpRequestMessage(method, url);
            request.Content = content;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mimeType));

            return request;
        }

        protected async Task RequestGetForAsync(string uri, string mimeType, Action<HttpResponseMessage> assert)
        {
            using (HttpRequestMessage request = CreateRequest(uri, mimeType, HttpMethod.Get, null))
            using (HttpResponseMessage response = await Client.SendAsync(request))
            {
                assert(response);
            }
        }

        protected async Task RequestPostForAsync(string uri, string mimeType, object content, Action<HttpResponseMessage> assert)
        {
            using (HttpRequestMessage request = CreateRequest(uri, mimeType, HttpMethod.Post, new ObjectContent(typeof(object), content, new JsonMediaTypeFormatter())))
            using (HttpResponseMessage response = await Client.SendAsync(request))
            {
                assert(response);
            }
        }
    }
}