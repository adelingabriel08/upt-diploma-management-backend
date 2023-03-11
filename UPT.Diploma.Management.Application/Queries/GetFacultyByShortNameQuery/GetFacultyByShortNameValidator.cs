using FluentValidation;

namespace UPT.Diploma.Management.Application.Queries.GetFacultyByShortNameQuery;

public class GetFacultyByShortNameValidator : AbstractValidator<GetFacultyByShortNameQuery>
{
    public GetFacultyByShortNameValidator()
    {
        RuleFor(x => x.ShortName).NotNull()
            .WithMessage("Numele facultății nu poate fi null!");
        
        RuleFor(x => x.ShortName).NotEmpty()
            .WithMessage("Numele facultății nu poate fi gol!");
        
        RuleFor(x => x.ShortName).MaximumLength(5)
            .WithMessage("Numele facultății nu poate depăși 5 litere!");
    }
}