using RehearsalHub.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Notifications
{
    public interface INotificationService
    {
        Task CreateAsync(string userId, string message, string? url);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);

        Task<bool> DeleteNotificationAsync(int notificationId, string userId);

        Task MarkAllAsReadAsync(string userId);
    }
}
