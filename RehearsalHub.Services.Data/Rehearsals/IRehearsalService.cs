using RehearsalHub.Web.ViewModels.Rehearsal;

namespace RehearsalHub.Services.Data.Rehearsals
{
    /// <summary>
    /// Defines business logic operations related to rehearsals.
    /// </summary>
    public interface IRehearsalService
    {
        /// <summary>
        /// Gets all rehearsals for a specific band accessible by the given user.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Collection of rehearsal index view models.</returns>
        Task<List<RehearsalIndexViewModel>> GetBandRehearsalsAsync(int bandId, string userId);

        /// <summary>
        /// Gets all upcoming rehearsals for a specific band.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Collection of upcoming rehearsals.</returns>
        Task<List<RehearsalIndexViewModel>> GetUpcomingRehearsalsAsync(int bandId, string userId);

        /// <summary>
        /// Gets all past rehearsals for a specific band.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Collection of past rehearsals.</returns>
        Task<List<RehearsalIndexViewModel>> GetPastRehearsalsAsync(int bandId, string userId);

        /// <summary>
        /// Gets detailed information for a rehearsal.
        /// </summary>
        /// <param name="id">The rehearsal identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Rehearsal details view model or null if not found or access denied.</returns>
        Task<RehearsalDetailsViewModel?> GetRehearsalDetailsAsync(int id, string userId);

        /// <summary>
        /// Gets rehearsal data for editing.
        /// </summary>
        /// <param name="id">The rehearsal identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Rehearsal input model or null if not found or access denied.</returns>
        Task<RehearsalInputModel?> GetRehearsalForEditAsync(int id, string userId);

        /// <summary>
        /// Prepares a model for creating a rehearsal.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Initialized rehearsal input model.</returns>
        Task<RehearsalInputModel?> GetRehearsalForCreateAsync(int bandId, string userId);

        /// <summary>
        /// Creates a new rehearsal.
        /// </summary>
        /// <param name="model">The rehearsal input model.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>The identifier of the created rehearsal.</returns>
        Task<int> CreateRehearsalAsync(RehearsalInputModel model, string userId);

        /// <summary>
        /// Updates an existing rehearsal.
        /// </summary>
        /// <param name="model">The rehearsal input model.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>True if update was successful.</returns>
        Task<bool> UpdateRehearsalAsync(RehearsalInputModel model, string userId);

        /// <summary>
        /// Deletes a rehearsal.
        /// </summary>
        /// <param name="id">The rehearsal identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>True if deletion was successful.</returns>
        Task<bool> DeleteRehearsalAsync(int id, string userId);

        /// <summary>
        /// Determines whether a user is allowed to edit a rehearsal.
        /// </summary>
        /// <param name="rehearsalId">The rehearsal identifier.</param>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>True if the user has edit permissions.</returns>
        Task<bool> CanUserEditRehearsalAsync(int rehearsalId, string userId);

        /// <summary>
        /// Gets all upcoming rehearsals for all bands the user belongs to.
        /// </summary>
        /// <param name="userId">The current user identifier.</param>
        /// <returns>Collection of upcoming rehearsals.</returns>
        Task<List<RehearsalIndexViewModel>> GetAllUpcomingForUserAsync(string userId);

        /// <summary>
        /// Validates that the end time is after the start time.
        /// </summary>
        /// <param name="start">Rehearsal start time.</param>
        /// <param name="end">Rehearsal end time.</param>
        /// <returns>True if the time range is valid.</returns>
        bool ValidateTimeRange(DateTime start, DateTime end);

        /// <summary>
        /// Validates that the rehearsal is not scheduled in the past.
        /// </summary>
        /// <param name="start">Rehearsal start time.</param>
        /// <returns>True if the start time is in the future.</returns>
        bool ValidateNotInPast(DateTime start);
    }
}
