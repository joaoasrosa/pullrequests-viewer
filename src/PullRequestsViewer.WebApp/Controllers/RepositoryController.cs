using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.WebApp.Extensions;
using PullRequestsViewer.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PullRequestsViewer.WebApp.Controllers
{
    public class RepositoryController : Controller
    {
        private readonly IOrganisationRepository _organisationRepository;

        public readonly IRepositoryRepository _repositoryRepository;
        private readonly IRepositoryPersistence _repositoryPersistence;
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly ILogger<RepositoryController> _logger;

        public RepositoryController(IOrganisationRepository organisationRepository,
            IRepositoryRepository repositoryRepository,
            IRepositoryPersistence repositoryPersistence,
            ICredentialsRepository credentialsRepository,
            ILogger<RepositoryController> logger)
        {
            _organisationRepository = organisationRepository;
            _repositoryRepository = repositoryRepository;
            _repositoryPersistence = repositoryPersistence;
            _credentialsRepository = credentialsRepository;
            _logger = logger;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                // TODO make this middleware for relevant actions.
                if (!_credentialsRepository.IsUserSetted())
                    return RedirectToAction("Index", "Home");

                var organisationModels = new List<OrganisationModel>();

                var organisations = await _organisationRepository.GetOrganisationsAsync();

                foreach (var organisation in organisations)
                {
                    var organisationModel = new OrganisationModel
                    {
                        Name = organisation.Name,
                        Repositories = (await _repositoryRepository.GetAllAsync(organisation)).ConvertToModel()
                    };
                    organisationModels.Add(organisationModel);
                }

                return View(new OrganisationRepositoriesViewModel(organisationModels,
                    await _repositoryPersistence.GetAllAsync()));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexAsync(
            OrganisationRepositoriesViewModel organisationRepositoriesViewModel)
        {
            try
            {
                await _repositoryPersistence.SaveAsync(
                    organisationRepositoriesViewModel.ConvertToDomainRepositories());
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}