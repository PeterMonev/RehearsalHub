using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Web.ViewModels.Notification;
using System.Security.Claims;

namespace RehearsalHub.Web.Controllers
{
    /// <summary>
    /// Controller for managing user notifications.
    /// </summary>
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly ILogger<NotificationsController> logger;

        public NotificationsController(
            INotificationService notificationService,
            ILogger<NotificationsController> logger)
        {
            this.notificationService = notificationService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the current authenticated user's ID.
        /// </summary>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// Displays all notifications for the current user and marks them as read.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            try
            {
                var notifications = await notificationService.GetUserNotificationsAsync(userId);

                var viewModel = notifications.Select(n => new NoficationViewModel
                {
                    Id = n.Id,
                    Message = n.Message,
                    LinkUrl = n.LinkUrl,
                    IsRead = n.IsRead,
                    CreatedOn = n.CreatedOn
                }).ToList();

                await notificationService.MarkAllAsReadAsync(userId);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading notifications for user {UserId}", userId);
                TempData["ErrorMessage"] = "Unable to load notifications. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Deletes a specific notification.
        /// </summary>
        /// <param name="id">The notification ID to delete</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                logger.LogWarning("Invalid notification ID: {NotificationId}", id);
                TempData["ErrorMessage"] = "Invalid notification.";
                return RedirectToAction(nameof(Index));
            }

            var userId = GetCurrentUserId();

            try
            {
                var success = await notificationService.DeleteNotificationAsync(id, userId);

                if (success)
                {
                    logger.LogInformation("User {UserId} deleted notification {NotificationId}", userId, id);
                    TempData["SuccessMessage"] = "Notification deleted successfully.";
                }
                else
                {
                    logger.LogWarning("Failed to delete notification {NotificationId} for user {UserId}", id, userId);
                    TempData["ErrorMessage"] = "Unable to delete notification. It may no longer exist.";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting notification {NotificationId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "An error occurred while deleting the notification.";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Marks all notifications as read (optional endpoint if needed separately).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetCurrentUserId();

            try
            {
                await notificationService.MarkAllAsReadAsync(userId);
                TempData["SuccessMessage"] = "All notifications marked as read.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error marking notifications as read for user {UserId}", userId);
                TempData["ErrorMessage"] = "An error occurred.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}