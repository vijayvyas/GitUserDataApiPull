using Microsoft.Extensions.Caching.Memory;
using Moq;
using SampleAppTest.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace SampleAppTest.UnitTest

{
    public class GitIntegrationTest
    {

        [Fact]
        public async void Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitApi", "1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.GetAsync("https://api.github.com/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}