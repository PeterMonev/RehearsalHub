namespace RehearsalHub.Web.ViewModels.Rehearsal
{
    public class RehearsalDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartRehearsal { get; set; }
        public DateTime EndRehearsal { get; set; }
        public string? Notes { get; set; }
        public int BandId { get; set; }
        public string BandName { get; set; } = null!;
        public int? SetlistId { get; set; }
        public string? SetlistName { get; set; }
        public string Duration { get; set; } = "0h 0m";
        public string FormattedDate { get; set; } = string.Empty;
        public string FormattedStartTime { get; set; } = string.Empty;
        public string FormattedEndTime { get; set; } = string.Empty;
        public bool IsUpcoming { get; set; }
        public bool IsHappeningNow { get; set; }

        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public List<RehearsalSongViewModel> Songs { get; set; } = new();
    }
}