using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using PullRequest=PullRequestsViewer.Domain.PullRequest;
using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    /// <summary>
    /// The GitHub Pull Request repository.
    /// </summary>
    public class PullRequestRepository : IPullRequestRepository
    {
        private readonly IGitHubClient _gitHubClient;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="gitHubClient">The GitHub client.</param>
        public PullRequestRepository(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        /// <summary>
        /// Returns all open Pull Requests for the given Repositories.
        /// </summary>
        /// <param name="repositories">The Repositories.</param>
        /// <returns>The open Pull Requests.</returns>
        public async Task<IReadOnlyList<PullRequest>> GetAll(IReadOnlyList<Repository> repositories)
        {
            var pullRequests = new List<Octokit.PullRequest>();

            foreach(var repository in repositories)
            {
                pullRequests.AddRange(
                    await _gitHubClient.PullRequest.GetAllForRepository(repository.OwnerLogin, repository.Name));
            }

            return pullRequests.ConvertToDomain();
        }
    }
}