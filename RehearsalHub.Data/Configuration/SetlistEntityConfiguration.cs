using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Data.Models;

namespace RehearsalHub.Data.Configuration
{
    public class SetlistEntityConfiguration : IEntityTypeConfiguration<Setlist>
    {
        private readonly IEnumerable<Setlist> Setlists = new List<Setlist>
        {
            new Setlist { Id = 1, Name = "Rehearsal Set", BandId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 2, Name = "Live Show", BandId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 3, Name = "Practice Night", BandId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 4, Name = "Festival Set", BandId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 5, Name = "Warmup", BandId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 6, Name = "Main Set", BandId = 5, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 7, Name = "Encore Set", BandId = 6, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 8, Name = "Acoustic", BandId = 7, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 9, Name = "Heavy Set", BandId = 8, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 10, Name = "Chill Set", BandId = 9, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 11, Name = "Night Session", BandId = 10, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 12, Name = "Club Gig", BandId = 11, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 13, Name = "Studio Test", BandId = 12, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 14, Name = "Jam Session", BandId = 13, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 15, Name = "Road Trip", BandId = 14, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 16, Name = "Basement", BandId = 15, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 17, Name = "Open Air", BandId = 16, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 18, Name = "Late Show", BandId = 17, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 19, Name = "Soundcheck", BandId = 18, CreatedOn = DateTime.UtcNow, IsDeleted = false },
            new Setlist { Id = 20, Name = "Final Show", BandId = 19, CreatedOn = DateTime.UtcNow, IsDeleted = false }
        };

        public void Configure(EntityTypeBuilder<Setlist> entity)
        {
            entity.HasQueryFilter(s => !s.IsDeleted);

            entity.Property(s => s.IsDeleted)
                  .HasDefaultValue(false);

            entity.HasData(Setlists);
        }
    }
}