using RehearsalHub.Web.ViewModels.Bands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Services.Data.Bands
{
    public interface IBandService
    {
        Task<IEnumerable<BandIndexViewModel>> GetAllBandsAsync(string userId);
    }
}
