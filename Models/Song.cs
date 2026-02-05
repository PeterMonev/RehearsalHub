using RehearsalHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.DataValidation.Song;

namespace RehearsalHub.Models
{
    public class Song : BaseEntity
    {
        public Song()
        {
            this.SetlistSongs = new HashSet<SetlistSong>();
        }
        [Key]

        public int Id { get; set; }

        [Required]
        [MinLength(ArtistMinLength)]
        [MaxLength(ArtistMaxLength)]
        public string Artist { get; set; } = null!;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [RegularExpression(DurationRegex, ErrorMessage = "Duration must be in format mm:ss")]
        [MinLength(DurationMinLength)]
        [MaxLength(DurationMaxLength)]
        public string Duration { get; set; } = null!;

        [Required]
        public MusicGenre Genre { get; set; }

        [Required]
        public MusicalKey MusicalKey { get; set; }

        [Range(0, 320, ErrorMessage = "Tempo must be between 00 and 320 BPM")]
        public int? Tempo {  get; set; }

        [Required]
        public bool IsPrivate { get; set;}

        public int? OwnerBandId { get; set; }

        [ForeignKey(nameof(OwnerBandId))]
        
        public Band? OwnerBand { get; set; }

        public virtual ICollection<SetlistSong> SetlistSongs { get; set; } 
    }
}
