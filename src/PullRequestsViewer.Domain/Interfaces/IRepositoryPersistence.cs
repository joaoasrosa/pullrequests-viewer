using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.Domain.Interfaces
{
    /// <summary>
    /// Repository persistence interface.
    /// </summary>
    public interface IRepositoryPersistence
    {
        /// <summary>
        /// Returns all the stored repositories.
        /// </summary>
        /// <returns>The Repositories.</returns>
        Task<IReadOnlyList<Repository>> GetAllAsync();

        /// <summary>
        /// Saves the repositories.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        Task SaveAsync(IReadOnlyList<Repository> repositories);
    }
}
