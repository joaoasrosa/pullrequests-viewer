using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using PullRequest = PullRequestsViewer.Domain.PullRequest;
using Repository = PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    public class PullRequestRepository : IPullRequestRepository
    {
        private readonly IGitHubClient _gitHubClient;

        public PullRequestRepository(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<IReadOnlyList<PullRequest>> GetAll(IReadOnlyList<Repository> repositories)
        {
            var pullRequests = new List<Octokit.PullRequest>();

            foreach (var repository in repositories)
            {
                pullRequests.AddRange(
                    await _gitHubClient.PullRequest.GetAllForRepository(repository.OwnerLogin, repository.Name));
            }

            return pullRequests.ConvertToDomain();
        }
    }
}