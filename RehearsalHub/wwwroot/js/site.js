// 1. Band Image Preview (Safe selector)
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('imageUrlInput')?.addEventListener('input', function () {
        const preview = document.getElementById('preview');
        if (preview) {
            preview.src = this.value;
        }
    });
});

// 2. SignalR Configuration & Initialization
(function () {
    const configEl = document.getElementById('signalr-config');
    if (!configEl) return;

    const hubUrl = configEl.dataset.hubUrl;
    const invitationUrl = configEl.dataset.invitationUrl;

    console.log("Initializing SignalR connection to:", hubUrl);

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

    let lastCount = -1;

    connection.on("UpdateInviteCount", function (count) {
        console.log("✅ SignalR: Received invite count:", count);

        const badge = document.getElementById("notification-badge");
        const wrapper = document.getElementById("notifications-wrapper");

        if (badge) {
            badge.innerText = count;
            count > 0 ? badge.classList.remove("d-none") : badge.classList.add("d-none");
        }

        if (wrapper) {
            if (count > 0) {
                wrapper.innerHTML = `<li><a class="dropdown-item text-center py-2" href="${invitationUrl}"><span class="badge bg-danger">${count}</span> Pending Invitations</a></li>`;
            } else {
                wrapper.innerHTML = `<li><a class="dropdown-item text-muted text-center py-3">No new invites</a></li>`;
            }
        }

        if (lastCount !== -1 && count > lastCount) {
            Swal.fire({
                toast: true,
                position: 'bottom-end',
                icon: 'info',
                title: 'You have a new band invitation! 🎸',
                showConfirmButton: false,
                timer: 4000
            });
        }
        lastCount = count;
    });

    function startConnection() {
        connection.start()
            .then(() => {
                console.log("✅ SignalR: Connected!");
                connection.invoke("GetInitialCount").catch(err => console.error(err));
            })
            .catch(err => {
                console.error("❌ SignalR Connection Error:", err);
                setTimeout(startConnection, 5000);
            });
    }

    connection.onreconnected(() => {
        connection.invoke("GetInitialCount");
    });

    startConnection();
})();