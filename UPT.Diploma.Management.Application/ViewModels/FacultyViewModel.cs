using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.ViewModels;

public record FacultyViewModel : ViewModelBase
{
    public string Name { get; init; }
    public string ShortName { get; init; }
}