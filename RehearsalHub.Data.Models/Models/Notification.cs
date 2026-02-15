using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RehearsalHub.Common.DataValidation.Notification;

namespace RehearsalHub.Data.Models.Models
{
    public class Notification : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; } = null!;

        public string? LinkUrl { get; set; } = null!;

        public bool IsRead { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; } = null!;
        public virtual ApplicationUser Recipient { get; set; } = null!;
    }
}
