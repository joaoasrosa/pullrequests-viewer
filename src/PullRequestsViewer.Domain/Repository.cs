namespace PullRequestsViewer.Domain
{
    /// <summary>
    /// Repository.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Repository Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Repository Owner.
        /// </summary>
        // TODO: probably refactor it...
        public string OwnerLogin { get; set; }
    }
}