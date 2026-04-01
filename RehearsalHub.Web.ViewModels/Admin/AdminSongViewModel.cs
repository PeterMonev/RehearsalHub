namespace RehearsalHub.Web.ViewModels.Admin
{
    /// <summary>
    /// ViewModel for displaying a song summary in the admin panel.
    /// </summary>
    public class AdminSongViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public string Duration { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string MusicalKey { get; set; } = null!;

        public int? Tempo { get; set; }

        public bool IsPrivate { get; set; }

        public string CreatorName { get; set; } = null!;

        public string? OwnerBandName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TempoDisplay => Tempo.HasValue ? $"{Tempo} BPM" : "N/A";

        public string VisibilityDisplay => IsPrivate ? "Private" : "Public";
    }
}
