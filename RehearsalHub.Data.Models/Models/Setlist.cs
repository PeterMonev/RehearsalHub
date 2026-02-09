using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Setlist;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Data.Models
{
    public class Setlist : BaseEntity
    {
        public Setlist()
        {
            this.SetlistSongs = new HashSet<SetlistSong>();
            this.Rehearsals = new HashSet<Rehearsal>();
        }
        [Key]

        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
           
        [Required]
        public int BandId { get; set; }

        [ForeignKey(nameof(BandId))]

        public Band Band { get; set; } = null!;

        [Column(TypeName = DateTimeColumnType)]
        public DateTime? RehearsalDate { get; set; }

        public virtual ICollection<SetlistSong> SetlistSongs { get; set; } 

        public virtual ICollection<Rehearsal> Rehearsals { get; set; }
    }
}
