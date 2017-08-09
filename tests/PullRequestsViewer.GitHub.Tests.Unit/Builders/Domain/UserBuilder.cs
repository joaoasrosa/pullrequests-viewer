using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Tests.Builders.Domain
{
    internal static class UserBuilder
    {
        internal static User GenerateValidUser()
        {
            return new User("joaoasrosa", "MagicWand!");
        }
    }
}