using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.LoginCommand;

public class LoginCmdValidator : AbstractValidator<LoginCmd>
{
    public LoginCmdValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
            .WithMessage("Email-ul nu este valid!");
        
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Parola nu poate fi null!")
            .NotEmpty().WithMessage("Parola invalidă!");
    }
}