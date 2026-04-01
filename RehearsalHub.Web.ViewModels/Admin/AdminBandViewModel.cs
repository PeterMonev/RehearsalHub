namespace RehearsalHub.Web.ViewModels.Admin
{
    /// <summary>
    /// ViewModel for displaying a band summary in the admin panel.
    /// </summary>
    public class AdminBandViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public int MemberCount { get; set; }

        public int RehearsalCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
