using PullRequestsViewer.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PullRequestsViewer.WebApp.Models
{
    public class OrganisationRepositoriesViewModel
    {

        public OrganisationRepositoriesViewModel(IReadOnlyList<OrganisationModel> organisations, IReadOnlyList<Repository> repositories)
        {
            foreach (var organisation in organisations)
            {
                foreach (var repository in organisation.Repositories)
                {
                    if (repositories.Any(x => x.Name.Equals(repository.Name)))
                    {
                        repository.Selected = true;
                    }

                }
            }

            Organisations = organisations;
        }

        public IReadOnlyList<OrganisationModel> Organisations { get; private set; } = new List<OrganisationModel>();
    }
}
