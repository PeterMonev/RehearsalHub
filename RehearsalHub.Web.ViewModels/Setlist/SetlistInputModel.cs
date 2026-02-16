using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RehearsalHub.Common.DataValidation.Setlist;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class SetlistInputModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Setlist name is required")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "Name must be between {2} and {1} characters")]
        [Display(Name = "Setlist Name")]
        public string Name { get; set; } = null!;

        [Required]
        public int BandId { get; set; }

        [Display(Name = "Rehearsal Date (Optional)")]
        [Column(TypeName = DateTimeColumnType)]
        public DateTime? RehearsalDate { get; set; }

        public string? BandName { get; set; }
    }

}
