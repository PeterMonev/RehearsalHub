using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.BandMember;
using static RehearsalHub.Common.ImageConstants;

namespace RehearsalHub.Data.Models
{
    public class BandMember : BaseEntity
    {
        public BandMember()
        {
            this.AvatarUrl = GetRandomMemberImage();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AvatarUrlMaxLength)]
        public string AvatarUrl { get; set; }

        [Required]
        public int BandId { get; set; }

        [Required]
        [ForeignKey(nameof(BandId))]
        public Band Band { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        [Required]
        public bool IsConfirmed { get; set; } = false;

        public Guid? InvitationToken { get; set; }

        [Required]
        public BandRole Role { get; set; } 

        [Required]
        public InstrumentType Instrument { get; set; } 
    }
}
