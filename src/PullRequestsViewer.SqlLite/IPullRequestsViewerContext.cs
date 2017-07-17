using Microsoft.EntityFrameworkCore;
using PullRequestsViewer.Domain;

namespace PullRequestsViewer.SqlLite
{
    public interface IPullRequestsViewerContext
    {
        DbSet<Repository> Repositories { get; set; }
    }
}
