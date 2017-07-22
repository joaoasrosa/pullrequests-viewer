using System.Collections.Generic;

namespace PullRequestsViewer.WebApp.Models
{
    public class OrganisationModel
    {
        public string Name { get; set; }

        public IReadOnlyList<RepositoryModel> Repositories { get; set; }
    }
}
