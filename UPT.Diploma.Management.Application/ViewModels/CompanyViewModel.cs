using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.ViewModels;

public record CompanyViewModel : ViewModelBase
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string ShortDescription { get; set; }
    public string UserId { get; set; }
    public UserViewModel? User { get; set; }
}