using Octokit;
using System;
using System.Collections.Generic;

namespace PullRequestsViewer.GitHub.Tests.Builders.GitHub
{
    internal static class PullRequestBuilder
    {
        internal static IReadOnlyList<PullRequest> GenerateValidPullRequests()
        {
            return new[]
            {
                new PullRequest(1, new Uri("https://www.github.com"), new Uri("https://www.github.com"), new Uri("https://www.github.com"), 
                    new Uri("https://www.github.com"), new Uri("https://www.github.com"), new Uri("https://www.github.com"), 1, ItemState.Open, 
                    "Foo", "Bar", DateTimeOffset.Now, DateTimeOffset.Now, null, null, null, null, new User(), 
                    null, null, null, null, 1, 1, 1, 0, 1, null, false)
            };
        }

        internal static IReadOnlyList<PullRequest> GenerateNullPullRequests()
        {
            return null;
        }
    }
}
