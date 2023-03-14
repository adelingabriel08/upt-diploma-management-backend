using FluentValidation;

namespace UPT.Diploma.Management.Application.Queries.GetCompanyByIdQuery;

public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id-ul companiei nu poate fi null!")
            .NotEmpty().WithMessage("Id-ul companiei nu poate fi gol!")
            .GreaterThan(0).WithMessage("Id-ul companiei este invalid!");
    }
}