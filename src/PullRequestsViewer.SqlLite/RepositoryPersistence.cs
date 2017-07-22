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

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="context">The databse context.</param>
        public RepositoryPersistence(IPullRequestsViewerContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Repository>> GetAllAsync()
        {
            return await _context.Repositories.ToAsyncEnumerable().ToArray();
        }

        /// <inheritdoc />
        public async Task SaveAsync(IReadOnlyList<Repository> repositories)
        {
            foreach (var repository in repositories)
            {
                var repositoryEntity = await _context.Repositories.FindAsync(repository.Name);
                if (null == repositoryEntity)
                {
                    await _context.Repositories.AddAsync(repository);
                }
                else
                {
                    repositoryEntity.Name = repository.Name;
                    repositoryEntity.OwnerLogin = repository.OwnerLogin;
                    _context.Repositories.Update(repositoryEntity);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
