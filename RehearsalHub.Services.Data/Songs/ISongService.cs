using RehearsalHub.Web.ViewModels.Song;

public interface ISongService
{
    Task<int> CreateSongAsync(SongInputModel model, string userId);

    Task<SongPagedViewModel> GetSongsPagedAsync(
        string userId,
        int page,
        int pageSize,
        string? searchTerm = null,
        int? bandId = null,
        string? genre = null,
        string? key = null,
        string? tempo = null);

    Task<SongDetailsViewModel?> GetSongDetailsAsync(int id);
    Task<bool> DeleteSongAsync(int songId, string userId);
    Task<SongInputModel?> GetSongForEditAsync(int id, string userId);
    Task<bool> UpdateSongAsync(SongInputModel model, string userId);
}