using FluentAssertions;
using PullRequestsViewer.WebApp.Tests.Acceptance.Stubs;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.WebApp.Tests.Acceptance.Controllers
{
    public class HomeControllerTests : IClassFixture<TestFixture<Startup, StartupStub>>
    {
        private readonly HttpClient _client;

        public HomeControllerTests(TestFixture<Startup, StartupStub> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task IndexAsync_IfNotAuthenticated_RedirectToLoginPage()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.Should().Be("/Home/Login");
        }
    }
}
