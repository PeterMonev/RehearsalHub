using RehearsalHub.Web.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Bands
{
    /// <summary>
    /// ViewModel for displaying a brief summary of a band in a list.
    /// </summary>
    public class BandIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public bool IsOwner { get; set; }

        public int MembersCount { get; set; }

        public IEnumerable<RehearsalInfoViewModel> UpcomingRehearsals { get; set; } = new List<RehearsalInfoViewModel>();
    }

}
