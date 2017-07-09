using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.Domain.Interfaces
{
    /// <summary>
    /// Pull Requests repository.
    /// </summary>
    public interface IPullRequestRepository
    {
        /// <summary>
        /// Returns all open Pull Requests for the given Repositories.
        /// </summary>
        /// <param name="repositories">The Repositories.</param>
        /// <returns>The open Pull Requests.</returns>
        Task<IReadOnlyList<PullRequest>> GetAll(IReadOnlyList<Repository> repositories);
    }
}