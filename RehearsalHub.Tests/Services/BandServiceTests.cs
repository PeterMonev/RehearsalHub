
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
        public async Task CreateBandAsync_ValidInput_CreatesBandAndReturnsId()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "New Band",
                Genre = MusicGenre.Rock,
                SelectedInstrument = InstrumentType.Guitar,
                ImageUrl = "https://example.com/img.png"
            };

            var id = await service.CreateBandAsync(model, user.Id);

            id.Should().BeGreaterThan(0);
            context.Bands.Should().ContainSingle(b => b.Name == "New Band");
        }

        [Fact]
        public async Task CreateBandAsync_AddsOwnerAsMember()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "With Owner Member",
                Genre = MusicGenre.Jazz,
                SelectedInstrument = InstrumentType.Piano
            };

            var id = await service.CreateBandAsync(model, user.Id);

            var member = context.BandMembers.FirstOrDefault(bm => bm.BandId == id && bm.UserId == user.Id);
            member.Should().NotBeNull();
            member!.Role.Should().Be(BandRole.Owner);
            member.IsConfirmed.Should().BeTrue();
            member.Instrument.Should().Be(InstrumentType.Piano);
        }

        [Fact]
        public async Task CreateBandAsync_NullModel_ThrowsArgumentNullException()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.CreateBandAsync(null!, "user-id"));
        }

        [Fact]
        public async Task CreateBandAsync_EmptyOwnerId_ThrowsArgumentException()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.CreateBandAsync(new BandInputModel { Name = "X" }, ""));
        }

        [Fact]
        public async Task CreateBandAsync_NoImageUrl_AssignsRandomImage()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var model = new BandInputModel
            {
                Name = "No Image Band",
                Genre = MusicGenre.Blues,
                SelectedInstrument = InstrumentType.Guitar,
                ImageUrl = ""
            };

            var id = await service.CreateBandAsync(model, user.Id);
            var band = context.Bands.First(b => b.Id == id);

            band.ImageUrl.Should().NotBeNullOrEmpty();
        }

        // ───────────── DeleteBandAsync ─────────────

        [Fact]
        public async Task DeleteBandAsync_ByOwner_DeletesAndReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.DeleteBandAsync(band.Id, owner.Id);

                result.Should().BeTrue();
                context.Bands.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task DeleteBandAsync_ByNonOwner_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.DeleteBandAsync(band.Id, stranger.Id);

                result.Should().BeFalse();
                context.Bands.Should().ContainSingle();
            }
        }

        [Fact]
        public async Task DeleteBandAsync_NonExistentBand_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(9999, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_InvalidBandId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(0, "user-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_EmptyUserId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandAsync(1, "");

            result.Should().BeFalse();
        }

        // ───────────── GetBandsPagedAsync ─────────────

        [Fact]
        public async Task GetBandsPagedAsync_ReturnsOwnedBands()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Bands.AddRange(
                TestDataBuilder.CreateBand(user.Id, "My Band 1"),
                TestDataBuilder.CreateBand(user.Id, "My Band 2")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetBandsPagedAsync(user.Id, 1, 10);

            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBandsPagedAsync_ReturnsMemberBands()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var member = TestDataBuilder.CreateUser();
            context.Users.AddRange(owner, member);
            var band = TestDataBuilder.CreateBand(owner.Id, "Member's Band");
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            context.BandMembers.Add(new BandMember
            {
                BandId = band.Id,
                UserId = member.Id,
                Role = BandRole.Member,
                IsConfirmed = true,
                Instrument = InstrumentType.Drums
            });
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetBandsPagedAsync(member.Id, 1, 10);

            result.Items.Should().ContainSingle(b => b.Name == "Member's Band");
        }

        [Fact]
        public async Task GetBandsPagedAsync_EmptyUserId_ReturnsEmptyResult()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandsPagedAsync("", 1, 10);

            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task GetBandsPagedAsync_WithSearchTerm_FiltersCorrectly()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);
            context.Bands.AddRange(
                TestDataBuilder.CreateBand(user.Id, "Rock Legends"),
                TestDataBuilder.CreateBand(user.Id, "Jazz Masters"),
                TestDataBuilder.CreateBand(user.Id, "Rock Stars")
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetBandsPagedAsync(user.Id, 1, 10, searchTerm: "Rock");

            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBandsPagedAsync_Pagination_ReturnsCorrectPage()
        {
            using var context = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            context.Users.Add(user);

            for (int i = 1; i <= 12; i++)
            {
                context.Bands.Add(TestDataBuilder.CreateBand(user.Id, $"Band {i:D2}"));
            }
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetBandsPagedAsync(user.Id, 2, 5);

            result.Items.Should().HaveCount(5);
            result.TotalPages.Should().Be(3);
        }

        [Fact]
        public async Task GetBandsPagedAsync_UnconfirmedMember_NotIncluded()
        {
            using var context = TestDbContextFactory.Create();
            var owner = TestDataBuilder.CreateUser();
            var invited = TestDataBuilder.CreateUser();
            context.Users.AddRange(owner, invited);
            var band = TestDataBuilder.CreateBand(owner.Id, "Unconfirmed Band");
            context.Bands.Add(band);
            await context.SaveChangesAsync();

            context.BandMembers.Add(new BandMember
            {
                BandId = band.Id,
                UserId = invited.Id,
                Role = BandRole.Member,
                IsConfirmed = false,
                Instrument = InstrumentType.Guitar
            });
            await context.SaveChangesAsync();

            var service = CreateService(context);
            var result = await service.GetBandsPagedAsync(invited.Id, 1, 10);

            result.Items.Should().BeEmpty();
        }


        [Fact]
        public async Task GetBandEditAsync_ByOwner_ReturnsViewModel()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.GetBandEditAsync(band.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Id.Should().Be(band.Id);
                result.Name.Should().Be(band.Name);
            }
        }

        [Fact]
        public async Task GetBandEditAsync_ByNonOwner_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetBandEditAsync(band.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetBandEditAsync_EmptyUserId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandEditAsync(1, "");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandEditAsync_InvalidId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandEditAsync(0, "user-id");

            result.Should().BeNull();
        }

        // ───────────── EditBandAsync ─────────────

        [Fact]
        public async Task EditBandAsync_ByOwner_UpdatesAndReturnsTrue()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new BandEditViewModel
                {
                    Id = band.Id,
                    Name = "Updated Name",
                    Genre = MusicGenre.Metal,
                    ImageUrl = "https://example.com/new.png"
                };

                var result = await service.EditBandAsync(model, owner.Id);

                result.Should().BeTrue();
                var updated = context.Bands.First(b => b.Id == band.Id);
                updated.Name.Should().Be("Updated Name");
                updated.Genre.Should().Be(MusicGenre.Metal);
            }
        }

        [Fact]
        public async Task EditBandAsync_ByNonOwner_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var model = new BandEditViewModel
                {
                    Id = band.Id,
                    Name = "Hacked",
                    Genre = MusicGenre.Pop,
                    ImageUrl = "https://example.com/hack.png"
                };

                var result = await service.EditBandAsync(model, stranger.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task EditBandAsync_NullModel_ThrowsArgumentNullException()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.EditBandAsync(null!, "user-id"));
        }

        [Fact]
        public async Task EditBandAsync_EmptyUserId_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.EditBandAsync(new BandEditViewModel { Id = 1 }, "");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task EditBandAsync_NoImageUrl_AssignsRandomImage()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var model = new BandEditViewModel
                {
                    Id = band.Id,
                    Name = "Updated",
                    Genre = MusicGenre.Rock,
                    ImageUrl = ""
                };

                var result = await service.EditBandAsync(model, owner.Id);

                result.Should().BeTrue();
                context.Bands.First(b => b.Id == band.Id).ImageUrl.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public async Task GetBandDetailsAsync_ByOwner_ReturnsDetails()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.GetBandDetailsAsync(band.Id, owner.Id);

                result.Should().NotBeNull();
                result!.Name.Should().Be(band.Name);
                result.IsOwner.Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetBandDetailsAsync_ByConfirmedMember_ReturnsDetails()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
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
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetBandDetailsAsync(band.Id, member.Id);

                result.Should().NotBeNull();
                result!.IsOwner.Should().BeFalse();
            }
        }

        [Fact]
        public async Task GetBandDetailsAsync_ByStranger_ReturnsNull()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var stranger = TestDataBuilder.CreateUser();
                context.Users.Add(stranger);
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.GetBandDetailsAsync(band.Id, stranger.Id);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetBandDetailsAsync_EmptyUserId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandDetailsAsync(1, "");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandDetailsAsync_InvalidId_ReturnsNull()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.GetBandDetailsAsync(0, "user-id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_OwnerRemovesMember_ReturnsTrue()
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
                    Instrument = InstrumentType.Drums
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, owner.Id);

                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_MemberLeavesSelf_ReturnsTrue()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
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
                    Instrument = InstrumentType.Vocals
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, member.Id);

                result.Should().BeTrue();
            }
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_CannotRemoveOwner_ReturnsFalse()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.DeleteBandMembersDetailsAsync(band.Id, owner.Id, owner.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_StrangerTriesToRemoveMember_ReturnsFalse()
        {
            var (context, band, _) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var member = TestDataBuilder.CreateUser();
                var stranger = TestDataBuilder.CreateUser();
                context.Users.AddRange(member, stranger);
                context.BandMembers.Add(new BandMember
                {
                    BandId = band.Id,
                    UserId = member.Id,
                    Role = BandRole.Member,
                    IsConfirmed = true,
                    Instrument = InstrumentType.Guitar
                });
                await context.SaveChangesAsync();

                var service = CreateService(context);
                var result = await service.DeleteBandMembersDetailsAsync(band.Id, member.Id, stranger.Id);

                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_NonExistentBand_ReturnsFalse()
        {
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var result = await service.DeleteBandMembersDetailsAsync(9999, "member-id", "owner-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandMembersDetailsAsync_NonExistentMember_ReturnsFalse()
        {
            var (context, band, owner) = await SeedBandWithOwnerAsync();
            using (context)
            {
                var service = CreateService(context);
                var result = await service.DeleteBandMembersDetailsAsync(band.Id, "ghost-user", owner.Id);

                result.Should().BeFalse();
            }
        }

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
        public async Task EditBandAsync_WithNullModel_ThrowsArgumentNullException()
        {
          
            using var context = TestDbContextFactory.Create();
            var service = CreateService(context);

            var act = () => service.EditBandAsync(null!, "user-id");

            await act.Should().ThrowAsync<ArgumentNullException>();
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
