using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PullRequestsViewer.Domain.Interfaces;
using System;

namespace PullRequestsViewer.SqlLite.Bootstrap
{
    public static class SqlLiteBootstrapExtension
    {
        public static void SqlLiteBootstrap(this IServiceCollection services)
        {
            services.AddDbContext<PullRequestsViewerContext>(x => x.UseSqlite(PullRequestsViewerContext.DataSource));
            services.AddScoped<IPullRequestsViewerContext>(x => x.GetService<PullRequestsViewerContext>());
            services.AddScoped<IRepositoryPersistence, RepositoryPersistence>();
        }

        public static void InitialiseDatastore(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PullRequestsViewerContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
