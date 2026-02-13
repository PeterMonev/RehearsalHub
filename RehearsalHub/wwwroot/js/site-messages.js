document.addEventListener('DOMContentLoaded', function () {
    const container = document.getElementById('toast-notifications');
    if (!container) return;

    const successMsg = container.dataset.success;
    const errorMsg = container.dataset.error;

    const commonConfig = {
        borderRadius: '15px',
        timer: 3000,
        timerProgressBar: true
    };

    if (successMsg) {
        Swal.fire({
            ...commonConfig,
            title: 'Success!',
            text: successMsg,
            icon: 'success',
            showConfirmButton: false
        });
    }

    if (errorMsg) {
        Swal.fire({
            ...commonConfig,
            title: 'Error!',
            text: errorMsg,
            icon: 'error',
            confirmButtonColor: '#dc3545',
            timer: null
        });
    }
});