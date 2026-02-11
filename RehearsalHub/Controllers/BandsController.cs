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

        public BandsController(IBandService bandService)
        {
            this.bandService = bandService;
        }

        private string GetUsedId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User identifier could not be found.");
            }

            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            if(page < 1)
            {
                page = 1;
            }

            string? userId = GetUsedId();

            if (userId == null)
            {
                return BadRequest();
            }
 
            var viewModel = await this.bandService.GetBandsPagedAsync(userId, page, PageSize);

            return View(viewModel);
        }
    }
}
