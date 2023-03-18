using FluentValidation;

namespace UPT.Diploma.Management.Application.Queries.GetTopicsQuery;

public class GetTopicsQueryValidator : AbstractValidator<GetTopicsQuery>
{
    public GetTopicsQueryValidator()
    {
        RuleFor(x => x.Skip)
            .NotNull().WithMessage("Numarul de topic-uri skip-uite nu poate fi null!")
            .GreaterThanOrEqualTo(0).WithMessage("Numarul de topic-uri skip-uite este invalid!");
        
        RuleFor(x => x.Take)
            .NotNull().WithMessage("Numarul de topic-uri nu poate fi null!")
            .NotEmpty().WithMessage("Numarul de topic-uri nu poate fi gol!")
            .GreaterThan(0).WithMessage("Numarul de topic-uri este invalid!");
            
    }
}