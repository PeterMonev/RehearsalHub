using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RehearsalHub.Services.Data.Invitation;

/// <summary>
/// SignalR hub responsible for notifying users about their pending band invitations.
/// </summary>
public class NotificationsHub : Hub
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationsHub> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsHub"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve scoped services.</param>
    /// <param name="logger">Logger for recording hub events and errors.</param>
    public NotificationsHub(IServiceProvider serviceProvider, ILogger<NotificationsHub> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Called when a client connects to the hub.
    /// Logs the connection details.
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("User connected to SignalR Hub. UserId: {UserId}, ConnectionId: {ConnectionId}",
            userId, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// Sends the initial count of pending invitations to the connected client.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task GetInitialCount()
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("GetInitialCount invoked. UserId: {UserId}", userId);

        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("UserId is null or empty in GetInitialCount");
            await Clients.Caller.SendAsync("UpdateInviteCount", 0);
            return;
        }

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var invitationService = scope.ServiceProvider.GetRequiredService<IInvitationService>();
            var count = await invitationService.GetPendingCountAsync(userId);

            _logger.LogInformation("Sending invite count: {Count} to user: {UserId}", count, userId);
            await Clients.Caller.SendAsync("UpdateInviteCount", count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetInitialCount for user {UserId}", userId);
            await Clients.Caller.SendAsync("UpdateInviteCount", 0);
        }
    }

    /// <summary>
    /// Refreshes the list of pending invitations for the connected client.
    /// Sends the latest invitations to the caller.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task RefreshInvitations()
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("RefreshInvitations invoked. UserId: {UserId}", userId);

        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("UserId is null or empty in RefreshInvitations");
            return;
        }

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var invitationService = scope.ServiceProvider.GetRequiredService<IInvitationService>();
            var invitations = await invitationService.GetPendingInvitationsAsync(userId);

            _logger.LogInformation("Sending {Count} invitations to user: {UserId}", invitations.Count(), userId);
            await Clients.Caller.SendAsync("UpdateInvitations", invitations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in RefreshInvitations for user {UserId}", userId);
        }
    }
}
