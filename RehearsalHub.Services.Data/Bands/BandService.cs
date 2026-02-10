using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data;
using RehearsalHub.Web.ViewModels.Bands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Bands
{
    public class BandService : IBandService
    {
        private readonly ApplicationDbContext dbContext;
        public BandService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BandIndexViewModel>> GetAllBandsAsync(string userId)
        {
            return await this.dbContext.Bands
                .AsNoTracking()
                .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId))
                .Select(b => new BandIndexViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.ToString(),
                    ImageUrl = b.ImageUrl,
                    IsOwner = b.OwnerId == userId,
                    MembersCount = b.Members.Count(),
                })
                .OrderBy(b => b.Name)
                .ToListAsync();

        }
    }
}
