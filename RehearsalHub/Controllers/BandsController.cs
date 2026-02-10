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
        private readonly IBandService bandService;
        public BandsController(IBandService bandService)
        {
            this.bandService = bandService;
        }

        private string GetUsedId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUsedId();

            if (userId == null)
            {
                return BadRequest();
            }
 
            var viewModel = await this.bandService.GetAllBandsAsync(userId);

            return View(viewModel);
        }
    }
}
