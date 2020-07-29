using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace IntegrationTestsPOC.IntegrationTests.Controllers
{
    public class PeopleControllerTests : BaseServerTests
    {
        [Fact]
        public async Task ShouldGetAll()
        {
            // arrange
            var expectedJson = JsonConvert.SerializeObject(new List<dynamic>
            {
                new {id = 1, firstName = "Andrew", lastName = "Peters"},
                new {id = 2, firstName = "Brice", lastName = "Lambson"},
                new {id = 3, firstName = "Rowan", lastName = "Miller"}
            });

            var mimeType = "application/json";

            // act
            using (var actualResponse = await GetAsync("api/people", mimeType))
            {
                // assert
                actualResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var actualResponseContent = await actualResponse.Content.ReadAsStringAsync();
                var actual = JToken.Parse(actualResponseContent);
                var expected = JToken.Parse(expectedJson);
                actual.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task ShouldAddNewPersonOnPost()
        {
            // arrange
            var mimeType = "application/json";
            var postBody = new { firstName = "Mike", lastName = "Morelez" };

            var expectedJson = JsonConvert.SerializeObject
            (
                new { id = 4, firstName = "Mike", lastName = "Morelez" }
            );

            // act
            using (var actualResponse = await PostAsync("api/people", mimeType, postBody))
            {
                // assert
                actualResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var actualResponseContent = await actualResponse.Content.ReadAsStringAsync();
                var actual = JToken.Parse(actualResponseContent);
                var expected = JToken.Parse(expectedJson);
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}