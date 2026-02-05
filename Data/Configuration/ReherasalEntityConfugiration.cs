using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Models;

namespace RehearsalHub.Data.Configuration
{
    public class ReherasalEntityConfugiration : IEntityTypeConfiguration<Rehearsal>
    {
        private readonly IEnumerable<Rehearsal> Rehearsals = new List<Rehearsal>
        {
new Rehearsal { Id = 1, Name = "Evening Practice", BandId = 1, SetlistId = 1, StartRehearsal = DateTime.Parse("2024-05-20 18:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-20 20:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 2, Name = "Studio Jam", BandId = 2, SetlistId = 3, StartRehearsal = DateTime.Parse("2024-05-21 14:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-21 17:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 3, Name = "Sound Check", BandId = 3, SetlistId = 4, StartRehearsal = DateTime.Parse("2024-05-22 10:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-22 12:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 4, Name = "Full Band", BandId = 4, SetlistId = 5, StartRehearsal = DateTime.Parse("2024-05-23 19:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-23 21:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 5, Name = "Warmup", BandId = 5, SetlistId = 6, StartRehearsal = DateTime.Parse("2024-05-24 11:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-24 12:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 6, Name = "Late Night", BandId = 6, SetlistId = 7, StartRehearsal = DateTime.Parse("2024-05-25 22:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-26 00:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 7, Name = "Groove Session", BandId = 7, SetlistId = 8, StartRehearsal = DateTime.Parse("2024-05-26 15:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-26 18:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 8, Name = "Drum Focus", BandId = 8, SetlistId = 9, StartRehearsal = DateTime.Parse("2024-05-27 13:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-27 15:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 9, Name = "Vocal Practice", BandId = 9, SetlistId = 10, StartRehearsal = DateTime.Parse("2024-05-28 17:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-28 19:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow },
                new Rehearsal { Id = 10, Name = "Stage Run", BandId = 10, SetlistId = 11, StartRehearsal = DateTime.Parse("2024-05-29 16:00").ToUniversalTime(), EndRehearsal = DateTime.Parse("2024-05-29 19:00").ToUniversalTime(), CreatedOn = DateTime.UtcNow }
        };

        public void Configure(EntityTypeBuilder<Rehearsal> entity)
        {
            entity.HasData(Rehearsals);
        }
    }
}