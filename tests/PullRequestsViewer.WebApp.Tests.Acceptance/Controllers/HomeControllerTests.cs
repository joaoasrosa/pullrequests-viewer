using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.WebApp.Tests.Acceptance.Controllers
{
    public class HomeControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public HomeControllerTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task IndexAsync_IfNotAuthenticated_RedirectToLoginPage()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.LocalPath.Should().Be("/Home/Login");
        }
    }
}
