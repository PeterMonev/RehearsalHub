using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using RehearsalHub.Data;
using RehearsalHub.Data.Models.Models;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Tests.Helpers;
using Xunit;
namespace RehearsalHub.Tests.Services { 
public class NotificationServiceTests
{
    private static IHubContext<NotificationsHub> HubMock()
    {
        var clientsMock = new Mock<IHubClients>();
        var clientProxyMock = new Mock<IClientProxy>();
        clientsMock.Setup(c => c.User(It.IsAny<string>())).Returns(clientProxyMock.Object);
        var hubMock = new Mock<IHubContext<NotificationsHub>>();
        hubMock.Setup(h => h.Clients).Returns(clientsMock.Object);
        return hubMock.Object;
    }

    private static NotificationService Svc(ApplicationDbContext ctx)
        => new(ctx, HubMock(), new Mock<ILogger<NotificationService>>().Object);

    private static Notification MakeNotif(string userId, string message = "msg", bool isRead = false, string? url = null)
        => new()
        {
            RecipientId = userId,
            Message = message,
            IsRead = isRead,
            LinkUrl = url,
            CreatedOn = DateTime.UtcNow
        };


    [Fact]
    public async Task Create_EmptyUserId_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync("", "msg", null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_WhitespaceUserId_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync("   ", "msg", null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_NullUserId_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync(null!, "msg", null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_EmptyMessage_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync("u1", "", null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_WhitespaceMessage_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync("u1", "   ", null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_NullMessage_DoesNotSave()
    {
        using var ctx = TestDbContextFactory.Create();
        await Svc(ctx).CreateAsync("u1", null!, null);
        ctx.Notifications.Should().BeEmpty();
    }

    [Fact]
    public async Task GetNotifications_EmptyDb_ReturnsEmpty()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserNotificationsAsync("u1");
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetNotifications_EmptyUserId_ReturnsEmpty()
    {
        using var ctx = TestDbContextFactory.Create();
        ctx.Notifications.Add(MakeNotif("u1"));
        await ctx.SaveChangesAsync();

        var result = await Svc(ctx).GetUserNotificationsAsync("");

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetNotifications_NullUserId_ReturnsEmpty()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).GetUserNotificationsAsync(null!);
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetNotifications_WhitespaceUserId_ReturnsEmpty()
    {
        using var ctx = TestDbContextFactory.Create();
        ctx.Notifications.Add(MakeNotif("u1"));
        await ctx.SaveChangesAsync();

        var result = await Svc(ctx).GetUserNotificationsAsync("  ");

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Delete_NonExistentId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(9999, "u1");
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_EmptyUserId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(1, "");
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_NullUserId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(1, null!);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_ZeroNotificationId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(0, "u1");
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_NegativeNotificationId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(-1, "u1");
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_WhitespaceUserId_ReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var result = await Svc(ctx).DeleteNotificationAsync(1, "   ");
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_CalledTwice_SecondReturnsFalse()
    {
        using var ctx = TestDbContextFactory.Create();
        var n = MakeNotif("u1");
        ctx.Notifications.Add(n);
        await ctx.SaveChangesAsync();

        await Svc(ctx).DeleteNotificationAsync(n.Id, "u1");
        var secondResult = await Svc(ctx).DeleteNotificationAsync(n.Id, "u1");

        secondResult.Should().BeFalse();
    }


    [Fact]
    public async Task MarkAllAsRead_UnreadNotifications_MarksAllTrue()
    {
        using var ctx = TestDbContextFactory.Create();
        ctx.Notifications.AddRange(MakeNotif("u1"), MakeNotif("u1"), MakeNotif("u1"));
        await ctx.SaveChangesAsync();

        await Svc(ctx).MarkAllAsReadAsync("u1");

        ctx.Notifications.All(n => n.IsRead).Should().BeTrue();
    }

    [Fact]
    public async Task MarkAllAsRead_MixedReadAndUnread_AllBecomeRead()
    {
        using var ctx = TestDbContextFactory.Create();
        ctx.Notifications.Add(MakeNotif("u1", isRead: true));
        ctx.Notifications.Add(MakeNotif("u1", isRead: false));
        ctx.Notifications.Add(MakeNotif("u1", isRead: false));
        await ctx.SaveChangesAsync();

        await Svc(ctx).MarkAllAsReadAsync("u1");

        ctx.Notifications.All(n => n.IsRead).Should().BeTrue();
    }

    [Fact]
    public async Task MarkAllAsRead_NoNotificationsForUser_NoException()
    {
        using var ctx = TestDbContextFactory.Create();
        var ex = await Record.ExceptionAsync(() => Svc(ctx).MarkAllAsReadAsync("u1"));
        ex.Should().BeNull();
    }

    [Fact]
    public async Task MarkAllAsRead_EmptyDb_NoException()
    {
        using var ctx = TestDbContextFactory.Create();
        var ex = await Record.ExceptionAsync(() => Svc(ctx).MarkAllAsReadAsync("u1"));
        ex.Should().BeNull();
    }

    [Fact]
    public async Task MarkAllAsRead_LargeNumberOfNotifications_AllMarked()
    {
        using var ctx = TestDbContextFactory.Create();
        for (int i = 0; i < 100; i++)
            ctx.Notifications.Add(MakeNotif("u1", $"msg{i}", isRead: false));
        await ctx.SaveChangesAsync();

        await Svc(ctx).MarkAllAsReadAsync("u1");

        ctx.Notifications.All(n => n.IsRead).Should().BeTrue();
    }

}

}