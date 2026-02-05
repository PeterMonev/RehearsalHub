using System.ComponentModel.DataAnnotations;

namespace RehearsalHub.Models.Interfaces
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
