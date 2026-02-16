namespace RehearsalHub.Web.ViewModels.Song
{
    public class SongDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string? Genre { get; set; }
        public string? Duration { get; set; }
        public string? MusicalKey { get; set; }
        public int? Tempo { get; set; }
        public bool IsPrivate { get; set; }

        public int? OwnerBandId { get; set; }
        public string? OwnerBandName { get; set; }
        public string? CreatorId { get; set; }
        public bool CanEdit { get; set; } 

        public IEnumerable<SongSetlistViewModel> IncludedInSetlists { get; set; } = new List<SongSetlistViewModel>();
    }
}