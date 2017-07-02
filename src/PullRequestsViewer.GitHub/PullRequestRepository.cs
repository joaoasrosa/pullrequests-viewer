using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using PullRequest=PullRequestsViewer.Domain.PullRequest;
using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    public class PullRequestRepository : IPullRequestRepository
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly ICredentialsRepository _credentialsRepository;

        public PullRequestRepository(IGitHubClient gitHubClient, ICredentialsRepository credentialsRepository)
        {
            _gitHubClient = gitHubClient;
            _credentialsRepository = credentialsRepository;
        }

        public async Task<IEnumerable<PullRequest>> GetAll()
        {
            var repositories = await _gitHubClient.Repository.GetAllForUser(_credentialsRepository.User.Username);

            repositories = await _gitHubClient.Repository.GetAllForCurrent();

            var results = new List<Octokit.PullRequest>();

            foreach(var repository in repositories)
            {
                results.AddRange(await _gitHubClient.PullRequest.GetAllForRepository(repository.Owner.Login, repository.Name));
            }

            repositories = await _gitHubClient.Repository.GetAllForOrg("coolblue-development");

            foreach(var repository in repositories)
            {
                results.AddRange(await _gitHubClient.PullRequest.GetAllForRepository(repository.Owner.Login, repository.Name));
            }

            throw new NotImplementedException();
        }

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