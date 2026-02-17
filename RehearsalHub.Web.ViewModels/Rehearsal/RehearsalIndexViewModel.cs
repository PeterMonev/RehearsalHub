namespace RehearsalHub.Web.ViewModels.Rehearsal
{
    public class RehearsalIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BandName { get; set; } = null!;
        public string SetlistName { get; set; } = "No Setlist";
        public DateTime StartRehearsal { get; set; }
        public DateTime EndRehearsal { get; set; }
        public string Duration { get; set; } = "0h 0m";
        public string FormattedDate { get; set; } = string.Empty;
        public string FormattedTime { get; set; } = string.Empty;
        public bool IsUpcoming { get; set; }
        public bool IsHappeningNow { get; set; }
    }
}
