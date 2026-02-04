using RehearsalHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RehearsalHub.Common.DataValidation.Song;

namespace RehearsalHub.Models
{
    public class Song
    {
        [Key]

        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [RegularExpression(DurationRegex, ErrorMessage = "Duration must be in format mm:ss")]
        [MinLength(DurationMinLength)]
        [MaxLength(DuraitonMaxLength)]
        public string Duration { get; set; } = null!;

        [Required]
        public MusicGenre Genre { get; set; }

        [Required]
        public MusicalKey Key { get; set; }

        [Range(0, 320, ErrorMessage = "Tempo must be between 00 and 320 BPM")]
        public int? Tempo {  get; set; }

        public virtual ICollection<SetlistSong> SetlistSongs { get; set; } = new HashSet<SetlistSong>();
    }
}
