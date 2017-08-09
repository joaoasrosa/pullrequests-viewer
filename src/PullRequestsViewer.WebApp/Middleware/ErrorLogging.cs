using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PullRequestsViewer.WebApp.Middleware
{
    public class ErrorLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLogging> _logger;

        public ErrorLogging(RequestDelegate next, ILogger<ErrorLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
