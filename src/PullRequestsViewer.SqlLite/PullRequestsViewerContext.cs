using Microsoft.EntityFrameworkCore;
using PullRequestsViewer.Domain;
using System.Threading.Tasks;

namespace PullRequestsViewer.SqlLite
{
    public class PullRequestsViewerContext : DbContext, IPullRequestsViewerContext
    {
        public PullRequestsViewerContext() : base() { }

        public PullRequestsViewerContext(DbContextOptions options) : base(options) { }

        public DbSet<Repository> Repositories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Repository>().HasKey(x => x.Name);
        }

        async Task IPullRequestsViewerContext.SaveChangesAsync()
        {
           await this.SaveChangesAsync();
        }
    }
}
