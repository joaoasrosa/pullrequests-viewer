using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullRequestsViewer.SqlLite
{
    public class RepositoryPersistence : IRepositoryPersistence
    {
        private readonly IPullRequestsViewerContext _context;

        public RepositoryPersistence(IPullRequestsViewerContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Repository>> GetAllAsync()
        {
            return await _context.Repositories.ToAsyncEnumerable().ToArray();
        }
    }
}
