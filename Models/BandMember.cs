using RehearsalHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.BandMember;

namespace RehearsalHub.Models
{
    public class BandMember 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(AvatarUrlMaxLength)]
        [Url]
        public string? AvatarUrl { get; set; }

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
        public BandRole Role { get; set; } 

        [Required]
        public InstrumentType Instrument { get; set; } 
    }
}
