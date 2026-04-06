using FluentAssertions;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Songs;
using RehearsalHub.Tests.Helpers;
using RehearsalHub.Web.ViewModels.Song;
using Xunit;

namespace RehearsalHub.Tests.Services
{
    public class SongServiceTests
    {
        private SongService CreateService(RehearsalHub.Data.ApplicationDbContext ctx)
            => new SongService(ctx);

        [Fact]
        public async Task CreateSongAsync_WithValidData_ReturnsSongIdGreaterThanZero()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Title = "Master of Puppets",
                Artist = "Metallica",
                Duration = "08:35",
                Genre = MusicGenre.Metal,
                MusicalKey = MusicalKey.E,
                Tempo = 212,
                IsPrivate = false
            };


            var id = await service.CreateSongAsync(model, creator.Id);


            id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateSongAsync_WithValidData_PersistsAllFieldsCorrectly()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Title = "Comfortably Numb",
                Artist = "Pink Floyd",
                Duration = "06:23",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.B,
                Tempo = 63,
                IsPrivate = true
            };

            var id = await service.CreateSongAsync(model, creator.Id);

            var song = context.Songs.Find(id);
            song.Should().NotBeNull();
            song!.Title.Should().Be("Comfortably Numb");
            song.Artist.Should().Be("Pink Floyd");
            song.Genre.Should().Be(MusicGenre.Rock);
            song.MusicalKey.Should().Be(MusicalKey.B);
            song.Tempo.Should().Be(63);
            song.IsPrivate.Should().BeTrue();
            song.CreatorId.Should().Be(creator.Id);
        }

        [Fact]
        public async Task GetSongsPagedAsync_PublicSong_IsVisibleToAllUsers()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, otherUser);
            context.Songs.Add(TestDataBuilder.CreateSong(creator.Id, isPrivate: false));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetSongsPagedAsync(otherUser.Id, 1, 10);


            result.Songs.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetSongsPagedAsync_PrivateSong_IsVisibleOnlyToCreator()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, otherUser);
            context.Songs.Add(TestDataBuilder.CreateSong(creator.Id, isPrivate: true));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var creatorResult = await service.GetSongsPagedAsync(creator.Id, 1, 10);
            var otherUserResult = await service.GetSongsPagedAsync(otherUser.Id, 1, 10);

            creatorResult.Songs.Should().HaveCount(1);
            otherUserResult.Songs.Should().BeEmpty();
        }

        [Fact]
        public async Task GetSongsPagedAsync_FilterByGenre_ReturnsOnlyMatchingSongs()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Song 1", genre: MusicGenre.Metal),
                TestDataBuilder.CreateSong(user.Id, "Song 2", genre: MusicGenre.Jazz),
                TestDataBuilder.CreateSong(user.Id, "Song 3", genre: MusicGenre.Metal)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, genre: "Metal");


            result.Songs.Should().HaveCount(2);
            result.Songs.Should().OnlyContain(s => s.Genre == "Metal");
        }

        [Fact]
        public async Task GetSongsPagedAsync_FilterByMusicalKey_ReturnsOnlyMatchingSongs()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "In C", musicalKey: MusicalKey.C),
                TestDataBuilder.CreateSong(user.Id, "In G", musicalKey: MusicalKey.G),
                TestDataBuilder.CreateSong(user.Id, "Also C", musicalKey: MusicalKey.C)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, key: "C");


            result.Songs.Should().HaveCount(2);
        }

        [Theory]
        [InlineData("slow", 60, 1)] 
        [InlineData("medium", 100, 1)]
        [InlineData("fast", 180, 1)] 
        public async Task GetSongsPagedAsync_FilterByTempo_ReturnsCorrectSong(
            string tempoFilter, int tempo, int expectedCount)
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.Add(TestDataBuilder.CreateSong(user.Id, tempo: tempo));
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, tempo: tempoFilter);


            result.Songs.Should().HaveCount(expectedCount);
        }

        [Fact]
        public async Task GetSongsPagedAsync_TempoSlow_ReturnsOnlySongsBelowEighty()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Slow", tempo: 60),
                TestDataBuilder.CreateSong(user.Id, "Medium", tempo: 100),
                TestDataBuilder.CreateSong(user.Id, "Fast", tempo: 160)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, tempo: "slow");

            result.Songs.Should().ContainSingle();
            result.Songs.First().Title.Should().Be("Slow");
        }

        [Fact]
        public async Task GetSongsPagedAsync_TempoMedium_ReturnsSongsBetween80And120()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Slow", tempo: 60),
                TestDataBuilder.CreateSong(user.Id, "Med1", tempo: 80),
                TestDataBuilder.CreateSong(user.Id, "Med2", tempo: 120),
                TestDataBuilder.CreateSong(user.Id, "Fast", tempo: 121)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, tempo: "medium");

            result.Songs.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetSongsPagedAsync_SearchByTitle_ReturnsMatchingSongs()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Bohemian Rhapsody"),
                TestDataBuilder.CreateSong(user.Id, "Another One Bites"),
                TestDataBuilder.CreateSong(user.Id, "Radio Ga Ga")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, searchTerm: "Bohemian");


            result.Songs.Should().ContainSingle();
            result.Songs.First().Title.Should().Be("Bohemian Rhapsody");
        }

        [Fact]
        public async Task GetSongsPagedAsync_SearchByArtist_ReturnsMatchingSongs()
        {
        
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Song 1", artist: "Queen"),
                TestDataBuilder.CreateSong(user.Id, "Song 2", artist: "Metallica"),
                TestDataBuilder.CreateSong(user.Id, "Song 3", artist: "Queen")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetSongsPagedAsync(user.Id, 1, 10, searchTerm: "Queen");


            result.Songs.Should().HaveCount(2);
        }

        // ══════════════════════════════════════════════════════════════════════
        // DeleteSongAsync
        // ══════════════════════════════════════════════════════════════════════

        [Fact]
        public async Task DeleteSongAsync_ByCreator_DeletesSongAndReturnsTrue()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            var song = TestDataBuilder.CreateSong(creator.Id, "My Song");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.DeleteSongAsync(song.Id, creator.Id);


            result.Should().BeTrue();
            context.Songs.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteSongAsync_ByNonCreator_ReturnsFalse()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, otherUser);
            var song = TestDataBuilder.CreateSong(creator.Id);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.DeleteSongAsync(song.Id, otherUser.Id);


            result.Should().BeFalse();
            context.Songs.Should().HaveCount(1); 
        }

        [Fact]
        public async Task DeleteSongAsync_WithNonExistentId_ReturnsFalse()
        {
        
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);


            var result = await service.DeleteSongAsync(9999, "user-id");


            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSongAsync_ByCreator_UpdatesAllFieldsAndReturnsTrue()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            var song = TestDataBuilder.CreateSong(creator.Id, "Old Title", "Old Artist");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var updateModel = new SongInputModel
            {
                Id = song.Id,
                Title = "New Title",
                Artist = "New Artist",
                Duration = "04:00",
                Genre = MusicGenre.Jazz,
                MusicalKey = MusicalKey.A,
                Tempo = 90,
                IsPrivate = true
            };


            var result = await service.UpdateSongAsync(updateModel, creator.Id);


            result.Should().BeTrue();

            var updated = context.Songs.Find(song.Id);
            updated!.Title.Should().Be("New Title");
            updated.Artist.Should().Be("New Artist");
            updated.Genre.Should().Be(MusicGenre.Jazz);
            updated.IsPrivate.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateSongAsync_ByNonCreator_ReturnsFalse()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, otherUser);
            var song = TestDataBuilder.CreateSong(creator.Id, "Protected");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var updateModel = new SongInputModel
            {
                Id = song.Id,
                Title = "Hacked!",
                Artist = song.Artist,
                Duration = song.Duration,
                Genre = song.Genre,
                MusicalKey = song.MusicalKey
            };


            var result = await service.UpdateSongAsync(updateModel, otherUser.Id);


            result.Should().BeFalse();

            var unchanged = context.Songs.Find(song.Id);
            unchanged!.Title.Should().Be("Protected");
        }

        [Fact]
        public async Task GetSongDetailsAsync_WithExistingId_ReturnsCorrectDetails()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            var song = TestDataBuilder.CreateSong(creator.Id, "Wish You Were Here", "Pink Floyd");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongDetailsAsync(song.Id);


            result.Should().NotBeNull();
            result!.Title.Should().Be("Wish You Were Here");
            result.Artist.Should().Be("Pink Floyd");
        }

        [Fact]
        public async Task GetSongDetailsAsync_WithNonExistentId_ReturnsNull()
        {
        
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);


            var result = await service.GetSongDetailsAsync(9999);


            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongForEditAsync_WithPrivateSongByCreator_ReturnsInputModel()
        {
        
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            context.Users.Add(creator);
            var song = TestDataBuilder.CreateSong(creator.Id, isPrivate: true);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);


            var result = await service.GetSongForEditAsync(song.Id, creator.Id);


            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSongForEditAsync_WithNonExistentSong_ReturnsNull()
        {
        
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);


            var result = await service.GetSongForEditAsync(9999, "user-id");


            result.Should().BeNull();
        }
    }
}
