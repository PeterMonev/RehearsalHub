namespace RehearsalHub.Services.Data.Songs
{
    using Microsoft.EntityFrameworkCore;
    using RehearsalHub.Data;
    using RehearsalHub.Data.Models;
    using RehearsalHub.Data.Models.Enums;
    using RehearsalHub.Web.ViewModels.Song;

    /// <summary>
    /// Service handling business logic for song management, including CRUD operations and pagination.
    /// </summary>
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext dbContext;

        public SongService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new song entry in the database.
        /// </summary>
        /// <param name="model">The input model containing song details.</param>
        /// <param name="userId">The ID of the user creating the song.</param>
        /// <returns>The ID of the newly created song.</returns>
        public async Task<int> CreateSongAsync(SongInputModel model, string userId)
        {
            var song = new Song
            {
                Artist = model.Artist,
                Title = model.Title,
                Duration = model.Duration,
                Genre = model.Genre,
                MusicalKey = model.MusicalKey,
                Tempo = model.Tempo,
                IsPrivate = model.IsPrivate,
                OwnerBandId = model.BandId,
                CreatorId = userId
            };

            await dbContext.Songs.AddAsync(song);
            await dbContext.SaveChangesAsync();

            return song.Id;
        }

        /// <summary>
        /// Retrieves a paginated list of songs based on visibility, band ownership, and search criteria.
        /// </summary>
        /// <param name="userId">The ID of the current user.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="searchTerm">Optional search string to filter by title or artist.</param>
        /// <param name="bandId">Optional filter to show songs belonging to a specific band.</param>
        /// <returns>A view model containing the paginated songs and metadata.</returns>
        /// <summary>
        /// Retrieves a paginated list of songs based on visibility, band ownership, and search criteria.
        /// </summary>
        /// <param name="userId">The ID of the current user.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="searchTerm">Optional search string to filter by title or artist.</param>
        /// <param name="bandId">Optional filter to show songs belonging to a specific band.</param>
        /// <param name="genre">Optional filter by genre.</param>
        /// <param name="key">Optional filter by musical key.</param>
        /// <param name="tempo">Optional filter by tempo range.</param>
        /// <returns>A view model containing the paginated songs and metadata.</returns>
        public async Task<SongPagedViewModel> GetSongsPagedAsync(
            string userId,
            int page,
            int pageSize,
            string? searchTerm = null,
            int? bandId = null,
            string? genre = null,
            string? key = null,
            string? tempo = null)
        {
            IQueryable<Song> query = dbContext.Songs.AsNoTracking();

            query = query.Where(s =>
                !s.IsPrivate ||
                s.CreatorId == userId ||
                (s.OwnerBand != null && s.OwnerBand.OwnerId == userId)
            );

            if (bandId.HasValue)
            {
                query = query.Where(s => s.OwnerBandId == bandId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string wildcard = $"%{searchTerm.Trim()}%";
                query = query.Where(s =>
                    EF.Functions.Like(s.Title, wildcard) ||
                    EF.Functions.Like(s.Artist, wildcard));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                if (Enum.TryParse<MusicGenre>(genre, true, out var genreEnum))
                {
                    query = query.Where(s => s.Genre == genreEnum);
                }
            }

            if (!string.IsNullOrWhiteSpace(key))
            {
                if (Enum.TryParse<MusicalKey>(key, true, out var keyEnum))
                {
                    query = query.Where(s => s.MusicalKey == keyEnum);
                }
            }

            if (!string.IsNullOrWhiteSpace(tempo))
            {
                query = tempo.ToLower() switch
                {
                    "slow" => query.Where(s => s.Tempo < 80),
                    "medium" => query.Where(s => s.Tempo >= 80 && s.Tempo <= 120),
                    "fast" => query.Where(s => s.Tempo > 120),
                    _ => query
                };
            }

            int totalSongs = await query.CountAsync();

            var songs = await query
                .OrderByDescending(s => s.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SongIndexViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    Duration = s.Duration,
                    Genre = s.Genre.ToString(),
                    MusicalKey = s.MusicalKey.ToString(),
                    Tempo = s.Tempo,
                    IsPrivate = s.IsPrivate,
                    CanEdit =
                        s.CreatorId == userId ||
                        (s.OwnerBand != null && s.OwnerBand.OwnerId == userId)
                })
                .ToListAsync();

            return new SongPagedViewModel
            {
                Songs = songs,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalSongs / pageSize),
                SearchTerm = searchTerm,
                BandId = bandId,
                Genre = genre,
                Key = key,
                Tempo = tempo
            };
        }

        /// <summary>
        /// Gets detailed information for a specific song, including its presence in setlists.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <returns>A detailed view model or null if not found.</returns>
        public async Task<SongDetailsViewModel?> GetSongDetailsAsync(int id)
        {
            return await dbContext.Songs
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new SongDetailsViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    Genre = s.Genre.ToString(),
                    MusicalKey = s.MusicalKey.ToString(),
                    Duration = s.Duration,
                    Tempo = s.Tempo,
                    IsPrivate = s.IsPrivate,
                    OwnerBandId = s.OwnerBandId,
                    OwnerBandName = s.OwnerBand != null ? s.OwnerBand.Name : null,
                    CreatorId = s.CreatorId, 
                    IncludedInSetlists = s.SetlistSongs
                        .Where(ss => !ss.Setlist.IsDeleted)
                        .Select(ss => new SongSetlistViewModel
                        {
                            Id = ss.SetlistId,
                            Name = ss.Setlist.Name,
                            BandName = ss.Setlist.Band.Name
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Deletes a song if the user has the necessary permissions (is owner of the band associated with a private song).
        /// </summary>
        /// <param name="songId">The ID of the song to delete.</param>
        /// <param name="userId">The ID of the user requesting deletion.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteSongAsync(int songId, string userId)
        {
            var song = await dbContext.Songs
                .Include(s => s.OwnerBand)
                .FirstOrDefaultAsync(s => s.Id == songId);

            if (song == null) return false;

            if (song.CreatorId != userId &&
                song.OwnerBand?.OwnerId != userId)
            {
                return false;
            }

            dbContext.Songs.Remove(song);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves a song and maps it to SongInputModel for editing.
        /// </summary>
        public async Task<SongInputModel?> GetSongForEditAsync(int id, string userId)
        {
            var song = await dbContext.Songs
                .Include(s => s.OwnerBand)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (song == null) return null;

            if (!song.IsPrivate)
            {
                    return null;
            }

            return new SongInputModel
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Duration = song.Duration,
                Genre = song.Genre,
                MusicalKey = song.MusicalKey,
                Tempo = song.Tempo,
                IsPrivate = song.IsPrivate,
                BandId = song.OwnerBandId
            };
        }

        /// <summary>
        /// Updates an existing song in the database.
        /// </summary>
        public async Task<bool> UpdateSongAsync(SongInputModel model, string userId)
        {
            var song = await dbContext.Songs
                .Include(s => s.OwnerBand)
                .FirstOrDefaultAsync(s => s.Id == model.Id);

            if (song == null) return false;

            if (song.CreatorId != userId &&
                song.OwnerBand?.OwnerId != userId)
            {
                return false;
            }

            song.Title = model.Title;
            song.Artist = model.Artist;
            song.Duration = model.Duration;
            song.Genre = model.Genre;
            song.MusicalKey = model.MusicalKey;
            song.Tempo = model.Tempo;
            song.IsPrivate = model.IsPrivate;
            song.OwnerBandId = model.BandId;

            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}