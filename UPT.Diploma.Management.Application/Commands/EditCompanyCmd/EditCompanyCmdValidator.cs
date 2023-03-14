using FluentValidation;

namespace UPT.Diploma.Management.Application.Commands.EditCompanyCmd;

public class EditCompanyCmdValidator : AbstractValidator<EditCompanyCmd>
{
    public EditCompanyCmdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id-ul companiei nu poate fi null!")
            .NotEmpty().WithMessage("Id-ul companiei nu poate fi gol!")
            .GreaterThan(0).WithMessage("Id-ul companiei este invalid!");
        
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Numele companiei nu poate fi null!")
            .NotEmpty().WithMessage("Numele companiei nu poate fi gol!")
            .MaximumLength(150).WithMessage("Numele companiei nu poate depăși 150 de caractere!");
        
        RuleFor(x => x.ShortDescription)
            .NotNull().WithMessage("Descrierea companiei nu poate fi null!")
            .NotEmpty().WithMessage("Descrierea companiei nu poate fi gol!")
            .MaximumLength(500).WithMessage("Descrierea companiei nu poate depăși 500 de caractere!");
        
        RuleFor(x => x.LogoUrl)
            .NotNull().WithMessage("Link-ul logo-ului nu poate fi null!")
            .NotEmpty().WithMessage("Link-ul logo-ului nu poate fi gol!")
            .MaximumLength(2000).WithMessage("Link-ul logo-ului nu poate depăși 2000 de caractere!");
    }
}