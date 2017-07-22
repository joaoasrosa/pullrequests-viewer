using PullRequestsViewer.Domain;
using System.Collections.Generic;

namespace PullRequestsViewer.GitHub.Tests.Builders.Domain
{
    internal static  class RepositoryBuilder
    {
        internal static IReadOnlyList<Repository> GenerateValidRepositories() {
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
