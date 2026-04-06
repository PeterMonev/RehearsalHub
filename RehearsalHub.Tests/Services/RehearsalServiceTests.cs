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
