using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Song
{
    public class SongIndexViewModel
    {
        public int Id { get; set; }

        public string Artist { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Duration { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string MusicalKey { get; set; } = null!;

        public int? Tempo { get; set; }

        public bool IsPrivate { get; set; }

        public string? OwnerBandName { get; set; }

        public string TempoDisplay => Tempo.HasValue ? $"{Tempo} BPM" : "N/A";
        public bool CanEdit { get; set; }

    }

}
