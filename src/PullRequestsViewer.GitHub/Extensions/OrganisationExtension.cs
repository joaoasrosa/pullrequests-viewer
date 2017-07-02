using System.Collections.Generic;
using System.Linq;

using Octokit;

using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Extensions
{
    internal static class OrganisationExtension
    {
        internal static IEnumerable<Organisation> ConvertToDomain(this IEnumerable<Organization> organisations)
        {
            if (organisations == null)
                return null;

            var readOnlyOrganisations = organisations.ToArray();
            var domainOrganisations = new Organisation[readOnlyOrganisations.Length];

            for (var i = 0; i < readOnlyOrganisations.Length; i++)
            {
                domainOrganisations[i] = new Organisation
                {
                    Name = readOnlyOrganisations[i].Name
                };
            }

            return domainOrganisations;
        }
    }
}