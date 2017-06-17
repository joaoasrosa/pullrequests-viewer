using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;
using Octokit.Internal;

using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

namespace PullRequestsViewer.GitHub
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public OrganisationRepository(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public async Task<IEnumerable<Organisation>> GetOrganisationsAsync(string username)
        {
            InMemoryCredentialStore credentials = new InMemoryCredentialStore(
                new Credentials(_credentialsRepository.User.Username,
                    _credentialsRepository.User.Password));
            GitHubClient client = new GitHubClient(
                new ProductHeaderValue("PullRequestsViewer"),
                credentials);

            var organisations = await client.Organization.GetAllForCurrent();

            return organisations.ConvertTo();
        }
    }
}