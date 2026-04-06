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

        private async Task<(RehearsalHub.Data.ApplicationDbContext ctx, Band band, ApplicationUser owner)>
    SeedBandAsync()
        {
            var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var band = TestDataBuilder.CreateBand(owner.Id);
            context.Users.Add(owner);
            context.Bands.Add(band);
            await context.SaveChangesAsync();
            return (context, band, owner);
        }

        private InvitationService CreateService(RehearsalHub.Data.ApplicationDbContext ctx)
            => new InvitationService(
                ctx,
                notificationMock.Object,
                hubContextMock.Object,
                loggerMock.Object);


        [Fact]
        public async Task GetPendingCountAsync_EmptyUserId_ReturnsZero()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetPendingCountAsync("");

            result.Should().Be(0);
        }

        [Fact]
        public async Task GetPendingCountAsync_NullUserId_ReturnsZero()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetPendingCountAsync(null!);

            result.Should().Be(0);
        }

        [Fact]
        public async Task GetPendingCountAsync_WithMultiplePendingInvitations_ReturnsCorrectCount()
        {
            var (context, _, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);

                var owner2 = TestDataBuilder.CreateUser();
                var band2 = TestDataBuilder.CreateBand(owner2.Id);
                context.Users.Add(owner2);
                context.Bands.Add(band2);
                await context.SaveChangesAsync();

                context.BandMembers.Add(new BandMember
                {
                    BandId = context.Bands.First().Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });
                context.BandMembers.Add(new BandMember
                {
                    BandId = band2.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Bass,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetPendingCountAsync(user.Id);

                result.Should().Be(2);
            }
        }

        [Fact]
        public async Task GetPendingCountAsync_ConfirmedMembershipsNotCounted()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = true,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetPendingCountAsync(user.Id);

                result.Should().Be(0);
            }
        }

        [Fact]
        public async Task SendInviteAsync_ValidEmailLookup_CreatesInvitation()
        {
            var (context, band, owner) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, user.Email!, "Guitar");

                result.Should().BeTrue();
                context.BandMembers.Should().ContainSingle(bm =>
                    bm.UserId == user.Id && !bm.IsConfirmed);
            }
        }

        [Fact]
        public async Task SendInviteAsync_ValidUsernameLookup_CreatesInvitation()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, user.UserName!, "Bass");

                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task SendInviteAsync_EmptyTargetSearch_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.SendInviteAsync(1, "", "Guitar");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_EmptyInstrument_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.SendInviteAsync(1, "test@email.com", "");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_InvalidInstrumentType_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.SendInviteAsync(1, "test@email.com", "Trombone");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_UserNotFound_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, "ghost@ghost.com", "Guitar");

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task SendInviteAsync_BandNotFound_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.SendInviteAsync(9999, user.Email!, "Guitar");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendInviteAsync_OwnerInvitingThemselves_ReturnsFalse()
        {
            var (context, band, owner) = await SeedBandAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, owner.Email!, "Guitar");

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task SendInviteAsync_AlreadyActiveMember_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                context.Users.Add(member);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = member.Id,
                    IsConfirmed = true,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, member.Email!, "Guitar");

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task SendInviteAsync_AlreadyPendingInvitation_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, user.Email!, "Guitar");

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task SendInviteAsync_ReactivateDeclinedInvitation_ReturnsTrue()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);

                var declinedInvite = new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = true,
                    IsDeleted = true,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                };
                context.BandMembers.Add(declinedInvite);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.SendInviteAsync(band.Id, user.Email!, "Bass");

                result.Should().BeTrue();
                var reactivated = context.BandMembers.First(bm => bm.UserId == user.Id);
                reactivated.IsDeleted.Should().BeFalse();
                reactivated.IsDeletedInvitation.Should().BeFalse();
                reactivated.IsConfirmed.Should().BeFalse();
            }
        }

        [Fact]
        public async Task AcceptInviteAsync_ValidInvite_ConfirmsMembershipAndReturnsBandId()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                var invite = new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeleted = false,
                    IsDeletedInvitation = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                };
                context.BandMembers.Add(invite);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AcceptInviteAsync(invite.Id, user.Id);

                result.Should().Be(band.Id);
                context.BandMembers.First(bm => bm.Id == invite.Id).IsConfirmed.Should().BeTrue();
            }
        }

        [Fact]
        public async Task AcceptInviteAsync_EmptyUserId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.AcceptInviteAsync(1, "");

            result.Should().BeNull();
        }

        [Fact]
        public async Task AcceptInviteAsync_NullUserId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.AcceptInviteAsync(1, null!);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AcceptInviteAsync_NonExistentInvite_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.AcceptInviteAsync(9999, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task AcceptInviteAsync_AlreadyConfirmed_ReturnsNull()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                var invite = new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = true,
                    IsDeleted = false,
                    IsDeletedInvitation = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                };
                context.BandMembers.Add(invite);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AcceptInviteAsync(invite.Id, user.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task AcceptInviteAsync_WrongUser_ReturnsNull()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                var other = TestDataBuilder.CreateUser();
                context.Users.AddRange(user, other);
                var invite = new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeleted = false,
                    IsDeletedInvitation = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                };
                context.BandMembers.Add(invite);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.AcceptInviteAsync(invite.Id, other.Id);

                result.Should().BeNull();
            }
        }


        [Fact]
        public async Task DeclineInviteAsync_EmptyUserId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeclineInviteAsync(1, "");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeclineInviteAsync_NullUserId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeclineInviteAsync(1, null!);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeclineInviteAsync_NonExistentInvite_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeclineInviteAsync(9999, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeclineInviteAsync_WrongUser_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                var other = TestDataBuilder.CreateUser();
                context.Users.AddRange(user, other);
                var invite = new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeleted = false,
                    IsDeletedInvitation = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                };
                context.BandMembers.Add(invite);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.DeclineInviteAsync(invite.Id, other.Id);

                result.Should().BeFalse();
                context.BandMembers.First(bm => bm.Id == invite.Id).IsDeleted.Should().BeFalse();
            }
        }


        [Fact]
        public async Task GetPendingInvitationsAsync_EmptyUserId_ReturnsEmpty()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetPendingInvitationsAsync("");

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPendingInvitationsAsync_NullUserId_ReturnsEmpty()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetPendingInvitationsAsync(null!);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPendingInvitationsAsync_WithPendingInvites_ReturnsInvitations()
        {
            var (context, band, owner) = await SeedBandAsync();
            using (context)
            {
                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetPendingInvitationsAsync(user.Id);

                result.Should().ContainSingle();
            }
        }

        [Fact]
        public async Task GetPendingInvitationsAsync_OnlyReturnsPendingNotConfirmed()
        {
            var (context, band, _) = await SeedBandAsync();
            using (context)
            {
                var owner2 = TestDataBuilder.CreateUser();
                var band2 = TestDataBuilder.CreateBand(owner2.Id);
                context.Users.Add(owner2);
                context.Bands.Add(band2);

                var user = TestDataBuilder.CreateUser();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = user.Id,
                    IsConfirmed = true,
                    IsDeletedInvitation = true,
                    IsDeleted = false,
                    Instrument = InstrumentType.Guitar,
                    Role = BandRole.Member
                });

                context.BandMembers.Add(new BandMember
                {
                    BandId = band2.Id,
                    UserId = user.Id,
                    IsConfirmed = false,
                    IsDeletedInvitation = false,
                    IsDeleted = false,
                    Instrument = InstrumentType.Bass,
                    Role = BandRole.Member
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetPendingInvitationsAsync(user.Id);

                result.Should().ContainSingle();
            }
        }

        [Fact]
        public async Task GetPendingInvitationsAsync_UserWithNoInvitations_ReturnsEmpty()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetPendingInvitationsAsync(user.Id);

            result.Should().BeEmpty();
        }

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
