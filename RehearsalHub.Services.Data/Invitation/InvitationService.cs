using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Web.ViewModels.Invitation;

namespace RehearsalHub.Services.Data.Invitation
{
    /// <summary>
    /// Service for managing band invitations including sending, accepting, and declining invites.
    /// </summary>
    public class InvitationService : IInvitationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly INotificationService notificationService;
        private readonly IHubContext<NotificationsHub> hubContext;
        private readonly ILogger<InvitationService> logger;

        public InvitationService(
            ApplicationDbContext dbContext,
            INotificationService notificationService,
            IHubContext<NotificationsHub> hubContext,
            ILogger<InvitationService> logger)
        {
            this.dbContext = dbContext;
            this.notificationService = notificationService;
            this.hubContext = hubContext;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the count of pending (unconfirmed, non-deleted) invitations for a user.
        /// </summary>
        public async Task<int> GetPendingCountAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetPendingCountAsync called with null or empty userId");
                return 0;
            }

            try
            {
                return await dbContext.BandMembers
                    .CountAsync(bm => bm.UserId == userId
                                   && !bm.IsConfirmed
                                   && !bm.IsDeletedInvitation
                                   && !bm.IsDeleted);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting pending invitation count for user {UserId}", userId);
                return 0;
            }
        }

        /// <summary>
        /// Sends a band invitation to a user by email or username.
        /// Reactivates previously declined invitations if they exist.
        /// </summary>
        public async Task<bool> SendInviteAsync(int bandId, string targetSearch, string instrument)
        {
            if (string.IsNullOrWhiteSpace(targetSearch))
            {
                logger.LogWarning("SendInviteAsync called with empty targetSearch");
                return false;
            }

            if (string.IsNullOrWhiteSpace(instrument))
            {
                logger.LogWarning("SendInviteAsync called with empty instrument");
                return false;
            }

            if (!Enum.TryParse<InstrumentType>(instrument, out var instrumentType))
            {
                logger.LogWarning("Invalid instrument type: {Instrument}", instrument);
                return false;
            }

            try
            {
                var user = await dbContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == targetSearch || u.UserName == targetSearch);

                if (user == null)
                {
                    logger.LogWarning("User not found: {TargetSearch}", targetSearch);
                    return false;
                }

                var band = await dbContext.Bands
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == bandId);

                if (band == null)
                {
                    logger.LogWarning("Band not found: {BandId}", bandId);
                    return false;
                }

                if (band.OwnerId == user.Id)
                {
                    logger.LogWarning("Band owner {OwnerId} attempted to invite themselves to band {BandId}", user.Id, bandId);
                    return false;
                }

                var isActiveMember = await dbContext.BandMembers
                    .AnyAsync(bm => bm.BandId == bandId
                                 && bm.UserId == user.Id
                                 && bm.IsConfirmed
                                 && !bm.IsDeleted);

                if (isActiveMember)
                {
                    logger.LogInformation("User {UserId} is already a member of band {BandId}", user.Id, bandId);
                    return false;
                }

                var hasPendingInvitation = await dbContext.BandMembers
                    .AnyAsync(bm => bm.BandId == bandId
                                 && bm.UserId == user.Id
                                 && !bm.IsConfirmed
                                 && !bm.IsDeletedInvitation
                                 && !bm.IsDeleted);

                if (hasPendingInvitation)
                {
                    logger.LogInformation("User {UserId} already has a pending invitation to band {BandId}", user.Id, bandId);
                    return false;
                }

                var existingInvitation = await dbContext.BandMembers
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(bm => bm.BandId == bandId && bm.UserId == user.Id);

                if (existingInvitation != null)
                {
                    logger.LogInformation("Reactivating invitation for user {UserId} to band {BandId}", user.Id, bandId);

                    existingInvitation.IsDeleted = false;
                    existingInvitation.IsDeletedInvitation = false;
                    existingInvitation.IsConfirmed = false;
                    existingInvitation.Instrument = instrumentType;
                    existingInvitation.Role = BandRole.Member;
                    existingInvitation.DeletedOn = null;
                    existingInvitation.ModifiedOn = DateTime.UtcNow;
                }
                else
                {
                    logger.LogInformation("Creating new invitation for user {UserId} to band {BandId}", user.Id, bandId);

                    var newInvite = new BandMember
                    {
                        BandId = bandId,
                        UserId = user.Id,
                        Instrument = instrumentType,
                        IsConfirmed = false,
                        IsDeletedInvitation = false,
                        Role = BandRole.Member
                    };

                    await dbContext.BandMembers.AddAsync(newInvite);
                }

                await dbContext.SaveChangesAsync();

                await NotifyUserOfInvitationAsync(user.Id);


                logger.LogInformation("Successfully sent invitation to user {UserId} for band {BandId}", user.Id, bandId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending invitation to {TargetSearch} for band {BandId}", targetSearch, bandId);
                return false;
            }
        }

        /// <summary>
        /// Accepts an invitation and confirms the user as a band member.
        /// Returns the BandId if successful, null otherwise.
        /// </summary>
        public async Task<int?> AcceptInviteAsync(int inviteId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("AcceptInviteAsync called with null or empty userId");
                return null;
            }

            try
            {
                var invite = await dbContext.BandMembers
                    .FirstOrDefaultAsync(bm => bm.Id == inviteId
                                            && bm.UserId == userId
                                            && !bm.IsDeleted
                                            && !bm.IsConfirmed);

                if (invite == null)
                {
                    logger.LogWarning("Invitation {InviteId} not found for user {UserId}", inviteId, userId);
                    return null;
                }

                invite.IsConfirmed = true;
                invite.IsDeletedInvitation = true;
                invite.ModifiedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();

                await UpdateClientInvitationCountAsync(userId);

                logger.LogInformation("User {UserId} accepted invitation {InviteId} for Band {BandId}", userId, inviteId, invite.BandId);

                return invite.BandId; 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error accepting invitation {InviteId} for user {UserId}", inviteId, userId);
                return null;
            }
        }

        /// <summary>
        /// Declines an invitation by soft-deleting it.
        /// </summary>
        public async Task<bool> DeclineInviteAsync(int inviteId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("DeclineInviteAsync called with null or empty userId");
                return false;
            }

            try
            {
                var invite = await dbContext.BandMembers
                    .FirstOrDefaultAsync(bm => bm.Id == inviteId
                                            && bm.UserId == userId
                                            && !bm.IsDeleted
                                            && !bm.IsConfirmed);

                if (invite == null)
                {
                    logger.LogWarning("Invitation {InviteId} not found for user {UserId}", inviteId, userId);
                    return false;
                }

                invite.IsDeletedInvitation = true;
                invite.IsDeleted = true;
                invite.DeletedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
                await UpdateClientInvitationCountAsync(userId);

                logger.LogInformation("User {UserId} declined invitation {InviteId}", userId, inviteId);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error declining invitation {InviteId} for user {UserId}", inviteId, userId);
                return false;
            }
        }

        /// <summary>
        /// Gets all pending invitations for a specific user.
        /// </summary>
        public async Task<IEnumerable<InviteBandMemberInputModel>> GetPendingInvitationsAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetPendingInvitationsAsync called with null or empty userId");
                return Enumerable.Empty<InviteBandMemberInputModel>();
            }

            try
            {
                return await dbContext.BandMembers
                    .AsNoTracking()
                    .Where(bm => bm.UserId == userId
                              && !bm.IsConfirmed
                              && !bm.IsDeletedInvitation
                              && !bm.IsDeleted)
                    .Select(bm => new InviteBandMemberInputModel
                    {
                        Id = bm.Id,
                        BandName = bm.Band.Name,
                        Instrument = bm.Instrument,
                        InvitedBy = bm.Band.Owner.UserName!,
                        BandImageUrl = bm.Band.ImageUrl,
                        Message = $"You've been invited to join {bm.Band.Name} as {bm.Instrument} by {bm.Band.Owner.UserName}"
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting pending invitations for user {UserId}", userId);
                return Enumerable.Empty<InviteBandMemberInputModel>();
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Updates the client-side invitation count via SignalR.
        /// </summary>
        private async Task UpdateClientInvitationCountAsync(string userId)
        {
            try
            {
                var count = await GetPendingCountAsync(userId);
                await hubContext.Clients.User(userId).SendAsync("UpdateInviteCount", count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating client invitation count for user {UserId}", userId);
            }
        }

        /// <summary>
        /// Notifies a user of a new invitation via SignalR and the notification system.
        /// </summary>
        private async Task NotifyUserOfInvitationAsync(string userId)
        {
            try
            {
                var count = await GetPendingCountAsync(userId);
                await hubContext.Clients.User(userId).SendAsync("UpdateInviteCount", count);
                await notificationService.CreateAsync(userId, "You have a new band invite!", "/Invitation/Index");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error notifying user {UserId} of invitation", userId);
            }
        }

        #endregion
    }
}