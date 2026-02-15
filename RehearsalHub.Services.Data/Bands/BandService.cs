using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.GCommon;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Shared;
using static RehearsalHub.Common.ImageConstants;

namespace RehearsalHub.Services.Data.Bands
{
    /// <summary>
    /// Service for managing band operations including creation, editing, deletion, and member management.
    /// </summary>
    public class BandService : IBandService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly INotificationService notificationService;
        private readonly ILogger<BandService> logger;

        public BandService(
            ApplicationDbContext dbContext,
            INotificationService notificationService,
            ILogger<BandService> logger)
        {
            this.dbContext = dbContext;
            this.notificationService = notificationService;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new band with the owner as the first confirmed member.
        /// Uses a transaction to ensure data consistency.
        /// </summary>
        public async Task<int> CreateBandAsync(BandInputModel model, string ownerId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new ArgumentException("Owner ID cannot be null or empty", nameof(ownerId));
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var band = new Band
                {
                    Name = model.Name,
                    Genre = model.Genre,
                    OwnerId = ownerId,
                    ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                    ? GetRandomBandImage()
                    : model.ImageUrl

                };

                band.Members.Add(new BandMember
                {
                    UserId = ownerId,
                    Role = BandRole.Owner,
                    Instrument = model.SelectedInstrument,
                    IsConfirmed = true
                });

                await dbContext.Bands.AddAsync(band);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                logger.LogInformation("Band {BandId} created by user {UserId}", band.Id, ownerId);
                return band.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Failed to create band for user {UserId}", ownerId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a band and all related data (members, invitations, notifications).
        /// Only the band owner can delete the band.
        /// </summary>
        public async Task<bool> DeleteBandAsync(int bandId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("DeleteBandAsync called with null or empty userId");
                return false;
            }

            if (bandId <= 0)
            {
                logger.LogWarning("DeleteBandAsync called with invalid bandId: {BandId}", bandId);
                return false;
            }

            try
            {
                var band = await dbContext.Bands
                    .Include(b => b.Members) 
                    .FirstOrDefaultAsync(b => b.Id == bandId && b.OwnerId == userId);

                if (band == null)
                {
                    logger.LogWarning("Delete failed: Band {BandId} not found or unauthorized for user {UserId}", bandId, userId);
                    return false;
                }

                await DeleteBandInvitationNotificationsAsync(bandId);

                dbContext.Bands.Remove(band);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Band {BandId} deleted by user {UserId}", bandId, userId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting band {BandId} for user {UserId}", bandId, userId);
                return false;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of bands that the user owns or is a member of.
        /// </summary>
        public async Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId,int page,int pageSize, string? searchTerm = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetBandsPagedAsync called with null or empty userId");
                return new PagedResult<BandIndexViewModel>(
                    new List<BandIndexViewModel>(), 0, page, pageSize);
            }

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                var query = dbContext.Bands
                    .AsNoTracking()
                    .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId && m.IsConfirmed));

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
                        MembersCount = b.Members.Count(m => m.IsConfirmed && !m.IsDeleted),
                        UpcomingRehearsals = b.Rehearsals
                            .Where(r => r.EndRehearsal > DateTime.UtcNow && !r.IsDeleted)
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving bands for user {UserId}", userId);
                return new PagedResult<BandIndexViewModel>(
                    new List<BandIndexViewModel>(), 0, page, pageSize);
            }
        }

        /// <summary>
        /// Retrieves band details for editing. Only the owner can edit a band.
        /// </summary>
        public async Task<BandEditViewModel?> GetBandEditAsync(int id, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetBandEditAsync called with null or empty userId");
                return null;
            }

            if (id <= 0)
            {
                logger.LogWarning("GetBandEditAsync called with invalid id: {Id}", id);
                return null;
            }

            try
            {
                return await dbContext.Bands
                    .AsNoTracking()
                    .Where(b => b.Id == id && b.OwnerId == userId)
                    .Select(b => new BandEditViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Genre = b.Genre,
                        ImageUrl = b.ImageUrl
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving band {BandId} for edit by user {UserId}", id, userId);
                return null;
            }
        }

        /// <summary>
        /// Updates band information. Only the owner can edit a band.
        /// </summary>
        public async Task<bool> EditBandAsync(BandEditViewModel model, string userId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("EditBandAsync called with null or empty userId");
                return false;
            }

            try
            {
                var band = await dbContext.Bands
                    .FirstOrDefaultAsync(b => b.Id == model.Id && b.OwnerId == userId);

                if (band == null)
                {
                    logger.LogWarning("Unauthorized or non-existent edit attempt for band {BandId} by user {UserId}",
                        model.Id, userId);
                    return false;
                }

                band.Name = model.Name;
                band.Genre = model.Genre;
                band.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
    ? GetRandomBandImage()
    : model.ImageUrl;


                await dbContext.SaveChangesAsync();

                logger.LogInformation("Band {BandId} updated by user {UserId}", model.Id, userId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing band {BandId} for user {UserId}", model.Id, userId);
                return false;
            }
        }

        /// <summary>
        /// Retrieves detailed band information including members, setlists, and upcoming rehearsals.
        /// User must be the owner or a confirmed member to access.
        /// </summary>
        public async Task<BandDetailsViewModel?> GetBandDetailsAsync(int id, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetBandDetailsAsync called with null or empty userId");
                return null;
            }

            if (id <= 0)
            {
                logger.LogWarning("GetBandDetailsAsync called with invalid id: {Id}", id);
                return null;
            }

            try
            {
                var bandDetails = await dbContext.Bands
                    .AsNoTracking()
                    .Where(b => b.Id == id &&
                        (b.OwnerId == userId || b.Members.Any(m => m.UserId == userId && m.IsConfirmed && !m.IsDeleted)))
                    .Select(b => new BandDetailsViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Genre = b.Genre.ToString(),
                        ImageUrl = b.ImageUrl,
                        IsOwner = b.OwnerId == userId,
                        Members = b.Members
                            .Where(m => m.IsConfirmed && !m.IsDeleted)
                            .Select(m => new BandMemberViewModel
                            {
                                FullName = m.User.UserName ?? "Unknown",
                                Instrument = m.Instrument.ToString(),
                                IsLeader = m.Role == BandRole.Owner,
                                UserId = m.UserId,
                                AvatarUrl = m.User.ProfilePictureUrl
                            }).ToList(),
                        Setlists = b.Setlists
                            .Where(s => !s.IsDeleted)
                            .Select(s => new BandSetlistViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                SongsCount = s.SetlistSongs.Count()
                            }).ToList(),
                        UpcomingRehearsals = b.Rehearsals
                            .Where(r => r.EndRehearsal > DateTime.UtcNow && !r.IsDeleted)
                            .OrderBy(r => r.StartRehearsal)
                            .Select(r => new RehearsalInfoViewModel
                            {
                                Id = r.Id,
                                Start = r.StartRehearsal,
                                End = r.EndRehearsal,
                                SetlistName = r.Setlist != null ? r.Setlist.Name : null
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (bandDetails == null)
                {
                    logger.LogWarning("Access denied or not found: User {UserId} attempted to view band {BandId}", userId, id);
                }

                return bandDetails;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving details for band {BandId} for user {UserId}", id, userId);
                throw;
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Deletes all invitation-related notifications for a band.
        /// This prevents orphaned notifications when a band is deleted.
        /// </summary>
        private async Task DeleteBandInvitationNotificationsAsync(int bandId)
        {
            try
            {
                var invitedUserIds = await dbContext.BandMembers
                    .AsNoTracking()
                    .Where(bm => bm.BandId == bandId && !bm.IsConfirmed && !bm.IsDeleted)
                    .Select(bm => bm.UserId)
                    .Distinct()
                    .ToListAsync();

                var notificationsToDelete = await dbContext.Notifications
                    .Where(n => invitedUserIds.Contains(n.RecipientId) &&
                                n.LinkUrl == "/Invitation/Index")
                    .ToListAsync();

                if (notificationsToDelete.Any())
                {
                    dbContext.Notifications.RemoveRange(notificationsToDelete);
                    await dbContext.SaveChangesAsync();

                    logger.LogInformation("Deleted {Count} invitation notifications for band {BandId}",
                        notificationsToDelete.Count, bandId);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to delete invitation notifications for band {BandId}", bandId);
            }
        }

        /// <summary>
        /// Removes a member from the band. 
        /// Can be performed by the owner or the member themselves (leaving).
        /// </summary>
        public async Task<bool> DeleteBandMembersDetailsAsync(int bandId,string targetUserId, string userId)
        {
            try
            {
                var band = await dbContext.Bands.Include(b => b.Members).FirstOrDefaultAsync(b => b.Id == bandId);

                if (band == null) return false;

                var memberToRemove = band.Members.FirstOrDefault(m => m.UserId == targetUserId && !m.IsDeleted && m.IsConfirmed);

                if (memberToRemove == null) return false;

                bool isOwner = band.OwnerId == userId;
                bool isSelfRemoving = targetUserId == userId;

                if (memberToRemove.Role == BandRole.Owner)
                {
                    logger.LogWarning("Attempt to remove owner {UserId} from band {BandId}", targetUserId, bandId);
                    return false;
                }

                if (!isOwner && !isSelfRemoving)
                {
                    logger.LogWarning("Unauthorized member removal attempt by user {userId} for band {BandId}", userId, bandId);
                    return false;
                }

                dbContext.BandMembers.Remove(memberToRemove);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("User {TargetId} removed from band {BandId} by {ReqId}", targetUserId, bandId, userId);
                return true;
            } catch (Exception ex)
            {
                logger.LogError(ex, "Error removing member {TargetId} from band {BandId}", targetUserId, bandId);
                return false;
            }
        }

        #endregion
    }
}