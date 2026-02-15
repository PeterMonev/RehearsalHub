document.addEventListener('DOMContentLoaded', function () {
    const container = document.getElementById('tempdata-container');
    if (!container) return;

    const successMsg = container.dataset.success;
    const errorMsg = container.dataset.error;

    if (successMsg) {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: successMsg,
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true
        });
    }

    if (errorMsg) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: errorMsg,
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 4000,
            timerProgressBar: true
        });
    }
});
