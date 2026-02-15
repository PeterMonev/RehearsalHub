using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProfilePictureUrl { get; set; } = null!;
        public BandRole? RoleInBand { get; set; }
        public InstrumentType Instrument { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<UserBandViewModel> Bands { get; set; } = new HashSet<UserBandViewModel>();


    }
}
