// Search functionality
const searchInput = document.getElementById('searchInput');
const songItems = document.querySelectorAll('.song-item');
const noResults = document.getElementById('noResults');

searchInput.addEventListener('input', function () {
    const searchTerm = this.value.toLowerCase();
    let visibleCount = 0;

    songItems.forEach(item => {
        const title = item.dataset.title;
        const artist = item.dataset.artist;

        if (title.includes(searchTerm) || artist.includes(searchTerm)) {
            item.classList.remove('d-none');
            visibleCount++;
        } else {
            item.classList.add('d-none');
        }
    });

    if (visibleCount === 0) {
        noResults.classList.remove('d-none');
    } else {
        noResults.classList.add('d-none');
    }
});

// Select All functionality
const selectAllBtn = document.getElementById('selectAllBtn');
const checkboxes = document.querySelectorAll('.song-checkbox');
const selectionCount = document.getElementById('selectionCount');

let allSelected = false;

selectAllBtn.addEventListener('click', function () {
    allSelected = !allSelected;
    checkboxes.forEach(checkbox => {
        checkbox.checked = allSelected;
    });
    this.innerHTML = allSelected
        ? '<i class="fa-solid fa-xmark me-1"></i>Deselect All'
        : '<i class="fa-solid fa-check-double me-1"></i>Select All';
    updateSelectionCount();
});

// Update selection count
checkboxes.forEach(checkbox => {
    checkbox.addEventListener('change', updateSelectionCount);
});

function updateSelectionCount() {
    const count = document.querySelectorAll('.song-checkbox:checked').length;
    selectionCount.textContent = count === 1 ? '1 selected' : count + ' selected';
}

// Initial count
updateSelectionCount();