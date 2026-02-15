using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Invitation;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    /// <summary>
    /// Handles band invitation operations including viewing, accepting, and declining invitations.
    /// </summary>
    [Authorize]
    public class InvitationController : Controller
    {
        private readonly IInvitationService invitationService;
        private readonly ILogger<InvitationController> logger;

        public InvitationController(
            IInvitationService invitationService,
            ILogger<InvitationController> logger)
        {
            this.invitationService = invitationService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the current authenticated user's ID.
        /// </summary>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// Displays all pending invitations for the current user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            try
            {
                var invites = await invitationService.GetPendingInvitationsAsync(userId);
                return View(invites);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load invitations for user {UserId}", userId);
                TempData["ErrorMessage"] = "Unable to load invitations. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Accepts a band invitation and adds the user as a confirmed member.
        /// </summary>
        /// <param name="id">The invitation ID to accept</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid invitation ID: {InvitationId}", id);
                TempData["ErrorMessage"] = "Invalid invitation.";
                return RedirectToAction(nameof(Index));
            }

            var userId = GetCurrentUserId();

            try
            {
                var success = await invitationService.AcceptInviteAsync(id, userId);

                if (success)
                {
                    logger.LogInformation("User {UserId} accepted invitation {InvitationId}", userId, id);
                    TempData["SuccessMessage"] = "You joined the band! 🤘";
                    return RedirectToAction("Index", "Bands");
                }

                logger.LogWarning("Failed to accept invitation {InvitationId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "Unable to accept invitation. It may no longer be valid.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error accepting invitation {InvitationId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "An error occurred while accepting the invitation.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Declines a band invitation.
        /// </summary>
        /// <param name="id">The invitation ID to decline</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid invitation ID: {InvitationId}", id);
                TempData["ErrorMessage"] = "Invalid invitation.";
                return RedirectToAction(nameof(Index));
            }

            var userId = GetCurrentUserId();

            try
            {
                var success = await invitationService.DeclineInviteAsync(id, userId);

                if (success)
                {
                    logger.LogInformation("User {UserId} declined invitation {InvitationId}", userId, id);
                    TempData["SuccessMessage"] = "Invitation declined.";
                }
                else
                {
                    logger.LogWarning("Failed to decline invitation {InvitationId} for user {UserId}", id, userId);
                    TempData["ErrorMessage"] = "Unable to decline invitation. It may no longer exist.";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error declining invitation {InvitationId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "An error occurred while declining the invitation.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}