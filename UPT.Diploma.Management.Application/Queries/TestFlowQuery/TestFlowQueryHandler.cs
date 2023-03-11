using MediatR;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.TestFlowQuery;

public class TestFlowQueryHandler : IRequestHandler<TestFlowQuery, BaseQueryResult<TestFlowViewModel>>
{
    public async Task<BaseQueryResult<TestFlowViewModel>> Handle(TestFlowQuery request,
        CancellationToken cancellationToken)
        => new BaseQueryResult<TestFlowViewModel>()
            { QueryPayload = new TestFlowViewModel() { Message = request.Message } };
}