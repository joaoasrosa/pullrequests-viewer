using PullRequestsViewer.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PullRequestsViewer.GitHub.Extensions
{
    internal static class RepositoryExtension
    {
        internal static IReadOnlyList<Repository> ConvertToDomain(this IOrderedEnumerable<Octokit.Repository> repositories)
        {
            if (repositories == null)
                return null;

            var domainRepositories = new Repository[repositories.Count()];

            for (var i = 0; i < repositories.Count(); i++)
            {
                domainRepositories[i] = new Repository
                {
                    Name = repositories.ElementAt(i).Name,
                    OwnerLogin = repositories.ElementAt(i).Owner.Login
                };
            }

            return domainRepositories;
        }
    }
}