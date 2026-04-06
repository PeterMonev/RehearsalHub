using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;
using RehearsalHub.Services.Data.Users;
using RehearsalHub.Tests.Helpers;
using Xunit;

namespace RehearsalHub.Tests.Services { 
public class UserServiceTests
{
    private static UserService Svc(ApplicationDbContext ctx)
        => new(ctx, new Mock<ILogger<UserService>>().Object);

    private static async Task<ApplicationUser> SeedFullUser(ApplicationDbContext ctx,
        string? id = null, string? name = null, string? email = null)
    {
        var u = TestDataBuilder.CreateUser(id, name, email);
        ctx.Users.Add(u);
        await ctx.SaveChangesAsync();
        return u;
    }

    private static async Task<Band> SeedBandWithMember(ApplicationDbContext ctx,
        ApplicationUser owner, string bandName, BandRole memberRole = BandRole.Owner,
        InstrumentType instrument = InstrumentType.Guitar, bool confirmed = true)
    {
        var band = TestDataBuilder.CreateBand(owner.Id, bandName);
        ctx.Bands.Add(band);
        await ctx.SaveChangesAsync();

        var member = new BandMember
        {
            UserId = owner.Id,
            BandId = band.Id,
            Role = memberRole,
            Instrument = instrument,
            IsConfirmed = confirmed
        };
        ctx.BandMembers.Add(member);
        await ctx.SaveChangesAsync();
        return band;
    }


    [Fact]
    public async Task GetUserProfile_NullId_ReturnsNull()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserProfileAsync(null!);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetUserProfile_EmptyId_ReturnsNull()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserProfileAsync("");
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetUserProfile_WhitespaceId_ReturnsNull()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserProfileAsync("   ");
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetUserProfile_NonExistentUser_ReturnsNull()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserProfileAsync("ghost-id");
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetUserProfile_ExistingUser_ReturnsProfile()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx, name: "john");
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUserProfile_ReturnsCorrectUserName()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx, name: "rockstar");
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result!.UserName.Should().Be("rockstar");
    }

    [Fact]
    public async Task GetUserProfile_ReturnsCorrectEmail()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx, email: "star@band.com");
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result!.Email.Should().Be("star@band.com");
    }

    [Fact]
    public async Task GetUserProfile_ReturnsProfilePictureUrl()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result!.ProfilePictureUrl.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetUserProfile_UserWithNoBands_ReturnEmptyBandList()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result!.Bands.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserProfile_UserWithConfirmedBand_ReturnsBandInList()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx, name: "bassist");
        await SeedBandWithMember(ctx, u, "My Band", confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.Should().HaveCount(1);
        result.Bands.First().BandName.Should().Be("My Band");
    }

    [Fact]
    public async Task GetUserProfile_UnconfirmedBandMembership_ExcludesFromBands()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        await SeedBandWithMember(ctx, u, "Unconfirmed Band", confirmed: false);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserProfile_MultipleBands_ReturnsAllConfirmed()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        await SeedBandWithMember(ctx, u, "Band One", confirmed: true);
        await SeedBandWithMember(ctx, u, "Band Two", confirmed: true);
        await SeedBandWithMember(ctx, u, "Band Three Pending", confirmed: false);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUserProfile_BandMembership_MapsRoleCorrectly()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        await SeedBandWithMember(ctx, u, "My Band", memberRole: BandRole.Owner, confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.First().Role.Should().Be("Owner");
    }

    [Fact]
    public async Task GetUserProfile_BandMembership_MapsInstrumentCorrectly()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        await SeedBandWithMember(ctx, u, "Band", instrument: InstrumentType.Drums, confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.First().Instrument.Should().Be("Drums");
    }

    [Fact]
    public async Task GetUserProfile_BandMembership_MapsBandIdCorrectly()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        var b = await SeedBandWithMember(ctx, u, "Band", confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.First().BandId.Should().Be(b.Id);
    }

    [Fact]
    public async Task GetUserProfile_BandMembership_MapsBandImageUrl()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        await SeedBandWithMember(ctx, u, "Band", confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.First().BandImageUrl.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetUserProfile_TwoUsersInDb_ReturnsCorrectOne()
    {
        using var ctx = TestDbContextFactory.Create();
        var u1 = await SeedFullUser(ctx, name: "userone");
        var u2 = await SeedFullUser(ctx, name: "usertwo");

        var result = await Svc(ctx).GetUserProfileAsync(u1.Id);

        result!.UserName.Should().Be("userone");
    }

    [Fact]
    public async Task GetUserProfile_PhoneNumber_NullWhenNotSet()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        var result = await Svc(ctx).GetUserProfileAsync(u.Id);
        result!.PhoneNumber.Should().BeNull();
    }

    [Fact]
    public async Task GetUserProfile_PhoneNumber_ReturnedWhenSet()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = TestDataBuilder.CreateUser();
        u.PhoneNumber = "+359888123456";
        ctx.Users.Add(u);
        await ctx.SaveChangesAsync();

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.PhoneNumber.Should().Be("+359888123456");
    }

    [Fact]
    public async Task GetUserProfile_AllBandMembershipsConfirmed_AllReturned()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        for (int i = 1; i <= 5; i++)
            await SeedBandWithMember(ctx, u, $"Band {i}", confirmed: true);

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        result!.Bands.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetUserProfile_MixInstruments_EachBandMapsOwnInstrument()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);

        var bandA = TestDataBuilder.CreateBand(u.Id, "Band A");
        ctx.Bands.Add(bandA);
        await ctx.SaveChangesAsync();
        ctx.BandMembers.Add(new BandMember { UserId = u.Id, BandId = bandA.Id, Role = BandRole.Member, Instrument = InstrumentType.Bass, IsConfirmed = true });

        var bandB = TestDataBuilder.CreateBand(u.Id, "Band B");
        ctx.Bands.Add(bandB);
        await ctx.SaveChangesAsync();
        ctx.BandMembers.Add(new BandMember { UserId = u.Id, BandId = bandB.Id, Role = BandRole.Member, Instrument = InstrumentType.Vocals, IsConfirmed = true });

        await ctx.SaveChangesAsync();

        var result = await Svc(ctx).GetUserProfileAsync(u.Id);

        var instruments = result!.Bands.Select(b => b.Instrument).ToList();
        instruments.Should().Contain("Bass");
        instruments.Should().Contain("Vocals");
    }

    [Fact]
    public async Task GetUserProfile_NoException_WhenUserHasDeletedBandMembers()
    {
        using var ctx = TestDbContextFactory.Create();
        var u = await SeedFullUser(ctx);
        var band = TestDataBuilder.CreateBand(u.Id, "Band");
        ctx.Bands.Add(band);
        await ctx.SaveChangesAsync();

        ctx.BandMembers.Add(new BandMember
        {
            UserId = u.Id,
            BandId = band.Id,
            Role = BandRole.Member,
            Instrument = InstrumentType.Other,
            IsConfirmed = false,
            IsDeleted = true
        });
        await ctx.SaveChangesAsync();

        var ex = await Record.ExceptionAsync(() => Svc(ctx).GetUserProfileAsync(u.Id));
        ex.Should().BeNull();
    }
}
}