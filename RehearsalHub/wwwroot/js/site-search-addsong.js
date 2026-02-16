
    const addSongsFilters = {
        search: '',
    genre: 'all',
    tempo: 'all',
    show: 'available'
        };

    const addSongsSearch = document.getElementById('addSongsSearch');
    const addSongsClearSearch = document.getElementById('addSongsClearSearch');
    const addSongCards = document.querySelectorAll('.add-song-card');
    const addSongsNoResults = document.getElementById('addSongsNoResults');
    const addSongsSelectAll = document.getElementById('addSongsSelectAll');
    const addSongChecks = document.querySelectorAll('.add-song-check');
    const addSongsSelected = document.getElementById('addSongsSelected');
    const addSongsSubmitCount = document.getElementById('addSongsSubmitCount');
    const addSongsCount = document.getElementById('addSongsCount');
    const addSongsActiveFilters = document.getElementById('addSongsActiveFilters');
    const addSongsFilterBadges = document.getElementById('addSongsFilterBadges');
    const addSongsClearAll = document.getElementById('addSongsClearAll');
    const addSongsFilterBtns = document.querySelectorAll('.add-songs-filter');

    addSongsSearch.addEventListener('input', function() {
        addSongsFilters.search = this.value.toLowerCase();
    applyAddSongsFilters();
        });

    addSongsClearSearch.addEventListener('click', function() {
        addSongsSearch.value = '';
    addSongsFilters.search = '';
    applyAddSongsFilters();
        });

        addSongsFilterBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();

            const type = this.dataset.type;
            const value = this.dataset.filter;

            document.querySelectorAll(`[data-type="${type}"].add-songs-filter`).forEach(b => {
                b.classList.remove('active');
                if (type === 'genre') {
                    b.classList.remove('btn-primary');
                    b.classList.add('btn-outline-primary');
                } else if (type === 'tempo') {
                    b.classList.remove('btn-success');
                    b.classList.add('btn-outline-success');
                } else if (type === 'show') {
                    b.classList.remove('btn-secondary');
                    b.classList.add('btn-outline-secondary');
                }
            });

            this.classList.add('active');
            if (type === 'genre') {
                this.classList.remove('btn-outline-primary');
                this.classList.add('btn-primary');
            } else if (type === 'tempo') {
                this.classList.remove('btn-outline-success');
                this.classList.add('btn-success');
            } else if (type === 'show') {
                this.classList.remove('btn-outline-secondary');
                this.classList.add('btn-secondary');
            }

            addSongsFilters[type] = value;
            applyAddSongsFilters();
        });
        });

    addSongsClearAll.addEventListener('click', function() {
        addSongsFilters.search = '';
    addSongsFilters.genre = 'all';
    addSongsFilters.tempo = 'all';
    addSongsFilters.show = 'available';
    addSongsSearch.value = '';

            addSongsFilterBtns.forEach(btn => {
                const type = btn.dataset.type;
    const value = btn.dataset.filter;
    if ((type === 'genre' && value === 'all') ||
    (type === 'tempo' && value === 'all') ||
    (type === 'show' && value === 'available')) {
        btn.click();
                }
            });
        });

    function applyAddSongsFilters() {
        let visible = 0;

            addSongCards.forEach(card => {
                const title = card.dataset.title || '';
    const artist = card.dataset.artist || '';
    const genre = card.dataset.genre || '';
    const tempo = card.dataset.tempo || '';
    const added = card.dataset.added === 'true';

    let show = true;

    if (addSongsFilters.search && !title.includes(addSongsFilters.search) && !artist.includes(addSongsFilters.search)) {
        show = false;
                }
    if (addSongsFilters.genre !== 'all' && genre !== addSongsFilters.genre) {
        show = false;
                }
    if (addSongsFilters.tempo !== 'all' && tempo !== addSongsFilters.tempo) {
        show = false;
                }
    if (addSongsFilters.show === 'available' && added) {
        show = false;
                } else if (addSongsFilters.show === 'added' && !added) {
        show = false;
                }

    if (show) {
        card.classList.remove('d-none');
    visible++;
                } else {
        card.classList.add('d-none');
                }
            });

    addSongsCount.textContent = visible;
            addSongsNoResults.classList.toggle('d-none', visible > 0);
    updateAddSongsFilterBadges();
    updateAddSongsSelectAll();
        }
    function updateAddSongsFilterBadges() {
            const badges = [];
    if (addSongsFilters.search) {
        badges.push(`<span class="badge bg-primary px-3 py-2"><i class="fa-solid fa-search me-1"></i>${addSongsFilters.search}</span>`);
            }
    if (addSongsFilters.genre !== 'all') {
        badges.push(`<span class="badge bg-primary px-3 py-2"><i class="fa-solid fa-guitar me-1"></i>${addSongsFilters.genre}</span>`);
            }
    if (addSongsFilters.tempo !== 'all') {
        badges.push(`<span class="badge bg-success px-3 py-2"><i class="fa-solid fa-gauge-high me-1"></i>${addSongsFilters.tempo.toUpperCase()}</span>`);
            }
    if (addSongsFilters.show !== 'available') {
        badges.push(`<span class="badge bg-secondary px-3 py-2"><i class="fa-solid fa-eye me-1"></i>${addSongsFilters.show}</span>`);
            }

    addSongsFilterBadges.innerHTML = badges.join('');
    addSongsActiveFilters.classList.toggle('d-none', badges.length === 0);
        }

    let addSongsAllSelected = false;

    addSongsSelectAll.addEventListener('click', function() {
            const visibleChecks = Array.from(addSongChecks).filter(cb => {
                return !cb.closest('.add-song-card').classList.contains('d-none');
            });

    addSongsAllSelected = !addSongsAllSelected;
            visibleChecks.forEach(cb => cb.checked = addSongsAllSelected);

    this.innerHTML = addSongsAllSelected
    ? '<i class="fa-solid fa-xmark me-1"></i>Deselect All'
    : '<i class="fa-solid fa-check-double me-1"></i>Select All Visible';

    updateAddSongsSelection();
        });

    function updateAddSongsSelectAll() {
            const visibleChecks = Array.from(addSongChecks).filter(cb => {
                return !cb.closest('.add-song-card').classList.contains('d-none');
            });

            addSongsAllSelected = visibleChecks.length > 0 && visibleChecks.every(cb => cb.checked);
    addSongsSelectAll.innerHTML = addSongsAllSelected
    ? '<i class="fa-solid fa-xmark me-1"></i>Deselect All'
    : '<i class="fa-solid fa-check-double me-1"></i>Select All Visible';
        }

        addSongChecks.forEach(cb => {
        cb.addEventListener('change', function () {
            updateAddSongsSelection();
            updateAddSongsSelectAll();
        });
        });

    function updateAddSongsSelection() {
            const count = document.querySelectorAll('.add-song-check:checked').length;
    addSongsSelected.textContent = count === 1 ? '1 selected' : count + ' selected';
    addSongsSubmitCount.textContent = count;

            if (count > 0) {
        addSongsSelected.classList.remove('bg-success', 'bg-opacity-25', 'text-success');
    addSongsSelected.classList.add('bg-success', 'text-white');
            } else {
        addSongsSelected.classList.remove('bg-success', 'text-white');
    addSongsSelected.classList.add('bg-success', 'bg-opacity-25', 'text-success');
            }
        }

    updateAddSongsSelection();
    applyAddSongsFilters();