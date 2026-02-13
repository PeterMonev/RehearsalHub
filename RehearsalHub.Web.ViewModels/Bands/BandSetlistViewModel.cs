using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Bands
{
    public class BandSetlistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SongsCount { get; set; }
    }
}
