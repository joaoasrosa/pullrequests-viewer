using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly IGitHubClient _gitHubClient;

        public RepositoryRepository(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<IReadOnlyList<Repository>> GetAll(Organisation organisation)
        {
            var repositories = await _gitHubClient.Repository.GetAllForOrg(organisation.Name);

            return repositories.ConvertToDomain();
        }
    }
}