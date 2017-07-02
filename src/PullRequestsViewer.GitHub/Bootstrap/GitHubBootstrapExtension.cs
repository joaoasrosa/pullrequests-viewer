using Microsoft.Extensions.DependencyInjection;
using Octokit;
using Octokit.Internal;
using PullRequestsViewer.Domain.Interfaces;

namespace PullRequestsViewer.GitHub.Bootstrap
{
    public static class GitHubBootstrapExtension
    {
        public static void GitHubBootstrap(this IServiceCollection services)
        {
            services.AddSingleton<ICredentialsRepository, CredentialsRepository>();

            services.AddScoped<ICredentialStore, InMemoryCredentialStore>(serviceProvider =>
            {
                var credentialsRepository = serviceProvider.GetService<ICredentialsRepository>();

                if (credentialsRepository.User == null)
                    return null;

                return new InMemoryCredentialStore(new Credentials(credentialsRepository.User.Username,
                    credentialsRepository.User.Password));
            });

            services.AddScoped<IGitHubClient, GitHubClient>(serviceProvider =>
            {
                var credentialStore = serviceProvider.GetService<ICredentialStore>();

                if (credentialStore == null)
                    return null;

                return new GitHubClient(new ProductHeaderValue("PullRequestsViewer"), credentialStore);
            });

            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IRepositoryRepository, RepositoryRepository>();
            services.AddScoped<IPullRequestRepository, PullRequestRepository>();
        }
    }
}
