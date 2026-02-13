using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RehearsalHub.Data.Models.Enums;
using static RehearsalHub.Common.DataValidation.Band;

namespace RehearsalHub.Web.ViewModels.Bands
{
    public class BandInputModel
    {

        [Required]
        [MaxLength(NameMaxLength)]
        [Display(Name = "Band Name")]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Genre")]
        public MusicGenre Genre { get; set; }

        [Url]
        [Display(Name = "Band Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Display(Name = "Your Instrument")]
        public InstrumentType SelectedInstrument { get; set; }
    }
}
