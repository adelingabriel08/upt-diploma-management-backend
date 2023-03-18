using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.CreateApplicationCmd;

public class CreateApplicationCmdValidator : AbstractValidator<CreateApplicationCmd>
{
    public CreateApplicationCmdValidator()
    {
        RuleFor(x => x.Observations)
            .MaximumLength(500).WithMessage("Observațiile nu pot depăși 500 de caractere!");
        
    }
}