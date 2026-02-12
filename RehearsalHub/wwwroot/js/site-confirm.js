function initializeDeleteConfirmation() {
    const deleteButtons = document.querySelectorAll('.btn-delete-confirm');

    deleteButtons.forEach(button => {
        button.replaceWith(button.cloneNode(true));
    });

    document.querySelectorAll('.btn-delete-confirm').forEach(button => {
        button.addEventListener('click', function (e) {
            const form = this.closest('form');
            const message = this.getAttribute('data-confirm-message') || "This action cannot be undone!";
            const title = this.getAttribute('data-confirm-title') || "Are you sure?";

            Swal.fire({
                title: title,
                text: message,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Yes, do it!',
                cancelButtonText: 'Cancel',
                borderRadius: '15px'
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
        });
    });
}

document.addEventListener('DOMContentLoaded', initializeDeleteConfirmation);

