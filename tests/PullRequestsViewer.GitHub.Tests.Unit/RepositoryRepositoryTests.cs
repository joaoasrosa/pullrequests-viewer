using FluentAssertions;
using Moq;
using Octokit;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Tests.Builders.Domain;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class RepositoryRepositoryTests
    {
        private readonly Mock<IRepositoriesClient> _repositoriesClientMock;
        private readonly Mock<ICredentialsRepository> _credentialsRepositoryMock;
        private readonly RepositoryRepository _sut;

        public RepositoryRepositoryTests()
        {
            var gitHubClientMock = new Mock<IGitHubClient>();
            _repositoriesClientMock = new Mock<IRepositoriesClient>();
            gitHubClientMock.SetupGet(x => x.Repository).Returns(_repositoriesClientMock.Object);

            _credentialsRepositoryMock = new Mock<ICredentialsRepository>();
            _credentialsRepositoryMock.SetupGet(x=>x.User).Returns(UserBuilder.GenerateValidUser());

            _sut = new RepositoryRepository(gitHubClientMock.Object, _credentialsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Always_CallsGitHubRepositoryClient()
        {
            var organisation = OrganisationBuilder.GenerateValidOrganisation();

            await _sut.GetAllAsync(organisation);

            _repositoriesClientMock.Verify(x => x.GetAllForOrg(organisation.Name), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_IfGetAllForOrgReturnsNull_ReturnsNull()
        {
            _repositoriesClientMock.Setup(x => x.GetAllForOrg(It.IsAny<string>()))
                .ReturnsAsync(Builders.GitHub.RepositoryBuilder.GenerateNullRepositories());

            var result = await _sut.GetAllAsync(OrganisationBuilder.GenerateValidOrganisation());

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_IfGetAllForOrgReturnsRepositories_ReturnsRepositories()
        {
            var repositories = Builders.GitHub.RepositoryBuilder.GenerateValidRepositories();
            _repositoriesClientMock.Setup(x => x.GetAllForOrg(It.IsAny<string>())).ReturnsAsync(repositories);

            var result = await _sut.GetAllAsync(OrganisationBuilder.GenerateValidOrganisation());

            result.ShouldBeEquivalentTo(repositories.Select(x => new Domain.Repository { Name = x.Name }));
        }

        [Fact]
        public async Task GetAllForCurrentAsync_Always_CallsGitHubRepositoryClient()
        {
            await _sut.GetAllForCurrentAsync();

            _repositoriesClientMock.Verify(x => x.GetAllForUser(_credentialsRepositoryMock.Object.User.Username), Times.Once);
        }

        [Fact]
        public async Task GetAllForCurrentAsync_IfGetAllForUserReturnsNull_ReturnsNull()
        {
            _repositoriesClientMock.Setup(x => x.GetAllForUser(It.IsAny<string>()))
                .ReturnsAsync(Builders.GitHub.RepositoryBuilder.GenerateNullRepositories());

            var result = await _sut.GetAllForCurrentAsync();

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllForCurrentAsync_IfGetAllForUserReturnsRepositories_ReturnsRepositories()
        {
            var repositories = Builders.GitHub.RepositoryBuilder.GenerateValidRepositories();
            _repositoriesClientMock.Setup(x => x.GetAllForUser(It.IsAny<string>())).ReturnsAsync(repositories);

            var result = await _sut.GetAllForCurrentAsync();

            result.ShouldBeEquivalentTo(repositories.Select(x => new Domain.Repository { Name = x.Name }));
        }
    }
}
