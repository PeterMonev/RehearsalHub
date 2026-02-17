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

//Rehearsal
(function () {

    function getNowForInput() {
        const now = new Date();
        now.setSeconds(0, 0);
        return now.toISOString().slice(0, 16);
    }

    const startInput = document.getElementById('rehearsalStart');
    const endInput = document.getElementById('rehearsalEnd');
    const nowStr = getNowForInput();

    startInput.min = nowStr;
    endInput.min = nowStr;


    startInput.addEventListener('change', function () {
        const startVal = this.value;

        if (startVal) {

            endInput.min = startVal;


            if (endInput.value && endInput.value <= startVal) {
                const startDate = new Date(startVal);
                startDate.setHours(startDate.getHours() + 1);
                endInput.value = startDate.toISOString().slice(0, 16);
            }
        }
    });


    const form = startInput.closest('form');
    form.addEventListener('submit', function (e) {
        const startVal = startInput.value;
        const endVal = endInput.value;

        if (!startVal) return;

        const startDate = new Date(startVal);
        const now = new Date();

        if (startDate < now) {
            e.preventDefault();
            startInput.classList.add('is-invalid');


            let feedback = startInput.nextElementSibling;
            if (!feedback || !feedback.classList.contains('invalid-feedback')) {
                feedback = document.createElement('div');
                feedback.classList.add('invalid-feedback');
                startInput.parentNode.insertBefore(feedback, startInput.nextSibling);
            }
            feedback.textContent = 'Rehearsal must be scheduled from now onwards.';
            startInput.scrollIntoView({ behavior: 'smooth', block: 'center' });
            return;
        }

        if (endVal && endVal <= startVal) {
            e.preventDefault();
            endInput.classList.add('is-invalid');

            let feedback = endInput.nextElementSibling;
            if (!feedback || !feedback.classList.contains('invalid-feedback')) {
                feedback = document.createElement('div');
                feedback.classList.add('invalid-feedback');
                endInput.parentNode.insertBefore(feedback, endInput.nextSibling);
            }
            feedback.textContent = 'End time must be after start time.';
            endInput.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    });


    startInput.addEventListener('change', () => startInput.classList.remove('is-invalid'));
    endInput.addEventListener('change', () => endInput.classList.remove('is-invalid'));

})();