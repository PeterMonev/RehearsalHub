using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
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

        private static async Task<ApplicationUser> SeedUser(ApplicationDbContext ctx,
            string? id = null, string? name = null, string? email = null, bool deleted = false)
        {
            var u = TestDataBuilder.CreateUser(id, name, email);
            u.IsDeleted = deleted;
            ctx.Users.Add(u);
            await ctx.SaveChangesAsync();
            return u;
        }

        private static async Task<Band> SeedBand(ApplicationDbContext ctx, string ownerId,
            string? name = null, bool deleted = false)
        {
            var b = TestDataBuilder.CreateBand(ownerId, name);
            b.IsDeleted = deleted;
            ctx.Bands.Add(b);
            await ctx.SaveChangesAsync();
            return b;
        }

        private static async Task<Song> SeedSong(ApplicationDbContext ctx, string creatorId,
            string? title = null, string? artist = null, bool deleted = false, bool isPrivate = false)
        {
            var s = TestDataBuilder.CreateSong(creatorId, title, artist, isPrivate);
            s.IsDeleted = deleted;
            ctx.Songs.Add(s);
            await ctx.SaveChangesAsync();
            return s;
        }

        [Fact]
        public async Task GetDashboardStats_EmptyDb_ReturnsAllZeroes()
        {

            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalUsers.Should().Be(0);
            result.TotalBands.Should().Be(0);
            result.TotalSongs.Should().Be(0);
            result.TotalRehearsals.Should().Be(0);
            result.TotalSetlists.Should().Be(0);
            result.NewUsersThisMonth.Should().Be(0);
            result.ActiveBands.Should().Be(0);
        }

        [Fact]
        public async Task GetDashboardStats_OneActiveUser_CountsOne()
        {
            using var ctx = TestDbContextFactory.Create();
            await SeedUser(ctx);
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalUsers.Should().Be(1);
        }

        [Fact]
        public async Task GetDashboardStats_OneBand_CountsOne()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id);
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalBands.Should().Be(1);
        }

        [Fact]
        public async Task GetDashboardStats_MultipleBands_CorrectCount()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Band A");
            await SeedBand(ctx, u.Id, "Band B");
            await SeedBand(ctx, u.Id, "Band C");
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalBands.Should().Be(3);
        }

        [Fact]
        public async Task GetDashboardStats_OneSong_CountsOne()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedSong(ctx, u.Id);
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalSongs.Should().Be(1);
        }



        [Fact]
        public async Task GetDashboardStats_NewUsersThisMonth_CountsCorrectly()
        {
            using var ctx = TestDbContextFactory.Create();
            var now = DateTime.UtcNow;
            var thisMonth = TestDataBuilder.CreateUser();
            thisMonth.CreatedOn = new DateTime(now.Year, now.Month, 1);
            var lastMonth = TestDataBuilder.CreateUser();
            lastMonth.CreatedOn = now.AddMonths(-1);
            ctx.Users.AddRange(thisMonth, lastMonth);
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.NewUsersThisMonth.Should().Be(1);
        }

        [Fact]
        public async Task GetDashboardStats_ReturnsNotNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetDashboardStats_TotalSetlists_CountsCorrectly()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id);
            ctx.Setlists.Add(TestDataBuilder.CreateSetlist(b.Id));
            ctx.Setlists.Add(TestDataBuilder.CreateSetlist(b.Id));
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetDashboardStatsAsync();
            result.TotalSetlists.Should().Be(2);
        }

        [Fact]
        public async Task GetUsersPaged_EmptyDb_ReturnsEmptyPage()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10);
            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task GetUsersPaged_SingleUser_ReturnsThatUser()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx, name: "alice");
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10);

            result.Items.Should().HaveCount(1);
            result.Items.First().UserName.Should().Be("alice");
        }

        [Fact]
        public async Task GetUsersPaged_SearchByUserName_FiltersCorrectly()
        {
            using var ctx = TestDbContextFactory.Create();
            var alice = await SeedUser(ctx, name: "alice_test");
            var bob = await SeedUser(ctx, name: "bob_test");
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10, "alice");

            result.TotalItems.Should().Be(1);
            result.Items.First().UserName.Should().Be("alice_test");
        }

        [Fact]
        public async Task GetUsersPaged_SearchByEmail_FiltersCorrectly()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = TestDataBuilder.CreateUser(email: "special@example.com");
            ctx.Users.Add(u);
            await ctx.SaveChangesAsync();
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10, "special");

            result.TotalItems.Should().Be(1);
            result.Items.First().Email.Should().Be("special@example.com");
        }

        [Fact]
        public async Task GetUsersPaged_SearchNoMatch_ReturnsEmpty()
        {
            using var ctx = TestDbContextFactory.Create();
            await SeedUser(ctx, name: "charlie");

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10, "zzznomatch");

            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task GetUsersPaged_AdminFlagSetForAdminUser()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx, name: "superadmin");
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(true);

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10);

            result.Items.First().IsAdmin.Should().BeTrue();
        }

        [Fact]
        public async Task GetUsersPaged_NullSearchTerm_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            for (int i = 0; i < 3; i++)
            {
                var u = TestDataBuilder.CreateUser();
                ctx.Users.Add(u);
                userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
                userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);
            }
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10, null);

            result.TotalItems.Should().Be(3);
        }

        [Fact]
        public async Task GetUsersPaged_EmptySearchTerm_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            for (int i = 0; i < 2; i++)
            {
                var u = TestDataBuilder.CreateUser();
                ctx.Users.Add(u);
                userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
                userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);
            }
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 10, "");

            result.TotalItems.Should().Be(2);
        }

        [Fact]
        public async Task GetUsersPaged_PageSizeOne_ReturnsOneItem()
        {
            using var ctx = TestDbContextFactory.Create();
            for (int i = 0; i < 5; i++)
            {
                var u = TestDataBuilder.CreateUser();
                ctx.Users.Add(u);
                userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
                userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);
            }
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetUsersPagedAsync(1, 1);

            result.Items.Should().HaveCount(1);
            result.TotalItems.Should().Be(5);
        }

        [Fact]
        public async Task GetBandsPaged_NoBands_ReturnsEmptyPage()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10);
            result.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBandsPaged_WithOneBand_ReturnsThatBand()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Metallica");

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10);

            result.TotalItems.Should().Be(1);
            result.Items.First().Name.Should().Be("Metallica");
        }

        [Fact]
        public async Task GetBandsPaged_SearchFiltersCorrectly()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Iron Maiden");
            await SeedBand(ctx, u.Id, "Black Sabbath");

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10, "iron");

            result.TotalItems.Should().Be(1);
            result.Items.First().Name.Should().Be("Iron Maiden");
        }

        [Fact]
        public async Task GetBandsPaged_SearchCaseInsensitive()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Nirvana");

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10, "NIRVANA");

            result.TotalItems.Should().Be(1);
        }

        [Fact]
        public async Task GetBandsPaged_PaginationWorks()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            for (int i = 1; i <= 10; i++)
                await SeedBand(ctx, u.Id, $"Band {i:D2}");

            var page1 = await CreateService(ctx).GetBandsPagedAsync(1, 4);
            var page2 = await CreateService(ctx).GetBandsPagedAsync(2, 4);
            var page3 = await CreateService(ctx).GetBandsPagedAsync(3, 4);

            page1.Items.Should().HaveCount(4);
            page2.Items.Should().HaveCount(4);
            page3.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBandsPaged_OrderedAlphabetically()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Zebra");
            await SeedBand(ctx, u.Id, "Aardvark");
            await SeedBand(ctx, u.Id, "Mojo");

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10);

            result.Items.Select(b => b.Name).Should().BeInAscendingOrder();
        }

        [Fact]
        public async Task GetBandsPaged_NullSearch_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedBand(ctx, u.Id, "Band A");
            await SeedBand(ctx, u.Id, "Band B");

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10, null);

            result.TotalItems.Should().Be(2);
        }

        [Fact]
        public async Task GetBandsPaged_GenreMappedToString()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = TestDataBuilder.CreateBand(u.Id, "Jazz Band", MusicGenre.Jazz);
            ctx.Bands.Add(b);
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetBandsPagedAsync(1, 10);

            result.Items.First().Genre.Should().Be("Jazz");
        }

        [Fact]
        public async Task PromoteUser_NullId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).PromoteUserAsync(null!);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUser_EmptyId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).PromoteUserAsync("");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUser_WhitespaceId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).PromoteUserAsync("   ");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUser_NonExistentUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).PromoteUserAsync("nonexistent-id");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task PromoteUser_AlreadyAdmin_ReturnsTrueWithoutCallingAdd()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(true);

            var result = await CreateService(ctx).PromoteUserAsync(u.Id);

            result.Should().BeTrue();
            userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task PromoteUser_NotAdmin_CallsAddToRole()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);
            userManagerMock.Setup(x => x.AddToRoleAsync(u, "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await CreateService(ctx).PromoteUserAsync(u.Id);

            result.Should().BeTrue();
            userManagerMock.Verify(x => x.AddToRoleAsync(u, "Admin"), Times.Once);
        }

        [Fact]
        public async Task PromoteUser_AddToRoleFails_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.IsInRoleAsync(u, "Admin")).ReturnsAsync(false);
            userManagerMock.Setup(x => x.AddToRoleAsync(u, "Admin"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Role error" }));

            var result = await CreateService(ctx).PromoteUserAsync(u.Id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUser_SelfDemotion_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DemoteUserAsync("admin-1", "admin-1");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUser_NonExistentUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            userManagerMock.Setup(x => x.FindByIdAsync("ghost")).ReturnsAsync((ApplicationUser?)null);

            var result = await CreateService(ctx).DemoteUserAsync("ghost", "current-admin");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUser_ExistingUser_CallsRemoveFromRole()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.RemoveFromRoleAsync(u, "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await CreateService(ctx).DemoteUserAsync(u.Id, "different-admin");

            result.Should().BeTrue();
            userManagerMock.Verify(x => x.RemoveFromRoleAsync(u, "Admin"), Times.Once);
        }

        [Fact]
        public async Task DemoteUser_RemoveFails_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.RemoveFromRoleAsync(u, "Admin"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "fail" }));

            var result = await CreateService(ctx).DemoteUserAsync(u.Id, "another");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DemoteUser_DifferentIds_NotSelfDemotion()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            userManagerMock.Setup(x => x.FindByIdAsync(u.Id)).ReturnsAsync(u);
            userManagerMock.Setup(x => x.RemoveFromRoleAsync(u, "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await CreateService(ctx).DemoteUserAsync(u.Id, "admin-X");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBand_ExistingBand_ReturnsTrueAndRemoves()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id, "To Delete");

            var result = await CreateService(ctx).DeleteBandAsync(b.Id);

            result.Should().BeTrue();
            ctx.Bands.Any(x => x.Id == b.Id).Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBand_NonExistentId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DeleteBandAsync(9999);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBand_ZeroId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DeleteBandAsync(0);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteBand_NegativeId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DeleteBandAsync(-5);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetAllSongsPaged_NoSongs_ReturnsEmpty()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10);
            result.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSongsPaged_IncludesPrivateSongs()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedSong(ctx, u.Id, isPrivate: true);

            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10);

            result.TotalItems.Should().Be(1);
        }

        [Fact]
        public async Task GetAllSongsPaged_SearchByTitle_Filters()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedSong(ctx, u.Id, title: "Stairway to Heaven");
            await SeedSong(ctx, u.Id, title: "Hotel California");

            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10, "stairway");

            result.TotalItems.Should().Be(1);
            result.Items.First().Title.Should().Be("Stairway to Heaven");
        }

        [Fact]
        public async Task GetAllSongsPaged_SearchByArtist_Filters()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedSong(ctx, u.Id, artist: "Led Zeppelin", title: "Song A");
            await SeedSong(ctx, u.Id, artist: "Eagles", title: "Song B");

            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10, "led");

            result.TotalItems.Should().Be(1);
        }

        [Fact]
        public async Task GetAllSongsPaged_SearchCaseInsensitive()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            await SeedSong(ctx, u.Id, title: "Bohemian Rhapsody");

            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10, "BOHEMIAN");

            result.TotalItems.Should().Be(1);
        }

        [Fact]
        public async Task GetAllSongsPaged_PaginationWorks()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            for (int i = 1; i <= 8; i++)
                await SeedSong(ctx, u.Id, title: $"Song {i:D2}");

            var page1 = await CreateService(ctx).GetAllSongsPagedAsync(1, 3);
            var page3 = await CreateService(ctx).GetAllSongsPagedAsync(3, 3);

            page1.Items.Should().HaveCount(3);
            page3.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllSongsPaged_MapsAllFields()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var song = new Song
            {
                Title = "Test",
                Artist = "Artist",
                Duration = "03:00",
                Genre = MusicGenre.Jazz,
                MusicalKey = MusicalKey.A,
                Tempo = 90,
                IsPrivate = false,
                CreatorId = u.Id,
                CreatedOn = DateTime.UtcNow
            };
            ctx.Songs.Add(song);
            await ctx.SaveChangesAsync();

            var result = await CreateService(ctx).GetAllSongsPagedAsync(1, 10);

            var item = result.Items.First();
            item.Title.Should().Be("Test");
            item.Artist.Should().Be("Artist");
            item.Genre.Should().Be("Jazz");
            item.Tempo.Should().Be(90);
        }



        [Fact]
        public async Task AdminDeleteSong_ExistingSong_ReturnsTrueAndDeletes()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var s = await SeedSong(ctx, u.Id, title: "Delete Me");

            var result = await CreateService(ctx).AdminDeleteSongsAsync(s.Id);

            result.Should().BeTrue();
            ctx.Songs.Any(x => x.Id == s.Id).Should().BeFalse();
        }

        [Fact]
        public async Task AdminDeleteSong_NonExistentId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).AdminDeleteSongsAsync(9999);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminDeleteSong_ZeroId_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).AdminDeleteSongsAsync(0);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUser_SelfDeletion_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DeleteUserAsync("admin1", "admin1");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_SoftDeletes()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);

            var result = await CreateService(ctx).DeleteUserAsync(u.Id, "other-admin");

            result.Should().BeTrue();
            var updated = await ctx.Users.FindAsync(u.Id);
            updated!.IsDeleted.Should().BeTrue();
            updated.DeletedOn.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteUser_NonExistentUser_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).DeleteUserAsync("ghost-id", "admin");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUser_SetsDeletedOnTimestamp()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var before = DateTime.UtcNow;

            await CreateService(ctx).DeleteUserAsync(u.Id, "admin");

            var updated = await ctx.Users.FindAsync(u.Id);
            updated!.DeletedOn.Should().BeOnOrAfter(before);
        }

        [Fact]
        public async Task GetBandForEdit_InvalidId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetBandForEditAsync(0);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandForEdit_NegativeId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetBandForEditAsync(-1);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBandForEdit_ExistingBand_ReturnsMappedModel()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id, "Existing Band");

            var result = await CreateService(ctx).GetBandForEditAsync(b.Id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Existing Band");
            result.Id.Should().Be(b.Id);
        }

        [Fact]
        public async Task GetBandForEdit_NonExistentId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetBandForEditAsync(999);
            result.Should().BeNull();
        }

        [Fact]
        public async Task AdminEditBand_NullModel_ThrowsArgumentNullException()
        {
            using var ctx = TestDbContextFactory.Create();
            await Assert.ThrowsAsync<ArgumentNullException>(() => CreateService(ctx).AdminEditBandAsync(null!));
        }

        [Fact]
        public async Task AdminEditBand_ExistingBand_UpdatesFields()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id, "Original");

            var model = new BandEditViewModel { Id = b.Id, Name = "Updated", Genre = MusicGenre.Metal, ImageUrl = "https://new.png" };
            var result = await CreateService(ctx).AdminEditBandAsync(model);

            result.Should().BeTrue();
            var updated = await ctx.Bands.FindAsync(b.Id);
            updated!.Name.Should().Be("Updated");
            updated.Genre.Should().Be(MusicGenre.Metal);
        }

        [Fact]
        public async Task AdminEditBand_NonExistentBand_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var model = new BandEditViewModel { Id = 999, Name = "X", Genre = MusicGenre.Rock };

            var result = await CreateService(ctx).AdminEditBandAsync(model);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminEditBand_NullImageUrl_KeepsOriginalImageUrl()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id, "Band");
            var original = b.ImageUrl;

            var model = new BandEditViewModel { Id = b.Id, Name = "New Name", Genre = b.Genre, ImageUrl = null };
            await CreateService(ctx).AdminEditBandAsync(model);

            var updated = await ctx.Bands.FindAsync(b.Id);
            updated!.ImageUrl.Should().Be(original);
        }

        [Fact]
        public async Task AdminEditBand_EmptyImageUrl_KeepsOriginalImageUrl()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id, "Band");
            var original = b.ImageUrl;

            var model = new BandEditViewModel { Id = b.Id, Name = "New Name", Genre = b.Genre, ImageUrl = "" };
            await CreateService(ctx).AdminEditBandAsync(model);

            var updated = await ctx.Bands.FindAsync(b.Id);
            updated!.ImageUrl.Should().Be(original);
        }


        [Fact]
        public async Task GetSongForAdminEdit_ZeroId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetSongForAdminEditAsync(0);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongForAdminEdit_NegativeId_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetSongForAdminEditAsync(-3);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSongForAdminEdit_ExistingSong_ReturnsMapped()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var s = await SeedSong(ctx, u.Id, title: "My Song", artist: "My Artist");

            var result = await CreateService(ctx).GetSongForAdminEditAsync(s.Id);

            result.Should().NotBeNull();
            result!.Title.Should().Be("My Song");
            result.Artist.Should().Be("My Artist");
        }

        [Fact]
        public async Task GetSongForAdminEdit_NonExistentSong_ReturnsNull()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).GetSongForAdminEditAsync(8888);
            result.Should().BeNull();
        }

        [Fact]
        public async Task AdminEditSong_NullModel_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await CreateService(ctx).AdminEditSongAsync(null!);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminEditSong_ExistingSong_UpdatesAllFields()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var s = await SeedSong(ctx, u.Id, title: "Old Title");

            var model = new SongInputModel
            {
                Id = s.Id,
                Title = "New Title",
                Artist = "New Artist",
                Duration = "04:00",
                Genre = MusicGenre.Jazz,
                MusicalKey = MusicalKey.B,
                Tempo = 100,
                IsPrivate = true
            };

            var result = await CreateService(ctx).AdminEditSongAsync(model);

            result.Should().BeTrue();
            var updated = ctx.Songs.Find(s.Id);
            updated!.Title.Should().Be("New Title");
            updated.Artist.Should().Be("New Artist");
            updated.Tempo.Should().Be(100);
            updated.IsPrivate.Should().BeTrue();
        }

        [Fact]
        public async Task AdminEditSong_NonExistentSong_ReturnsFalse()
        {
            using var ctx = TestDbContextFactory.Create();
            var model = new SongInputModel { Id = 9999, Title = "X", Artist = "Y", Duration = "01:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.C };

            var result = await CreateService(ctx).AdminEditSongAsync(model);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AdminCreateSong_ValidModel_ReturnsSongId()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);

            var model = new SongInputModel
            {
                Title = "Brand New",
                Artist = "Admin Artist",
                Duration = "03:00",
                Genre = MusicGenre.Pop,
                MusicalKey = MusicalKey.C,
                Tempo = 120,
                IsPrivate = false
            };

            var id = await CreateService(ctx).AdminCreateSongAsync(model, u.Id);

            id.Should().BeGreaterThan(0);
            ctx.Songs.Any(s => s.Id == id).Should().BeTrue();
        }

        [Fact]
        public async Task AdminCreateSong_SetsCreatorId()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var model = new SongInputModel { Title = "T", Artist = "A", Duration = "01:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.A };

            var id = await CreateService(ctx).AdminCreateSongAsync(model, u.Id);

            ctx.Songs.Find(id)!.CreatorId.Should().Be(u.Id);
        }

        [Fact]
        public async Task AdminCreateSong_SetsPrivateFlag()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var model = new SongInputModel { Title = "T", Artist = "A", Duration = "01:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.A, IsPrivate = true };

            var id = await CreateService(ctx).AdminCreateSongAsync(model, u.Id);

            ctx.Songs.Find(id)!.IsPrivate.Should().BeTrue();
        }

        [Fact]
        public async Task AdminCreateSong_WithBandId_SetsBandId()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var b = await SeedBand(ctx, u.Id);
            var model = new SongInputModel { Title = "T", Artist = "A", Duration = "01:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.A, BandId = b.Id };

            var id = await CreateService(ctx).AdminCreateSongAsync(model, u.Id);

            ctx.Songs.Find(id)!.OwnerBandId.Should().Be(b.Id);
        }

        [Fact]
        public async Task AdminCreateSong_TwoSongsHaveDifferentIds()
        {
            using var ctx = TestDbContextFactory.Create();
            var u = await SeedUser(ctx);
            var model1 = new SongInputModel { Title = "S1", Artist = "A", Duration = "01:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.A };
            var model2 = new SongInputModel { Title = "S2", Artist = "A", Duration = "02:00", Genre = MusicGenre.Rock, MusicalKey = MusicalKey.B };

            var id1 = await CreateService(ctx).AdminCreateSongAsync(model1, u.Id);
            var id2 = await CreateService(ctx).AdminCreateSongAsync(model2, u.Id);

            id1.Should().NotBe(id2);
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

    }
}
