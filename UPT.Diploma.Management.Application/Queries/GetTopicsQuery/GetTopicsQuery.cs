using MediatR;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.GetTopicsQuery;

public class GetTopicsQuery : IRequest<BaseQueryResult<List<TopicViewModel>>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
}