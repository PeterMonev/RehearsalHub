using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data;
using RehearsalHub.GCommon;
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

        public async Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId, int page,int pageSize)
        {
            int totalCount = await this.dbContext.Bands
                .CountAsync(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId));

            List<BandIndexViewModel> bands = await this.dbContext.Bands
                .AsNoTracking()
                .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId))
                .OrderBy(b => b.Name)
                .Skip((page - 1 ) * pageSize)
                .Take(pageSize)
                .Select(b => new BandIndexViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre.ToString(),
                    ImageUrl = b.ImageUrl,
                    IsOwner = b.OwnerId == userId,
                    MembersCount = b.Members.Count(),
                })
                .ToListAsync();

            return new PagedResult<BandIndexViewModel>(bands, totalCount, page, pageSize);
        }

        public Task<int> GetTotalBandsCountAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
