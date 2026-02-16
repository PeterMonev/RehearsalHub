using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Song
{
    public class SongSetlistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BandName { get; set; } = null!;
    }
}
