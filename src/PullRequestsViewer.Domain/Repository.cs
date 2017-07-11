namespace PullRequestsViewer.Domain
{
    public class Repository
    {
        public string Name { get; set; }

        // TODO: probably refactor it...
        public string OwnerLogin { get; set; }
    }
}