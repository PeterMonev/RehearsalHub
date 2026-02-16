using RehearsalHub.Web.ViewModels.User;

namespace RehearsalHub.Services.Data.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves profile information for a specific user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User profile view model or null if user is not found.</returns>
        Task<UserProfileViewModel?> GetUserProfileAsync(string userId);
    }
}
