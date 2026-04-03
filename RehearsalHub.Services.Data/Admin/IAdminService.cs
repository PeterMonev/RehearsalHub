using RehearsalHub.GCommon;
using RehearsalHub.Web.ViewModels.Admin;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Song;

namespace RehearsalHub.Services.Data.Admin
{
    /// <summary>
    /// Defines the contract for admin-specific operations:
    /// dashboard statistics, user role management, band oversight, and song moderation.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Retrieves aggregate statistics for the admin dashboard.
        /// </summary>
        Task<AdminDashboardViewModel> GetDashboardStatsAsync();

        /// <summary>
        /// Retrieves a paginated list of all users with their role information.
        /// </summary>
        /// <param name="page">Current page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="searchTerm">Optional filter by username or email.</param>
        Task<PagedResult<AdminUserViewModel>> GetUsersPagedAsync(
            int page, int pageSize, string? searchTerm = null);

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

        /// <summary>Soft-deletes a user account.</summary>
        Task<bool> DeleteUserAsync(string userId, string currentAdminId);

        /// <summary>
        /// Retrieves a paginated list of all non-deleted bands.
        /// </summary>
        /// <param name="page">Current page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="searchTerm">Optional filter by band name.</param>
        Task<PagedResult<AdminBandViewModel>> GetBandsPagedAsync(
            int page, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Soft-deletes a band by its ID.
        /// Cascades to related rehearsals and setlists via ApplicationDbContext.ApplyAuditInfo.
        /// </summary>
        /// <param name="bandId">The ID of the band to delete.</param>
        /// <returns>True if the deletion succeeded.</returns>
        Task<bool> DeleteBandAsync(int bandId);

        /// <summary>Gets band data for admin editing — no ownership check.</summary>
        Task<BandEditViewModel?> GetBandForEditAsync(int bandId);

        /// <summary>Updates a band — admin override, no ownership check.</summary>
        Task<bool> AdminEditBandAsync(BandEditViewModel model);

        /// <summary>
        /// Retrieves a paginated list of ALL songs in the system (public and private).
        /// Admin bypasses the normal visibility rules.
        /// </summary>
        /// <param name="page">Current page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="searchTerm">Optional filter by title or artist.</param>
        Task<PagedResult<AdminSongViewModel>> GetAllSongsPagedAsync(int page, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Hard-deletes a song by its ID regardless of who created it.
        /// Admin can delete any song in the system.
        /// </summary>
        /// <param name="songId">The ID of the song to delete.</param>
        /// <returns>True if the deletion succeeded.</returns>
        Task<bool> AdminDeleteSongsAsync(int songId);

        /// <summary>Gets song data for admin editing — no ownership check.</summary>
        Task<SongInputModel?> GetSongForAdminEditAsync(int songId);

        /// <summary>Updates any song — admin override, no ownership check.</summary>
        Task<bool> AdminEditSongAsync(SongInputModel model);
    }
}
