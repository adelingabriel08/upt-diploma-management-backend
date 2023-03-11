using MediatR;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.TestFlowQuery;

public record TestFlowQuery : IRequest<BaseQueryResult<TestFlowViewModel>>
{
    public string Message { get; init; }
}