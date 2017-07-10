using Moq;
using Octokit;
using PullRequestsViewer.GitHub.Tests.Builders;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class RepositoryRepositoryTests
    {
        private readonly Mock<IRepositoriesClient> _repositoriesClientMock;
        private readonly RepositoryRepository _sut;

        public RepositoryRepositoryTests()
        {
            var gitHubClientMock = new Mock<IGitHubClient>();
            _repositoriesClientMock = new Mock<IRepositoriesClient>();
            gitHubClientMock.SetupGet(x => x.Repository).Returns(_repositoriesClientMock.Object);

            _sut = new RepositoryRepository(gitHubClientMock.Object);
        }

        [Fact]
        public async Task GetOrganisationsAsync_Always_CallsGitHubRepositoryClient()
        {
            var organisation = OrganisationBuilder.GenerateValidOrganisation();

            await _sut.GetAll(organisation);

            _repositoriesClientMock.Verify(x => x.GetAllForOrg(organisation.Name), Times.Once);
        }
    }
}
