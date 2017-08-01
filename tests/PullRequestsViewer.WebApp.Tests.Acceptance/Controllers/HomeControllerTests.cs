using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.WebApp.Tests.Acceptance.Controllers
{
    public class HomeControllerTests : IDisposable
    {
        private readonly HttpClient _client;

        public HomeControllerTests()
        {
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new[] { KeyValuePair.Create("ConnectionStrings:PullRequestsViewerDatabase", "Data Source=:memory:") });
            });
            
            webHostBuilder.UseStartup<Startup>();
            var testServer = new TestServer(webHostBuilder);

            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task IndexAsync_IfNotAuthenticated_ReturnsStatusCode302()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
