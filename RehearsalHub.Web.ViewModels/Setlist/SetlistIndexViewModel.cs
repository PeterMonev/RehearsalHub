using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Setlist
{
    public class SetlistIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BandName { get; set; } = null!;
        public int SongsCount { get; set; }
        public DateTime? RehearsalDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
