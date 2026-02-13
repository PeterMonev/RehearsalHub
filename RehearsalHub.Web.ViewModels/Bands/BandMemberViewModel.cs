namespace RehearsalHub.Web.ViewModels.Bands
{
    public class BandMemberViewModel
    {
        public string FullName { get; set; } = null!;

        public string Instrument { get; set; } = null!;

        public bool IsLeader { get; set; }

        public string UserId { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;
    }
}