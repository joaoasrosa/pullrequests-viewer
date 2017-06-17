using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;
using Octokit.Internal;

using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

using Repository=PullRequestsViewer.Domain.Repository;

namespace PullRequestsViewer.GitHub
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public RepositoryRepository(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public async Task<IReadOnlyList<Repository>> GetAll(Organisation organisation)
        {
            InMemoryCredentialStore credentials = new InMemoryCredentialStore(
                new Credentials(_credentialsRepository.User.Username,
                    _credentialsRepository.User.Password));
            GitHubClient client = new GitHubClient(
                new ProductHeaderValue("PullRequestsViewer"),
                credentials);

            var repositories = await client.Repository.GetAllForOrg(organisation.Name);

            return repositories.ConvertTo();
        }
    }
}