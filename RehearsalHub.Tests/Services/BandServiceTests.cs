
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Bands;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Tests.Helpers;
using RehearsalHub.Web.ViewModels.Bands;
using Xunit;
using RehearsalHub.Data;

namespace RehearsalHub.Tests.Services
{
    public class BandServiceTests
    {
        private readonly Mock<INotificationService> notificationServiceMock;
        private readonly Mock<ILogger<BandService>> loggerMock;

        public BandServiceTests()
        {
            notificationServiceMock = new Mock<INotificationService>();
            loggerMock = new Mock<ILogger<BandService>>();
        }

        private BandService CreateService(ApplicationDbContext context)
            => new BandService(
                context,
                notificationServiceMock.Object,
                loggerMock.Object);

        [Fact]
        public async Task CreateBandAsync_WithValidModel_ReturnsBandIdGreaterThanZero()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "Metallica",
                Genre = MusicGenre.Metal,
                ImageUrl = "https://example.com/img.png",
                SelectedInstrument = InstrumentType.Guitar
            };

            var bandId = await service.CreateBandAsync(model, owner.Id);

            bandId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateBandAsync_WithValidModel_SavesBandToDatabase()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "Iron Maiden",
                Genre = MusicGenre.Metal,
                SelectedInstrument = InstrumentType.Guitar
            };

            var bandId = await service.CreateBandAsync(model, owner.Id);

            var savedBand = context.Bands.Find(bandId);
            savedBand.Should().NotBeNull();
            savedBand!.Name.Should().Be("Iron Maiden");
            savedBand.OwnerId.Should().Be(owner.Id);
        }

        [Fact]
        public async Task CreateBandAsync_WithValidModel_CreatesOwnerAsMemberWithOwnerRole()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "Test Band",
                Genre = MusicGenre.Rock,
                SelectedInstrument = InstrumentType.Drums
            };

            var bandId = await service.CreateBandAsync(model, owner.Id);

            var member = context.BandMembers
                .FirstOrDefault(m => m.BandId == bandId && m.UserId == owner.Id);

            member.Should().NotBeNull();
            member!.Role.Should().Be(BandRole.Owner);
            member.IsConfirmed.Should().BeTrue();
        }

        [Fact]
        public async Task CreateBandAsync_WithNullModel_ThrowsArgumentNullException()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var act = () => service.CreateBandAsync(null!, "user-id");

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateBandAsync_WithEmptyOwnerId_ThrowsArgumentException()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);
            var model = new BandInputModel { Name = "Band", Genre = MusicGenre.Rock };

            var act = () => service.CreateBandAsync(model, "");

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task DeleteBandAsync_ByBandOwner_ReturnsTrue()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandAsync(band.Id, owner.Id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBandAsync_ByNonOwner_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, otherUser);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandAsync(band.Id, otherUser.Id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_WithZeroBandId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(0, "some-user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_WithNegativeBandId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(-5, "some-user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_WithEmptyUserId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(1, "");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_WithNonExistentBandId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(9999, "some-user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetBandsPagedAsync_ReturnsOnlyBandsWhereUserIsMemberOrOwner()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var otherOwner = TestDataBuilder.CreateUser();
            context.Users.AddRange(owner, otherOwner);

            var myBand = TestDataBuilder.CreateBand(owner.Id, "My Band");
            var otherBand = TestDataBuilder.CreateBand(otherOwner.Id, "Other Band");
            context.Bands.AddRange(myBand, otherBand);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetBandsPagedAsync(owner.Id, 1, 10);

            result.Items.Should().ContainSingle();
            result.Items.First().Name.Should().Be("My Band");
        }

        [Fact]
        public async Task GetBandsPagedAsync_WithSearchTerm_FiltersCorrectly()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            context.Bands.AddRange(
                TestDataBuilder.CreateBand(owner.Id, "Metallica"),
                TestDataBuilder.CreateBand(owner.Id, "Iron Maiden"),
                TestDataBuilder.CreateBand(owner.Id, "Metal Church")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetBandsPagedAsync(owner.Id, 1, 10, "Metal");

            result.Items.Should().HaveCount(2);
            result.Items.Should().OnlyContain(b => b.Name.Contains("Metal"));
        }

        [Fact]
        public async Task GetBandsPagedAsync_WithEmptyUserId_ReturnsEmptyResult()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandsPagedAsync("", 1, 10);

            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task GetBandsPagedAsync_PaginationReturnsCorrectPage()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            for (int i = 1; i <= 7; i++)
                context.Bands.Add(TestDataBuilder.CreateBand(owner.Id, $"Band {i:D2}"));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var page1 = await service.GetBandsPagedAsync(owner.Id, 1, 3);

            page1.Items.Should().HaveCount(3);     
            page1.TotalItems.Should().Be(7);       
            page1.TotalPages.Should().Be(3);        
            page1.HasNextPage.Should().BeTrue();
            page1.HasPreviousPage.Should().BeFalse();
        }

        [Fact]
        public async Task GetBandsPagedAsync_LastPage_HasNoPreviousNextPage()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            context.Users.Add(owner);
            context.Bands.AddRange(
                TestDataBuilder.CreateBand(owner.Id, "Band 1"),
                TestDataBuilder.CreateBand(owner.Id, "Band 2")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var page2 = await service.GetBandsPagedAsync(owner.Id, 2, 1);

            page2.HasPreviousPage.Should().BeTrue();
            page2.HasNextPage.Should().BeFalse();
        }

        [Fact]
        public async Task GetBandEditAsync_ByOwner_ReturnsBandEditViewModel()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id, "Edit Me");
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);
           
            var result = await service.GetBandEditAsync(band.Id, owner.Id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Edit Me");
            result.Id.Should().Be(band.Id);
        }

        [Fact]
        public async Task GetBandEditAsync_ByNonOwner_ReturnsNull()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, otherUser);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetBandEditAsync(band.Id, otherUser.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandEditAsync_WithInvalidId_ReturnsNull()
        {
          
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandEditAsync(0, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandEditAsync_WithEmptyUserId_ReturnsNull()
        {
          
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandEditAsync(1, "");

            result.Should().BeNull();
        }

        [Fact]
        public async Task EditBandAsync_ByOwner_UpdatesNameAndReturnsTrue()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id, "Old Name");
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var editModel = new BandEditViewModel
            {
                Id = band.Id,
                Name = "New Name",
                Genre = MusicGenre.Jazz,
                ImageUrl = "https://new.png"
            };

            var result = await service.EditBandAsync(editModel, owner.Id);

            result.Should().BeTrue();

            var updated = context.Bands.Find(band.Id);
            updated!.Name.Should().Be("New Name");
            updated.Genre.Should().Be(MusicGenre.Jazz);
        }

        [Fact]
        public async Task EditBandAsync_ByNonOwner_ReturnsFalse()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var otherUser = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id, "Protected Band");
            context.Users.AddRange(owner, otherUser);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var editModel = new BandEditViewModel { Id = band.Id, Name = "Hacked!" };

            var result = await service.EditBandAsync(editModel, otherUser.Id);

            result.Should().BeFalse();

            var unchanged = context.Bands.Find(band.Id);
            unchanged!.Name.Should().Be("Protected Band");
        }

        [Fact]
        public async Task EditBandAsync_WithNullModel_ThrowsArgumentNullException()
        {
          
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var act = () => service.EditBandAsync(null!, "user-id");

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_OwnerRemovesMember_ReturnsTrue()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var member = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, member);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(owner.Id, BandRole.Owner));
            band.Members.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, owner.Id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_MemberLeavesThemself_ReturnsTrue()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var member = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, member);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(owner.Id, BandRole.Owner));
            band.Members.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, member.Id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_AttemptToRemoveOwner_ReturnsFalse()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(owner.Id, BandRole.Owner));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandMembersDetailsAsync(band.Id, owner.Id, owner.Id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_UnauthorizedUser_ReturnsFalse()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var member = TestDataBuilder.CreateUser();
            var randomUser = TestDataBuilder.CreateUser(); 
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, member, randomUser);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(owner.Id, BandRole.Owner));
            band.Members.Add(TestDataBuilder.CreateMember(member.Id, BandRole.Member));
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, randomUser.Id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetBandDetailsAsync_ByOwner_ReturnsDetails()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id, "Details Band");
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetBandDetailsAsync(band.Id, owner.Id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Details Band");
            result.IsOwner.Should().BeTrue();
        }

        [Fact]
        public async Task GetBandDetailsAsync_ByNonMember_ReturnsNull()
        {
          
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var stranger = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, stranger);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.GetBandDetailsAsync(band.Id, stranger.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandDetailsAsync_WithInvalidId_ReturnsNull()
        {
          
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandDetailsAsync(0, "user-id");

            result.Should().BeNull();
        }
    }
}
