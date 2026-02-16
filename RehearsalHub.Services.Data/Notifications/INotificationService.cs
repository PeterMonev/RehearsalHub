using RehearsalHub.Data.Models.Models;

namespace RehearsalHub.Services.Data.Notifications
{
    /// <summary>
    /// Defines operations for managing user notifications.
    /// Provides functionality for creating, retrieving, deleting,
    /// and marking notifications as read.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Creates a new notification for a specific user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="message">Notification message.</param>
        /// <param name="url">Optional URL associated with the notification.</param>
        Task CreateAsync(string userId, string message, string? url);

        /// <summary>
        /// Retrieves all notifications for a specific user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Collection of user notifications.</returns>
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);

        /// <summary>
        /// Deletes a notification.
        /// Only the owner of the notification is allowed to perform this operation.
        /// </summary>
        /// <param name="notificationId">Notification identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>True if deletion was successful.</returns>
        Task<bool> DeleteNotificationAsync(int notificationId, string userId);

        /// <summary>
        /// Marks all notifications for a specific user as read.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        Task MarkAllAsReadAsync(string userId);
    }
}
