using Microsoft.EntityFrameworkCore;
using PullRequestsViewer.Domain;
using System.Threading.Tasks;

namespace PullRequestsViewer.SqlLite
{
    public interface IPullRequestsViewerContext
    {
        DbSet<Repository> Repositories { get; set; }

        Task SaveChangesAsync();
    }
}
