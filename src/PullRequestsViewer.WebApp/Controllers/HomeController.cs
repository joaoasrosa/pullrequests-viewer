using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PullRequestsViewer.WebApp.Models;
using PullRequestsViewer.Domain.Interfaces;
using PullRequestsViewer.Domain;

namespace PullRequestsViewer.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryPersistence _repositoryPersistence;
        private readonly ICredentialsRepository _credentialsRepository;

        public HomeController(ICredentialsRepository credentialsRepository, IRepositoryPersistence repositoryPersistence)
        {
            _credentialsRepository = credentialsRepository;
            _repositoryPersistence = repositoryPersistence;
        }

        public async Task<IActionResult> Index()
        {
            if (!_credentialsRepository.IsUserSetted())
                return RedirectToAction("Login");

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
