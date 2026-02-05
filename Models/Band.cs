using RehearsalHub.Models.Enums;
using RehearsalHub.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Band;
using static RehearsalHub.Common.ImageConstants;

namespace RehearsalHub.Models
{
    public class Band : BaseEntity
    {
        public Band()
        {
            this.ImageUrl = GetRandomBandImage();

            this.Members = new HashSet<BandMember>();
            this.Setlists = new HashSet<Setlist>();
            this.Rehearsals = new HashSet<Rehearsal>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public MusicGenre Genre { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;
        public ApplicationUser Owner { get; set; } = null!;

        public virtual ICollection<BandMember> Members { get; set; } 

        public virtual ICollection<Setlist> Setlists { get; set; } 

        public virtual ICollection<Rehearsal> Rehearsals { get; set; } 
    }
}
