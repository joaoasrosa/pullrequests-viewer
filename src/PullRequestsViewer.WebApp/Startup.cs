using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PullRequestsViewer.GitHub.Bootstrap;
using PullRequestsViewer.SqlLite.Bootstrap;
using Serilog;

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
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.GitHubBootstrap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

        protected void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                // Specifying dispose: true closes and flushes the Serilog `Log` class when the app shuts down.
                builder.AddSerilog(dispose: true);
            });

            // TODO move to react and remove this dependency
            services.Configure<FormOptions>(x => x.ValueCountLimit = 2048);
            services.AddMvc();
            services.SqlLiteBootstrap(Configuration.GetConnectionString("PullRequestsViewerDatabase"));
        }

        private void SetupLogger() => Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();
    }
}
