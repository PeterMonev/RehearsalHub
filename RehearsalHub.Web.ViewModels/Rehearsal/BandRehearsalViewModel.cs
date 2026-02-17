namespace RehearsalHub.Web.ViewModels.Rehearsal
{
    public class BandRehearsalViewModel
    {
        public int Id { get; set; }
        public string SetlistName { get; set; } = "No Setlist";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
