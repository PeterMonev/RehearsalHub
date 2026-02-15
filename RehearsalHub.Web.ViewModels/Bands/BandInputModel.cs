namespace RehearsalHub.Web.ViewModels.Bands
{
    using System.ComponentModel.DataAnnotations;
    using RehearsalHub.Data.Models.Enums;
    using static RehearsalHub.Common.DataValidation.Band; 

    public class BandInputModel
    {
        [Required(ErrorMessage = "Band name is required!")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "The band name must be between {2} and {1} characters long.")]
        [Display(Name = "Band Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please select a music genre.")]
        [Display(Name = "Genre")]
        public MusicGenre Genre { get; set; }

        [MaxLength(ImageUrlMaxLength, ErrorMessage = "URL is too long.")]
        [Display(Name = "Band Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Please select the instrument you play in this band.")]
        [Display(Name = "Your Instrument")]
        public InstrumentType SelectedInstrument { get; set; }
    }
}