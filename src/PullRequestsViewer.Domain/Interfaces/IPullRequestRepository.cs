using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.Domain.Interfaces
{
    public interface IPullRequestRepository
    {
        Task<IEnumerable<PullRequest>> GetAll();

        Task<IReadOnlyList<PullRequest>> GetAll(IReadOnlyList<Repository> repositories);
    }
}