using PullRequestsViewer.Domain;
using System.Collections.Generic;

namespace PullRequestsViewer.Common.Tests.Builders.Domain
{
    public static class RepositoryBuilder
    {
        public static IReadOnlyList<Repository> GenerateValidRepositories()
        {
            return new[]
            {
                new Repository
                {
                    Name = "Foo",
                    OwnerLogin = "joaoasrosa"
                }
            };
        }
    }
}
