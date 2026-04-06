using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Setlists;
using RehearsalHub.Tests.Helpers;
using RehearsalHub.Web.ViewModels.Setlist;
using Xunit;

namespace RehearsalHub.Tests.Services
{
    public class SetlistServiceTests
    {
        private SetlistService CreateService(RehearsalHub.Data.ApplicationDbContext ctx)
            => new SetlistService(ctx);


        private async Task<(RehearsalHub.Data.ApplicationDbContext ctx, Band band, ApplicationUser owner)>
            SeedBandWithOwnerAsync()
        {
            var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);

            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(owner.Id, BandRole.Owner));
            await context.SaveChangesAsync();

            return (context, band, owner);
        }

        [Fact]
        public async Task GetSetlistDetailsAsync_ByOwner_ReturnsDetails()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Main Set");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistDetailsAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Name.Should().Be("Main Set");
                result.BandId.Should().Be(band.Id);
                result.CanEdit.Should().BeTrue();
                result.CanAddSongs.Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetSetlistDetailsAsync_ByMember_ReturnsDetailsWithoutEditPermission()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                context.Users.Add(member);
                band.Members.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));

                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Set For Member");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistDetailsAsync(setlist.Id, member.Id);

                result.Should().NotBeNull();
                result!.CanEdit.Should().BeFalse();
                result.CanAddSongs.Should().BeFalse();
            }
        }

        [Fact]
        public async Task GetSetlistDetailsAsync_ByStranger_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);

                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistDetailsAsync(setlist.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetSetlistDetailsAsync_NonExistentId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetSetlistDetailsAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSetlistDetailsAsync_WithSongs_ReturnsSongsAndTotalDuration()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Full Set");
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, "Song A", "Artist A");
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                context.SetlistSongs.Add(new SetlistSong { SetlistId = setlist.Id, SongId = song.Id });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistDetailsAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Songs.Should().ContainSingle();
                result.Songs[0].Title.Should().Be("Song A");
                result.TotalDuration.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public async Task GetSetlistForEditAsync_ByOwner_ReturnsModel()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Edit Me");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistForEditAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Name.Should().Be("Edit Me");
                result.BandId.Should().Be(band.Id);
            }
        }

        [Fact]
        public async Task GetSetlistForEditAsync_ByNonOwner_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetSetlistForEditAsync(setlist.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetSetlistForEditAsync_NonExistentSetlist_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetSetlistForEditAsync(9999, "user-id");

            result.Should().BeNull();
        }

        // ───────────── UpdateSetlistAsync ─────────────

        [Fact]
        public async Task UpdateSetlistAsync_ByOwner_UpdatesAndReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Old Name");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new SetlistInputModel
                {
                    Id = setlist.Id,
                    Name = "New Name",
                    BandId = band.Id,
                    RehearsalDate = DateTime.UtcNow.AddDays(7)
                };

                var result = await service.UpdateSetlistAsync(model, owner.Id);

                result.Should().BeTrue();
                context.Setlists.First(s => s.Id == setlist.Id).Name.Should().Be("New Name");
            }
        }

        [Fact]
        public async Task UpdateSetlistAsync_ByNonOwner_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Original");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new SetlistInputModel
                {
                    Id = setlist.Id,
                    Name = "Hacked Name",
                    BandId = band.Id
                };

                var result = await service.UpdateSetlistAsync(model, stranger.Id);

                result.Should().BeFalse();
                context.Setlists.First(s => s.Id == setlist.Id).Name.Should().Be("Original");
            }
        }

        [Fact]
        public async Task UpdateSetlistAsync_NonExistentSetlist_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.UpdateSetlistAsync(
                new SetlistInputModel { Id = 9999, Name = "X", BandId = 1 }, "user-id");

            result.Should().BeFalse();
        }

        // ───────────── GetAvailableSongsAsync ─────────────

        [Fact]
        public async Task GetAvailableSongsAsync_ByOwner_ReturnsViewModel()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "My Setlist");
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var publicSong = TestDataBuilder.CreateSong(owner.Id, "Public Song", isPrivate: false);
                var privateSong = TestDataBuilder.CreateSong(owner.Id, "Private Song", isPrivate: true, ownerBandId: band.Id);
                context.Songs.AddRange(publicSong, privateSong);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAvailableSongsAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.SetlistId.Should().Be(setlist.Id);
                result.AvailableSongs.Should().HaveCount(2);
            }
        }

        [Fact]
        public async Task GetAvailableSongsAsync_ByNonOwner_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAvailableSongsAsync(setlist.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetAvailableSongsAsync_NonExistentSetlist_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetAvailableSongsAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAvailableSongsAsync_SongsAlreadyInSetlist_MarkedAsAlreadyAdded()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, "In Setlist", isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                context.SetlistSongs.Add(new SetlistSong { SetlistId = setlist.Id, SongId = song.Id });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAvailableSongsAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.AvailableSongs.Should().ContainSingle(s => s.IsAlreadyInSetlist);
            }
        }

        [Fact]
        public async Task GetAvailableSongsAsync_PrivateSongOfOtherBand_NotIncluded()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var otherOwner = TestDataBuilder.CreateUser();
                var otherBand = TestDataBuilder.CreateBand(otherOwner.Id);
                context.Users.Add(otherOwner);
                context.Bands.Add(otherBand);

                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var privateSongOtherBand = TestDataBuilder.CreateSong(otherOwner.Id,
                    "Secret Song", isPrivate: true, ownerBandId: otherBand.Id);
                context.Songs.Add(privateSongOtherBand);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAvailableSongsAsync(setlist.Id, owner.Id);

                result.Should().NotBeNull();
                result!.AvailableSongs.Should().BeEmpty();
            }
        }

        // ───────────── AddSongsToSetlistAsync - edge cases ─────────────

        [Fact]
        public async Task AddSongsToSetlistAsync_EmptySongIdList_ReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AddSongsToSetlistAsync(setlist.Id, new List<int>(), owner.Id);

                result.Should().BeTrue();
                context.SetlistSongs.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_NonExistentSongId_SkipsAndReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AddSongsToSetlistAsync(setlist.Id, new List<int> { 9999 }, owner.Id);

                result.Should().BeTrue();
                context.SetlistSongs.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_NonExistentSetlist_ReturnsFalse()
        {
            var (context, _, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.AddSongsToSetlistAsync(9999, new List<int> { 1 }, owner.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_MultipleSongsAtOnce_AddsAll()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song1 = TestDataBuilder.CreateSong(owner.Id, "Song 1", isPrivate: false);
                var song2 = TestDataBuilder.CreateSong(owner.Id, "Song 2", isPrivate: false);
                var song3 = TestDataBuilder.CreateSong(owner.Id, "Song 3", isPrivate: false);
                context.Songs.AddRange(song1, song2, song3);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AddSongsToSetlistAsync(
                    setlist.Id, new List<int> { song1.Id, song2.Id, song3.Id }, owner.Id);

                result.Should().BeTrue();
                context.SetlistSongs.Should().HaveCount(3);
            }
        }

        // ───────────── RemoveSongFromSetlistAsync - edge cases ─────────────

        [Fact]
        public async Task RemoveSongFromSetlistAsync_NonExistentSetlist_ReturnsFalse()
        {
            var (context, _, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.RemoveSongFromSetlistAsync(9999, 1, owner.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task CanUserEditSetlistAsync_NonExistentSetlist_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.CanUserEditSetlistAsync(9999, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateSetlistAsync_ByOwner_ReturnsSetlistId()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new SetlistInputModel
                {
                    Name = "Set 1 — Rock Night",
                    BandId = band.Id
                };


                var id = await service.CreateSetlistAsync(model, owner.Id);


                id.Should().BeGreaterThan(0);
                context.Setlists.Should().ContainSingle(s => s.Name == "Set 1 — Rock Night");
            }
        }

        [Fact]
        public async Task CreateSetlistAsync_ByNonOwner_ThrowsUnauthorizedAccessException()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new SetlistInputModel
                {
                    Name = "Unauthorized Setlist",
                    BandId = band.Id
                };

                var act = () => service.CreateSetlistAsync(model, stranger.Id);


                await act.Should().ThrowAsync<UnauthorizedAccessException>();
            }
        }
        [Fact]
        public async Task DeleteSetlistAsync_ByOwner_ReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "Delete Me");
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.DeleteSetlistAsync(setlist.Id, owner.Id);


                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task DeleteSetlistAsync_ByNonOwner_ReturnsFalse()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.DeleteSetlistAsync(setlist.Id, stranger.Id);


                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteSetlistAsync_WithNonExistentId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteSetlistAsync(9999, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_ByOwner_AddsSongsAndReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.AddSongsToSetlistAsync(
                    setlist.Id, new List<int> { song.Id }, owner.Id);


                result.Should().BeTrue();
                context.SetlistSongs.Should().ContainSingle();
            }
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_DuplicateSong_SkipsAndReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                var service = CreateService(context);

                await service.AddSongsToSetlistAsync(setlist.Id, new List<int> { song.Id }, owner.Id);
                var result = await service.AddSongsToSetlistAsync(
                    setlist.Id, new List<int> { song.Id }, owner.Id);

                result.Should().BeTrue();
                context.SetlistSongs.Should().HaveCount(1);
            }
        }

        [Fact]
        public async Task AddSongsToSetlistAsync_ByNonOwner_ReturnsFalse()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(stranger.Id, isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.AddSongsToSetlistAsync(
                    setlist.Id, new List<int> { song.Id }, stranger.Id);


                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task RemoveSongFromSetlistAsync_ByOwner_RemovesSongAndReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                var setlistSong = new SetlistSong { SetlistId = setlist.Id, SongId = song.Id };
                context.SetlistSongs.Add(setlistSong);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.RemoveSongFromSetlistAsync(setlist.Id, song.Id, owner.Id);


                result.Should().BeTrue();
                context.SetlistSongs.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task RemoveSongFromSetlistAsync_ByNonOwner_ReturnsFalse()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, isPrivate: false);
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                var setlistSong = new SetlistSong { SetlistId = setlist.Id, SongId = song.Id };
                context.SetlistSongs.Add(setlistSong);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.RemoveSongFromSetlistAsync(setlist.Id, song.Id, stranger.Id);


                result.Should().BeFalse();
                context.SetlistSongs.Should().HaveCount(1); 
            }
        }

        [Fact]
        public async Task RemoveSongFromSetlistAsync_SongNotInSetlist_ReturnsFalse()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);

                var result = await service.RemoveSongFromSetlistAsync(setlist.Id, 999, owner.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task CanUserEditSetlistAsync_Owner_ReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.CanUserEditSetlistAsync(setlist.Id, owner.Id);


                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task CanUserEditSetlistAsync_NonOwner_ReturnsFalse()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);
                await context.SaveChangesAsync();

                var service = CreateService(context);


                var result = await service.CanUserEditSetlistAsync(setlist.Id, stranger.Id);


                result.Should().BeFalse();
            }
        }
    }
}
