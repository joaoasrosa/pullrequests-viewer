using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PullRequestsViewer.GitHub.Bootstrap;
using PullRequestsViewer.SqlLite.Bootstrap;
using Serilog;
using Microsoft.Extensions.Logging;

namespace PullRequestsViewer.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SetupLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                // Specifying dispose: true closes and flushes the Serilog `Log` class when the app shuts down.
                builder.AddSerilog(dispose: true);
            });

            services.AddMvc();
            services.GitHubBootstrap();
            services.SqlLiteBootstrap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.ApplicationServices.InitialiseDatastore();
        }

        private void SetupLogger() => Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();
    }
}
