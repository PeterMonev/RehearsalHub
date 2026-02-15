using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.User
{
    public class UserBandViewModel
    {
        public int BandId { get; set; }
        public string BandName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Instrument { get; set; } = null!;
        public string BandImageUrl { get; set; } = null!;
    }
}
