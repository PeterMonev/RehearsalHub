using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RehearsalHub.Data;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Users
{
    /// <summary>
    /// Service for managing user-related data operations, such as retrieving profiles and band memberships.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserService> logger;

        public UserService(ApplicationDbContext dbContext, ILogger<UserService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves the profile of a user, including their bands, roles, and instruments.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A <see cref="UserProfileViewModel"/> containing user information, or null if not found.</returns>
        public async Task<UserProfileViewModel?> GetUserProfileAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogWarning("GetUserProfileAsync called with null or empty userId");
                return null;
            }

            try
            {
                var user = await dbContext.Users
                    .Include(u => u.BandMembers)
                        .ThenInclude(bm => bm.Band)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    logger.LogWarning("User {UserId} not found", userId);
                    return null;
                }

                var model = new UserProfileViewModel
                {
                    UserName = user.UserName!, 
                    Email = user.Email!,       
                    PhoneNumber = user.PhoneNumber,
                    ProfilePictureUrl = user.ProfilePictureUrl!,

                    Bands = user.BandMembers
                        .Where(bm => bm.IsConfirmed)
                        .Select(bm => new UserBandViewModel
                        {
                            BandId = bm.BandId,
                            BandName = bm.Band.Name,
                            Role = bm.Role.ToString(),
                            Instrument = bm.Instrument.ToString(),
                            BandImageUrl = bm.Band.ImageUrl
                        })
                        .ToList()
                };

                logger.LogInformation("Successfully retrieved profile for user {UserId}", userId);
                return model;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user profile for user {UserId}", userId);
                return null;
            }
        }
    }
}
