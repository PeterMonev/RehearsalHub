using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.GCommon;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Shared;
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
        private readonly ILogger<BandService> logger;
        public BandService(ApplicationDbContext dbContext, ILogger<BandService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<int> CreateBandAsync(BandInputModel model, string ownerId)
        {
            Band band = new Band
            {
                Name = model.Name,
                Genre = model.Genre,
                OwnerId = ownerId,
                CreatedOn = DateTime.UtcNow
            };

            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                band.ImageUrl = model.ImageUrl;
            }

            band.Members.Add(new BandMember
            {
                UserId = ownerId,
                Role = 0,
                Instrument = model.SelectedInstrument,
            });

            await this.dbContext.Bands.AddAsync(band);
            await this.dbContext.SaveChangesAsync();

            return band.Id;
        }

        public async Task<bool> DeleteBandAsync(int bandId, string userId)
        {
          if (string.IsNullOrWhiteSpace(userId) || bandId <= 0)
            {
                this.logger.LogWarning("Delete attempted with invalid data: UserId {UserId}, BandId {BandId}", userId, bandId);
                return false;
            }

            Band? band = await dbContext.Bands.FirstOrDefaultAsync(b => b.Id == bandId && b.OwnerId == userId);

            if(band == null)
            {
                this.logger.LogWarning("Delete failed: Band {BandId} not found or user {UserId} is not the authorized owner.", bandId, userId);
                return false;
            }

            try
            {
                dbContext.Bands.Remove(band);
                await dbContext.SaveChangesAsync();

                this.logger.LogInformation("Band {BandId} was successfully deleted by user {UserId}.", bandId, userId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting band {BandId}", bandId);
                return false;
            }
        }

        public async Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId, int page,int pageSize, string? searchTerm)
        {
            try
            {
                var query = this.dbContext.Bands
                .AsNoTracking()
                .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId));

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(b => b.Name.Contains(searchTerm));
                }
                int totalCount = await query.CountAsync();

                List<BandIndexViewModel> bands = await query
                    .OrderBy(b => b.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(b => new BandIndexViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Genre = b.Genre.ToString(),
                        ImageUrl = b.ImageUrl,
                        IsOwner = b.OwnerId == userId,
                        MembersCount = b.Members.Count(),
                        UpcomingRehearsals = b.Rehearsals
                        .Where(r => r.EndRehearsal > DateTime.UtcNow)
                        .OrderBy(r => r.StartRehearsal)
                        .Take(3)
                        .Select(r => new RehearsalInfoViewModel
                        {
                            Id = r.Id,
                            Start = r.StartRehearsal,
                            End = r.EndRehearsal,
                            SetlistName = r.Setlist != null ? r.Setlist.Name : null,
                        })
                    })
                    .ToListAsync();

                    return new PagedResult<BandIndexViewModel>(bands, totalCount, page, pageSize);
            } catch (Exception e)
            {
                logger.LogError(e, "Database error in GetBandsPagedAsync");
                throw;
            }

        }

    }
}
