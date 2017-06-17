using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;
using Octokit.Internal;

using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using PullRequest=PullRequestsViewer.Domain.PullRequest;
using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    public class PullRequestRepository : IPullRequestRepository
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public PullRequestRepository(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public async Task<IEnumerable<PullRequest>> GetAll()
        {
            InMemoryCredentialStore credentials = new InMemoryCredentialStore(
                new Credentials(_credentialsRepository.User.Username,
                    _credentialsRepository.User.Password));
            GitHubClient client = new GitHubClient(
                new ProductHeaderValue("PullRequestsViewer"),
                credentials);


            var repositories = await client.Repository.GetAllForUser(_credentialsRepository.User.Username);

            repositories = await client.Repository.GetAllForCurrent();

            var results = new List<Octokit.PullRequest>();

            foreach(var repository in repositories)
            {
                results.AddRange(await client.PullRequest.GetAllForRepository(repository.Owner.Login, repository.Name));
            }

            repositories = await client.Repository.GetAllForOrg("coolblue-development");

            foreach(var repository in repositories)
            {
                results.AddRange(await client.PullRequest.GetAllForRepository(repository.Owner.Login, repository.Name));
            }

            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<PullRequest>> GetAll(IReadOnlyList<Repository> repositories)
        {
            InMemoryCredentialStore credentials = new InMemoryCredentialStore(
                new Credentials(_credentialsRepository.User.Username,
                    _credentialsRepository.User.Password));
            GitHubClient client = new GitHubClient(
                new ProductHeaderValue("PullRequestsViewer"),
                credentials);

            var pullRequests = new List<Octokit.PullRequest>();

            foreach(var repository in repositories)
            {
                pullRequests.AddRange(
                    await client.PullRequest.GetAllForRepository(repository.OwnerLogin, repository.Name));
            }

            return pullRequests.ConvertTo();
        }
    }
}