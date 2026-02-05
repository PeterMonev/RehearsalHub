using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Models;
using RehearsalHub.Models.Enums;
using static RehearsalHub.Common.ImageConstants;


namespace RehearsalHub.Data.Configuration
{
    public class BandEntityConfiguration : IEntityTypeConfiguration<Band>
    {
        private readonly IEnumerable<Band> Bands = new List<Band>
        {
                new Band { Id = 1, Name = "RockStars", Genre = MusicGenre.Rock, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 2, Name = "MetalHeads", Genre = MusicGenre.Metal, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 3, Name = "Jazz Collective", Genre = MusicGenre.Jazz, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 4, Name = "Blues Brothers", Genre = MusicGenre.Blues, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 5, Name = "Funk Factory", Genre = MusicGenre.Funk, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 6, Name = "Urban Flow", Genre = MusicGenre.HipHop, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 7, Name = "ElectroWave", Genre = MusicGenre.Electronic, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 8, Name = "Pop Squad", Genre = MusicGenre.Pop, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 9, Name = "Heavy Unit", Genre = MusicGenre.Metal, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 10, Name = "Alternative Vibes", Genre = MusicGenre.Rock, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 11, Name = "Soul Train", Genre = MusicGenre.Funk, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 12, Name = "Night Jam", Genre = MusicGenre.Jazz, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 13, Name = "Garage Noise", Genre = MusicGenre.Rock, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 14, Name = "Dark Riffs", Genre = MusicGenre.Metal, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 15, Name = "Smooth Tones", Genre = MusicGenre.Blues, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 16, Name = "Stage Kings", Genre = MusicGenre.Rock, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 17, Name = "Groove Lab", Genre = MusicGenre.Funk, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 18, Name = "Beat Makers", Genre = MusicGenre.HipHop, OwnerId = "seed-user-3", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 19, Name = "Synth Storm", Genre = MusicGenre.Electronic, OwnerId = "seed-user-1", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow },
                new Band { Id = 20, Name = "Pop Nation", Genre = MusicGenre.Pop, OwnerId = "seed-user-2", ImageUrl = GetRandomBandImage(), CreatedOn = DateTime.UtcNow }
        };
        public void Configure(EntityTypeBuilder<Band> entity)
        {
            entity.HasData(Bands);
        }
    }
}
