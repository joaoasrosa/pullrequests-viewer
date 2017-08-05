using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PullRequestsViewer.SqlLite;
using System;

namespace PullRequestsViewer.WebApp.Tests.Acceptance.Stubs
{
    public class StartupStub : Startup
    {
        private static IApplicationBuilder _applicationBuilder;

        public StartupStub(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.GitHubStubBootstrap();
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.Configure(app, env);
            _applicationBuilder = app;
        }

        public static void TearDownDatabases()
        {
            using (var serviceScope = _applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PullRequestsViewerContext>();

                context.Database.EnsureDeleted();
            }
        }
    }
}
