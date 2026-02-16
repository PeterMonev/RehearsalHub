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
            new BandMember { Id = 1, BandId = 1, UserId = "de305d54-75b4-4311-81d9-7ed39190224b", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 2, BandId = 1, UserId = "seed-user-2", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 3, BandId = 2, UserId = "seed-user-2", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 4, BandId = 2, UserId = "seed-user-3", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 5, BandId = 13, UserId = "seed-user-3", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 6, BandId = 3, UserId = "seed-user-4", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Saxophone, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 7, BandId = 4, UserId = "seed-user-4", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 8, BandId = 4, UserId = "seed-user-5", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 9, BandId = 5, UserId = "seed-user-5", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Vocals, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 10, BandId = 5, UserId = "seed-user-6", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 11, BandId = 6, UserId = "de305d54-75b4-4311-81d9-7ed39190224b", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 12, BandId = 6, UserId = "seed-user-7", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 13, BandId = 7, UserId = "seed-user-7", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Synthesizer, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 14, BandId = 7, UserId = "seed-user-8", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Vocals, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 15, BandId = 8, UserId = "seed-user-8", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Saxophone, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 16, BandId = 8, UserId = "seed-user-9", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 17, BandId = 9, UserId = "seed-user-9", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Guitar, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 18, BandId = 9, UserId = "seed-user-10", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Bass, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 19, BandId = 10, UserId = "seed-user-10", Role = BandRole.Owner, IsConfirmed = true, Instrument = InstrumentType.Keyboard, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new BandMember { Id = 20, BandId = 10, UserId = "seed-user-1", Role = BandRole.Member, IsConfirmed = true, Instrument = InstrumentType.Drums, AvatarUrl = GetRandomMemberImage(), CreatedOn = DateTime.UtcNow, IsDeleted = false }
        };

        public void Configure(EntityTypeBuilder<BandMember> entity)
        {
            entity.HasQueryFilter(m => !m.IsDeleted);


            entity.Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            entity.HasData(BandMembers);
        }
    }
}