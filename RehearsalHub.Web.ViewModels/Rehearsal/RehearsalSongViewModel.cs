namespace RehearsalHub.Web.ViewModels.Rehearsal
{
    public class RehearsalSongViewModel
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int? Tempo { get; set; }
    }
}
