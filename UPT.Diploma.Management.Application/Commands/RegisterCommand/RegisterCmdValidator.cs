using System.Text.RegularExpressions;
using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.RegisterCommand;

public class RegisterCmdValidator : AbstractValidator<RegisterCmd>
{
    public RegisterCmdValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
            .WithMessage("Prenumele nu poate fi null!");
        
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("Prenumele nu poate fi gol!");
        
        RuleFor(x => x.FirstName).MaximumLength(100)
            .WithMessage("Prenumele nu poate fi mai lung de 100 de caractere!");
        
        RuleFor(x => x.LastName).NotNull()
            .WithMessage("Numele nu poate fi null!");
        
        RuleFor(x => x.LastName).NotEmpty()
            .WithMessage("Numele nu poate fi gol!");
        
        RuleFor(x => x.LastName).MaximumLength(100)
            .WithMessage("Numele nu poate fi mai lung de 100 de caractere!");

        RuleFor(x => x.Email).EmailAddress()
            .WithMessage("Email-ul nu este valid!");
        
        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
            .NotNull().WithMessage("Numărul de telefon lipsește!")
            .MinimumLength(10).WithMessage("Numărul de telefon nu poate avea mai puțin de 10 caractere!")
            .MaximumLength(20).WithMessage("Numărul de telefon nu poate avea mai mult de 50 caractere!")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Numărul de telefon nu este valid!");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("Parola nu poate fi null!")
            .NotEmpty().WithMessage("Parola invalidă!");
    }
}