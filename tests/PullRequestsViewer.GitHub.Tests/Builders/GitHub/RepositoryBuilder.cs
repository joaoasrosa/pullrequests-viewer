using Octokit;
using System;
using System.Collections.Generic;

namespace PullRequestsViewer.GitHub.Tests.Builders.GitHub
{
    internal static class RepositoryBuilder
    {
        internal static IReadOnlyList<Repository> GenerateValidRepositories()
        {
            return new[]
            {
                new Repository(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1,
                    new User(), "Foo", string.Empty, string.Empty, string.Empty, string.Empty, true, false, 1, 1, string.Empty, 1,
                    null, DateTimeOffset.Now, DateTimeOffset.Now, null, null, null, true, true, true, true, 1, 1, null, null, null)
            };
        }

        internal static IReadOnlyList<Repository> GenerateNullRepositories()
        {
            return null;
        }
    }
}
