using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Exceptions;

namespace UPT.Diploma.Management.Application.Commands.CreateTopicCmd;

public class CreateTopicCmdHandler : IRequestHandler<CreateTopicCmd, BaseResult>
{
    public async Task<BaseResult> Handle(CreateTopicCmd request, CancellationToken cancellationToken)
    {
        var validator = new CreateTopicCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }
        
        // TODO add logic here
    }
}