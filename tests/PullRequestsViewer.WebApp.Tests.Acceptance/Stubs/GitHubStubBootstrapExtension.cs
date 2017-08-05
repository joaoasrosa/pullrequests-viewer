using Microsoft.Extensions.DependencyInjection;
using Octokit;
using Octokit.Internal;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.GitHub;

namespace PullRequestsViewer.WebApp.Tests.Acceptance.Stubs
{
    public static  class GitHubStubBootstrapExtension
    {
        public static void GitHubStubBootstrap(this IServiceCollection services)
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

            services.AddScoped<IGitHubClient, GitHubClientStub>(serviceProvider =>
            {
                var credentialStore = serviceProvider.GetService<ICredentialStore>();

                if (credentialStore == null)
                    return null;

                return new GitHubClientStub();
            });

            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IRepositoryRepository, RepositoryRepository>();
            services.AddScoped<IPullRequestRepository, PullRequestRepository>();
        }
    }
}
