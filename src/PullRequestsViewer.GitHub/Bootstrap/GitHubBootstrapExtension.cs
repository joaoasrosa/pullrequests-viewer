using Microsoft.Extensions.DependencyInjection;
using PullRequestsViewer.Domain.Interfaces;

namespace PullRequestsViewer.GitHub.Bootstrap
{
   public static class GitHubBootstrapExtension
    {
        public static void GitHubBootstrap(this IServiceCollection services)
        {
            services.AddSingleton<ICredentialsRepository, CredentialsRepository>();
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IRepositoryRepository, RepositoryRepository>();
            services.AddScoped<IPullRequestRepository, PullRequestRepository>();
        }
    }
}
