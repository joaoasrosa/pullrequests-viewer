using PullRequestsViewer.Domain;
using PullRequestsViewer.WebApp.Models;
using System.Collections.Generic;

namespace PullRequestsViewer.WebApp.Extensions
{
    internal static class PullRequestViewModelExtension
    {
        internal static IReadOnlyList<PullRequestViewModel> ConvertToViewModel(this IReadOnlyList<PullRequest> pullRequests)
        {
            if (pullRequests == null)
                return null;

            var pullRequestViewModels = new PullRequestViewModel[pullRequests.Count];

            for (var i = 0; i < pullRequests.Count; i++)
            {
                pullRequestViewModels[i] = new PullRequestViewModel
                {
                    AuthorName = pullRequests[i].AuthorName,
                    CreatedDate = pullRequests[i].CreatedDate,
                    Description = pullRequests[i].Description,
                    HtmlUrl = pullRequests[i].HtmlUrl,
                    LastUpdateDate = pullRequests[i].LastUpdateDate,
                    Number = pullRequests[i].Number,
                    Title = pullRequests[i].Title
                };
            }

            return pullRequestViewModels;
        }
    }
}
