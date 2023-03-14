using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.DeleteTopicCmd;

public class DeleteTopicCmdValidator : AbstractValidator<DeleteTopicCmd>
{
    public DeleteTopicCmdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id-ul temei nu poate fi null!")
            .NotEmpty().WithMessage("Id-ul temei nu poate fi gol!")
            .GreaterThan(0).WithMessage("Id-ul temei este invalid!");
    }
}