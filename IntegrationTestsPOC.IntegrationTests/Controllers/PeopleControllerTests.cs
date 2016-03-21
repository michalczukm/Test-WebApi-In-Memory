using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTestsPOC.IntegrationTests.Controllers
{
    [TestFixture]
    public class PeopleControllerTests : BaseServerTests
    {
        [Test]
        public void GetAll()
        {
            // arrange
            var expectedJson = JsonConvert.SerializeObject(new List<dynamic>
            {
                new {id = 1, firstName = "Andrew", lastName = "Peters"},
                new {id = 2, firstName = "Brice", lastName = "Lambson"},
                new {id = 3, firstName = "Rowan", lastName = "Miller"}
            });

            var mimeType = "application/json";

            // act & assert
            Action<HttpResponseMessage> assert = response =>
            {
                response.ShouldContainContent(mimeType, expectedJson);
            };

            RequestFor("api/people", mimeType, HttpMethod.Get, assert);
        }
    }
}