using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.ViewModels;

public record TestFlowViewModel : ViewModelBase
{
    public string Message { get; init; }
}