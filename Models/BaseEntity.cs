using RehearsalHub.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Models
{
    public abstract class BaseEntity : IAuditInfo
    {
        [Column(TypeName = DateTimeColumnType)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column(TypeName = DateTimeColumnType)]
        public DateTime? ModifiedOn { get; set; }
    }
}
