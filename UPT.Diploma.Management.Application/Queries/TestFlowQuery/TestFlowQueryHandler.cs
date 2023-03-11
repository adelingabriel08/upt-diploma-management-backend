using MediatR;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.TestFlowQuery;

public class TestFlowQueryHandler : IRequestHandler<TestFlowQuery, BaseQueryResult<TestFlowViewModel>>
{
    public async Task<BaseQueryResult<TestFlowViewModel>> Handle(TestFlowQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new TestFlowQueryValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }
        
        // insert logic here
        
        return new BaseQueryResult<TestFlowViewModel>()
            { QueryPayload = new TestFlowViewModel() { Message = request.Message } };
    }
}