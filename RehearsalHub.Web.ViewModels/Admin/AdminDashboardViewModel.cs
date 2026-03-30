namespace RehearsalHub.Web.ViewModels.Admin
{
    /// <summary>
    /// ViewModel for the admin dashboard overview page.
    /// Contains aggregate statistics shown at the top of the panel.
    /// </summary>
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }

        public int TotalBands { get; set; }

        public int TotalSongs { get; set; }

        public int TotalRehearsals { get; set; }

        public int TotalSetlists { get; set; }

        public int NewUsersThisMonth { get; set; }

        public int ActiveBands { get; set; }
    }
}
