using RehearsalHub.Web.ViewModels.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Invitation
{
    /// <summary>
    /// Defines the contract for managing invitations for band members.
    /// </summary>
    public interface IInvitationService
    {
        /// <summary>
        /// Sends an invitation to a user to join a band with a specific instrument.
        /// </summary>
        /// <param name="bandId">The unique identifier of the band.</param>
        /// <param name="targetSearch">The search string to identify the target user (e.g., username or email).</param>
        /// <param name="instrument">The instrument the invited user will play.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> indicating whether the invitation was successfully sent.
        /// </returns>
        Task<bool> SendInviteAsync(int bandId, string targetSearch, string instrument);

        /// <summary>
        /// Retrieves all pending invitations for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A <see cref="Task{IEnumerable{InviteBandMemberInputModel}}"/> containing all pending invitations.
        /// </returns>
        Task<IEnumerable<InviteBandMemberInputModel>> GetPendingInvitationsAsync(string userId);

        /// <summary>
        /// Accepts a pending invitation for a user to join a band.
        /// </summary>
        /// <param name="inviteId">The unique identifier of the invitation.</param>
        /// <param name="userId">The unique identifier of the user accepting the invitation.</param>
        /// <returns>
        /// A <see cref="Task{Nullable{Int32}}"/> containing the band ID if the invitation was accepted successfully, or null if the operation failed.
        /// </returns>
        Task<int?> AcceptInviteAsync(int inviteId, string userId);

        /// <summary>
        /// Declines a pending invitation for a user.
        /// </summary>
        /// <param name="inviteId">The unique identifier of the invitation.</param>
        /// <param name="userId">The unique identifier of the user declining the invitation.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> indicating whether the invitation was successfully declined.
        /// </returns>
        Task<bool> DeclineInviteAsync(int inviteId, string userId);

        /// <summary>
        /// Gets the number of pending invitations for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A <see cref="Task{Int32}"/> representing the total number of pending invitations.
        /// </returns>
        Task<int> GetPendingCountAsync(string userId);
    }
}
