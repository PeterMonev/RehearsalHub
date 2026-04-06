using FluentAssertions;
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
