using FluentAssertions;
using RehearsalHub.Data.Models;
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
        public async Task CreateSongAsync_PublicSong_SavesAndReturnsId()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Title = "Summer Jam",
                Artist = "The Band",
                Duration = "04:20",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.G,
                Tempo = 130,
                IsPrivate = false
            };

            var id = await service.CreateSongAsync(model, user.Id);

            id.Should().BeGreaterThan(0);
            context.Songs.Should().ContainSingle(s => s.Title == "Summer Jam");
        }

        [Fact]
        public async Task CreateSongAsync_PrivateSong_SavesWithIsPrivateTrue()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(user.Id);
            context.Users.Add(user);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Title = "Secret Song",
                Artist = "Hidden",
                Duration = "03:00",
                Genre = MusicGenre.Metal,
                MusicalKey = MusicalKey.Eminor,
                Tempo = 90,
                IsPrivate = true,
                BandId = band.Id
            };

            var id = await service.CreateSongAsync(model, user.Id);
            var saved = context.Songs.First(s => s.Id == id);

            saved.IsPrivate.Should().BeTrue();
            saved.OwnerBandId.Should().Be(band.Id);
            saved.CreatorId.Should().Be(user.Id);
        }

        // ───────────── GetSongsPagedAsync ─────────────

        [Fact]
        public async Task GetSongsPagedAsync_WithoutFilters_ReturnsPublicAndOwnSongs()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            var other = TestDataBuilder.CreateUser();
            context.Users.AddRange(user, other);
            await context.SaveChangesAsync();

            var publicSong = TestDataBuilder.CreateSong(other.Id, "Public", isPrivate: false);
            var privateSong = TestDataBuilder.CreateSong(other.Id, "Private Other", isPrivate: true);
            var ownPrivate = TestDataBuilder.CreateSong(user.Id, "Own Private", isPrivate: true);
            context.Songs.AddRange(publicSong, privateSong, ownPrivate);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20);

            result.Songs.Should().HaveCount(2); // public + own private
            result.Songs.Select(s => s.Title).Should().Contain("Public");
            result.Songs.Select(s => s.Title).Should().Contain("Own Private");
            result.Songs.Select(s => s.Title).Should().NotContain("Private Other");
        }

        [Fact]
        public async Task GetSongsPagedAsync_WithSearchTerm_FiltersCorrectly()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Bohemian Rhapsody", "Queen"),
                TestDataBuilder.CreateSong(user.Id, "Hotel California", "Eagles"),
                TestDataBuilder.CreateSong(user.Id, "Queen of the Night", "Whitney")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, searchTerm: "queen");

            result.Songs.Should().HaveCount(2);
            result.SearchTerm.Should().Be("queen");
        }

        [Fact]
        public async Task GetSongsPagedAsync_WithGenreFilter_FiltersCorrectly()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Rock Song", genre: MusicGenre.Rock),
                TestDataBuilder.CreateSong(user.Id, "Jazz Song", genre: MusicGenre.Jazz),
                TestDataBuilder.CreateSong(user.Id, "Rock 2", genre: MusicGenre.Rock)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, genre: "Rock");

            result.Songs.Should().HaveCount(2);
            result.Songs.All(s => s.Genre == "Rock").Should().BeTrue();
        }

        [Fact]
        public async Task GetSongsPagedAsync_WithKeyFilter_FiltersCorrectly()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Key of C", musicalKey: MusicalKey.C),
                TestDataBuilder.CreateSong(user.Id, "Key of G", musicalKey: MusicalKey.G)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, key: "C");

            result.Songs.Should().ContainSingle(s => s.MusicalKey == "C");
        }

        [Fact]
        public async Task GetSongsPagedAsync_SlowTempoFilter_ReturnsOnlySlowSongs()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Ballad", tempo: 60),
                TestDataBuilder.CreateSong(user.Id, "Medium", tempo: 100),
                TestDataBuilder.CreateSong(user.Id, "Fast Track", tempo: 160)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, tempo: "slow");

            result.Songs.Should().ContainSingle(s => s.Title == "Ballad");
        }

        [Fact]
        public async Task GetSongsPagedAsync_MediumTempoFilter_ReturnsOnlyMediumSongs()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Slow", tempo: 60),
                TestDataBuilder.CreateSong(user.Id, "Medium", tempo: 100),
                TestDataBuilder.CreateSong(user.Id, "Fast", tempo: 160)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, tempo: "medium");

            result.Songs.Should().ContainSingle(s => s.Title == "Medium");
        }

        [Fact]
        public async Task GetSongsPagedAsync_FastTempoFilter_ReturnsOnlyFastSongs()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            context.Songs.AddRange(
                TestDataBuilder.CreateSong(user.Id, "Slow", tempo: 60),
                TestDataBuilder.CreateSong(user.Id, "Fast 1", tempo: 160),
                TestDataBuilder.CreateSong(user.Id, "Fast 2", tempo: 200)
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, tempo: "fast");

            result.Songs.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetSongsPagedAsync_WithBandIdFilter_ReturnsOnlyBandSongs()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(user.Id);
            context.Users.Add(user);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var bandSong = TestDataBuilder.CreateSong(user.Id, "Band Song", ownerBandId: band.Id);
            var otherSong = TestDataBuilder.CreateSong(user.Id, "No Band Song");
            context.Songs.AddRange(bandSong, otherSong);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20, bandId: band.Id);

            result.Songs.Should().ContainSingle(s => s.Title == "Band Song");
        }

        [Fact]
        public async Task GetSongsPagedAsync_Pagination_ReturnsCorrectPage()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            for (int i = 1; i <= 15; i++)
            {
                context.Songs.Add(TestDataBuilder.CreateSong(user.Id, $"Song {i:D2}"));
            }
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 2, 5);

            result.Songs.Should().HaveCount(5);
            result.CurrentPage.Should().Be(2);
            result.TotalPages.Should().Be(3);
        }

        [Fact]
        public async Task GetSongsPagedAsync_EmptyDatabase_ReturnsEmptyResult()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetSongsPagedAsync("user-id", 1, 10);

            result.Songs.Should().BeEmpty();
            result.TotalPages.Should().Be(0);
        }

        [Fact]
        public async Task GetSongsPagedAsync_CanEdit_TrueForCreator()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "My Song", isPrivate: false);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(user.Id, 1, 20);

            result.Songs.Should().ContainSingle(s => s.CanEdit);
        }

        [Fact]
        public async Task GetSongsPagedAsync_CanEdit_FalseForNonCreator()
        {
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var viewer = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, viewer);
            var song = TestDataBuilder.CreateSong(creator.Id, "Not Mine", isPrivate: false);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongsPagedAsync(viewer.Id, 1, 20);

            result.Songs.Should().ContainSingle(s => !s.CanEdit);
        }

        // ───────────── GetSongDetailsAsync ─────────────

        [Fact]
        public async Task GetSongDetailsAsync_ExistingSong_ReturnsViewModel()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Details Song", "The Artist");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongDetailsAsync(song.Id);

            result.Should().NotBeNull();
            result!.Title.Should().Be("Details Song");
            result.Artist.Should().Be("The Artist");
            result.CreatorId.Should().Be(user.Id);
        }

        [Fact]
        public async Task GetSongDetailsAsync_NonExistentId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetSongDetailsAsync(9999);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongDetailsAsync_SongInSetlists_ReturnsSetlistInfo()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(user.Id);
            context.Users.Add(user);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var song = TestDataBuilder.CreateSong(user.Id, "Setlist Song");
            context.Songs.Add(song);

            var setlist = TestDataBuilder.CreateSetlist(band.Id, "My Setlist");
            setlist.Band = band;
            context.Setlists.Add(setlist);
            await context.SaveChangesAsync();

            context.SetlistSongs.Add(new SetlistSong { SetlistId = setlist.Id, SongId = song.Id });
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongDetailsAsync(song.Id);

            result.Should().NotBeNull();
            result!.IncludedInSetlists.Should().ContainSingle(s => s.Name == "My Setlist");
        }

        // ───────────── DeleteSongAsync ─────────────

        [Fact]
        public async Task DeleteSongAsync_ByCreator_DeletesAndReturnsTrue()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "To Delete");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.DeleteSongAsync(song.Id, user.Id);

            result.Should().BeTrue();
            context.Songs.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteSongAsync_ByBandOwner_DeletesAndReturnsTrue()
        {
            using var context = TestDbContextFactory.Create();
            var bandOwner = TestDataBuilder.CreateUser();
            var creator = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(bandOwner.Id);
            context.Users.AddRange(bandOwner, creator);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var song = TestDataBuilder.CreateSong(creator.Id, "Band Song", ownerBandId: band.Id);
            song.OwnerBand = band;
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.DeleteSongAsync(song.Id, bandOwner.Id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteSongAsync_ByStranger_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var stranger = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, stranger);
            var song = TestDataBuilder.CreateSong(creator.Id, "Protected Song");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.DeleteSongAsync(song.Id, stranger.Id);

            result.Should().BeFalse();
            context.Songs.Should().ContainSingle();
        }

        [Fact]
        public async Task DeleteSongAsync_NonExistentId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteSongAsync(9999, "user-id");

            result.Should().BeFalse();
        }

        // ───────────── GetSongForEditAsync ─────────────

        [Fact]
        public async Task GetSongForEditAsync_PrivateSong_ReturnsModel()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Private Edit", isPrivate: true);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongForEditAsync(song.Id, user.Id);

            result.Should().NotBeNull();
            result!.Title.Should().Be("Private Edit");
            result.IsPrivate.Should().BeTrue();
        }

        [Fact]
        public async Task GetSongForEditAsync_PublicSong_ReturnsNull()
        {
            // By current implementation, non-private songs return null
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Public Edit", isPrivate: false);
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetSongForEditAsync(song.Id, user.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongForEditAsync_NonExistentSong_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetSongForEditAsync(9999, "user-id");

            result.Should().BeNull();
        }

        // ───────────── UpdateSongAsync ─────────────

        [Fact]
        public async Task UpdateSongAsync_ByCreator_UpdatesAndReturnsTrue()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Old Title");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Id = song.Id,
                Title = "New Title",
                Artist = "New Artist",
                Duration = "05:00",
                Genre = MusicGenre.Jazz,
                MusicalKey = MusicalKey.D,
                Tempo = 110,
                IsPrivate = false
            };

            var result = await service.UpdateSongAsync(model, user.Id);

            result.Should().BeTrue();
            var updated = context.Songs.First(s => s.Id == song.Id);
            updated.Title.Should().Be("New Title");
            updated.Artist.Should().Be("New Artist");
            updated.Genre.Should().Be(MusicGenre.Jazz);
        }

        [Fact]
        public async Task UpdateSongAsync_ByStranger_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var creator = TestDataBuilder.CreateUser();
            var stranger = TestDataBuilder.CreateUser();
            context.Users.AddRange(creator, stranger);
            var song = TestDataBuilder.CreateSong(creator.Id, "Original");
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Id = song.Id,
                Title = "Hacked",
                Artist = "Hacker",
                Duration = "01:00",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.C,
                Tempo = 100,
                IsPrivate = false
            };

            var result = await service.UpdateSongAsync(model, stranger.Id);

            result.Should().BeFalse();
            context.Songs.First(s => s.Id == song.Id).Title.Should().Be("Original");
        }

        [Fact]
        public async Task UpdateSongAsync_NonExistentSong_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.UpdateSongAsync(new SongInputModel
            {
                Id = 9999,
                Title = "X",
                Artist = "Y",
                Duration = "01:00",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.C,
                Tempo = 100
            }, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSongAsync_ByBandOwner_UpdatesAndReturnsTrue()
        {
            using var context = TestDbContextFactory.Create();
            var bandOwner = TestDataBuilder.CreateUser();
            var creator = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(bandOwner.Id);
            context.Users.AddRange(bandOwner, creator);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var song = TestDataBuilder.CreateSong(creator.Id, "Band Song", ownerBandId: band.Id);
            song.OwnerBand = band;
            context.Songs.Add(song);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new SongInputModel
            {
                Id = song.Id,
                Title = "Updated by Owner",
                Artist = "Owner",
                Duration = "03:00",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.C,
                Tempo = 120
            };

            var result = await service.UpdateSongAsync(model, bandOwner.Id);

            result.Should().BeTrue();
        }
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
