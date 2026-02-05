using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Rehearsal;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Models
{
    public class Rehearsal : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = DateTimeColumnType)]
        public DateTime StartRehearsal { get; set; }

        [Required]
        [Column(TypeName = DateTimeColumnType)]
        public DateTime EndRehearsal { get; set; }

        [MaxLength(NotesMaxLength)]
        public string? Notes { get; set; }

        [Required]
        public int  BandId { get; set; }

        [ForeignKey(nameof(BandId))]
        public Band Band { get; set; } = null!;

        public int? SetlistId { get; set; }

        [ForeignKey(nameof(SetlistId))]
        public virtual Setlist? Setlist { get; set; }
    }
}
