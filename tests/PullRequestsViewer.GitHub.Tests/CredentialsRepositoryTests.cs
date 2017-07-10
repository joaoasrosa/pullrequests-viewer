using PullRequestsViewer.Domain;
using PullRequestsViewer.GitHub.Tests.Builders;
using PullRequestsViewer.GitHub;

using FluentAssertions;

using Xunit;

namespace PullRequestsViewer.GitHub.Tests
{
    public class CredentialsRepositoryTests
    {
        private readonly CredentialsRepository _sut;

        public CredentialsRepositoryTests()
        {
            _sut = new CredentialsRepository();
        }

        [Fact]
        public void SetUser_Always_SetsCorrectUser()
        {
            var user = UserBuilder.GenerateValidUser();

            _sut.SetUser(user);

            _sut.User.Should().Be(user);
        }

        [Fact]
        public void IsUserSetted_IfUserIsNull_ReturnsFalse()
        {
            var result = _sut.IsUserSetted();

            result.Should().BeFalse();
        }

        [Fact]
        public void IsUserSetted_IfUserIsnotNull_ReturnsTrue()
        {
            _sut.SetUser(UserBuilder.GenerateValidUser());

            var result = _sut.IsUserSetted();

            result.Should().BeTrue();
        }
    }
}