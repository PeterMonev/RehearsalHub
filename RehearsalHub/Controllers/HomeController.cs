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

        public IActionResult StatusCode(int code)
        {
            _logger.LogWarning("HTTP {StatusCode} returned for path: {Path}",
               code, HttpContext.Request.Path);

            ViewData["StatusCode"] = code;

            return code switch
            {
                404 => View("NotFound"),
                _   => View("ServerError")
            };
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TermsOfUse()
        {
            return View();
        }
    }
}
