using System.Collections.Generic;

using PullRequestsViewer.Domain;

namespace PullRequestsViewer.GitHub.Extensions
{
    internal static class RepositoryExtension
    {
        internal static IReadOnlyList<Repository> ConvertToDomain(this IReadOnlyList<Octokit.Repository> repositories)
        {
            if (repositories == null)
                return null;

            var domainRepositories = new Repository[repositories.Count];

            for (var i = 0; i < repositories.Count; i++)
            {
                domainRepositories[i] = new Repository
                {
                    Name = repositories[i].Name,
                    OwnerLogin = repositories[i].Owner.Login
                };
            }

            return domainRepositories;
        }
    }
}