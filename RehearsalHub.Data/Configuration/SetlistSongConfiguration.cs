using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Data.Models;

namespace RehearsalHub.Data.Configuration
{
    public class SetListSongEntityConfiguration : IEntityTypeConfiguration<SetlistSong>
    {
        private readonly IEnumerable<SetlistSong> SetListSongs = new List<SetlistSong>
        {
            new SetlistSong{ SetlistId=1, SongId=1 },
            new SetlistSong{ SetlistId=1, SongId=2 },
            new SetlistSong { SetlistId = 1, SongId = 3 },
            new SetlistSong{ SetlistId = 2, SongId = 4 },
            new SetlistSong { SetlistId = 2, SongId = 5 },
            new SetlistSong { SetlistId = 3, SongId = 6 },
            new SetlistSong { SetlistId = 3, SongId = 7 },
            new SetlistSong { SetlistId = 4, SongId = 8 },   
            new SetlistSong { SetlistId = 4, SongId = 9 },
            new SetlistSong { SetlistId = 5, SongId = 10 },
            new SetlistSong { SetlistId = 6, SongId = 11 },
            new SetlistSong { SetlistId = 7, SongId = 12 },
            new SetlistSong { SetlistId = 8, SongId = 13 },
            new SetlistSong { SetlistId = 9, SongId = 14 },
            new SetlistSong { SetlistId = 10, SongId = 15 },
            new SetlistSong { SetlistId = 11, SongId = 16 },
            new SetlistSong { SetlistId = 12, SongId = 17 },
            new SetlistSong { SetlistId = 13, SongId = 18 },
            new SetlistSong { SetlistId = 14, SongId = 19 },
            new SetlistSong { SetlistId = 15, SongId = 20 }
        };

        public void Configure(EntityTypeBuilder<SetlistSong> entity)
        {
            entity.HasData(SetListSongs);
        }
    }
}
