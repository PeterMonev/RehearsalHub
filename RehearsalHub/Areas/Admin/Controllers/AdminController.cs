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
        public string GetCurrentUserId() => userManager.GetUserId(User);

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

        /// <summary>
        /// Displays a paginated, searchable list of all bands.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Bands(string? searchTerm, int page = 1)
        {
            if (page < 1) page = 1;

            try
            {
                var model = await adminService.GetBandsPagedAsync(page, PageSize, searchTerm);
                ViewData["CurrentSearch"] = searchTerm;
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading admin bands list");
                TempData["ErrorMessage"] = "Unable to load bands. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Promotes a user to the Admin role.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["ErrorMessage"] = "Invalid user.";
                return RedirectToAction(nameof(Users));
            }

            try
            {

                var success = await adminService.PromoteUserAsync(id);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Could not promote user. Please try again.";
                    return RedirectToAction(nameof(Users));
                }

                var user = await userManager.FindByIdAsync(id);
                TempData["SuccessMessage"] = $"{user?.UserName} has been promoted to Admin.";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error promoting user {UserId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Users));
        }

        /// <summary>
        /// Removes the Admin role from a user.
        /// Prevents self-demotion.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DemoteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["ErrorMessage"] = "Invalid user.";
                return RedirectToAction(nameof(Users));
            }

            try
            {
                var success = await adminService.DemoteUserAsync(id, GetCurrentUserId());

                if (!success)
                {
                    TempData["ErrorMessage"] = "You cannot remove your own Admin role.";
                    return RedirectToAction(nameof(Users));
                }

                var user = await userManager.FindByIdAsync(id);
                TempData["SuccessMessage"] = $"{user?.UserName} is no longer an Admin.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error demoting user {UserId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Users));
        }


        /// <summary>
        /// Soft-deletes a band and all its related rehearsals and setlists.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBand(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid band.";
                return RedirectToAction(nameof(Bands));
            }

            try
            {
                var success = await adminService.DeleteBandAsync(id);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Band not found or already deleted.";
                    return RedirectToAction(nameof(Bands));
                }

                TempData["SuccessMessage"] = "Band has been successfully deleted.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting band {BandId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Bands));
        }

        /// <summary>
        /// Displays a paginated, searchable list of ALL songs in the system.
        /// Admin sees both public and private songs.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Songs(string? searchTerm, int page = 1)
        {
            if (page < 1) page = 1;

            try
            {
                var model = await adminService.GetAllSongsPagedAsync(page, PageSize, searchTerm);
                ViewData["CurrentSearch"] = searchTerm;
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading admin songs list");
                TempData["ErrorMessage"] = "Unable to load songs. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
