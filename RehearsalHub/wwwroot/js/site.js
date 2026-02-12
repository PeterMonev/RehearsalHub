// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Band Edit Image
document.getElementById('imageUrlInput').addEventListener('input', function () {
    const preview = document.getElementById('preview');
    if (preview) {
        preview.src = this.value;
    }
});
