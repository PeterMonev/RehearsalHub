namespace RehearsalHub.Web.ViewModels.Admin
{
    /// <summary>
    /// ViewModel for displaying a user summary in the admin panel.
    /// </summary>
    public class AdminUserViewModel
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int BandCount { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
