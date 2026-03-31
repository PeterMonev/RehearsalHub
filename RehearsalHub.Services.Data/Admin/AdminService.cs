using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Web.ViewModels.Admin;

namespace RehearsalHub.Areas.Admin.Data
{
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
    }
}
