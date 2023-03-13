using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.CreateTopicCmd;

public class CreateTopicCmdValidator : AbstractValidator<CreateTopicCmd>
{
    public CreateTopicCmdValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Numele temei nu poate fi null!")
            .NotEmpty().WithMessage("Numele temei nu poate fi gol!")
            .MaximumLength(200).WithMessage("Numele temei nu poate depăși 200 de caractere!");
        
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Descrierea temei nu poate fi null!")
            .NotEmpty().WithMessage("Descrierea temei nu poate fi gol!")
            .MaximumLength(2000).WithMessage("Descrierea temei nu poate depăși 2000 de caractere!");
        
        RuleFor(x => x.FacultyId)
            .GreaterThan(0).WithMessage("Id-ul facultății este invalid!");
    }
}