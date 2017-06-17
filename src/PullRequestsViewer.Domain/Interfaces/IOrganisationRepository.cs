using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.Domain.Interfaces
{
    public interface IOrganisationRepository
    {
        Task<IEnumerable<Organisation>> GetOrganisationsAsync(string username);
    }
}