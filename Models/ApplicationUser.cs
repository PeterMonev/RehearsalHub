using Microsoft.AspNetCore.Identity;

namespace RehearsalHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePictureUrl {  get; set; }
        public virtual ICollection<BandMember> BandMembers { get; set; } = new HashSet<BandMember>();  
        public virtual ICollection<Band> OwnedBands { get; set; } = new HashSet<Band>();
    }
}
