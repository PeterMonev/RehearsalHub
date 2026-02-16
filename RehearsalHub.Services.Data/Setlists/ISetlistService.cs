using RehearsalHub.Web.ViewModels.Setlist;

namespace RehearsalHub.Services.Data.Setlists
{
    public interface ISetlistService
    {
        /// <summary>
        /// Creates a new setlist for a band
        /// </summary>
        Task<int> CreateSetlistAsync(SetlistInputModel model, string userId);

        /// <summary>
        /// Gets detailed information about a setlist
        /// </summary>
        Task<SetlistDetailsViewModel?> GetSetlistDetailsAsync(int id, string userId);

        /// <summary>
        /// Gets a setlist for editing
        /// </summary>
        Task<SetlistInputModel?> GetSetlistForEditAsync(int id, string userId);

        /// <summary>
        /// Updates an existing setlist
        /// </summary>
        Task<bool> UpdateSetlistAsync(SetlistInputModel model, string userId);

        /// <summary>
        /// Deletes a setlist (only band owner can delete)
        /// </summary>
        Task<bool> DeleteSetlistAsync(int id, string userId);

        /// <summary>
        /// Gets available songs to add to setlist
        /// </summary>
        Task<AddSongToSetlistViewModel?> GetAvailableSongsAsync(int setlistId, string userId);

        /// <summary>
        /// Adds songs to a setlist
        /// </summary>
        Task<bool> AddSongsToSetlistAsync(int setlistId, List<int> songIds, string userId);

        /// <summary>
        /// Removes a song from a setlist
        /// </summary>
        Task<bool> RemoveSongFromSetlistAsync(int setlistId, int songId, string userId);

        /// <summary>
        /// Checks if user can edit setlist (is band owner)
        /// </summary>
        Task<bool> CanUserEditSetlistAsync(int setlistId, string userId);
    }
}