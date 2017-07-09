using System.Threading.Tasks;

using Moq;

using Octokit;

using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class OrganisationRepositoryTests
    {
        private readonly Mock<IGitHubClient> _gitHubClientMock;
        private readonly OrganisationRepository _sut;

        public OrganisationRepositoryTests()
        {
            _gitHubClientMock = new Mock<IGitHubClient>();
            var organisations = new Mock<IOrganizationsClient>();
            _gitHubClientMock.SetupGet(x => x.Organization).Returns(organisations.Object);


            _sut = new OrganisationRepository(_gitHubClientMock.Object);
        }

        [Fact]
        public async Task GetOrganisationsAsync_Always_CallsGitHubClient()
        {
            await _sut.GetOrganisationsAsync();

            _gitHubClientMock.Verify(x => x.Organization.GetAllForCurrent(), Times.Once);
        }
    }
}