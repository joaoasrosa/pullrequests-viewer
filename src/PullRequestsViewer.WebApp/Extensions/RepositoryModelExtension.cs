using PullRequestsViewer.Domain;
using PullRequestsViewer.WebApp.Models;
using System.Collections.Generic;

namespace PullRequestsViewer.WebApp.Extensions
{
    internal static class RepositoryModelExtension
    {
        internal static IReadOnlyList<RepositoryModel> ConvertToModel(this IReadOnlyList<Repository> repositories)
        {
            if (repositories == null)
                return null;

            var repositoryModels = new RepositoryModel[repositories.Count];

            for (var i = 0; i < repositories.Count; i++)
            {
                repositoryModels[i] = new RepositoryModel
                {
                    Name = repositories[i].Name,
                    OwnerLogin = repositories[i].OwnerLogin
                };
            }

            return repositoryModels;
        }
    }
}
