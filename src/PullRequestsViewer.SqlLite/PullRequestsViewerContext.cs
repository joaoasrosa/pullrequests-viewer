using Microsoft.EntityFrameworkCore;
using PullRequestsViewer.Domain;

namespace PullRequestsViewer.SqlLite
{
    public class PullRequestsViewerContext : DbContext, IPullRequestsViewerContext
    {
        internal static string DataSource = "Data Source=PullRequestsViewer.db";

        public PullRequestsViewerContext() : base() { }

        public PullRequestsViewerContext(DbContextOptions options) : base(options) { }

        public DbSet<Repository> Repositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DataSource);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Repository>().HasKey(x => x.Name);
        }
    }
}
