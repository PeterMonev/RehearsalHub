using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Invitation;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Tests.Helpers;
using Xunit;

namespace RehearsalHub.Tests.Services
{
    public class InvitationServiceTests
    {
        private readonly Mock<INotificationService> notificationMock;
        private readonly Mock<IHubContext<NotificationsHub>> hubContextMock;
        private readonly Mock<ILogger<InvitationService>> loggerMock;

        public InvitationServiceTests()
        {
            notificationMock = new Mock<INotificationService>();
            loggerMock = new Mock<ILogger<InvitationService>>();

            var mockClients = new Mock<IHubClients>();
            var mockClient = new Mock<IClientProxy>();
            mockClients.Setup(c => c.User(It.IsAny<string>())).Returns(mockClient.Object);

            hubContextMock = new Mock<IHubContext<NotificationsHub>>();
            hubContextMock.Setup(h => h.Clients).Returns(mockClients.Object);
        }

        private InvitationService CreateService(RehearsalHub.Data.ApplicationDbContext ctx)
            => new InvitationService(
                ctx,
                notificationMock.Object,
                hubContextMock.Object,
                loggerMock.Object);

        [Fact]
        public async Task GetPendingCountAsync_WithPendingInvitations_ReturnsCorrectCount()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var invite = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, invite);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(new BandMember
            {
                UserId = invite.Id,
                Role = BandRole.Member,
                Instrument = InstrumentType.Guitar,
                IsConfirmed = false,  
                IsDeleted = false,
                IsDeletedInvitation = false
            });
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var count = await service.GetPendingCountAsync(invite.Id);

        
            count.Should().Be(1);
        }

        [Fact]
        public async Task GetPendingCountAsync_WithNoPendingInvitations_ReturnsZero()
        {
       
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var count = await service.GetPendingCountAsync(user.Id);

        
            count.Should().Be(0);
        }

        [Fact]
        public async Task GetPendingCountAsync_WithEmptyUserId_ReturnsZero()
        {
       
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

        
            var count = await service.GetPendingCountAsync("");

        
            count.Should().Be(0);
        }

        [Fact]
        public async Task GetPendingCountAsync_AlreadyConfirmedInvitation_NotCounted()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var member = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, member);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            band.Members.Add(TestDataBuilder.CreateMember(member.Id, isConfirmed: true));
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var count = await service.GetPendingCountAsync(member.Id);

            count.Should().Be(0);
        }

        [Fact]
        public async Task SendInviteAsync_ToExistingUserByEmail_ReturnsTrue()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var target = TestDataBuilder.CreateUser(email: "target@test.com");
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, target);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var result = await service.SendInviteAsync(band.Id, "target@test.com", "Guitar");

        
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SendInviteAsync_ToNonExistentUser_ReturnsFalse()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.SendInviteAsync(band.Id, "nobody@nowhere.com", "Guitar");

        
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_WithEmptyTargetSearch_ReturnsFalse()
        {
       
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

        
            var result = await service.SendInviteAsync(1, "", "Guitar");

        
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_WithInvalidInstrument_ReturnsFalse()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var target = TestDataBuilder.CreateUser(email: "target@test.com");
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, target);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var result = await service.SendInviteAsync(band.Id, "target@test.com", "Theremin");

        
            result.Should().BeFalse();
        }

        [Fact]
        public async Task AcceptInviteAsync_WithValidPendingInvite_ReturnsBandId()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var invite = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, invite);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var pendingMember = new BandMember
            {
                UserId = invite.Id,
                BandId = band.Id,
                Role = BandRole.Member,
                Instrument = InstrumentType.Bass,
                IsConfirmed = false,
                IsDeleted = false,
                IsDeletedInvitation = false
            };
            context.BandMembers.Add(pendingMember);
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var result = await service.AcceptInviteAsync(pendingMember.Id, invite.Id);

            result.Should().Be(band.Id);

            var member = context.BandMembers.Find(pendingMember.Id);
            member!.IsConfirmed.Should().BeTrue();
        }

        [Fact]
        public async Task AcceptInviteAsync_WithNonExistentInvite_ReturnsNull()
        {
       
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

        
            var result = await service.AcceptInviteAsync(9999, "user-id");

        
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeclineInviteAsync_WithValidPendingInvite_MarksAsDeclinedAndReturnsTrue()
        {
       
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var invite = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.AddRange(owner, invite);
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            var pendingMember = new BandMember
            {
                UserId = invite.Id,
                BandId = band.Id,
                Role = BandRole.Member,
                Instrument = InstrumentType.Vocals,
                IsConfirmed = false,
                IsDeleted = false,
                IsDeletedInvitation = false
            };
            context.BandMembers.Add(pendingMember);
            await context.SaveChangesAsync();

            var service = CreateService(context);

        
            var result = await service.DeclineInviteAsync(pendingMember.Id, invite.Id);

        
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeclineInviteAsync_WithNonExistentInvite_ReturnsFalse()
        {
       
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

        
            var result = await service.DeclineInviteAsync(9999, "user-id");

        
            result.Should().BeFalse();
        }
    }
}
