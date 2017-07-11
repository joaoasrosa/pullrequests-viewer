using PullRequestsViewer.Domain;
using System.Collections.Generic;

namespace PullRequestsViewer.GitHub.Extensions
{
    internal static class PullRequestExtension
    {
        internal static IReadOnlyList<PullRequest> ConvertToDomain(this IReadOnlyList<Octokit.PullRequest> pullRequests)
        {
            if(pullRequests == null)
                return null;

            var domainPullRequests = new PullRequest[pullRequests.Count];

            for(var i = 0;i < pullRequests.Count;i++)
            {
                domainPullRequests[i] = new PullRequest
                                        {
                                            Title = pullRequests[i].Title,
                                            Description = pullRequests[i].Body,
                                            Url = pullRequests[i].Url,
                                            AuthorName = pullRequests[i].User.Name,
                                            Number = pullRequests[i].Number
                                        };
            }

            return domainPullRequests;
        }
    }
}