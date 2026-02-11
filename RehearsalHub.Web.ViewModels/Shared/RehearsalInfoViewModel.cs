using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Shared
{
    public class RehearsalInfoViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Duration => End - Start;

        public string? SetlistName { get; set; }

        public string DurationText => Duration.Minutes == 0 ? $"{(int)Duration.TotalHours}h" : $"{(int)Duration.TotalHours}h {Duration.Minutes}m";
    }
}
