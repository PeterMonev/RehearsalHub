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
        private const int PageSize = 10;

        private readonly IAdminService adminService;
        private readonly ILogger<AdminController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger, UserManager<ApplicationUser> userManager)
        {
            this.adminService = adminService;
            this.logger = logger;
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets the current admin user's ID.
        /// </summary>
        public string GetUserId() => userManager.GetUserId(User);

        /// <summary>
        /// Displays the admin dashboard with aggregate statistics.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var stats = await adminService.GetDashboardStatsAsync();
                return View(stats);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading admin dashboard");
                TempData["ErrorMessage"] = "Unable to load dashboard. Please try again.";
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        /// <summary>
        /// Displays a paginated, searchable list of all users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Users(string? searchTerm, int page = 1)
        {
            if (page < 1) page = 1;

            try
            {
                var model = await adminService.GetUsersPagedAsync(page, PageSize, searchTerm);
                ViewData["CurrentSearch"] = searchTerm;
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading admin users list");
                TempData["ErrorMessage"] = "Unable to load users. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
    
}
