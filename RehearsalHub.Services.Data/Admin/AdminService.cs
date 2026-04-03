using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.GCommon;
using RehearsalHub.Services.Data.Admin;
using RehearsalHub.Web.ViewModels.Admin;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Song;

namespace RehearsalHub.Areas.Admin.Data
{
    /// <summary>
    /// Service for admin-specific operations: dashboard stats,
    /// paginated user/band management, and role assignment.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<AdminService> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<AdminService> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves aggregate counts for the admin dashboard.
        /// </summary>
        public async Task<AdminDashboardViewModel> GetDashboardStatsAsync()
        {
            var now = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            AdminDashboardViewModel? stats = new AdminDashboardViewModel
            {
                TotalUsers = await dbContext.Users.CountAsync(u => !u.IsDeleted),
                TotalBands = await dbContext.Bands.CountAsync(b => !b.IsDeleted),
                TotalSongs = await dbContext.Songs.CountAsync(s => !s.IsDeleted),
                TotalRehearsals = await dbContext.Rehearsals.CountAsync(r => !r.IsDeleted),
                TotalSetlists = await dbContext.Setlists.CountAsync(s => !s.IsDeleted),
                NewUsersThisMonth = await dbContext.Users.CountAsync(u =>
                    !u.IsDeleted && u.CreatedOn >= firstDayOfMonth),
                ActiveBands = await dbContext.Bands.CountAsync(b =>
                    !b.IsDeleted &&
                    b.Rehearsals.Any(r => !r.IsDeleted && r.StartRehearsal >= now))
            };

            logger.LogInformation("Admin dashboard stats loaded — {Users} users, {Bands} bands, {Songs} songs",
                stats.TotalUsers, stats.TotalBands, stats.TotalSongs);

            return stats;
        }

        /// <summary>
        /// Returns paginated users with optional search and admin role check.
        /// </summary>
        public async Task<PagedResult<AdminUserViewModel>> GetUsersPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var query = dbContext.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(u =>
                    u.UserName!.ToLower().Contains(term) ||
                    u.Email!.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();

            var user = await query.OrderBy(u => u.UserName).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.IsDeleted,
                    u.CreatedOn,
                    BandCount = u.BandMembers.Count(bm => bm.IsConfirmed)
                })
                .ToListAsync();

            List<AdminUserViewModel> result = new List<AdminUserViewModel>();

            foreach (var u in user)
            {
                var appUser = await userManager.FindByIdAsync(u.Id);
                var isAdmin = appUser != null && await userManager.IsInRoleAsync(appUser, "Admin");

                result.Add(new AdminUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName!,
                    Email = u.Email!,
                    BandCount = u.BandCount,
                    IsAdmin = isAdmin,
                    IsDeleted = u.IsDeleted,
                    CreatedOn = u.CreatedOn
                });
            }

            logger.LogInformation("Admin loaded {Count} users (page {Page})", result.Count, page);
            return new PagedResult<AdminUserViewModel>(result, totalCount, page, pageSize);
        }

        /// <summary>
        /// Returns a paginated list of bands with optional search.
        /// </summary>
        public async Task<PagedResult<AdminBandViewModel>> GetBandsPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var query = dbContext.Bands.AsNoTracking().Where(b => !b.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();

            List<AdminBandViewModel> bands = await query.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(b => new AdminBandViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.ToString(),
                    OwnerName = b.Owner.UserName!,
                    MemberCount = b.Members.Count(bm => bm.IsConfirmed),
                    RehearsalCount = b.Rehearsals.Count(r => !r.IsDeleted),
                    CreatedOn = b.CreatedOn
                })
                .ToListAsync();

            logger.LogInformation("Admin loaded {Count} bands (page {Page})", bands.Count, page);
            return new PagedResult<AdminBandViewModel>(bands, totalCount, page, pageSize);
        }

        /// <summary>
        /// Promotes a user to Admin role if not already assigned.
        /// </summary>
        public async Task<bool> PromoteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("PromoteUserAsync called with null or empty userId");
                return false;
            }

            ApplicationUser user = await dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                logger.LogWarning("PromoteUserAsync: user {UserId} not found", userId);
                return false;
            }

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                logger.LogInformation("User {UserId} is already Admin", userId);
                return true;
            }

            var result = await userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
                logger.LogInformation("User {UserId} promoted to Admin", userId);
            else
                logger.LogWarning("Failed to promote user {UserId}: {Errors}",
                    userId, string.Join(", ", result.Errors.Select(e => e.Description)));

            return result.Succeeded;
        }

        /// <summary>
        /// Removes Admin role from a user.
        /// </summary>
        public async Task<bool> DemoteUserAsync(string userId, string currentAdminId)
        {
            if (userId == currentAdminId)
            {
                logger.LogWarning("Admin {UserId} attempted to demote themselves", userId);
                return false;
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogWarning("DemoteUserAsync: user {UserId} not found", userId);
                return false;
            }

            var result = await userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded)
                logger.LogInformation("User {UserId} demoted from Admin", userId);
            else
                logger.LogWarning("Failed to demote user {UserId}: {Errors}",
                    userId, string.Join(", ", result.Errors.Select(e => e.Description)));

            return result.Succeeded;
        }

        /// <summary>
        /// Soft deletes a band.
        /// </summary>
        public async Task<bool> DeleteBandAsync(int bandId)
        {
            try
            {
                var band = await dbContext.Bands.FindAsync(bandId);

                if (band == null || band.IsDeleted)
                {
                    logger.LogWarning("DeleteBandAsync: band {BandId} not found or already deleted", bandId);
                    return false;
                }

                dbContext.Bands.Remove(band);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Band {BandId} soft-deleted by admin", bandId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting band {BandId}", bandId);
                return false;
            }
        }

        /// <summary>
        /// Returns all songs (admin bypasses visibility rules).
        /// </summary>
        public async Task<PagedResult<AdminSongViewModel>> GetAllSongsPagedAsync(
            int page, int pageSize, string? searchTerm = null)
        {
            var query = dbContext.Songs
                .Where(s => !s.IsDeleted)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(s =>
                    s.Title.ToLower().Contains(term) ||
                    s.Artist.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();

            var songs = await query
                .OrderByDescending(s => s.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new AdminSongViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    Duration = s.Duration,
                    Genre = s.Genre.ToString(),
                    MusicalKey = s.MusicalKey.ToString(),
                    Tempo = s.Tempo,
                    IsPrivate = s.IsPrivate,
                    CreatorName = s.Creator.UserName!,
                    OwnerBandName = s.OwnerBand != null ? s.OwnerBand.Name : null,
                    CreatedOn = s.CreatedOn
                })
                .ToListAsync();

            logger.LogInformation("Admin loaded {Count} songs (page {Page})", songs.Count, page);
            return new PagedResult<AdminSongViewModel>(songs, totalCount, page, pageSize);
        }

        /// <summary>
        /// Hard deletes a song.
        /// </summary>
        public async Task<bool> AdminDeleteSongsAsync(int songId)
        {
            try
            {
                var song = await dbContext.Songs.FindAsync(songId);

                if (song == null || song.IsDeleted)
                {
                    logger.LogWarning("AdminDeleteSongAsync: song {SongId} not found or already deleted", songId);
                    return false;
                }

                dbContext.Songs.Remove(song);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Song {SongId} deleted by admin", songId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting song {SongId}", songId);
                return false;
            }
        }

        /// <summary>
        /// Soft deletes a user.
        /// </summary>
        public async Task<bool> DeleteUserAsync(string userId, string currentAdminId)
        {
            if (userId == currentAdminId)
            {
                logger.LogWarning("Admin {UserId} attempted to delete themselves", userId);
                return false;
            }

            try
            {
                var user = await dbContext.Users.FindAsync(userId);

                if (user == null || user.IsDeleted)
                {
                    return false;
                }

                user.IsDeleted = true;
                user.DeletedOn = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();

                logger.LogInformation("User {UserId} soft-deleted by admin", userId);
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user {UserId}", userId);
                return false;
            }
        }

        /// <summary>
        /// Gets band data for editing.
        /// </summary>
        public async Task<BandEditViewModel?> GetBandForEditAsync(int bandId)
        {
            if (bandId <= 0)
            {
                logger.LogWarning("Invalid bandId: {BandId}", bandId);
                return null;
            }

            return await dbContext.Bands.AsNoTracking().Where(b => b.Id == bandId && !b.IsDeleted)
                .Select(b => new BandEditViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre,
                    ImageUrl = b.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Edits band data.
        /// </summary>
        public async Task<bool> AdminEditBandAsync(BandEditViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                var band = await dbContext.Bands.FirstOrDefaultAsync(b => b.Id == model.Id && !b.IsDeleted);

                if (band == null) return false;

                band.Name = model.Name;
                band.Genre = model.Genre;
                band.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                    ? band.ImageUrl
                    : model.ImageUrl;

                await dbContext.SaveChangesAsync();
                logger.LogInformation("Band {BandId} edited by admin", model.Id);
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing band {BandId}", model.Id);
                return false;
            }
        }

        /// <summary>
        /// Gets song data for editing.
        /// </summary>
        public async Task<SongInputModel?> GetSongForAdminEditAsync(int songId)
        {
            if (songId <= 0)
            {
                logger.LogWarning("Invalid songId: {SongId}", songId);
                return null;
            }

            try
            {
                var song = await dbContext.Songs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == songId && !s.IsDeleted);

                if (song == null)
                {
                    logger.LogWarning("Song not found {SongId}", songId);
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading song for edit {SongId}", songId);
                return null;
            }
        }

        /// <summary>
        /// Edits an existing song.
        /// </summary>
        public async Task<bool> AdminEditSongAsync(SongInputModel model)
        {
            if (model == null)
            {
                logger.LogWarning("AdminEditSongAsync: model is null");
                return false;
            }

            try
            {
                var song = await dbContext.Songs.FirstOrDefaultAsync(s => s.Id == model.Id && !s.IsDeleted);

                if (song == null)
                {
                    logger.LogWarning("AdminEditSongAsync: Song {SongId} not found", model.Id);
                    return false;
                }

                song.Title = model.Title;
                song.Artist = model.Artist;
                song.Duration = model.Duration;
                song.Genre = model.Genre;
                song.MusicalKey = model.MusicalKey;
                song.Tempo = model.Tempo;
                song.IsPrivate = model.IsPrivate;
                song.OwnerBandId = model.BandId ?? song.OwnerBandId;

                await dbContext.SaveChangesAsync();
                logger.LogInformation("Song {SongId} edited by admin", model.Id);
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing song {SongId}", model.Id);
                return false;
            }
        }

        /// <summary>
        /// Creates a new song by admin.
        /// </summary>
        public async Task<int> AdminCreateSongAsync(SongInputModel model, string adminId)
        {
            try
            {
                var song = new Song
                {
                    Title = model.Title,
                    Artist = model.Artist,
                    Duration = model.Duration,
                    Genre = model.Genre,
                    MusicalKey = model.MusicalKey,
                    Tempo = model.Tempo,
                    IsPrivate = model.IsPrivate,
                    OwnerBandId = model.BandId,
                    CreatorId = adminId
                };

                await dbContext.Songs.AddAsync(song);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Song {SongId} created by admin {AdminId}", song.Id, adminId);
                return song.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating song by admin {AdminId}", adminId);
                return 0;
            }
        }
    }
}