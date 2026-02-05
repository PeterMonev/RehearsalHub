using Microsoft.AspNetCore.Identity;
using RehearsalHub.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Profile;
using static RehearsalHub.Common.ImageConstants;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Models
{
    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            this.ProfilePictureUrl = GetRandomUserImage();

            this.BandMembers = new HashSet<BandMember>();
            this.OwnedBands = new HashSet<Band>();
        }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ProfilePictureUrl {  get; set; }
        public virtual ICollection<BandMember> BandMembers { get; set; }
        public virtual ICollection<Band> OwnedBands { get; set; }

        [Column(TypeName = DateTimeColumnType)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column(TypeName = DateTimeColumnType)]
        public DateTime? ModifiedOn { get; set; }
    }
}
