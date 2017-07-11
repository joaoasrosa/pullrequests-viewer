using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Tests.Builders.Domain
{
    internal static class OrganisationBuilder
    {
        internal static Organisation GenerateValidOrganisation()
        {
            return new Organisation
            {
                Name = "Foo"
            };
        }
    }
}
