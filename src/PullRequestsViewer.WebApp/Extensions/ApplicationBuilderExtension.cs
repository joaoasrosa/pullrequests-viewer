using Microsoft.AspNetCore.Builder;
using PullRequestsViewer.WebApp.Middleware;

namespace PullRequestsViewer.WebApp.Extensions
{
    internal static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLogging>();
        }
    }
}
