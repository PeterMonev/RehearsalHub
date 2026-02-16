using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class SetlistDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BandId { get; set; }
        public string BandName { get; set; } = null!;
        public DateTime? RehearsalDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool CanEdit { get; set; }
        public bool CanAddSongs { get; set; }
        public List<SetlistSongViewModel> Songs { get; set; } = new();
        public int TotalSongs => Songs.Count;
        public string TotalDuration { get; set; } = "0:00";
    }
}
