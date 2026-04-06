using FluentAssertions;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Rehearsals;
using RehearsalHub.Tests.Helpers;
using RehearsalHub.Web.ViewModels.Rehearsal;
using Xunit;

namespace RehearsalHub.Tests.Services
{
    public class RehearsalServiceTests
    {
        private RehearsalService CreateService(RehearsalHub.Data.ApplicationDbContext ctx)
            => new RehearsalService(ctx);

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
        public async Task GetRehearsalDetailsAsync_ByOwner_ReturnsDetails()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id, "The Setlist");
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "Friday Session");
                rehearsal.Band = band;
                rehearsal.Setlist = setlist;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                rehearsal.SetlistId = setlist.Id;
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalDetailsAsync(rehearsal.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Name.Should().Be("Friday Session");
                result.BandId.Should().Be(band.Id);
                result.CanEdit.Should().BeTrue();
                result.CanDelete.Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetRehearsalDetailsAsync_ByMember_ReturnsDetailsWithoutEditPermission()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                context.Users.Add(member);
                context.BandMembers.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));
                var bm = context.BandMembers.Local.Last();
                var dbMember = new BandMember
                {
                    BandId = band.Id,
                    UserId = member.Id,
                    Role = BandRole.Member,
                    IsConfirmed = true,
                    Instrument = InstrumentType.Guitar
                };
                context.BandMembers.Add(dbMember);

                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "Member Test");
                rehearsal.Band = band;
                rehearsal.Setlist = setlist;
                rehearsal.SetlistId = setlist.Id;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalDetailsAsync(rehearsal.Id, member.Id);

                result.Should().NotBeNull();
                result!.CanEdit.Should().BeFalse();
                result.CanDelete.Should().BeFalse();
            }
        }

        [Fact]
        public async Task GetRehearsalDetailsAsync_ByStranger_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);

                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id);
                rehearsal.Band = band;
                rehearsal.Setlist = setlist;
                rehearsal.SetlistId = setlist.Id;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalDetailsAsync(rehearsal.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetRehearsalDetailsAsync_NonExistentId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetRehearsalDetailsAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRehearsalDetailsAsync_WithSongs_ReturnsSongsInDetails()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist = TestDataBuilder.CreateSetlist(band.Id);
                setlist.Band = band;
                context.Setlists.Add(setlist);

                var song = TestDataBuilder.CreateSong(owner.Id, "Rock Anthem");
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                context.SetlistSongs.Add(new SetlistSong { SetlistId = setlist.Id, SongId = song.Id });

                var rehearsal = new Rehearsal
                {
                    Name = "Band Night",
                    BandId = band.Id,
                    Band = band,
                    SetlistId = setlist.Id,
                    Setlist = setlist,
                    StartRehearsal = DateTime.UtcNow.AddDays(1),
                    EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(2),
                    CreatedOn = DateTime.UtcNow
                };
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalDetailsAsync(rehearsal.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Songs.Should().ContainSingle(s => s.Title == "Rock Anthem");
            }
        }

        [Fact]
        public async Task GetRehearsalForCreateAsync_ByOwner_ReturnsModel()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.GetRehearsalForCreateAsync(band.Id, owner.Id);

                result.Should().NotBeNull();
                result!.BandId.Should().Be(band.Id);
                result.BandName.Should().Be(band.Name);
            }
        }

        [Fact]
        public async Task GetRehearsalForCreateAsync_ByNonOwner_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalForCreateAsync(band.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetRehearsalForCreateAsync_NonExistentBand_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetRehearsalForCreateAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRehearsalForCreateAsync_WithSetlists_ReturnsAvailableSetlists()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var setlist1 = TestDataBuilder.CreateSetlist(band.Id, "Setlist A");
                var setlist2 = TestDataBuilder.CreateSetlist(band.Id, "Setlist B");
                context.Setlists.AddRange(setlist1, setlist2);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalForCreateAsync(band.Id, owner.Id);

                result.Should().NotBeNull();
                result!.AvailableSetlists.Should().HaveCount(2);
            }
        }

        [Fact]
        public async Task GetRehearsalForEditAsync_ByOwner_ReturnsModel()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "My Rehearsal");
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalForEditAsync(rehearsal.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Name.Should().Be("My Rehearsal");
                result.BandId.Should().Be(band.Id);
            }
        }

        [Fact]
        public async Task GetRehearsalForEditAsync_ByNonOwner_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "Secret");
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetRehearsalForEditAsync(rehearsal.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetRehearsalForEditAsync_NonExistentId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetRehearsalForEditAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateRehearsalAsync_ByOwner_UpdatesAndReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "Original Name");
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Id = rehearsal.Id,
                    Name = "Updated Name",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(2),
                    EndRehearsal = DateTime.UtcNow.AddDays(2).AddHours(3),
                    Notes = "Updated notes"
                };

                var result = await service.UpdateRehearsalAsync(model, owner.Id);

                result.Should().BeTrue();
                context.Rehearsals.First(r => r.Id == rehearsal.Id).Name.Should().Be("Updated Name");
                context.Rehearsals.First(r => r.Id == rehearsal.Id).Notes.Should().Be("Updated notes");
            }
        }

        [Fact]
        public async Task UpdateRehearsalAsync_ByNonOwner_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "Original");
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Id = rehearsal.Id,
                    Name = "Hacked",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(1),
                    EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(2)
                };

                var result = await service.UpdateRehearsalAsync(model, stranger.Id);

                result.Should().BeFalse();
                context.Rehearsals.First(r => r.Id == rehearsal.Id).Name.Should().Be("Original");
            }
        }

        [Fact]
        public async Task UpdateRehearsalAsync_NonExistentRehearsal_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.UpdateRehearsalAsync(new RehearsalInputModel
            {
                Id = 9999,
                Name = "X",
                BandId = 1,
                StartRehearsal = DateTime.UtcNow.AddDays(1),
                EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(2)
            }, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetAllUpcomingForUserAsync_ReturnsRehearsalsForOwnerAndMember()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                context.Users.Add(member);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = member.Id,
                    Role = BandRole.Member,
                    IsConfirmed = true,
                    Instrument = InstrumentType.Bass
                });

                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Upcoming 1", start: DateTime.Now.AddDays(1)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Upcoming 2", start: DateTime.Now.AddDays(3))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);

                var ownerResult = await service.GetAllUpcomingForUserAsync(owner.Id);
                ownerResult.Should().HaveCount(2);

                var memberResult = await service.GetAllUpcomingForUserAsync(member.Id);
                memberResult.Should().HaveCount(2);
            }
        }

        [Fact]
        public async Task GetAllUpcomingForUserAsync_ExcludesPastRehearsals()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Past",
                        start: DateTime.Now.AddDays(-5),
                        end: DateTime.Now.AddDays(-5).AddHours(2)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Future",
                        start: DateTime.Now.AddDays(2))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAllUpcomingForUserAsync(owner.Id);

                result.Should().ContainSingle();
                result[0].Name.Should().Be("Future");
            }
        }

        [Fact]
        public async Task GetAllUpcomingForUserAsync_Stranger_ReturnsEmpty()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                context.Rehearsals.Add(TestDataBuilder.CreateRehearsal(band.Id, start: DateTime.Now.AddDays(1)));
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAllUpcomingForUserAsync(stranger.Id);

                result.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task GetAllUpcomingForUserAsync_AreOrderedByStartTimeAscending()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "C", start: DateTime.Now.AddDays(10)),
                    TestDataBuilder.CreateRehearsal(band.Id, "A", start: DateTime.Now.AddDays(2)),
                    TestDataBuilder.CreateRehearsal(band.Id, "B", start: DateTime.Now.AddDays(5))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetAllUpcomingForUserAsync(owner.Id);

                result.Select(r => r.Name).Should().ContainInOrder("A", "B", "C");
            }
        }

        [Fact]
        public async Task GetPastRehearsalsAsync_AreOrderedByStartDescending()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Oldest",
                        start: DateTime.Now.AddDays(-10), end: DateTime.Now.AddDays(-10).AddHours(2)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Newest Past",
                        start: DateTime.Now.AddDays(-2), end: DateTime.Now.AddDays(-2).AddHours(2))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetPastRehearsalsAsync(band.Id, owner.Id);

                result.Select(r => r.Name).Should().ContainInOrder("Newest Past", "Oldest");
            }
        }

        [Fact]
        public async Task CreateRehearsalAsync_WithEndBeforeStart_ThrowsInvalidOperationException()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Name = "Bad Times",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(1).AddHours(3),
                    EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(1)
                };

                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    service.CreateRehearsalAsync(model, owner.Id));
            }
        }

        [Fact]
        public async Task CreateRehearsalAsync_WithPastStartDate_ThrowsInvalidOperationException()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Name = "Past Rehearsal",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(-1),
                    EndRehearsal = DateTime.UtcNow.AddDays(-1).AddHours(2)
                };

                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    service.CreateRehearsalAsync(model, owner.Id));
            }
        }

        [Fact]
        public async Task GetBandRehearsalsAsync_ByUnauthorizedUser_ReturnsEmptyList()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                context.Rehearsals.Add(TestDataBuilder.CreateRehearsal(band.Id));
                await context.SaveChangesAsync();

                var service = CreateService(context);

                var result = await service.GetBandRehearsalsAsync(band.Id, stranger.Id);

      
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task GetBandRehearsalsAsync_WithConfirmedMember_ReturnsRehearsals()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                context.Users.Add(member);
                band.Members.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));
                context.Rehearsals.Add(TestDataBuilder.CreateRehearsal(band.Id, "Member Rehearsal"));
                await context.SaveChangesAsync();

                var service = CreateService(context);

                var result = await service.GetBandRehearsalsAsync(band.Id, member.Id);

      
                result.Should().ContainSingle();
            }
        }

        [Fact]
        public async Task GetUpcomingRehearsalsAsync_ReturnsOnlyFutureRehearsals()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Past", start: DateTime.Now.AddDays(-3)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Future", start: DateTime.Now.AddDays(3))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.GetUpcomingRehearsalsAsync(band.Id, owner.Id);

                result.Should().ContainSingle();
                result.First().Name.Should().Be("Future");
            }
        }

        [Fact]
        public async Task GetUpcomingRehearsalsAsync_AreOrderedByStartTimeAscending()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Third", start: DateTime.Now.AddDays(10)),
                    TestDataBuilder.CreateRehearsal(band.Id, "First", start: DateTime.Now.AddDays(2)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Second", start: DateTime.Now.AddDays(5))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.GetUpcomingRehearsalsAsync(band.Id, owner.Id);

                result.Select(r => r.Name)
                      .Should().ContainInOrder("First", "Second", "Third");
            }
        }

        [Fact]
        public async Task GetUpcomingRehearsalsAsync_ByUnauthorizedUser_ReturnsEmptyList()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                context.Rehearsals.Add(
                    TestDataBuilder.CreateRehearsal(band.Id, start: DateTime.Now.AddDays(1)));
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.GetUpcomingRehearsalsAsync(band.Id, stranger.Id);

      
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task GetPastRehearsalsAsync_ReturnsOnlyPastRehearsals()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                context.Rehearsals.AddRange(
                    TestDataBuilder.CreateRehearsal(band.Id, "Old",
                        start: DateTime.Now.AddDays(-7),
                        end: DateTime.Now.AddDays(-7).AddHours(2)),
                    TestDataBuilder.CreateRehearsal(band.Id, "Future",
                        start: DateTime.Now.AddDays(3),
                        end: DateTime.Now.AddDays(3).AddHours(2))
                );
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.GetPastRehearsalsAsync(band.Id, owner.Id);

                result.Should().ContainSingle();
                result.First().Name.Should().Be("Old");
            }
        }

        [Fact]
        public async Task GetPastRehearsalsAsync_ByUnauthorizedUser_ReturnsEmptyList()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                context.Rehearsals.Add(TestDataBuilder.CreateRehearsal(band.Id,
                    start: DateTime.Now.AddDays(-5),
                    end: DateTime.Now.AddDays(-5).AddHours(2)));
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.GetPastRehearsalsAsync(band.Id, stranger.Id);

      
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task CreateRehearsalAsync_ByOwner_SavesRehearsalAndReturnsId()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Name = "Friday Jam",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(1),
                    EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(3),
                    Notes = "Bring extra strings"
                };

  
                var id = await service.CreateRehearsalAsync(model, owner.Id);

      
                id.Should().BeGreaterThan(0);
                context.Rehearsals.Should().ContainSingle(r => r.Name == "Friday Jam");
            }
        }

        [Fact]
        public async Task CreateRehearsalAsync_ByUnauthorizedUser_ThrowsException()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new RehearsalInputModel
                {
                    Name = "Hack Session",
                    BandId = band.Id,
                    StartRehearsal = DateTime.UtcNow.AddDays(1),
                    EndRehearsal = DateTime.UtcNow.AddDays(1).AddHours(2)
                };

                await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                    service.CreateRehearsalAsync(model, stranger.Id));
            }
        }

        [Fact]
        public async Task DeleteRehearsalAsync_ByOwner_ReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id, "To Delete");
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.DeleteRehearsalAsync(rehearsal.Id, owner.Id);

      
                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task DeleteRehearsalAsync_ByUnauthorizedUser_ReturnsFalse()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id);
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.DeleteRehearsalAsync(rehearsal.Id, stranger.Id);

      
                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteRehearsalAsync_WithNonExistentId_ReturnsFalse()
        {

            var (context, _, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);

  
                var result = await service.DeleteRehearsalAsync(9999, owner.Id);

      
                result.Should().BeFalse();
            }
        }

        [Fact]
        public void ValidateTimeRange_EndAfterStart_ReturnsTrue()
        {

            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);
            var start = DateTime.Now.AddHours(1);
            var end = DateTime.Now.AddHours(3);

         
            var result = service.ValidateTimeRange(start, end);

  
            result.Should().BeTrue();
        }

        [Fact]
        public void ValidateTimeRange_EndBeforeStart_ReturnsFalse()
        {

            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);
            var start = DateTime.Now.AddHours(3);
            var end = DateTime.Now.AddHours(1);

         
            var result = service.ValidateTimeRange(start, end);

  
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateTimeRange_EndEqualsStart_ReturnsFalse()
        {

            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);
            var time = DateTime.Now.AddHours(2);

            var result = service.ValidateTimeRange(time, time);

  
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateNotInPast_FutureDate_ReturnsTrue()
        {

            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

         
            var result = service.ValidateNotInPast(DateTime.Now.AddDays(1));

  
            result.Should().BeTrue();
        }

        [Fact]
        public void ValidateNotInPast_PastDate_ReturnsFalse()
        {

            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

         
            var result = service.ValidateNotInPast(DateTime.Now.AddDays(-1));

  
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CanUserEditRehearsalAsync_Owner_ReturnsTrue()
        {

            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id);
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.CanUserEditRehearsalAsync(rehearsal.Id, owner.Id);

      
                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task CanUserEditRehearsalAsync_NonOwner_ReturnsFalse()
        {

            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                var rehearsal = TestDataBuilder.CreateRehearsal(band.Id);
                rehearsal.Band = band;
                context.Rehearsals.Add(rehearsal);
                await context.SaveChangesAsync();

                var service = CreateService(context);

  
                var result = await service.CanUserEditRehearsalAsync(rehearsal.Id, stranger.Id);

      
                result.Should().BeFalse();
            }
        }
    }
}
