using System.Collections.Generic;

namespace PullRequestsViewer.WebApp.Models
{
    public class OrganisationRepositoriesViewModel
    {
        public IReadOnlyList<OrganisationModel> Organisations { get; set; } = new List<OrganisationModel>();
    }
}
