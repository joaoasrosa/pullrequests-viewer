using FluentAssertions;
using Moq;
using Octokit;
using PullRequestsViewer.Domain;
using PullRequestsViewer.GitHub.Tests.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class OrganisationRepositoryTests
    {
        private readonly Mock<IOrganizationsClient> _organizationsClientMock;
        private readonly OrganisationRepository _sut;

        public OrganisationRepositoryTests()
        {
            var gitHubClientMock = new Mock<IGitHubClient>();
            _organizationsClientMock = new Mock<IOrganizationsClient>();
            gitHubClientMock.SetupGet(x => x.Organization).Returns(_organizationsClientMock.Object);

            _sut = new OrganisationRepository(gitHubClientMock.Object);
        }

        [Fact]
        public async Task GetOrganisationsAsync_Always_CallsGitHubOrganizationsClient()
        {
            await _sut.GetOrganisationsAsync();

            _organizationsClientMock.Verify(x => x.GetAllForCurrent(), Times.Once);
        }

        [Fact]
        public async Task GetOrganisationsAsync_IfGetAllForCurrentReturnsNull_ReturnsNull()
        {
            _organizationsClientMock.Setup(x => x.GetAllForCurrent()).ReturnsAsync(OrganizationBuilder.GenerateNullOrganizations());

            var result = await _sut.GetOrganisationsAsync();

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetOrganisationsAsync_IfGetAllForCurrentReturnsOrganizations_ReturnsOrganisations()
        {
            var organizations = OrganizationBuilder.GenerateValidOrganizations();
            _organizationsClientMock.Setup(x => x.GetAllForCurrent()).ReturnsAsync(organizations);

            var result = await _sut.GetOrganisationsAsync();

            result.ShouldBeEquivalentTo(organizations.Select(x => new Organisation { Name = x.Name }));
        }
    }
}