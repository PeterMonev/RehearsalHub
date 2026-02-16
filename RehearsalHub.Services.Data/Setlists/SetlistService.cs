using Microsoft.EntityFrameworkCore;
using RehearsalHub.Common.Helpers;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Web.ViewModels.Setlist;

namespace RehearsalHub.Services.Data.Setlists
{
    public class SetlistService : ISetlistService
    {
        private readonly ApplicationDbContext dbContext;

        public SetlistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new setlist for a specific band.
        /// Only the band owner is allowed to create setlists.
        /// </summary>
        /// <param name="model">Setlist input data.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>The ID of the newly created setlist.</returns>
        public async Task<int> CreateSetlistAsync(SetlistInputModel model, string userId)
        {
            var band = await dbContext.Bands
                .FirstOrDefaultAsync(b => b.Id == model.BandId && b.OwnerId == userId);

            if (band == null)
            {
                throw new UnauthorizedAccessException("Only band owner can create setlists");
            }

            var setlist = new Setlist
            {
                Name = model.Name,
                BandId = model.BandId,
                RehearsalDate = model.RehearsalDate
            };

            await dbContext.Setlists.AddAsync(setlist);
            await dbContext.SaveChangesAsync();

            return setlist.Id;
        }

        /// <summary>
        /// Retrieves detailed information about a setlist including songs and band data.
        /// Band owners and band members are allowed to access the details.
        /// </summary>
        /// <param name="id">Setlist identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>Setlist details view model or null if access is denied.</returns>
        public async Task<SetlistDetailsViewModel?> GetSetlistDetailsAsync(int id, string userId)
        {
            var setlist = await dbContext.Setlists
                .AsNoTracking()
                .Include(s => s.Band)
                .Include(s => s.SetlistSongs)
                    .ThenInclude(ss => ss.Song)
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (setlist == null) return null;

            var isBandMember = await dbContext.BandMembers
                .AnyAsync(bm => bm.BandId == setlist.BandId && bm.UserId == userId);

            var isBandOwner = setlist.Band.OwnerId == userId;

            if (!isBandOwner && !isBandMember)
            {
                return null;
            }

            var songs = setlist.SetlistSongs
                .Select(ss => new SetlistSongViewModel
                {
                    SongId = ss.SongId,
                    Title = ss.Song.Title,
                    Artist = ss.Song.Artist,
                    Duration = ss.Song.Duration,
                    Genre = ss.Song.Genre.ToString(),
                    MusicalKey = ss.Song.MusicalKey.ToString(),
                    Tempo = ss.Song.Tempo
                })
                .ToList();

         
            var durations = songs.Select(s => s.Duration);
            var totalDuration = MusicHelper.CalculateTotalDuration(durations);

            return new SetlistDetailsViewModel
            {
                Id = setlist.Id,
                Name = setlist.Name,
                BandId = setlist.BandId,
                BandName = setlist.Band.Name,
                RehearsalDate = setlist.RehearsalDate,
                CreatedOn = setlist.CreatedOn,
                CanEdit = isBandOwner,
                CanAddSongs = isBandOwner,
                Songs = songs,
                TotalDuration = totalDuration  
            };
        }

        /// <summary>
        /// Retrieves setlist data for editing.
        /// Only the band owner can edit a setlist.
        /// </summary>
        /// <param name="id">Setlist identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>Setlist input model or null if user is not authorized.</returns>
        public async Task<SetlistInputModel?> GetSetlistForEditAsync(int id, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (setlist == null) return null;

            if (setlist.Band.OwnerId != userId)
            {
                return null;
            }

            return new SetlistInputModel
            {
                Id = setlist.Id,
                Name = setlist.Name,
                BandId = setlist.BandId,
                RehearsalDate = setlist.RehearsalDate,
                BandName = setlist.Band.Name
            };
        }

        /// <summary>
        /// Updates an existing setlist.
        /// Only the band owner is allowed to perform updates.
        /// </summary>
        /// <param name="model">Updated setlist data.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>True if update was successful; otherwise false.</returns>
        public async Task<bool> UpdateSetlistAsync(SetlistInputModel model, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .FirstOrDefaultAsync(s => s.Id == model.Id && !s.IsDeleted);

            if (setlist == null) return false;

            if (setlist.Band.OwnerId != userId)
            {
                return false;
            }

            setlist.Name = model.Name;
            setlist.RehearsalDate = model.RehearsalDate;

            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Soft deletes a setlist.
        /// Only the band owner is allowed to delete a setlist.
        /// </summary>
        /// <param name="id">Setlist identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteSetlistAsync(int id, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (setlist == null) return false;

            if (setlist.Band.OwnerId != userId)
            {
                return false;
            }

            setlist.IsDeleted = true;
            setlist.DeletedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets all available songs that can be added to a setlist.
        /// Excludes songs already added and applies tempo categorization.
        /// Only the band owner is allowed to perform this operation.
        /// </summary>
        /// <param name="setlistId">Setlist identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>View model containing available songs.</returns>
        public async Task<AddSongToSetlistViewModel?> GetAvailableSongsAsync(int setlistId, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .Include(s => s.SetlistSongs)
                .Where(s => s.Id == setlistId && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (setlist == null) return null;

            if (setlist.Band.OwnerId != userId)
            {
                return null;
            }

            var existingSongIds = setlist.SetlistSongs.Select(ss => ss.SongId).ToHashSet();

            var availableSongs = await dbContext.Songs
                .AsNoTracking()
                .Where(s => !s.IsDeleted &&
                    (s.OwnerBandId == setlist.BandId || !s.IsPrivate))
                .OrderBy(s => s.Title)
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Artist,
                    s.Duration,
                    s.Genre,
                    s.Tempo,
                    IsAlreadyInSetlist = existingSongIds.Contains(s.Id)
                })
                .ToListAsync();

            var songs = availableSongs.Select(s => new AvailableSongViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Artist = s.Artist,
                Duration = s.Duration,
                Genre = s.Genre.ToString(),
                Tempo = s.Tempo,
                IsAlreadyInSetlist = s.IsAlreadyInSetlist,
            }).ToList();

            return new AddSongToSetlistViewModel
            {
                SetlistId = setlistId,
                SetlistName = setlist.Name,
                BandId = setlist.BandId,
                AvailableSongs = songs
            };
        }

        /// <summary>
        /// Adds multiple songs to a setlist.
        /// Skips songs that are already added.
        /// Only the band owner is allowed to perform this operation.
        /// </summary>
        /// <param name="setlistId">Setlist identifier.</param>
        /// <param name="songIds">Collection of song identifiers.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>True if songs were added successfully.</returns>
        public async Task<bool> AddSongsToSetlistAsync(int setlistId, List<int> songIds, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .Include(s => s.SetlistSongs)
                .FirstOrDefaultAsync(s => s.Id == setlistId && !s.IsDeleted);

            if (setlist == null) return false;

            if (setlist.Band.OwnerId != userId)
            {
                return false;
            }

            var existingSongIds = setlist.SetlistSongs.Select(ss => ss.SongId).ToHashSet();

            foreach (var songId in songIds)
            {
                if (existingSongIds.Contains(songId))
                    continue;

                var song = await dbContext.Songs
                    .FirstOrDefaultAsync(s => s.Id == songId && !s.IsDeleted &&
                        (s.OwnerBandId == setlist.BandId || !s.IsPrivate));

                if (song != null)
                {
                    var setlistSong = new SetlistSong
                    {
                        SetlistId = setlistId,
                        SongId = songId
                    };

                    await dbContext.SetlistSongs.AddAsync(setlistSong);
                }
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Removes a song from a setlist.
        /// Only the band owner is allowed to perform this operation.
        /// </summary>
        /// <param name="setlistId">Setlist identifier.</param>
        /// <param name="songId">Song identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>True if removal was successful.</returns>
        public async Task<bool> RemoveSongFromSetlistAsync(int setlistId, int songId, string userId)
        {
            var setlist = await dbContext.Setlists
                .Include(s => s.Band)
                .FirstOrDefaultAsync(s => s.Id == setlistId && !s.IsDeleted);

            if (setlist == null) return false;

            if (setlist.Band.OwnerId != userId)
            {
                return false;
            }

            var setlistSong = await dbContext.SetlistSongs
                .FirstOrDefaultAsync(ss => ss.SetlistId == setlistId && ss.SongId == songId);

            if (setlistSong == null) return false;

            dbContext.SetlistSongs.Remove(setlistSong);
            await dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Checks whether the current user can edit a specific setlist.
        /// Only band owners are allowed to edit setlists.
        /// </summary>
        /// <param name="setlistId">Setlist identifier.</param>
        /// <param name="userId">Current user identifier.</param>
        /// <returns>True if user is allowed to edit the setlist.</returns>
        public async Task<bool> CanUserEditSetlistAsync(int setlistId, string userId)
        {
            return await dbContext.Setlists
                .Include(s => s.Band)
                .AnyAsync(s => s.Id == setlistId &&
                          !s.IsDeleted &&
                          s.Band.OwnerId == userId);
        }
    }
}