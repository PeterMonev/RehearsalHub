using RehearsalHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Band;

namespace RehearsalHub.Models
{
    public class Band
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public MusicGenre Genre { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;
        public ApplicationUser Owner { get; set; } = null!;

        public virtual ICollection<BandMember> Members { get; set; } = new HashSet<BandMember>();

        public virtual ICollection<Setlist> Setlists { get; set; } = new HashSet<Setlist>();

        public virtual ICollection<Rehearsal> Rehearsals { get; set; } = new HashSet<Rehearsal> ();
    }
}
