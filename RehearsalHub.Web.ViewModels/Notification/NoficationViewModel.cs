using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehearsalHub.Web.ViewModels.Notification
{
    public class NoficationViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string? LinkUrl { get; set; }
        public bool IsRead { get; set; }
        public string TimeAgo { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
