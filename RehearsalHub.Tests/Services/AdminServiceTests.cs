using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using RehearsalHub.Areas.Admin.Data;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Tests.Helpers;
using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Song;
using Xunit;

namespace RehearsalHub.Tests.Services
{
    public class AdminServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        private readonly Mock<ILogger<AdminService>> loggerMock;
        public AdminServiceTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            loggerMock = new Mock<ILogger<AdminService>>();
        }

        private AdminService CreateService(ApplicationDbContext context)
            => new AdminService(context, userManagerMock.Object, loggerMock.Object);

        [Fact]
        public async Task GetDashboardStatsAsync_ReturnsCorrectCounts()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var band = TestDataBuilder.CreateBand(user.Id);
            ctx.Bands.Add(band);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetDashboardStatsAsync();

            result.Should().NotBeNull();
            result.TotalUsers.Should().Be(1);
            result.TotalBands.Should().Be(1);
        }

        [Fact]
        public async Task GetBandsPagedAsync_NoBands_ReturnsEmptyPage()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetBandsPagedAsync(1, 10);

            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task GetBandsPagedAsync_WithBands_ReturnsMappedItems()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var band = TestDataBuilder.CreateBand(user.Id, "Test Band");
            ctx.Bands.Add(band);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetBandsPagedAsync(1, 10);

            result.TotalItems.Should().Be(1);
            result.Items.First().Name.Should().Be("Test Band");
        }

        [Fact]
        public async Task GetBandsPagedAsync_WithSearchTerm_FiltersResults()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            ctx.Bands.Add(TestDataBuilder.CreateBand(user.Id, "Metallica"));
            ctx.Bands.Add(TestDataBuilder.CreateBand(user.Id, "Nirvana"));
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetBandsPagedAsync(1, 10, "metal");

            result.TotalItems.Should().Be(1);
            result.Items.First().Name.Should().Be("Metallica");
        }

        [Fact]
        public async Task DeleteBandAsync_ExistingBand_ReturnsTrueAndRemovesBand()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var band = TestDataBuilder.CreateBand(user.Id, "To Delete");
            ctx.Bands.Add(band);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.DeleteBandAsync(band.Id);

            result.Should().BeTrue();
            ctx.Bands.Any(b => b.Id == band.Id).Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBandAsync_NonExistingBand_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.DeleteBandAsync(9999);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetBandForEditAsync_ExistingBand_ReturnsMappedViewModel()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var band = TestDataBuilder.CreateBand(user.Id, "Edit Me");
            ctx.Bands.Add(band);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetBandForEditAsync(band.Id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Edit Me");
            result.Id.Should().Be(band.Id);
        }

        [Fact]
        public async Task GetBandForEditAsync_InvalidId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetBandForEditAsync(0);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandForEditAsync_NonExistingBand_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetBandForEditAsync(42);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AdminEditBandAsync_ExistingBand_UpdatesAndReturnsTrue()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var band = TestDataBuilder.CreateBand(user.Id, "Old Name");
            ctx.Bands.Add(band);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var model = new BandEditViewModel
            {
                Id = band.Id,
                Name = "New Name",
                Genre = MusicGenre.Jazz,
                ImageUrl = "https://new.img/band.png"
            };

            var result = await svc.AdminEditBandAsync(model);

            result.Should().BeTrue();
            var updated = ctx.Bands.Find(band.Id);
            updated!.Name.Should().Be("New Name");
            updated.Genre.Should().Be(MusicGenre.Jazz);
        }

        [Fact]
        public async Task AdminEditBandAsync_NonExistingBand_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);
            var model = new BandEditViewModel { Id = 9999, Name = "Ghost", Genre = MusicGenre.Rock };

            var result = await svc.AdminEditBandAsync(model);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminEditBandAsync_NullModel_ThrowsArgumentNullException()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            await Assert.ThrowsAsync<ArgumentNullException>(() => svc.AdminEditBandAsync(null!));
        }

        [Fact]
        public async Task GetAllSongsPagedAsync_NoSongs_ReturnsEmptyPage()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetAllSongsPagedAsync(1, 10);

            result.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSongsPagedAsync_WithSearchTerm_FiltersResults()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            ctx.Songs.Add(TestDataBuilder.CreateSong(user.Id, "Bohemian Rhapsody", "Queen"));
            ctx.Songs.Add(TestDataBuilder.CreateSong(user.Id, "Enter Sandman", "Metallica"));
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetAllSongsPagedAsync(1, 10, "bohemian");

            result.TotalItems.Should().Be(1);
            result.Items.First().Title.Should().Be("Bohemian Rhapsody");
        }

        [Fact]
        public async Task AdminDeleteSongsAsync_ExistingNonDeletedSong_ReturnsTrueAndRemovesSong()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Kill This Song");
            ctx.Songs.Add(song);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.AdminDeleteSongsAsync(song.Id);

            result.Should().BeTrue();
            ctx.Songs.Any(s => s.Id == song.Id).Should().BeFalse();
        }

        [Fact]
        public async Task AdminDeleteSongsAsync_NonExistingSong_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.AdminDeleteSongsAsync(9999);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetSongForAdminEditAsync_ExistingSong_ReturnsMappedModel()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Fade to Black", "Metallica");
            ctx.Songs.Add(song);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetSongForAdminEditAsync(song.Id);

            result.Should().NotBeNull();
            result!.Title.Should().Be("Fade to Black");
            result.Artist.Should().Be("Metallica");
        }

        [Fact]
        public async Task GetSongForAdminEditAsync_InvalidId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetSongForAdminEditAsync(-1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongForAdminEditAsync_NonExistingSong_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.GetSongForAdminEditAsync(999);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AdminEditSongAsync_ExistingSong_UpdatesAndReturnsTrue()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            var song = TestDataBuilder.CreateSong(user.Id, "Old Title", "Old Artist");
            ctx.Songs.Add(song);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var model = new SongInputModel
            {
                Id = song.Id,
                Title = "New Title",
                Artist = "New Artist",
                Duration = "04:20",
                Genre = MusicGenre.Metal,
                MusicalKey = MusicalKey.Aminor,
                Tempo = 130,
                IsPrivate = false
            };

            var result = await svc.AdminEditSongAsync(model);

            result.Should().BeTrue();
            var updated = ctx.Songs.Find(song.Id);
            updated!.Title.Should().Be("New Title");
            updated.Artist.Should().Be("New Artist");
        }

        [Fact]
        public async Task AdminEditSongAsync_NullModel_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.AdminEditSongAsync(null!);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminEditSongAsync_NonExistingSong_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);
            var model = new SongInputModel
            {
                Id = 9999,
                Title = "Ghost",
                Artist = "Nobody",
                Duration = "03:00",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.C
            };

            var result = await svc.AdminEditSongAsync(model);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminCreateSongAsync_ValidModel_ReturnsNewSongId()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var model = new SongInputModel
            {
                Title = "Admin Song",
                Artist = "Admin Artist",
                Duration = "03:45",
                Genre = MusicGenre.Rock,
                MusicalKey = MusicalKey.G,
                Tempo = 120,
                IsPrivate = false
            };

            var id = await svc.AdminCreateSongAsync(model, user.Id);

            id.Should().BeGreaterThan(0);
            ctx.Songs.Any(s => s.Title == "Admin Song").Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUserAsync_CannotDeleteSelf_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.DeleteUserAsync(user.Id, user.Id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUserAsync_NonExistingUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.DeleteUserAsync("ghost-id", "admin-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUserAsync_ExistingUser_SoftDeletesAndReturnsTrue()
        {
            using var ctx = TestDbContextFactory.Create();
            var admin = TestDataBuilder.CreateUser();
            var target = TestDataBuilder.CreateUser();
            ctx.Users.AddRange(admin, target);
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.DeleteUserAsync(target.Id, admin.Id);

            result.Should().BeTrue();
            var deletedUser = await ctx.Users.FindAsync(target.Id);
            deletedUser!.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task PromoteUserAsync_NullOrEmptyUserId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.PromoteUserAsync("");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUserAsync_NonExistingUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.PromoteUserAsync("nonexistent-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUserAsync_ExistingUser_NotYetAdmin_CallsAddToRole()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            userManagerMock
                .Setup(m => m.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                .ReturnsAsync(false);
            userManagerMock
                .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var svc = CreateService(ctx);
            var result = await svc.PromoteUserAsync(user.Id);

            result.Should().BeTrue();
            userManagerMock.Verify(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"), Times.Once);
        }

        [Fact]
        public async Task PromoteUserAsync_AlreadyAdmin_ReturnsTrueWithoutAddingRole()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();

            userManagerMock
                .Setup(m => m.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                .ReturnsAsync(true);

            var svc = CreateService(ctx);
            var result = await svc.PromoteUserAsync(user.Id);

            result.Should().BeTrue();
            userManagerMock.Verify(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task DemoteUserAsync_CannotDemoteSelf_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var svc = CreateService(ctx);

            var result = await svc.DemoteUserAsync("same-id", "same-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUserAsync_NonExistingUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            userManagerMock
                .Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);

            var svc = CreateService(ctx);
            var result = await svc.DemoteUserAsync("ghost-id", "admin-id");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUserAsync_ExistingUser_CallsRemoveFromRole()
        {
            using var ctx = TestDbContextFactory.Create();
            var user = TestDataBuilder.CreateUser();

            userManagerMock
                .Setup(m => m.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            userManagerMock
                .Setup(m => m.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var svc = CreateService(ctx);
            var result = await svc.DemoteUserAsync(user.Id, "other-admin-id");

            result.Should().BeTrue();
            userManagerMock.Verify(m => m.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), "Admin"), Times.Once);
        }
    }
}
