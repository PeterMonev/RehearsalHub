using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Web.ViewModels.Rehearsal;
using RehearsalHub.Common.Helpers;

namespace RehearsalHub.Services.Data.Rehearsals
{
    /// <summary>
    /// Handles rehearsal-related business logic.
    /// </summary>
    public class RehearsalService : IRehearsalService
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Creates a new instance of <see cref="RehearsalService"/>.
        /// </summary>
        public RehearsalService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all rehearsals for a band.
        /// </summary>
        public async Task<List<RehearsalIndexViewModel>> GetBandRehearsalsAsync(int bandId, string userId)
        {
            var hasAccess = await dbContext.Bands
                .AnyAsync(b => b.Id == bandId && !b.IsDeleted &&
                    (b.OwnerId == userId || b.Members.Any(m => m.UserId == userId)));

            if (!hasAccess)
            {
                return new List<RehearsalIndexViewModel>();
            }

            var rehearsals = await dbContext.Rehearsals
                .AsNoTracking()
                .Include(r => r.Band)
                .Include(r => r.Setlist)
                .Where(r => r.BandId == bandId && !r.IsDeleted)
                .OrderByDescending(r => r.StartRehearsal)
                .ToListAsync();

            return MapToIndexViewModels(rehearsals);
        }

        /// <summary>
        /// Gets upcoming rehearsals for a band.
        /// </summary>
        public async Task<List<RehearsalIndexViewModel>> GetUpcomingRehearsalsAsync(int bandId, string userId)
        {
            var hasAccess = await dbContext.Bands
                .AnyAsync(b => b.Id == bandId && !b.IsDeleted &&
                    (b.OwnerId == userId || b.Members.Any(m => m.UserId == userId)));

            if (!hasAccess)
            {
                return new List<RehearsalIndexViewModel>();
            }

            var now = DateTime.Now;

            var rehearsals = await dbContext.Rehearsals
                .AsNoTracking()
                .Include(r => r.Band)
                .Include(r => r.Setlist)
                .Where(r => r.BandId == bandId && !r.IsDeleted && r.StartRehearsal > now)
                .OrderBy(r => r.StartRehearsal)
                .ToListAsync();

            return MapToIndexViewModels(rehearsals);
        }

        /// <summary>
        /// Gets past rehearsals for a band.
        /// </summary>
        public async Task<List<RehearsalIndexViewModel>> GetPastRehearsalsAsync(int bandId, string userId)
        {
            var hasAccess = await dbContext.Bands
                .AnyAsync(b => b.Id == bandId && !b.IsDeleted &&
                    (b.OwnerId == userId || b.Members.Any(m => m.UserId == userId)));

            if (!hasAccess)
            {
                return new List<RehearsalIndexViewModel>();
            }

            var now = DateTime.Now;

            var rehearsals = await dbContext.Rehearsals
                .AsNoTracking()
                .Include(r => r.Band)
                .Include(r => r.Setlist)
                .Where(r => r.BandId == bandId && !r.IsDeleted && r.EndRehearsal < now)
                .OrderByDescending(r => r.StartRehearsal)
                .ToListAsync();

            return MapToIndexViewModels(rehearsals);
        }

        /// <summary>
        /// Gets detailed rehearsal information.
        /// </summary>
        public async Task<RehearsalDetailsViewModel?> GetRehearsalDetailsAsync(int id, string userId)
        {
            var rehearsal = await dbContext.Rehearsals
                .AsNoTracking()
                .Include(r => r.Band)
                .Include(r => r.Setlist)
                    .ThenInclude(s => s.SetlistSongs)
                        .ThenInclude(ss => ss.Song)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (rehearsal == null || rehearsal.Setlist == null)
            {
                return null;
            }

            var hasAccess = rehearsal.Band.OwnerId == userId ||
                await dbContext.BandMembers.AnyAsync(bm =>
                    bm.BandId == rehearsal.BandId && bm.UserId == userId);

            if (!hasAccess)
            {
                return null;
            }

            var duration = DateTimeHelper.GetDuration(rehearsal.StartRehearsal, rehearsal.EndRehearsal);

            var songs = rehearsal.Setlist == null
                ? new List<RehearsalSongViewModel>()
                : rehearsal.Setlist.SetlistSongs
                    .Select(ss => new RehearsalSongViewModel
                    {
                        SongId = ss.SongId,
                        Title = ss.Song.Title,
                        Artist = ss.Song.Artist,
                        Duration = ss.Song.Duration,
                        Genre = ss.Song.Genre.ToString(),
                        Tempo = ss.Song.Tempo
                    })
                    .ToList();

            return new RehearsalDetailsViewModel
            {
                Id = rehearsal.Id,
                Name = rehearsal.Name,
                StartRehearsal = rehearsal.StartRehearsal,
                EndRehearsal = rehearsal.EndRehearsal,
                Notes = rehearsal.Notes,
                BandId = rehearsal.BandId,
                BandName = rehearsal.Band.Name,
                SetlistId = rehearsal.SetlistId,
                SetlistName = rehearsal.Setlist?.Name,
                Duration = DateTimeHelper.FormatDuration(duration),
                FormattedDate = DateTimeHelper.FormatDateForDisplay(rehearsal.StartRehearsal),
                FormattedStartTime = DateTimeHelper.FormatTimeForDisplay(rehearsal.StartRehearsal),
                FormattedEndTime = DateTimeHelper.FormatTimeForDisplay(rehearsal.EndRehearsal),
                IsUpcoming = DateTimeHelper.IsUpcoming(rehearsal.StartRehearsal),
                IsHappeningNow = DateTimeHelper.IsHappeningNow(rehearsal.StartRehearsal, rehearsal.EndRehearsal),
                CanEdit = rehearsal.Band.OwnerId == userId,
                CanDelete = rehearsal.Band.OwnerId == userId,
                Songs = songs
            };
        }

        /// <summary>
        /// Gets rehearsal model for creation.
        /// </summary>
        public async Task<RehearsalInputModel?> GetRehearsalForCreateAsync(int bandId, string userId)
        {
            var band = await dbContext.Bands
                .FirstOrDefaultAsync(b => b.Id == bandId && !b.IsDeleted && b.OwnerId == userId);

            if (band == null)
            {
                return null;
            }

            return new RehearsalInputModel
            {
                BandId = bandId,
                BandName = band.Name,
                AvailableSetlists = await GetBandSetlistsAsync(bandId),
                StartRehearsal = DateTime.Now.AddDays(1).Date.AddHours(18),
                EndRehearsal = DateTime.Now.AddDays(1).Date.AddHours(20)
            };
        }

        /// <summary>
        /// Gets rehearsal model for editing.
        /// </summary>
        public async Task<RehearsalInputModel?> GetRehearsalForEditAsync(int id, string userId)
        {
            var rehearsal = await dbContext.Rehearsals
                .Include(r => r.Band)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (rehearsal == null || rehearsal.Band.OwnerId != userId)
            {
                return null;
            }

            return new RehearsalInputModel
            {
                Id = rehearsal.Id,
                Name = rehearsal.Name,
                StartRehearsal = rehearsal.StartRehearsal,
                EndRehearsal = rehearsal.EndRehearsal,
                Notes = rehearsal.Notes,
                BandId = rehearsal.BandId,
                SetlistId = rehearsal.SetlistId,
                BandName = rehearsal.Band.Name,
                AvailableSetlists = await GetBandSetlistsAsync(rehearsal.BandId)
            };
        }

        /// <summary>
        /// Creates new rehearsal.
        /// </summary>
        public async Task<int> CreateRehearsalAsync(RehearsalInputModel model, string userId)
        {
            var band = await dbContext.Bands
                .FirstOrDefaultAsync(b => b.Id == model.BandId && !b.IsDeleted && b.OwnerId == userId);

            if (band == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!ValidateNotInPast(model.StartRehearsal) ||
                !ValidateTimeRange(model.StartRehearsal, model.EndRehearsal))
            {
                throw new InvalidOperationException();
            }

            var rehearsal = new Rehearsal
            {
                Name = model.Name,
                StartRehearsal = model.StartRehearsal,
                EndRehearsal = model.EndRehearsal,
                Notes = model.Notes,
                BandId = model.BandId,
                SetlistId = model.SetlistId
            };

            await dbContext.Rehearsals.AddAsync(rehearsal);
            await dbContext.SaveChangesAsync();

            return rehearsal.Id;
        }

        /// <summary>
        /// Updates rehearsal.
        /// </summary>
        public async Task<bool> UpdateRehearsalAsync(RehearsalInputModel model, string userId)
        {
            var rehearsal = await dbContext.Rehearsals
                .Include(r => r.Band)
                .FirstOrDefaultAsync(r => r.Id == model.Id && !r.IsDeleted);

            if (rehearsal == null || rehearsal.Band.OwnerId != userId)
            {
                return false;
            }

            rehearsal.Name = model.Name;
            rehearsal.StartRehearsal = model.StartRehearsal;
            rehearsal.EndRehearsal = model.EndRehearsal;
            rehearsal.Notes = model.Notes;
            rehearsal.SetlistId = model.SetlistId;

            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Soft deletes rehearsal.
        /// </summary>
        public async Task<bool> DeleteRehearsalAsync(int id, string userId)
        {
            var rehearsal = await dbContext.Rehearsals
                .Include(r => r.Band)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (rehearsal == null || rehearsal.Band.OwnerId != userId)
            {
                return false;
            }

            rehearsal.IsDeleted = true;
            rehearsal.DeletedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if user can edit rehearsal.
        /// </summary>
        public async Task<bool> CanUserEditRehearsalAsync(int rehearsalId, string userId)
        {
            return await dbContext.Rehearsals
                .Include(r => r.Band)
                .AnyAsync(r => r.Id == rehearsalId &&
                               !r.IsDeleted &&
                               r.Band.OwnerId == userId);
        }

        /// <summary>
        /// Validates time range.
        /// </summary>
        public bool ValidateTimeRange(DateTime start, DateTime end)
            => DateTimeHelper.IsValidTimeRange(start, end);

        /// <summary>
        /// Validates not in past.
        /// </summary>
        public bool ValidateNotInPast(DateTime start)
            => DateTimeHelper.IsNotInPast(start);

        /// <summary>
        /// Gets setlists for band.
        /// </summary>
        private async Task<List<SetlistOptionViewModel>> GetBandSetlistsAsync(int bandId)
        {
            return await dbContext.Setlists
                .AsNoTracking()
                .Where(s => s.BandId == bandId && !s.IsDeleted)
                .Select(s => new SetlistOptionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    SongsCount = s.SetlistSongs.Count
                })
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Maps rehearsal entities to index models.
        /// </summary>
        private List<RehearsalIndexViewModel> MapToIndexViewModels(List<Rehearsal> rehearsals)
        {
            return rehearsals.Select(r =>
            {
                var duration = DateTimeHelper.GetDuration(r.StartRehearsal, r.EndRehearsal);

                return new RehearsalIndexViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    BandName = r.Band.Name,
                    SetlistName = r.Setlist?.Name ?? "No Setlist",
                    StartRehearsal = r.StartRehearsal,
                    EndRehearsal = r.EndRehearsal,
                    Duration = DateTimeHelper.FormatDuration(duration),
                    FormattedDate = DateTimeHelper.FormatDateForDisplay(r.StartRehearsal),
                    FormattedTime =
                        $"{DateTimeHelper.FormatTimeForDisplay(r.StartRehearsal)} - {DateTimeHelper.FormatTimeForDisplay(r.EndRehearsal)}",
                    IsUpcoming = DateTimeHelper.IsUpcoming(r.StartRehearsal),
                    IsHappeningNow = DateTimeHelper.IsHappeningNow(r.StartRehearsal, r.EndRehearsal)
                };
            }).ToList();
        }

        /// <summary>
        /// Gets upcoming rehearsals for user.
        /// </summary>
        public async Task<List<RehearsalIndexViewModel>> GetAllUpcomingForUserAsync(string userId)
        {
            var now = DateTime.Now;

            var rehearsals = await dbContext.Rehearsals
                .AsNoTracking()
                .Include(r => r.Band)
                .Include(r => r.Setlist)
                .Where(r => !r.IsDeleted &&
                            r.StartRehearsal >= now &&
                            (r.Band.OwnerId == userId ||
                             r.Band.Members.Any(m => m.UserId == userId)))
                .OrderBy(r => r.StartRehearsal)
                .ToListAsync();

            return MapToIndexViewModels(rehearsals);
        }
    }
}
