using RehearsalHub.Web.ViewModels.Admin;

namespace RehearsalHub.Areas.Admin.Data
{
    public interface IAdminService
    {
        public Task<AdminDashboardViewModel> GetDashboardStatsAsync();
    }
}
