using RehearsalHub.Web.ViewModels.Song;

public class SongPagedViewModel
{
    public IEnumerable<SongIndexViewModel> Songs { get; set; } = new List<SongIndexViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? SearchTerm { get; set; }
    public int? BandId { get; set; }
    public string? Genre { get; set; }
    public string? Key { get; set; }
    public string? Tempo { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasActiveFilters =>
        !string.IsNullOrEmpty(SearchTerm) ||
        !string.IsNullOrEmpty(Genre) ||
        !string.IsNullOrEmpty(Key) ||
        !string.IsNullOrEmpty(Tempo);
}