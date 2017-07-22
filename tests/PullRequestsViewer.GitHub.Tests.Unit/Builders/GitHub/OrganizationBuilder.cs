using System;
using System.Collections.Generic;

using Octokit;

namespace PullRequestsViewer.GitHub.Tests.Builders.GitHub
{
    internal static class OrganizationBuilder
    {
        internal static IReadOnlyList<Organization> GenerateValidOrganizations()
        {
            return new[]
                   {
                       new Organization(string.Empty,
                           string.Empty,
                           string.Empty,
                           1,
                           string.Empty,
                           DateTime.Now,
                           1,
                           string.Empty,
                           1,
                           1,
                           true,
                           string.Empty,
                           1,
                           1,
                           string.Empty,
                           "Foo",
                           string.Empty,
                           1,
                           null,
                           1,
                           1,
                           1,
                           string.Empty,
                           string.Empty),
                       new Organization(string.Empty,
                           string.Empty,
                           string.Empty,
                           1,
                           string.Empty,
                           DateTime.Now,
                           1,
                           string.Empty,
                           1,
                           1,
                           true,
                           string.Empty,
                           1,
                           1,
                           string.Empty,
                           "Bar",
                           string.Empty,
                           1,
                           null,
                           1,
                           1,
                           1,
                           string.Empty,
                           string.Empty)
                   };
        }

        internal static IReadOnlyList<Organization> GenerateNullOrganizations()
        {
            return null;
        }
    }
}