using FluentValidation;

namespace UPT.Diploma.Management.Application.Queries.TestFlowQuery;

public class TestFlowQueryValidator : AbstractValidator<TestFlowQuery>
{
    public TestFlowQueryValidator()
    {
        RuleFor(x => x.Message).NotNull()
            .WithMessage("Message cannot be null");
        
        RuleFor(x => x.Message).NotEmpty()
            .WithMessage("Message cannot be empty");
        
        RuleFor(x => x.Message).MaximumLength(20)
            .WithMessage("Message cannot be larger than 20 characters");
    }
}