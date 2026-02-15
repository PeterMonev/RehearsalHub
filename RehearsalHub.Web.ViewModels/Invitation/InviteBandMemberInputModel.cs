namespace RehearsalHub.Web.ViewModels.Invitation
{
    using System.ComponentModel.DataAnnotations;
    using RehearsalHub.Data.Models.Enums;
    using static RehearsalHub.Common.DataValidation.Band;

    public class InviteBandMemberInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Band name is required.")]
        [StringLength(NameMaxLength, MinimumLength = 3,
            ErrorMessage = "Band name must be between 3 and {1} characters.")]
        public string BandName { get; set; } = null!;

        [Required(ErrorMessage = "Please select the required instrument for the position.")]
        [EnumDataType(typeof(InstrumentType), ErrorMessage = "Invalid instrument selection.")]
        public InstrumentType? Instrument { get; set; }

        [Required(ErrorMessage = "Inviter name is missing.")]
        public string InvitedBy { get; set; } = null!;

        [Required(ErrorMessage = "Band image is required for the invitation card.")]
        [Url(ErrorMessage = "Invalid image URL.")]
        public string BandImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Please write a personal message to the musician.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "The message must be between 10 and 500 characters.")]
        public string Message { get; set; } = null!;
    }
}