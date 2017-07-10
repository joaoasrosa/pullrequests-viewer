using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Tests.Builders
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
