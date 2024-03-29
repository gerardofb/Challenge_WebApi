using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestChallengeWebAPI
{
    public class TestOne : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestOne(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Theory]
        [InlineData("/api/UserPermissions/GetPermissions/2")]
        
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:65500");
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content?.Headers?.ContentType?.ToString());
            
        }
        [Theory]
        [InlineData("/api/UserElasticPermissions")]
        [InlineData("/api/UserElasticPermissions/2")]
        
        public async Task Get_EndpointContentFirstTest(string url)
        {
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:65500");
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"userId\":2", content);
        }
        [Theory]
        [InlineData("/api/UserPermissions/GetUser/Ricardo")]
        public async Task Get_EndpointContentSecondTest(string url)
        {
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:65500");
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"name\":\"Ricardo", content);
        }
    }
}