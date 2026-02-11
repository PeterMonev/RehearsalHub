using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Bands;
using RehearsalHub.Web.ViewModels.Bands;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    [Authorize]
    public class BandsController : Controller
    {
        const int PageSize = 6;

        private readonly IBandService bandService;
        private readonly ILogger<BandsController> logger;

        public BandsController(IBandService bandService, ILogger<BandsController> logger)
        {
            this.bandService = bandService;
            this.logger = logger;
        }

        private string? GetUsedId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm,int page = 1)
        {
            if (page < 1) page = 1;

            string? userId = GetUsedId();

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            try
            {
                var viewModel = await this.bandService.GetBandsPagedAsync(userId, page, PageSize, searchTerm);
                ViewData["CurrentSearch"] = searchTerm;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred while loading bands for user {UserId} on page {Page}", userId, page);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
