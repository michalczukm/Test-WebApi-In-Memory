using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Transactions;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace IntegrationTestsPOC.IntegrationTests
{
    public class BaseServerTests
    {
        private TestServer _server;
        private HttpClient _client;
        private TransactionScope _transactionScope;

        protected HttpClient Client => _client;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            _server = TestServer.Create<Startup>();

            _client = _server.HttpClient;
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
            _transactionScope.Dispose();
        }

        protected HttpRequestMessage CreateRequest(string url, string mimeType, HttpMethod method)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mimeType));

            return request;
        }

        protected void RequestFor(string uri, string mimeType, HttpMethod httpMethod, Action<HttpResponseMessage> assert)
        {
            using (var request = CreateRequest(uri, mimeType, httpMethod))
            using (HttpResponseMessage response = Client.SendAsync(request).Result)
            {
                assert(response);
            }
        }
    }
}