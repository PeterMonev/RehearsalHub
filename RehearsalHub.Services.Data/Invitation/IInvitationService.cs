using RehearsalHub.Web.ViewModels.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Invitation
{
    public interface IInvitationService
    {
        Task<bool> SendInviteAsync(int bandId, string targetSearch, string instrument);
        Task<IEnumerable<InviteBandMemberInputModel>> GetPendingInvitationsAsync(string userId);
        Task<int?> AcceptInviteAsync(int inviteId, string userId);
        Task<bool> DeclineInviteAsync(int inviteId, string userId);
        Task<int> GetPendingCountAsync(string userId);
    }
}
