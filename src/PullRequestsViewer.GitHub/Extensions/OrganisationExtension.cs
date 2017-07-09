using System.Collections.Generic;

using Octokit;

using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Extensions
{
    internal static class OrganisationExtension
    {
        internal static IReadOnlyList<Organisation> ConvertToDomain(this IReadOnlyList<Organization> organisations)
        {
            if(organisations == null)
                return null;

            var domainOrganisations = new Organisation[organisations.Count];

            for(var i = 0;i < organisations.Count;i++)
            {
                domainOrganisations[i] = new Organisation
                                         {
                                             Name = organisations[i].Name
                                         };
            }

            return domainOrganisations;
        }
    }
}