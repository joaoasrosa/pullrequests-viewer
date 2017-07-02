using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PullRequestsViewer.WebApp.Models;
using PullRequestsViewer.Domain.Interfaces;

namespace PullRequestsViewer.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPullRequestRepository _pullRequest;
        private readonly ICredentialsRepository _credentialsRepository;

        public HomeController(ICredentialsRepository credentialsRepository, IPullRequestRepository pullRequest)
        {
            _credentialsRepository = credentialsRepository;
            _pullRequest = pullRequest;
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
