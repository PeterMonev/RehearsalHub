using RehearsalHub.Web.ViewModels.Bands;
using RehearsalHub.Web.ViewModels.Shared;

public class BandDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public bool IsOwner { get; set; }

    public List<BandMemberViewModel> Members { get; set; } = new List<BandMemberViewModel>();

    public List<RehearsalInfoViewModel> UpcomingRehearsals { get; set; } = new List<RehearsalInfoViewModel>();

    public List<BandSetlistViewModel> Setlists { get; set; } = new List<BandSetlistViewModel>();

}