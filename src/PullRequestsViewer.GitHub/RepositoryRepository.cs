using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    /// <summary>
    /// The GitHub Repository repository.
    /// </summary>
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly IGitHubClient _gitHubClient;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="gitHubClient">The GitHub client.</param>
        public RepositoryRepository(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Repository>> GetAllAsync(Organisation organisation)
        {
            var repositories = await _gitHubClient.Repository.GetAllForOrg(organisation.Name);

            return repositories.ConvertToDomain();
        }
    }
}