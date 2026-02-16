using RehearsalHub.Web.ViewModels.Song;

/// <summary>
/// Defines operations for managing songs in the system.
/// Provides methods for creating, retrieving, updating, deleting,
/// and browsing songs with filtering and pagination.
/// </summary>
public interface ISongService
{
    /// <summary>
    /// Creates a new song owned by the current user or their band.
    /// </summary>
    /// <param name="model">Song input data.</param>
    /// <param name="userId">Current user identifier.</param>
    /// <returns>The ID of the newly created song.</returns>
    Task<int> CreateSongAsync(SongInputModel model, string userId);

    /// <summary>
    /// Retrieves a paged collection of songs with optional filtering.
    /// </summary>
    /// <param name="userId">Current user identifier.</param>
    /// <param name="page">Current page number.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="searchTerm">Optional search term.</param>
    /// <param name="bandId">Optional band filter.</param>
    /// <param name="genre">Optional genre filter.</param>
    /// <param name="key">Optional musical key filter.</param>
    /// <param name="tempo">Optional tempo filter.</param>
    /// <returns>Paged collection of songs.</returns>
    Task<SongPagedViewModel> GetSongsPagedAsync(
        string userId,
        int page,
        int pageSize,
        string? searchTerm = null,
        int? bandId = null,
        string? genre = null,
        string? key = null,
        string? tempo = null);

    /// <summary>
    /// Retrieves detailed information about a song.
    /// </summary>
    /// <param name="id">Song identifier.</param>
    /// <returns>Song details view model or null if not found.</returns>
    Task<SongDetailsViewModel?> GetSongDetailsAsync(int id);

    /// <summary>
    /// Deletes a song.
    /// Only the song owner is allowed to perform this operation.
    /// </summary>
    /// <param name="songId">Song identifier.</param>
    /// <param name="userId">Current user identifier.</param>
    /// <returns>True if deletion was successful.</returns>
    Task<bool> DeleteSongAsync(int songId, string userId);

    /// <summary>
    /// Retrieves song data for editing.
    /// Only the song owner is allowed to edit the song.
    /// </summary>
    /// <param name="id">Song identifier.</param>
    /// <param name="userId">Current user identifier.</param>
    /// <returns>Song input model or null if user is not authorized.</returns>
    Task<SongInputModel?> GetSongForEditAsync(int id, string userId);

    /// <summary>
    /// Updates an existing song.
    /// Only the song owner is allowed to perform updates.
    /// </summary>
    /// <param name="model">Updated song data.</param>
    /// <param name="userId">Current user identifier.</param>
    /// <returns>True if update was successful.</returns>
    Task<bool> UpdateSongAsync(SongInputModel model, string userId);
}
