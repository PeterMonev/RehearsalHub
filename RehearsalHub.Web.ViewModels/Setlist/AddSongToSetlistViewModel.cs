using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RehearsalHub.Common.DataValidation.Setlist;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class AddSongToSetlistViewModel
    {
        public int SetlistId { get; set; }

        public string SetlistName { get; set; } = null!;
        public int BandId { get; set; }

        public List<AvailableSongViewModel> AvailableSongs { get; set; } = new();
        public List<int> SelectedSongIds { get; set; } = new();
    }
}
