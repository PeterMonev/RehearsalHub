using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Rehearsals;
using RehearsalHub.Web.ViewModels.Rehearsal;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    /// <summary>
    /// Controller responsible for managing rehearsals for bands.
    /// Provides actions for listing, creating, editing, viewing details, and deleting rehearsals.
    /// Only authorized users can access these actions.
    /// </summary>
    [Authorize]
    public class RehearsalsController : Controller
    {
        private readonly IRehearsalService rehearsalService;
        private readonly ILogger<RehearsalsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RehearsalsController"/> class.
        /// </summary>
        /// <param name="rehearsalService">Service for handling rehearsal operations.</param>
        /// <param name="logger">Logger instance for logging errors and information.</param>
        public RehearsalsController(
            IRehearsalService rehearsalService,
            ILogger<RehearsalsController> logger)
        {
            this.rehearsalService = rehearsalService;
            this.logger = logger;
        }

        /// <summary>
        /// Displays all rehearsals for a specific band.
        /// </summary>
        /// <param name="bandId">The ID of the band.</param>
        /// <returns>View with a list of rehearsals.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int bandId)
        {
            var userId = GetCurrentUserId();

            try
            {
                var rehearsals = await rehearsalService.GetBandRehearsalsAsync(bandId, userId);
                ViewBag.BandId = bandId;
                return View(rehearsals);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading rehearsals for band {BandId}", bandId);
                TempData["ErrorMessage"] = "An error occurred while loading rehearsals.";
                return RedirectToAction("Details", "Bands", new { id = bandId });
            }
        }

        /// <summary>
        /// Shows the form for creating a new rehearsal for a band.
        /// </summary>
        /// <param name="bandId">The ID of the band.</param>
        /// <returns>Create rehearsal view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create(int bandId)
        {
            var userId = GetCurrentUserId();

            try
            {
                var model = await rehearsalService.GetRehearsalForCreateAsync(bandId, userId);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "You don't have permission to create rehearsals for this band.";
                    return RedirectToAction("Details", "Bands", new { id = bandId });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading create rehearsal form for band {BandId}", bandId);
                TempData["ErrorMessage"] = "An error occurred while loading the form.";
                return RedirectToAction("Details", "Bands", new { id = bandId });
            }
        }

        /// <summary>
        /// Handles the submission of a new rehearsal.
        /// Validates the model and checks business rules like time range and not scheduling in the past.
        /// </summary>
        /// <param name="model">Rehearsal input model.</param>
        /// <returns>Redirects to rehearsal details or reloads form on error.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RehearsalInputModel model)
        {
            var currentUserId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                await ReloadCreateModel(model, currentUserId);
                return View(model);
            }

            if (!rehearsalService.ValidateNotInPast(model.StartRehearsal))
            {
                ModelState.AddModelError(nameof(model.StartRehearsal), "Rehearsal must be scheduled from now onwards");
                await ReloadCreateModel(model, currentUserId);
                return View(model);
            }

            if (!rehearsalService.ValidateTimeRange(model.StartRehearsal, model.EndRehearsal))
            {
                ModelState.AddModelError(nameof(model.EndRehearsal), "End time must be after start time");
                await ReloadCreateModel(model, currentUserId);
                return View(model);
            }

            try
            {
                int rehearsalId = await rehearsalService.CreateRehearsalAsync(model, currentUserId);

                logger.LogInformation("User {UserId} created rehearsal {RehearsalId}", currentUserId, rehearsalId);
                TempData["SuccessMessage"] = "Rehearsal booked successfully! 🎸";

                return RedirectToAction(nameof(Details), new { id = rehearsalId });
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "Only band owners can create rehearsals.";
                return RedirectToAction("Details", "Bands", new { id = model.BandId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await ReloadCreateModel(model, currentUserId);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating rehearsal for user {UserId}", currentUserId);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the rehearsal.");
                await ReloadCreateModel(model, currentUserId);
                return View(model);
            }
        }

        /// <summary>
        /// Shows detailed information for a specific rehearsal.
        /// </summary>
        /// <param name="id">The ID of the rehearsal.</param>
        /// <returns>View with rehearsal details.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var model = await rehearsalService.GetRehearsalDetailsAsync(id, userId);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "Rehearsal not found or you don't have access to it.";
                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading rehearsal {RehearsalId}", id);
                TempData["ErrorMessage"] = "An error occurred while loading the rehearsal.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Shows the form to edit an existing rehearsal.
        /// </summary>
        /// <param name="id">The ID of the rehearsal.</param>
        /// <returns>Edit rehearsal view.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var model = await rehearsalService.GetRehearsalForEditAsync(id, userId);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "You don't have permission to edit this rehearsal.";
                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading edit form for rehearsal {RehearsalId}", id);
                TempData["ErrorMessage"] = "An error occurred while loading the form.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        /// <summary>
        /// Handles the submission of an edited rehearsal.
        /// Validates model and business rules before updating.
        /// </summary>
        /// <param name="model">Rehearsal input model.</param>
        /// <returns>Redirects to details or reloads form on error.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RehearsalInputModel model)
        {
            var currentUserId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                await ReloadEditModel(model, currentUserId);
                return View(model);
            }

            if (!rehearsalService.ValidateNotInPast(model.StartRehearsal))
            {
                ModelState.AddModelError(nameof(model.StartRehearsal), "Rehearsal must be scheduled from now onwards");
                await ReloadEditModel(model, currentUserId);
                return View(model);
            }

            if (!rehearsalService.ValidateTimeRange(model.StartRehearsal, model.EndRehearsal))
            {
                ModelState.AddModelError(nameof(model.EndRehearsal), "End time must be after start time");
                await ReloadEditModel(model, currentUserId);
                return View(model);
            }

            try
            {
                bool isUpdated = await rehearsalService.UpdateRehearsalAsync(model, currentUserId);

                if (!isUpdated)
                {
                    TempData["ErrorMessage"] = "You don't have permission to edit this rehearsal.";
                    return RedirectToAction("Index", "Home");
                }

                logger.LogInformation("User {UserId} updated rehearsal {RehearsalId}", currentUserId, model.Id);
                TempData["SuccessMessage"] = "Rehearsal updated successfully! 🎸";

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await ReloadEditModel(model, currentUserId);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating rehearsal {RehearsalId}", model.Id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the rehearsal.");
                await ReloadEditModel(model, currentUserId);
                return View(model);
            }
        }

        /// <summary>
        /// Deletes a rehearsal if the user has permission.
        /// </summary>
        /// <param name="id">The ID of the rehearsal to delete.</param>
        /// <returns>Redirects to band details or home on error.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var rehearsal = await rehearsalService.GetRehearsalDetailsAsync(id, userId);

                if (rehearsal == null)
                {
                    TempData["ErrorMessage"] = "Rehearsal not found.";
                    return RedirectToAction("Index", "Home");
                }

                int bandId = rehearsal.BandId;

                bool isDeleted = await rehearsalService.DeleteRehearsalAsync(id, userId);

                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "You don't have permission to delete this rehearsal.";
                    return RedirectToAction("Details", "Bands", new { id = bandId });
                }

                logger.LogInformation("User {UserId} deleted rehearsal {RehearsalId}", userId, id);
                TempData["SuccessMessage"] = "Rehearsal cancelled successfully.";

                return RedirectToAction("Details", "Bands", new { id = bandId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting rehearsal {RehearsalId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the rehearsal.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Displays all upcoming rehearsals for the currently logged-in user.
        /// </summary>
        /// <returns>View with upcoming rehearsals for user.</returns>
        [HttpGet]
        public async Task<IActionResult> MyRehearsals()
        {
            var userId = GetCurrentUserId();

            try
            {
                var rehearsals = await rehearsalService.GetAllUpcomingForUserAsync(userId);
                return View("Index", rehearsals);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading rehearsals for user {UserId}", userId);
                TempData["ErrorMessage"] = "An error occurred while loading rehearsals.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Retrieves the current logged-in user's ID from claims.
        /// </summary>
        /// <returns>User ID string.</returns>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// Reloads the create rehearsal model with available setlists and band name.
        /// </summary>
        private async Task ReloadCreateModel(RehearsalInputModel model, string userId)
        {
            var reloaded = await rehearsalService.GetRehearsalForCreateAsync(model.BandId, userId);
            if (reloaded != null)
            {
                model.AvailableSetlists = reloaded.AvailableSetlists;
                model.BandName = reloaded.BandName;
            }
        }

        /// <summary>
        /// Reloads the edit rehearsal model with available setlists and band name.
        /// </summary>
        private async Task ReloadEditModel(RehearsalInputModel model, string userId)
        {
            var reloaded = await rehearsalService.GetRehearsalForEditAsync(model.Id!.Value, userId);
            if (reloaded != null)
            {
                model.AvailableSetlists = reloaded.AvailableSetlists;
                model.BandName = reloaded.BandName;
            }
        }
    }
}
