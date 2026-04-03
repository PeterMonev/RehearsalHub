using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Areas.Admin.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Services.Data.Admin;
using RehearsalHub.Web.ViewModels.Admin;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Song;

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
        /// Displays a paginated and searchable list of users.
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
        /// Displays a paginated and searchable list of bands.
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
        /// Promotes a user to Admin role.
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
        /// Demotes a user from Admin role (prevents self-demotion).
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
        /// Soft deletes a band and its related data.
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
        /// Loads band data for editing.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> EditBand(int id)
        {
            var model = await adminService.GetBandForEditAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Band not found.";
                return RedirectToAction(nameof(Bands));
            }

            return View(model);
        }

        /// <summary>
        /// Updates band information.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBand(BandEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool success = await adminService.AdminEditBandAsync(model);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Could not update band.";
                    return View(model);
                }
                TempData["SuccessMessage"] = $"\"{model.Name}\" has been updated.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing band {BandId}", model.Id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Bands));
        }

        /// <summary>
        /// Displays all songs (public and private).
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

        /// <summary>
        /// Deletes a song (admin override).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid song.";
                return RedirectToAction(nameof(Songs));
            }

            try
            {
                var success = await adminService.AdminDeleteSongsAsync(id);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Song not found or already deleted.";
                    return RedirectToAction(nameof(Songs));
                }

                TempData["SuccessMessage"] = "Song has been successfully deleted.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting song {SongId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Songs));
        }

        /// <summary>
        /// Soft deletes a user.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["ErrorMessage"] = "Invalid user.";
                return RedirectToAction(nameof(Users));
            }

            try
            {
                bool success = await adminService.DeleteUserAsync(id, GetCurrentUserId());
                if (!success)
                {
                    TempData["ErrorMessage"] = "Cannot delete this user.";
                    return RedirectToAction(nameof(Users));
                }
                TempData["SuccessMessage"] = "User has been deleted.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user {UserId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Users));
        }

        /// <summary>
        /// Loads song data for editing.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> EditSong(int id)
        {
            var model = await adminService.GetSongForAdminEditAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Song not found.";
                return RedirectToAction(nameof(Songs));
            }

            return View(model);
        }

        /// <summary>
        /// Updates song information.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(SongInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool success = await adminService.AdminEditSongAsync(model);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Could not update song.";
                    return View(model);
                }

                TempData["SuccessMessage"] = $"\"{model.Title}\" has been updated.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing song {SongId}", model.Id);
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Songs));
        }

        /// <summary>
        /// Displays form for creating a new song.
        /// </summary>
        [HttpGet]
        public IActionResult CreateSong()
        {
            return View(new SongInputModel { IsPrivate = false });
        }

        /// <summary>
        /// Creates a new song.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSong(SongInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int success = await adminService.AdminCreateSongAsync(model, GetCurrentUserId());

                if (success == 0)
                {
                    TempData["ErrorMessage"] = "Could not create song.";
                    return View(model);
                }

                TempData["SuccessMessage"] = $"\"{model.Title}\" has been created.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating song");
                TempData["ErrorMessage"] = "An unexpected error occurred.";
            }

            return RedirectToAction(nameof(Songs));
        }
    }
}