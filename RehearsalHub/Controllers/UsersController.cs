using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Users;
using RehearsalHub.Web.ViewModels.User;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    /// <summary>
    /// Controller for managing user profiles and related information.
    /// </summary>
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the current authenticated user's ID.
        /// </summary>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// Displays the profile of the currently logged-in user, including all bands, roles, and instruments.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Profile(string? id)
        {
            string userId = id ?? GetCurrentUserId();

            try
            {
                var model = await userService.GetUserProfileAsync(userId);

                if (model == null)
                {
                    logger.LogWarning("Profile not found for user {UserId}", userId);
                    TempData["ErrorMessage"] = "Profile could not be loaded.";
                    return RedirectToAction("Index", "Home");
                }

                return View("Profile", model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading profile for user {UserId}", userId);
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
