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
    /// <summary>
    /// Defines the contract for managing bands and their members.
    /// </summary>
    public interface IBandService
    {
        /// <summary>
        /// Retrieves a paginated list of bands for a user, optionally filtered by a search term.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the list.</param>
        /// <param name="page">The current page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="searchTerm">Optional search term to filter bands by name.</param>
        /// <returns>
        /// A <see cref="Task{PagedResult{BandIndexViewModel}}"/> containing the paginated bands.
        /// </returns>
        Task<PagedResult<BandIndexViewModel>> GetBandsPagedAsync(string userId, int page, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Creates a new band with the specified input model and owner.
        /// </summary>
        /// <param name="model">The band data to create.</param>
        /// <param name="ownerId">The user ID of the band owner.</param>
        /// <returns>
        /// A <see cref="Task{Int32}"/> representing the ID of the newly created band.
        /// </returns>
        Task<int> CreateBandAsync(BandInputModel model, string ownerId);

        /// <summary>
        /// Deletes a band if the requesting user is authorized.
        /// </summary>
        /// <param name="bandId">The unique identifier of the band to delete.</param>
        /// <param name="userId">The unique identifier of the user requesting deletion.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> indicating whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteBandAsync(int bandId, string userId);

        /// <summary>
        /// Retrieves a band for editing, if the user has permission.
        /// </summary>
        /// <param name="id">The ID of the band to edit.</param>
        /// <param name="userId">The ID of the user requesting the edit.</param>
        /// <returns>
        /// A <see cref="Task{BandEditViewModel}"/> with the editable band data, or null if not found or unauthorized.
        /// </returns>
        Task<BandEditViewModel?> GetBandEditAsync(int id, string userId);

        /// <summary>
        /// Updates an existing band's information.
        /// </summary>
        /// <param name="model">The updated band data.</param>
        /// <param name="userId">The ID of the user performing the edit.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> indicating whether the edit was successful.
        /// </returns>
        Task<bool> EditBandAsync(BandEditViewModel model, string userId);

        /// <summary>
        /// Retrieves detailed information about a specific band.
        /// </summary>
        /// <param name="id">The unique identifier of the band.</param>
        /// <param name="userId">The ID of the user requesting details.</param>
        /// <returns>
        /// A <see cref="Task{BandDetailsViewModel}"/> containing the band's details, or null if not found or unauthorized.
        /// </returns>
        Task<BandDetailsViewModel?> GetBandDetailsAsync(int id, string userId);

        /// <summary>
        /// Removes a specific member from a band's details, if the requesting user has permission.
        /// </summary>
        /// <param name="bandId">The ID of the band.</param>
        /// <param name="targetUserId">The ID of the member to remove.</param>
        /// <param name="userId">The ID of the user performing the removal.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> indicating whether the member removal was successful.
        /// </returns>
        Task<bool> DeleteBandMembersDetailsAsync(int bandId, string targetUserId, string userId);
    }
}
