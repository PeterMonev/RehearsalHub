// Band Edit Image Preview
document.getElementById('imageUrlInput')?.addEventListener('input', function () {
    const preview = document.getElementById('preview');
    if (preview) {
        preview.src = this.value;
    }
});

// SignalR Connection (GLOBAL)
console.log("Initializing SignalR connection...");

window.connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationsHub")
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("UpdateInviteCount", function (count) {
    console.log("✅ SignalR: Received invite count:", count);

    var badge = document.getElementById("notification-badge");
    var wrapper = document.getElementById("notifications-wrapper");

    if (badge) {
        badge.innerText = count;
        if (count > 0) {
            badge.classList.remove("d-none");
        } else {
            badge.classList.add("d-none");
        }
    }

    if (wrapper) {
        if (count > 0) {
            wrapper.innerHTML = `<li><a class="dropdown-item text-center py-2" href="/Invitation/Index"><span class="badge bg-danger">${count}</span> Pending Invitations</a></li>`;
        } else {
            wrapper.innerHTML = `<li><a class="dropdown-item text-muted text-center py-3">No new invites</a></li>`;
        }
    }
});

connection.onreconnecting((error) => {
    console.warn("⚠️ SignalR reconnecting...", error);
});

connection.onreconnected((connectionId) => {
    console.log("✅ SignalR reconnected. ConnectionId:", connectionId);
    connection.invoke("GetInitialCount").catch(err => console.error("❌ Error invoking GetInitialCount:", err));
});

connection.onclose((error) => {
    console.error("❌ SignalR connection closed", error);
});

function startConnection() {
    connection.start()
        .then(function () {
            console.log("✅ SignalR: Connected successfully!");
            return connection.invoke("GetInitialCount");
        })
        .then(function () {
            console.log("✅ GetInitialCount invoked successfully");
        })
        .catch(function (err) {
            console.error("❌ SignalR connection error:", err);
            setTimeout(startConnection, 5000);
        });
}

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", startConnection);
} else {
    startConnection();
}

