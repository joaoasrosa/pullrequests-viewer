using FluentAssertions;
using Moq;
using Octokit;
using PullRequestsViewer.GitHub.Tests.Builders.GitHub;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class PullRequestRepositoryTests
    {
        private readonly Mock<IPullRequestsClient> _pullRequestsClientMock;
        private readonly PullRequestRepository _sut;

        public PullRequestRepositoryTests()
        {
            var gitHubClientMock = new Mock<IGitHubClient>();
            _pullRequestsClientMock = new Mock<IPullRequestsClient>();
            gitHubClientMock.SetupGet(x => x.PullRequest).Returns(_pullRequestsClientMock.Object);

            _sut = new PullRequestRepository(gitHubClientMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Always_CallsGitHubPullRequestClient()
        {
            _pullRequestsClientMock.Setup(x => x.GetAllForRepository(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(PullRequestBuilder.GenerateValidPullRequests());

            var repositories = Common.Tests.Builders.Domain.RepositoryBuilder.GenerateValidRepositories();

            await _sut.GetAllAsync(repositories);

            foreach (var repository in repositories)
            {
                _pullRequestsClientMock.Verify(x => x.GetAllForRepository(repository.OwnerLogin, repository.Name), Times.Once);
            }
        }

        [Fact]
        public async Task GetAllAsync_IfGetAllForOrgReturnsRepositories_ReturnsRepositories()
        {
            var pullRequests = PullRequestBuilder.GenerateValidPullRequests();
            _pullRequestsClientMock.Setup(x => x.GetAllForRepository(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(pullRequests);

            var result = await _sut.GetAllAsync(Common.Tests.Builders.Domain.RepositoryBuilder.GenerateValidRepositories());

            result.ShouldBeEquivalentTo(pullRequests.Select(x => new Domain.PullRequest
            {
                Title = x.Title,
                AuthorName = x.User.Name,
                Description = x.Body,
                Number = x.Number,
                HtmlUrl = x.Url
            }));
        }
    }
}
