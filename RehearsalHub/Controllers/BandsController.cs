using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Data.Models;
using RehearsalHub.Services.Data.Bands;
using RehearsalHub.Services.Data.Invitation;
using RehearsalHub.Web.ViewModels.Bands;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    /// <summary>
    /// Controller for managing band operations including CRUD and member invitations.
    /// </summary>
    [Authorize]
    public class BandsController : Controller
    {
        private const int PageSize = 6;

        private readonly IBandService bandService;
        private readonly IInvitationService invitationService;
        private readonly ILogger<BandsController> logger;

        public BandsController(
            IBandService bandService,
            IInvitationService invitationService,
            ILogger<BandsController> logger)
        {
            this.bandService = bandService;
            this.invitationService = invitationService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the current authenticated user's ID.
        /// </summary>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// Displays a paginated list of bands the user owns or is a member of.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm, int page = 1)
        {
            if (page < 1) page = 1;

            var userId = GetCurrentUserId();

            try
            {
                var viewModel = await bandService.GetBandsPagedAsync(userId, page, PageSize, searchTerm);
                ViewData["CurrentSearch"] = searchTerm;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading bands for user {UserId} on page {Page}", userId, page);
                TempData["ErrorMessage"] = "Unable to load bands. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Displays the band creation form.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View(new BandInputModel());
        }

        /// <summary>
        /// Creates a new band with the current user as owner.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                int bandId = await bandService.CreateBandAsync(model, userId);

                logger.LogInformation("User {UserId} created band {BandId}", userId, bandId);
                TempData["SuccessMessage"] = "Band created successfully! 🎸";

                return RedirectToAction(nameof(Details), new { id = bandId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating band for user {UserId}", userId);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the band.");
                return View(model);
            }
        }

        /// <summary>
        /// Deletes a band. Only the owner can delete their band.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid band ID for deletion: {BandId}", id);
                TempData["ErrorMessage"] = "Invalid band.";
                return RedirectToAction(nameof(Index));
            }

            var userId = GetCurrentUserId();

            try
            {
                bool isDeleted = await bandService.DeleteBandAsync(id, userId);

                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "You don't have permission to delete this band or it doesn't exist.";
                    return RedirectToAction(nameof(Index));
                }

                logger.LogInformation("User {UserId} deleted band {BandId}", userId, id);
                TempData["SuccessMessage"] = "The band was successfully deleted! 🎸";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting band {BandId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "A technical error occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Displays the band edit form. Only the owner can edit.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid band ID for edit: {BandId}", id);
                return NotFound();
            }

            var userId = GetCurrentUserId();

            try
            {
                var band = await bandService.GetBandEditAsync(id, userId);

                if (band == null)
                {
                    logger.LogWarning("Unauthorized or non-existent edit attempt for band {BandId} by user {UserId}", id, userId);
                    return NotFound();
                }

                return View(band);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading edit form for band {BandId}", id);
                TempData["ErrorMessage"] = "Unable to load band for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Updates band information. Only the owner can edit.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BandEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                bool isUpdated = await bandService.EditBandAsync(model, userId);

                if (!isUpdated)
                {
                    TempData["ErrorMessage"] = "You are not authorized to edit this band.";
                    return RedirectToAction(nameof(Index));
                }

                logger.LogInformation("User {UserId} edited band {BandId}", userId, model.Id);
                TempData["SuccessMessage"] = "Changes saved successfully! 🎸";

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing band {BandId} for user {UserId}", model.Id, userId);
                ModelState.AddModelError(string.Empty, "An error occurred while editing the band.");
                return View(model);
            }
        }

        /// <summary>
        /// Displays detailed band information including members, setlists, and rehearsals.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid band ID for details: {BandId}", id);
                return NotFound();
            }

            var userId = GetCurrentUserId();

            try
            {
                var band = await bandService.GetBandDetailsAsync(id, userId);

                if (band == null)
                {
                    logger.LogWarning("User {UserId} attempted to access non-existent or unauthorized band {BandId}", userId, id);
                    return NotFound();
                }

                return View(band);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading details for band {BandId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "Unable to load band details.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Sends a band invitation to a user by email or username.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invite(int bandId, string searchTerm, string instrument)
        {
            if (bandId <= 0)
            {
                logger.LogWarning("Invalid band ID for invitation: {BandId}", bandId);
                TempData["ErrorMessage"] = "Invalid band.";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                TempData["ErrorMessage"] = "Please enter a username or email.";
                return RedirectToAction(nameof(Details), new { id = bandId });
            }

            if (string.IsNullOrWhiteSpace(instrument))
            {
                TempData["ErrorMessage"] = "Please select an instrument.";
                return RedirectToAction(nameof(Details), new { id = bandId });
            }

            try
            {
                var success = await invitationService.SendInviteAsync(bandId, searchTerm, instrument);

                if (success)
                {
                    logger.LogInformation("Invitation sent to {SearchTerm} for band {BandId}", searchTerm, bandId);
                    TempData["SuccessMessage"] = "Invitation sent successfully! 🎸";
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to send invitation. User may not exist, already be a member, or have a pending invitation.";
                }

                return RedirectToAction(nameof(Details), new { id = bandId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending invite for band {BandId}", bandId);
                TempData["ErrorMessage"] = "An error occurred while sending the invitation.";
                return RedirectToAction(nameof(Details), new { id = bandId });
            }
        }

        /// <summary>
        /// Removes a member from the band or allows a member to leave.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMember(int bandId, string userId)
        {
            var currentUserId = GetCurrentUserId();

            try
            {
                bool success = await bandService.DeleteBandMembersDetailsAsync(bandId, userId, currentUserId);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Action failed. Check permissions.";
                    return RedirectToAction(nameof(Details), new { id = bandId });
                }

                if (userId == currentUserId)
                {
                    TempData["SuccessMessage"] = "You left the band. 🎸";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Member removed.";
                return RedirectToAction(nameof(Details), new { id = bandId });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RemoveMember error");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}