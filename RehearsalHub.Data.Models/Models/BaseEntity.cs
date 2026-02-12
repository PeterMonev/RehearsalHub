using RehearsalHub.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using static RehearsalHub.Common.EntityConstants;

namespace RehearsalHub.Data.Models
{
    public abstract class BaseEntity : IAuditInfo
    {
        [Column(TypeName = DateTimeColumnType)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column(TypeName = DateTimeColumnType)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Column(TypeName = DateTimeColumnType)]
        public DateTime? DeletedOn { get; set; }
    }
}
