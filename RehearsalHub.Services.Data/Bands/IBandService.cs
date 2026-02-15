using RehearsalHub.GCommon;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Bands
{
    public interface IBandService
    {
        Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId, int page, int pageSize, string? searchTerm = null);

        Task<int> CreateBandAsync(BandInputModel model, string ownerId);

        Task<bool> DeleteBandAsync(int bandId, string userId);

        Task<BandEditViewModel?> GetBandEditAsync(int id, string userId);

        Task<bool> EditBandAsync(BandEditViewModel model, string userId);

        Task<BandDetailsViewModel?> GetBandDetailsAsync(int id, string userId);

        Task<bool> DeleteBandMembersDetailsAsync(int bandId, string targetUserId, string userId);
    }
}
