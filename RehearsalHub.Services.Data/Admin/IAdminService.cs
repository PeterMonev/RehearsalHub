using Microsoft.AspNetCore.Mvc.RazorPages;
using RehearsalHub.GCommon;
using RehearsalHub.Web.ViewModels.Admin;

namespace RehearsalHub.Areas.Admin.Data
{
    /// <summary>
    /// Defines the contract for admin-specific operations:
    /// dashboard statistics, user role management, and band oversight.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Retrieves aggregate statistics for the admin dashboard.
        /// </summary>
        public Task<AdminDashboardViewModel> GetDashboardStatsAsync();

        /// <summary>
        /// Retrieves a paginated list of all users with their role information.
        /// </summary>
        /// <param name="page">Current page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="searchTerm">Optional search term to filter by username or email.</param>
        Task<PagedResult<AdminUserViewModel>> GetUsersPagedAsync(int page, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Retrieves a paginated list of all non-deleted bands.
        /// </summary>
        /// <param name="page">Current page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="searchTerm">Optional search term to filter by band name.</param>
        Task<PagedResult<AdminBandViewModel>> GetBandsPagedAsync(int page, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Promotes a user to the Admin role.
        /// </summary>
        /// <param name="userId">The ID of the user to promote.</param>
        /// <returns>True if the operation succeeded.</returns>
        Task<bool> PromoteUserAsync(string userId);

        /// <summary>
        /// Removes the Admin role from a user.
        /// Guards against self-demotion via <paramref name="currentAdminId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user to demote.</param>
        /// <param name="currentAdminId">The ID of the currently logged-in admin.</param>
        /// <returns>True if the operation succeeded.</returns>
        Task<bool> DemoteUserAsync(string userId, string currentAdminId);
    }
}
