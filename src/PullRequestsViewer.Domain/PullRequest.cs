using System;

namespace PullRequestsViewer.Domain
{
    public class PullRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Url { get; set; }
        public string AuthorName { get; set; }
        public int Number { get; set; }
    }
}