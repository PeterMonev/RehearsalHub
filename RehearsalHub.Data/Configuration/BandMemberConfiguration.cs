using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using static RehearsalHub.Common.ImageConstants;


namespace RehearsalHub.Data.Configuration
{
    public class BandMemberConfiguration : IEntityTypeConfiguration<BandMember>
    {
        private readonly IEnumerable<BandMember> BandMembers = new List<BandMember>
        {
                new BandMember { Id = 1, BandId = 1, UserId = "seed-user-1", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 2, BandId = 1, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 3, BandId = 1, UserId = "seed-user-3", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 4, BandId = 2, UserId = "seed-user-2", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 5, BandId = 2, UserId = "seed-user-1", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 6, BandId = 2, UserId = "seed-user-3", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Vocals, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 7, BandId = 3, UserId = "seed-user-3", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 8, BandId = 3, UserId = "seed-user-1", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Saxophone, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 9, BandId = 3, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 10, BandId = 4, UserId = "seed-user-1", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 11, BandId = 4, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 12, BandId = 4, UserId = "seed-user-3", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 13, BandId = 5, UserId = "seed-user-2", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 14, BandId = 5, UserId = "seed-user-3", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 15, BandId = 5, UserId = "seed-user-1", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Vocals, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 16, BandId = 6, UserId = "seed-user-3", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 17, BandId = 6, UserId = "seed-user-1", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 18, BandId = 6, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 19, BandId = 7, UserId = "seed-user-1", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Synthesizer, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow },
                new BandMember { Id = 20, BandId = 7, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), InvitationToken = null, CreatedOn = DateTime.UtcNow }
        };
        public void Configure(EntityTypeBuilder<BandMember> entity)
        {
            entity.HasData(BandMembers);
        }
    }
}
