using Octokit;
using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository = PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    /// <summary>
    /// The GitHub Repository repository.
    /// </summary>
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly ICredentialsRepository _credentialsRepository;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="gitHubClient">The GitHub client.</param>
        public RepositoryRepository(IGitHubClient gitHubClient, ICredentialsRepository credentialsRepository)
        {
            _gitHubClient = gitHubClient;
            _credentialsRepository = credentialsRepository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Repository>> GetAllAsync(Organisation organisation)
        {
            var repositories = await _gitHubClient.Repository.GetAllForOrg(organisation.Name);

            if (repositories == null)
                return null;

            return repositories.OrderBy(x => x.Name).ConvertToDomain();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Repository>> GetAllForCurrentAsync()
        {
            var repositories = await _gitHubClient.Repository.GetAllForUser(_credentialsRepository.User.Username);

            if (repositories == null)
                return null;

            return repositories.OrderBy(x => x.Name).ConvertToDomain();
        }
    }
}