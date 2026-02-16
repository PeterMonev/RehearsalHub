namespace RehearsalHub.Web.ViewModels.Song
{
    using System.ComponentModel.DataAnnotations;
    using RehearsalHub.Data.Models.Enums;
    using static RehearsalHub.Common.DataValidation.Song;

    public class SongInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(ArtistMaxLength, MinimumLength = ArtistMinLength)]
        public string Artist { get; set; } = null!;

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [RegularExpression(DurationRegex, ErrorMessage = "Duration must be in format mm:ss")]
        public string Duration { get; set; } = null!;

        [Required]
        public MusicGenre Genre { get; set; }

        [Required]
        public MusicalKey MusicalKey { get; set; }

        [Range(0, 320)]
        public int? Tempo { get; set; }

        public bool IsPrivate { get; set; }

        public int? BandId { get; set; } 
    }
}