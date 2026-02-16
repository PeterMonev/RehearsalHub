using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class AvailableSongViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int? Tempo { get; set; }
        public bool IsAlreadyInSetlist { get; set; }
        public string TempoCategory { get; set; } = "unknown";
    }
}
