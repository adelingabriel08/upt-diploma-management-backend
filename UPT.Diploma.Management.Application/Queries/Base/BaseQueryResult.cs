using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.Queries.Base;

public class BaseQueryResult<TViewModel> where TViewModel : ViewModelBase
{
    public TViewModel QueryPayload { get; set; }
}