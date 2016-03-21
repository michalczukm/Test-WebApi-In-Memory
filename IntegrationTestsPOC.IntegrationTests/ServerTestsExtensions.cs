using System.Net.Http;
using FluentAssertions;
using Newtonsoft.Json;

namespace IntegrationTestsPOC.IntegrationTests
{
    public static class ServerTestsExtensions
    {
        public static void ShouldContainContent(this HttpResponseMessage response, string mimeType, string expectedJson)
        {
            response.Content.Should().NotBeNull();
            response.Content.Headers.ContentType.MediaType.Should().Be(mimeType);

            string serializeObject = JsonConvert.SerializeObject(response.Content.ReadAsAsync<dynamic>().Result);
            serializeObject.Should().Be(expectedJson);
        }
    }
}