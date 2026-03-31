using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.GCommon;
using RehearsalHub.Web.ViewModels.Admin;
using static RehearsalHub.Common.DataValidation;

namespace RehearsalHub.Areas.Admin.Data
{
    /// <summary>
    /// Service for admin-specific operations: dashboard stats,
    /// paginated user/band management, and role assignment.
    /// Follows the same conventions as BandService and RehearsalService.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<AdminService> logger;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminService(ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager, ILogger<AdminService> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves aggregate counts for the admin dashboard.
        /// Each query is independent to avoid unnecessary JOINs.
        /// </summary>
        public async Task<AdminDashboardViewModel> GetDashboardStatsAsync()
        {
           var now = DateTime.UtcNow;
           var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            AdminDashboardViewModel? stats = new AdminDashboardViewModel{
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

        public async Task<PagedResult<AdminUserViewModel>> GetUsersPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var query =  dbContext.Users.AsNoTracking();

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

            foreach(var u in user)
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
        /// Returns a paginated, optionally filtered list of all non-deleted bands.
        /// </summary>
        public async Task<PagedResult<AdminBandViewModel>> GetBandsPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var query = dbContext.Bands.AsNoTracking().Where(b => !b.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm)){

                var term = searchTerm.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(term));
            }

            var totalCount = await dbContext.Bands.CountAsync();


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
        /// Promotes a user to the Admin role if not already assigned.
        /// </summary>
        public async Task<bool> PromoteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("PromoteUserAsync called with null or empty userId");
                return false;
            }

            ApplicationUser user = await dbContext.Users.FindAsync(userId);

            if(user == null)
            {
                logger.LogWarning("PromoteUserAsync: user {UserId} not found", userId);
                return false;
            }

            if(await userManager.IsInRoleAsync(user, "Admin"))
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
    }
}
