using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Band band = new Band
                {
                    Name = model.Name,
                    Genre = model.Genre,
                    OwnerId = ownerId,
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

                await transaction.CommitAsync();

                return band.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Transaction failed for CreateBandAsync. User: {UserId}", ownerId);
                throw;
            }
        }

        public async Task<bool> DeleteBandAsync(int bandId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) || bandId <= 0) return false;

            Band? band = await dbContext.Bands
                .FirstOrDefaultAsync(b => b.Id == bandId && b.OwnerId == userId);

            if (band == null)
            {
                this.logger.LogWarning("Delete failed: Band {BandId} not found or unauthorized for User {UserId}.", bandId, userId);
                return false;
            }

            dbContext.Bands.Remove(band);
            await dbContext.SaveChangesAsync();

            this.logger.LogInformation("Band {BandId} deleted by user {UserId}.", bandId, userId);
            return true;
        }

        public async Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId, int page, int pageSize, string? searchTerm)
        {
            var query = this.dbContext.Bands
                .AsNoTracking()
                .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId));

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Name.Contains(searchTerm));
            }

            int totalCount = await query.CountAsync();

            var bands = await query
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
        }

        public async Task<BandEditViewModel?> GetBandEditAsync(int id, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) || id <= 0)
            {
                logger.LogWarning("Invalid parameters for GetBandForEditAsync: Id {Id}, UserId {UserId}", id, userId);
                return null;
            }

            return await this.dbContext.Bands.Where(b => b.Id == id && b.OwnerId == userId)
                .AsNoTracking()
                .Select(b => new BandEditViewModel
            {
                    Id = b.Id,
                    Name = b.Name,
                    Genre = b.Genre,
                    ImageUrl = b.ImageUrl

             }).FirstOrDefaultAsync();
        }

        public async Task<bool> EditBandAsync(BandEditViewModel model, string userId)
        {
            Band? band = await dbContext.Bands
                .FirstOrDefaultAsync(b => b.Id == model.Id && b.OwnerId == userId);

            if (band == null)
            {
                logger.LogWarning("Unauthorized attempt to edit Band {BandId} by User {UserId}", model.Id, userId);
                return false;
            }

            band.Name = model.Name;
            band.Genre = model.Genre;
            band.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
