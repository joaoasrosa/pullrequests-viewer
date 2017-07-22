using Octokit;
using PullRequestsViewer.Domain;
using System.Collections.Generic;

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
                                             Name = organisations[i].Login
                                         };
            }

            return domainOrganisations;
        }
    }
}