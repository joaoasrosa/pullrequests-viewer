using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.Domain.Interfaces
{
    public interface IRepositoryRepository
    {
        Task<IReadOnlyList<Repository>> GetAllAsync(Organisation organisation);
    }
}