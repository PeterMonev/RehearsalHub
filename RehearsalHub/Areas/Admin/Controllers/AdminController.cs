using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Areas.Admin.Data;
using RehearsalHub.Data.Models;

namespace RehearsalHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly ILogger<AdminController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger, UserManager<ApplicationUser> userManager)
        {
            this.adminService = adminService;
            this.logger = logger;
            this.userManager = userManager;
        }

        public string GetUserId() => userManager.GetUserId(User);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try {
                var stats = await adminService.GetDashboardStatsAsync();
                return View(stats);
            } catch (Exception ex)
            {
                logger.LogError(ex, "Error loading admin dashboard");
                TempData["ErrorMessage"] = "Unable to load dashboard. Please try again.";
                return RedirectToAction("Index", "Home", new { area = ""});
            }
        }
    }
}
