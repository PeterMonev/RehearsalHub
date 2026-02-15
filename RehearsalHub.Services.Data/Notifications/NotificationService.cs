using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models.Models;

namespace RehearsalHub.Services.Data.Notifications
{
    /// <summary>
    /// Service for managing user notifications and real-time notification delivery via SignalR.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHubContext<NotificationsHub> hubContext;
        private readonly ILogger<NotificationService> logger;

        public NotificationService(
            ApplicationDbContext dbContext,
            IHubContext<NotificationsHub> hubContext,
            ILogger<NotificationService> logger)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new notification and sends real-time update to the user via SignalR.
        /// </summary>
        /// <param name="userId">The recipient user ID</param>
        /// <param name="message">The notification message</param>
        /// <param name="url">Optional link URL for the notification</param>
        public async Task CreateAsync(string userId, string message, string? url)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("CreateAsync called with null or empty userId");
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                logger.LogWarning("CreateAsync called with null or empty message for user {UserId}", userId);
                return;
            }

            try
            {
                var notification = new Notification
                {
                    RecipientId = userId,
                    Message = message,
                    LinkUrl = url,
                    CreatedOn = DateTime.UtcNow,
                    IsRead = false
                };

                await dbContext.Notifications.AddAsync(notification);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Notification created for user {UserId}: {Message}", userId, message);

                await SendRealTimeNotificationAsync(userId, message, url);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating notification for user {UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all notifications for a specific user, ordered by creation date (newest first).
        /// </summary>
        /// <param name="userId">The user ID to retrieve notifications for</param>
        /// <returns>Collection of notifications</returns>
        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetUserNotificationsAsync called with null or empty userId");
                return Enumerable.Empty<Notification>();
            }

            try
            {
                return await dbContext.Notifications
                    .AsNoTracking()
                    .Where(n => n.RecipientId == userId)
                    .OrderByDescending(n => n.CreatedOn)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving notifications for user {UserId}", userId);
                return Enumerable.Empty<Notification>();
            }
        }

        /// <summary>
        /// Deletes a notification if it belongs to the specified user.
        /// </summary>
        /// <param name="notificationId">The notification ID to delete</param>
        /// <param name="userId">The user ID for authorization check</param>
        /// <returns>True if deleted successfully, false if not found or unauthorized</returns>
        public async Task<bool> DeleteNotificationAsync(int notificationId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("DeleteNotificationAsync called with null or empty userId");
                return false;
            }

            if (notificationId <= 0)
            {
                logger.LogWarning("DeleteNotificationAsync called with invalid notificationId: {NotificationId}", notificationId);
                return false;
            }

            try
            {
                var notification = await dbContext.Notifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId && n.RecipientId == userId);

                if (notification == null)
                {
                    logger.LogWarning("Notification {NotificationId} not found for user {UserId}", notificationId, userId);
                    return false;
                }

                dbContext.Notifications.Remove(notification);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Notification {NotificationId} deleted by user {UserId}", notificationId, userId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting notification {NotificationId} for user {UserId}", notificationId, userId);
                return false;
            }
        }

        /// <summary>
        /// Marks all unread notifications as read for a specific user.
        /// </summary>
        /// <param name="userId">The user ID whose notifications to mark as read</param>
        public async Task MarkAllAsReadAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("MarkAllAsReadAsync called with null or empty userId");
                return;
            }

            try
            {
                var unreadCount = await dbContext.Notifications
                    .Where(n => n.RecipientId == userId && !n.IsRead)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(n => n.IsRead, true));

                if (unreadCount > 0)
                {
                    logger.LogInformation("Marked {Count} notifications as read for user {UserId}", unreadCount, userId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error marking notifications as read for user {UserId}", userId);
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Sends a real-time notification to the user via SignalR.
        /// </summary>
        private async Task SendRealTimeNotificationAsync(string userId, string message, string? url)
        {
            try
            {
                await hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
                {
                    message,
                    url
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to send real-time notification to user {UserId}", userId);
            }
        }

        #endregion
    }
}