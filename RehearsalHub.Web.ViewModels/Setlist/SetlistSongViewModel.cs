using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class SetlistSongViewModel
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string MusicalKey { get; set; } = null!;
        public int? Tempo { get; set; }
        public string TempoDisplay => Tempo.HasValue ? $"{Tempo} BPM" : "N/A";
    }
}
