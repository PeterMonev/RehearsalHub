using RehearsalHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Invitation
{
    public class InviteBandMemberInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BandName { get; set; } = null!;

        public InstrumentType Instrument { get; set; }

        [Required]
        public string InvitedBy { get; set; } = null!;

        [Required]
        [Url]
        public string BandImageUrl { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
    }
}
