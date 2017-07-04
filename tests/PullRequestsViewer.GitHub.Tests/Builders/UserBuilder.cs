using PullRequestsViewer.Domain;
namespace PullRequestsViewer.GitHub.Tests.Builders
{
    internal static class UserBuilder
    {
        internal static User GenerateValidUser()
        {
            return new User()
            {
                Username = "joaoasrosa",
                Password = "MagicWand!"
            };
        }
    }
}
