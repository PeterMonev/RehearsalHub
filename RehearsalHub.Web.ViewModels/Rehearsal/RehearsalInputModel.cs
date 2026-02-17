using System.ComponentModel.DataAnnotations;
using static RehearsalHub.Common.DataValidation.Rehearsal;

namespace RehearsalHub.Web.ViewModels.Rehearsal
{
    public class RehearsalInputModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Rehearsal name is required")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "Name must be between {2} and {1} characters")]
        [Display(Name = "Rehearsal Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Start date and time is required")]
        [Display(Name = "Start Date & Time")]
        public DateTime StartRehearsal { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "End date and time is required")]
        [Display(Name = "End Date & Time")]
        public DateTime EndRehearsal { get; set; } = DateTime.Now.AddDays(1).AddHours(2);

        [StringLength(NotesMaxLength, ErrorMessage = "Notes cannot exceed {1} characters")]
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Required]
        public int BandId { get; set; }

        [Display(Name = "Setlist (Optional)")]
        public int? SetlistId { get; set; }

        public string? BandName { get; set; }
        public List<SetlistOptionViewModel> AvailableSetlists { get; set; } = new();
    }
}
