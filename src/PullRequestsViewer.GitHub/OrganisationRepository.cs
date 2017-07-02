using System.Collections.Generic;
using System.Threading.Tasks;

using Octokit;

using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub.Extensions;

namespace PullRequestsViewer.GitHub
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly IGitHubClient _gitHubClient;

        public OrganisationRepository(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<IEnumerable<Organisation>> GetOrganisationsAsync(string username)
        {
            var organisations = await _gitHubClient.Organization.GetAllForCurrent();

            return organisations.ConvertToDomain();
        }
    }
}