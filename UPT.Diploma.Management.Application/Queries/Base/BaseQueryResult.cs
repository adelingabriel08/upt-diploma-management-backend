using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.Queries.Base;

public class BaseQueryResult<TViewModel> where TViewModel : class
{
    public TViewModel QueryPayload { get; set; }
}