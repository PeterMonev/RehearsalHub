using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Web.ViewModels;

namespace RehearsalHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusCode(int code)
        {
            var originalPath = HttpContext.Features
                 .Get<Microsoft.AspNetCore.Diagnostics.IStatusCodeReExecuteFeature>()
                 ?.OriginalPath ?? HttpContext.Request.Path;

            _logger.LogWarning("HTTP {StatusCode} — Path: {Path} — User: {User}",
               code, originalPath, User.Identity?.Name ?? "anonymous");

            var model = ErrorViewModel.ForStatusCode(code);
            return View("Error", model);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = ErrorViewModel.ForStatusCode(500);
            model.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(model);
        }

        public IActionResult TermsOfUse()
        {
            return View();
        }
    }
}
