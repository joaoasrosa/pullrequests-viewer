using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PullRequestsViewer.Domain;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.WebApp.Extensions;
using PullRequestsViewer.WebApp.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PullRequestsViewer.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryPersistence _repositoryPersistence;
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IPullRequestRepository _pullRequestRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICredentialsRepository credentialsRepository,
            IRepositoryPersistence repositoryPersistence,
            IPullRequestRepository pullRequestRepository,
            ILogger<HomeController> logger)
        {
            _credentialsRepository = credentialsRepository;
            _repositoryPersistence = repositoryPersistence;
            _pullRequestRepository = pullRequestRepository;
            _logger = logger;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                if (!_credentialsRepository.IsUserSetted())
                    return RedirectToAction("Login");

                var repositories = await _repositoryPersistence.GetAllAsync();

                if (repositories == null || !repositories.Any())
                {
                    return View(null);
                }

                var pullRequests = await _pullRequestRepository.GetAllAsync(repositories);

                return View(pullRequests.ConvertToViewModel().OrderByDescending(x => x.TotalOpenTimeInMinutes).ToArray());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred retrieving the Pull Requests.");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                _credentialsRepository.SetUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                // TODO: error message
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
