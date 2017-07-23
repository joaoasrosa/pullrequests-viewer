using PullRequestsViewer.Domain;
using PullRequestsViewer.WebApp.Models;
using System.Collections.Generic;

namespace PullRequestsViewer.WebApp.Extensions
{
    internal static class OrganisationRepositoriesViewModelExtension
    {
        internal static IReadOnlyList<Repository> ConvertToDomainRepositories(this OrganisationRepositoriesViewModel organisationRepositoriesViewModel)
        {
            if (organisationRepositoriesViewModel == null)
                return null;
            var repositories = new List<Repository>();

            foreach (var organisation in organisationRepositoriesViewModel.Organisations)
            {
                foreach (var repository in organisation.Repositories)
                {
                    // TODO: Business logic here. Once we move to react we won't need it.
                    if (!repository.Selected)
                        continue;

                    repositories.Add(new Repository
                    {
                        Name = repository.Name,
                        OwnerLogin = repository.OwnerLogin
                    });
                }
            }

            return repositories;
        }
    }
}
